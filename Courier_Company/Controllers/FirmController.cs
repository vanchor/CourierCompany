using Courier_Company.Models;
using Courier_Company.Models.FirmViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Courier_Company.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FirmController : ControllerBase
    {
        private readonly IMongoCollection<Firm> _FirmCollection;

        public FirmController(IOptions<CourierCompanyStoreSettings> courierCompanyStoreSettings)
        {
            var mongoClient = new MongoClient(
                courierCompanyStoreSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                courierCompanyStoreSettings.Value.DatabaseName);

            _FirmCollection = mongoDatabase.GetCollection<Firm>(
                courierCompanyStoreSettings.Value.FirmCollectionName);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Firm>>> Get()
        {
            return await _FirmCollection.Find(_ => true).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Firm>> Get(string id)
        {
            if (!ObjectId.TryParse(id, out _))
                return NotFound("Incorrect id");

            return await _FirmCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        [HttpPost]
        public async Task Post([FromBody] CreateFirm newFirm)
        {
            var f = new Firm(newFirm);

            await _FirmCollection.InsertOneAsync(f);
        }

        [HttpPost("addCar/{id}")]
        public async Task<ActionResult> Post(string id, [FromBody] car newCar)
        {
            if (!ObjectId.TryParse(id, out _))
                return NotFound("Incorrect id");

            var filter = Builders<Firm>.Filter.Eq(x => x.Id, id);
            var update = Builders<Firm>.Update.Push<car>(x => x.car, newCar);
            await _FirmCollection.FindOneAndUpdateAsync(filter, update);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] Firm updatedFirm)
        {
            if (!ObjectId.TryParse(id, out _))
                return NotFound("Incorrect id");
            if (id != updatedFirm.Id)
                return BadRequest("The id in the request does not match the id in the body.");

            await _FirmCollection.ReplaceOneAsync(x => x.Id == id, updatedFirm);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await _FirmCollection.DeleteOneAsync(x => x.Id == id);
        }
    }
}
