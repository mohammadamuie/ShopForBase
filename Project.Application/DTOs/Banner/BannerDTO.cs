using Project.Application.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.BakeryCategory
{
    public class BannerDTO : BaseDTO
    {
        public string Url { get; set; }
        public string Image { get; set; }
    }
}
