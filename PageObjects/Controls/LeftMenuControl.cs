using OpenQA.Selenium;

namespace PageObjects.Controls
{
    public class LeftMenuControl : BaseGmailPage
    {
        private IWebElement ComposeButton => GetElement(By.CssSelector("div.aic"));
        private IWebElement InboxItem => GetElement(By.CssSelector("div.aHS-bnt"));

        public LeftMenuControl(IWebDriver driver) : base(driver)
        {
        }

        public void ClickComposeButton() => ClickOnElement(ComposeButton, "Click on 'Compose' button");

        public void ClickInboxItem() => ClickOnElement(InboxItem, "Click on 'Inbox' item in the left menu");
    }
}
