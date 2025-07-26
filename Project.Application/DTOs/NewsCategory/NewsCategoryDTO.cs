using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.NewsCategory
{
    public class NewsCategoryDTO
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public int NewsCount { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
