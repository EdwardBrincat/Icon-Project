using Icon_Automation_Libs.ActionContext.Models;
using Icon_Automation_Libs.Contracts;
using Icon_Automation_Libs.Services.User;
using Icon_Automation_Libs.Services.WeatherStack;

namespace Icon_Automation_Libs.Command;

public class WeatherStackCommandFactory
{
    private readonly IWeatherStackCurrentService _service;

    public WeatherStackCommandFactory(
        IWeatherStackCurrentService service
    )
    {
        _service = service;
    }

    public async Task<CurrentWeatherResult> ExecuteGetCurrentWeatherCommand(CurrentWeatherInput currentWeatherInput, WeatherStackActionContext context = null)
        => await _service.GetCurrentWeather(currentWeatherInput, context);
}
