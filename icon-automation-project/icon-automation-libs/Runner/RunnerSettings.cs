namespace Icon_Automation_Libs.Runner;

public class RunnerSettings
{
	public  string Environment { get; set; }
	public bool IsHeadless { get; set; }
    public string OperatingSystem { get; }
    public bool BypassCatpcha { get; }
}
