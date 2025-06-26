using Icon_Automation_Libs.Fixtures.UI;
using Icon_Automation_Libs.Scenario;

namespace Icon_Automation_Specs.Context;

public class UiContext : ContextBase
{
    public NavigationUiFixture NavigationUiFixture { get; }
    public LoginUiFixture LoginUiFixture { get; }
    public LoginUICompositeFixture LoginUICompositeFixture { get; }
    public HomeUiFixture HomeUiFixture { get; }


    public UiContext(
        IScenarioContext scenarioContext,
        NavigationUiFixture navigationUiFixture,
        LoginUiFixture loginUiFixture,
        LoginUICompositeFixture loginUICompositeFixture,
        HomeUiFixture homeUiFixture
    ) : base(scenarioContext)
    {
        NavigationUiFixture = navigationUiFixture;
        LoginUiFixture = loginUiFixture;
        LoginUICompositeFixture = loginUICompositeFixture;
        HomeUiFixture = homeUiFixture;
    }
}
