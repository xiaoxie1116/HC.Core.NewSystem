using Castle.DynamicProxy;
using HC.Common.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HC.Core.WebApi.AOP
{
    /// <summary>
    /// 日志AOP
    /// </summary>
    public class LoggerInterceptor : IInterceptor
    {
        /// <summary>
        /// 实现接口方法
        /// </summary>
        /// <param name="invocation"></param>
        public void Intercept(IInvocation invocation)
        {
            // 事前处理: 在服务方法执行之前,做相应的逻辑处理
            var dataIntercept = $"【当前执行方法】：{ invocation.Method.Name} \r\n 【携带的参数有】： {string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray())} \r\n";

            try
            {
                // 执行当前访问的服务方法,(注意:如果下边还有其他的AOP拦截器的话,会跳转到其他的AOP里)
                invocation.Proceed();
                //异步方法
                if (IsAsyncMethod(invocation.Method))
                {
                    if (invocation.Method.ReturnType == typeof(Task))
                    {
                        invocation.ReturnValue = InternalAsyncHelper.AwaitTaskWithPostActionAndFinally(
                            (Task)invocation.ReturnValue,
                            async () => await SuccessAction(invocation, dataIntercept),/*成功时执行*/
                            ex =>
                            {
                                LoggerError(ex, dataIntercept);
                            });
                    }
                    //Task<TResult>
                    else
                    {
                        invocation.ReturnValue = InternalAsyncHelper.CallAwaitTaskWithPostActionAndFinallyAndGetResult(
                         invocation.Method.ReturnType.GenericTypeArguments[0],
                         invocation.ReturnValue,

                         async (o) => await SuccessAction(invocation, dataIntercept, o),/*成功时执行*/
                         ex =>
                         {
                             LoggerError(ex, dataIntercept);
                         });
                    }
                }
                else
                {
                    //同步
                    dataIntercept += ($"【执行完成结果】：{invocation.ReturnValue}");
                    Parallel.For(0, 1, e =>
                    {
                        LoggerLock.OutPutLogger("AOPLog", new string[] { dataIntercept });
                    });
                }


            }
            catch
            {


            }

            // 事后处理: 在service被执行了以后,做相应的处理,这里是输出到日志文件
            dataIntercept += ($"【执行完成结果】：{invocation.ReturnValue}");

            // 输出到日志文件
            Parallel.For(0, 1, e =>
            {
                LoggerLock.OutPutLogger("AOPLog", new string[] { dataIntercept });
            });
        }

        /// <summary>
        /// 判断是否是异步方法
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static bool IsAsyncMethod(MethodInfo method)
        {
            return (
                method.ReturnType == typeof(Task) ||
                (method.ReturnType.IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>))
                );
        }

        private void LoggerError(Exception ex, string dataIntercept)
        {
            if (ex != null)
            {
                //执行的 service 中，收录异常
                //MiniProfiler.Current.CustomTiming("Errors：", ex.Message);
                //执行的 service 中，捕获异常
                dataIntercept += ($"【执行完成结果】：方法中出现异常：{ex.Message + ex.InnerException}\r\n");


                //并行处理  异常日志里有详细的堆栈信息
                Parallel.For(0, 1, e =>
                {
                    LoggerLock.OutPutLogger("AOPLog", new string[] { dataIntercept });
                });
            }
        }


        private async Task SuccessAction(IInvocation invocation, string dataIntercept, object o = null)
        {
            invocation.ReturnValue = o;
            var type = invocation.Method.ReturnType;
            if (typeof(Task).IsAssignableFrom(type))
            {
                var resultProperty = type.GetProperty("Result");
                //类型错误 都可以不要invocation参数，直接将o系列化保存到日志中
                dataIntercept += ($"【执行完成结果】：{JsonConvert.SerializeObject(invocation.ReturnValue)}");
            }
            else
            {
                dataIntercept += ($"【执行完成结果】：{invocation.ReturnValue}");
            }

            await Task.Run(() =>
            {
                Parallel.For(0, 1, e =>
                {
                    LoggerLock.OutPutLogger("AOPLog", new string[] { dataIntercept });
                });
            });
        }

    }


    internal static class InternalAsyncHelper
    {
        public static async Task AwaitTaskWithPostActionAndFinally(Task actualReturnValue, Func<Task> postAction, Action<Exception> finalAction)
        {
            Exception exception = null;

            try
            {
                await actualReturnValue;
                await postAction();
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            finally
            {
                finalAction(exception);
            }
        }

        public static object CallAwaitTaskWithPostActionAndFinallyAndGetResult(Type taskReturnType, object actualReturnValue, Func<object, Task> action, Action<Exception> finalAction)
        {
            return typeof(InternalAsyncHelper)
                .GetMethod("AwaitTaskWithPostActionAndFinallyAndGetResult", BindingFlags.Public | BindingFlags.Static)
                .MakeGenericMethod(taskReturnType)  //可以用来指定泛型方法和泛型类的具体类型
                .Invoke(null, new object[] { actualReturnValue, action, finalAction });
        }

        public static async Task<T> AwaitTaskWithPostActionAndFinallyAndGetResult<T>(Task<T> actualReturnValue, Func<object, Task> postAction, Action<Exception> finalAction)
        {
            Exception exception = null;
            try
            {
                var result = await actualReturnValue;
                await postAction(result);
                return result;
            }
            catch (Exception ex)
            {
                exception = ex;
                throw;
            }
            finally
            {
                finalAction(exception);
            }
        }

    }
}
