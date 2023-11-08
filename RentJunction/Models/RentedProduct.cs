using RentJunction.Models;

namespace RentJunction.Controller
{
    public class RentedProduct : Product
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}