using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Application.DTOs.DataTable
{
    public class CategoryDataTable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Parent { get; set; }
        public string Rote { get; set; }
        public string Image { get; set; }
        public bool? Special { get; set; }
        public int? ParentId { get; set; }
        public int ChildsCount { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedAt { get; set; }
    }
    public class CategoryDataTableInput : DatatableInput
    {
        public string Name { get; set; }
    }
    
}
