using Framework.Infrastructure;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Framework
{
    public abstract class BasePageCommon
    {
        protected readonly IWebDriver _driver;
        protected WebDriverWait wait;
        protected Configuration config;

        public BasePageCommon(IWebDriver driver)
        {
            config = Configuration.GetConfiguration();
            _driver = driver;
            wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(config.ExplicitWait));
        }

        protected void NavigateTo(string urlPart)
        {
            var url = $"{config.BaseUrl}{urlPart}";
            _driver.Url = url;
            Console.WriteLine($"Navigate to '{url}'");
        }

        protected IWebElement GetElement(By by)
        {
            wait.Until(ElementPresent(by));
            return _driver.FindElement(by);
        }

        protected IList<IWebElement> GetElements(By by)
        {
            wait.Until(ElementPresent(by));
            return _driver.FindElements(by);
        }

        protected void EnterText(IWebElement element, string textValue, [Optional] string logMessage)
        {
            element.Clear();
            element.SendKeys(textValue);
            Console.WriteLine(logMessage);
        }

        protected void ClickOnElement(IWebElement element, [Optional] string logMessage)
        {
            element.Click();
            if (logMessage != null)
            {
                Console.WriteLine(logMessage);
            }
        }

        protected Func<IWebDriver, bool> ElementPresent(By by) => (driver) => driver.FindElements(by).Count > 0;

        protected bool IsElementPresent(By by) => _driver.FindElements(by).Count > 0;

        protected bool IsElementVisible(IWebElement element) => element.Displayed? true : false;
    }
}
