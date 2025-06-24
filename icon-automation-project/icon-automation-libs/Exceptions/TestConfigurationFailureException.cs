namespace Icon_Automation_Libs.Exceptions;

public class TestConfigurationFailureException : Exception
{
	public TestConfigurationFailureException()
	{
	}

	public TestConfigurationFailureException(string message) : base(message)
	{
	}

	public TestConfigurationFailureException(string message, Exception inner) : base(message, inner)
	{
	}
}
