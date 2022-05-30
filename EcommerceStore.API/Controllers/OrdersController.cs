using EcommerceStore.API.Constants;
using EcommerceStore.Application.Exceptions;
using EcommerceStore.Application.Interfaces;
using EcommerceStore.Application.Models.InputModels;
using EcommerceStore.Application.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = $"{Roles.admin},{Roles.customer}")]
        [HttpGet("{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<OrderViewModel>> GetByIdAsync([FromRoute] int orderId)
        {
            var orderViewModel = await _orderService.GetOrderByIdAsync(orderId);

            return Ok(orderViewModel);
        }

        /// <summary>
        /// Get list of orders for specified user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <response code="200">Returns when list of orders is successfully obtained</response>
        [Authorize(Roles = $"{Roles.admin},{Roles.customer}")]
        [HttpGet("/users/{userId}")]
        [ProducesResponseType(typeof(List<OrderViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<OrderViewModel>>> GetAllAsync([FromRoute] int userId)
        {
            var ordersViewModel = await _orderService.GetAllOrdersForUserAsync(userId);

            return Ok(ordersViewModel);
        }

        /// <summary>
        /// Creates an order with number of products
        /// </summary>
        /// <param name="orderInputModel"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///     POST 
        /// {
        ///     "ProductsDetails":
        ///     [
        ///         {
        ///             "ProductId": 2,
        ///             "ProductAmount": 1
        ///         },
        ///         {
        ///             "ProductId": 1,
        ///             "ProductAmount": 3
        ///         }
        ///     ]
        ///  }
        /// </remarks>
        /// <response code="200">Returns when order is successfully created</response>
        /// <response code="400">Returns when order input details are incorrect</response>
        [Authorize(Roles = $"{Roles.admin},{Roles.customer}")]
        [HttpPost("/users/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateAsync([FromRoute] int userId, [FromBody] OrderInputModel orderInputModel)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);

            await _orderService.CreateOrderAsync(userId, orderInputModel);

            return Ok();
        }

        /// <summary>
        /// Updates an existing order
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        /// <response code="200">Returns when order is successfully updated</response>
        /// <response code="400">Returns when order input details are incorrect</response>        
        [Authorize(Roles = $"{Roles.admin},{Roles.customer}")]
        [HttpPut("{orderId}")]
        [ProducesResponseType(typeof(OrderViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateStatusAsync([FromRoute] int orderId)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);

            await _orderService.UpdateOrderAsync(orderId);

            return Ok();
        }

        /// <summary>
        /// Canceles an order
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        /// <response code="200">Returns when order is successfully canceled</response>
        [Authorize(Roles = $"{Roles.admin},{Roles.customer}")]
        [HttpDelete("{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> CancelAsync([FromRoute] int orderId)
        {
            await _orderService.CancelOrderAsync(orderId);

            return Ok();
        }

        /// <summary>
        /// Add new product to existing order
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderInputModel"></param>
        /// <returns></returns>
        /// <exception cref="ValidationException"></exception>
        /// <response code="200">Returns when product is successfully added to order</response>
        /// <response code="400">Returns when failed during adding product to order</response>
        [Authorize(Roles = $"{Roles.admin},{Roles.customer}")]
        [HttpPost("{orderId}/products")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddProductsAsync([FromRoute] int orderId, [FromBody] OrderInputModel orderInputModel)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);

            await _orderService.AddProductsToOrderAsync(orderId, orderInputModel);

            return Ok();
        }

        /// <summary>
        /// Remove product(-s) from existing order
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        [Authorize(Roles = $"{Roles.admin},{Roles.customer}")]
        [HttpDelete("{orderId}/products/{productId}")]
        public async Task<ActionResult> RemoveProductAsync([FromRoute] int orderId, [FromBody] int productId)
        {
            await _orderService.RemoveProductFromOrderAsync(orderId, productId);

            return Ok();
        }

    }
}