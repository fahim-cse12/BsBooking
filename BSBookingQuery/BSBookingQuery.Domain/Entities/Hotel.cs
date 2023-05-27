using System.ComponentModel.DataAnnotations;

namespace BSBookingQuery.Domain.Entities
{
    public class Hotel : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public int Rating { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
