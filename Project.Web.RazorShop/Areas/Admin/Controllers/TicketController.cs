using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs.TicketMessage;
using Project.Application.Features.Interfaces;
using Project.Application.Helpers;
using Project.Application.Responses;

namespace Project.Web.SuperAdmin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = PublicHelper.Roles.Administrator)]
    public class TicketController : Controller
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Detail(int Id)
        {
            var data=await _ticketService.Details(Id);
            return View(data);
        }

        [HttpGet]
        public async Task<JsonResult> GetData(int page, string search)
        {
            var res = await _ticketService.GetAllPaginate(search, page,false);
            return Json(res);
        }
        
        [HttpPost]
        public async Task<JsonResult> AddMessage(CreateTicketMessage input)
        {
            await _ticketService.AddMessage(input);

            return new Response<string>(ResponseStatus.Succeed).ToJsonResult();
        }
    }
}
