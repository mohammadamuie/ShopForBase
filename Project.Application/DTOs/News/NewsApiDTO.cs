using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.News
{
    public class NewsApiData
    {
        public string news_url { get; set; }
        public string image_url { get; set; }
        public string title { get; set; }
        public string text { get; set; }
        public string source_name { get; set; }
        public string date { get; set; }
        public List<string> topics { get; set; }
        public string sentiment { get; set; }
        public string type { get; set; }

        public DateTime? CreateDate { get; set; }
    }

    public class NewsApiDTO
    {
        public List<NewsApiData> data { get; set; }
        public string Message { get; set; }
    }
}
