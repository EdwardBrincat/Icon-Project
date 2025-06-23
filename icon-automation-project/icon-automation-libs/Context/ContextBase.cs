using Icon_Automation_Libs.Scenario;

namespace Icon_Automation_Specs.Context;
public interface IHasScenarioContext
{
    IScenarioContext ScenarioContext { get; set; }
}

public class ContextBase : IHasScenarioContext
{
    public ContextBase(IScenarioContext scenarioContext)
    {
        ScenarioContext = scenarioContext;
    }

    public IScenarioContext ScenarioContext { get; set; }
}
