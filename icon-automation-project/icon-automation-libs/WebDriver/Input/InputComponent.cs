using Icon_Automation_Libs.WebDriver.Selenium;
using OpenQA.Selenium;

namespace Icon_Automation_Libs.WebDriver.Input;

public class InputComponent : IComponent
{
	private readonly IDriverClient _driver;
	private readonly By _inputField;	

	public InputComponent(
		IDriverClient driver,
        InputElementQueryBuilder inputElementQuery
    )
	{
		_driver = driver;
		_inputField = inputElementQuery.Build();		
	}	

    public InputComponent WaitForInputToBeVisible()
    {
        _driver.WaitForElementToBeVisible(_inputField);
        return this;
    }

    public InputComponent WaitForInputNotToBeVisible()
    {
        _driver.WaitForElementToBeNoLongerVisible(_inputField);
        return this;
    }

    public InputComponent WaitToBeNotInteractable()
    {
        _driver.WaitForElementToNotBeInteractable(_inputField);
        return this;
    }

    public InputComponent WaitToBeInteractable()
    {
        _driver.WaitForElementToBeInteractable(_inputField);
        return this;
    }

    public InputComponent Clear()
	{
		_driver.Clear(_inputField);
		return this;
	}

	public InputComponent Click()
	{
		_driver.Click(_inputField);
		return this;
	}	

	public InputComponent Value(string value)
	{
		_driver.Input(_inputField, value);
		return this;
	}

    public InputComponent ValueWithScript(string value)
    {
        _driver.ExecuteScriptWithElement($"arguments[0].value = '{value}';", _inputField);
        return this;
    }

    public InputComponent SendKey(string value)
    {
        _driver.SendKeys(value);
        return this;
    }

    public InputComponent DeleteInput()
	{
		var text = _driver.WaitForElementToBeVisible(_inputField).GetAttribute(_inputField, "value");
		for (var i = 0; i < text.Length; i++)
		{
			_driver.SendKeys(Keys.Backspace);
		}

		return this;
	}

    public string GetValue() => _driver.WaitForElementToBeVisible(_inputField).GetAttribute(_inputField, "value");
}