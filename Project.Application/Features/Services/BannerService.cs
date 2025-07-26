using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Application.Contracts.Infrastructure;
using Project.Application.Contracts.Persistence;
using Project.Application.DTOs.BakeryCategory;
using Project.Application.DTOs.DataTable;
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
    public class BannerService : IBannerService
    {
        private readonly IBannerRepository _bannerRepository;
        private readonly IMapper _mapper;
        private readonly string container;
        private readonly IFileStorageService _storageService;

        public BannerService(IBannerRepository bannerRepository, IMapper mapper, IFileStorageService storageService)
        {
            _bannerRepository = bannerRepository;
            _mapper = mapper;
            container = "banner";
            _storageService = storageService;
        }

        public async Task<DatatableResponse<BannerDTO>> Datatable(DatatableInput input, FiltersFromRequestDataTable filtersFromRequest)
        {
            var data = _bannerRepository.GetAllQueryable()
                .Where(w => w.IsActive == true)
                .AsNoTracking();

            var totalRecords = await data.CountAsync();

            if (!string.IsNullOrWhiteSpace(filtersFromRequest.SearchValue))
            {
                data = data.Where(w =>
                    w.Url.Contains(filtersFromRequest.SearchValue.NormalizeText())
                );
            }

            return await data.ToDataTableAsync<Banner, BannerDTO>(totalRecords, filtersFromRequest, _mapper);
        }
        public async Task<BannerDTO> Create(UpsertBanner input)
        {
            var model = _mapper.Map<Banner>(input);
            if (input.Image != null)
            {
                model.Image = await _storageService.SaveFile(container, input.Image);
            }
            model = await _bannerRepository.Add(model);

            return _mapper.Map<BannerDTO>(model);
        }

        public async Task<BannerDTO> Edit(UpsertBanner input)
        {
            var find = await _bannerRepository.GetNoTracking(input.Id.Value);
            if (find == null)
            {
                throw new NotFoundException();
            }
            find.Url = input.Url;
            if (input.Image != null && find.Image != null)
            {
                find.Image = await _storageService.EditFile(container, input.Image, find.Image);
            }
            else
            {
                if (input.Image != null )
                {
                    find.Image = await _storageService.SaveFile(container, input.Image);
                }
            }
            await _bannerRepository.Update(find);

            return _mapper.Map<BannerDTO>(find);
        }
        public async Task<UpsertBanner> GetToEdit(int id)
        {
            var find = await _bannerRepository.GetNoTracking(id);

            return _mapper.Map<UpsertBanner>(find);
        }

        public async Task<List<BannerDTO>> GetToList()
        {
            var find = await _bannerRepository.GetAllQueryable().Where(w => w.IsActive == true).ToListAsync();

            return _mapper.Map<List<BannerDTO>>(find);
        }

        public async Task<bool> Delete(int id)
        {
            if (await _bannerRepository.Get(id) == null)
            {
                return false;
            }

            await _bannerRepository.Delete(id);

            return true;
        }
        public async Task<DatatableResponse<BannerDTO>> GetDataTable(CategoryDataTableInput input, FiltersFromRequestDataTable filtersFromRequest)
        {
            var data = _bannerRepository.GetAllQueryable().AsQueryable();


            data = data.Where(w =>
              w.IsActive == input.IsActive
            );


            var totalRecords = await data.CountAsync();

            if (input.Id.HasValue && input.Id.Value > 0)
            {
                data = data.Where(w => w.Id == input.Id.Value);
            }

            if (!string.IsNullOrEmpty(filtersFromRequest.SearchValue))
            {
                data = data.Where(w =>
                    w.Id.ToString().Contains(filtersFromRequest.SearchValue.Trim().ToLower())
                );
            }


            return await data.ToDataTableAsync<Banner, BannerDTO>(totalRecords, filtersFromRequest, _mapper);

        }
        public async Task ChangeIsActive(int id, bool IsActive)
        {
            if (IsActive)
            {
                await _bannerRepository.Recover(id);
            }
            else
            {
                await _bannerRepository.Delete(id);
            }
        }
        public async Task UnAvailable(int id)
        {
            if (await _bannerRepository.Get(id) == null)
            {
                throw new NotFoundException();
            }

            await _bannerRepository.Delete(id);
        }

    }
}
