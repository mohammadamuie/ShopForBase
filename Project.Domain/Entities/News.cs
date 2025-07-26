using Project.Domain.Entities.Base;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities.News
{
    public class News : BaseEntity
    {
        public string Title { get; set; }

        public string ShortContent { get; set; }

        public string Content { get; set; }

        public string MetaDescription { get; set; }

        public string MetaKeywords { get; set; }

        public string MetaTitle { get; set; }

        public string ImageUrl { get; set; }

        public string Tags { get; set; }

        public string Url { get; set; }

        public DateTime? ShowDateTime { get; set; }

        public int CategoryId { get; set; }

        public NewsCategory Category { get; set; }

    }
}
