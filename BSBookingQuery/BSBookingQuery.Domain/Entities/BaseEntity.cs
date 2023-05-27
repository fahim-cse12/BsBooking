using System.ComponentModel.DataAnnotations;

namespace BSBookingQuery.Domain.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
