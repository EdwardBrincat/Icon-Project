using FluentAssertions;
using Icon_Automation_Libs.ActionContext.Factory;
using Icon_Automation_Libs.Config.Model;
using Icon_Automation_Libs.PageObjects.Login;
using Icon_Automation_Libs.Runner;
using Icon_Automation_Libs.Scenario;
using Icon_Automation_Libs.WebDriver.Selenium;

namespace Icon_Automation_Libs.Fixtures.UI;

public class LoginUiFixture : FixtureBase
{
    public readonly IDriverClient _driver;
    private readonly LoginPage _loginPage;
    private readonly RunnerContext _runnerContext;

    public LoginUiFixture(
        ConfigModel config,
        IScenarioContext scenarioContext,
        IDriverClient driver,
        RunnerContext runnerContext,
        LoginPage loginPage
    ) : base(
        config,
        scenarioContext
    )
    {        
        _driver = driver;
        _loginPage = loginPage;
        _runnerContext = runnerContext;
    }

    public void The_login_button_should_be_present()
        => _loginPage.LoginButtonIsPresent();

    public void The_user_clicks_the_login_button()
        => _loginPage.ClickLoginButton();

    public void The_conitnue_button_should_be_present()
       => _loginPage.ContinueButtonIsPresent();

    public void The_user_clicks_the_conitnue_button()
        => _loginPage.ClickContinueButton();

    public void The_user_waits_for_the_login_email_to_be_visible()
       => _loginPage.IsEmailInputVisible();

    public void The_user_enters_the_login_email(string email)
        => _loginPage.EnterEmailInput(email);

    public void The_user_enters_the_login_password(string password)
        => _loginPage.EnterPasswordInput(password);

    public void The_login_error_message_is_verified(string message)
        => _loginPage.GetErrorMessage().Should().Be(message);
    public async Task InterceptCheckPasswordResponseToSetCheck(string urlEndpoint, string redirectUrl, bool passwordChecked)
        => await _driver.InterceptGetGamesResponseToSetSportsbook(urlEndpoint, redirectUrl, passwordChecked);
}

