using FluentlyHttpClient;
using Icon_Automation_Libs.Config.Model;
using Microsoft.Extensions.DependencyInjection;


namespace Icon_Automation_Libs.Clients;

public static class ClientHttpExtensions
{
    private const string ReqResIdentifier = "ReqRes Client";
    private const string WeatherStackIdentifier = "WeatherStack Client";

    public static IFluentHttpClient GetUserClient(this IFluentHttpClientFactory factory)
    => factory.Get(ReqResIdentifier);

    public static IFluentHttpClient GetWeatherClient(this IFluentHttpClientFactory factory)
   => factory.Get(WeatherStackIdentifier);

    public static IServiceProvider ConfigureUserClient(this IServiceProvider services)
    {
        var config = services.GetService<ConfigModel>();
                
        var fluentHttpAuthClientFactory = services.GetRequiredService<IFluentHttpClientFactory>();
        fluentHttpAuthClientFactory.CreateBuilder(ReqResIdentifier)
            .WithMessageHandler(new HttpClientHandler { ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true })
            .WithBaseUrl(config.ApiUrl)
            .WithUserAgent(ReqResIdentifier)
            .WithTimeout(config.Timeout)
            .UseTimer()
            .Register();

        return services;
    }

    public static IServiceProvider ConfigureWeatherstackClient(this IServiceProvider services)
    {
        var config = services.GetService<ConfigModel>();

        var fluentHttpAuthClientFactory = services.GetRequiredService<IFluentHttpClientFactory>();
        fluentHttpAuthClientFactory.CreateBuilder(WeatherStackIdentifier)
            .WithMessageHandler(new HttpClientHandler { ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true })
            .WithBaseUrl(config.WeatherStackApiUrl)
            .WithUserAgent(WeatherStackIdentifier)
            .WithTimeout(config.Timeout)
            .UseTimer()
            .Register();

        return services;
    }
}
