using Project.Application.DTOs.Base;

namespace Project.Application.DTOs.TicketMessage
{
    public class TicketMessageDTO : BaseDTO
    {
        public int TicketId { get; set; }
        public string Message { get; set; }
        public string AttachmentUrl { get; set; }
        public bool SeenByAdmin { get; set; }
        public bool SeenByUser { get; set; }
        public bool SentByUser { get; set; } = false;
    }
}
