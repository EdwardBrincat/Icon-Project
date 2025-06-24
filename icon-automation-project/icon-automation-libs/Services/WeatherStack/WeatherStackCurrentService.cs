using AutoMapper;
using Icon_Automation_Libs.ActionContext.Models;
using Icon_Automation_Libs.Clients.WeatherStack;
using Icon_Automation_Libs.Clients.WeatherStack.Models;
using Icon_Automation_Libs.Contracts;

namespace Icon_Automation_Libs.Services.WeatherStack;

public interface IWeatherStackCurrentService
{
    Task<CurrentWeatherResult> GetCurrentWeather(
         CurrentWeatherInput currentWeatherInput,
         WeatherStackActionContext context = null
     );
}

public class WeatherStackCurrentService : IWeatherStackCurrentService
{
    private readonly IWeatherStackClient _weatherStackClient;
    private readonly IMapper _mapper;

    public WeatherStackCurrentService(IWeatherStackClient weatherStackClient, IMapper mapper)
    {
        _weatherStackClient = weatherStackClient;
        _mapper = mapper;
    }

    public async Task<CurrentWeatherResult> GetCurrentWeather(CurrentWeatherInput currentWeatherInput, WeatherStackActionContext context = null)
    {
        var request = _mapper.Map<CurrentWeatherRequest>(currentWeatherInput);
        var response = await _weatherStackClient.GetCurrentWeather(request, context);
        var result = _mapper.Map<CurrentWeatherResult>(response.Data);
        result.ResponseCode = response.StatusCode;
        return result;
    }
}

