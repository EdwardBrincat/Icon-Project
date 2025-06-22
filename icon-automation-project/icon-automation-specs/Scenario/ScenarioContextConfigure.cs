namespace Icon_Automation_Libs.Scenario;

public class ScenarioContextConfigure
{
    private string _feature;
    private string _scenarioIdentifier;

    public virtual ScenarioContextConfigure WithScenarioId(string id)
    {
        _scenarioIdentifier = id;
        return this;
    }

    public virtual ScenarioContextConfigure WithFeatureContext(string feature)
    {
        _feature = feature;
        return this;
    }

    public void Set(IScenarioContext scenarioContext)
    {
        scenarioContext.FeatureContext = _feature;
        scenarioContext.ScenarioIdentifier = _scenarioIdentifier;
    }
}
