using AngleSharp.Dom;
using Icon_Automation_Libs.Exceptions;
using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace Icon_Automation_Libs.WebDriver.Selenium;

public interface IDriverClient
{
	void Dispose();
	Task Open(string url);
	Task SimulateNavigationViaUrl(string url);
	IDriverClient Click(By element);
	IDriverClient Click(IWebElement element);
    IDriverClient Click(By element, int index);
    IDriverClient ClickBlankSpace();
	string GetText(By element);
	string GetText(IWebElement element);
	IDriverClient Clear(By element);	
	IDriverClient Input(By element, string text);
	IDriverClient WaitForElementToBeInteractable(By element);
	IDriverClient WaitForElementToBeInteractable(IWebElement element);
    IDriverClient WaitForElementToBeInteractable(By element, int index);
    IDriverClient WaitForElementToNotBeInteractable(By element);
	IDriverClient WaitForElementToBeVisible(By element);	
	void RefreshPage();	
	IDriverClient Back();	
	IDriverClient WaitForPageToLoad();	
	IList<IWebElement> GetAllWebElements(By element);
	string WaitForElementTextToBeValue(By element, string expectedText);	
	string GetUrl();	
	IDriverClient WaitForElementToBeNoLongerVisible(By element);	
	IWebElement GetWebElement(By element);
	IDriverClient SwitchInstance<T>(T newDriver);
	T GetInstance<T>();		
	void ChangeWindowSize(int width, int height);	
	TestStepFailureException HandleFailureException(string message);
	IDriverClient Wait(int time);		
	bool IsElementPresent(By element, int retries);	
	bool IsElementNotPresent(By element, int retries);
	bool IsElementToNotBeInteractable(By element, int retries);
	bool IsElementToBeInteractable(By element, int retries);
	IDriverClient SendKeys(string keyCode);
	IWebElement GetParentElement(IWebElement element);	
	IList<IWebElement> GetAllChildElements(IWebElement element);
	IDriverClient ExecuteScript(string script);	
	IDriverClient Close();
	string GetAttribute(By element, string attribute);
    IDriverClient ExecuteScriptWithElement(string script, By element);
    IDriverClient ExecuteScriptWithElement(string script, IWebElement element);
}