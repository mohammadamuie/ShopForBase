using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Enums
{
    public enum DiscountCodeType
    {
        [Display(Name = "فقط برای محصول")]
        ForProduct,
        [Display(Name = "فقط برای کاربر")]
        ForUser,
        [Display(Name = "محصول و کاربر")]
        ForPeoductAndUser,
        [Display(Name = "کد تخفیف برای همه")]
        DiscountForAll
    }
}
