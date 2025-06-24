using Icon_Automation_Libs.WebDriver.Selenium;
using OpenQA.Selenium;

namespace Icon_Automation_Libs.WebDriver.Button;

public class ButtonComponent : IComponent
{
	private readonly By _button;
	private readonly IDriverClient _driver;

	public ButtonComponent(
		IDriverClient driver,
		ButtonElementQueryBuilder buttonElementQuery
	)
	{
		_driver = driver;
		_button = buttonElementQuery.Build();		
	}

	public string GetSelector()
		=> _button.Criteria;    

    public ButtonComponent WaitToBeVisible()
    {
        _driver.WaitForElementToBeVisible(_button);
        return this;
    }

    public ButtonComponent WaitNotToBeVisible()
    {
        _driver.WaitForElementToBeNoLongerVisible(_button);
        return this;
    }

    public ButtonComponent WaitToBeInteractable()
	{
		_driver.WaitForElementToBeInteractable(_button);
		return this;
	}

    public ButtonComponent WaitToBeInteractable(int index)
    {
        _driver.WaitForElementToBeInteractable(_button, index);
        return this;
    }

    public ButtonComponent WaitToNotBeInteractable()
	{
		_driver.WaitForElementToNotBeInteractable(_button);
		return this;
	}

	public ButtonComponent Click()
	{
		_driver.Click(_button);
		return this;
	}

    public ButtonComponent Click(int index)
    {
        _driver.Click(_button, index);
        return this;
    }

    public string GetText()
		=> _driver.GetAttribute(_button, "innerText");	
}