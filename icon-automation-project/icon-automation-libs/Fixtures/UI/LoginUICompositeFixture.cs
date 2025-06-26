using Icon_Automation_Libs.Config.Model;
using Icon_Automation_Libs.Runner;
using Icon_Automation_Libs.Scenario;
using LightBDD.Framework;
using LightBDD.Framework.Scenarios;

namespace Icon_Automation_Libs.Fixtures.UI;

public class LoginUICompositeFixture : FixtureBase
{
    private readonly LoginUiFixture _loginUiFixture;
    private readonly HomeUiFixture _homeUiFixture;
    private readonly RunnerContext _runnerContext;

    public LoginUICompositeFixture(
         ConfigModel config,
         IScenarioContext scenarioContext,
         RunnerContext runnerContext,
         LoginUiFixture loginUiFixture,
         HomeUiFixture homeUiFixture
     ) : base(
         config,
         scenarioContext
     )
    {
        _loginUiFixture = loginUiFixture;
        _homeUiFixture = homeUiFixture;
        _runnerContext = runnerContext;
    }

    public CompositeStep The_user_logs_the_evernote_site(EverNoteTestData testData)
        => CompositeStep.DefineNew()
            .AddSteps(
                then => _loginUiFixture.The_user_enters_the_login_email(testData.Email),
                then => _loginUiFixture.The_conitnue_button_should_be_present(),
                then => _loginUiFixture.The_user_clicks_the_conitnue_button(),
                then => _loginUiFixture.The_user_enters_the_login_password(testData.CorrectPassword),
                then => _loginUiFixture.The_user_clicks_the_conitnue_button(),
                then => _homeUiFixture.The_user_details_placeholder_should_be_present())
            .Build();
}
