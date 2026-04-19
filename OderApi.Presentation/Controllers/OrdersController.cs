using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OderApi.Application.DTOs;
using OderApi.Application.DTOs.NewFolder;
using OderApi.Application.Interface;
using OderApi.Application.Services;
using System.Diagnostics.Eventing.Reader;

namespace OderApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(IOrder orderInterface, IOrderService orderService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
        {
            var orders = await orderService.GetAllAsync();
            if (!orders.Any())
            {
                return NotFound("No order detected in the database");

            }
            var (_, list) = OrderConversion.FromEntity(null, (IEnumerable<Domain.Entities.Order?>)orders);
            return !list!.Any() ? NotFound("No order detected in the database") : Ok(list);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderDTO>> GetOrder(int id)
        {
            var order = await orderInterface.FindByIdAsync(id);
            if (order is null)
            {
                return NotFound(null);
            }
            var (_order, _) = OrderConversion.FromEntity(order, null!);
            return Ok(_order);
        }

        [HttpGet("client/{client:int}")]
        public async Task<ActionResult<OrderDTO>> GetClientOrders(int clientId)
        {
            if(clientId <= 0 ) return BadRequest("Invalid data provided");

            var orders = await orderInterface.GetOrdersByClientId(clientId);
            return !orders.Any() ? NotFound(null) : Ok(orders);
            //if (orders == null || !orders.Any()) return NotFound(null);
            //return Ok(orders);
            
        }

        [HttpGet("details/{orderId}")]
        public async Task<ActionResult<OrderDetailsDTO>> GetOrderDetails(int orderId)
        {
            if (orderId <= 0) return BadRequest("Invalid data provided");

            var orderDetails = await orderInterface.GetOrderDetails(orderId);
            return orderDetails is null ? NotFound(null) : Ok(orderDetails);
        }

        [HttpPost]
        public async Task<ActionResult<Response>> CreateOrder(OrderDTO orderDTO)
        {
            // Check model state if all data annotations are passed.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // convert to entity
            var getEntity = OrderConversion.ToEntity(orderDTO);
            var response = await orderInterface.CreateAsync(getEntity);
            return response.Flag ? Ok(response) : BadRequest(response);

        }

        [HttpPut]
        public async Task<ActionResult<Response>> UpdateOrder(OrderDTO orderDTO)
        {
            // convert from dto to entity
            var order = OrderConversion.ToEntity(orderDTO);
            var response = await orderInterface.UpdateAsync(order);
            return response.Flag ? Ok(response) : BadRequest(response);
        }


        [HttpDelete]
        public async Task<ActionResult<Response>> DeleteOrder(OrderDTO orderDTO)
        {
            // convert from dto to entity
            var order = OrderConversion.ToEntity(orderDTO);
            var response = await orderInterface.DeleteAsync(order);
            return response.Flag ? Ok(response) : BadRequest(response);
        }
    }
}
