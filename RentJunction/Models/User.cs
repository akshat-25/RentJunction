namespace RentJunction.Models
{
    public class User
    {
        public string UserID { get; set; }
        public string FullName { get; set; }
        public string City { get; set; }
        public long PhoneNumber { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}