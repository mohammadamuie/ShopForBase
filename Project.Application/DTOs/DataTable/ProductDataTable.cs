using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Application.DTOs.DataTable
{
    public class ProductDataTable
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string BannerUrl { get; set; }
        public int? Discount { get; set; }
        public string CategoryName { get; set; }
        public string Status { get; set; }
        public string Price { get; set; }
        public bool? IsActive { get; set; }
        public bool? Special { get; set; }
        public string CreatedAt { get; set; }
    }
    public class ProductDataTableInput : DatatableInput
    {
        public string Title { get; set; }
    }

}
