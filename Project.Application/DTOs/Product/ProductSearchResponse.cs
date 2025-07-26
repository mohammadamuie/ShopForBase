using System.Collections.Generic;

namespace Project.Application.DTOs.Product
{
    public class ProductSearchResponse
    {
        public int TotalCount { get; set; }
        public int Size { get; set; }
        public int CurrentPage { get; set; }
        public IEnumerable<ProductSearchDTO> Data { get; set; }
    }
}
