using Project.Application.DTOs.BakeryCategory;
using Project.Application.DTOs.Category;
using Project.Application.DTOs.News;
using Project.Application.DTOs.Product;
using Project.Application.DTOs.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.HomePage
{
    public class HomeDTO
    {
        public ProductSearchResponse SpecialProducts { get; set; }
        public ProductSearchResponse LastProducts { get; set; }
        public ProductSearchResponse RandomProducts { get; set; }
        public List<NewsDTO> RandomNews { get; set; }
        public List<CategoryDTO> SpecialCategories { get; set; }
        public List<BannerDTO> Banner { get; set; }
        public SettingDTO Setting { get; set; }
    }
}
