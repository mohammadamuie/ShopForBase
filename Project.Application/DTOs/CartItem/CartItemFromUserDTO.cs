using Project.Application.DTOs.Base;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.CartItem
{
    public class CartItemFromUserDTO
    {
        public int? id { get; set; }
     
        public int quantity { get; set; }

        public string key { get; set; }


    }
}
