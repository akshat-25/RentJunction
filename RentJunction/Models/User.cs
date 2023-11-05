using System;

namespace RentJunction.Models
{
   
    public class User
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public long PhoneNumber { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role role { get; set; }
    }
}