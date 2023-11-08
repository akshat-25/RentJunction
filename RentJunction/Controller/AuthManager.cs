
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
    public bool Register<T>(T entity) where T : class
    {
        if (DBAuth.Instance.DbRegister(entity))
        {
            return true;
        }

        Console.WriteLine("User already exist....");
        return false;

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