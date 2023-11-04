using Newtonsoft.Json;
using RentJunction.Controller;

public sealed class DBCustomer : DBHandler
{
    private static DBCustomer _instance = null;
    private static readonly object _lockObj = new object();
    public List<Customer> _customerList { get; set; }
    public static DBCustomer Instance
    {
        get
        {
            lock (_lockObj)
            {
                if (_instance == null)
                {
                    _instance = new DBCustomer();
                }
            }
            return _instance;
        }
    }
    private DBCustomer(){
        _customerList = new List<Customer>();
        
        try
        {
            _customerList = JsonConvert.DeserializeObject<List<Customer>>(File.ReadAllText(Message.customerPath));
        }
        catch
        {
            Console.WriteLine(Message.error);
            UI.StartMenu();
        }
    

    }

}