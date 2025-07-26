using Project.Application.DTOs.Datatable;
using Project.Application.DTOs.DataTable;
using Project.Application.DTOs.Ticket;
using Project.Application.DTOs.TicketMessage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Application.Features.Interfaces
{
    public interface ITicketService
    {
        Task AddMessage(CreateTicketMessage input, bool byAdmin = true);
        Task<bool> Close(int id);
        Task<TicketDTO> Create(CreateTicket input);
        Task<DatatableResponse<TicketDTO>> Datatable(TicketDatatableInput input, FiltersFromRequestDataTable filtersFromRequest);
        Task<bool> Delete(int id);
        Task<TicketWithMessagesDTO> Details(int id);
        Task<List<TicketDTO>> GetAll(string search, bool byUser = false);
        Task<List<TicketDTO>> GetAllPaginate(string search, int page, bool byUser = false);
        Task<bool> Recover(int id);
    }
}
