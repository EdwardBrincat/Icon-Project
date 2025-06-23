using LightBDD.Core.Execution;
using LightBDD.Core.Extensibility.Execution;
using LightBDD.NUnit3;

namespace Icon_Automation_Libs.Attributes;

[AttributeUsage(AttributeTargets.All)]
public class IconScenarioAttribute : ScenarioAttribute, IScenarioDecoratorAttribute
{
    private readonly string _id;
    private readonly string _featureName;
    private readonly List<string>? _tags;

    public IconScenarioAttribute(string id, string featureName)
    {
        _id = id;
        _featureName = featureName;        
    }

    public int Order { get; set; }

    public async Task ExecuteAsync(IScenario scenario, Func<Task> scenarioInvocation)
    {	
		await scenarioInvocation();		
    }
}
