using Project.Domain.Entities.Base;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities.ProductModels
{
    public class Product : MetaBaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Attributes { get; set; }
        public string ProductType { get; set; }
        [Range(0, 100, ErrorMessage = "درصد تخفیف باید از صفر تا صد درصد باشد")]
        public double? Discount { get; set; }
        public bool? Special { get; set; }
        //در صورتی که این متغیر فعال باشد قیمت را از این متد میخواند
        public ProductTypeStatus Status { get; set; }
        public double? price { get; set; }
        public int Inventory { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string BannerUrl { get; set; }
        public List<ProductImages> Images { get; set; }
        public List<DiscountCode> DiscountCodes { get; set; }
        public string Url { get; set; }
    }
}
