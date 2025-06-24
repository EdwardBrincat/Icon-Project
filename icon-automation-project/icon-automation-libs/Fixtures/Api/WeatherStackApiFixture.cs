using AutoFixture;
using Elastic.Clients.Elasticsearch.Xpack;
using FluentAssertions;
using Icon_Automation_Libs.ActionContext.Factory;
using Icon_Automation_Libs.Clients.WeatherStack.Models;
using Icon_Automation_Libs.Command;
using Icon_Automation_Libs.Config.Model;
using Icon_Automation_Libs.Contracts;
using Icon_Automation_Libs.Scenario;
using Icon_Automation_Libs.Utils;
using System.Globalization;
using System.Net;

namespace Icon_Automation_Libs.Fixtures.Api;

public class WeatherStackApiFixture : FixtureBase
{
    private readonly IActionContextFactory _actionContextFactory;
    private readonly WeatherStackCommandFactory _weatherStackCommandFactory;
    private readonly ConfigModel _config;

    public WeatherStackApiFixture(
        ConfigModel config,
        IScenarioContext scenarioContext,
        IActionContextFactory actionContextFactory,
        WeatherStackCommandFactory weatherStackCommandFactory
    ) : base(config, scenarioContext)
    {
        _config = config;
        _actionContextFactory = actionContextFactory;
        _weatherStackCommandFactory = weatherStackCommandFactory;
    }

    public async Task A_get_weather_stack_current_weather_action_is_requested(string cityCountry)
    {
        var action = _actionContextFactory.CreateWeatherStackActionContextBuilder()
            .Build();

        var result = await _weatherStackCommandFactory.ExecuteGetCurrentWeatherCommand(new Fixture().BuildData<CurrentWeatherInput>()
            .With(p => p.Query, cityCountry)
            .With(p => p.AccessKey, _config.WeatherStackAccessKey)
            .Create(),
            action);

        ScenarioContext.AddOrUpdateValue($"{WeatherStackKeys.WeatherStackResult}", result);
    }

    public void The_api_response_status_is_verified()
    {
        var result = ScenarioContext.GetValue<CurrentWeatherResult>($"{WeatherStackKeys.WeatherStackResult}");
        result.Should().NotBeNull();
        result.ResponseCode.Should().Be(HttpStatusCode.OK);
    }

    public void The_current_weather_response_is_verified(string cityCountry)
    {
        var result = ScenarioContext.GetValue<CurrentWeatherResult>($"{WeatherStackKeys.WeatherStackResult}");
        var weatherCodes = WeatherCodesService.LoadWeatherCodes();
        var city = cityCountry.Split(", ")[0];

        result.Location.Should().NotBeNull();
        result.Location.Name.ToLower().Should().Be(city.ToLower());


        result.Current.Should().NotBeNull();

        var weatherCode = weatherCodes.FirstOrDefault(code => code.Code == result.Current.WeatherCode);

        result.Current.WeatherCode.Should().Be(weatherCode.Code);                   
        result.Current.Humidity.Should().BeGreaterThanOrEqualTo(0);
        result.Current.Temperature.Should().BeInRange(-100, +100);
    }

    public void The_last_weather_observation_time_never_exceeds_24h_is_verified()
    {
        var result = ScenarioContext.GetValue<CurrentWeatherResult>($"{WeatherStackKeys.WeatherStackResult}");
                
        string localDatePart = result.Location.Localtime.ToString().Split(' ')[0];
        string timePart = result.Current.ObservationTime;

        
        string fullDateTimeString = $"{localDatePart} {timePart}";


        DateTime observationDateTime = DateTime.ParseExact(
            fullDateTimeString,
            "dd/MM/yyyy hh:mm tt", // Corrected format
            CultureInfo.InvariantCulture
        );

        TimeZoneInfo regionTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"); 
        DateTime currentLocalTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, regionTimeZone);
                
        TimeSpan timeDifference = currentLocalTime - observationDateTime;

        timeDifference.Should().BeLessThanOrEqualTo(TimeSpan.FromHours(24));
    }


}

