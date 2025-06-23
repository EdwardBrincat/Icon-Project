using Elastic.Transport.Extensions;
using Icon_Automation_Libs.DependencyInjection;
using Icon_Automation_Libs.Runner;
using LightBDD.Core.Execution;
using LightBDD.Core.Extensibility.Execution;


namespace Icon_Automation_Libs.Attributes;

[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class SuiteAttribute : Attribute, IScenarioDecoratorAttribute
{
    private readonly string _filter;
    private readonly RunnerContext _runnerContext;

    public SuiteAttribute(SuiteType filter)
    {
        _filter = filter.ToString();
        _runnerContext = ServiceResolver.ResolveStatic<RunnerContext>();
    }

    public int Order { get; set; }

    public async Task ExecuteAsync(IScenario scenario, Func<Task> scenarioInvocation)
    {       
        await scenarioInvocation();
    }
}
