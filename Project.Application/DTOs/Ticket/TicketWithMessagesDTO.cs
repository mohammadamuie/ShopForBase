using Project.Application.DTOs.TicketMessage;
using System.Collections.Generic;

namespace Project.Application.DTOs.Ticket
{
    public class TicketWithMessagesDTO 
    {
        public TicketDTO Details { get; set; }
        public List<TicketMessageDTO> Messages { get; set; } = new List<TicketMessageDTO>();
    }
}