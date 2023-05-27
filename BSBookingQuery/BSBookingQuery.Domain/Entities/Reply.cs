namespace BSBookingQuery.Domain.Entities
{
    public class Reply : BaseEntity
    {
        public string Message { get; set; }
        public string ReplierName { get; set; }
        public int CommentId { get; set; }
        public Comment Comment { get; set; }
    }
}
