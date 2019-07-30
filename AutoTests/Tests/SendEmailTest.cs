using NUnit.Framework;

namespace AutoTests.Tests
{
    [TestFixture]
    public class SendEmailTest : TestBaseGmail
    {
        [Test]
        [TestCase("kovbels@gmail.com", "Subject 1", "Some text 1")]
        [TestCase("kovbels@gmail.com", "Subject 2", "Some text 2")]
        [TestCase("kovbels@gmail.com", "Subject 3", "Some text 3")]
        public void SendEmail(string to, string subject, string emailBody)
        {
            //Arrange
            int number = RandomInt.Next(0, 9999999);
            subject = $"{subject} {number}";

            //Test
            Ui.InboxPage.LeftMenu.ClickComposeButton();
            Ui.NewMessageDialog.FillToField(to);
            Ui.NewMessageDialog.FillSubject(subject);
            Ui.NewMessageDialog.FillBody(emailBody);
            Ui.NewMessageDialog.FillCc(Config.Email);
            Ui.NewMessageDialog.ClickSendButton();

            //Asserts
            Assert.That(Ui.InboxPage.Grid.IsEmailExist(subject));
        }

        [Test]
        public void PrintSubjectsOfLatestEmails()
        {
            Ui.InboxPage.LeftMenu.ClickInboxItem();
            Ui.InboxPage.Grid.PrintLatestEmails(10);
        }
    }
}
