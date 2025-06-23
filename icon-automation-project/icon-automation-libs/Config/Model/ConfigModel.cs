using Icon_Automation_Libs.Contracts;

namespace Icon_Automation_Libs.Config.Model;

public class ConfigModel
{
    public string Url { get; set; }
    public int Timeout { get; set; }

    public int Retries { get; set; }
    public string apiKey { get; set; } 
    public Dictionary<string, UserTestData> TestData { get; set; }
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

