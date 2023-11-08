
using RentJunction.Models;

namespace RentJunction.Controller
{
    public class Owner : User
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public List<Product> ListedProducts;
     
    }
}