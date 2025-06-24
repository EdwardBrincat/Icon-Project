using Icon_Automation_Libs.Account;
using Icon_Automation_Libs.ActionContext.Factory;
using Icon_Automation_Libs.Clients.User;
using Icon_Automation_Libs.Clients.WeatherStack;
using Icon_Automation_Libs.Command;
using Icon_Automation_Libs.Config.Model;
using Icon_Automation_Libs.DependencyInjection;
using Icon_Automation_Libs.Fixtures.Api;
using Icon_Automation_Libs.Fixtures.UI;
using Icon_Automation_Libs.PageObjects.Login;
using Icon_Automation_Libs.PageObjects.Navigation;
using Icon_Automation_Libs.Scenario;
using Icon_Automation_Libs.Services.User;
using Icon_Automation_Libs.Services.WeatherStack;
using Icon_Automation_Libs.WebDriver;
using Icon_Automation_Libs.WebDriver.BrowserConfigurations;
using Icon_Automation_Libs.WebDriver.Selenium;
using Icon_Automation_Specs.Context;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;

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
        services.AddScoped<UserApiFixture>();
        services.AddScoped<WeatherStackApiFixture>();

        return services;
    }

    public static IServiceCollection AddContext(this IServiceCollection services)
    {
        services.AddApiFixtures();
        services.AddUiFixtures();
        services.AddScoped<ApiContext>();
        services.AddScoped<UiContext>();

        return services;
    }

    public static IServiceCollection AddWebDriverClient(this IServiceCollection services, ScreenOrientation screenOrientation)
    {
        services.AddScoped<IDriverClient, WebDriverClient>(); ;
        services.AddScoped(p => p.GetRequiredService<WebDriverClientFactory>().Create(ScreenOrientation.Portrait));       
        services.AddScoped<WebDriverBrowserConfigurations>();
        services.AddScoped<WebDriverClientFactory>();
        services.AddScoped<WebDriver.WebElementFactory>();

        return services;
    }

    public static IServiceCollection AddPageObjects(this IServiceCollection services)
    {
        services.AddScoped<LoginPage>();
        services.AddScoped<NavigationPage>();

        return services;
    }

    public static IServiceCollection AddUiFixtures(this IServiceCollection services)
    {
        services.AddScoped<LoginUiFixture>();
        services.AddScoped<NavigationUiFixture>();
        return services;
    }

    public static IServiceCollection AddWeatherStackClientCollection(this IServiceCollection services)
    {
        services.AddScoped<IWeatherStackClient, WeatherStackClient>();
        return services;
    }

    public static IServiceCollection AddWeatherStackServiceCollection(this IServiceCollection services)
    {
        services.AddScoped<IWeatherStackCurrentService, WeatherStackCurrentService>();
        return services;
    }

    public static IServiceCollection AddWeatherStackCommandsCollection(this IServiceCollection services)
    {
        services.AddSingleton<WeatherStackCommandFactory>();
        return services;
    }
}
