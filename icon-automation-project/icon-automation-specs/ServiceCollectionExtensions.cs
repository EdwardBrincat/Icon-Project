using Icon_Automation_Libs.Account;
using Icon_Automation_Libs.Clients;
using Icon_Automation_Libs.Services.User;
using Microsoft.Extensions.DependencyInjection;

namespace Icon_Automation_Libs;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUserClientCollection(this IServiceCollection services)
    {
        services.AddScoped<IUserClient, UserClient>();
		return services;
    }

    public static IServiceCollection AddUserServiceCollection(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        return services;
    }

    public static IServiceCollection AddUserCommandsCollection(this IServiceCollection services)
    {
        services.AddSingleton<UserCommandFactory>();
        return services;
    }
}
