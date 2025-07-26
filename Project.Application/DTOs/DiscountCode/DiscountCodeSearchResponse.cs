using Project.Application.DTOs.DiscountCode;
using System.Collections.Generic;

namespace Project.Application.DTOs.Product
{
    public class DiscountCodeSearchResponse
    {
        public int TotalCount { get; set; }
        public int Size { get; set; }
        public int CurrentPage { get; set; }
        public IEnumerable<DiscountCodeDTO> Data { get; set; }
    }
}
