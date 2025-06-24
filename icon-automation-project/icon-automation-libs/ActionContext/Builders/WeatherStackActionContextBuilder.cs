using Icon_Automation_Libs.ActionContext.Models;

namespace Icon_Automation_Libs.ActionContext.Builders;

public class WeatherStackActionContextBuilder : IActionContextBuilder<WeatherStackActionContext>
{
    private string _xApiKey;

    public WeatherStackActionContext Build() =>
        new()
        {
            XApiKey = _xApiKey
        };

    public virtual WeatherStackActionContextBuilder WithActionContext(WeatherStackActionContext context)
    {
        _xApiKey = context.XApiKey;
        return this;
    }

    public virtual WeatherStackActionContextBuilder WithXApiKey(string authToken)
    {
        _xApiKey = authToken;
        return this;
    }

    public WeatherStackActionContextBuilder When(
        Func<bool> condition,
        Action<WeatherStackActionContextBuilder> action
    )
    {
        if (condition.Invoke()) action.Invoke(this);
        return this;
    }
}
