using Microsoft.Extensions.DependencyInjection;

namespace Icon_Automation_Libs.DependencyInjection;

public class InstanceCreator
{
    private readonly IServiceProvider _serviceProvider;

    public InstanceCreator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public T Create<T>(params object[] args)
        => ActivatorUtilities.CreateInstance<T>(_serviceProvider, args);
}
