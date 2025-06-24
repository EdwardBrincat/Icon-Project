using AutoMapper;
using Icon_Automation_Libs.Clients.User.Model;
using Icon_Automation_Libs.Clients.WeatherStack.Models;
using Icon_Automation_Libs.Contracts;
using AirQualityModel = Icon_Automation_Libs.Clients.WeatherStack.Models.AirQuality;
using AirQualityContract = Icon_Automation_Libs.Contracts.AirQuality;
using AstroModel = Icon_Automation_Libs.Clients.WeatherStack.Models.Astro;
using AstroContract = Icon_Automation_Libs.Contracts.Astro;
using LocationModel = Icon_Automation_Libs.Clients.WeatherStack.Models.Location;
using LocationContract = Icon_Automation_Libs.Contracts.Location;
using RequestModel = Icon_Automation_Libs.Clients.WeatherStack.Models.Request;
using RequestContract = Icon_Automation_Libs.Contracts.Request;

namespace Icon_Automation_Libs.Services.WeatherStack;

public class WeatherStackCurrentMappingProfile : Profile
{
    public WeatherStackCurrentMappingProfile()
    {
        CreateMap<CurrentWeatherModel, CurrentWeatherDetails>()
            .ForMember(dest => dest.ObservationTime, opt => opt.MapFrom(src => src.ObservationTime))
            .ForMember(dest => dest.Temperature, opt => opt.MapFrom(src => src.Temperature))
            .ForMember(dest => dest.WeatherCode, opt => opt.MapFrom(src => src.WeatherCode))
            .ForMember(dest => dest.WeatherIcons, opt => opt.MapFrom(src => src.WeatherIcons))
            .ForMember(dest => dest.WeatherDescriptions, opt => opt.MapFrom(src => src.WeatherDescriptions))
            .ForMember(dest => dest.Astro, opt => opt.MapFrom(src => src.Astro))
            .ForMember(dest => dest.AirQuality, opt => opt.MapFrom(src => src.AirQuality))
            .ForMember(dest => dest.WindSpeed, opt => opt.MapFrom(src => src.WindSpeed))
            .ForMember(dest => dest.WindDegree, opt => opt.MapFrom(src => src.WindDegree))
            .ForMember(dest => dest.WindDir, opt => opt.MapFrom(src => src.WindDir))
            .ForMember(dest => dest.Pressure, opt => opt.MapFrom(src => src.Pressure))
            .ForMember(dest => dest.Precip, opt => opt.MapFrom(src => src.Precip))
            .ForMember(dest => dest.Humidity, opt => opt.MapFrom(src => src.Humidity))
            .ForMember(dest => dest.Cloudcover, opt => opt.MapFrom(src => src.Cloudcover))
            .ForMember(dest => dest.Feelslike, opt => opt.MapFrom(src => src.Feelslike))
            .ForMember(dest => dest.UvIndex, opt => opt.MapFrom(src => src.UvIndex))
            .ForMember(dest => dest.Visibility, opt => opt.MapFrom(src => src.Visibility))
            .ForMember(dest => dest.IsDay, opt => opt.MapFrom(src => src.IsDay))
            .ForMember(dest => dest.WeatherDescriptions, opt => opt.MapFrom(src => src.WeatherDescriptions));

        CreateMap<RequestModel, RequestContract>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Query, opt => opt.MapFrom(src => src.Query))
            .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Language))
            .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit));

        CreateMap<LocationModel, LocationContract>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
            .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.Region))
            .ForMember(dest => dest.Lat, opt => opt.MapFrom(src => src.Lat))
            .ForMember(dest => dest.Lon, opt => opt.MapFrom(src => src.Lon))
            .ForMember(dest => dest.TimezoneId, opt => opt.MapFrom(src => src.TimezoneId))
            .ForMember(dest => dest.Localtime, opt => opt.MapFrom(src => src.Localtime))
            .ForMember(dest => dest.LocaltimeEpoch, opt => opt.MapFrom(src => src.LocaltimeEpoch))
            .ForMember(dest => dest.UtcOffset, opt => opt.MapFrom(src => src.UtcOffset));

        CreateMap<AstroModel, AstroContract>()
            .ForMember(dest => dest.Sunrise, opt => opt.MapFrom(src => src.Sunrise))
            .ForMember(dest => dest.Sunset, opt => opt.MapFrom(src => src.Sunset))
            .ForMember(dest => dest.Moonrise, opt => opt.MapFrom(src => src.Moonrise))
            .ForMember(dest => dest.Moonset, opt => opt.MapFrom(src => src.Moonset))
            .ForMember(dest => dest.MoonPhase, opt => opt.MapFrom(src => src.MoonPhase))
            .ForMember(dest => dest.MoonIllumination, opt => opt.MapFrom(src => src.MoonIllumination));

        CreateMap<AirQualityModel, AirQualityContract>()
            .ForMember(dest => dest.Co, opt => opt.MapFrom(src => src.Co))
            .ForMember(dest => dest.No2, opt => opt.MapFrom(src => src.No2))
            .ForMember(dest => dest.O3, opt => opt.MapFrom(src => src.O3))
            .ForMember(dest => dest.So2, opt => opt.MapFrom(src => src.So2))
            .ForMember(dest => dest.Pm2_5, opt => opt.MapFrom(src => src.Pm2_5))
            .ForMember(dest => dest.Pm10, opt => opt.MapFrom(src => src.Pm10))
            .ForMember(dest => dest.UsEpaIndex, opt => opt.MapFrom(src => src.UsEpaIndex))
            .ForMember(dest => dest.GbDefraIndex, opt => opt.MapFrom(src => src.GbDefraIndex));

        CreateMap<CurrentWeatherResponse, CurrentWeatherResult>()
            .ForMember(dest => dest.Request, opt => opt.MapFrom(src => src.Request))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
            .ForMember(dest => dest.Current, opt => opt.MapFrom(src => src.Current));

        CreateMap<CurrentWeatherInput, CurrentWeatherRequest>()
            .ForMember(dest => dest.Query, opt => opt.MapFrom(src => src.Query))
            .ForMember(dest => dest.AccessKey, opt => opt.MapFrom(src => src.AccessKey));
    }
}
