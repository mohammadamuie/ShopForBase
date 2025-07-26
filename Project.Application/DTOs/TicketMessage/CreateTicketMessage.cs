using Microsoft.AspNetCore.Http;

namespace Project.Application.DTOs.TicketMessage
{
    public class CreateTicketMessage
    {
        public int TicketId { get; set; }
        public string Message { get; set; }
#nullable enable
        public IFormFile? Attachment { get; set; }
#nullable disable
    }
}
