using Newtonsoft.Json;

public class DBHandler : IDBHandler
{
    public void UpdateDB<T>(string path, List<T> list)
    {
        try
        {
            var jsonFile = JsonConvert.SerializeObject(list);
            File.WriteAllText(path, jsonFile);
        }
        catch(Exception ex)
        {
            File.AppendAllText(Strings.errorLoggerPath, ex.ToString() + DateTime.Now);
            Console.WriteLine(Strings.error);
        }
    }
    
}