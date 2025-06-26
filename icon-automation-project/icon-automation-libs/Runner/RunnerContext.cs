using Icon_Automation_Libs.Extensions;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Xml;

namespace Icon_Automation_Libs.Runner;

public class RunnerContext
{
    public string TestId {  get; set; }
    public string Env { get; }
    public string OperatingSystem { get; }
    public bool IsHeadless { get; }
    public bool BypassCatpcha { get; }

    public RunnerContext(RunnerSettings settings)
    {
        Env = settings.Environment;
		IsHeadless = settings.IsHeadless;
        OperatingSystem = settings.OperatingSystem;
        BypassCatpcha = settings.BypassCatpcha;

    }

    public RunnerContext()
    {	
		Env = TestContext.Parameters["env"];
        OperatingSystem = TestContext.Parameters["os"];
        IsHeadless = TestContext.Parameters["isHeadless"] != null ? TestContext.Parameters["isHeadless"].ToBool() : default;
        BypassCatpcha = TestContext.Parameters["bypassCaptacha"] != null ? TestContext.Parameters["bypassCaptacha"].ToBool() : default;
    }  	
}
