using Icon_Automation_Libs.Config.Model;
using Icon_Automation_Libs.WebDriver;
using Icon_Automation_Libs.WebDriver.Button;
using Icon_Automation_Libs.WebDriver.Input;
using Icon_Automation_Libs.WebDriver.Modal;
using Icon_Automation_Libs.WebDriver.Selenium;
using Icon_Automation_Libs.WebDriver.Text;
using System.ComponentModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Icon_Automation_Libs.PageObjects.Login;

public class LoginPage : PageObject<LoginPage>
{
    public readonly IDriverClient _driver;
    public InputComponent UsernameInput { get; }
    public InputComponent PasswordInput { get; }
    public ButtonComponent LoginButton { get; }
    public ButtonComponent ContinueButton { get; }
    public TextComponent ErrorMessage { get;  }
    public ModalComponent CookieModal { get; }
    public ButtonComponent ConitnueWithoutCookiesButton { get; }

    public LoginPage(
        IDriverClient driver,
        WebElementFactory factory,
        ConfigModel config
    ) : base(driver, factory, config)
    {
        _driver = driver;
        LoginButton = Factory.CreateButtonElement("class", "link-background-animated rounded-lg px-3 pb-2.5 pt-2").AsComponent();
        ContinueButton = Factory.CreateButtonElement("type", "submit").AsComponent();
        UsernameInput = Factory.CreateInputElement("id", "email").AsComponent();
        PasswordInput = Factory.CreateInputElement("type", "password").AsComponent();
        ErrorMessage = Factory.CreateTextElement("class", "block w-full text-r14 text-secondary-red-400 my-4 text-left").AsComponent();
        CookieModal = Factory.CreateModalElement("data-sentry-component", "CookieBanner").AsComponent();
        ConitnueWithoutCookiesButton = Factory.CreateButtonElement("class", "rounded-sm px-2 py-1.5 text-[11px] font-normal leading-normal text-grey-90 hover:bg-grey-20").AsComponent();

    }

    public LoginPage LoginButtonIsPresent()
    {
        LoginButton.WaitToBeVisible();
        return this;
    }

    public LoginPage ClickLoginButton()
    {
        LoginButton.WaitToBeInteractable().Click();
        return this;
    }

    public LoginPage ContinueButtonIsPresent()
    {
        ContinueButton.WaitToBeVisible();
        return this;
    }

    public LoginPage ClickContinueButton()
    {
        ContinueButton.WaitToBeInteractable().Click();
        return this;
    }

    public LoginPage EnterEmailInput(string email)
    {
        UsernameInput.WaitForInputToBeVisible()
            .Clear()
            .Value(email);

        return this;
    }

    public LoginPage EnterPasswordInput(string password)
    {
        PasswordInput.WaitForInputToBeVisible()
            .Clear();          
            //.Value(password);

        foreach (char c in password)
            PasswordInput.SendKey(c.ToString());

        return this;
    }

    public string GetErrorMessage()
       => ErrorMessage.WaitToBeVisible().GetText();

    public LoginPage WaitForCookieModalToBeVisible()
    {
        CookieModal.WaitToBeVisible();
        return this;
    }

    public LoginPage ClickConitnueWithoutCookiesButtonButton()
    {
        ConitnueWithoutCookiesButton.WaitToBeInteractable().Click();
        return this;
    }
}
