using Icon_Automation_Libs.Attributes;
using Icon_Automation_Libs.Config.Model;
using Icon_Automation_Libs.Const;
using Icon_Automation_Libs.Runner;
using Icon_Automation_Libs.UiTestCaseData;
using Icon_Automation_Specs.Context;
using LightBDD.Framework;
using LightBDD.Framework.Scenarios;
using LightBDD.NUnit3;
using NUnit.Framework;
using System.Threading.Tasks;
using static Icon_Automation_Libs.Const.FeatureConst;

namespace Icon_Automation_Specs.Features.Ui;


[Suite(SuiteType.UI)]
[Category(FeatureConst.UI.Evernote)]
[Label("Evernote Feature")]
[FeatureDescription(
    @"In order to verify the evernote site as a tester I want to be able to login and add a note")]
public class EvernoteFeature : FeatureFixture
{

    [IconScenario("Scenario-UI-1", FeatureConst.UI.Evernote)]
    [TestCaseSource(typeof(UiTestCaseData))]
    public async Task Scenario_Ui_001_TESTID_Login_negative_path(string testId, EverNoteTestData testData) =>
        await Runner.WithContext<UiContext>()
            .AddAsyncSteps(
                given => given.NavigationUiFixture.The_root_page_is_opened())
            .AddSteps(                
                when => when.LoginUiFixture.The_user_enters_the_login_email(testData.Email),
                then => then.LoginUiFixture.The_conitnue_button_should_be_present(),
                then => then.LoginUiFixture.The_user_clicks_the_conitnue_button(false),
                then => then.LoginUiFixture.The_user_enters_the_login_password(testData.InCorrectPassword),
                then => then.LoginUiFixture.The_user_clicks_the_conitnue_button(false),
                then => then.LoginUiFixture.The_login_error_message_is_verified(testData.ErrorMessage))
            .RunAsync();

    [IconScenario("Scenario-UI-2", FeatureConst.UI.Evernote)]
    [TestCaseSource(typeof(UiTestCaseData))]
    public async Task Scenario_Ui_002_TESTID_Successful_Login(string testId, EverNoteTestData testData) =>
        await Runner.WithContext<UiContext>()
        .AddAsyncSteps(
                given => given.NavigationUiFixture.The_root_page_is_opened())
            .AddSteps(
                 when => when.LoginUICompositeFixture.The_user_logs_the_evernote_site(testData))
        .RunAsync();

    [IconScenario("Scenario-UI-3", FeatureConst.UI.Evernote)]
    [TestCaseSource(typeof(UiTestCaseData))]
    public async Task Scenario_UI_003_TESTID_Note_Creation(string testId, EverNoteTestData testData) =>
        await Runner.WithContext<UiContext>()
        .AddAsyncSteps(
                given => given.NavigationUiFixture.The_root_page_is_opened())
            .AddSteps(
                when => when.LoginUICompositeFixture.The_user_logs_the_evernote_site(testData),
                then => then.HomeUiFixture.The_user_enters_note_text(testData.NoteText),
                then => then.HomeUiFixture.The_user_clicks_the_goto_notebook_button(),
                then => then.HomeUiFixture.The_user_clicks_the_user_details_placeholder_to_expand_user_menu(),
                then => then.HomeUiFixture.The_user_click_the_sign_out_menu_option(),
                then => then.LoginUiFixture.The_user_waits_for_the_login_email_to_be_visible(),
                then => then.LoginUICompositeFixture.The_user_logs_the_evernote_site(testData),                
                then => then.HomeUiFixture.The_user_clicks_the_note_button_to_open_note(),
                then => then.HomeUiFixture.The_user_waits_for_the_note_text_to_be_visible(),
                then => then.HomeUiFixture.The_note_text_is_verified(testData.NoteText))
        .RunAsync();
}


