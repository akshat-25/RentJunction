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
        catch
        {
            Console.WriteLine(Message.error);
        }
    }
    
}