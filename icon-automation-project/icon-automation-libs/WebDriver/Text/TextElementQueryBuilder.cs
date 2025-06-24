namespace Icon_Automation_Libs.WebDriver.Text;

public class TextElementQueryBuilder : WebElementQueryBuilder<TextComponent>
{
	public TextElementQueryBuilder()
	{
	}

	public TextElementQueryBuilder(WebElementQueryBuilder parent = null) : base(parent)
	{
	}	

	public TextElementQueryBuilder(IServiceProvider serviceProvider, string childAttributeName, string childAttributeValue) : base(serviceProvider)
	{
		WithChildAttributeContains(childAttributeName, childAttributeValue);
	}	
}