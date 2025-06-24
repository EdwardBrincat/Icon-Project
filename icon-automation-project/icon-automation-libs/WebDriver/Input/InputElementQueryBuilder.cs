namespace Icon_Automation_Libs.WebDriver.Input;

public class InputElementQueryBuilder : WebElementQueryBuilder<InputComponent>
{
	public InputElementQueryBuilder()
	{
	}

	public InputElementQueryBuilder(WebElementQueryBuilder parent = null) : base(parent)
	{
	}	

	public InputElementQueryBuilder(IServiceProvider serviceProvider, string childAttributeName, string childAttributeValue) : base(
		serviceProvider)
	{
		WithChildAttributeContains(childAttributeName, childAttributeValue);
	}	
}