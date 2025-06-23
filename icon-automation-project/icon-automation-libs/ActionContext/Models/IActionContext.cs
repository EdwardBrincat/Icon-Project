using FluentlyHttpClient;

namespace Icon_Automation_Libs.ActionContext.Models;

public interface IActionContext
{
    void EnsureAuthenticated(string? claim = null, string? allowClaimValue = null);        
    FluentHttpHeaders? Headers { get; }
    string? XApiKey { get; }
}
