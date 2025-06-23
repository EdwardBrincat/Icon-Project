using Icon_Automation_Libs.Config.Model;
using Icon_Automation_Libs.Runner;
using Newtonsoft.Json;

namespace Icon_Automation_Libs.Config;

public static class ConfigFetcher
{

    private static ConfigModel _config;


    public static ConfigModel GetConfiguration()
    {
        if (_config != null)
            return _config;

        string filePath = "config.json"; 

        if (!File.Exists(filePath))
        {
            Console.WriteLine("Config file not found.");
            return null;
        }

        var json = File.ReadAllText(filePath);

        return JsonConvert.DeserializeObject<ConfigModel>(json);
    }
}

