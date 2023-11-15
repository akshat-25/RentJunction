using Newtonsoft.Json;
using RentJunction.Models;

namespace RentJunction.Controller
{
    public class Customer : User 
    {
        public List<RentedProduct> rentedProducts;

    }

}
