using EcommerceStore.Application.Exceptions;
using EcommerceStore.Application.Interfaces;
using EcommerceStore.Application.Models.InputModels;
using EcommerceStore.Application.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceStore.API.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderViewModel>> GetByIdAsync([FromRoute] int orderId)
        {
            var orderViewModel = await _orderService.GetOrderByIdAsync(orderId);

            return Ok(orderViewModel);
        }
        
        [HttpGet]
        public async Task<ActionResult<List<OrderViewModel>>> GetAllAsync()
        {
            var ordersViewModel = await _orderService.GetAllOrdersAsync();

            return Ok(ordersViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] OrderInputModel orderInputModel)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);

            await _orderService.CreateOrderAsync(orderInputModel);

            return Ok();
        }

        [HttpPut("{orderId}")]
        public async Task<ActionResult> UpdateAsync([FromRoute] int orderId, [FromBody] OrderInputModel orderInputModel)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);

            await _orderService.UpdateOrderAsync(orderId, orderInputModel);

            return Ok();
        }

        [HttpDelete("{orderId}")]
        public async Task<ActionResult> RemoveAsync([FromRoute] int orderId)
        {
            await _orderService.RemoveOrderAsync(orderId);

            return Ok();
        }
    }
}