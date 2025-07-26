using Project.Application.DTOs.Base;
using Project.Application.DTOs.Product;
using Project.Domain.Entities;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.CartItem
{
    public class CartItemDTO
    {
        public int ItemId { get; set; }

        public int? ProductId { get; set; }
        public ProductDTO Product { get; set; }

        public string Type { get; set; }

        public double Price { get; set; }
        public int Count { get; set; }
        public string LocalCode { get; set; }

    }
}
