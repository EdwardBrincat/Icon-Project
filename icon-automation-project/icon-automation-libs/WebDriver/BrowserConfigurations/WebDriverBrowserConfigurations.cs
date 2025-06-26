using Icon_Automation_Libs.Config.Model;
using Icon_Automation_Libs.Extensions;
using Icon_Automation_Libs.Runner;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Icon_Automation_Libs.WebDriver.BrowserConfigurations;

public class WebDriverBrowserConfigurations
{
	private readonly RunnerContext _runnerContext;
	private readonly ConfigModel _config;

	public WebDriverBrowserConfigurations(
		RunnerContext runnerContext,
        ConfigModel config
	)
	{
		_runnerContext = runnerContext;		
		_config = config;
	}

	public ChromeOptions GetChromeConfigs(
		string operatingSystem,
		ScreenOrientation screenOrientation,
		string? language = null,
		string? userAgent = null,
		bool? isJavascriptDisabled = null
	)
	{
		var chromeOptions = new ChromeOptions();

        if (_runnerContext.IsHeadless)
            chromeOptions.AddArgument("--headless=new");

        chromeOptions.PlatformName = operatingSystem;
        chromeOptions.PageLoadStrategy = PageLoadStrategy.Normal;
        chromeOptions.AddArguments("--disable-search-engine-choice-screen");
        chromeOptions.AddArgument("--no-sandbox");
        chromeOptions.AddArgument("--disable-setuid-sandbox");
        chromeOptions.AddArgument("--privileged");
        chromeOptions.AddArgument("--disable-extensions");
        chromeOptions.AddArgument("--ignore-certificate-errors");
        chromeOptions.AddArgument("--disable-popup-blocking");
        chromeOptions.AddArgument("--whitelist-ip %*");
        chromeOptions.AddArgument("--mute-audio");
        chromeOptions.AddArgument("--disable-site-isolation-trials");

        // Allow all cookies
        chromeOptions.AddUserProfilePreference("profile.default_content_setting_values.cookies", 1);
        chromeOptions.AddUserProfilePreference("profile.block_third_party_cookies", false);

        // Optional: disable automation flag to reduce bot detection
        chromeOptions.AddExcludedArgument("enable-automation");
        chromeOptions.AddAdditionalOption("useAutomationExtension", false);
        chromeOptions.AddArgument("--disable-blink-features=AutomationControlled");

        chromeOptions.AddUserProfilePreference("download.prompt_for_download", false);
        chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");

        if (isJavascriptDisabled is true)
            chromeOptions.AddUserProfilePreference("profile.managed_default_content_settings.javascript", JavascriptSettings.StrictBlock);

        if (!userAgent.IsNullOrEmpty())
            chromeOptions.AddArgument($"--user-agent={userAgent}");
        else
            chromeOptions.AddArgument("--user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) Chrome/114.0.0.0 Safari/537.36");

        if (!language.IsNullOrEmpty())
            chromeOptions.AddArgument($"--lang={language}");

        return chromeOptions;		
	}	
}