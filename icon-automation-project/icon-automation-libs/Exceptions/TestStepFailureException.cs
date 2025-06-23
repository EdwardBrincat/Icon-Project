namespace Icon_Automation_Libs.Exceptions;

public class TestStepFailureException : Exception
{
    public TestStepFailureException()
    {
    }

    public TestStepFailureException(string message) : base(message)
    {
    }

    public TestStepFailureException(string message, Exception inner) : base(message, inner)
    {
    }
}
