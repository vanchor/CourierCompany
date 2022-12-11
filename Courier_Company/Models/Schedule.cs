namespace Courier_Company.Models
{
    public class Schedule
    {
        public int id_schedule { get; set; }
        public string start_day { get; set; } = null!;
        public string end_day { get; set; } = null!;
        public string start_time { get; set; } = null!;
        public string end_time { get; set; } = null!;
    }
}
