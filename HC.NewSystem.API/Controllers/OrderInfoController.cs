using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HC.Core.DTO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HC.Core.IServices;

namespace HC.NewSystem.API.Controllers
{
    /// <summary>
    /// 订单控制器
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class OrderInfoController : ControllerBase
    {

        private readonly ILogger<OrderInfoController> _logger;

        private IOrderServices _services;

        public OrderInfoController(ILogger<OrderInfoController> logger, IOrderServices services)
        {
            _logger = logger;
            _services = services;
        }

        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<OrderVM> GetOrderByID(int OrderID)
        {
            var result = await _services.QueryById(OrderID);
            return result;
        }

    }
}
