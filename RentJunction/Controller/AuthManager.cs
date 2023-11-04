using RentJunction.Controller;
using RentJunction.Models;
using RentJunction.Views;


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

    
     public bool Register(object entity)
    {
       if(entity is Customer)
        {
           Customer customer = (Customer)entity;
            if (DBAuth.Instance.DbRegister(customer))
            {
                return true;
            }
            return false;
        }

       else if(entity is Owner)
        {
            Owner owner = (Owner)entity;
            if(DBAuth.Instance.DbRegister(owner))
            {
                return true;
            }
            return false;
        }

       else
        {
            Admin admin = (Admin)entity;
            if (DBAuth.Instance.DbRegister(admin))
            {
                return true;
            }
            return false;
        }
    }
    
    public Object Login(string username, string password)
    {
        var customer = DBAuth.Instance.Login(username,password);    
        
        if(customer != null)
        {
            return customer;
        }

        var owner = DBAuth.Instance.Login(username,password);  
        if(owner != null) 
        { 
        return owner;
        }

        var admin = DBAuth.Instance.Login(username,password);
        if(admin != null)
        {
            return admin;
        }

        return null;     
    }

}