using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.Product
{
    public class ProductDetailDTO
    {
        public ProductDTO Product { get; set; }
        public List<ProductDTO>  ProductList { get; set; }

    }
}
