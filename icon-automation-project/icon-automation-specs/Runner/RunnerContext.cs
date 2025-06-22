using Newtonsoft.Json;
using NUnit.Framework;
using System.Xml;

namespace Icon_Automation_Libs.Runner;

public class RunnerContext
{
    public string TestId {  get; set; }
    public string Product { get; }
    public string Env { get; }
    public bool IsHeadless { get; }

	public RunnerContext(RunnerSettings settings)
    {
        Env = settings.Environment;
		IsHeadless = settings.IsHeadless;
	}

    public RunnerContext()
    {		
		Product = TestContext.Parameters["product"];
		Env = TestContext.Parameters["env"];				
	}  	
}
