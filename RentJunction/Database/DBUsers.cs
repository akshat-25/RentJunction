using Newtonsoft.Json;
using RentJunction.Models;

public sealed class DBUsers : DBHandler , IDBUsers
{
    private static DBUsers _instance = null;
    private static readonly object _lockObj = new object();
    public List<User> UserList { get; set; }
    public static DBUsers Instance
    {
        get
        {
            lock (_lockObj)
            {
                if (_instance == null)
                {
                    _instance = new DBUsers();
                }
            }
            return _instance;
        }
    }
    private DBUsers(){
        UserList = new List<User>();       
        try
        {
            UserList = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(Strings.userPath));
        }
        catch(Exception ex)
        {
            File.AppendAllText(Strings.errorLoggerPath, ex.ToString() + DateTime.Now);
            Console.WriteLine(Strings.error);
            return;
        }
    }

}