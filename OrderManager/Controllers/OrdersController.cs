using Database;
using Database.Entities;
using Microsoft.AspNetCore.Mvc;
using OrderManager.Models;
using OrderManager.Services;

namespace OrderManager.Controllers;

[Route("api/orders")]
public class OrdersController : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetAllOrders([FromServices] IOrderService orderService)
    {
        try
        {
            var orders = await orderService.GetOrdersAsync();
            return Ok(orders);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while retrieving orders.");
        }
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetOrderById([FromRoute] Guid id, [FromServices] IOrderService orderService)
    {
        try
        {
            var order = await orderService.GetOrderByIdAsync(id);
            if(order != null) return Ok(order);
            return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving order {id}.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderModel model, [FromServices] IOrderService orderService)
    {
        // TODO: add real validation
        if (model == null)
        {
            return BadRequest("Order model is invalid");
        }

        try
        {
            var order = await orderService.CreateOrderAsync(model);
            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error");
        }
    }
}