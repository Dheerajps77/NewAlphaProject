using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaPoint_QA.Pages
{
    class AdminTicketsPage
    {










        public Dictionary<string, string> GetWithdrawTicketsFieldsByTicketID(IWebDriver driver,string ticketIDValue)
        {
            Dictionary<string, string> withdrawTicketsFields = null;
            try
            {
                int count = driver.FindElements(By.XPath("//div[@class='ReactVirtualized__Grid__innerScrollContainer']/div")).Count;
                    for (int i = 1; i <= count - 1; i++)
                    {
                        string ticketID = driver.FindElement(By.XPath("//div[@class='ReactVirtualized__Grid__innerScrollContainer']/div[" + i + "]/div[1]")).Text;
                       
                        if (ticketID.Equals(ticketIDValue))
                        {
                        withdrawTicketsFields = new Dictionary<string, string>();
                        withdrawTicketsFields.Add("Ticket Id", ticketID);
                        withdrawTicketsFields.Add("Account Id", driver.FindElement(By.XPath("//div[@class='ReactVirtualized__Grid__innerScrollContainer']/div[" + i + "]/div[2]")).Text);
                        withdrawTicketsFields.Add("Asset", driver.FindElement(By.XPath("//div[@class='ReactVirtualized__Grid__innerScrollContainer']/div[" + i + "]/div[3]")).Text);
                        withdrawTicketsFields.Add("Amount", driver.FindElement(By.XPath("//div[@class='ReactVirtualized__Grid__innerScrollContainer']/div[" + i + "]/div[4]")).Text);
                        withdrawTicketsFields.Add("Request Code", driver.FindElement(By.XPath("//div[@class='ReactVirtualized__Grid__innerScrollContainer']/div[" + i + "]/div[5]")).Text);
                        withdrawTicketsFields.Add("Request User", driver.FindElement(By.XPath("//div[@class='ReactVirtualized__Grid__innerScrollContainer']/div[" + i + "]/div[6]")).Text);
                        withdrawTicketsFields.Add("Status", driver.FindElement(By.XPath("//div[@class='ReactVirtualized__Grid__innerScrollContainer']/div[" + i + "]/div[7]")).Text);
                        break;
                        }
                    }
            }
            catch (Exception)
            {
                throw;
            }
            return withdrawTicketsFields;
        }









    }
}
