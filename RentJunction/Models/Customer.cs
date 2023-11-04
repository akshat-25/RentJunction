using Newtonsoft.Json;
using RentJunction.Models;

namespace RentJunction.Controller
{
    public class Customer : User , IUser
    {
        public List<RentedProduct> rentedProducts;

        public string Username { get; set; }
        public string Password { get; set; }
    }
}
