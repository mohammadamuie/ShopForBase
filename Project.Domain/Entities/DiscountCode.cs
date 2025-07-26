using Project.Domain.Entities.Base;
using Project.Domain.Entities.ProductModels;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities
{
    public class DiscountCode : BaseEntity
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public int Discount { get; set; }
        public string UserId { get; set; }
        public DiscountCodeType DiscountCodeType { get; set; }
        public DateTime? ExpireTime { get; set; }
        public int? ProductId { get; set; }
        public Product Product { get; set; }
        public List<DiscountCodesUsed> DiscountCodesUseds { get; set; }
    }
}
