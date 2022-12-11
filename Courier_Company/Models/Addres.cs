using System.Text.Json.Serialization;

namespace Courier_Company.Models
{
    public class Addres
    {
        public int AddresId { get; set; }
        public string Country { get; set; } = null!;
        public string city { get; set; } = null!;
        public string zip_code { get; set; } = null!;
        public string street { get; set; } = null!;
        public int house_number { get; set; }
        public int local_number { get; set; }

        public int ClientId { get; set; }
    }

    public class AddresCreate
    {
        public string Country { get; set; } = null!;
        public string city { get; set; } = null!;
        public string zip_code { get; set; } = null!;
        public string street { get; set; } = null!;
        public int house_number { get; set; }
        public int local_number { get; set; }
    }
}
