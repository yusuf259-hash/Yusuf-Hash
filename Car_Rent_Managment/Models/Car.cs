namespace Car_Rent_Managment.Models
{
    public class Car
    {
        public int CarId { get; set; }
        public int OwnerId { get; set; }
        public string CarName { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string CarNumber { get; set; } = string.Empty;
        public int Seats { get; set; }
        public decimal PricePerDay { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
