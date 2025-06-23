using AutoMapper;
using Icon_Automation_Libs.Services.User;

namespace Icon_Automation_Libs;

public static class MapperProfileExtensions
{
    public static IMapperConfigurationExpression AddMappingProfiles(this IMapperConfigurationExpression mapper)
    {
        mapper.AddProfile<UserMappingProfile>();

		return mapper;
    }
}
