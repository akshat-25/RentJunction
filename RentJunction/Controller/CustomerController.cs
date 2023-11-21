
namespace RentJunction.Models
{
    public class CustomerController : ICustomerController
    {
        public IDBUsers DBUsers { get; set; }
        public CustomerController(IDBUsers DBusers)
        {
            DBUsers = DBusers;
        }
        public List<User> GetCustomer()
        {   
            List<User> userList = DBUsers.UserList;

            return userList.FindAll((user) => user.Role == Role.Customer);
        }     
    }
}
    