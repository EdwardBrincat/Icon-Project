using Icon_Automation_Libs.Config.Model;
using Icon_Automation_Libs.WebDriver;
using Icon_Automation_Libs.WebDriver.Button;
using Icon_Automation_Libs.WebDriver.Input;
using Icon_Automation_Libs.WebDriver.PlaceHolder;
using Icon_Automation_Libs.WebDriver.Selenium;
using Icon_Automation_Libs.WebDriver.Text;

namespace Icon_Automation_Libs.PageObjects.Home;

public class HomePage : PageObject<HomePage>
{
    public readonly IDriverClient _driver;
    public PlaceHolderComponent UserDetailsPlaceHolder { get; }
    public ButtonComponent CreateNoteButton { get; }
    public ButtonComponent GotoNotebookButton { get; }
    public ButtonComponent LogoutButton { get; }
    public InputComponent NoteInput { get; }
    public ButtonComponent NoteButton { get; }

    public HomePage(
        IDriverClient driver,
        WebElementFactory factory,
        ConfigModel config
    ) : base(driver, factory, config)
    {
        _driver = driver;
        UserDetailsPlaceHolder = Factory.CreatePlaceHolderElement("id", "qa-NAV_USER").AsComponent();
        CreateNoteButton = Factory.CreateButtonElement("id", "qa-SIDEBAR_CREATE_NOTE").AsComponent();
        GotoNotebookButton = Factory.CreateButtonElement("id", "qa-NOTE_PARENT_NOTEBOOK_BTN").AsComponent();        
        LogoutButton = Factory.CreateButtonElement("id", "qa-ACCOUNT_DROPDOWN_LOGOUT_outer").AsComponent();
        NoteInput = Factory.CreateInputElement("id", "en-note").AsComponent();
        NoteButton = Factory.CreateButtonElement("id", "qa-NOTES_SIDEBAR_NOTE").AsComponent();
    }

    public HomePage UserDetailsPlaceHolderIsPresent()
    {
        UserDetailsPlaceHolder.WaitToBeVisible();
        return this;
    }

    public HomePage ClickUserDetailsPlaceHolder()
    {
        UserDetailsPlaceHolder.WaitToBeVisible().Click();
        return this;
    }

    public HomePage ClickCreateNoteButton()
    {
        CreateNoteButton.WaitToBeInteractable().Click();
        return this;
    }

    public HomePage ClickGotoNotebookButton()
    {
        GotoNotebookButton.WaitToBeInteractable().Click();
        return this;
    }

    public HomePage ClickLogoutButton()
    {
        LogoutButton.WaitToBeInteractable().Click();
        return this;
    }

    public HomePage InputNoteText(string noteText)
    {
        NoteInput.WaitToBeInteractable().Clear().Value(noteText);
        return this;
    }

    public HomePage ClickNoteButton()
    {
        NoteButton.WaitToBeInteractable().Click();
        return this;
    }

    public HomePage IsNoteInputVisible()
    {
        NoteInput.WaitForInputToBeVisible();
        return this;
    }

    public string GetNoteText()
        => NoteInput.GetValue();

}

