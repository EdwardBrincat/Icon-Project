using Icon_Automation_Libs.Fixtures.UI;
using Icon_Automation_Libs.Scenario;

namespace Icon_Automation_Specs.Context;

public class UiContext : ContextBase
{
    public NavigationUiFixture NavigationUiFixture { get; }
    public LoginUiFixture LoginUiFixture { get; }


    public UiContext(
        IScenarioContext scenarioContext,
        NavigationUiFixture navigationUiFixture,
        LoginUiFixture loginUiFixture
    ) : base(scenarioContext)
    {
        NavigationUiFixture = navigationUiFixture;
        LoginUiFixture = loginUiFixture;
    }
}
