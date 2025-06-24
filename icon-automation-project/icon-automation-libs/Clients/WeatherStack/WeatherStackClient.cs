using FluentlyHttpClient;
using Icon_Automation_Libs.Clients.User.Model;
using Icon_Automation_Libs.Clients.WeatherStack.Models;
using Icon_Automation_Libs.Http;

namespace Icon_Automation_Libs.Clients.WeatherStack;

public interface IWeatherStackClient
{
    Task<FluentHttpResponse<CurrentWeatherResponse>> GetCurrentWeather(
         CurrentWeatherRequest currentWeatherRequest,
         HttpRequestClientContext context = null
     );
}

public class WeatherStackClient : IWeatherStackClient
{
    private readonly IFluentHttpClient _httpClient;

    public WeatherStackClient(IFluentHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.GetWeatherClient();
    }


    public async Task<FluentHttpResponse<CurrentWeatherResponse>> GetCurrentWeather(
      CurrentWeatherRequest currentWeatherRequest,
      HttpRequestClientContext context = null
    )
        => await _httpClient.CreateRequest($"current?query={currentWeatherRequest.Query}&access_key={currentWeatherRequest.AccessKey}")
            .AsGet()
            .FromClientContext(context)
            .ReturnAsResponse<CurrentWeatherResponse>();
}
