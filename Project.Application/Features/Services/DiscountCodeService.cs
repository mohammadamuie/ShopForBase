using AutoMapper;
using DNTPersianUtils.Core;
using Microsoft.EntityFrameworkCore;
using Project.Application.Contracts.Persistence;
using Project.Application.DTOs.DataTable;
using Project.Application.DTOs.DiscountCode;
using Project.Application.DTOs.Product;
using Project.Application.Extensions;
using Project.Application.Features.Interfaces;
using Project.Domain.Entities;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Services
{
    public class DiscountCodeService : IDiscountCodeService
    {
        private readonly IDiscountCodeRepository _codeRepository;
        private readonly IMapper _mapper;
        private readonly IIdentityUserService _identityUserService;

        public DiscountCodeService(IDiscountCodeRepository codeRepository, IMapper mapper, IIdentityUserService identityUserService)
        {
            _codeRepository = codeRepository;
            _mapper = mapper;
            _identityUserService = identityUserService;
        }

        public async Task Create(UpsertDiscountCode entity)
        {
            var model = new DiscountCode();
            switch (entity.DiscountCodeType)
            {
                case DiscountCodeType.ForProduct:
                    model.ProductId = entity.ProductId;
                    break;
                case DiscountCodeType.ForUser:
                    model.UserId = entity.UserId;
                    break;
                case DiscountCodeType.ForPeoductAndUser:
                    model.UserId = entity.UserId;
                    model.ProductId = entity.ProductId;
                    break;
                case DiscountCodeType.DiscountForAll:
                    break;
            }
            if (entity.Code != null)
            {
                await _codeRepository.CheckCodeAsync(entity.Code);
                model.Code = entity.Code;
            }
            else
            {
                var code = Guid.NewGuid().ToString().Substring(0, 7);
                await _codeRepository.CheckCodeAsync(code);
                model.Code = code;
            }
            model.Discount = entity.Discount;
            model.Description= entity.Description;
            model.DiscountCodeType = entity.DiscountCodeType;
            model.ExpireTime = entity.ExpireTime.ToGregorianDateTime();
            await _codeRepository.Add(model);
        }

        public async Task Edit(UpsertDiscountCode entity)
        {
            var model = await _codeRepository.Get(entity.Id.Value);
            switch (entity.DiscountCodeType)
            {
                case DiscountCodeType.ForProduct:
                    model.ProductId = entity.ProductId;
                    model.UserId = null;
                    break;
                case DiscountCodeType.ForUser:
                    model.UserId = entity.UserId;
                    model.ProductId = null;
                    break;
                case DiscountCodeType.ForPeoductAndUser:
                    model.UserId = entity.UserId;
                    model.ProductId = entity.ProductId;
                    break;
                case DiscountCodeType.DiscountForAll:
                    model.UserId = null;
                    model.ProductId = null;
                    break;
            }
            if (entity.Code != null)
            {
                if (model.Code != entity.Code)
                {
                    await _codeRepository.CheckCodeAsync(entity.Code);
                    model.Code = entity.Code;
                }
            }
            else
            {
                var code = Guid.NewGuid().ToString().Substring(0, 7);
                await _codeRepository.CheckCodeAsync(code);
                model.Code = code;
            }
            model.DiscountCodeType = entity.DiscountCodeType;
            model.Description = entity.Description;
            model.Discount = entity.Discount;
            model.ExpireTime = entity.ExpireTime.ToGregorianDateTime();
            await _codeRepository.Update(model);
        }
        public async Task<DiscountCodeSearchResponse> GetData(int page, int? countpage)
        {
            var user =await _identityUserService.CurrentLoginDTO();

            page = page < 1 ? 1 : page;

            int count = 12;
            var data = _codeRepository.GetAllQueryable()
                .Where(w => w.IsActive == true && w.UserId == user.Id.ToString())
                .OrderByDescending(o => o.Id)
                .AsQueryable();
            var model = new DiscountCodeSearchResponse();


            model.TotalCount = data.Count();

            var product = await data.Skip((page - 1) * count).Take(count).ToListAsync();
            var productDTO = _mapper.Map<IEnumerable<DiscountCodeDTO>>(product);
            model.Data = productDTO;
            model.Size = countpage.Value;
            model.CurrentPage = page;
            return model;
        }

        public async Task<DatatableResponse<DiscountCodeDataTable>> GetDataTable(DiscountCodeDataTableInput input, FiltersFromRequestDataTable filtersFromRequest)
        {
            var data = _codeRepository.GetAllQueryable()
                .Where(w => w.IsActive == input.IsActive);


            var totalRecords = await data.CountAsync();

            if (input.Id.HasValue && input.Id.Value > 0)
            {
                data = data.Where(w => w.Id == input.Id.Value);
            }

            if (!string.IsNullOrEmpty(filtersFromRequest.SearchValue))
            {
                data = data.Where(w =>
                    w.Code.ToLower().Contains(filtersFromRequest.SearchValue.Trim().ToLower())
                );
            }


            return await data.ToDataTableAsync<DiscountCode, DiscountCodeDataTable>(totalRecords, filtersFromRequest, _mapper);

        }
    }
}
