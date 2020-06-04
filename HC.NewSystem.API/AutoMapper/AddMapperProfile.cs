using AutoMapper;
using HC.Core.DB.Entitys;
using HC.Core.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HC.NewSystem.WebApi
{
    /// <summary>
    /// 创建实体映射
    /// services.AddAutoMapper是会自动找到所有继承了Profile的类然后进行配置
    /// </summary>
    public class AddMapperProfile : Profile 
    {
        public AddMapperProfile()
        {
            CreateMap<Order, OrderVM>();
            CreateMap<OrderVM, Order>();
        }
    }
}
