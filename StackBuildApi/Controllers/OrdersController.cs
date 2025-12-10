using Microsoft.AspNetCore.Mvc;
using StackBuilApi.Core.Interface.iservices;
using StackBuildApi.Core.DTO;
using StackBuildApi.Model;

namespace StackBuildApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService) => _orderService = orderService;

        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderDto dto)
        {
            var response = await _orderService.PlaceOrderAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
    }
}
