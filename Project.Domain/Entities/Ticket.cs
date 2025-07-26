using Project.Domain.Entities.Base;
using Project.Domain.Enums;
using System.Collections.Generic;

namespace Project.Domain.Entities
{
    public class Ticket : BaseEntity
    {
        public string Title { get; set; }
        public int UserId { get; set; }
        public JWTUser User { get; set; }
        public TicketStatus Status { get; set; }
        public TicketPriority Priority { get; set; }
        public ICollection<TicketMessage> Messages { get; set; }
    }
}