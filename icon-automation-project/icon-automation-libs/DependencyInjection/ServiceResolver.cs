using Microsoft.Extensions.DependencyInjection;

namespace Icon_Automation_Libs.DependencyInjection;

public static class ServiceResolver
{
    private static IServiceProvider _serviceProvider;

    public static void SetServiceProvider(IServiceProvider provider) => _serviceProvider = provider;

    public static void TearDownServiceProvider(bool scenarioPassed = true)
    {		
		// todo: throw exception if provider is null
		if (_serviceProvider is IDisposable disposable)
            disposable.Dispose();

        _serviceProvider = null;
    }

    public static T ResolveStatic<T>() => _serviceProvider.GetService<T>();
}
