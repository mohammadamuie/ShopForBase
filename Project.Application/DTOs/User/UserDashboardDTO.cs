using System;
using System.Collections;
using System.Collections.Generic;
using Project.Application.DTOs.Base;
using Project.Application.DTOs.DiscountCode;
using Project.Application.DTOs.Product;

namespace Project.Application.DTOs.User
{
    public class UserDashboardDTO 
    {
        public UserProfile Profile { get; set; }
        public DiscountCodeSearchResponse DiscountCode { get; set; }
    }
}
