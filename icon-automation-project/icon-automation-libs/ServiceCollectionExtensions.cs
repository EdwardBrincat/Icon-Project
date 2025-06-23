using Icon_Automation_Libs.Account;
using Icon_Automation_Libs.ActionContext.Factory;
using Icon_Automation_Libs.Clients.User;
using Icon_Automation_Libs.Config.Model;
using Icon_Automation_Libs.DependencyInjection;
using Icon_Automation_Libs.Fixtures.Api;
using Icon_Automation_Libs.Scenario;
using Icon_Automation_Libs.Services.User;
using Icon_Automation_Specs.Context;
using Microsoft.Extensions.DependencyInjection;

namespace Icon_Automation_Libs;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAutomationCore(this IServiceCollection services)
    {
        services.AddSingleton<InstanceCreator>();
        services.AddSingleton<IActionContextFactory, ActionContextFactory>();
        services.AddScoped<IScenarioContext, ScenarioContext>();
        return services;
    }

    public static IServiceCollection AddConfig(this IServiceCollection services, ConfigModel config)
    {
        services.AddSingleton(config);

        return services;
    }

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

    public static IServiceCollection AddApiFixtures(this IServiceCollection services)
    {
        services.AddScoped<UserApiFixutres>();

        return services;
    }

    public static IServiceCollection AddApiContext(this IServiceCollection services)
    {
        services.AddApiFixtures();
        services.AddScoped<ApiContext>();

        return services;
    }
}
