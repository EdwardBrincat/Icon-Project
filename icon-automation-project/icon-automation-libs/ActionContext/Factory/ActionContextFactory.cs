using Icon_Automation_Libs.ActionContext.Builders;
using Icon_Automation_Libs.DependencyInjection;

namespace Icon_Automation_Libs.ActionContext.Factory;

public interface IActionContextFactory
{
    UserActionContextBuilder CreateUserActionContext();
    WeatherStackActionContextBuilder CreateWeatherStackActionContextBuilder();
}

public class ActionContextFactory : IActionContextFactory
{
    private readonly InstanceCreator _instanceCreator;

    public ActionContextFactory(InstanceCreator instanceCreator)
    {
        _instanceCreator = instanceCreator;
    }

    public UserActionContextBuilder CreateUserActionContext() => _instanceCreator.Create<UserActionContextBuilder>();

    public WeatherStackActionContextBuilder CreateWeatherStackActionContextBuilder() => _instanceCreator.Create<WeatherStackActionContextBuilder>();
}
