using Framework;
using NUnit.Framework;
using PageObjects;

namespace AutoTests
{
    [SetUpFixture]
    public abstract class TestBaseGmail : BaseTestCommon
    {
        protected ViewFactory Ui => ViewFactoryPool.GetPageFactory();

        [SetUp]
        protected void SetUp()
        {
            LoginIfNeed();
        }

        [TearDown]
        protected override void TearDown()
        {
            ViewFactoryPool.RemovePageFactory();
            base.TearDown();
        }

        [OneTimeSetUp]
        protected void OneTimeSetUp()
        {
        }

        [OneTimeTearDown]
        protected override void OneTimeTearDown()
        {
            base.OneTimeTearDown();
        }

        private void LoginIfNeed()
        {
            if (!Ui.InboxPage.IsLoggedIn())
            {
                Ui.ChooseAccountPage.Open();
                Ui.ChooseAccountPage.FillEmail(Config.Email);
                Ui.ChooseAccountPage.ClickNextButton();
                Ui.PasswordPage.FillPassword(Config.Password);
                Ui.PasswordPage.ClickNextButton();
            }
        }
    }
}
