using RentJunction.Models;
public class UserController : IUserController
{
    public IDBUsers DBUsers { get; set; }

    public UserController(IDBUsers DBusers)
    {
        DBUsers = DBusers;
    }
    public List<User> GetUserMasterList()
    {
        return DBUsers.UserList;
    }
    public void UpdateDBUser(List<User> list)
    {
        DBUsers.UpdateDB(Strings.userPath, list);
    }
}