using HC.Common.Base.Services;
using HC.Core.DB.Entitys;
using HC.Core.DTO.Models;
using System;
using System.Threading.Tasks;

namespace HC.Core.IServices
{
    /// <summary>
    /// 订单服务
    /// </summary>
    public interface IOrderServices : IBaseServices<Order>
    {
        Task<OrderVM> GetEntityByID(int OrderID);
    }
}
