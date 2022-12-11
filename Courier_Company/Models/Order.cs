using System.Text.Json.Serialization;

namespace Courier_Company.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string status { get; set; } = null!;
        public DateTime submission_date { get; set; }
        public DateTime realization_date { get; set; }
        public string methodOfPayment { get; set; } = null!;
        public double Price { get; set; }

        public int ClientId { get; set; }

        public Order(){}

        public Order(OrderCreate order)
        {
            status = order.status;
            submission_date = order.submission_date;
            realization_date = order.realization_date;
            methodOfPayment = order.methodOfPayment;
            Price = order.Price;
        }
    }

    public class OrderCreate
    {
        public string status { get; set; } = null!;
        public DateTime submission_date { get; set; }
        public DateTime realization_date { get; set; }
        public string methodOfPayment { get; set; } = null!;
        public double Price { get; set; }
    }
}
