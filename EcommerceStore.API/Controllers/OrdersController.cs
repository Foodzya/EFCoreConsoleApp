using EcommerceStore.Application.Interfaces;
using EcommerceStore.Application.Models.InputModels;
using EcommerceStore.Application.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceStore.API.Controllers
{
    /// <summary>
    /// Order controller for managing orders using CRUD operations
    /// </summary>
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Get order by order id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        /// <response code="200">Returns when order is successfully obtained</response>
        [HttpGet("{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<OrderViewModel>> GetByIdAsync([FromRoute] int orderId)
        {
            var orderViewModel = await _orderService.GetOrderByIdAsync(orderId);

            return Ok(orderViewModel);
        }

        /// <summary>
        /// Get list of orders
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns when list of orders is successfully obtained</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<OrderViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<OrderViewModel>>> GetAllAsync()
        {
            var ordersViewModel = await _orderService.GetAllOrdersAsync();

            return Ok(ordersViewModel);
        }

        /// <summary>
        /// Creates an order
        /// </summary>
        /// <param name="orderInputModel"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///     POST 
        /// 
        ///     {
        ///         "modifiedDate": "2022-05-12T15:30:00",
        ///         "status": "In Review",
        ///         "userId": 1
        ///     }
        /// </remarks>
        /// <response code="200">Returns when order is successfully created</response>
        /// <response code="400">Returns when order input details are incorrect</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateAsync([FromBody] OrderInputModel orderInputModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _orderService.CreateOrderAsync(orderInputModel);

            return Ok();
        }

        /// <summary>
        /// Updates an existing order
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderInputModel"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///     PUT 
        /// 
        ///     {
        ///         "modifiedDate": "2022-05-12T15:30:00",
        ///         "status": "In Review",
        ///         "userId": 1
        ///     }
        /// </remarks>
        /// <response code="200">Returns when order is successfully updated</response>
        /// <response code="400">Returns when order input details are incorrect</response>        
        [HttpPut("{orderId}")]
        [ProducesResponseType(typeof(OrderViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateAsync([FromRoute] int orderId, [FromBody] OrderInputModel orderInputModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _orderService.UpdateOrderAsync(orderId, orderInputModel);

            return Ok();
        }

        /// <summary>
        /// Deletes an existing order
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        /// <response code="200">Returns when order is successfully deleted</response>
        [HttpDelete("{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> RemoveAsync([FromRoute] int orderId)
        {
            await _orderService.RemoveOrderAsync(orderId);

            return Ok();
        }
    }
}