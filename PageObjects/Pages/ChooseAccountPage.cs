using OpenQA.Selenium;

namespace PageObjects.Pages
{
    public class ChooseAccountPage : BaseGmailPage
    {
        private IWebElement EmailField => GetElement(By.CssSelector("input#identifierId"));
        private IWebElement NextButton => GetElement(By.CssSelector("div#identifierNext"));

        public ChooseAccountPage(IWebDriver driver) : base(driver)
        {
        }

        public void Open() => NavigateTo("/");

        public void FillEmail(string email) => EnterText(EmailField, email, $"Fill email with '{email}'");

        public void ClickNextButton() => ClickOnElement(NextButton, "Click 'Next' button");
    }
}
