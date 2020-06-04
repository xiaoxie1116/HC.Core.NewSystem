using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using Autofac;
using System.Reflection;
using Autofac.Extras.DynamicProxy;
using Autofac.Extensions.DependencyInjection;

namespace HC.NewSystem.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            //注册要通过反射创建的组件
            var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location); //获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Version = "v1.0",
                    Title = "HC.WebAPI",
                    Description = "华程国旅后台接口",
                });
                // 为 Swagger JSON and UI设置xml文档注释路径
                var xmlPath = Path.Combine(basePath, "HC.NewSystem.WebApi.xml");
                c.IncludeXmlComments(xmlPath, true);
                var xmlModelPath = Path.Combine(basePath, "HC.Core.DTO.Models.xml"); //Model实体注释                  
                c.IncludeXmlComments(xmlModelPath);

            });
            #endregion

            #region Autofac

            var builder = new ContainerBuilder();

            //整个程序集的注入实现层级解耦，如果路径不对，请修改对应的生成路径
            var servicesDllFile = Path.Combine(basePath, "HC.Core.Services.dll");
            var assemblysServices = Assembly.LoadFrom(servicesDllFile);
            var cacheType = new List<Type>();

            builder.RegisterAssemblyTypes(assemblysServices).AsImplementedInterfaces()
                 .AsImplementedInterfaces()
                 .InstancePerLifetimeScope()
                 .EnableInterfaceInterceptors()//引用Autofac.Extras.DynamicProxy;
                 .InterceptedBy(cacheType.ToArray());//将拦截器添加到要注入容器的接口或者类之上。(可以直接替换拦截器)

            var repositoryDllFile = Path.Combine(basePath, "HY.Core.Repository.dll");
            var assemblysRepository = Assembly.LoadFrom(repositoryDllFile);
            builder.RegisterAssemblyTypes(assemblysRepository).AsImplementedInterfaces();
            #endregion
            //将services填充到Autofac容器生成器中
            builder.Populate(services);

            services.AddControllers();
            //使用已进行的组件登记创建新容器，判断是否注入到容器中，可以直接看看容器 ApplicationContainer  Registrations的内容
            var ApplicationContainer = builder.Build();
            return new AutofacServiceProvider(ApplicationContainer);//第三方IOC接管 core内置DI容器
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "HC.Core.NewSystem v1");
                // 将swagger设置成首页              
                c.RoutePrefix = "";  //路径配置，设置为空，表示直接在根域名（localhost:44305）访问该文件,注意localhost:44305/swagger是访问不到的，去launchSettings.json把launchUrl去掉                

            });
            #endregion

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
