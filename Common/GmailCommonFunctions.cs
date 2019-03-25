using AlphaPoint_QA.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace AlphaPoint_QA.Common
{
    class GmailCommonFunctions
    {
        By confirmYourWithdrawLink = By.XPath("//a[contains(text(),'apexwebqa.')]");
        By googleAccount = By.XPath("//a[contains(@aria-label,'Google Account')]");
        By signOutButton = By.XPath("//a[text()='Sign out']");
        By userNameField = By.CssSelector("input[id='identifierId']");
        By userNameNextButton = By.CssSelector("div#identifierNext content>span.RveJvd.snByac");
        By passwordField = By.CssSelector("input[name='password']");
        By passwordNextButton = By.CssSelector("div#passwordNext content span.RveJvd.snByac");
        By listOfEmails = By.CssSelector("div.Cp tr");
        public string Gmail(IWebDriver driver, string gmailUserName, string password, string subject)
        {
            string linkText = null;
            try
            {
                string url = TestData.GetData("GmailURL");
                driver.Navigate().GoToUrl(url);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                driver.FindElement(userNameField).SendKeys(gmailUserName);
                driver.FindElement(userNameNextButton).Click();
                driver.FindElement(passwordField).SendKeys(password);
                Thread.Sleep(2000);
                driver.FindElement(passwordNextButton).Click();
                Thread.Sleep(3000);
                List<IWebElement> unReadEmails = new List<IWebElement>();
                ReadOnlyCollection<IWebElement> emailList = driver.FindElements(listOfEmails);
                for (int i = 0; i < emailList.Count; i++)
                {
                    string subjecttext = emailList[i].Text;
                    unReadEmails.Add(emailList[i]);
                    if (emailList[i].Text.Contains(subject))
                    {
                        unReadEmails[i].Click();
                        Thread.Sleep(2000);
                        break;
                    }
                }
                Thread.Sleep(1000);
                linkText = driver.FindElement(confirmYourWithdrawLink).Text;
                return linkText;
            }
            catch (StaleElementReferenceException)
            {
                Thread.Sleep(2000);
                linkText = driver.FindElement(confirmYourWithdrawLink).Text;
                return linkText;
            }
        }

        public void GmailLogout(IWebDriver driver)
        {
            try
            {
                driver.FindElement(googleAccount).Click();
                driver.FindElement(signOutButton).Click();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
