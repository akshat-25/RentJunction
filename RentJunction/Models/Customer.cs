using Newtonsoft.Json;
using RentJunction.Models;

namespace RentJunction.Controller
{
    public class Customer : User 
    {
        public List<RentedProduct> rentedProducts;

        //public Customer(string fullName, string address, long phNo, string email, string password, int role,string username)
        //{
        //    this.PhoneNumber = phNo;
        //    this.Address = address;
        //    this.Email = email;
        //    this.Password = password;
        //    this.FullName   = fullName;
        //    this.role = (Role)role;
        //    this.Username = username;

        //}
       
    }

}
