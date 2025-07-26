using Project.Domain.Entities.Base;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities.News
{
    public class NewsCategory : BaseEntity
    {
        public string Name { get; set; }

        public List<News> News { get; set; }

    }
}
