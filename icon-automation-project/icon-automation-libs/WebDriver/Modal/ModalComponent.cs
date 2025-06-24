using Icon_Automation_Libs.WebDriver.Selenium;
using OpenQA.Selenium;

namespace Icon_Automation_Libs.WebDriver.Modal;

public class ModalComponent : IComponent
{
	private readonly By _close;
	private readonly IDriverClient _driver;
	private readonly By _modal;	

	public ModalComponent(
		IDriverClient driver,
		ModalElementQueryBuilder modalElementBuilder
	)
	{
		_driver = driver;
		_modal = modalElementBuilder.Build();
	}

	public string GetSelector()
		=> _modal.Criteria;

	public ModalComponent WaitToBeVisible()
	{
		_driver.WaitForElementToBeVisible(_modal);
		return this;
	}

	public ModalComponent WaitToBeVisible(int v)
	{
		_driver.WaitForElementToBeVisible(_modal);
		return this;
	}

	public ModalComponent WaitNotTobBeVisible()
	{
		_driver.WaitForElementToBeNoLongerVisible(_modal);
		return this;
	}

	public ModalComponent Close()
	{
		_driver.WaitForElementToBeInteractable(_close).Click(_close);
		return this;
	}

	public bool IsPresent(int retries)
		=> _driver.IsElementPresent(_modal, retries);
}