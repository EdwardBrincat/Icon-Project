using Icon_Automation_Libs.Config;
using Icon_Automation_Libs.Config.Model;
using Icon_Automation_Libs.Extensions;
using Icon_Automation_Libs.Runner;
using Icon_Automation_Libs.Utils;
using LightBDD.Core.Configuration;
using LightBDD.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Icon_Automation_Libs.DependencyInjection;

public class FixtureStartup
{
    private Action<IServiceCollection> _containerConfigureServiceCollections;
    private Action<IServiceProvider> _containerConfigureServiceProviders;
    private string _testId = string.Empty;
    public RunnerContext RunnerContext { get; }
    public IServiceProvider Services { get; private set; }
    public ConfigModel Config { get; private set; }

    public FixtureStartup()
    {
        RunnerContext = new RunnerContext();
		RunnerContext.TestId = $"Automation-Test-Run-{DateTime.UtcNow.GetEpochTimeStamp()}-{RandomUtils.GenerateString(5).ToLower()}";
	}        
    
    public void Configure(LightBddConfiguration lightBddConfig)
    {
        Config = ConfigFetcher.GetConfiguration();
		_testId = RunnerContext.TestId;

		Services = BuildContainer();

		
        lightBddConfig.DependencyContainerConfiguration()
            .UseContainer(Services, true);

        lightBddConfig.ExecutionExtensionsConfiguration();
    }

    public void ConfigureServiceCollections(Action<IServiceCollection> configure)
        => _containerConfigureServiceCollections = configure;

    public void ConfigureServiceProviders(Action<IServiceProvider> configure)
        => _containerConfigureServiceProviders = configure;

    private IServiceProvider BuildContainer()
    {
        var services = new ServiceCollection();		
		services.AddSingleton(RunnerContext);
        services.AddAutomationCore();
        services.AddConfig(Config);

        _containerConfigureServiceCollections?.Invoke(services);

        var serviceProvider = services
            .BuildServiceProvider();

        _containerConfigureServiceProviders?.Invoke(serviceProvider);

        ServiceResolver.SetServiceProvider(serviceProvider);

        return serviceProvider;
    }	
}
