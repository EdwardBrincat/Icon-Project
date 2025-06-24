using System.Runtime.Serialization;

namespace Icon_Automation_Libs.WebDriver;
public class BrowserConst
{
    public const string Chrome = "chrome";
	public const string FireFox = "fireFox";
	public const string Safari = "safari";
	public const string Edge = "edge";
	public const string BrowserStack = "browserStack";
}

public enum BrowserType
{
	[EnumMember(Value = "chrome")]
	Chrome,

	[EnumMember(Value = "edge")]
	Edge,

	[EnumMember(Value = "firefox")]
	FireFox,

	[EnumMember(Value = "safari")]
	Safari
}


public enum JavascriptSettings
{
	[EnumMember(Value = "allow")]
	Allow = 0,

	[EnumMember(Value = "block")]
	Block = 1,

	[EnumMember(Value = "strictblock")]
	StrictBlock = 2
}

