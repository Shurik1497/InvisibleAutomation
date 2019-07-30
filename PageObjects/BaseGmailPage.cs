using Framework;
using OpenQA.Selenium;

namespace PageObjects
{
    public abstract class BaseGmailPage : BasePageCommon
    {
        public BaseGmailPage(IWebDriver driver) : base(driver)
        {
        }

        public bool IsLoggedIn()
        {
            return IsElementPresent(By.CssSelector("a[href*='accounts.google.com/SignOutOptions']"));
        }
    }
}
