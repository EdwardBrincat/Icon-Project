using AutoMapper;
using Icon_Automation_Libs.Clients.User.Model;
using Icon_Automation_Libs.Contracts;

namespace Icon_Automation_Libs.Services.User;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<UserModel, UserDetails>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Avatar));

        CreateMap<SupportModel, SupportDetails>()
            .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text));

        CreateMap<UsersResponse, UsersResult>()
            .ForMember(dest => dest.Page, opt => opt.MapFrom(src => src.Page))
            .ForMember(dest => dest.TotalPages, opt => opt.MapFrom(src => src.TotalPages))
            .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize))
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total))
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data))
            .ForMember(dest => dest.Support, opt => opt.MapFrom(src => src.Support));

        CreateMap<UsersInput, UsersRequest>()
            .ForMember(dest => dest.Page, opt => opt.MapFrom(src => src.Page));
    }
}
