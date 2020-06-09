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
using AutoMapper;
using HC.Core.Repository;
using HC.Core.WebApi.Filters;

namespace HC.Core.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //��־AOPע��
            services.AddSingleton(new Common.Tools.LoggerLock(Env.ContentRootPath));
            //ע��Ҫͨ�����䴴�������
            var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location); //��ȡӦ�ó�������Ŀ¼�����ԣ����ܹ���Ŀ¼Ӱ�죬������ô˷�����ȡ·����
            // Swagger UI
            services.AddSwaggerSetup(basePath);
            // Automapper
            services.AddAutoMapperSetup();

            services.AddControllers(s =>
            {
                s.Filters.Add(typeof(GlobalExceptionFilter));

            });
        }

        /// <summary>
        /// Autofacע�ᣬע����Program.CreateHostBuilder�����Autofac���񹤳�
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModuleRegister());
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
                // ��swagger���ó���ҳ              
                c.RoutePrefix = "";  //·�����ã�����Ϊ�գ���ʾֱ���ڸ�������localhost:44305�����ʸ��ļ�,ע��localhost:44305/swagger�Ƿ��ʲ����ģ�ȥlaunchSettings.json��launchUrlȥ��                

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
