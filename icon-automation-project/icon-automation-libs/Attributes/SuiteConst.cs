using System.Runtime.Serialization;

namespace Icon_Automation_Libs.Attributes;

public static class SuiteConst
{
    public const string API = "api";
    public const string UI = "ui";
    public const string All = "all";
}

public enum SuiteType
{
    [EnumMember(Value = "api")]
    API,
    [EnumMember(Value = "ui")]
    UI,
    [EnumMember(Value = "all")]
    All
}
