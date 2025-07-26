using Project.Domain.Entities.Base;

namespace Project.Domain.Entities
{
    public class TicketMessage : BaseEntity
    {
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public string Message { get; set; }
        public string AttachmentUrl { get; set; }
        public bool SeenByAdmin { get; set; }
        public bool SeenByUser { get; set; }
        public bool SentByUser { get; set; } = false;
    }
}