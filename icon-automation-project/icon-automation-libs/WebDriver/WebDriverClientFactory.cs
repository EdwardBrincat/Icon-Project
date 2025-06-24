using Icon_Automation_Libs.Config.Model;
using Icon_Automation_Libs.Runner;
using Icon_Automation_Libs.WebDriver.BrowserConfigurations;
using Icon_Automation_Libs.WebDriver.Selenium;
using LightBDD.Framework;
using LightBDD.Framework.Scenarios;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System.Drawing;
using System.Text.Json;

namespace Icon_Automation_Libs.WebDriver;

public class WebDriverClientFactory
{
	private readonly WebDriverBrowserConfigurations _webDriverBrowserConfigs;	
	private readonly RunnerContext _runnerContext;
	private readonly IServiceProvider _serviceProvider;
	private IWebDriver _driver = null!;	
	private readonly ConfigModel _config;

	public WebDriverClientFactory(
		IServiceProvider serviceProvider,
		RunnerContext runnerContext,
		WebDriverBrowserConfigurations webDriverBrowserConfigs,
        ConfigModel config
	)
	{
		_serviceProvider = serviceProvider;
		_runnerContext = runnerContext;
		_webDriverBrowserConfigs = webDriverBrowserConfigs;
		_config = config;
	}

	public IDriverClient Create(ScreenOrientation screenOrientation)
	{
		var attempts = 1;			

		var lazyDriver = new Lazy<IWebDriver>(() => InitSelenium());
		return ActivatorUtilities.CreateInstance<WebDriverClient>(_serviceProvider, lazyDriver);

		IWebDriver InitSelenium()
		{
			try
			{
                var service = ChromeDriverService.CreateDefaultService("chromedriver.exe");
                
                _driver = new ChromeDriver(service, _webDriverBrowserConfigs.GetChromeConfigs(_runnerContext.OperatingSystem, screenOrientation));                

				_driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
				_driver.Manage().Window.Size = new Size(1920, 1080);

				return _driver;
			}
			catch (WebDriverException ex)
			{
				if (attempts == 3)
				{					
					StepExecution.Current.IgnoreScenario($"Failed to start web driver instance due to: {ex}");
				}
				attempts++;
				return InitSelenium();
			}
		}		
	}	
}