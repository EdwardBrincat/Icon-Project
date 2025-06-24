namespace Icon_Automation_Libs.WebDriver.Modal;

public class ModalElementQueryBuilder : WebElementQueryBuilder<ModalComponent>
{
	public ModalElementQueryBuilder()
	{
	}

	public ModalElementQueryBuilder(WebElementQueryBuilder parent = null) : base(parent)
	{
	}

	public ModalElementQueryBuilder(IServiceProvider serviceProvider, string attributeName, string attributeValue) : base(serviceProvider)
	{
		WithAttributeContains(attributeName, attributeValue);
	}
}