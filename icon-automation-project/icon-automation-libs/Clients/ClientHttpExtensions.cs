using FluentlyHttpClient;
using Icon_Automation_Libs.Config.Model;
using Microsoft.Extensions.DependencyInjection;


namespace Icon_Automation_Libs.Clients;

public static class ClientHttpExtensions
{
    private const string Identifier = "ReqRes Client";
    
    public static IFluentHttpClient GetUserClient(this IFluentHttpClientFactory factory)
    => factory.Get(Identifier);
        
    public static IServiceProvider ConfigureUserClient(this IServiceProvider services)
    {
        var config = services.GetService<ConfigModel>();
                
        var fluentHttpAuthClientFactory = services.GetRequiredService<IFluentHttpClientFactory>();
        fluentHttpAuthClientFactory.CreateBuilder(Identifier)
            .WithMessageHandler(new HttpClientHandler { ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true })
            .WithBaseUrl(config.Url)
            .WithUserAgent(Identifier)
            .WithTimeout(config.Timeout)
            .UseTimer()
            .Register();

        return services;
    }    
}
