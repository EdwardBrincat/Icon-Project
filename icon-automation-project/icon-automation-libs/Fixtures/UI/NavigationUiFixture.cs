using Icon_Automation_Libs.Config.Model;
using Icon_Automation_Libs.PageObjects.Login;
using Icon_Automation_Libs.PageObjects.Navigation;
using Icon_Automation_Libs.Runner;
using Icon_Automation_Libs.Scenario;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icon_Automation_Libs.Fixtures.UI;

public class NavigationUiFixture : FixtureBase
{
    private readonly NavigationPage _navigationPage;
    private readonly RunnerContext _runnerContext;
    private readonly ConfigModel _config;

    public NavigationUiFixture(
        ConfigModel config,
        IScenarioContext scenarioContext,
        RunnerContext runnerContext,
        NavigationPage navigationPage
    ) : base(
        config,
        scenarioContext
    )
    {
        _navigationPage = navigationPage;
        _runnerContext = runnerContext;
        _config = config;
    }

    public void The_root_page_is_opened()
    {       
        _navigationPage.Open(_config.UiBaseUrl);

        _navigationPage.WaitForPageToLoad();
    }

    public void The_page_is_refreshed()
        => _navigationPage.PageRefresh();
}
