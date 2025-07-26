using Project.Application.Helpers;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.DiscountCode
{
    public class UpsertDiscountCode
    {
        public int? Id { get; set; }
        
        public string Description { get; set; }
        
        public string Code { get; set; }


        [Display(Name = "درصد تخفیف")]
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        [Range(0, 100, ErrorMessage = "درصد تخفیف باید از صفر تا صد درصد باشد")]
        public int Discount { get; set; }
        
        public string UserId { get; set; }
        
        public int ProductId { get; set; }
        
        [Display(Name ="تاریخ منسوخ شدن")]
        [Required(ErrorMessage =PublicHelper.RequiredValidationErrorMessage)]
        public string ExpireTime { get; set; }
        
        public DiscountCodeType DiscountCodeType { get; set; }
    }
}
