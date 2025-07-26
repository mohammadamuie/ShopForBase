using AutoMapper;
using DNTPersianUtils.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Project.Application.Contracts.Infrastructure;
using Project.Application.Contracts.Persistence;
using Project.Application.DTOs.Datatable;
using Project.Application.DTOs.DataTable;
using Project.Application.DTOs.News;
using Project.Application.Exceptions;
using Project.Application.Extensions;
using Project.Application.Features.Interfaces;
using Project.Application.Helpers;
using Project.Application.Models;
using Project.Domain.Entities.News;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _storageService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string container;
        public NewsService(INewsRepository newsRepository, IMapper mapper, IFileStorageService storageService, IHttpContextAccessor httpContextAccessor)
        {
            _newsRepository = newsRepository;
            _mapper = mapper;
            _storageService = storageService;
            _httpContextAccessor = httpContextAccessor;
            container = "news";

        }

        public async Task Create(UpsertNews create)
        {
            if (create.Image == null && create.Image.Length <= 0)
            {
                throw new BadRequestException("تصویر خبر & مقاله را وارد کنید");
            }
            var filterUrl = PublicHelper.FilterUrl(create.Url);
            var findUrl = await _newsRepository.GetByUrlNoTraking(filterUrl);
            if (findUrl != null)
            {
                throw new BadRequestException("لینک وارد شده از قبل وجود دارد");
            }

            var model = _mapper.Map<News>(create);
            model.Url = filterUrl;

            var showDateTime = create.Date + " " + create.Time;

            model.ShowDateTime = showDateTime.ToGregorianDateTime();

            if (create.Image != null && create.Image.Length > 0)
                model.ImageUrl = await _storageService.SaveFile(container, create.Image);

            await _newsRepository.Add(model);

        }
        public async Task Edit(UpsertNews edit)
        {
            var find = await _newsRepository.GetNoTracking(edit.Id.Value);
            if (find == null)
            {
                throw new NotFoundException();
            }
            var filterUrl = PublicHelper.FilterUrl(edit.Url);
            if (find.Url != filterUrl)
            {
                var findUrl = await _newsRepository.GetByUrlNoTraking(filterUrl);
                if (findUrl != null)
                {
                    throw new BadRequestException("لینک وارد شده از قبل وجود دارد");
                }
            }

            var model = _mapper.Map<News>(edit);

            model.Url = filterUrl;

            var showDateTime = edit.Date + " " + edit.Time;
            model.ShowDateTime = showDateTime.ToGregorianDateTime();

            if (edit.Image != null && edit.Image.Length > 0)
            {
                model.ImageUrl = await _storageService.EditFile(container, edit.Image, find.ImageUrl);
            }
            else
            {
                model.ImageUrl = find.ImageUrl;
            }

            await _newsRepository.Update(model);

        }
        public async Task ActiveNews(int Id, bool isActive)
        {
            var find = await _newsRepository.Get(Id);
            if (find == null)
            {
                throw new BadRequestException("عدم دسترسی");
            }
            find.IsActive = isActive;
            find.UpdatedAt = DateTime.Now;
            await _newsRepository.Update(find);

        }
        public async Task<UpsertNews> GetEditNews(int Id)
        {
            var data = await _newsRepository.GetNoTracking(Id);

            var model = _mapper.Map<UpsertNews>(data);
            model.Url = data.Url;
            model.Date = data.ShowDateTime.ToString();
            model.Time = data.ShowDateTime?.ToString("H:mm");

            return model;
        }
        public async Task<DatatableResponse<NewsDTO>> Datatable(NewsDatatableInput input, FiltersFromRequestDataTable filtersFromRequest)
        {
            var data = _newsRepository.GetAllQueryable()
                .Where(w => w.IsActive == input.IsActive)
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

            return await data.ToDataTableAsync<News, NewsDTO>(totalRecords, filtersFromRequest, _mapper);
        }
        public async Task<int> GetCount()
        {
            return _newsRepository.GetAllQueryable()
                .Where(w => w.IsActive == true && w.ShowDateTime <= DateTime.Now).AsNoTracking().Count();
        }
        public async Task<List<NewsDTO>> GetNewsData(string search, int CurrentPage, int PageSize, int? CategoryId, string Tags)
        {


            var data = _newsRepository.GetAllQueryable()
                .Include(w => w.Category)
                .Where(w => w.IsActive == true && w.ShowDateTime <= DateTime.Now);


            if (!string.IsNullOrWhiteSpace(search))
            {
                data = data.Where(w => w.Tags.Contains(search) || w.Title.Contains(search));
            }
            if (!string.IsNullOrWhiteSpace(Tags))
            {
                data = data.Where(w => w.Tags.Contains(Tags));
            }
            if (CategoryId.HasValue)
            {
                data = data.Where(w => w.CategoryId == CategoryId);
            }
            data = data.Skip((CurrentPage - 1) * PageSize).Take(PageSize);

            var model = _mapper.Map<List<NewsDTO>>(data);

            return model;


        }
        public async Task<List<NewsDTO>> GetNewsDataByTake(int? CategoryId, int Take)
        {

            var data = _newsRepository.GetAllQueryable()
                .Include(w => w.Category)
                .Where(w => w.IsActive == true && w.ShowDateTime <= DateTime.Now)
                .OrderBy(x => Guid.NewGuid()).Take(Take);

            if (CategoryId.HasValue)
            {
                data = data.Where(w => w.CategoryId == CategoryId);
            }
            var model = _mapper.Map<List<NewsDTO>>(data);

            return model;


        }

        public async Task<NewsDTO> GetNewsDetail(string Url)
        {
            var data = await _newsRepository.GetByUrlNoTraking(Url);

            return _mapper.Map<NewsDTO>(data);
        }
    }
}
