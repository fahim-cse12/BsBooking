using System.ComponentModel.DataAnnotations;

namespace BSBookingQuery.Utility.Models
{
    public class HotelModel : BaseModel
    {
        [Required(ErrorMessage ="Hotel Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Hotel Location is required")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Please select rating")]
        public int Rating { get; set; }
    }
}
