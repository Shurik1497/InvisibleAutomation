using Framework.Infrastructure;
using OpenQA.Selenium;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace PageObjects
{
    public class ViewFactoryPool
    {
        private IDictionary<IWebDriver, ViewFactory> _PageFactoriesBag = new ConcurrentDictionary<IWebDriver, ViewFactory>();

        public static ViewFactory GetPageFactory()
        {
            return PageFactoryInstance._GetPageFactory();
        }

        public static void RemovePageFactory()
        {
            PageFactoryInstance._RemovePageFactory(WebDriverFactory.GetCurrentDriver());
        }

        #region singleton

        private static ViewFactoryPool instance;
        private static object syncRoot = new Object();

        private static ViewFactoryPool PageFactoryInstance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new ViewFactoryPool();
                    }
                }
                return instance;
            }
        }

        #endregion

        private ViewFactory _GetPageFactory()
        {
            if (WebDriverFactory.GetCurrentDriver() != null && _PageFactoriesBag.ContainsKey(WebDriverFactory.GetCurrentDriver()))
                return _PageFactoriesBag[WebDriverFactory.GetCurrentDriver()];
            else
            {
                var driver = WebDriverFactory.GetDriver(Configuration.GetConfiguration().HubUrl);
                var _pageObject = new ViewFactory(driver);
                _PageFactoriesBag.Add(driver, _pageObject);
                return _pageObject;
            }
        }

        private void _RemovePageFactory(IWebDriver driver)
        {
            _PageFactoriesBag.Remove(driver);
        }
    }
}
