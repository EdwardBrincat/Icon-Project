using FluentlyHttpClient;

namespace Icon_Automation_Libs.Http;

public class HttpRequestClientContext
{
    public FluentHttpHeaders Headers { get; set; }
    public Dictionary<string, object> Items { get; set; }
}
