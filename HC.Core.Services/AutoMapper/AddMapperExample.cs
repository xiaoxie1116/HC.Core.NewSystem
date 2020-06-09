using AutoMapper;
using HC.Core.DB.Entitys;
using HC.Core.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HC.Core.Services
{
    /// <summary>
    /// 创建实体映射
    /// services.AddAutoMapper是会自动找到所有继承了Profile的类然后进行配置
    /// 这个方法只能放在api项目中，不能放在services项目里面，因为 services.AddAutoMapper 找不到
    /// </summary>
    public class AddMapperExample : Profile
    {
        public AddMapperExample()
        {
            CreateMap<Order, OrderVM>();
            CreateMap<OrderVM, Order>();
        }
    }
}
