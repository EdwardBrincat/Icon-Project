using Icon_Automation_Libs.ActionContext.Models;

namespace Icon_Automation_Libs.ActionContext.Builders;

public class UserActionContextBuilder : IActionContextBuilder<UserActionContext>
{
	private string _xApiKey;

    public UserActionContext Build() =>
        new()
        {
            XApiKey = _xApiKey
		};

    public virtual UserActionContextBuilder WithActionContext(UserActionContext context)
    {
        _xApiKey = context.XApiKey;
		return this;
    }

    public virtual UserActionContextBuilder WithXApiKey(string authToken)
    {
        _xApiKey = authToken;
        return this;
    }          

    public UserActionContextBuilder When(
        Func<bool> condition,
        Action<UserActionContextBuilder> action
    )
    {
        if (condition.Invoke()) action.Invoke(this);
        return this;
    }
}
