using AutoMapper;
using Icon_Automation_Libs.Services.User;
using Icon_Automation_Libs.Services.WeatherStack;

namespace Icon_Automation_Libs;

public static class MapperProfileExtensions
{
    public static IMapperConfigurationExpression AddMappingProfiles(this IMapperConfigurationExpression mapper)
    {
        mapper.AddProfile<UserMappingProfile>();
        mapper.AddProfile<WeatherStackCurrentMappingProfile>();

        return mapper;
    }
}
