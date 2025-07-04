﻿using AutoMapper;
using Icon_Automation_Libs;
using Icon_Automation_Libs.Clients;
using Icon_Automation_Libs.DependencyInjection;
using Icon_Automation_Specs;
using LightBDD.Core.Configuration;
using LightBDD.NUnit3;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using OpenQA.Selenium;

[assembly: Parallelizable(ParallelScope.Fixtures)]
[assembly: ConfiguredLightBddScope]

namespace Icon_Automation_Specs;

internal class ConfiguredLightBddScopeAttribute : LightBddScopeAttribute
{
    private readonly FixtureStartup _startup;

    public ConfiguredLightBddScopeAttribute()
    {
        _startup = new FixtureStartup();
    }

    protected override void OnConfigure(LightBddConfiguration configuration)
    {
        var mapperConfig = new MapperConfiguration(cfg => cfg
            .AddMappingProfiles());

        _startup.ConfigureServiceCollections(services => services
            .AddContext()
            .AddFluentlyHttpClient()
            .AddUserClientCollection()
            .AddWeatherStackClientCollection()
            .AddUserServiceCollection()
            .AddWeatherStackServiceCollection()
            .AddUserCommandsCollection()
            .AddWeatherStackCommandsCollection()
            .AddPageObjects()
            .AddUiFixtures()
            .AddWebDriverClient(ScreenOrientation.Landscape)
            .AddSingleton(provider => mapperConfig.CreateMapper(provider.GetService)));

        _startup.ConfigureServiceProviders(provider => provider
            .ConfigureUserClient()
            .ConfigureWeatherstackClient());

        _startup.Configure(configuration);
    }
}
