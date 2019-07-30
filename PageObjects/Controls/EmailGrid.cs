using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PageObjects.Controls
{
    public class EmailGrid : BaseGmailPage
    {
        private IList<IWebElement> VisibleEmailSubjects => GetElements(By.XPath("//div[not(contains(@style, 'none'))]//div[@class='y6']/span"));

        public EmailGrid(IWebDriver driver) : base(driver)
        {
        }

        /// <summary>
        /// Implemented for count < 50
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public void PrintLatestEmails(int count)
        {
            if (count > 50)
            {
                throw new ElementNotInteractableException();
            }

            var counter = VisibleEmailSubjects.Count < count ? VisibleEmailSubjects.Count : count;
            VisibleEmailSubjects.OrderByDescending(i => i.GetAttribute("data-legacy-last-message-id"));

            Console.WriteLine($"Print subjects of latest {counter} emails:");

            for (int i = 0; i < counter; i++)
            {
                Console.WriteLine($"\t{VisibleEmailSubjects[i].Text}");
            }
        }

        public bool IsEmailExist(string subject)
        {
            return VisibleEmailSubjects.Any(i => i.Text == subject);
        }
    }
}
