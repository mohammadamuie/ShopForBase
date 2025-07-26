using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Project.Application.Contracts.Infrastructure;
using Project.Application.Contracts.Persistence;
using Project.Application.DTOs.DataTable;
using Project.Application.DTOs.Product;
using Project.Application.Exceptions;
using Project.Application.Extensions;
using Project.Application.Features.Interfaces;
using Project.Application.Helpers;
using Project.Domain.Entities;
using Project.Domain.Entities.ProductModels;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Project.Application.Features.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductImagesRepository _productImagesRepository;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _storageService;
        private readonly string container;
        public ProductService(IProductRepository productRepository, IFileStorageService storageService, IMapper mapper, IProductImagesRepository productImagesRepository)
        {
            _productRepository = productRepository;
            _storageService = storageService;
            _mapper = mapper;
            _productImagesRepository = productImagesRepository;
            container = "product";

        }


        public async Task<DatatableResponse<ProductDataTable>> GetDataTable(ProductDataTableInput input, FiltersFromRequestDataTable filtersFromRequest)
        {
            var data = _productRepository.GetAllQueryable()
                .Include(w => w.Category)
                .Where(w => w.IsActive == input.IsActive)
                .AsNoTracking();

            var totalRecords = await data.CountAsync();

            if (input.Id.HasValue && input.Id.Value > 0)
            {
                data = data.Where(w => w.Id == input.Id.Value);
            }

            if (!string.IsNullOrEmpty(filtersFromRequest.SearchValue))
            {
                data = data.Where(w =>
                    w.Title.ToLower().Contains(filtersFromRequest.SearchValue.Trim().ToLower()) ||
                    w.Id.ToString().Contains(filtersFromRequest.SearchValue.Trim().ToLower())
                );
            }

            if (!string.IsNullOrEmpty(input.Title))
            {
                data = data.Where(w => w.Title.ToLower().Contains(input.Title.Trim().ToLower()));
            }

            return await data.ToDataTableAsync<Product, ProductDataTable>(totalRecords, filtersFromRequest, _mapper);

        }
        public async Task<int> Create(UpsertProduct entity)
        {
            if (entity.BannerUrl == null)
            {
                throw new BadRequestException("تصویر محصول را وارد کنید");
            }
            var getUrl = await _productRepository.GetByUrlNoTraking(entity.Url);
            if (getUrl != null)
            {
                throw new BadRequestException("لینک وارد شده از قبل وجود دارد لطفا لینک جدید را وارد کنید");
            }

            if (entity.Attributes == null)
            {
                throw new BadRequestException("لطفا ویژگی های محصول را وارد کنید");
            }

            var model = _mapper.Map<Product>(entity);
            if (entity.IsPrice == true)
            {
                if (entity.price == null)
                {
                    throw new BadRequestException("لطفا قیمت را وارد کنید");
                }
                model.Status = ProductTypeStatus.Singleprice;
            }
            else
            {
                if (entity.ProductType == null)
                {
                    throw new BadRequestException("لطفا انواع محصول را وارد کنید");
                }
                if (entity.TypeIsColor)
                {
                    model.Status = ProductTypeStatus.Color;
                }
                else
                {
                    model.Status = ProductTypeStatus.Size;
                }
                model.ProductType = JsonConvert.SerializeObject(entity.ProductType);
            }

            model.Url = PublicHelper.FilterUrl(entity.Url);
            model.Attributes = JsonConvert.SerializeObject(entity.Attributes);

            if (entity.BannerUrl != null && entity.BannerUrl.Length > 0)
                model.BannerUrl = await _storageService.SaveFile(container, entity.BannerUrl);

            await _productRepository.Add(model);
            return model.Id;
        }
        public async Task Edit(UpsertProduct entity)
        {
            var product = await _productRepository.Get(entity.Id.Value);
            if (product == null)
            {
                throw new NotFoundException();
            }
            if (product.Url != entity.Url)
            {
                var getUrl = await _productRepository.GetByUrlNoTraking(entity.Url);
                if (getUrl != null)
                {
                    throw new BadRequestException("لینک وارد شده از قبل وجود دارد لطفا لینک جدید را وارد کنید");
                }
            }
            if (entity.IsPrice == true)
            {
                if (entity.price == null)
                {
                    throw new BadRequestException("لطفا قیمت را وارد کنید");
                }
                product.Status = ProductTypeStatus.Singleprice;
            }
            else
            {
                if (entity.ProductType == null)
                {
                    throw new BadRequestException("لطفا انواع محصول را وارد کنید");
                }
                if (entity.TypeIsColor)
                {
                    product.Status = ProductTypeStatus.Color;
                }
                else
                {
                    product.Status = ProductTypeStatus.Size;
                }
                product.ProductType = JsonConvert.SerializeObject(entity.ProductType);
            }
            if (entity.Attributes == null)
            {
                throw new BadRequestException("لطفا ویژگی های محصول را وارد کنید");
            }

            product.Url = PublicHelper.FilterUrl(entity.Url);
            product.price = entity.price;
            product.Title = entity.Title;
            product.Description = entity.Description;
            product.Inventory = entity.Inventory;
            product.Attributes = JsonConvert.SerializeObject(entity.Attributes);
            product.CategoryId = entity.CategoryId;
            product.Discount = entity.Discount;
            product.MetaTitle = entity.MetaTitle;
            product.MetaDescription = entity.MetaDescription;
            product.MetaKeyWord = entity.MetaKeyWord;
            if (entity.BannerUrl != null && entity.BannerUrl.Length > 0)
                product.BannerUrl = await _storageService.SaveFile(container, entity.BannerUrl);

            await _productRepository.Update(product);
        }
        public async Task<ProductDTO> GetProductDTO(int Id)
        {
            var data = await _productRepository.GetNoTracking(Id);

            var model = _mapper.Map<ProductDTO>(data);
            model.Url = data.Url;
            model.BannerImage = data.BannerUrl;
            model.IsPrice = data.Status == ProductTypeStatus.Singleprice ? true : false;
            model.TypeIsColor = data.Status == ProductTypeStatus.Color ? true : false;
            model.Attributes = data.Attributes != null ? (List<ProductAttributes>)JsonConvert.DeserializeObject(data.Attributes, typeof(List<ProductAttributes>)) : null;
            model.ProductType = data.ProductType != null ? (List<ProductType>)JsonConvert.DeserializeObject(data.ProductType, typeof(List<ProductType>)) : null;
            model.Galey = await _productImagesRepository.GetByProductId(Id);

            return model;
        }
        
        public async Task<ProductDTO> GetProductDetailDTO(string Url)
        {
            var data = await _productRepository.GetByUrlNoTraking(Url);
            if (data == null)
            {
                throw new NotFoundException();
            }
            var model = _mapper.Map<ProductDTO>(data);
            model.BannerImage = data.BannerUrl;
            model.IsPrice = data.Status == ProductTypeStatus.Singleprice ? true : false;
            model.TypeIsColor = data.Status == ProductTypeStatus.Color ? true : false;
            model.Attributes = data.Attributes != null ? (List<ProductAttributes>)JsonConvert.DeserializeObject(data.Attributes, typeof(List<ProductAttributes>)) : null;
            model.ProductType = data.ProductType != null ? (List<ProductType>)JsonConvert.DeserializeObject(data.ProductType, typeof(List<ProductType>)) : null;
            model.Galey = await _productImagesRepository.GetByProductId(data.Id);
            model.DiscountPrice = (data.Discount != null ? data.price - ((data.Discount / 100) * data.price) : null);

            return model;
        }
        public async Task Active(int id)
        {
            await _productRepository.Recover(id);
        }
        public async Task Delete(int id)
        {
            await _productRepository.Delete(id);
        }
        public async Task Special(int id)
        {
            var find = await _productRepository.GetAllQueryable().FirstOrDefaultAsync(w => w.Id == id);
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
            await _productRepository.Update(find);
        }
        public async Task<ProductImages> AddProductGallery(IFormFile Image, int ProductId)
        {
            var product = await _productRepository.Get(ProductId);
            if (product == null)
            {
                throw new NotFoundException();
            }

            if (Image != null && Image.Length > 0)
            {
                var model = new ProductImages();
                model.Url = await _storageService.SaveFile(container, Image);
                model.ProductId = product.Id;

                await _productImagesRepository.Add(model);
                return model;
            }
            else
            {
                throw new BadRequestException("تصویر را وارد وارد کنید");
            }
        }
        public async Task DeleteProductGallery(int Id)
        {
            await _productImagesRepository.Remove(Id);
        }

        public async Task<ProductSearchResponse> GetData(string search, int page, int? category, int? countpage, int? Discount, bool? Special, bool? random)
        {
            page = page < 1 ? 1 : page;

            int count = 12;
            var data = _productRepository.GetAllQueryable()
                .Include(p => p.Category)
                .Where(w => w.IsActive == true)
                .OrderByDescending(o => o.Id)
                .AsQueryable();
            var model = new ProductSearchResponse();

            if (!string.IsNullOrWhiteSpace(search))
            {
                data = data.Where(w => w.Title.Contains(search));
            }
            if (Special.HasValue)
            {
                data = data.Where(w => w.Special == Special);
            }
            if (category.HasValue)
            {
                data = data.Where(w => w.CategoryId == category);
            }
            if (Discount.HasValue)
            {
                data = data.Where(w => w.Discount == Discount);
            }
            if (countpage.HasValue)
            {
                count = (int)countpage;
            }

            if (random.HasValue)
            {
                data = data.OrderBy(x => Guid.NewGuid());
            }

            model.TotalCount = data.Count();

            var product = await data.Skip((page - 1) * count).Take(count)
                 .Select(s => new ProductSearchDTO
                 {
                     Id = s.Id,
                     Url = s.Url,
                     CategoryId = s.CategoryId,
                     CategoryName = s.Category.Name,
                     Title = s.Title,
                     BannerImage = s.BannerUrl,
                     price = s.price,
                     Discount = s.Discount,
                     Inventory = s.Inventory,
                     Special = s.Special,
                     DiscountPrice = (s.Discount != null ? s.price - ((s.Discount / 100) * s.price) : null)
                 }).ToListAsync();

            model.Data = product;
            model.Size = countpage.Value;
            model.CurrentPage = page;
            return model;
        }
        public async Task<List<ProductDTO>> GetByTake(int take, int CategoryId)
        {
            var data = await _productRepository.GetAllQueryable().Include(w => w.Category).Where(w => w.IsActive == true && w.CategoryId == CategoryId).Take(take).ToListAsync();
            return _mapper.Map<List<ProductDTO>>(data);
        }
        public async Task<IEnumerable<SelectListItem>> GetSelectOptionData()
        {
            var data = await _productRepository.GetAll();
            return new SelectList(data, "Id", "Title").Append(new SelectListItem
            {
                Value = "0",
                Text = "محصول مورد نظر را انتخاب کنید",
                Selected = true,
                Disabled = false,
                Group = null
            });
        }


        public async Task UpdateQuantity(int id, int count, bool isMinus)
        {
            var find = await _productRepository.GetAllQueryable().SingleOrDefaultAsync(x => x.Id == id);
            if (isMinus)
            {
                find.Inventory -= count;
            }
            else
            {
                find.Inventory += count;
            }
            await _productRepository.Update(find);
        }
        public async Task CheckQuantity(int id, int count)
        {
            var find = await _productRepository.GetAllQueryable().SingleOrDefaultAsync(x => x.Id == id);
            if (count > find.Inventory)
            {
                throw new BadRequestException($"موجودی {find.Title} {find.Inventory} عدد می باشد");
            }
        }
    }
}
