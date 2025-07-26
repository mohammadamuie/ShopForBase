using Project.Domain.Enums;

namespace Project.Application.DTOs.Ticket
{
    public class CreateTicket
    {
        public string Title { get; set; }
        public TicketPriority Priority { get; set; }
    }
}