using Newtonsoft.Json;
using RentJunction.Controller;

public sealed class DBOwner : DBHandler
{
    private static DBOwner _instance = null;
    private static readonly object _lockObj = new object();
    public List<Owner> _ownerList { get; set; }
    public static DBOwner Instance
    {
        get
        {
            lock (_lockObj)
            {
                if (_instance == null)
                {
                    _instance = new DBOwner();
                }
            }
            return _instance;
        }
    }
    private DBOwner()
    {
        _ownerList = new List<Owner>();

        try
        {
            _ownerList = JsonConvert.DeserializeObject<List<Owner>>(File.ReadAllText(Message.ownerPath));
        }
        catch(Exception ex)
        {
            File.AppendAllText(Message.errorLoggerPath, ex.ToString());
            Console.WriteLine(Message.error);
            UI.StartMenu();
        }

    }

}