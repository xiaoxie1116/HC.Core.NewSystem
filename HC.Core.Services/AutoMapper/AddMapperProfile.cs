using AutoMapper;
using HC.Core.DB.Entitys;
using HC.Core.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HC.Core.Services
{
    public class AddMapperProfile : Profile  //services.AddAutoMapper是会自动找到所有继承了Profile的类然后进行配置
    {
        public AddMapperProfile()
        {
            CreateMap<Order, OrderVM>();
        }
    }
}
