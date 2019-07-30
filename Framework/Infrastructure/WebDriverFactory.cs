using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;

namespace Framework.Infrastructure
{
    public class WebDriverFactory
    {
        private static readonly ThreadLocal<IWebDriver> ThreadLocalDriver = new ThreadLocal<IWebDriver>();
        private static readonly ConcurrentBag<IWebDriver> AllDrivers = new ConcurrentBag<IWebDriver>();

        #region singleton

        private static WebDriverFactory _instance;
        private static readonly object SyncRoot = new object();

        public static WebDriverFactory FactoryInstance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new WebDriverFactory();
                        }
                    }
                }

                return _instance;
            }
        }

        #endregion

        #region Work with Driver

        public static IWebDriver GetDriver([Optional] string hub) =>
            FactoryInstance._GetDriver(hub);

        public static void DismissDriver(IWebDriver driver) => FactoryInstance.__DismissDriver(driver);

        public static void DismissCurrentDriver() =>
            FactoryInstance.__DismissDriver(FactoryInstance._GetCurrentDriver());

        public static IWebDriver GetCurrentDriver() => FactoryInstance._GetCurrentDriver();

        public static void DismissAll() => FactoryInstance.__DismissAll();

        #endregion

        #region Get Driver

        private IWebDriver _GetDriver(string hub)
        {
            if (ThreadLocalDriver.Value == null)
            {
                CreateNewDriver(hub);
            }
            else if (!ThreadLocalDriver.IsValueCreated)
            {
                CreateNewDriver(hub);
            }
            else
            {
                var currentDriver = ThreadLocalDriver.Value;
                try
                {
                    var currentUrl = currentDriver.Url;
                }
                catch (WebDriverException)
                {
                    CreateNewDriver(hub);
                }
            }

            return ThreadLocalDriver.Value;
        }

        #endregion

        #region Create Driver

        private void CreateNewDriver(string hub)
        {
            var driver = (hub == null || hub == string.Empty)
                ? CreateLocalDriver()
                : CreateRemoteDriver(hub);

            AllDrivers.Add(driver);
            ThreadLocalDriver.Value = driver;
        }

        private IWebDriver CreateRemoteDriver(string hub)
        {
            var options = new ChromeOptions();
            options.AddArguments("--disable-extensions", "--disable-infobars");
            var driver = new RemoteWebDriver(new Uri(hub), options);
            driver.Manage().Window.Position = new Point(0, 0);
            driver.Manage().Window.Size = new Size(1920, 1080);
            return driver;
        }

        private IWebDriver CreateLocalDriver()
        {
            var webDriver = ThreadLocalDriver.Value;

            var path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var options = new ChromeOptions();
            options.AddArgument("start-maximized");
            options.AddArguments("--disable-extensions", "--disable-infobars");
            var driver = new ChromeDriver(path, options, TimeSpan.FromMinutes(1));
            return driver;
        }

        #endregion

        private void __DismissDriver(IWebDriver driver)
        {
            driver.Close();
            driver.Quit();
        }

        private void __DismissAll()
        {
            foreach (var driver in AllDrivers)
            {
                __DismissDriver(driver);
            }

            AllDrivers.Clear();
        }

        private IWebDriver _GetCurrentDriver() => ThreadLocalDriver.Value;
    }
}