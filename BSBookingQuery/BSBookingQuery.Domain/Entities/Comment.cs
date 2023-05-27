using System.ComponentModel.DataAnnotations;

namespace BSBookingQuery.Domain.Entities
{
    public class Comment : BaseEntity
    {
       
        [Required]
        public string Message { get; set; }     
        public string CommenterName { get; set; }   
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }

        public ICollection<Reply> Replies { get; set; }
    }
}
