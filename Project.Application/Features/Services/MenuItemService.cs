using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Application.Contracts.Persistence;
using Project.Application.DTOs.Category;
using Project.Application.DTOs.DataTable;
using Project.Application.DTOs.MenuItem;
using Project.Application.DTOs.Pages;
using Project.Application.Exceptions;
using Project.Application.Extensions;
using Project.Application.Features.Interfaces;
using Project.Application.Helpers;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Services
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMapper _mapper;

        public MenuItemService(IMenuItemRepository menuItemRepository, IMapper mapper)
        {
            _menuItemRepository = menuItemRepository;
            _mapper = mapper;
        }

        public async Task<DatatableResponse<MenuItemDTO>> GetDataTable(CategoryDataTableInput input, FiltersFromRequestDataTable filtersFromRequest)
        {
            var data = _menuItemRepository.GetAllQueryable().Include(w=>w.Parent).Where(w => w.IsActive == input.IsActive).AsQueryable();

            var totalRecords = await data.CountAsync();

            if (input.Id.HasValue && input.Id.Value > 0)
            {
                data = data.Where(w => w.Id == input.Id.Value);
            }

            if (!string.IsNullOrEmpty(filtersFromRequest.SearchValue))
            {
                data = data.Where(w =>
                    w.Name.ToLower().Contains(filtersFromRequest.SearchValue.Trim().ToLower())
                );
            }


            return await data.ToDataTableAsync<MenuItem, MenuItemDTO>(totalRecords, filtersFromRequest, _mapper);

        }



        public async Task Disable(int Id)
        {
            var find = await _menuItemRepository.GetAllQueryable().FirstOrDefaultAsync(w => w.Id == Id);
            if (find == null)
            {
                throw new NotFoundException();
            }
            find.IsActive = false;
            find.UpdatedAt = DateTime.Now;
            await _menuItemRepository.Update(find);
        }

        public async Task Active(int Id)
        {
            var find = await _menuItemRepository.GetAllQueryable().FirstOrDefaultAsync(w => w.Id == Id);
            if (find == null)
            {
                throw new NotFoundException();
            }
            find.IsActive = true;
            find.UpdatedAt = DateTime.Now;
            await _menuItemRepository.Update(find);

        }


        public async Task Create(UpsertMenuItem model)
        {
            var menu = new MenuItem
            {
                Name = model.Name,
                Url = model.Url,
                ManualUrl = false,
            };
            if (!string.IsNullOrWhiteSpace(model.Url1))
            {
                menu.Url = model.Url1;
                menu.ManualUrl = true;
            }
            if (model.ParentId.HasValue && model.ParentId.Value > 0)
            {
                menu.ParentId = model.ParentId.Value;
            }

            await _menuItemRepository.Add(menu);
        }

        public async Task Edit(UpsertMenuItem model)
        {

            var menu = await _menuItemRepository.GetAllQueryable().FirstOrDefaultAsync(w => w.Id == model.Id);

            if (menu == null)
            {
                throw new NotFoundException();
            }

            menu.Name = model.Name;
            menu.Url = model.Url;
            menu.ManualUrl = false;
            if (!string.IsNullOrWhiteSpace(model.Url1))
            {
                menu.Url = model.Url1;
                menu.ManualUrl = true;
            }
            menu.ParentId = null;
            if (model.ParentId.HasValue && model.ParentId.Value > 0)
            {
                menu.ParentId = model.ParentId.Value;
            }

            await _menuItemRepository.Update(menu);
        }
        public async Task<List<MenuItemDTO>> GetMenuItemList()
        {
            var model = await _menuItemRepository.GetAllQueryable().Where(w => w.IsActive == true).ToListAsync();

            return _mapper.Map<List<MenuItemDTO>>(model);
        }
        public async Task<List<MenuItemDTO>> GetMenuItemAsync()
        {
            var data = await _menuItemRepository.GetAllQueryable()
                .Include(i => i.Childs).Where(w => w.IsActive == true && w.ParentId == null)
                .Select(s => new MenuItemDTO
                {
                    Childs = s.Childs.Where(w => w.IsActive == true).Select(ss => new MenuItemDTO
                    {
                        Childs = ss.Childs.Where(w => w.IsActive == true).Select(sss => new MenuItemDTO
                        {
                            Childs = null,
                            Name = sss.Name,
                            Url = sss.Url,
                            ManualUrl = sss.ManualUrl,
                            Id = sss.Id
                        }).ToList(),
                        Name = ss.Name,
                        Url = ss.Url,
                        ManualUrl = ss.ManualUrl,
                        Id = ss.Id
                    }).ToList(),
                    Name = s.Name,
                    Url = s.Url,
                    ManualUrl = s.ManualUrl,
                    Id = s.Id
                })
                .ToListAsync();

            return data;
        }
    }
}
