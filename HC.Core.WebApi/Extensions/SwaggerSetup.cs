using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HC.Core.WebApi
{
    /// <summary>
    /// 设置 SwaggerUI 组件
    /// </summary>
    public static class SwaggerSetup
    {
        public static void AddSwaggerSetup(this IServiceCollection services, string basePath)
        {
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
        }
    }
}
