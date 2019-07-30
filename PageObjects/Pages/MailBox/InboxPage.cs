using OpenQA.Selenium;
using PageObjects.Controls;

namespace PageObjects.Pages.MailBox
{
    public class InboxPage : BaseMailBoxPage
    {
        public EmailGrid Grid => new EmailGrid(_driver);

        public InboxPage(IWebDriver driver) : base(driver)
        {
        }
    }
}
