using Contracts.IService;
using Entities.Dtos.RequestDto;
using Entities.Dtos.ResponseDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Net;

namespace OrderService.Controllers
{
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IServices _service;
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;
        public OrderController(IServices Service, ILogger<OrderController> logger, IOrderService orderService)
        {
            _service = Service;
            _logger = logger;
            _orderService = orderService;
        }
        /// <summary>
        /// Add items to cart
        /// </summary>
        /// <param name="cart"></param>
        [HttpPost]
        [Authorize]
        [Route("api/order")]
        public IActionResult CreateOrder([FromBody] OrderFromCartDto order)
        {
            _logger.LogInformation("Creating your order");
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid data");
                ErrorDto badRequest = _service.ModelState(ModelState);
                return BadRequest(badRequest);
            }
            try
            {
           string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer", "");
                ResponseDto Id = _orderService.CreateOrder(order,token);
                _logger.LogInformation("Order Created successfully");
                return Created("Order created", Id);
            }
            catch (NotFoundException e)
            {
                _logger.LogDebug($"",e);
                return NotFound(new ErrorDto { ErrorMessage = "Notfound", StatusCode = (int)HttpStatusCode.NotFound, Description = e.Message });
            }
            catch (Exception)
            {
                _logger.LogError("Something went  wrong");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorDto { ErrorMessage = "IntenalServerError", StatusCode = (int)HttpStatusCode.InternalServerError, Description = "Something went wrong" });
            }



        }
        /// <summary>
        /// Get the order details by user id
        /// </summary>
        /// <param name="id"></param>
        [HttpGet]
        [Authorize]
        [Route("api/order")]
        public IActionResult GetAllorders()
        {
            try
            {
                List<OrderDto> orderDtos = _orderService.GetAllOrders();
                _logger.LogInformation("Getting order details  with user id");
                return StatusCode(StatusCodes.Status200OK, orderDtos);
            }
            catch (NotFoundException e)
            {
                _logger.LogDebug("No user with this user Id found in the database");
                return NotFound(new ErrorDto { ErrorMessage = "Notfound", StatusCode = (int)HttpStatusCode.NotFound, Description = e.Message });
            }
            catch (Exception)
            {
                _logger.LogError("Something went  wrong");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorDto { ErrorMessage = "IntenalServerError", StatusCode = (int)HttpStatusCode.InternalServerError, Description = "Something went wrong" });
            }

        }
        /// <summary>
        /// Get the order details by user id
        /// </summary>
        /// <param name="id"></param>
        [HttpGet]
        [Authorize]
        [Route("api/order/{orderId}")]
        public IActionResult GetOrderById([FromRoute] Guid orderId)
        {
            try
            {
                OrderDto order = _orderService.GetOrderByOrderId(orderId);

                return StatusCode(StatusCodes.Status200OK, order);
            }
            catch (NotFoundException e)
            {
                _logger.LogDebug("No user with this user Id found in the database");
                return NotFound(new ErrorDto { ErrorMessage = "Notfound", StatusCode = (int)HttpStatusCode.NotFound, Description = e.Message });
            }
            catch (Exception)
            {
                _logger.LogError("Something went  wrong");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorDto { ErrorMessage = "IntenalServerError", StatusCode = (int)HttpStatusCode.InternalServerError, Description = "Something went wrong" });
            }
        }
        /// <summary>
        /// Delete the order details by user id
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        [Authorize]
        [Route("api/order/{orderId}")]
        public IActionResult DeleteOrderById([FromRoute] Guid orderId)
        {
            try
            {
                 _orderService.DeleteOrderByOrderId(orderId);

                return StatusCode(StatusCodes.Status200OK);
            }
            catch (NotFoundException e)
            {
                _logger.LogDebug("No user with this user Id found in the database");
                return NotFound(new ErrorDto { ErrorMessage = "Notfound", StatusCode = (int)HttpStatusCode.NotFound, Description = e.Message });
            }
            catch (Exception)
            {
                _logger.LogError("Something went  wrong");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorDto { ErrorMessage = "IntenalServerError", StatusCode = (int)HttpStatusCode.InternalServerError, Description = "Something went wrong" });
            }
        }

    }
} 
