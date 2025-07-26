using Microsoft.AspNetCore.Http;
using Project.Application.DTOs.Category;
using Project.Domain.Entities;
using Project.Domain.Entities.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.Product
{
    public class ProductDTO
    {
        public int Id { get; set; }

        public int Inventory { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public List<ProductAttributes> Attributes { get; set; }

        public bool TypeIsColor { get; set; }

        public int? Discount { get; set; }

        public bool IsPrice { get; set; }

        public bool? Special { get; set; }

        public double? price { get; set; }

        public double? DiscountPrice { get; set; }

        public CategoryDTO Category { get; set; }
        public int CategoryId { get; set; }

        public IFormFile BannerUrl { get; set; }

        public string BannerImage { get; set; }

        public List<ProductType> ProductType { get; set; }

        public List<ProductImages> Galey { get; set; }

        public string MetaTitle { get; set; }

        public string MetaKeyWord { get; set; }

        public string MetaDescription { get; set; }

        public int CartQuantity { get; set; }

    }
}
