using Project.Application.DTOs.NewsCategory;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.News
{
    public class NewsDetailDTO
    {
        public NewsDTO News { get; set; }
        public List<NewsDTO> RandomNews { get; set; }
    }
}
