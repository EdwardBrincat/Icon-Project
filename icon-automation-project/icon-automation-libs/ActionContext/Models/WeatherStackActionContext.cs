using FluentlyHttpClient;
using Icon_Automation_Libs.Http;

namespace Icon_Automation_Libs.ActionContext.Models;

public class WeatherStackActionContext : IActionContext
{
    private FluentHttpHeaders? _headers;
    public string? XApiKey { get; set; }
    public void EnsureAuthenticated(string? claim = null, string? allowClaimValue = null)
    {

    }

    public FluentHttpHeaders Headers
    {
        get => _headers ??= GetAsHttpHeaders();
        set => _headers = value;
    }

    private FluentHttpHeaders GetAsHttpHeaders()
    {
        var headers = new FluentHttpHeaders();

        return headers;
    }

    public static implicit operator HttpRequestClientContext(WeatherStackActionContext action) =>
    new HttpRequestClientContext
    {
        Items = new Dictionary<string, object> { { "ActionContextKey", action } },
        Headers = new FluentHttpHeaders(action.Headers)
    };

    public HttpRequestClientContext ToHttpRequestClientContext()
        => new HttpRequestClientContext
        {
            Items = new Dictionary<string, object> { { "ActionContextKey", this } },
            Headers = new FluentHttpHeaders(this.Headers)
        };
}
