using FluentAssertions;
using Icon_Automation_Libs.Config.Model;
using Icon_Automation_Libs.PageObjects.Home;
using Icon_Automation_Libs.Runner;
using Icon_Automation_Libs.Scenario;

namespace Icon_Automation_Libs.Fixtures.UI;

public class HomeUiFixture : FixtureBase
{
    private readonly HomePage _homePage;
    private readonly RunnerContext _runnerContext;

    public HomeUiFixture(
        ConfigModel config,
        IScenarioContext scenarioContext,
        RunnerContext runnerContext,
        HomePage homePage
    ) : base(
        config,
        scenarioContext
    )
    {
        _homePage = homePage;
        _runnerContext = runnerContext;
    }

    public void The_user_details_placeholder_should_be_present()
        => _homePage.UserDetailsPlaceHolderIsPresent();

    public void The_user_clicks_the_add_new_note_button()
        => _homePage.ClickCreateNoteButton();

    public void The_user_enters_note_text(string text)
        => _homePage.InputNoteText(text);

    public void The_user_clicks_the_goto_notebook_button()
        => _homePage.ClickGotoNotebookButton();

    public void The_user_clicks_the_user_details_placeholder_to_expand_user_menu()
        => _homePage.ClickUserDetailsPlaceHolder();

    public void The_user_click_the_sign_out_menu_option()
        => _homePage.ClickLogoutButton();

    public void The_user_clicks_the_note_button_to_open_note()
       => _homePage.ClickNoteButton();

    public void The_user_waits_for_the_note_text_to_be_visible()
       => _homePage.IsNoteInputVisible();

    public void The_note_text_is_verified(string text)
       => _homePage.GetNoteText().Should().Be(text);
}
