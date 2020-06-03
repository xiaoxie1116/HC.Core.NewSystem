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

            //ע��Ҫͨ�����䴴�������
            var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location); //��ȡӦ�ó�������Ŀ¼�����ԣ����ܹ���Ŀ¼Ӱ�죬������ô˷�����ȡ·����
            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Version = "v1.0",
                    Title = "HC.WebAPI",
                    Description = "���̹��ú�̨�ӿ�",
                });
                // Ϊ Swagger JSON and UI����xml�ĵ�ע��·��
                var xmlPath = Path.Combine(basePath, "HC.NewSystem.WebApi.xml");
                c.IncludeXmlComments(xmlPath, true);
                var xmlModelPath = Path.Combine(basePath, "HC.Core.DTO.Models.xml"); //Modelʵ��ע��                  
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
