using Newtonsoft.Json;

public class DBHandler
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
            File.AppendAllText(Message.errorLoggerPath, ex.ToString());
            Console.WriteLine(Message.error);
        }
    }
    
}