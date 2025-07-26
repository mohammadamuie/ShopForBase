using Project.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities.ProductModels
{
    public class ProductImages : BaseEntity
    {
        public string Url { get; set; }
        public string Alt { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
