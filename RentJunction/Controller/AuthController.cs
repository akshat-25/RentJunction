using RentJunction.Models;
using RentJunction.Views;

public class AuthController : IAuthController
{
    public IDBUsers DBusers { get; set; }
    public AuthController(IDBUsers DBusers) { 
         this.DBusers = DBusers;
    }
    public bool Register(User user)
    {
        if (IsExists(user.Username))
        {
            return false;
        }

        if (DBusers.UserList != null)
        {
            foreach (var localUser in DBusers.UserList)
            {
                if (localUser.Email == user.Email)
                {
                    Console.WriteLine(Strings.emailExist);
                    return false;
                }
            }
            DBusers.UserList.Add(user);
            DBusers.UpdateDB(Strings.userPath, DBusers.UserList);
            return true;
        }
        DBusers.UserList.Add(user);
        DBusers.UpdateDB(Strings.userPath, DBusers.UserList);
        return true;

    }
    private bool IsExists(string username)
    {
        if (DBusers.UserList.FindIndex((user) => user.Username == username) != -1)
            return true;

        return false;
    }
    private User Login(string username, string password)
    {
        var isValidUser = DBusers.UserList.FindIndex((cust) => cust.Username == username && cust.Password == password);
        if ( isValidUser != -1)
            return DBusers.UserList[isValidUser];

        else
            return null;
    }
    public bool GetUserUI(string username, string password)
    {
        var user = Login(username, password);

        if (user != null)
        {
            if (user.Role.Equals(Role.Customer))
            {
                ICustomerUI custUIObj = new CustomerUI(new CustomerController(DBUsers.Instance), new ProductController(DBProduct.Instance));
               
                custUIObj.LoginCustomerMenu(user);

                return true;
            }
            else if (user.Role.Equals(Role.Owner))
            {
                IOwnerUI ownerUIObj = new OwnerUI(new OwnerController(DBUsers.Instance), new CustomerController(DBUsers.Instance), new ProductController(DBProduct.Instance));
                
                ownerUIObj.LoginOwnerMenu(user);

                return true;
            }
            else if (user.Role.Equals(Role.Admin))
            {
                IAdminUI adminUIObj = new AdminUI(new CustomerController(DBUsers.Instance), new OwnerController(DBUsers.Instance), new UserController(DBUsers.Instance));
                
                adminUIObj.LoginAdminMenu(user);

                return true;
            }
            else
            {
                return false;
            }

        }
        else
        {
            return false;
        }
    }

}
