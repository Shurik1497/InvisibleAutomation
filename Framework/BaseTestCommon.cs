using Framework.Infrastructure;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;

namespace Framework
{
    public abstract class BaseTestCommon
    {
        protected Configuration Config;
        protected Random RandomInt = new Random();

        public BaseTestCommon()
        {
            Config = Configuration.GetConfiguration();
        }

        protected virtual void TearDown()
        {
            var testStatus = TestContext.CurrentContext.Result.Outcome.Status;

            if (testStatus == TestStatus.Failed)
            {
                WebDriverFactory.DismissCurrentDriver();
            }
        }

        protected virtual void OneTimeTearDown()
        {
            WebDriverFactory.DismissAll();
        }
    }
}
