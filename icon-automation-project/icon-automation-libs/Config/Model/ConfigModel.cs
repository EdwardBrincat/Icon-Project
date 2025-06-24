using Icon_Automation_Libs.Contracts;

namespace Icon_Automation_Libs.Config.Model;

public class ConfigModel
{
    public string ApiUrl { get; set; }
    public string UiBaseUrl { get; set; }
    public string WeatherStackApiUrl { get; set; }
    public int Timeout { get; set; }

    public int Retries { get; set; }
    public string apiKey { get; set; } 
    public string WeatherStackAccessKey { get; set; }
    public Dictionary<string, UserTestData> TestDataApi { get; set; }
    public Dictionary<string, EverNoteTestData> TestDataUi { get; set; }
    public List<string> Cities { get; set; }
}

public class UserTestData
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }
    public int TotalPages { get; set; }
    public UserDetails User { get; set; }
    public SupportDetails Support { get; set; }
}

public class EverNoteTestData
{
    public string Email { get; set; }
    public string CorrectPassword { get; set; }
    public string InCorrectPassword { get; set; }
    public string ErrorMessage { get; set; }
}

