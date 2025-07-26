using Project.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities
{
    public class DiscountCodesUsed : BaseEntity
    {
        public string UserId { get; set; }
        public int DiscountCodeId { get; set; }
        public DiscountCode DiscountCode { get; set; }
    }
}
