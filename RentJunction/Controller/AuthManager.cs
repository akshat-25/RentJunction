using RentJunction.Controller;
using RentJunction.Models;


public sealed class AuthManager
{
    private static AuthManager _instance = null;
    private static readonly object _lockObj = new object();

    private AuthManager() { }
    public static AuthManager Instance
    {
        get
        {
            lock (_lockObj)
            {
                if (_instance == null)
                {
                    _instance = new AuthManager();
                }
            }
            return _instance;
        }
    }
    public bool Register(User user)
    {
        if(user.role == Role.Customer)
        {
           Customer customer = new Customer()
            {
                Email = user.Email,
                Password = user.Password,
                Address = user.Address,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Username = user.Username,
                role = Role.Customer,
            };
            if (DbHandler.Instance.DbRegister(customer))
            {
                return true;
            }

            return false;

        }
        else
        {
           Owner owner = new Owner()
            {
                Email = user.Email,
                Password = user.Password,
                Address = user.Address,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Username = user.Username,
                role = Role.Owner,
            };
            if (DbHandler.Instance.DbRegister(owner))
            {
                return true;
            }
            return false;
        }
        
    }
    public Object Login(string username, string password)
    {
        var customer = DbHandler.Instance.Login(username,password);    
        
        if(customer != null)
        {
            return customer;
        }

        var owner = DbHandler.Instance.Login(username,password);  
        if(owner != null) 
        { 
        return owner;
        }

        var admin = DbHandler.Instance.Login(username,password);
        if(admin != null)
        {
            return admin;
        }

        return null;     
    }
    public bool AddAdmin(string username, string password)
    {
        Admin admin = new Admin
        {
            Username= username,
            password= password,
        };

        if (DbHandler.Instance.DbRegisterAdmin(admin))
        {
            return true;
        }

        return false;
    }

 
}