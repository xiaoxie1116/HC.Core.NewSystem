using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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

        public OrderInfoController(ILogger<OrderInfoController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<string> GetOrderInfo()
        {
            return "订单信息请求接口";
        }

    }
}
