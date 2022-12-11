using Courier_Company.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Courier_Company.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        protected readonly CourierCompanyDbContext _context;

        public OrderController(CourierCompanyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Order> Get()
        {
            return _context.Orders.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Order> Get(int id)
        {
            var order = _context.Orders.FirstOrDefault(x => x.OrderId == id);

            if (order == null)
                return NotFound();

            return order;
        }

        [HttpPost]
        public async Task<ActionResult> Post(int ClientId, [FromBody] OrderCreate newOrder)
        {
            var client = _context.Clients.Find(ClientId);
            if (client == null)
                return NotFound("No client with such id.");

            _context.Orders.Add(new Order(newOrder) { ClientId = client.ClientId });
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] OrderCreate updatedOrder)
        {
            var order = _context.Orders.Find(id);
            if (order == null)
                return NotFound("No order with such id.");

            order.status = updatedOrder.status;
            order.submission_date = updatedOrder.submission_date;
            order.realization_date = updatedOrder.realization_date;
            order.methodOfPayment = updatedOrder.methodOfPayment;
            order.Price = updatedOrder.Price;

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var ord = _context.Orders.Find(id);
            if (ord == null)
                return NotFound("No order with such id.");

            _context.Orders.Remove(ord);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}