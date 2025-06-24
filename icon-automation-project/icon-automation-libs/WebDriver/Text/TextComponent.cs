using Icon_Automation_Libs.WebDriver.Selenium;
using OpenQA.Selenium;
using System.ComponentModel;

namespace Icon_Automation_Libs.WebDriver.Text;

public class TextComponent : IComponent
{
	private readonly IDriverClient _driver;
	private readonly TextElementQueryBuilder _textElementQuery;
	private readonly By _text;

	public TextComponent(
		IDriverClient driver,
		TextElementQueryBuilder textElementQuery
	)
	{
		_driver = driver;
		_textElementQuery = textElementQuery;
		_text = textElementQuery.Build();
	}

	public TextComponent WaitToBeVisible()
	{
		_driver.WaitForElementToBeVisible(_text);
		return this;
	}

    public TextComponent WaitToBeNotVisible()
    {
        _driver.WaitForElementToBeNoLongerVisible(_text);
        return this;
    }    
		
	public TextComponent Click()
	{
		_driver.Click(_text);
		return this;
	}

	public string GetText()
		=> _driver.GetText(_text);

	public string GetInnerText()
		=> _driver.GetAttribute(_text, "innerText");	

	public string GetAttributeValue(string attributeName)
		=> _driver.GetAttribute(_text, attributeName);
		
}