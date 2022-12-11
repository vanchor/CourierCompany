using Courier_Company.Models.FirmViewModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Courier_Company.Models
{
    public class Firm
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = "";

        [BsonElement("name")]
        public string FirmName { get; set; } = null!;

        [BsonElement("phone_number")]
        public string phoneNumber { get; set; } = null!;

        [BsonElement("email")]
        public string Email { get; set; } = null!;

        public IEnumerable<car> car { get; set; }

        public Firm() { }


        public Firm(CreateFirm cFirm) 
        {
            FirmName = cFirm.FirmName;
            phoneNumber = cFirm.phoneNumber;
            Email = cFirm.Email;
            int i = 0;
            var cars = new List<car>();
            foreach (var c in cFirm.car)
            {
                cars.Add(new car()
                {
                    id_car = i,
                    type_car = c.type_car,
                    mark = c.mark,
                    model = c.model,
                    registration_number = c.registration_number,
                });
                i++;
            }

            car = cars;
        }
    }
}