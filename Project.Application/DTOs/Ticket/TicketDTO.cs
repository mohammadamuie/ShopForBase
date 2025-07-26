using Project.Application.DTOs.Base;
using Project.Domain.Enums;

namespace Project.Application.DTOs.Ticket
{
    public class TicketDTO : BaseDTO
    {
        public string Title { get; set; }
        public int UserId { get; set; }
        public string User { get; set; }
        public TicketStatus Status { get; set; }
        public TicketPriority Priority { get; set; }
        public int TotalMessages { get; set; }
    }
}