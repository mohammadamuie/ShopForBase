using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Enums
{
    public enum ProductTypeStatus
    {
        [Display(Name = "تک قیمت")]
        Singleprice,
        [Display(Name = "سایز")]
        Size,
        [Display(Name = "رنگ")]
        Color,
    }
}
