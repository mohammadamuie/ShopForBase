using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Project.Application.Contracts.Persistence;
using Project.Application.DTOs.Datatable;
using Project.Application.DTOs.DataTable;
using Project.Application.DTOs.NewsCategory;
using Project.Application.Exceptions;
using Project.Application.Extensions;
using Project.Application.Features.Interfaces;
using Project.Domain.Entities.News;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Services
{
    public class NewsCategoryService :INewsCategoryService
    {
        private readonly INewsCategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public NewsCategoryService(INewsCategoryRepository categoryRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task Create(UpsertNewsCategory create)
        {
            var model= _mapper.Map<NewsCategory>(create);

            await _categoryRepository.Add(model);
        }

        public async Task Edit(UpsertNewsCategory edit)
        {

            var find = await _categoryRepository.GetNoTracking(edit.Id.Value);
            if (find == null)
            {
                throw new NotFoundException();
            }
            var model= _mapper.Map<NewsCategory>(edit);

            await _categoryRepository.Update(model);
        }

        public async Task<UpsertNewsCategory> GetToEdit(int id)
        {
            var find = await _categoryRepository.Get(id);
            if (find == null)
            {
                throw new NotFoundException();
            }
            return _mapper.Map<UpsertNewsCategory>(find);
        }
        
        public async Task ActiveCategory(int Id,bool IsActive)
        {

            var find = await _categoryRepository.Get(Id);
            if (find == null)
            {
                throw new NotFoundException();
            }
            find.IsActive = IsActive;
            await _categoryRepository.Update(find);
        }

        public async Task<List<NewsCategoryDTO>> GetAll()
        {
            var find = await _categoryRepository.GetAll();
            
            if (find == null)
            {
                throw new NotFoundException();
            }

           

            return _mapper.Map<List<NewsCategoryDTO>>(find);
        }

        public async Task<DatatableResponse<NewsCategoryDTO>> Datatable(NewsCategoryDatatableInput input, FiltersFromRequestDataTable filtersFromRequest)
        {
            var data = _categoryRepository.GetAllQueryable()
                .Where(w => w.IsActive == input.IsActive)
                .AsNoTracking();

            var totalRecords = await data.CountAsync();

            if (!string.IsNullOrWhiteSpace(filtersFromRequest.SearchValue))
            {
                data = data.Where(w =>
                    w.Name.ToLower().Contains(filtersFromRequest.SearchValue.NormalizeText())
                );
            }

            if (input.Id.HasValue && input.Id.Value > 0)
            {
                data = data.Where(w => w.Id == input.Id);
            }

            return await data.ToDataTableAsync<NewsCategory, NewsCategoryDTO>(totalRecords, filtersFromRequest, _mapper);
        }

    }
}
