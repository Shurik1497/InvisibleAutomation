using OpenQA.Selenium;

namespace PageObjects.Dialogs
{
    public class NewMessageDialog : BaseGmailPage
    {
        private IWebElement ActiveToField => GetElement(By.CssSelector("textarea[name='to']"));
        private IWebElement NotActiveToField => GetElement(By.CssSelector("div.aoD"));
        private IWebElement SubjectField => GetElement(By.CssSelector("input[name='subjectbox']"));
        private IWebElement BodyField => GetElement(By.CssSelector("table#undefined div.editable"));
        private IWebElement ExpandCcLink => GetElement(By.CssSelector("span.aB[role='link']"));
        private IWebElement CcField => GetElement(By.CssSelector("textarea[name='cc']"));
        private IWebElement SendButton => GetElement(By.CssSelector("td.Up div[role='button']"));
        private readonly By _messageSentDialogBy = By.CssSelector("span.bAq");

        public NewMessageDialog(IWebDriver driver) : base(driver)
        {
        }

        public void FillToField(string addressTo)
        {
            FocusOnToField();
            EnterText(ActiveToField, addressTo, $"Fill 'To' with '{addressTo}'");
        }

        public void FillSubject(string subject) => EnterText(SubjectField, subject, $"Fill 'Subject' with '{subject}'");

        public void FillBody(string body) => EnterText(BodyField, body, $"Fill 'Email body' with '{body}'");

        public void ClickSendButton()
        {
            ClickOnElement(SendButton, "Click 'Send' button");
            wait.Until(ElementPresent(_messageSentDialogBy));
        }

        public void FillCc(string addressCc)
        {
            ExpandCcField();
            EnterText(CcField, addressCc, $"Fill 'Cc' with '{addressCc}'");
        }

        private void ExpandCcField()
        {
            FocusOnToField();
            ClickOnElement(ExpandCcLink, "Expand 'Cc' field");
        }

        private void FocusOnToField()
        {
            if (IsElementVisible(ActiveToField)) return;
            ClickOnElement(NotActiveToField);
        }
    }
}
