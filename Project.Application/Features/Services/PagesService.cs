using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Application.Contracts.Persistence;
using Project.Application.DTOs.DataTable;
using Project.Application.DTOs.Pages;
using Project.Application.Exceptions;
using Project.Application.Extensions;
using Project.Application.Features.Interfaces;
using Project.Application.Helpers;
using Project.Application.Responses;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Services
{
    public class PagesService : IPagesService
    {
        private readonly IPagesRepository _pagesRepository;
        private readonly IMapper _mapper;
        public PagesService(IPagesRepository pagesRepository, IMapper mapper)
        {
            _pagesRepository = pagesRepository;
            _mapper = mapper;
        }
        public async Task<DatatableResponse<PagesDTO>> GetDataTable(CategoryDataTableInput input, FiltersFromRequestDataTable filtersFromRequest)
        {
            var data = _pagesRepository.GetAllQueryable().Where(w => w.IsActive == input.IsActive).AsQueryable();

            var totalRecords = await data.CountAsync();

            if (input.Id.HasValue && input.Id.Value > 0)
            {
                data = data.Where(w => w.Id == input.Id.Value);
            }

            if (!string.IsNullOrEmpty(filtersFromRequest.SearchValue))
            {
                data = data.Where(w =>
                    w.Title.ToLower().Contains(filtersFromRequest.SearchValue.Trim().ToLower()) ||
                    w.MetaTitle.ToString().Contains(filtersFromRequest.SearchValue.Trim().ToLower())
                );
            }

            if (!string.IsNullOrEmpty(input.Name))
            {
                data = data.Where(w => w.Title.ToLower().Contains(input.Name.Trim().ToLower()));
            }

            return await data.ToDataTableAsync<Pages, PagesDTO>(totalRecords, filtersFromRequest, _mapper);

        }


        public async Task ActivePage(int Id)
        {
            var find = await _pagesRepository.GetAllQueryable().FirstOrDefaultAsync(w => w.Id == Id);
            if (find == null)
            {
                throw new NotFoundException();
            }
            find.IsActive = true;
            find.UpdatedAt = DateTime.Now;
            await _pagesRepository.Update(find);

        }

        public async Task DisablePage(int Id)
        {
            var find = await _pagesRepository.GetAllQueryable().FirstOrDefaultAsync(w => w.Id == Id);
            if (find == null)
            {
                throw new NotFoundException();
            }
            find.IsActive = false;
            find.UpdatedAt = DateTime.Now;
            await _pagesRepository.Update(find);

        }

        public async Task AddPage(UpsertPages input)
        {
            var model = new Pages()
            {
                Content = input.Content,
                MetaDescription = input.MetaDescription,
                MetaKeywords = input.MetaKeyWords,
                MetaTitle = input.MetaTitle,
                Title = input.Title,
            };

            var Url = PublicHelper.FilterUrl(input.Url);
            var mm = await _pagesRepository.GetAllQueryable().FirstOrDefaultAsync(w => w.Url == Url);
            if (mm != null)
            {
                throw new BadRequestException("لینک وارد شده قبلن وجود دارد");
            }
            else
            {
                model.Url = Url;
            }

            await _pagesRepository.Add(model);

        }

        public async Task UpdatePage(UpsertPages input)
        {
            var model = await _pagesRepository.GetAllQueryable().FirstOrDefaultAsync(w => w.Id == input.Id);
            if (model == null)
            {
                throw new NotFoundException();
            }

            model.MetaDescription = input.MetaDescription;
            model.MetaTitle = input.MetaTitle;
            model.MetaKeywords = input.MetaKeyWords;
            model.Title = input.Title;
            model.Content = input.Content;
            model.UpdatedAt = DateTime.Now;
            if (model.Url != input.Url)
            {
                var Url = await _pagesRepository.GetAllQueryable().FirstOrDefaultAsync(w => w.Url == input.Url);
                if (Url != null)
                {
                    throw new BadRequestException("لینک وارد شده قبلن وجود دارد");
                }
                model.Url = input.Url;

            }
            await _pagesRepository.Update(model);
        }

        public async Task<PagesDTO> GetByIdPage(int Id)
        {
            var s = await _pagesRepository.GetAllQueryable().FirstOrDefaultAsync(w => w.Id == Id);
            if (s == null)
            {
                throw new NotFoundException();
            }
            var model = new PagesDTO()
            {
                Title = s.Title,
                Content = s.Content,
                Url = s.Url,
                MetaTitle = s.MetaTitle,
                MetaDescription = s.MetaDescription,
                MetaKeyWords = s.MetaKeywords,
            };
            return model;
        }
        public async Task<PagesDTO> GetByUrlPage(string url)
        {
            var s = await _pagesRepository.GetAllQueryable().FirstOrDefaultAsync(w => w.Url == url);
            if (s == null)
            {
                throw new NotFoundException();
            }
            var model = new PagesDTO()
            {
                Title = s.Title,
                Content = s.Content,
                Url = s.Url,
                MetaTitle = s.MetaTitle,
                MetaDescription = s.MetaDescription,
                MetaKeyWords = s.MetaKeywords,
                CreatedAt = s.CreatedAt,
                UpdatedAt = s.UpdatedAt,
                IsActive= s.IsActive,
            };
            return model;
        }
        public async Task<List<PagesDTO>> GetPageList()
        {
            var model = await _pagesRepository.GetAllQueryable().Where(w => w.IsActive == true).ToListAsync();

            return _mapper.Map <List<PagesDTO>>(model);
        }
    }
}
