using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order_API.Models;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private static List<Order> _orders = new List<Order>
    {
        new Order { Id = 1, ProductName = "ProductA", Quantity = 10 },
        new Order { Id = 2, ProductName = "ProductB", Quantity = 5 }
        // Add more initial orders as needed
    };

    [Authorize("read")]
    [HttpGet]
    public IActionResult GetOrders()
    {
        return Ok(_orders);
    }

    [Authorize("read")]
    [HttpGet("{id}")]
    public IActionResult GetOrder(int id)
    {
        var order = _orders.FirstOrDefault(o => o.Id == id);

        if (order == null)
        {
            return NotFound();
        }

        return Ok(order);
    }

    [Authorize("write")]
    [HttpPost]
    public IActionResult CreateOrder([FromBody] Order newOrder)
    {
        if (newOrder == null)
        {
            return BadRequest("Invalid order data");
        }

        newOrder.Id = GenerateOrderId();
        _orders.Add(newOrder);

        return CreatedAtAction(nameof(GetOrder), new { id = newOrder.Id }, newOrder);
    }

    [Authorize("write")]
    [HttpPut("{id}")]
    public IActionResult UpdateOrder(int id, [FromBody] Order updatedOrder)
    {
        var existingOrder = _orders.FirstOrDefault(o => o.Id == id);

        if (existingOrder == null)
        {
            return NotFound();
        }

        existingOrder.ProductName = updatedOrder.ProductName;
        existingOrder.Quantity = updatedOrder.Quantity;

        return Ok(existingOrder);
    }

    [Authorize("write")]
    [HttpDelete("{id}")]
    public IActionResult DeleteOrder(int id)
    {
        var orderToRemove = _orders.FirstOrDefault(o => o.Id == id);

        if (orderToRemove == null)
        {
            return NotFound();
        }

        _orders.Remove(orderToRemove);

        return NoContent();
    }

    private int GenerateOrderId()
    {
        // Generate a unique order ID (this is a simple example, in a real-world scenario, you might use a more robust approach)
        return _orders.Max(o => o.Id) + 1;
    }
}



