namespace Icon_Automation_Libs.WebDriver.Button;

public class ButtonElementQueryBuilder : WebElementQueryBuilder<ButtonComponent>
{
	public ButtonElementQueryBuilder()
	{
	}

	public ButtonElementQueryBuilder(WebElementQueryBuilder? parent = null) : base(parent)
	{
	}	

	public ButtonElementQueryBuilder(IServiceProvider serviceProvider, string childAttributeName, string childAttributeValue) : base(
		serviceProvider)
	{
		WithChildAttributeContains(childAttributeName, childAttributeValue);
	}	
}