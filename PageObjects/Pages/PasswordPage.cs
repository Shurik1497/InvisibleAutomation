using OpenQA.Selenium;

namespace PageObjects.Pages
{
    public class PasswordPage : BaseGmailPage
    {
        private IWebElement PasswordField => GetElement(By.CssSelector("input[name='password']"));
        private IWebElement NextButton => GetElement(By.CssSelector("div#passwordNext"));
        
        public PasswordPage(IWebDriver driver) : base(driver)
        {
        }

        public void FillPassword(string password) => EnterText(PasswordField, password, $"Fill password with '{password}'");

        public void ClickNextButton() => ClickOnElement(NextButton, "Click 'Next' button");
    }
}
