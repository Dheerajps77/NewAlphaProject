using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using AlphaPoint_QA.Common;
using AlphaPoint_QA.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Events;

namespace AlphaPoint_QA.Pages
{
    class AdminOMSOrdersPage
    {
        IWebDriver driver;
        ProgressLogger logger;

        public AdminOMSOrdersPage(ProgressLogger logger)
        {
            this.logger = logger;
            driver = AlphaPointWebDriver.GetInstanceOfAlphaPointWebDriver();
        }

        By buySideBookRow = By.CssSelector("section.secondary_container.half_container:nth-of-type(1) div.ReactVirtualized__Grid__innerScrollContainer>div");
        By sellSideBookRow = By.CssSelector("section.secondary_container.half_container:nth-of-type(2) div.ReactVirtualized__Grid__innerScrollContainer>div");
        By omsNumOfOrdersDropDown = By.CssSelector("#Depth");
        By moreFiltersLink = By.CssSelector("div.head>span");
        By accountIdTextBox = By.CssSelector("input[name=AccountId]");
        By userIdTextBox = By.CssSelector("input[name=UserId]");
        By orderIdTextBox = By.CssSelector("input[name=OriginalOrderId]");
        By omsOrdersHistoryTab = By.CssSelector("#SelectTab1");
        By rejectReasonTitle = By.CssSelector("div.details_container>table:nth-of-type(1)>tbody>tr:nth-of-type(3)>td:nth-of-type(1)");
        By rejectReasonValue = By.CssSelector("div.details_container>table:nth-of-type(1)>tbody>tr:nth-of-type(3)>td:nth-of-type(2)");

        public IWebElement AccountIdTextBox()
        {
            return driver.FindElement(accountIdTextBox);
        }

        public IWebElement UserIdTextBox()
        {
            return driver.FindElement(userIdTextBox);
        }

        public IWebElement OrderIdTextBox()
        {
            return driver.FindElement(orderIdTextBox);
        }

        public IWebElement RejectReasonTitleElement()
        {
            return driver.FindElement(rejectReasonTitle);
        }

        public IWebElement RejectReasonValue()
        {
            return driver.FindElement(rejectReasonValue);
        }

        public void ClickMoreFiltersLink()
        {
            driver.FindElement(moreFiltersLink).Click();
        }

        public void ClickOMSOrdersHistoryTab()
        {
            driver.FindElement(omsOrdersHistoryTab).Click();
        }

        /// <summary>
        ///  Methods related to OMS Open Oders tab 
        /// </summary>

        //This method selects number of orders to be displayed on the OMS Orders page
        public void SelectOMSOrdersInstrument(string noOfOrders)
        {
            try
            {
                GenericUtils.SelectDropDownByValue(driver, omsNumOfOrdersDropDown, noOfOrders);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // This method verifies the number of orders displayed. 
        public bool VerifyNumOfOrdersDisplayed(string numOfOrders, string instrument)
        {
            bool flag = false;
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(logger);
            try
            {
                int numberOfOrders = Int32.Parse(numOfOrders);
                objAdminCommonFunctions.SelectOMSOrdersInstrument(instrument);
                Thread.Sleep(1000);
                // This select the number of orders to be displayed from the orders dropdown
                SelectOMSOrdersInstrument(numOfOrders);
                Thread.Sleep(1000);
                int countOfBuySideOrders = driver.FindElements(buySideBookRow).Count;
                int countOfSellSideOrders = driver.FindElements(sellSideBookRow).Count;
                if ((countOfBuySideOrders <= numberOfOrders) && (countOfSellSideOrders <= numberOfOrders))
                {
                    flag = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }

        // This selects the instrument on the OMS Open orders page and verifies the related orders in the buy and sell side books
        public bool VerifySelectOMSOpenOrdersInstrument(Dictionary<string, string> userDetailsDict, string instrument, string buyPrice, string sellPrice, string buyQuantity, string sellQuantity, string orderType)
        {
            bool orderFoundFlag = false;
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(logger);
            try
            {
                objAdminCommonFunctions.ClickOnOMSOrdersMenuLink();
                objAdminCommonFunctions.SelectOMSOrdersInstrument(instrument);
                Thread.Sleep(1000);
                orderFoundFlag = VerifyOMSOrdersBuySideBook(userDetailsDict["AccountId"], userDetailsDict["UserId"], buyPrice, buyQuantity, orderType);
                orderFoundFlag = VerifyOMSOrdersSellSideBook(userDetailsDict["AccountId"], userDetailsDict["UserId"], sellPrice, sellQuantity, orderType);
            }
            catch (Exception)
            {
                throw;
            }
            return orderFoundFlag;
        }

        // Add filter by accountId and verify that only the details of the accountId which was passed on filter is displayed in Buy side and Sell side order book 
        public bool VerifySearchOMSOrdersByAcountId(string instrument, string accountId)
        {
            bool accountIdFoundFlag = false;
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(logger);
            try
            {
                objAdminCommonFunctions.ClickOnOMSOrdersMenuLink();
                objAdminCommonFunctions.SelectOMSOrdersInstrument(instrument);
                ClickMoreFiltersLink();
                AccountIdTextBox().Clear();
                UserIdTextBox().Clear();
                AccountIdTextBox().SendKeys(accountId);
                ClickMoreFiltersLink();
                accountIdFoundFlag = VerifyAccountIdInBuySideBook(accountId);
                accountIdFoundFlag = VerifyAccountIdInSellSideBook(accountId);
            }
            catch (Exception)
            {
                throw;
            }
            return accountIdFoundFlag;
        }

        // This method will check if the accountId is present on the Buy side book or not. Returns false if the accountId is not present
        public bool VerifyAccountIdInBuySideBook(string accountId)
        {
            bool flag = true;
            int countOfBuySideOrders = driver.FindElements(buySideBookRow).Count;
            if (countOfBuySideOrders > 0)
            {
                for (int i = 1; i <= countOfBuySideOrders; i++)
                {
                    string text = driver.FindElement(By.XPath("//section[@class='secondary_container half_container'][1]//div[@class='ReactVirtualized__Grid__innerScrollContainer']/div[" + i + "]/div[2]")).Text;
                    if (!(text.Equals(accountId)))
                    {
                        flag = false;
                    }
                }
            }
            return flag;
        }

        // This method will check if the accountId is present on the Sell side book or not. Returns false if the accountId is not present
        public bool VerifyAccountIdInSellSideBook(string accountId)
        {
            bool flag = true;
            int countOfSellSideOrders = driver.FindElements(sellSideBookRow).Count;
            if (countOfSellSideOrders > 0)
            {
                for (int i = 1; i <= countOfSellSideOrders; i++)
                {
                    string text = driver.FindElement(By.XPath("//section[@class='secondary_container half_container'][2]//div[@class='ReactVirtualized__Grid__innerScrollContainer']/div[" + i + "]/div[2]")).Text;
                    if (!(text.Equals(accountId)))
                    {
                        flag = false;
                    }
                }
            }
            return flag;
        }

        // Add filter by userId and verify that only the details of the userId which was passed on filter is displayed in Buy side and Sell side order book 
        public bool VerifySearchOMSOrdersByUserId(string instrument, string userId)
        {
            bool userIdFoundFlag = false;
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(logger);
            try
            {
                objAdminCommonFunctions.ClickOnOMSOrdersMenuLink();
                objAdminCommonFunctions.SelectOMSOrdersInstrument(instrument);
                ClickMoreFiltersLink();
                AccountIdTextBox().Clear();
                UserIdTextBox().Clear();
                UserIdTextBox().SendKeys(userId);
                ClickMoreFiltersLink();
                userIdFoundFlag = VerifyUserIdInBuySideBook(userId);
                userIdFoundFlag = VerifyUserIdInSellSideBook(userId);
                return userIdFoundFlag;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // This method will check if the userId is present on the Buy side book or not. Returns false if the userId is not present
        public bool VerifyUserIdInBuySideBook(string userId)
        {
            bool flag = true;
            int countOfBuySideOrders = driver.FindElements(buySideBookRow).Count;
            if (countOfBuySideOrders > 0)
            {
                for (int i = 1; i <= countOfBuySideOrders; i++)
                {
                    string text = driver.FindElement(By.XPath("//section[@class='secondary_container half_container'][1]//div[@class='ReactVirtualized__Grid__innerScrollContainer']/div[" + i + "]/div[3]")).Text;
                    if (!(text.Equals(userId)))
                    {
                        flag = false;
                    }
                }
            }
            return flag;
        }

        // This method will check if the userId is present on the Sell side book or not. Returns false if the userId is not present
        public bool VerifyUserIdInSellSideBook(string userId)
        {
            bool flag = true;
            int countOfSellSideOrders = driver.FindElements(sellSideBookRow).Count;
            if (countOfSellSideOrders > 0)
            {
                for (int i = 1; i <= countOfSellSideOrders; i++)
                {
                    string text = driver.FindElement(By.XPath("//section[@class='secondary_container half_container'][2]//div[@class='ReactVirtualized__Grid__innerScrollContainer']/div[" + i + "]/div[3]")).Text;
                    if (!(text.Equals(userId)))
                    {
                        flag = false;
                    }
                }
            }
            return flag;
        }

        // This method verifies that a particular row is present in the OMS Orders Buy Side Book
        public bool VerifyOMSOrdersBuySideBook(string accountId, string userId, string price, string quantity, string orderType)
        {
            string expectedRow;
            string quantityValue;
            bool flag = false;
            quantityValue = GenericUtils.ConvertToDoubleFormat(Double.Parse(quantity));
            expectedRow = accountId + " || " + userId + " || " + price + " || " + quantityValue + " || " + orderType;
            var buyOrdersList = GetListOfOMSOrdersBuySideBook();
            if (buyOrdersList.Contains(expectedRow))
            {
                flag = true;
            }
            return flag;
        }

        // This method verifies that a particular row is present in the OMS Orders Sell Side Book
        public bool VerifyOMSOrdersSellSideBook(string accountId, string userId, string price, string quantity, string orderType)
        {
            string expectedRow;
            string quantityValue;
            bool flag = false;
            quantityValue = GenericUtils.ConvertToDoubleFormat(Double.Parse(quantity));
            expectedRow = accountId + " || " + userId + " || " + price + " || " + quantityValue + " || " + orderType;
            var sellOrdersList = GetListOfOMSOrdersSellSideBook();
            if (sellOrdersList.Contains(expectedRow))
            {
                flag = true;
            }
            return flag;
        }

        // This method gets a list of orders present in the OMS Orders Buy Side Book
        public ArrayList GetListOfOMSOrdersBuySideBook()
        {
            ArrayList buyOrdersList = new ArrayList();
            int countOfBuySideOrders = driver.FindElements(buySideBookRow).Count;
            for (int i = 1; i <= countOfBuySideOrders; i++)
            {
                String textFinal = "";
                int countOfColumns = driver.FindElements(By.XPath("//section[@class='secondary_container half_container'][1]//div[@class='ReactVirtualized__Grid__innerScrollContainer']/div[" + i + "]/div")).Count;
                for (int j = 2; j <= countOfColumns; j++)
                {
                    String text = driver.FindElement(By.XPath("//section[@class='secondary_container half_container'][1]//div[@class='ReactVirtualized__Grid__innerScrollContainer']/div[" + i + "]/div[" + j + "]")).Text;
                    if (j == 2)
                    {
                        textFinal = text;
                    }
                    else
                    {
                        textFinal = textFinal + " || " + text;
                    }
                }
                buyOrdersList.Add(textFinal);
            }
            return buyOrdersList;
        }

        // This method gets a list of orders present in the OMS Orders Buy Side Book
        public ArrayList GetListOfOMSOrdersSellSideBook()
        {
            ArrayList sellOrdersList = new ArrayList();
            int countOfSellSideOrders = driver.FindElements(sellSideBookRow).Count;
            for (int i = 1; i <= countOfSellSideOrders; i++)
            {
                String textFinal = "";
                int countOfColumns = driver.FindElements(By.XPath("//section[@class='secondary_container half_container'][2]//div[@class='ReactVirtualized__Grid__innerScrollContainer']/div[" + i + "]/div")).Count;
                for (int j = 2; j <= countOfColumns; j++)
                {
                    String text = driver.FindElement(By.XPath("//section[@class='secondary_container half_container'][2]//div[@class='ReactVirtualized__Grid__innerScrollContainer']/div[" + i + "]/div[" + j + "]")).Text;
                    if (j == 2)
                    {
                        textFinal = text;
                    }
                    else
                    {
                        textFinal = textFinal + " || " + text;
                    }

                }
                sellOrdersList.Add(textFinal);
            }
            return sellOrdersList;
        }

        /// <summary>
        ///  Methods related to OMS Oders History tab 
        /// </summary>

        // This selects the instrument on the OMS Open orders page and verifies the related orders in the buy and sell side books
        public bool VerifySelectOMSOrdersHistoryInstrument(string instrument, string numberOfRecords)
        {
            bool orderFoundFlag = false;
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(logger);
            try
            {
                objAdminCommonFunctions.ClickOnOMSOrdersMenuLink();
                ClickOMSOrdersHistoryTab();
                objAdminCommonFunctions.SelectOMSOrdersInstrument(instrument);
                Thread.Sleep(1000);
                orderFoundFlag = ScrollAndVerifyInstrument(instrument, numberOfRecords);
            }
            catch (Exception)
            {
                throw;
            }
            return orderFoundFlag;
        }

        // Users Scroll to verify that the instrument is matching to the instrument selected
        public bool ScrollAndVerifyInstrument(string instrument, string numberOfRecords)
        {
            bool flag = true;
            string instrumentText;
            int totalCount;
            int count;
            Actions actions = new Actions(driver);
            EventFiringWebDriver evw = new EventFiringWebDriver(driver);
            totalCount = Int32.Parse(numberOfRecords);
            count = totalCount / 10;
            for (int j = 1; j <= count; j++)
            {
                for (int i = 1; i <= 10; i++)
                {
                    By instrumentName = By.XPath("//div[@class='ReactVirtualized__Grid__innerScrollContainer']/div[" + i + "]/div[5]");
                    instrumentText = driver.FindElement(instrumentName).Text;
                    if (!(instrumentText.Equals(instrument)))
                    {
                        flag = false;
                    }
                }
                var queryString = "document.querySelector('div.ReactVirtualized__Grid.ReactVirtualized__Table__Grid').scrollTop=";
                evw.ExecuteScript(queryString + (j * 440));
                Thread.Sleep(1000);
            }
            return flag;
        }

        // This method verifies the number of orders displayed on OMS Orders History page
        public bool VerifyNumOfOrdersOnHistorypage(string numberOfRecords, string instrument)
        {
            bool flag = false;
            int totalCount;
            int count;
            int totalCountOfOrders = 0;
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(logger);
            try
            {
                int numberOfOrders = Int32.Parse(numberOfRecords);
                objAdminCommonFunctions.SelectOMSOrdersInstrument(instrument);
                Thread.Sleep(1000);
                // This select the number of orders to be displayed from the orders dropdown
                SelectOMSOrdersInstrument(numberOfRecords);
                Thread.Sleep(1000);
                Actions actions = new Actions(driver);
                EventFiringWebDriver evw = new EventFiringWebDriver(driver);
                totalCount = Int32.Parse(numberOfRecords);
                count = totalCount / 10;
                for (int j = 1; j <= count; j++)
                {
                    for (int i = 1; i <= 10; i++)
                    {
                        totalCountOfOrders++;
                    }
                    var queryString = "document.querySelector('div.ReactVirtualized__Grid.ReactVirtualized__Table__Grid').scrollTop=";
                    evw.ExecuteScript(queryString + (j * 440));
                    Thread.Sleep(1000);
                }
                if ((totalCountOfOrders <= numberOfOrders))
                {
                    flag = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }

        // This method navigates to OMS Orders History page and clears all filters
        public void ClearOrderHistoryFilters(string instrument, string numOfOrders)
        {
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(logger);
            try
            {
                objAdminCommonFunctions.ClickOnOMSOrdersMenuLink();
                ClickOMSOrdersHistoryTab();
                objAdminCommonFunctions.SelectOMSOrdersInstrument(instrument);
                SelectOMSOrdersInstrument(numOfOrders);
                ClickMoreFiltersLink();
                AccountIdTextBox().Clear();
                UserIdTextBox().Clear();
                OrderIdTextBox().Clear();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Add filter by accountId and verify that only the details of the accountId which was passed on filter is displayed in Buy side and Sell side order book 
        public bool VerifySearchOMSOrdersHistoryByAcountId(string instrument, string numOfOrders, string accountId)
        {
            bool accountIdFoundFlag = false;            
            try
            {
                ClearOrderHistoryFilters(instrument, numOfOrders);
                AccountIdTextBox().SendKeys(accountId);
                ClickMoreFiltersLink();
                accountIdFoundFlag = VerifyAccountIdInOrderHistoryBook(accountId, numOfOrders);
            }
            catch (Exception)
            {
                throw;
            }
            return accountIdFoundFlag;
        }

        // This method will check if the accountId is present on the Buy side book or not. Returns false if the accountId is not present
        public bool VerifyAccountIdInOrderHistoryBook(string accountId, string numberOfRecords)
        {
            bool flag = true;
            string accountIdText;
            int totalCount;
            int count;
            Actions actions = new Actions(driver);
            EventFiringWebDriver evw = new EventFiringWebDriver(driver);
            totalCount = Int32.Parse(numberOfRecords);
            count = totalCount / 10;
            for (int j = 1; j <= count; j++)
            {
                for (int i = 1; i <= 10; i++)
                {
                    By accountIdFromUI = By.XPath("//div[@class='ReactVirtualized__Grid__innerScrollContainer']/div[" + i + "]/div[2]");
                    accountIdText = driver.FindElement(accountIdFromUI).Text;
                    if (!(accountIdText.Equals(accountId)))
                    {
                        flag = false;
                    }
                }
                var queryString = "document.querySelector('div.ReactVirtualized__Grid.ReactVirtualized__Table__Grid').scrollTop=";
                evw.ExecuteScript(queryString + (j * 440));
                Thread.Sleep(1000);
            }
            return flag;
        }

        // Add filter by orderId and verify that only the details of the orderId which was passed on filter is displayed in History book
        public bool VerifySearchOMSOrdersHistoryByOrderId(string instrument, string numOfOrders)
        {
            bool accountIdFoundFlag = false;
            string orderId = null;
            try
            {
                ClearOrderHistoryFilters(instrument, numOfOrders);
                orderId = driver.FindElement(By.XPath("//div[@class='ReactVirtualized__Grid__innerScrollContainer']/div[1]/div[1]")).Text;
                OrderIdTextBox().SendKeys(orderId);
                ClickMoreFiltersLink();
                accountIdFoundFlag = VerifyOrderIdInOrderHistoryBook(orderId, numOfOrders);
            }
            catch (Exception)
            {
                throw;
            }
            return accountIdFoundFlag;
        }

        // This method will check if the orderId is present on the Buy side book or not. Returns false if the orderId is not present
        public bool VerifyOrderIdInOrderHistoryBook(string orderId, string numberOfRecords)
        {
            bool flag = true;
            string orderIdText;
            int totalCount;
            int count;
            Actions actions = new Actions(driver);
            EventFiringWebDriver evw = new EventFiringWebDriver(driver);
            totalCount = Int32.Parse(numberOfRecords);
            count = totalCount / 10;
            for (int j = 1; j <= count; j++)
            {
                for (int i = 1; i <= 10; i++)
                {
                    By orderIdFromUI = By.XPath("//div[@class='ReactVirtualized__Grid__innerScrollContainer']/div[" + i + "]/div[1]");
                    orderIdText = driver.FindElement(orderIdFromUI).Text;
                    if (!(orderIdText.Equals(orderId)))
                    {
                        flag = false;
                    }
                }
                var queryString = "document.querySelector('div.ReactVirtualized__Grid.ReactVirtualized__Table__Grid').scrollTop=";
                evw.ExecuteScript(queryString + (j * 440));
                Thread.Sleep(1000);
            }
            return flag;
        }

        // This method searches for Rejected order on History page -> Double clicks to select it and returns the rejected reason
        public bool VerifySearchRejectedOrder(string instrument, string numberOfRecords, string orderState)
        {
            bool flag = false;
            string orderStatusText;
            int totalCount;
            int count;
            Actions actions = new Actions(driver);
            EventFiringWebDriver evw = new EventFiringWebDriver(driver);
            totalCount = Int32.Parse(numberOfRecords);
            count = totalCount / 10;
            for (int j = 1; j <= count; j++)
            {
                for (int i = 1; i <= 10; i++)
                {
                    By orderStatusFromUI = By.XPath("//div[@class='ReactVirtualized__Grid__innerScrollContainer']/div[" + i + "]/div[4]");
                    orderStatusText = driver.FindElement(orderStatusFromUI).Text;
                    IWebElement orderStatusElement = driver.FindElement(By.XPath("//div[@class='ReactVirtualized__Grid__innerScrollContainer']/div[" + i + "]/div[4]"));
                    if ((orderStatusText.Equals(orderState)))
                    {
                        actions.DoubleClick(orderStatusElement).Build().Perform();
                        flag = true;
                        goto Endloop;
                    }
                }
                var queryString = "document.querySelector('div.ReactVirtualized__Grid.ReactVirtualized__Table__Grid').scrollTop=";
                evw.ExecuteScript(queryString + (j * 440));
                Thread.Sleep(1000);
            }
            Endloop:
            if (RejectReasonTitleElement().Text.Equals(Const.RejectReasonTitle))
            {
                flag = true;
                logger.LogCheckPoint(String.Format(LogMessage.OrderRejectedReason, RejectReasonValue().Text));
            }
            return flag;
        }
    }
}
