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
        public void ConfigureServices(IServiceCollection services)
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

            services.AddControllers();
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
