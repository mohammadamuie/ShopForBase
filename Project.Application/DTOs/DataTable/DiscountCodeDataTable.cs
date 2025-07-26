using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Application.DTOs.DataTable
{
    public class DiscountCodeDataTable
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string DiscountCodeType { get; set; }
        public DiscountCodeType DiscountCodeTypeEnum { get; set; }
        public int Discount { get; set; }
        public int? ProductId { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; }
        public string ExpireTime { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedAt { get; set; }
    }
    public class DiscountCodeDataTableInput : DatatableInput
    {
     
    }
    
}
