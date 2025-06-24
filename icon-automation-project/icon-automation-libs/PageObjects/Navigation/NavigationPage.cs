using Icon_Automation_Libs.Config.Model;
using Icon_Automation_Libs.WebDriver;
using Icon_Automation_Libs.WebDriver.Selenium;

namespace Icon_Automation_Libs.PageObjects.Navigation;

public class NavigationPage : PageObject<NavigationPage>
{
    public NavigationPage(
        IDriverClient driver,
        WebElementFactory factory,
        ConfigModel config
    ) : base(driver, factory, config)
    {	
	}
	
	public NavigationPage PageRefresh()
	{
		Driver.RefreshPage();
		return this;
	}
	
}
