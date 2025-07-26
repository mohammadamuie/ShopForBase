using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Application.Contracts.Persistence;
using Project.Application.DTOs.Ticket;
using Project.Application.DTOs.Datatable;
using Project.Application.Exceptions;
using Project.Application.Extensions;
using Project.Application.Features.Interfaces;
using Project.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;
using Project.Application.Contracts.Infrastructure;
using Project.Application.DTOs.User;
using Project.Application.DTOs.TicketMessage;
using Project.Domain.Enums;
using System.Collections.Generic;
using Project.Application.DTOs.DataTable;

namespace Project.Application.Features.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;
        private readonly ISmsSender _smsSender;
        private readonly UserDTO currentUser;
        private readonly IFileStorageService _storageService;
        private readonly string container;
        private readonly ITicketMessageRepository _ticketMessageRepository;

        public TicketService(ITicketRepository ticketRepository,
                             IMapper mapper,
                             ISmsSender smsSender,
                             IUserService userService,
                             IFileStorageService storageService,
                             ITicketMessageRepository ticketMessageRepository)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
            _smsSender = smsSender;
            currentUser = null;
            _storageService = storageService;
            container = "tickets";
            _ticketMessageRepository = ticketMessageRepository;
        }

        public async Task<DatatableResponse<TicketDTO>> Datatable(TicketDatatableInput input, FiltersFromRequestDataTable filtersFromRequest)
        {
            var data = _ticketRepository.GetAllQueryable()
                .Where(w => w.IsActive == input.IsActive)
                .Include(i1 => i1.Messages)
                .Include(i2 => i2.User)
                .AsNoTracking();

            var totalRecords = await data.CountAsync();

            if (!string.IsNullOrWhiteSpace(filtersFromRequest.SearchValue))
            {
                data = data.Where(w =>
                    w.Title.ToLower().Contains(filtersFromRequest.SearchValue.NormalizeText())
                );
            }

            if (input.Id.HasValue && input.Id.Value > 0)
            {
                data = data.Where(w => w.Id == input.Id);
            }

            return await data.ToDataTableAsync<Ticket, TicketDTO>(totalRecords, filtersFromRequest, _mapper);
        }

        public async Task<List<TicketDTO>> GetAllPaginate(string search, int page, bool byUser = false)
        {
            var items = await _ticketRepository.GetAllQueryable()
                .Where(w => w.IsActive == true )
                .Where(w => string.IsNullOrWhiteSpace(search) || w.Title.Contains(search))
                .Where(w => byUser != true || w.UserId == currentUser.Id)
                .Include(i => i.User)
                .AsNoTracking()
                .PaginateDefault(page)
                .ToListAsync();

            return _mapper.Map<List<TicketDTO>>(items);
        }

        public async Task<List<TicketDTO>> GetAll(string search, bool byUser = false)
        {
            var items = await _ticketRepository.GetAllQueryable()
                .Where(w => w.IsActive == true)
                .Where(w => string.IsNullOrWhiteSpace(search) || w.Title.Contains(search))
                .Where(w => byUser != true || w.UserId == currentUser.Id) // byUser == true ? w.UserId == currentUser.Id : true
                .Include(i => i.User)
                .Include(i => i.Messages)
                .AsNoTracking()
                .OrderByDescending(o => o.Status)
                .ThenByDescending(o => o.UpdatedAt)
                .ToListAsync();

            return _mapper.Map<List<TicketDTO>>(items);
        }

        public async Task<TicketDTO> Create(CreateTicket input)
        {
            var model = _mapper.Map<Ticket>(input);

            model.UserId = currentUser.Id;
            model.Status = TicketStatus.Pending;

            var res = await _ticketRepository.Add(model);

            //_smsSender.SimpleSend(currentUser.Phone, $"تیکت جدید با کد #{res.Id} ایجاد شد");

            return _mapper.Map<TicketDTO>(res);
        }

        public async Task AddMessage(CreateTicketMessage input, bool byAdmin = true)
        {
            var findTicket = await _ticketRepository.Get(input.TicketId);
            if (findTicket == null)
            {
                throw new NotFoundException();
            }

            if (byAdmin == false && findTicket.UserId != currentUser.Id)
            {
                throw new BadRequestException("اجازه دسترسی به این تیکت وجود ندارد");
            }

            var model = _mapper.Map<TicketMessage>(input);

            model.SeenByAdmin = byAdmin;
            model.SeenByUser = !byAdmin;
            model.SentByUser = !byAdmin;
            if (input.Attachment != null)
                model.AttachmentUrl = await _storageService.SaveFile(container, input.Attachment);

            var res = await _ticketMessageRepository.Add(model);

            findTicket.Status = byAdmin ? TicketStatus.WaitForUserResponse : TicketStatus.WaitForAdminResponse;
            await _ticketRepository.Update(findTicket);

        }

        public async Task<TicketWithMessagesDTO> Details(int id)
        {
            var findTicket = await _ticketRepository.GetWithUser(id);
            if (findTicket == null)
            {
                throw new NotFoundException();
            }

            var messages = await _ticketMessageRepository.GetAllQueryable()
                .Where(w => w.IsActive == true && w.TicketId == id)
                .AsNoTracking()
                .ToListAsync();

            var res = new TicketWithMessagesDTO
            {
                Details = _mapper.Map<TicketDTO>(findTicket),
                Messages = _mapper.Map<List<TicketMessageDTO>>(messages)
            };

            res.Details.TotalMessages = res.Messages.Count();

            return res;
        }

        public async Task<bool> Close(int id)
        {
            var find = await _ticketRepository.GetWithUser(id);

            if (find == null)
            {
                throw new NotFoundException();
            }

            find.Status = TicketStatus.Closed;

            await _ticketRepository.Update(find);

            //_smsSender.SimpleSend(
            //    find.User.Phone,
            //    $"تیکت شما با کد #{find.Id} توسط اپراتور بسته شد"
            //);

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            if (await _ticketRepository.Exist(id))
            {
                throw new NotFoundException();
            }

            await _ticketRepository.Delete(id);

            return true;
        }
        public async Task<bool> Recover(int id)
        {
            if (await _ticketRepository.Exist(id))
            {
                throw new NotFoundException();
            }

            await _ticketRepository.Recover(id);

            return true;
        }
    }
}