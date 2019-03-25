using AlphaPoint_QA.Common;
using AlphaPoint_QA.Utils;
using OpenQA.Selenium;
using System;
using System.Collections;
using System.Threading;
using Xunit;

namespace AlphaPoint_QA.Pages
{
    class DetailsOnLandingPage
    {
        ProgressLogger logger;
        static Config data;
        public static IWebDriver driver;

        public DetailsOnLandingPage(ProgressLogger logger)
        {
            this.logger = logger;
            data = ConfigManager.Instance;
            driver = AlphaPointWebDriver.GetInstanceOfAlphaPointWebDriver();
        }

        By exchangeLink = By.CssSelector("div.page-header-nav__menu-toggle");
        By orderBook = By.CssSelector("div.flex-table.orderbook div.flex-table__title");
        By openOrder = By.CssSelector("div.ap-tab__menu.order-history__menu div[data-test='Open Orders']");
        By filledOrder = By.CssSelector("div.ap-tab__menu.order-history__menu div[data-test='Filled Orders']");
        By inactiveOrder = By.CssSelector("div.ap-tab__menu.order-history__menu div[data-test='Inactive Orders']");
        By tradeReportsOrder = By.CssSelector("div.ap-tab__menu.order-history__menu div[data-test='Trade Reports']");
        By depositStatus = By.CssSelector("div.ap-tab__menu.order-history__menu div[data-test='Deposit Status']");
        By withdrawStatus = By.CssSelector("div.ap-tab__menu.order-history__menu div[data-test='Withdraw Status']");
        By countOfOrderHistoryList = By.CssSelector("div.ap-tab__menu.order-history__menu > div");
        By countOfOrderHistoryMenuLabelTextList = By.CssSelector("div.flex-table__header.order-history-table__table-header > div > div");
        By priceChartValue = By.CssSelector("div.tv-chart__header");
        By orderEntryButton = By.CssSelector("div[data-test='Order Entry']");
        By buyOrderEntryButton = By.CssSelector("label[data-test='Buy Side']");
        By sellOrderEntryButton = By.CssSelector("label[data-test='Sell Side']");
        By balancesButton = By.CssSelector("div[data-test=Balances]");
        By availableBalanceText = By.CssSelector("div.trade-component.instrument-positions-summary div > div:nth-of-type(2) div:nth-of-type(1)");
        By holdText = By.CssSelector("div.trade-component.instrument-positions-summary div > div:nth-of-type(3) div:nth-of-type(1)");
        By pendingDepositsText = By.CssSelector("div.trade-component.instrument-positions-summary div > div:nth-of-type(4) div:nth-of-type(1)");
        By totalBalanceText = By.CssSelector("div.trade-component.instrument-positions-summary div > div:nth-of-type(5) div:nth-of-type(1)");
        By countOfOrderBookMenuList = By.CssSelector("div.flex-table__header.flex-table__header--background.orderbook__header>div>div");
        By countOfRecentTradeMenuList = By.CssSelector("div.flex-table__column.recent-trades__header-column>div");
        By countOfOrderEntryMenuList = By.CssSelector("div.btn-group.btn-group-toggle.ap-radio-tab.order-entry__rt-wrapper>label");
        By countOfOrderEntrySubMenuList = By.CssSelector("div.btn-group.btn-group-toggle.ap-segmented-button.order-entry__rt-wrapper>label");
        By cancelSellButton = By.CssSelector("button.ap-inline-btn__btn.ap-inline-btn__btn--subtractive.bulk-cancel-buttons__btn.bulk-cancel-buttons__btn--subtractive:nth-of-type(1)");
        By cancelbuyButton = By.CssSelector("button.ap-inline-btn__btn.ap-inline-btn__btn--subtractive.bulk-cancel-buttons__btn.bulk-cancel-buttons__btn--subtractive:nth-of-type(2)");
        By cancelAllButton = By.CssSelector("button.ap-inline-btn__btn.ap-inline-btn__btn--subtractive.bulk-cancel-buttons__btn.bulk-cancel-buttons__btn--subtractive:nth-of-type(3)");
        By advancedOrderLink = By.CssSelector("div.order-entry__item-button");
        By intervalsToolButton = By.CssSelector("div#header-toolbar-intervals");
        By indicatorToolButton = By.CssSelector("div#header-toolbar-indicators");
        By fullscreenModeToolButton = By.CssSelector("div:nth-of-type(1) > div:nth-of-type(2) > div > div > div:nth-of-type(1) > div > div > div > div > div:nth-of-type(3) > div");
        By priceChartFrame = By.CssSelector("div#trading-view-chart iframe");
        By iconHideButton = By.CssSelector("div.pane-legend-line.pane-legend-wrap.study a:nth-of-type(1)");
        By iconFormatButton = By.CssSelector("div.pane-legend-line.pane-legend-wrap.study a:nth-of-type(2)");
        By iconDeleteButton = By.CssSelector("div.pane-legend-line.pane-legend-wrap.study a:nth-of-type(3)");

        public IWebElement IconHideButton()
        {
            return driver.FindElement(iconHideButton);
        }

        public IWebElement IconFormatButton()
        {
            return driver.FindElement(iconFormatButton);
        }

        public IWebElement IconDeleteButton()
        {
            return driver.FindElement(iconDeleteButton);
        }

        public IWebElement IntervalsToolButton()
        {
            return driver.FindElement(intervalsToolButton);
        }

        public IWebElement IndicatorToolButton()
        {
            return driver.FindElement(indicatorToolButton);
        }

        public IWebElement FullscreenModeToolButton()
        {
            return driver.FindElement(fullscreenModeToolButton);
        }

        public IWebElement CancelSellButton()
        {
            return driver.FindElement(cancelSellButton);
        }
        public IWebElement AdvancedOrderLink()
        {
            return driver.FindElement(advancedOrderLink);
        }

        public IWebElement CancelbuyButton()
        {
            return driver.FindElement(cancelbuyButton);
        }

        public IWebElement CancelAllButton()
        {
            return driver.FindElement(cancelAllButton);
        }
       
        public IWebElement BalancesButton()
        {
            return driver.FindElement(balancesButton);
        }
        
        public IWebElement AvailableBalanceText()
        {
            return driver.FindElement(availableBalanceText);
        }

        public IWebElement HoldText()
        {
            return driver.FindElement(holdText);
        }

        public IWebElement PendingDepositsText()
        {
            return driver.FindElement(pendingDepositsText);
        }

        public IWebElement TotalBalanceText()
        {
            return driver.FindElement(totalBalanceText);
        }

        public IWebElement OrderEntryButton()
        {
            return driver.FindElement(orderEntryButton);
        }

        public IWebElement BuyOrderEntryButton()
        {
            Thread.Sleep(2000);
            return driver.FindElement(buyOrderEntryButton);
        }

        public IWebElement SellOrderEntryButton()
        {
            Thread.Sleep(2000);
            return driver.FindElement(sellOrderEntryButton);
        }

        public IWebElement PriceChartValue()
        {
            return driver.FindElement(priceChartValue);
        }
        
        public IWebElement OpenOrder()
        {
            return driver.FindElement(openOrder);
        }
        public IWebElement FilledOrder()
        {
            return driver.FindElement(filledOrder);
        }
        public IWebElement InactiveOrder()
        {
            return driver.FindElement(inactiveOrder);
        }
        public IWebElement TradeReportsOrder()
        {
            return driver.FindElement(tradeReportsOrder);
        }
        public IWebElement DepositStatus()
        {
            return driver.FindElement(depositStatus);
        }

        public IWebElement WithdrawStatus()
        {
            return driver.FindElement(withdrawStatus);
        }

        public IWebElement ExchangeLink()
        {
            return driver.FindElement(exchangeLink);
        }

        public IWebElement OrderBook()
        {
            return driver.FindElement(orderBook);
        }


        // This method will click on order entry button
        public void OrderEntryBtn()
        {
            try
            {
                Thread.Sleep(2000);
                UserSetFunctions.Click(OrderEntryButton());
            }
            catch(Exception)
            {
                throw;
            }
        }

        // This method will verify various option in the price chart
        public bool VariouOptionInPriceChart()
        {
            bool flag = false;
            try
            {
                flag = true;
                driver.SwitchTo().Frame(driver.FindElement(priceChartFrame));                                           
                Thread.Sleep(2000);
                if(IndicatorToolButton().Enabled && FullscreenModeToolButton().Enabled && IconHideButton().Enabled && IconFormatButton().Enabled && IconDeleteButton().Enabled)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedVariousOptionInPriceChartPassed));
                }
                driver.SwitchTo().ParentFrame();
            }
                catch (Exception)
            {
                logger.LogCheckPoint(string.Format(LogMessage.VerifiedVariousOptionInPriceChartFailed));
                throw;
            }
            return flag;
        }

        // This method will verify various option cancell Sell, Buy, All button in order book section
        public bool FieldsInOrderBookSection()
        {
            bool flag = false;
            try
            {
                flag = true;
                if (CancelSellButton().Displayed && CancelbuyButton().Displayed && CancelAllButton().Displayed)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.cancellSellBuyAllPassed));
                }
            }
            catch (Exception)
            {
                logger.LogCheckPoint(string.Format(LogMessage.cancellSellBuyAllFailed));
                throw;
            }
            return flag;
        }

        // This method will verify if advance order Entry button is present in the page
        public bool AdvanceOrderBtn()
        {
            bool flag = false;
            try
            {
                flag = true;
                if (AdvancedOrderLink().Enabled)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedAdvanceOrderButtonPassed));
                }
            }
            catch (Exception)
            {
                logger.LogCheckPoint(string.Format(LogMessage.VerifiedAdvanceOrderButtonFailed));
                throw;
            }
            return flag;
        }

        // This method will verify if order Entry button is present in the page
        public bool OrderEntryButn()
        {
            bool flag = false;
            try
            {
                flag = true;
                if (OrderEntryButton().Enabled)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedOrderEntryButtonPassed));
                }
            }
            catch (Exception)
            {
                logger.LogCheckPoint(string.Format(LogMessage.VerifiedOrderEntryButtonFailed));
                throw;
            }
            return flag;
        }

        // This method will verify if balances button is present in the page
        public bool VerifyBalancesButton()
        {
            bool flag = false;
            try
            {
                flag = true;
                if (BalancesButton().Enabled)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedBalancesButtonPassed));
                }
            }
            catch (Exception)
            {
                logger.LogCheckPoint(string.Format(LogMessage.VerifiedBalancesButtonFailed));
                throw;
            }
            return flag;
        }

        // This method will click on balances button
        public void ClickOnBalancesButton()
        {
            try
            {
                Thread.Sleep(2000);
                UserSetFunctions.Click(BalancesButton());
            }
            catch (Exception)
            {
                throw;
            }
        }

        // This method will verify available balance under Balances tab
        public bool AvailableBalanceTxt(string availableBalanceValue)
        {
            string exepctedAvailableBalanceTextValue = AvailableBalanceText().Text;
            bool flag = false;
            try
            {
                flag = true;
                if (availableBalanceValue.Equals(exepctedAvailableBalanceTextValue))
                {
                    logger.LogCheckPoint(string.Format(LogMessage.availableBalancePassed));
                }
            }
            catch (Exception)
            {
                logger.LogCheckPoint(string.Format(LogMessage.availableBalanceFailed));
                throw;
            }
            return flag;
        }

        // This method will verify hold under Balances tab
        public bool HoldTxt(string holdValue)
        {
            string exepctedHoldTextValue = HoldText().Text;
            bool flag = false;
            try
            {
                flag = true;
                if (holdValue.Equals(exepctedHoldTextValue))
                {
                    logger.LogCheckPoint(string.Format(LogMessage.HoldPassed));
                }
            }
            catch (Exception)
            {
                logger.LogCheckPoint(string.Format(LogMessage.HoldFailed));
                throw;
            }
            return flag;
        }

        // This method will verify pending deposits under Balances tab
        public bool PendingDepositsTxt(string pendingDepositsValue)
        {
            string exepctedPendingDepositsTextValue = PendingDepositsText().Text;
            bool flag = false;
            try
            {
                flag = true;
                if (pendingDepositsValue.Equals(exepctedPendingDepositsTextValue))
                {
                    logger.LogCheckPoint(string.Format(LogMessage.PendingDespositsPassed));
                }
            }
            catch (Exception)
            {
                logger.LogCheckPoint(string.Format(LogMessage.PendingDespositsFailed));
                throw;
            }
            return flag;
        }

        // This method will verify pending deposits under Balances tab
        public bool TotalBalanceTxt(string totalBalanceValue)
        {
            string exepctedTotalBalanceTextValue = TotalBalanceText().Text;
            bool flag = false;
            try
            {
                flag = true;
                if (totalBalanceValue.Equals(exepctedTotalBalanceTextValue))
                {
                    logger.LogCheckPoint(string.Format(LogMessage.TotalBalancePassed));
                }
            }
            catch (Exception)
            {
                logger.LogCheckPoint(string.Format(LogMessage.TotalBalanceFailed));
                throw;
            }
            return flag;
        }

        // This method will verify price Chart
        public bool PriceChartTxt(string priceChartValue)
        {
            string exepctedPriceChartTextValue = PriceChartValue().Text;
            bool flag = false;
            try
            {
                flag = true;
                if (priceChartValue.Equals(exepctedPriceChartTextValue))
                {
                    logger.LogCheckPoint(string.Format(LogMessage.PriceChartPassed));
                }
            }
            catch (Exception)
            {
                logger.LogCheckPoint(string.Format(LogMessage.PriceChartFailed));
                throw;
            }
            return flag;
        }

        // This method will return the order entry text
        public string OrderEntryButtonText()
        {
            try
            {
                return OrderEntryButton().Text;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // This method will return the buy order entry text
        public string BuyOrderEntryButtonText()
        {
            try
            {
                return BuyOrderEntryButton().Text;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // This method will return the sell order entry text
        public string SellOrderEntryButtonText()
        {
            try
            {
                return SellOrderEntryButton().Text;
            }
            catch (Exception)
            {
                throw;
            }
        }        

        //This method will return the price Chart text value
        public string PriceChartTextValue()
        {
            try
            {
                return PriceChartValue().Text;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // This method will return the open order tab text value
        public string OpenOrderText()
        {
            try
            {
                return OpenOrder().Text;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // This method will return the filled order tab text value
        public string FilledOrderText()
        {
            try
            {
                return FilledOrder().Text;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // This method will return the order book text value
        public string OrderBookText()
        {
            try
            {
                return OrderBook().Text;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // This method will return the inactive order tab text value
        public string InactiveOrderText()
        {
            try
            {
                return InactiveOrder().Text;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // This method will return the trade report tab text value
        public string TradeReportsOrderText()
        {
            try
            {
                return TradeReportsOrder().Text;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // This method will return the deposit status tab text value
        public string DepositStatusText()
        {
            try
            {
                return DepositStatus().Text;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // This method will return the withdraw status tab text value
        public string WithdrawStatusText()
        {
            try
            {
                return WithdrawStatus().Text;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // This method will verify if exchange button link is enabled or disabled
        public bool ExchangeLinkButton()
        {
            bool flag = false;
            try
            {
                flag = true;
                // Verify Exchange Menu
                if (ExchangeLink().Displayed)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.ExchangeMenuPassed));
                }
            }
            catch (Exception)
            {
                logger.LogCheckPoint(string.Format(LogMessage.ExchangeMenuFailed));
                throw;
            }
            return flag;
        }

        // This method will verify if order entry button with buy is enabled or disabled
        public bool OrderEntryWithBuyOption()
        {
            bool flag = false;
            try
            {
                flag = true;
                if (OrderEntryButton().Displayed && BuyOrderEntryButton().Displayed)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.OrderEntryWithBuyOptionPassed));
                }
            }
            catch (Exception)
            {
                logger.LogCheckPoint(string.Format(LogMessage.OrderEntryWithBuyOptionFailed));
                throw;
            }
            return flag;
        }

        // This method will verify if order entry button with sell is enabled or disabled
        public bool OrderEntryWithSellOption()
        {
            bool flag = false;
            try
            {
                flag = true;
                if (OrderEntryButton().Displayed && SellOrderEntryButton().Displayed)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.OrderEntryWithSellOptionPassed));
                }
            }
            catch (Exception)
            {
                logger.LogCheckPoint(string.Format(LogMessage.OrderEntryWithSellOptionFailed));
                throw;
            }
            return flag;
        }


        // This will return the total count of list menu tab which is present in order book
        public int CountOfOrderBookMenuList()
        {
            return driver.FindElements(countOfOrderBookMenuList).Count;
        }

        // This method will return the total count of order history tab column value
        public int CountOfOrderHistoryList()
        {
            return driver.FindElements(countOfOrderHistoryList).Count;
        }

        // This method will return the total count of order entry menu tabs value
        public int CountOfOrderEntryMenuList()
        {
            return driver.FindElements(countOfOrderEntryMenuList).Count;
        }

        // This method will return the total count of order entry  sub menu tabs value
        public int CountOfOrderEntrySubMenuList()
        {
            return driver.FindElements(countOfOrderEntrySubMenuList).Count;
        }

        // This method will return the count of order entry menu lists
        public ArrayList GetOrderEntryMenuList()
        {
            try
            {
                ArrayList ordersEntryList = new ArrayList();
                int countOfOrderEntryMenuList = CountOfOrderEntryMenuList();
                for (int i = 1; i <= countOfOrderEntryMenuList; i++)
                {
                    String textFinal = Const.RemoveWhiteSpace;
                    textFinal = driver.FindElement(By.XPath("(//div[@class='btn-group btn-group-toggle ap-radio-tab order-entry__rt-wrapper']/label)[" + i + "]")).Text;
                    ordersEntryList.Add(textFinal);
                }
                return ordersEntryList;
            }
            catch (Exception)
            {

                throw;
            }
        }

        // This method will return the count of order entry sub menu lists
        public ArrayList GetOrderEntrySubMenuList()
        {
            try
            {
                ArrayList ordersEntryList = new ArrayList();
                int countOfOrderEntrySubMenuList = CountOfOrderEntrySubMenuList();
                for (int i = 1; i <= countOfOrderEntrySubMenuList; i++)
                {
                    String textFinal = Const.RemoveWhiteSpace;
                    textFinal = driver.FindElement(By.XPath("(//div[@class='btn-group btn-group-toggle ap-segmented-button order-entry__rt-wrapper']/label)[" + i + "]")).Text;
                    ordersEntryList.Add(textFinal);
                }
                return ordersEntryList;
            }
            catch (Exception)
            {

                throw;
            }
        }

        // This method verify the open orders name label and their child menu label name
        public bool VerifyBuyOrderEntryMenuAndSubMenuTab(string buy, string market, string limit, string stop)
        {
            try
            {
                var flag = false;                
                ArrayList expectedRow = new ArrayList();
                expectedRow.Add(market);
                expectedRow.Add(limit);
                expectedRow.Add(stop);                

                var listOfOrderEntryMenu = GetOrderEntryMenuList();
                UserSetFunctions.Click(BuyOrderEntryButton());
                var listOfOrderyEntryRowSubMenu = GetOrderEntrySubMenuList();
                
                if (listOfOrderEntryMenu.Contains(buy))
                {
                    flag = true;
                    try
                    {
                        logger.LogCheckPoint(string.Format(LogMessage.BuyMenuOrderEntryPassed, buy));
                    }
                    catch (Exception)
                    {
                        logger.LogCheckPoint(string.Format(LogMessage.BuyMenuOrderEntryFailed, buy));
                    }
                    Assert.Equal(expectedRow, listOfOrderyEntryRowSubMenu);
                    logger.LogCheckPoint(string.Format(LogMessage.BuySubMenuOrderEntryPassed, market, limit, stop, buy));
                    return flag;
                }
                return flag;
            }
            catch (Exception ex)
            {
                logger.LogCheckPoint(string.Format(LogMessage.BuySubMenuOrderEntryFailed, market, limit, stop, buy));
                logger.LogCheckPoint(ex.Message + ex.StackTrace);
                throw;
            }
        }

        // This method will return the total count of row menu label text value of order history tab
        public int CountOfOrderHistoryMenuLabelTextList()
        {
            return driver.FindElements(countOfOrderHistoryMenuLabelTextList).Count;
        }
              
        // This method will return the order book menu tab label name
        public ArrayList GetOrderBookMenuList()
        {
            try
            {
                ArrayList orderBookList = new ArrayList();
                int countOfOrderBookList = CountOfOrderBookMenuList();
                for (int i = 1; i <= countOfOrderBookList; i++)
                {
                    String textFinal = Const.RemoveWhiteSpace;
                    textFinal = driver.FindElement(By.XPath("(//div[@class='flex-table__header flex-table__header--background orderbook__header']//div//div)[" + i + "]")).Text;
                    orderBookList.Add(textFinal);
                }
                return orderBookList;
            }
            catch (Exception)
            {

                throw;
            }
        }

        // This will return the total count of list menu tab which is present in recent trade
        public int CountOfRecentTradeMenuList()
        {
            return driver.FindElements(countOfRecentTradeMenuList).Count;
        }

        // This method will return the recent trade menu tab label name
        public ArrayList GetRecentTradeMenuList()
        {
            try
            {
                ArrayList RecetTradeList = new ArrayList();
                int countOfRecentTradeList = CountOfRecentTradeMenuList();
                for (int i = 1; i <= countOfRecentTradeList; i++)
                {
                    String textFinal = Const.RemoveWhiteSpace;
                    textFinal = driver.FindElement(By.XPath("(//div[@class='flex-table__title recent-trades__table-title']//following::div//div[@class='flex-table__column recent-trades__header-column']//div)[" + i + "]")).Text;
                    RecetTradeList.Add(textFinal);
                }
                return RecetTradeList;
            }
            catch (Exception)
            {

                throw;
            }
        }

        // This method verify the open orders name label and their child menu label name
        public bool VerifySellOrderEntryMenuAndSubMenuTab(string sell, string market, string limit, string stop)
        {
            try
            {
                var flag = false;
                ArrayList expectedRow = new ArrayList();
                expectedRow.Add(market);
                expectedRow.Add(limit);
                expectedRow.Add(stop);

                var listOfOrderEntryMenu = GetOrderEntryMenuList();
                UserSetFunctions.Click(SellOrderEntryButton());
                var listOfOrderyEntryRowSubMenu = GetOrderEntrySubMenuList();

                if (listOfOrderEntryMenu.Contains(sell))
                {
                    flag = true;
                    try
                    {
                        logger.LogCheckPoint(string.Format(LogMessage.SellMenuOrderEntryPassed, sell));
                    }
                    catch (Exception)
                    {
                        logger.LogCheckPoint(string.Format(LogMessage.SellMenuOrderEntryFailed, sell));
                    }
                    Assert.Equal(expectedRow, listOfOrderyEntryRowSubMenu);
                    logger.LogCheckPoint(string.Format(LogMessage.SellSubMenuOrderEntryPassed, market, limit, stop, sell));
                    return flag;
                }
                return flag;
            }
            catch (Exception )
            {
                logger.LogCheckPoint(string.Format(LogMessage.SellSubMenuOrderEntryFailed, market, limit, stop, sell));
                throw;
            }
        }

        // This method will verify the details of order book
        public bool VerifyOrderBookMenuTab(string price, string qty, string mySize)
        {
            bool flag = false;
            try
            {
                flag = true;
                ArrayList list = new ArrayList();

                list.Add(price);
                list.Add(qty);
                list.Add(mySize);

                var listOfOrderBookMenuListItems = GetOrderBookMenuList();
                Assert.Equal(list, listOfOrderBookMenuListItems);
                logger.LogCheckPoint(string.Format(LogMessage.OrderBookDetailsPassed, price, qty, mySize));
                return flag;
            }
            catch (Exception)
            {
                logger.LogCheckPoint(string.Format(LogMessage.OrderBookDetailsFailed, price, qty, mySize));
            }
            return flag;
        }

        // This method will verify the details of order book
        public bool VerifyRecentTradesMenuTab(string price, string qty, string time)
        {
            bool flag = false;
            try
            {
                flag = true;
                ArrayList list = new ArrayList();

                list.Add(price);
                list.Add(qty);
                list.Add(time);

                var listOfRecentTradeMenuListItems = GetRecentTradeMenuList();
                Assert.Equal(list, listOfRecentTradeMenuListItems);
                logger.LogCheckPoint(string.Format(LogMessage.RecentTradeDetailsPassed, price, qty, time));
                return flag;
            }
            catch (Exception)
            {
                logger.LogCheckPoint(string.Format(LogMessage.RecentTradeDetailsFailed, price, qty, time));
            }
            return flag;
        }
      
        // This method will return the order history tab label name
        public ArrayList GetOrderHistoryList()
        {
            try
            {
                ArrayList ordersHistoryList = new ArrayList();
                int countOfOrdersList = CountOfOrderHistoryList();
                for (int i = 1; i <= countOfOrdersList; i++)
                {
                    String textFinal = Const.RemoveWhiteSpace;
                    textFinal = driver.FindElement(By.XPath("(//div[@class='ap-tab__menu order-history__menu']/div)[" + i + "]")).Text;
                    ordersHistoryList.Add(textFinal);
                }
                return ordersHistoryList;
            }
            catch (Exception)
            {

                throw;
            }
        }

        // This method will return label text of row menu of Order history tab
        public ArrayList GetOrderHistoryRowMenuTextList()
        {
            try
            {
                ArrayList ordersHistoryList = new ArrayList();
                int countOfOrdersList = CountOfOrderHistoryMenuLabelTextList();
                for (int i = 1; i <= countOfOrdersList; i++)
                {
                    String textFinal = Const.RemoveWhiteSpace;
                    textFinal = driver.FindElement(By.XPath("(//div[@class='flex-table__header order-history-table__table-header']/div/div)[" + i + "]")).Text;
                    ordersHistoryList.Add(textFinal);
                }
                return ordersHistoryList;
            }
            catch (Exception)
            {

                throw;
            }
        }

        // This method verify the open orders name label and their child menu label name
        public bool VerifyOpenOrdersTab(string openOrders, string pair, string side, string type, string size, string price, string orderTime, string status, string action)
        {
            try
            {
                var flag = false;
                ArrayList expectedRow = new ArrayList();
                expectedRow.Add(pair);
                expectedRow.Add(side);
                expectedRow.Add(type);
                expectedRow.Add(size);
                expectedRow.Add(price);
                expectedRow.Add(orderTime);
                expectedRow.Add(status);
                expectedRow.Add(action);

                var listOfOpenOrders = GetOrderHistoryList();
                var listOfOpenOrdersRowMenu = GetOrderHistoryRowMenuTextList();
                UserCommonFunctions.OpenOrderTab(driver);
                if (listOfOpenOrders.Contains(openOrders))
                {
                    flag = true;
                    try
                    {
                        logger.LogCheckPoint(LogMessage.OpenOrdersPassed);
                    }
                    catch (Exception)
                    {
                        logger.LogCheckPoint(string.Format(LogMessage.OpenOrdersFailed));
                    }
                        Assert.Equal(expectedRow, listOfOpenOrdersRowMenu);                    
                        logger.LogCheckPoint(string.Format(LogMessage.OpenOrdersInnerMenuLabelPassed));
                        return flag;
                    }
                    return flag;
                }
            catch (Exception)
            {
                logger.LogCheckPoint(string.Format(LogMessage.OpenOrdersInnerMenuLabelFailed));
                throw;
            }
        }


        // This method verify the filled orders name label and their child menu label name
        public bool VerifyFilledOrdersTab(string filledOrders, string id, string pair, string side, string size, string price, string total, string fee, string executionId, string orderTime)
        {
            try
            {
                var flag = false;
                ArrayList expectedRow = new ArrayList();
                expectedRow.Add(id);
                expectedRow.Add(pair);
                expectedRow.Add(side);
                expectedRow.Add(size);
                expectedRow.Add(price);
                expectedRow.Add(total);
                expectedRow.Add(fee);
                expectedRow.Add(executionId);
                expectedRow.Add(orderTime);

                var listOfFilledOrders = GetOrderHistoryList();
                UserCommonFunctions.FilledOrderTab(driver);
                var listOfFilledOrdersRowMenu = GetOrderHistoryRowMenuTextList();                
                if (listOfFilledOrders.Contains(filledOrders))
                {
                    flag = true;
                    try
                    {
                        logger.LogCheckPoint(string.Format(LogMessage.FilledOrdersPassed, filledOrders));
                    }
                    catch (Exception)
                    {
                        logger.LogCheckPoint(string.Format(LogMessage.FilledOrdersFailed));
                    }
                    Assert.Equal(expectedRow, listOfFilledOrdersRowMenu);                               
                    logger.LogCheckPoint(string.Format(LogMessage.filledOrdersInnerMenuLabelPassed));                    
                    return flag;
                }
                return flag;
            }
            catch (Exception)
            {
                logger.LogCheckPoint(string.Format(LogMessage.filledOrdersInnerMenuLabelFailed));
                throw;
            }
        }

        // This method verify the inactive orders name label and their child menu label name
        public bool VerifyInactiveOrdersTab(string inactiveOrders, string pair, string side, string type, string size, string price, string orderTime, string status)
        {
            try
            {
                var flag = false;
                ArrayList expectedRow = new ArrayList();
                expectedRow.Add(pair);
                expectedRow.Add(side);
                expectedRow.Add(type);
                expectedRow.Add(size);
                expectedRow.Add(price);
                expectedRow.Add(orderTime);
                expectedRow.Add(status);

                var listOfInactiveOrders = GetOrderHistoryList();
                UserCommonFunctions.InactiveTab(driver);
                var listOfInactiveOrdersRowMenu = GetOrderHistoryRowMenuTextList();
                if (listOfInactiveOrders.Contains(inactiveOrders))
                {
                    flag = true;
                    try
                    {
                        logger.LogCheckPoint(string.Format(LogMessage.InactiveOrdersPassed, inactiveOrders));
                    }
                    catch (Exception)
                    {
                        logger.LogCheckPoint(string.Format(LogMessage.InactiveOrdersFailed));
                    }
                    Assert.Equal(expectedRow, listOfInactiveOrdersRowMenu);
                    logger.LogCheckPoint(string.Format(LogMessage.InactiveOrdersInnerMenuLabelPassed));
                    return flag;
                }
                return flag;
            }
            catch (Exception)
            {
                logger.LogCheckPoint(string.Format(LogMessage.InactiveOrdersInnerMenuLabelFailed));
                throw;
            }
        }

        // This method verify the trade report orders name label and their child menu label name
        public bool VerifyTradeReportTab(string tradeReportOrders, string pair, string side, string size, string price, string fee, string orderTime, string status)
        {
            try
            {
                var flag = false;
                ArrayList expectedRow = new ArrayList();
                expectedRow.Add(pair);
                expectedRow.Add(side);
                expectedRow.Add(size);
                expectedRow.Add(price);
                expectedRow.Add(fee);
                expectedRow.Add(orderTime);
                expectedRow.Add(status);

                var listOfTradeReportsOrders = GetOrderHistoryList();
                UserCommonFunctions.TradeTab(driver);
                var listOftradeReportsRowMenu = GetOrderHistoryRowMenuTextList();
                if (listOfTradeReportsOrders.Contains(tradeReportOrders))
                {
                    flag = true;
                    try
                    {
                        logger.LogCheckPoint(string.Format(LogMessage.TradeReportsPassed, tradeReportOrders));
                    }
                    catch (Exception)
                    {
                        logger.LogCheckPoint(string.Format(LogMessage.TradeReportsFailed));
                    }
                    Assert.Equal(expectedRow, listOftradeReportsRowMenu);
                    logger.LogCheckPoint(string.Format(LogMessage.TradeOrdersInnerMenuLabelPassed));
                    return flag;
                }
                return flag;
            }
            catch (Exception)
            {
                logger.LogCheckPoint(string.Format(LogMessage.TradeOrdersInnerMenuLabelFailed));
                throw;
            }
        }

        // This method verify the trade report orders name label and their child menu label name
        public bool VerifyDepositStatusTab(string depositStatus, string product, string amount, string status, string created, string fee)
        {
            try
            {
                var flag = false;
                ArrayList expectedRow = new ArrayList();
                expectedRow.Add(product);
                expectedRow.Add(amount);
                expectedRow.Add(status);
                expectedRow.Add(created);
                expectedRow.Add(fee);
                var listOfDepositStatus = GetOrderHistoryList();
                UserCommonFunctions.DepositTab(driver);
                var listOfDepositStatusRowMenu = GetOrderHistoryRowMenuTextList();
                if (listOfDepositStatus.Contains(depositStatus))
                {
                    flag = true;
                    try
                    {
                        logger.LogCheckPoint(string.Format(LogMessage.DepositStatusPassed, depositStatus));
                    }
                    catch (Exception)
                    {
                        logger.LogCheckPoint(string.Format(LogMessage.DepositStatusFailed));
                    }
                    Assert.Equal(expectedRow, listOfDepositStatusRowMenu);
                    logger.LogCheckPoint(string.Format(LogMessage.DeopsitStatusInnerMenuLabelPassed));
                    return flag;
                }
                return flag;
            }
            catch (Exception)
            {
                logger.LogCheckPoint(string.Format(LogMessage.DeopsitStatusInnerMenuLabelFailed));
                throw;
            }
        }

        // This method verify the trade report orders name label and their child menu label name
        public bool VerifyWithdrawStatusTab(string withdrawStatus, string product, string amount, string status, string created, string fee, string action)
        {
            try
            {
                var flag = false;
                ArrayList expectedRow = new ArrayList();
                expectedRow.Add(product);
                expectedRow.Add(amount);
                expectedRow.Add(status);
                expectedRow.Add(created);
                expectedRow.Add(fee);
                expectedRow.Add(action);
                var listOfWithdrawStatus = GetOrderHistoryList();
                UserCommonFunctions.WithdrawTab(driver);
                var listOfWithdrawStatusRowMenu = GetOrderHistoryRowMenuTextList();
                if (listOfWithdrawStatus.Contains(withdrawStatus))
                {
                    flag = true;
                    try
                    {
                        logger.LogCheckPoint(string.Format(LogMessage.WithdrawStatusPassed, withdrawStatus));
                    }
                    catch (Exception)
                    {
                        logger.LogCheckPoint(string.Format(LogMessage.WithdrawStatusFailed));
                        throw;
                    }
                    Assert.Equal(expectedRow, listOfWithdrawStatusRowMenu);
                    logger.LogCheckPoint(string.Format(LogMessage.WithdrawStatusInnerMenuLabelPassed));
                    return flag;
                }
                return flag;
            }
            catch (Exception)
            {
                logger.LogCheckPoint(string.Format(LogMessage.WithdrawStatusInnerMenuLabelFailed));
                throw;
            }
        }
    }
}
