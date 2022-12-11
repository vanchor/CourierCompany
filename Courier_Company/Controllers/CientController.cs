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
    public class ClientController : ControllerBase
    {
        protected readonly CourierCompanyDbContext _context;

        public ClientController(CourierCompanyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Client> Get()
        {
            return _context.Clients.Include(c => c.Addres).Include(c => c.Order).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Client> Get(int id)
        {
            var client = _context.Clients.Include(c => c.Addres).Include(c => c.Order).FirstOrDefault(x => x.ClientId == id);

            if (client == null)
                return NotFound();

            return client;
        }

        [HttpPost]
        public async Task Post([FromBody] ClientCreate newClient)
        {
            var client = new Client(newClient);

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ClientUpdate updatedClient)
        {
            var client = _context.Clients.Find(id);
            if (client == null)
                return NotFound("No client with such id.");

            client.name = updatedClient.name;
            client.surname = updatedClient.surname;
            client.phone_number = updatedClient.phone_number;

            _context.Clients.Update(client);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var client = _context.Clients.Find(id);
            if (client == null)
                return NotFound("No client with such id.");

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}