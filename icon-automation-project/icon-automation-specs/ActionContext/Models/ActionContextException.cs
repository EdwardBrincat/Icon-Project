namespace Icon_Automation_Libs.ActionContext.Models;

public class ActionContextException : Exception
{
    public ActionContextException()
    {
    }

    public ActionContextException(string message) : base(message)
    {
    }

    public ActionContextException(string message, Exception inner) : base(message, inner)
    {
    }
}
