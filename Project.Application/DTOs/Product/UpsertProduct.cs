using Microsoft.AspNetCore.Http;
using Project.Application.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.Product
{
    public class UpsertProduct
    {
        public int? Id { get; set; }

        [Display(Name = "موجودی محصول")]
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public int Inventory { get; set; }
        [Display(Name = "آدرس صفحه")]
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public string Url { get; set; }
        [Display(Name = "عنوان محصول")]
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public string Title { get; set; }

        [Display(Name = "توضیحات محصول")]
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public string Description { get; set; }

        [Display(Name = "ویژگی های محصول")]
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public List<ProductAttributes> Attributes { get; set; }

        public bool TypeIsColor { get; set; }
        [Range(0, 100, ErrorMessage = "درصد تخفیف باید از صفر تا صد درصد باشد")]
        public int? Discount { get; set; }
        public bool IsPrice { get; set; }
        [Display(Name = "تک قیمت محصول")]
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public double? price { get; set; }
        [Display(Name = "دسته بندی محصول")]
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public int CategoryId { get; set; }
        [Display(Name = "تصویر محصول")]
        public IFormFile BannerUrl { get; set; }
        public string BannerImage { get; set; }
        public List<ProductType> ProductType { get; set; }
        [Display(Name = "متا عنوان")]
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public string MetaTitle { get; set; }
        [Display(Name = "متا کلمات کلیدی")]
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public string MetaKeyWord { get; set; }
        [Display(Name = "متا توضیحات")]
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public string MetaDescription { get; set; }
    }
}
