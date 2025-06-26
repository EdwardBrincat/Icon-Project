namespace Icon_Automation_Libs.WebDriver.PlaceHolder;

public class PlaceHolderElementQueryBuilder : WebElementQueryBuilder<PlaceHolderComponent>
{
    public PlaceHolderElementQueryBuilder()
    {
    }

    public PlaceHolderElementQueryBuilder(WebElementQueryBuilder parent = null) : base(parent)
    {
    }

    public PlaceHolderElementQueryBuilder(IServiceProvider serviceProvider, string attributeName, string attributeValue) : base(serviceProvider)
    {
        WithAttributeContains(attributeName, attributeValue);
    }
}