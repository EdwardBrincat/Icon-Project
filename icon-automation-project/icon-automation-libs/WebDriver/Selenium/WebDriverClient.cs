using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using Icon_Automation_Libs.Config.Model;
using Icon_Automation_Libs.Exceptions;
using Icon_Automation_Libs.Runner;
using LightBDD.Framework;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using Polly;
using Polly.Retry;
using Sketch7.Core;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Text.RegularExpressions;
using SeleniumCookie = OpenQA.Selenium.Cookie;

namespace Icon_Automation_Libs.WebDriver.Selenium;

public class WebDriverClient : IDisposable, IDriverClient
{
	private readonly Lazy<IWebDriver> _lazyDriver;	
	private readonly RetryPolicy _retryPolicy;
	private readonly RetryPolicy _retryPolicyNullReference;	
	private readonly RunnerContext _runnerContext;
	private readonly WebElementFactory _factory;


	public WebDriverClient(
		Lazy<IWebDriver> driver,
		ConfigModel config,
		RunnerContext runnerContext,
		WebElementFactory factory
	)
	{
		_lazyDriver = driver;
		
		
		_runnerContext = runnerContext;
		_factory = factory;

		_retryPolicy = Policy
			.Handle<WebDriverException>()
			.WaitAndRetry(config.Retries,
				x => TimeSpan.FromSeconds(config.Timeout));

		_retryPolicyNullReference = Policy
			.Handle<NullReferenceException>()
			.WaitAndRetry(config.Retries,
				x => TimeSpan.FromSeconds(config.Timeout));

		Driver = _lazyDriver.Value;
	}

	private IWebDriver Driver { get; set; }

	public bool ScenarioPassed { get; set; } = false;

	public string Scenario { get; set; }

	public void Dispose()
	{
		if (_lazyDriver.IsValueCreated)
			Driver?.Dispose();
	}

	public IDriverClient SwitchInstance<T>(T newDriver)
	{
		Driver = (IWebDriver)newDriver;
		return this;
	}

	public T GetInstance<T>() => (T)Driver;


	public Task Open(string url)
	{
		try
		{
            Driver.Navigate().GoToUrl(url);
			WaitForPageToLoad();
            return Task.CompletedTask;
		}
		catch (WebDriverException e)
		{			
			throw HandleFailureException($"Failed to open url[{url}] due to [{e.Message}]");
		}
	}

	public Task SimulateNavigationViaUrl(string url)
	{
		try
		{
			var js = Driver as IJavaScriptExecutor;
			js.ExecuteScript($"window.history.pushState(null, '', '{url}');");
			js.ExecuteScript("window.dispatchEvent(new Event('popstate'));");
			return Task.CompletedTask;
		}
		catch (WebDriverException e)
		{		
			throw HandleFailureException($"Failed to navigate to url via component[{url}] due to [{e.Message}]");
		}
	}	

	public string GetUrl()
	{
		try
		{			
			return Driver.Url;
		}
		catch (WebDriverException e)
		{		
			throw HandleFailureException($"Failed to retrieve url due to [{e.Message}]");
		}
	}	

	public IDriverClient Click(By element)
	{
		try
		{
			Driver.FindElement(element).Click();
			return this;
		}
		catch (WebDriverException e)
		{			
			throw HandleFailureException($"Failed to click[{element}] due to [{e.Message}]");
		}
	}

	public IDriverClient Click(IWebElement element)
	{
		try
		{
			element.Click();		
			return this;
		}
		catch (WebDriverException e)
		{			
			throw HandleFailureException($"Failed to click[{element}] due to [{e.Message}]");
		}
	}

    public IDriverClient Click(By element, int index)
    {
        try
        {
            var elements = Driver.FindElements(element);
            elements.ElementAt(index).Click();
            return this;
        }
        catch (WebDriverException e)
        {
            throw HandleFailureException($"Failed to click[{element}] due to [{e.Message}]");
        }
    }

    public IDriverClient SendKeys(string keyCode)
	{
		try
		{
			var action = new Actions(Driver);
			action.SendKeys(keyCode).Perform();	
			return this;
		}
		catch (Exception e)
		{			
			throw HandleFailureException($"Failed to press {keyCode} due to [{e.Message}]");
		}
	}

	public IDriverClient ClickBlankSpace()
	{
		try
		{		
			Driver.FindElement(By.XPath("//html")).Click();
			return this;
		}
		catch (Exception e)
		{		
			throw HandleFailureException($"Failed to click blank space on page due to [{e.Message}]");
		}
	}

	public string GetText(By element)
	{
		try
		{
			var text = string.Empty;
			_retryPolicy.Execute(() =>
			{		
				text = Driver.FindElement(element).Text;
				return text;
			});
			return text;
		}
		catch (WebDriverException e)
		{			
			throw HandleFailureException($"Failed to get text from element[{element}] due to [{e.Message}]");
		}
	}

	public string GetText(IWebElement element)
	{
		try
		{
			var text = string.Empty;
			_retryPolicy.Execute(() =>
			{		
				text = element.Text;
				return text;
			});
			return text;
		}
		catch (WebDriverException e)
		{			
			throw HandleFailureException($"Failed to get text from element[{element}] due to [{e.Message}]");
		}
	}

	public string WaitForElementTextToBeValue(By element, string expectedText)
	{
		try
		{
			var text = string.Empty;
			_retryPolicy.Execute(() =>
			{
				text = GetText(element);
				
				if (text.Equals(expectedText))
					return text;
				throw new WebDriverException(
					$"Element[{element}] does not contain expected text [{expectedText}].");
			});

			return text;
		}
		catch (WebDriverException e)
		{			
			throw HandleFailureException($"Failed to get text and verify text for element[{element}] due to [{e.Message}]");
		}
	}

	public string WaitForElementTextToBeValue(IWebElement element, string expectedText)
	{
		try
		{
			var text = string.Empty;
			_retryPolicy.Execute(() =>
			{
				text = GetText(element);
		
				if (text.Equals(expectedText))
					return text;
				throw new WebDriverException(
					$"Element[{element}] does not contain expected text [{expectedText}].");
			});

			return text;
		}
		catch (WebDriverException e)
		{			
			throw HandleFailureException($"Failed to get text and verify text for element[{element}] due to [{e.Message}]");
		}
	}

	public IDriverClient Clear(By element)
	{
		try
		{
			Driver.FindElement(element).Clear();
			
			return this;
		}
		catch (WebDriverException e)
		{			
			throw HandleFailureException($"Failed to clear[{element}] due to [{e.Message}]");
		}
	}		

	public IDriverClient Input(By element, string text)
	{
		try
		{
			Driver.FindElement(element).SendKeys(text);		
			return this;
		}
		catch (WebDriverException e)
		{			
			throw HandleFailureException($"Failed to Input [{text}] into [{element}] due to [{e.Message}]");
		}
	}	

	public IDriverClient Wait(int time)
	{
		try
		{
			Task.Delay(time).Wait();
	
			return this;
		}
		catch (Exception e)
		{
			throw HandleFailureException($"Wait [{time}] milliseconds was not successful due to [{e.Message}]");
		}
	}

	public IDriverClient WaitForElementToBeInteractable(By element)
	{
		try
		{
			_retryPolicy.Execute(() =>
			{
				if (!Driver.FindElement(element).Displayed || !Driver.FindElement(element).Enabled)
					throw new WebDriverException($"Element [{element}] is not in an interactable state.");
			});
	
			return this;
		}
		catch (WebDriverException e)
		{
			throw HandleFailureException($"[{element}] was not interactable due to [{e.Message}]");
		}
	}

    public IDriverClient WaitForElementToBeInteractable(By element, int index)
    {
        try
        {
            _retryPolicy.Execute(() =>
            {
                var elements = Driver.FindElements(element);                

                if (elements.ElementAt(index).Displayed || elements.ElementAt(index).Enabled)
                    throw new WebDriverException($"Element [{element}] is not in an interactable state.");
            });

            return this;
        }
        catch (WebDriverException e)
        {
            throw HandleFailureException($"[{element}] was not interactable due to [{e.Message}]");
        }
    }

    public IDriverClient WaitForElementToBeInteractable(IWebElement element)
	{
		try
		{
			_retryPolicy.Execute(() =>
			{
				if (!element.Displayed || !element.Enabled)
					throw new WebDriverException($"Element [{element}] is not in an interactable state.");
			});
		
			return this;
		}
		catch (WebDriverException e)
		{
			throw HandleFailureException($"[{element}] was not interactable due to [{e.Message}]");
		}
	}

	public bool IsElementToBeInteractable(By element, int retries)
	{
		try
		{			
			Policy.Handle<WebDriverException>()
				.WaitAndRetry(retries, x => TimeSpan.FromSeconds(2))
				.Execute(() =>
				{
					if (!Driver.FindElement(element).Displayed || !Driver.FindElement(element).Enabled)
						throw new WebDriverException($"Element [{element}] is not in an interactable state.");
				});
		
			return true;
		}
		catch (Exception e)
		{
			return false;
		}
	}

	public IDriverClient WaitForElementToNotBeInteractable(By element)
	{
		try
		{
			_retryPolicy.Execute(() =>
			{
				if (Driver.FindElement(element).Displayed && Driver.FindElement(element).Enabled)
					throw new WebDriverException($"Element [{element}] is still interactable.");
			});
		
			return this;
		}
		catch (WebDriverException e)
		{		
			throw HandleFailureException($"[{element}] was still interactable due to [{e.Message}]");
		}
	}

	public bool IsElementToNotBeInteractable(By element, int retries)
	{
		try
		{
			Policy.Handle<WebDriverException>()
				.WaitAndRetry(retries, x => TimeSpan.FromSeconds(2))
				.Execute(() =>
				{
					if (Driver.FindElement(element).Displayed && Driver.FindElement(element).Enabled)
						throw new WebDriverException($"Element [{element}] was still interactable.");
				});

			return true;
		}
		catch (Exception e)
		{
			return false;
		}
	}

	public IDriverClient WaitForElementToBeVisible(By element)
	{
		try
		{
			_retryPolicy.Execute(() =>
			{
				if (!Driver.FindElement(element).Displayed)
					throw new WebDriverException($"Element [{element}] was not visible.");
			});
		
			return this;
		}
		catch (WebDriverException e)
		{		
			throw HandleFailureException($"[{element}] was not visible due to [{e.Message}]");
		}
	}
	

	public IDriverClient WaitForElementToBeNoLongerVisible(By element)
	{
		try
		{
			_retryPolicy.Execute(() =>
			{
				try
				{
					if (Driver.FindElement(element).Displayed)
						throw new WebDriverException($"Element [{element}] was visible.");
				}
				catch (NoSuchElementException)
				{
				}
			});

			return this;
		}
		catch (WebDriverException e)
		{
			throw HandleFailureException($"[{element}] was still visible due to [{e.Message}]");
		}
	}	

	public void RefreshPage()
	{
		var js = Driver as IJavaScriptExecutor;
		js.ExecuteScript("location.reload();", null);		
	}
	
	public IDriverClient Close()
	{
		try
		{		
			Driver.Close();
			return this;
		}
		catch (Exception e)
		{		
			return this;
		}
	}

	public IDriverClient ExecuteScript(string script)
	{
		try
		{		
			Driver.ExecuteJavaScript(script);
			return this;
		}
		catch (Exception e)
		{
			throw HandleFailureException($"Failed to execute script [{script}] due to [{e.Message}]");
		}
	}	

	public bool IsElementPresent(By element, int retries)
	{
		try
		{	
			Policy.Handle<WebDriverException>()
				.WaitAndRetry(retries, x => TimeSpan.FromSeconds(2))
				.Execute(() =>
				{
					if (!Driver.FindElement(element).Displayed)
						throw new WebDriverException($"Element [{element}] was not present.");
				});
		
			return true;
		}
		catch (Exception e)
		{		
			return false;
		}
	}

	public bool IsElementNotPresent(By element, int retries)
	{
		try
		{		
			Policy.Handle<WebDriverException>()
				.WaitAndRetry(retries, x => TimeSpan.FromSeconds(1))
				.Execute(() =>
				{
					try
					{
						if (Driver.FindElement(element).Displayed)
							throw new WebDriverException($"Element [{element}] was present.");
					}
					catch (NoSuchElementException)
					{						
					}
				});			
			return true;
		}
		catch (Exception e)
		{			
			return false;
		}
	}

	public IDriverClient Back()
	{
		try
		{			
			Driver.Navigate().Back();
			return this;
		}
		catch (Exception e)
		{		
			throw HandleFailureException($"Navigation failed due to [{e.Message}]");
		}
	}	

	public IWebElement GetWebElement(By element)
	{
		try
		{	
			return Driver.FindElement(element);
		}
		catch (Exception e)
		{		
			throw HandleFailureException($"Failed Converting By element [{element}] to IWebElement due to [{e.Message}].");
		}
	}

	public IList<IWebElement> GetAllWebElements(By element)
	{
		try
		{		
			return Driver.FindElements(element);
		}
		catch (Exception e)
		{		
			throw HandleFailureException($"Failed getting all web elements of type [{element}] as IWebElement due to [{e.Message}].");
		}
	}
	
	public IWebElement GetParentElement(IWebElement element)
	{
		try
		{
			var js = Driver as IJavaScriptExecutor;
			
			return (IWebElement)js.ExecuteScript("return arguments[0].parentNode;", element);
		}
		catch (Exception e)
		{			
			throw HandleFailureException($"Failed getting parent element for [{element}] due to [{e.Message}]");
		}
	}

	public IWebElement GetChildElement(IWebElement element)
	{
		try
		{
			var js = Driver as IJavaScriptExecutor;
			
			return (IWebElement)js.ExecuteScript("return arguments[0].firstElementChild;", element);
		}
		catch (Exception e)
		{			
			throw HandleFailureException($"Failed getting child element for [{element}] due to [{e.Message}]");
		}
	}

	public IList<IWebElement> GetAllChildElements(IWebElement element)
	{
		try
		{
			var js = Driver as IJavaScriptExecutor;		
			return (IList<IWebElement>)js.ExecuteScript("return arguments[0].children;", element);
		}
		catch (Exception e)
		{			
			throw HandleFailureException($"Failed getting all child elements for [{element}] due to [{e.Message}]");
		}
	}

	public TestStepFailureException HandleFailureException(string message)
	{
		var msg = Regex.Replace(message, @"For documentation on this error, please visit: http[^\s]+", "]");

		var browserLogs = Driver.Manage().Logs?.GetLog("browser");
		if (browserLogs != null && browserLogs.Count > 0)
		{
			var log = $"Browser Logs [{string.Join("\n", browserLogs)}";
			StepExecution.Current.Comment(log);			
		}
		
		Driver.Dispose();
		throw new TestStepFailureException(msg);
	}	

	public IDriverClient WaitForPageToLoad()
	{
		try
		{
			var js = Driver as IJavaScriptExecutor;
	
			_retryPolicy.Execute(() =>
			{
				var pageReadyState = (string)js.ExecuteScript("return document.readyState");

				if (!pageReadyState.Equals("complete"))
					throw new WebDriverException("Page still loading.....");
			});           

            return this;
		}
		catch (Exception e)
		{	
			throw HandleFailureException($"Timeout elapsed while waiting for page to load due to [{e.Message}].");
		}
	}	

	public void ChangeWindowSize(int width, int height)
	{
		try
		{
			_retryPolicy.Execute(() =>
				{
					Driver.Manage().Window.Size = new Size(width, height);
					var newSize = Driver.Manage().Window.Size;
					if (_runnerContext.IsHeadless && !newSize.Width.Equals(width))
						throw new WebDriverException($"Error while resizing window to w:{width} h:{height}.");
				});
		}
		catch (Exception e)
		{
			throw HandleFailureException($"Failed to set window size to width: {width} and height: {height} due to [{e.Message}].");
		}
	}

    public string GetAttribute(By element, string attribute)
    {
        try
        {
            var value = string.Empty;
            _retryPolicy.Execute(() =>
            {     
                value = Driver.FindElement(element).GetAttribute(attribute);             
            });
            return value;
        }
        catch (Exception e)
        {
            throw HandleFailureException($"Failed to get attribute [{attribute}] from element [${element}] due to [{e.Message}]");
        }
    }

    public IDriverClient ExecuteScriptWithElement(string script, By element)
    {
        ExecuteScriptWithElement(script, Driver.FindElement(element));
        return this;
    }

    public IDriverClient ExecuteScriptWithElement(string script, IWebElement element)
    {
        try
        {
            Driver.ExecuteJavaScript(script, element);
            return this;
        }
        catch (Exception e)
        {
            throw HandleFailureException($"Failed to execute script [{script}] for element [{element}] due to [{e.Message}]");
        }
    }
}