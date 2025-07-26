using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Application.Contracts.Infrastructure;
using Project.Application.Contracts.Persistence;
using Project.Application.DTOs.Category;
using Project.Application.DTOs.DataTable;
using Project.Application.Exceptions;
using Project.Application.Extensions;
using Project.Application.Features.Interfaces;
using Project.Domain.Entities;
using Project.Domain.Entities.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Services
{
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _storageService;
        private readonly string container;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper, IFileStorageService storageService)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            container = "category";
            _storageService = storageService;
        }

        public async Task Create(UpsertCategory entity)
        {
            var model = new Category();
            model.Name = entity.Name;
            if (entity.Image != null && entity.Image.Length > 0)
                model.Image = await _storageService.SaveFile(container, entity.Image);

            if (entity.ParentId != 0)
            {
                model.ParentId = entity.ParentId;
            }
            await _categoryRepository.Add(model);

        }

        public async Task Edit(UpsertCategory entity)
        {
            var category = await _categoryRepository.GetNoTracking(entity.Id.Value);
            if (category == null)
            {
                throw new NotFoundException();
            }

            if (entity.Image != null && entity.Image.Length > 0)
                category.Image = await _storageService.SaveFile(container, entity.Image);

            category.Name = entity.Name;
            if (entity.ParentId == 0)
            {
                category.ParentId = null;
            }
            else
            {
                category.ParentId = entity.ParentId;
            }
            await _categoryRepository.Update(category);

        }
        public async Task Special(int id)
        {
            var find = await _categoryRepository.GetAllQueryable().FirstOrDefaultAsync(w => w.Id == id);
            if (find.Special != null)
            {
                if (find.Special.Value)
                {
                    find.Special = false;
                }
                else
                {
                    find.Special = true;
                }
            }
            else
            {
                find.Special = true;
            }
            await _categoryRepository.Update(find);
        }

        public async Task ChangeIsActive(int id,bool IsActive)
        {
            if (IsActive)
            {
            await _categoryRepository.Recover(id);
            }
            else
            {
            await _categoryRepository.Delete(id);
            }
        }

        public async Task<List<SelectCategory>> GetSelectOptionData()
        {
            var data = _categoryRepository.GetAllQueryable();
            data =await _categoryRepository.CategoryWhere(data, true);
            data = data.Where(w => w.Parent == null || w.Parent.Parent == null);
            var model = await data.Select(w => new SelectCategory()
            {
                Id = w.Id,
                Name = (w.Parent.Parent != null ? ("(" + w.Parent.Parent.Name + ") ") : "") + (w.Parent != null ? ("(" + w.Parent.Name + ") ") : "") + w.Name,
            }).OrderByDescending(w => w.Name).ToListAsync();

            return model;
        }
        public async Task<List<SelectCategory>> GetSelectOptionData2()
        {
            var data = _categoryRepository.GetAllQueryable();
            data =await _categoryRepository.CategoryWhere(data, true);
            data = data.Where(w => w.Childs.Count() < 1);
            var model = await data.Select(w => new SelectCategory()
            {
                Id = w.Id,
                Name = (w.Parent.Parent != null ? ("(" + w.Parent.Parent.Name + ") ") : "") + (w.Parent != null ? ("(" + w.Parent.Name + ") ") : "") + w.Name,
            }).OrderByDescending(w => w.Name).ToListAsync();

            return model;
        }
        public async Task<List<CategoryDTO>> GetSpecial()
        {
            var data = _categoryRepository.GetAllQueryable().Where(w=>w.IsActive==true&&w.Special==true).AsNoTracking();
            
            return _mapper.Map<List<CategoryDTO>>(data);
        }

        public async Task<List<LandingCategoryMenuItem>> GetCategoryMenuItemAsync()
        {
            var data = await _categoryRepository.GetAllQueryable()
                .Include(i => i.Childs).Where(w => w.IsActive == true && w.ParentId == null)
                .Select(s => new LandingCategoryMenuItem
                {
                    IsRoot = !s.ParentId.HasValue,
                    Childs = s.Childs.Where(w => w.IsActive == true).Select(ss => new LandingCategoryMenuItem
                    {
                        IsRoot = !ss.ParentId.HasValue,
                        Childs = ss.Childs.Where(w => w.IsActive == true).Select(sss => new LandingCategoryMenuItem
                        {
                            IsRoot = false,
                            Childs = null,
                            Name = sss.Name,
                            Id = sss.Id
                        }).ToList(),
                        Name = ss.Name,
                        Id = ss.Id
                    }).ToList(),
                    Name = s.Name,
                    Id=s.Id
                })
                .ToListAsync();

            return data;
        }
        public async Task<DatatableResponse<CategoryDataTable>> GetDataTable(CategoryDataTableInput input, FiltersFromRequestDataTable filtersFromRequest)
        {
            var data = _categoryRepository.GetAllQueryable().Include(w=>w.Parent).Include(w => w.Childs).AsQueryable();
            if (input.IsActive)
            {
                data = await _categoryRepository.CategoryWhere(data, input.IsActive);
            }
            else
            {

                data = data.Where(w =>
                  w.IsActive == input.IsActive
                );
            }

            var totalRecords = await data.CountAsync();

            if (input.Id.HasValue && input.Id.Value > 0)
            {
                data = data.Where(w => w.Id == input.Id.Value);
            }

            if (!string.IsNullOrEmpty(filtersFromRequest.SearchValue))
            {
                data = data.Where(w =>
                    w.Name.ToLower().Contains(filtersFromRequest.SearchValue.Trim().ToLower()) ||
                    w.Id.ToString().Contains(filtersFromRequest.SearchValue.Trim().ToLower())
                );
            }

            if (!string.IsNullOrEmpty(input.Name))
            {
                data = data.Where(w => w.Name.ToLower().Contains(input.Name.Trim().ToLower()));
            }

            return await data.ToDataTableAsync<Category, CategoryDataTable>(totalRecords, filtersFromRequest, _mapper);

        }
    }
}
