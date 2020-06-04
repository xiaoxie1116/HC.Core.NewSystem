using HC.Common.Base.Services;
using HC.Core.DTO.Models;
using HC.Core.IRepository;
using HC.Core.IServices;
using System;
using System.Threading.Tasks;
using AutoMapper;
namespace HC.Core.Services
{
    /// <summary>
    /// 订单服务
    /// </summary>
    public class OrderServices : BaseServices<OrderVM>, IOrderServices
    {

        IOrderRepository _repository;
        IMapper _mapper;

        public OrderServices(IOrderRepository orderRepository, IMapper mapper)
        {
            _repository = orderRepository;
            _mapper = mapper;

        }

        /// <summary>
        /// 根据ID获取订单信息
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public async Task<OrderVM> GetEntityByID(int OrderID)
        {
            var entity = await _repository.QueryById(OrderID);
            return _mapper.Map<OrderVM>(entity);
        }


    }
}
