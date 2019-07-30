using OpenQA.Selenium;
using PageObjects.Controls;

namespace PageObjects.Pages.MailBox
{
    public abstract class BaseMailBoxPage : BaseGmailPage
    {
        public LeftMenuControl LeftMenu => new LeftMenuControl(_driver);

        public BaseMailBoxPage(IWebDriver driver) : base(driver)
        {
        }
    }
}
