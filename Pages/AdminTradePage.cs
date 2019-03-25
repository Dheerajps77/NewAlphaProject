using AlphaPoint_QA.Common;
using AlphaPoint_QA.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AlphaPoint_QA.Pages
{
    class AdminTradePage
    {
        ProgressLogger logger;
        static Config data;
        public static IWebDriver driver;

        public AdminTradePage(ProgressLogger logger)
        {
            this.logger = logger;
            data = ConfigManager.Instance;
            driver = AlphaPointWebDriver.GetInstanceOfAlphaPointWebDriver();
        }

        By idTextUnderFilledTab = By.CssSelector("div.flex-table__body.order-history-table__body > div:nth-of-type(1) > div:nth-of-type(1)");
        By pairTextUnderFilledTab = By.CssSelector("div.flex-table__body.order-history-table__body > div:nth-of-type(1) > div:nth-of-type(2)");
        By sideTextUnderFilledTab = By.CssSelector("div.flex-table__body.order-history-table__body > div:nth-of-type(1) > div:nth-of-type(3)");
        By sizeTextUnderFilledTab = By.CssSelector("div.flex-table__body.order-history-table__body > div:nth-of-type(1) > div:nth-of-type(4)");
        By executionIdTextUnderFilledTab = By.CssSelector("div.flex-table__body.order-history-table__body > div:nth-of-type(1) > div:nth-of-type(8)");
        By selectInstrument = By.CssSelector("select[name=InstrumentId]");

        //First row values webelement locator under Trades Tab
        By firstTradeIdTextUnderTradesTab = By.CssSelector("div.ReactVirtualized__Grid__innerScrollContainer div:nth-of-type(1) div.ReactVirtualized__Table__rowColumn:nth-of-type(1)");
        By firstProductPairTextUnderTradesTab = By.CssSelector("div.ReactVirtualized__Grid__innerScrollContainer div:nth-of-type(1) div.ReactVirtualized__Table__rowColumn:nth-of-type(3)");
        By firstQuantityTextUnderTradesTab = By.CssSelector("div.ReactVirtualized__Grid__innerScrollContainer div:nth-of-type(1) div.ReactVirtualized__Table__rowColumn:nth-of-type(4)");
        By firstSizeTextUnderTradesTab = By.CssSelector("div.ReactVirtualized__Grid__innerScrollContainer div:nth-of-type(1) div.ReactVirtualized__Table__rowColumn:nth-of-type(5)");
        By firstExeuctionIdTextUnderTradesTab = By.CssSelector("div.ReactVirtualized__Grid__innerScrollContainer div:nth-of-type(1) div.ReactVirtualized__Table__rowColumn:nth-of-type(2)");
        By firstSideTextUnderTradesTab = By.CssSelector("div.ReactVirtualized__Grid__innerScrollContainer div:nth-of-type(1) div.ReactVirtualized__Table__rowColumn:nth-of-type(8)");


        //Second row values webelement locator under Trades Tab
        By secondTradeIdTextUnderTradesTab = By.CssSelector("div.ReactVirtualized__Grid__innerScrollContainer div:nth-of-type(2) div.ReactVirtualized__Table__rowColumn:nth-of-type(1)");
        By secondProductPairTextUnderTradesTab = By.CssSelector("div.ReactVirtualized__Grid__innerScrollContainer div:nth-of-type(2) div.ReactVirtualized__Table__rowColumn:nth-of-type(3)");
        By secondQuantityTextUnderTradesTab = By.CssSelector("div.ReactVirtualized__Grid__innerScrollContainer div:nth-of-type(2) div.ReactVirtualized__Table__rowColumn:nth-of-type(4)");
        By secondSizeTextUnderTradesTab = By.CssSelector("div.ReactVirtualized__Grid__innerScrollContainer div:nth-of-type(2) div.ReactVirtualized__Table__rowColumn:nth-of-type(5)");
        By secondExeuctionIdTextUnderTradesTab = By.CssSelector("div.ReactVirtualized__Grid__innerScrollContainer div:nth-of-type(2) div.ReactVirtualized__Table__rowColumn:nth-of-type(2)");
        By secondSideTextUnderTradesTab = By.CssSelector("div.ReactVirtualized__Grid__innerScrollContainer div:nth-of-type(2) div.ReactVirtualized__Table__rowColumn:nth-of-type(8)");
        By executionIdButton = By.CssSelector("div[aria-label='Execution Id']");

        By accountIdTextField = By.CssSelector("input[name=AccountId]");
        By userIdTextField = By.CssSelector("input[name=UserId]");
        By tradeIdTextField = By.CssSelector("input[name=TradeId]");
        By executionIdTextField = By.CssSelector("input[name=ExecutionId]");
        By showSearchButton = By.XPath("//span[text()='Show Search']");
        By hideSearchButton = By.XPath("//span[text()='Hide Search']");

        public IWebElement ShowSearchButton()
        {
            return driver.FindElement(showSearchButton);
        }

        public IWebElement HideSearchButton()
        {
            return driver.FindElement(hideSearchButton);
        }


        public IWebElement AccountIdTextField()
        {
            return driver.FindElement(accountIdTextField);
        }

        public IWebElement UserIdTextField()
        {
            return driver.FindElement(userIdTextField);
        }

        public IWebElement TradeIdTextField()
        {
            return driver.FindElement(tradeIdTextField);
        }

        public IWebElement ExecutionIdTextField()
        {
            return driver.FindElement(executionIdTextField);
        }

        // This method will click on "Show Search" button
        public void ClickOnShowSearchButton()
        {
            try
            {
                GenericUtils.WaitForElementClickable(driver, showSearchButton, 15).Click();
            }
            catch(Exception)
            {
                throw;
            }
        }

        // This method will click on "Hide Search" button
        public void ClickOnHideSearchButton()
        {
            try
            {
                GenericUtils.WaitForElementClickable(driver, hideSearchButton, 15).Click();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // This method will enter accountId in textfield
        public void EnterAccountId(string accountId)
        {
            try
            {
                UserSetFunctions.EnterText(AccountIdTextField(), accountId);
            }
            catch(Exception)
            {
                throw;
            }
        }

        // This method will enter userId in textfield
        public void EnterUserId(string userId)
        {
            try
            {
                UserSetFunctions.EnterText(UserIdTextField(), userId);
            }
            catch (Exception)
            {
                throw;
            }
        }


        // This method will enter tradeId in textfield
        public void EnterTradeId(string tradeId)
        {
            try
            {
                UserSetFunctions.EnterText(TradeIdTextField(), tradeId);
            }
            catch (Exception)
            {
                throw;
            }
        }


        // This method will enter executionId in textfield
        public void EnterExecutionId(string executionId)
        {
            try
            {
                UserSetFunctions.EnterText(ExecutionIdTextField(), executionId);
            }
            catch (Exception)
            {
                throw;
            }
        }


        By searchButton = By.CssSelector("button#SearchTrades");

        // This method will click on "Search" button
        public void ClickOnSearchButton()
        {
            try
            {
                GenericUtils.WaitForElementClickable(driver, searchButton, 15).Click();
            }
            catch(Exception)
            {
                throw;
            }
        }

        By countofTradeItems = By.CssSelector("div.ReactVirtualized__Grid__innerScrollContainer div.ReactVirtualized__Table__row");


        // This method will return the total count number of trade items
        public int totalCountOfTradeItems()
        {
            return driver.FindElements(countofTradeItems).Count;
        }

        // This method with will verify the AccountId textfield
        public bool VerifySearchByAccountId(string accountId)
        {

            bool flag = false;
            int totalCountOfAccountItemsBeforeSearch;
            int totalCountOfAccountItemsAfterSearch;
            try
            {
                totalCountOfAccountItemsBeforeSearch = totalCountOfTradeItems();
                ClickOnShowSearchButton();
                EnterAccountId(accountId);               
                ClickOnSearchButton();
                Thread.Sleep(2000);
                totalCountOfAccountItemsAfterSearch = totalCountOfTradeItems();

                if(!totalCountOfAccountItemsBeforeSearch.Equals(totalCountOfAccountItemsAfterSearch))
                {
                    flag = true;
                }

                if(flag)
                {
                    logger.LogCheckPoint(String.Format(LogMessage.VerifySearchByAccountIdPassed, accountId));
                }
                else
                {
                    logger.LogCheckPoint(String.Format(LogMessage.VerifySearchByAccountIdFailed, accountId));
                }
                UserSetFunctions.Clear(AccountIdTextField());
                driver.Navigate().Refresh();
                Thread.Sleep(1000);
                ClickOnShowSearchButton();

            }            
            catch(Exception)
            {
                throw;
            }
            return flag;
        }


        // This method with will verify the UserId textfield
        public bool VerifySearchByUserId(string userId)
        {
            bool flag = false;
            int totalCountOfAccountItemsBeforeSearch;
            int totalCountOfAccountItemsAfterSearch;
            try
            {
                totalCountOfAccountItemsBeforeSearch = totalCountOfTradeItems();
                ClickOnShowSearchButton();
                EnterUserId(userId);
                ClickOnSearchButton();
                Thread.Sleep(2000);
                totalCountOfAccountItemsAfterSearch = totalCountOfTradeItems();
                if (!totalCountOfAccountItemsBeforeSearch.Equals(totalCountOfAccountItemsAfterSearch))
                {
                    flag = true;
                }

                if (flag)
                {
                    logger.LogCheckPoint(String.Format(LogMessage.VerifySearchByUserIdPassed, userId));
                }
                else
                {
                    logger.LogCheckPoint(String.Format(LogMessage.VerifySearchByUserIdFailed, userId));
                }
                UserSetFunctions.Clear(AccountIdTextField());
                driver.Navigate().Refresh();
                Thread.Sleep(1000);
                ClickOnShowSearchButton();

            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }

        public IWebElement ClickOnExecutionIdButton()
        {
            return driver.FindElement(executionIdButton);
        }
        public IWebElement FirstTradeIdTextUnderTradesTab()
        {
            return driver.FindElement(firstTradeIdTextUnderTradesTab);
        }

        public IWebElement FirstProductPairTextUnderTradesTab()
        {
            return driver.FindElement(firstProductPairTextUnderTradesTab);
        }

        public IWebElement FirstQuantityTextUnderTradesTab()
        {
            return driver.FindElement(firstQuantityTextUnderTradesTab);
        }

        public IWebElement FirstSizeTextUnderTradesTab()
        {
            return driver.FindElement(firstSizeTextUnderTradesTab);
        }

        public IWebElement FirstExeuctionIdTextUnderTradesTab()
        {
            return driver.FindElement(firstExeuctionIdTextUnderTradesTab);
        }

        public IWebElement FirstSideTextUnderTradesTab()
        {
            return driver.FindElement(firstSideTextUnderTradesTab);
        }

        public IWebElement SecondTradeIdTextUnderTradesTab()
        {
            return driver.FindElement(secondTradeIdTextUnderTradesTab);
        }

        public IWebElement SecondProductPairTextUnderTradesTab()
        {
            return driver.FindElement(secondProductPairTextUnderTradesTab);
        }

        public IWebElement SecondQuantityTextUnderTradesTab()
        {
            return driver.FindElement(secondQuantityTextUnderTradesTab);
        }

        public IWebElement SecondSizeTextUnderTradesTab()
        {
            return driver.FindElement(secondSizeTextUnderTradesTab);
        }

        public IWebElement SecondExeuctionIdTextUnderTradesTab()
        {
            return driver.FindElement(secondExeuctionIdTextUnderTradesTab);
        }

        public IWebElement SecondSideTextUnderTradesTab()
        {
            return driver.FindElement(secondSideTextUnderTradesTab);
        }

        public IWebElement IdTextUnderFilledTab()
        {
            return driver.FindElement(idTextUnderFilledTab);
        }

        public IWebElement PairTextUnderFilledTab()
        {
            return driver.FindElement(pairTextUnderFilledTab);
        }

        public IWebElement SideTextUnderFilledTab()
        {
            return driver.FindElement(sideTextUnderFilledTab);
        }

        public IWebElement SizeTextUnderFilledTab()
        {
            return driver.FindElement(sizeTextUnderFilledTab);
        }

        public IWebElement ExecutionIdTextUnderFilledTab()
        {
            return driver.FindElement(executionIdTextUnderFilledTab);
        }

        public void SelectInstrumentInTradeTab()
        {
            try
            {
                GenericUtils.SelectDropDownByText(driver, selectInstrument, "BTCUSD");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SelectOrdersInTradeTab()
        {
            try
            {
                GenericUtils.SelectDropDownByText(driver, selectInstrument, "BTCUSD");
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This method will hold the values from filled order tab
        public Dictionary<string, string> TradeReportOrderValueFromFilledOrderTab()
        {
            string idValueText;
            string pairValueText;
            string sideValueText;
            string sizeValueText;
            string executionIdValueText;
            string idValueTextKey="";
            string pairValueTextKey="";
            string sideValueTextKey="";
            string sizeValueTextKey="";
            string executionIdValueTextKey="";

            try
            {
                Dictionary<string, string> placedOrderUnderFilledOrderTab = new Dictionary<string, string>();

                idValueText = IdTextUnderFilledTab().Text;
                pairValueText = PairTextUnderFilledTab().Text;
                sideValueText = SideTextUnderFilledTab().Text;
                sizeValueText = SizeTextUnderFilledTab().Text;
                executionIdValueText = ExecutionIdTextUnderFilledTab().Text;

                placedOrderUnderFilledOrderTab.Add(idValueTextKey, idValueText);
                placedOrderUnderFilledOrderTab.Add(pairValueTextKey, pairValueText);
                placedOrderUnderFilledOrderTab.Add(sideValueTextKey, sideValueText);
                placedOrderUnderFilledOrderTab.Add(sizeValueTextKey, sizeValueText);
                placedOrderUnderFilledOrderTab.Add(executionIdValueTextKey, executionIdValueText);

                return placedOrderUnderFilledOrderTab;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This method will hold the values under Trades order tab
        public Dictionary<string, string> FirstRowPlacedOrderValueUnderTradesTab()
        {
            string idValueText;
            string pairValueText;
            string sizeValueText;
            string executionIdValueText;
            string priceValueTextKey;
            string sideTextKey;

            try
            {
                Dictionary<string, string> placedOrderedUnderTradeTab = new Dictionary<string, string>();

                idValueText = FirstTradeIdTextUnderTradesTab().Text;
                pairValueText = FirstProductPairTextUnderTradesTab().Text;
                priceValueTextKey = FirstQuantityTextUnderTradesTab().Text;
                sizeValueText = FirstSizeTextUnderTradesTab().Text;
                executionIdValueText = FirstExeuctionIdTextUnderTradesTab().Text;
                sideTextKey = FirstSideTextUnderTradesTab().Text;

                placedOrderedUnderTradeTab.Add(Const.idValueTextKey, idValueText);
                placedOrderedUnderTradeTab.Add(Const.executionIdValueTextKey, executionIdValueText);
                placedOrderedUnderTradeTab.Add(Const.pairValueTextKey, pairValueText);
                placedOrderedUnderTradeTab.Add(Const.sizeValueTextKey, priceValueTextKey);
                placedOrderedUnderTradeTab.Add(Const.priceTextKey, sizeValueText);
                placedOrderedUnderTradeTab.Add(Const.sideValueTextKey, sideTextKey);

                return placedOrderedUnderTradeTab;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This method will hold the values under Trades order tab
        public Dictionary<string, string> SecondRowPlacedOrderValueUnderTradesTab()
        {
            string idValueText;
            string pairValueText;
            string sizeValueText;
            string executionIdValueText;
            string priceValueTextKey;
            string sideTextKey;

            try
            {
                Dictionary<string, string> placedOrderedUnderTradeTab = new Dictionary<string, string>();

                idValueText = SecondTradeIdTextUnderTradesTab().Text;
                pairValueText = SecondProductPairTextUnderTradesTab().Text;
                priceValueTextKey = SecondQuantityTextUnderTradesTab().Text;
                sizeValueText = SecondSizeTextUnderTradesTab().Text;
                executionIdValueText = SecondExeuctionIdTextUnderTradesTab().Text;
                sideTextKey = SecondSideTextUnderTradesTab().Text;

                placedOrderedUnderTradeTab.Add(Const.idValueTextKey, idValueText);
                placedOrderedUnderTradeTab.Add(Const.executionIdValueTextKey, executionIdValueText);
                placedOrderedUnderTradeTab.Add(Const.pairValueTextKey, pairValueText);
                placedOrderedUnderTradeTab.Add(Const.sizeValueTextKey, priceValueTextKey);
                placedOrderedUnderTradeTab.Add(Const.priceTextKey, sizeValueText);
                placedOrderedUnderTradeTab.Add(Const.sideValueTextKey, sideTextKey);

                return placedOrderedUnderTradeTab;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool VerifyTradeOrderUnderTradesTab()
        {
            bool flag = false;
            try
            {
                //UserCommonFunctions.FilledOrderTab(driver);
                //var dictValues = TradeReportOrderValue();

                SelectInstrumentInTradeTab();
                GenericUtils.WaitForElementClickable(driver, executionIdButton, 15).Click();
                var FirstRowOrderedValues = FirstRowPlacedOrderValueUnderTradesTab();
                var SecondRowOrderedValues = SecondRowPlacedOrderValueUnderTradesTab();

            }
            catch(Exception)
            {
                throw;
            }
            return flag;
        }
    }

}
