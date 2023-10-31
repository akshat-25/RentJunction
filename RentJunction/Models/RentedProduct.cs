using RentJunction.Models;

namespace RentJunction.Controller
{
    public class RentedProduct : Product
    {
        public string startDate { get; set; }
        public string endDate { get; set; }
    }
}