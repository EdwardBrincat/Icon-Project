namespace Icon_Automation_Libs.ActionContext.Builders;

public interface IActionContextBuilder<out TActionContextType>
{
    TActionContextType Build();
}
