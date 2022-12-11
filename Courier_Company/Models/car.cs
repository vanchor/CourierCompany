using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Courier_Company.Models
{
    public class car
    {
        public int id_car { get; set; }
        public string type_car { get; set; }
        public string mark { get; set; }
        public string model { get; set; }
        public string registration_number { get; set; }
    }
}