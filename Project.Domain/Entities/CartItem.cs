using Project.Domain.Entities.Base;
using Project.Domain.Entities.ProductModels;

namespace Project.Domain.Entities.News
{
    public class CartItem : BaseEntity
    {
        public Product Product { get; set; }
        public int ProductId { get; set; }

        public string Type { get; set; }

        public double Price { get; set; }
        public int Count { get; set; }
        public PurchaseRequest PurchaseRequest { get; set; }
        public int PurchaseRequestId { get; set; }


        public string LocalCode { get; set; }
    }
}
