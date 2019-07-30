using OpenQA.Selenium;
using PageObjects.Dialogs;
using PageObjects.Pages;
using PageObjects.Pages.MailBox;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace PageObjects
{
    public class ViewFactory
    {
        private readonly IWebDriver _driver;

        public ViewFactory(IWebDriver driver)
        {
            _driver = driver;
        }

        #region Pages

        public ChooseAccountPage ChooseAccountPage => Get<ChooseAccountPage>();
        public PasswordPage PasswordPage => Get<PasswordPage>();
        public InboxPage InboxPage => Get<InboxPage>();

        #endregion

        #region Dialogs

        public NewMessageDialog NewMessageDialog => Get<NewMessageDialog>();

        #endregion

        #region Get method

        private readonly IDictionary<Type, object> _pageObjects = new ConcurrentDictionary<Type, object>();

        private T Get<T>() where T : class
        {
            var type = typeof(T);
            if (_pageObjects.ContainsKey(type))
                return (T)_pageObjects[type];
            var pageObject = (T)Activator.CreateInstance(typeof(T), _driver);
            _pageObjects.Add(type, pageObject);
            return pageObject;
        }
        #endregion
    }
}
