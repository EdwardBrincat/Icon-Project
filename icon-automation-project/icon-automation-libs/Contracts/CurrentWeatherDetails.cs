using System.Net;

namespace Icon_Automation_Libs.Contracts;

public class Request
{
    public string Type { get; set; }
    public string Query { get; set; }
    public string Language { get; set; }
    public string Unit { get; set; }

}
public class Location
{
    public string Name { get; set; }
    public string Country { get; set; }
    public string Region { get; set; }
    public string Lat { get; set; }
    public string Lon { get; set; }
    public string TimezoneId { get; set; }
    public DateTime Localtime { get; set; }
    public int LocaltimeEpoch { get; set; }
    public string UtcOffset { get; set; }

}

public class Astro
{
    public string Sunrise { get; set; }
    public string Sunset { get; set; }
    public string Moonrise { get; set; }
    public string Moonset { get; set; }
    public string MoonPhase { get; set; }
    public int MoonIllumination { get; set; }

}

public class AirQuality
{
    public string Co { get; set; }
    public string No2 { get; set; }
    public string O3 { get; set; }
    public string So2 { get; set; }
    public string Pm2_5 { get; set; }
    public string Pm10 { get; set; }
    public string UsEpaIndex { get; set; }
    public string GbDefraIndex { get; set; }
}

public class CurrentWeatherDetails
{
    public string ObservationTime { get; set; }
    public int Temperature { get; set; }
    public int WeatherCode { get; set; }
    public IList<string> WeatherIcons { get; set; }
    public IList<string> WeatherDescriptions { get; set; }
    public Astro Astro { get; set; }
    public AirQuality AirQuality { get; set; }
    public int WindSpeed { get; set; }
    public int WindDegree { get; set; }
    public string WindDir { get; set; }
    public int Pressure { get; set; }
    public float Precip { get; set; }
    public int Humidity { get; set; }
    public int Cloudcover { get; set; }
    public int Feelslike { get; set; }
    public int UvIndex { get; set; }
    public int Visibility { get; set; }
    public string IsDay { get; set; }

}
public class CurrentWeatherResult
{
    public HttpStatusCode ResponseCode { get; set; }
    public Request Request { get; set; }
    public Location Location { get; set; }
    public CurrentWeatherDetails Current { get; set; }

}

public class CurrentWeatherInput
{
    public string Query { get; set; }
    public string AccessKey { get; set; }
}

public class WeatherCodes
{
    public int Code { get; set; }
    public string Description { get; set; }
    public string DayIcon { get; set; }
    public string NightIcon { get; set; }
}

