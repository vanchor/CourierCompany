using Courier_Company.Models.FirmViewModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Courier_Company.Models
{
    public class Courier
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = "";

        [BsonElement("name")]
        public string FirstName { get; set; } = null!;

        [BsonElement("surname")]
        public string Surname { get; set; } = null!;

        [BsonElement("pesel")]
        public string Pesel { get; set; } = null!;

        [BsonElement("phone_number")]
        public string PhoneNumber { get; set; } = null!;

        [BsonElement("email")]
        public string Email { get; set; } = null!;

        public Schedule schedule { get; set; } = null!;

        public int id_departure { get; set; }
    }
}

