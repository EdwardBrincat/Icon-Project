using Icon_Automation_Libs.WebDriver.Button;
using Icon_Automation_Libs.WebDriver.Input;
using Icon_Automation_Libs.WebDriver.Modal;
using Icon_Automation_Libs.WebDriver.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Icon_Automation_Libs.WebDriver;

public class WebElementFactory
{	
	private readonly IServiceProvider _serviceProvider;

	public WebElementFactory(
		IServiceProvider serviceProvider
	)
	{
		_serviceProvider = serviceProvider;
	}

	public WebElementQueryBuilder CreateElement() => new();

	public ButtonElementQueryBuilder CreateButtonElement(string childAttributeName, string childAttributeValue)
		=> ActivatorUtilities.CreateInstance<ButtonElementQueryBuilder>(_serviceProvider, childAttributeName, childAttributeValue);		

	public InputElementQueryBuilder CreateInputElement(string childAttributeName, string childAttributeValue)
		=> ActivatorUtilities.CreateInstance<InputElementQueryBuilder>(_serviceProvider, childAttributeName, childAttributeValue);

	public TextElementQueryBuilder CreateTextElement(string childAttributeName, string childAttributeValue)
		=> ActivatorUtilities.CreateInstance<TextElementQueryBuilder>(_serviceProvider, childAttributeName, childAttributeValue);

    public ModalElementQueryBuilder CreateModalElement(string childAttributeName, string childAttributeValue)
        => ActivatorUtilities.CreateInstance<ModalElementQueryBuilder>(_serviceProvider, childAttributeName, childAttributeValue);
}