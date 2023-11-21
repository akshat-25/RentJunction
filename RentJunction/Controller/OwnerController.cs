namespace RentJunction.Models
{
    public class OwnerController : IOwnerController
    {
        public IDBUsers DBUsers { get; set; }
        public OwnerController(IDBUsers DBusers)
        {
            DBUsers = DBusers;
        }       
        
        public List<User> GetOwnerList()
        {
            List<User> userList = DBUsers.UserList;

            return userList.FindAll((user) => user.Role == Role.Owner);
        }   
    }
}
