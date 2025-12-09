using Microsoft.AspNetCore.Mvc;
using StackBuilApi.Core.Interface.iservices;
using StackBuildApi.Core.DTO;

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
            // you can obtain customer id from token if you have auth
            var result = await _orderService.PlaceOrderAsync(dto);

            if (!result.Success)
                return BadRequest(new { error = result.Error });

            return CreatedAtAction(null, new { id = result.Order!.OrderId }, result.Order);
        }
    }
}
