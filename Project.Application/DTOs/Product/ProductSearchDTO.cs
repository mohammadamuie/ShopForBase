using Microsoft.AspNetCore.Http;
using Project.Domain.Entities.ProductModels;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.Product
{
    public class ProductSearchDTO
    {
        public int Id { get; set; }

        public int Inventory { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }

        public double? Discount { get; set; }

        public double? price { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public string BannerImage { get; set; }

        public double? DiscountPrice { get; set; }
        public bool? Special { get; set; }
    }
}
