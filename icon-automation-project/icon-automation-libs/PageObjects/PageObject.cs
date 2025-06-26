using Icon_Automation_Libs.Config.Model;
using Icon_Automation_Libs.Exceptions;
using Icon_Automation_Libs.WebDriver;
using Icon_Automation_Libs.WebDriver.Selenium;
using Polly;
using Polly.Retry;
using System.Collections.ObjectModel;

namespace Icon_Automation_Libs.PageObjects;

public interface IPageObject<out TPageObject>
{
	Task Open(string baseUrl);	
	void ChangeWindowSize(int width, int height);	
}

public class PageObject<TPageObject> : IPageObject<TPageObject>
	where TPageObject : IPageObject<TPageObject>
{
	private readonly TPageObject _this;

	protected readonly RetryPolicy ConfigRetryPolicy;
	protected readonly IDriverClient Driver;
	protected readonly WebElementFactory Factory;

	protected PageObject(
		IDriverClient driver,
		WebElementFactory factory,
		ConfigModel config
	)
	{
		Factory = factory;
		Driver = driver;
		_this = (TPageObject)(object)this;

		ConfigRetryPolicy = Policy
			.Handle<TestConfigurationFailureException>()
			.WaitAndRetry(config.Retries, x => TimeSpan.FromSeconds(config.Timeout));		
	}

	public async Task Open(string baseUrl)
	{
		await Driver.Open(baseUrl);
	}
	
	public TPageObject Do(Action<TPageObject> action)
	{
		action(_this);
		return _this;
	}

	public string GetUrl() => Driver.GetUrl();	

	public TPageObject WaitForPageToLoad()
	{
		Driver.WaitForPageToLoad();
		return _this;
	}

	public TPageObject FailTest(string message)
	{
		Driver.HandleFailureException(message);
		return _this;
	}

	public void ChangeWindowSize(int width, int height)
		=> Driver.ChangeWindowSize(width, height);
	
	public string GetSystemUrl()
		=> Uri.UnescapeDataString(Driver.GetUrl());	
}
