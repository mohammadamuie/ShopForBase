using Project.Application.DTOs.User;

namespace Project.Application.DTOs.ShoppingCart
{
    public class CheckoutDTO
    {
        public UserProfile Profile { get; set; }
        public double TotalPrice { get; set; }
        public double SendPrice { get; set; }
    }
}
