namespace Courier_Company.Models.FirmViewModel
{
    public class CreateFirm
    {
        public string FirmName { get; set; } = null!;
        public string phoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public IEnumerable<CreateCar> car { get; set; } = null;
    }
}
