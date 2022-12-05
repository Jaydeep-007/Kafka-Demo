using Microsoft.AspNetCore.Authentication;

namespace ProducerApplication.Models
{
    public class CarDetails
    {
        public int CarId { get; set; }
        public string CarName { get; set; }
        public string BookingStatus { get; set; }
    }
}
