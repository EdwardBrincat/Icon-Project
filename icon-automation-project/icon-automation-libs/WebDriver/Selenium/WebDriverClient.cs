using Icon_Automation_Libs.Config.Model;
using Icon_Automation_Libs.Exceptions;
using Icon_Automation_Libs.Runner;
using LightBDD.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using Polly;
using Polly.Retry;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;


namespace Icon_Automation_Libs.WebDriver.Selenium;

public class WebDriverClient : IDisposable, IDriverClient
{
	private readonly Lazy<IWebDriver> _lazyDriver;	
	private readonly RetryPolicy _retryPolicy;
	private readonly RetryPolicy _retryPolicyNullReference;	
	private readonly RunnerContext _runnerContext;
    private readonly ConfigModel _config;
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
        _config = config;
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


	public async Task Open(string url)
	{
		try
		{
			Driver.Navigate().GoToUrl(url);
			WaitForPageToLoad();

			if (_runnerContext.BypassCatpcha)
			{
				string token = await SolveCaptcha(_config.AntiCaptacha.ApiKey, _config.AntiCaptacha.SiteKey, url);
				Console.WriteLine("Solved Token: " + token);

				IWebElement captchaTextarea = Driver.FindElement(By.Name("h-captcha-response"));

				((IJavaScriptExecutor)Driver).ExecuteScript(@"
					arguments[0].value = arguments[1];
					arguments[0].dispatchEvent(new Event('input', { bubbles: true }));
					arguments[0].dispatchEvent(new Event('change', { bubbles: true }));
				", captchaTextarea, token);

				Thread.Sleep(1000);
				string currentValue = captchaTextarea.GetAttribute("value"); 
				Console.WriteLine("Injected token is now: " + currentValue);
			}
        }
		catch (WebDriverException e)
		{			
			throw HandleFailureException($"Failed to open url[{url}] due to [{e.Message}]");
		}
	}

    private async Task<string> SolveCaptcha(string apikey, string siteKey, string url)
    {
        using var client = new HttpClient();
        
        var taskRequest = new
        {
            clientKey = apikey,
            task = new
            {
                type = "HCaptchaTaskProxyless",
                websiteURL = url,
                websiteKey = siteKey
            }
        };

        var createResponse = await client.PostAsync(
            "https://api.anti-captcha.com/createTask",
            new StringContent(JObject.FromObject(taskRequest).ToString(), Encoding.UTF8, "application/json"));

        var createResult = JObject.Parse(await createResponse.Content.ReadAsStringAsync());

        if ((int)createResult["errorId"] != 0)
            throw new Exception("Task creation error: " + createResult["errorDescription"]);

        var taskId = (int)createResult["taskId"];
        Console.WriteLine("Task ID: " + taskId);
		        
        JObject result;
        do
        {
            await Task.Delay(5000); 

            var resultRequest = new
            {
                clientKey = apikey,
                taskId = taskId
            };

            var resultResponse = await client.PostAsync(
                "https://api.anti-captcha.com/getTaskResult",
                new StringContent(JObject.FromObject(resultRequest).ToString(), Encoding.UTF8, "application/json"));

            result = JObject.Parse(await resultResponse.Content.ReadAsStringAsync());

            Console.WriteLine("Polling... Status: " + result["status"]);

        } while (result["status"].ToString() != "ready");
		        
        string token = result["solution"]["gRecaptchaResponse"].ToString();
        return token;
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
			var action = new OpenQA.Selenium.Interactions.Actions(Driver);
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

    public void InterceptCheckPasswordResponseToSetCheck()
    {
        string script = @"
			const originalFetch = window.fetch;
			window.fetch = function(input, init) {
				if (typeof input === 'string' && input.includes('/api/checkPassword')) {
					return Promise.resolve(new Response(JSON.stringify({
						status: 'good' // ← change this to whatever you want
					}), {
						status: 200,
						headers: {
							'Content-Type': 'application/json'
						}
					}));
				}
				return originalFetch(input, init);
			};
		";

		((IJavaScriptExecutor)Driver).ExecuteScript(script);

       

        // Add all cookies
        Driver.Manage().Cookies.AddCookie(new Cookie("_ga", "GA1.1.1226390038.1750658109", ".evernote.com", "/", new DateTime(2026, 7, 31, 11, 45, 6)));
        Driver.Manage().Cookies.AddCookie(new Cookie("_ga_3EXC9WZ9CH", "GS2.1.s1750938306$o9$g0$t1750938314$j52$l0$h0", ".evernote.com", "/", new DateTime(2026, 7, 31, 11, 45, 14)));
        Driver.Manage().Cookies.AddCookie(new Cookie("_gcl_au", "1.1.29558065.1750658108", ".evernote.com", "/", new DateTime(2025, 9, 21, 5, 55, 8)));
        //Driver.Manage().Cookies.AddCookie(new Cookie("auth", "S=s641:U=11f328b9:E=197accb17c8:C=197ac5d3ac8:P=5fd:A=en-web:V=2:H=6405681a76247f413ea44f79598525e2", ".www.evernote.com", "/", new DateTime(2025, 6, 26, 15, 11, 23)));
        //Driver.Manage().Cookies.AddCookie(new Cookie("clipper-sso", "S=s641:U=11f328b9:E=19f040e66ef:C=197ac5d3af1:P=1d5:A=en-chrome-clipper-xauth-new:V=2:H=51ad01359e553a6c6945659565792780", ".www.evernote.com", "/", new DateTime(2026, 6, 26, 13, 11, 23)));
        //Driver.Manage().Cookies.AddCookie(new Cookie("cookieTestValue", "1750943479895", "www.evernote.com", "/", new DateTime(2026, 7, 31, 13, 11, 23)));
        Driver.Manage().Cookies.AddCookie(new Cookie("evernote-local-storage-id", "3ad31a14-1138-4cdb-aaa2-487481366b70", "accounts.evernote.com", "/", new DateTime(2026, 6, 26, 13, 11, 22)));
        Driver.Manage().Cookies.AddCookie(new Cookie("evernote-session", "ea33d3a0c164f915d2e8412fa541ce51e7a83e7b107dc60f6765fb5ad850fa06748dcb9442df1e7dec6c8cbb53b6dbb2f50fa8b2557e4dffc24f68a399fbc170.kCFTwrbtEvH0TvsTC6qp0K6ciR6rc6ZWaglVYACpRI0", "accounts.evernote.com", "/", new DateTime(2025, 7, 26, 13, 11, 23)));
        //Driver.Manage().Cookies.AddCookie(new Cookie("JSESSIONID", "5B9840385EF4B0CC27A5D7A5E922049A", "www.evernote.com", "/", null)); // Session cookie
        //Driver.Manage().Cookies.AddCookie(new Cookie("last-web-version-used", "Ion-on-Conduit", "www.evernote.com", "/", new DateTime(2026, 7, 31, 13, 11, 25)));
        //Driver.Manage().Cookies.AddCookie(new Cookie("lastAuthentication", "1750943480573/2d8613a2f054c419fb752a14c88cbca627fc55d210a455596d1103218426a0cd", "www.evernote.com", "/", new DateTime(2026, 7, 31, 13, 11, 24)));
        //Driver.Manage().Cookies.AddCookie(new Cookie("req_sec", "U=11f328b9:P=/:E=197ac6b26d9:S=e11c32c712f8a6288e50069153a671bed86fe1ff5ef50e39e2868755020b011d", "www.evernote.com", "/", new DateTime(2025, 6, 26, 13, 26, 36)));
        Driver.Manage().Cookies.AddCookie(new Cookie("session_info", "{%22userID%22:%22Profile:USR:301148345%22%2C%22serviceLevel%22:%22PROFESSIONAL%22%2C%22segmentation%22:%22DEFAULT%22%2C%22creationDate%22:1750658238000}", ".evernote.com", "/", new DateTime(2026, 6, 26, 13, 11, 30)));
        Driver.Manage().Cookies.AddCookie(new Cookie("userdata_accountType", "FREE", ".evernote.com", "/", new DateTime(2025, 6, 26, 15, 11, 23)));
        Driver.Manage().Cookies.AddCookie(new Cookie("userdata_acctCreatedTime", "1750658238000", ".evernote.com", "/", new DateTime(2025, 6, 26, 15, 11, 23)));
        Driver.Manage().Cookies.AddCookie(new Cookie("userdata_lastLoginTime", "1750943480557", ".evernote.com", "/", new DateTime(2025, 6, 26, 15, 11, 23)));
        Driver.Manage().Cookies.AddCookie(new Cookie("userdata_loggedIn", "true", ".evernote.com", "/", new DateTime(2025, 6, 26, 15, 11, 23)));
        Driver.Manage().Cookies.AddCookie(new Cookie("web50017PreUserGuid", "20e8f263-c339-4125-97c7-9cd1fb569b73", ".evernote.com", "/", new DateTime(2026, 6, 23, 5, 58, 6)));

    }
}