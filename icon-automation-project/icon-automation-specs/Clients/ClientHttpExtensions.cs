using FluentlyHttpClient;
using Icon_Automation_Libs.Models;


namespace Sc_Casino_Client;

public static class ClientHttpExtensions
{
    private const string Identifier = "ReqRes Client";
    
    public static IFluentHttpClient GetUserClient(this IFluentHttpClientFactory factory)
    => factory.Get(Identifier);

    /// <summary>
    /// Configure User client.
    /// </summary>
    /// <param name="httpClientFactory">Factory to configure on.</param>
    /// <param name="config">Config data.</param>
    public static void ConfigureUserClient(this IFluentHttpClientFactory httpClientFactory, ConfigModel config)
    {
        var httpClientBuilder = httpClientFactory.CreateBuilder(Identifier)
            .WithBaseUrl(config.Url)
            .WithUserAgent(Identifier)    
            .WithTimeout(config.Timeout)
            .UseTimer()					
            .UseLogging();
                    
        httpClientBuilder.Register();
    }    
}
