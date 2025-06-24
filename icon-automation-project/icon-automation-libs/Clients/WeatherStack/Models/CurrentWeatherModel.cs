using Icon_Automation_Libs.Clients.WeatherStack.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icon_Automation_Libs.Clients.WeatherStack.Models;

public class Request
{
    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("query")]
    public string Query { get; set; }

    [JsonProperty("language")]
    public string Language { get; set; }

    [JsonProperty("unit")]
    public string Unit { get; set; }

}
public class Location
{

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("country")]
    public string Country { get; set; }

    [JsonProperty("region")]
    public string Region { get; set; }

    [JsonProperty("lat")]
    public string Lat { get; set; }

    [JsonProperty("lon")]
    public string Lon { get; set; }

    [JsonProperty("timezone_id")]
    public string TimezoneId { get; set; }

    [JsonProperty("localtime")]
    public DateTime Localtime { get; set; }

    [JsonProperty("localtime_epoch")]
    public int LocaltimeEpoch { get; set; }

    [JsonProperty("utc_offset")]
    public string UtcOffset { get; set; }

}
public class Astro
{

    [JsonProperty("sunrise")]
    public string Sunrise { get; set; }

    [JsonProperty("sunset")]
    public string Sunset { get; set; }

    [JsonProperty("moonrise")]
    public string Moonrise { get; set; }

    [JsonProperty("moonset")]
    public string Moonset { get; set; }

    [JsonProperty("moon_phase")]
    public string MoonPhase { get; set; }

    [JsonProperty("moon_illumination")]
    public int MoonIllumination { get; set; }

}
public class AirQuality
{

    [JsonProperty("co")]
    public string Co { get; set; }

    [JsonProperty("no2")]
    public string No2 { get; set; }

    [JsonProperty("o3")]
    public string O3 { get; set; }

    [JsonProperty("so2")]
    public string So2 { get; set; }

    [JsonProperty("pm2_5")]
    public string Pm2_5 { get; set; }

    [JsonProperty("Pm10")]
    public string Pm10 { get; set; }

    [JsonProperty("us-epa-index")]
    public string UsEpaIndex { get; set; }

    [JsonProperty("gb-defra-index")]
    public string GbDefraIndex { get; set; }
}

public class CurrentWeatherModel
{

    [JsonProperty("observation_time")]
    public string ObservationTime { get; set; }

    [JsonProperty("temperature")]
    public int Temperature { get; set; }

    [JsonProperty("weather_code")]
    public int WeatherCode { get; set; }

    [JsonProperty("weather_icons")]
    public IList<string> WeatherIcons { get; set; }

    [JsonProperty("weather_descriptions")]
    public IList<string> WeatherDescriptions { get; set; }

    [JsonProperty("astro")]
    public Astro Astro { get; set; }

    [JsonProperty("air_quality")]
    public AirQuality AirQuality { get; set; }

    [JsonProperty("wind_speed")]
    public int WindSpeed { get; set; }

    [JsonProperty("wind_degree")]
    public int WindDegree { get; set; }

    [JsonProperty("wind_dir")]
    public string WindDir { get; set; }

    [JsonProperty("pressure")]
    public int Pressure { get; set; }

    [JsonProperty("precip")]
    public float Precip { get; set; }

    [JsonProperty("humidity")]
    public int Humidity { get; set; }

    [JsonProperty("cloudcover")]
    public int Cloudcover { get; set; }

    [JsonProperty("feelslike")]
    public int Feelslike { get; set; }

    [JsonProperty("uv_index")]
    public int UvIndex { get; set; }

    [JsonProperty("visibility")]
    public int Visibility { get; set; }

    [JsonProperty("is_day")]
    public string IsDay { get; set; }

}
public class CurrentWeatherResponse
{

    [JsonProperty("request")]
    public Request Request { get; set; }

    [JsonProperty("location")]
    public Location Location { get; set; }

    [JsonProperty("current")]
    public CurrentWeatherModel Current { get; set; }

}


public class CurrentWeatherRequest
{   
    public string Query { get; set; }
    public string AccessKey { get; set; }
}
