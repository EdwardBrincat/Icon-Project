using Icon_Automation_Libs.WebDriver.Modal;
using Icon_Automation_Libs.WebDriver.Selenium;
using OpenQA.Selenium;

namespace Icon_Automation_Libs.WebDriver.PlaceHolder;

public class PlaceHolderComponent : IComponent
{    
    private readonly IDriverClient _driver;
    private readonly By _placeHolder;

    public PlaceHolderComponent(
        IDriverClient driver,
        PlaceHolderElementQueryBuilder placeHolderElementQueryBuilder
    )
    {
        _driver = driver;
        _placeHolder = placeHolderElementQueryBuilder.Build();
    }

    public string GetSelector()
        => _placeHolder.Criteria;

    public PlaceHolderComponent WaitToBeVisible()
    {
        _driver.WaitForElementToBeVisible(_placeHolder);
        return this;
    }

    public PlaceHolderComponent WaitToBeVisible(int v)
    {
        _driver.WaitForElementToBeVisible(_placeHolder);
        return this;
    }

    public PlaceHolderComponent WaitNotTobBeVisible()
    {
        _driver.WaitForElementToBeNoLongerVisible(_placeHolder);
        return this;
    }

    public PlaceHolderComponent Click()
    {
        _driver.Click(_placeHolder);
        return this;
    }
}
