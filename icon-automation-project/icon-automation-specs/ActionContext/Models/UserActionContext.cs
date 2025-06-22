using FluentlyHttpClient;
using Icon_Automation_Libs.Extensions;
using Icon_Automation_Libs.Http;

namespace Icon_Automation_Libs.ActionContext.Models;

public class UserActionContext : IActionContext
{
    private FluentHttpHeaders? _headers;
    public string? XApiKey { get; set; }
    public void EnsureAuthenticated(string? claim = null, string? allowClaimValue = null)
    {
        if (XApiKey is null)
            throw new ActionContextException("User Request isn't authenticated.");
    }

    public FluentHttpHeaders Headers
    {
        get => _headers ??= GetAsHttpHeaders();
        set => _headers = value;
    }

    private FluentHttpHeaders GetAsHttpHeaders()
    {
        var headers = new FluentHttpHeaders();

        if (!XApiKey.IsNullOrEmpty())
            headers.Add(HttpHeaders.XApiKey, XApiKey);

		return headers;
    }

    public static implicit operator HttpRequestClientContext(UserActionContext action) =>
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
