using Newtonsoft.Json;

public sealed class DBAdmin : DBHandler
{
    private static DBAdmin _instance = null;
    private static readonly object _lockObj = new object();
    public List<Admin> _adminList { get; set; }

    public static DBAdmin Instance
    {
        get
        {
            lock (_lockObj)
            {
                if (_instance == null)
                {
                    _instance = new DBAdmin();
                }
            }
            return _instance;
        }
    }
    private DBAdmin()
    {
        _adminList = new List<Admin>();

        try
        {
            _adminList = JsonConvert.DeserializeObject<List<Admin>>(File.ReadAllText(Strings.adminPath));
        }
        catch(Exception ex) 
        {
            File.AppendAllText(Strings.errorLoggerPath, ex.ToString() + DateTime.Now);
            Console.WriteLine(Strings.error);
            UI.StartMenu();
        }

    }

}