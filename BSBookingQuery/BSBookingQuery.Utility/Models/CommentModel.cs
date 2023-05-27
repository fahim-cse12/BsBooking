using System.ComponentModel.DataAnnotations;

namespace BSBookingQuery.Utility.Models
{
    public class CommentModel : BaseModel 
    {
        [Required(ErrorMessage = "Message text is required")]
        public string Message { get; set; }
        [Required(ErrorMessage = "Commenter Name is missing")]
        [MaxLength(200)]
        public string CommenterName { get; set; }
        public int HotelId { get; set; }
        public HotelModel Hotel { get; set; }

    }
}
