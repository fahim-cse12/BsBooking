using System.ComponentModel.DataAnnotations;

namespace BSBookingQuery.Utility.Models
{
    public class ReplyModel : BaseModel 
    {
        [Required(ErrorMessage = "Message text is required")]
        public string Message { get; set; }
        public int CommentId { get; set; }
        [Required(ErrorMessage = "Replier Name is missing")]
        [MaxLength(200)]
        public string ReplierName { get; set; }
        public CommentModel Comment { get; set; }
    }
}
