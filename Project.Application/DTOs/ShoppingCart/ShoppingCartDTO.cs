using Project.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.ShoppingCart
{
    public class ShoppingCartDTO
    {
        public List<ProductDTO> Products { get; set; }
    }
}
