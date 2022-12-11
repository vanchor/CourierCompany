using Courier_Company.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Courier_Company.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourierController : ControllerBase
    {
        private readonly IMongoCollection<Courier> _CourierCollection;

        public CourierController(IOptions<CourierCompanyStoreSettings> courierCompanyStoreSettings)
        {
            var mongoClient = new MongoClient(
                courierCompanyStoreSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                courierCompanyStoreSettings.Value.DatabaseName);

            _CourierCollection = mongoDatabase.GetCollection<Courier>(
                courierCompanyStoreSettings.Value.CourierCollectionName);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Courier>>> Get()
        {
            return await _CourierCollection.Find(_ => true).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Courier>> Get(string id)
        {
            if (!ObjectId.TryParse(id, out _))
                return NotFound("Incorrect id");

            return await _CourierCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        [HttpPost]
        public async Task Post([FromBody] Courier newCourier)
        {
            await _CourierCollection.InsertOneAsync(newCourier);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] Courier updatedCourier)
        {
            if (!ObjectId.TryParse(id, out _))
                return NotFound("Incorrect id");
            if (id != updatedCourier.Id)
                return BadRequest("The id in the request does not match the id in the body.");

            await _CourierCollection.ReplaceOneAsync(x => x.Id == id, updatedCourier);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            if (!ObjectId.TryParse(id, out _))
                return NotFound("Incorrect id");

            await _CourierCollection.DeleteOneAsync(x => x.Id == id);

            return Ok();
        }
    }
}