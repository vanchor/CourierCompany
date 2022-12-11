namespace Courier_Company.Models
{
    public class Client
    {
        public int ClientId { get; set; }
        public string name { get; set; } = null!;
        public string surname { get; set; } = null!;
        public string phone_number { get; set; } = null!;
        public Addres Addres { get; set; } = null!;

        public IEnumerable<Order> Order { get; set; }

        public Client() { }

        public Client(ClientCreate client)
        {
            name = client.name;
            surname = client.surname;
            phone_number = client.phone_number;
            Addres = new Addres()
            {
                Country = client.Addres.Country,
                city = client.Addres.city,
                zip_code = client.Addres.zip_code,
                street = client.Addres.street,
                house_number = client.Addres.house_number,
                local_number = client.Addres.local_number
            };

            var ords = new List<Order>();
            foreach (var o in client.Order)
                ords.Add(new Order(o) { 
                    ClientId = ClientId
                });

            Order = ords;
        }

        public Client(ClientUpdate client)
        {
            name = client.name;
            surname = client.surname;
            phone_number = client.phone_number;
        }
    }

    public class ClientCreate
    {
        public string name { get; set; } = null!;
        public string surname { get; set; } = null!;
        public string phone_number { get; set; } = null!;
        public AddresCreate Addres { get; set; } = null!;

        public ICollection<OrderCreate> Order { get; set; } = null!;
    }

    public class ClientUpdate
    {
        public string name { get; set; } = null!;
        public string surname { get; set; } = null!;
        public string phone_number { get; set; } = null!;
    }
}
