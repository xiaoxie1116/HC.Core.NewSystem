using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HC.Core.DTO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HC.Core.IServices;

namespace HC.Core.WebApi.Controllers
{
    /// <summary>
    /// 订单控制器
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderInfoController : ControllerBase
    {
        private readonly ILogger<OrderInfoController> _logger;

        private readonly IOrderServices _services;

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
            if (OrderID <= 0) return null;
            var result = await _services.GetEntityByID(OrderID);
            return result;
        }

        /// <summary>
        /// 测试log4net
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> TestLog4net()
        {
            _logger.LogInformation($"LogInformation-- 信息: {DateTime.Now}");
            _logger.LogWarning($"LogWarning-- 警告: {DateTime.Now}");
            _logger.LogError($"LogError-- 错误: {DateTime.Now}");
            _logger.LogDebug($"LogDebug-- **debug**: {DateTime.Now}");

            int a = 0, b = 1;
            var c = b / a;

            return await Task.Run(() =>
            {
                return "测试ok";
            });

        }

    }
}
