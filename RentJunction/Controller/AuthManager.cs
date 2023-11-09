
public class AuthManager
{
  
 
        
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