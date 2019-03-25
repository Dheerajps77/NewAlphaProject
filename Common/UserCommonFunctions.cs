using AlphaPoint_QA.pages;
using AlphaPoint_QA.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Threading;

namespace AlphaPoint_QA.Common
{
    enum InstrumentName
    {
        DASCUSD,
        BTCUSD,
        ETHCUSD,
        LTCUSD,
        LTCBTC,
        BTCEUR,
        FUELBTC,
        ETHBTC
    }

    class UserCommonFunctions
    {
        ProgressLogger logger;
        static Config data;
        public static IWebDriver driver;

        public UserCommonFunctions(ProgressLogger logger)
        {
            this.logger = logger;
            data = ConfigManager.Instance;
            driver = AlphaPointWebDriver.GetInstanceOfAlphaPointWebDriver();
        }

        static IJavaScriptExecutor js;
        static By dashBoardMenu = By.CssSelector("div.page-header-nav__menu-toggle");
        static By selectExchangeLink = By.CssSelector("a[href='/exchange']");
        static By buyAndSell = By.CssSelector("a[href='/buy-sell']");
        static By userSetting = By.CssSelector("a[href='/settings/profile']");
        static By wallets = By.CssSelector("a[href='/wallets']");
        
        static By selectExchange = By.CssSelector("#root > div:nth-of-type(1) > div:nth-of-type(1) > div:nth-of-type(2) > a:nth-of-type(2)");
        static By clickOnInstrument = By.CssSelector("button.instrument-selector__trigger");
        static By askPrice = By.CssSelector("span.instrument-table__value.instrument-table__value--hideable.instrument-table__value--ask");
        static By selectInstrumentDASCUSD = By.XPath("//div[@class='flex-table__column instrument-selector-popup__column instrument-selector-popup__column--coin' and text()='DASCUSD']");
        static By selectInstrumentBTCUSD = By.XPath("//div[@class='flex-table__column instrument-selector-popup__column instrument-selector-popup__column--coin' and text()='BTCUSD']");
        static By selectInstrumentETHCUSD = By.XPath("//div[@class='flex-table__column instrument-selector-popup__column instrument-selector-popup__column--coin' and text()='ETHCUSD']");
        static By selectInstrumentLTCUSD = By.XPath("//div[@class='flex-table__column instrument-selector-popup__column instrument-selector-popup__column--coin' and text()='LTCUSD']");
        static By selectInstrumentLTCBTC = By.XPath("//div[@class='flex-table__column instrument-selector-popup__column instrument-selector-popup__column--coin' and text()='LTCBTC']");
        static By selectInstrumentBTCEUR = By.XPath("//div[@class='flex-table__column instrument-selector-popup__column instrument-selector-popup__column--coin' and text()='BTCEUR']");
        static By selectInstrumentFUELBTC = By.XPath("//div[@class='flex-table__column instrument-selector-popup__column instrument-selector-popup__column--coin' and text()='FUELBTC']");
        static By selectInstrumentETHBTC = By.XPath("//div[@class='flex-table__column instrument-selector-popup__column instrument-selector-popup__column--coin' and text()='ETHBTC']");
        static By getInstrumentName = By.CssSelector("#root > div:nth-of-type(1) > div:nth-of-type(2) > div:nth-of-type(1) > div:nth-of-type(1) > button > span:nth-of-type(1)");
        static By confirmOrderPop = By.XPath("//button[text()='Confirm Order']");

        static By signOutButton = By.CssSelector("a[class*='popover-menu__item user-summary']");
        static By userLogoButton = By.CssSelector("button[class*=user-summary__popover-menu-trigger]");
        static By balanceAmountInWallet = By.CssSelector("div.wallet-card__amount>span");
        static By dashBoardMenuListItems = By.CssSelector("a[class*='page-header-nav__item page-header-nav__item--hoverable']");
     
        static By openOrder = By.CssSelector("div[data-test='Open Orders']");
        static By filledOrder = By.CssSelector("div[data-test='Filled Orders']");
        static By inactiveOrder = By.CssSelector("div[data-test='Inactive Orders']");
        static By tradeOrder = By.CssSelector("div[data-test='Trade Reports']");
        static By depositOrder = By.CssSelector("div[data-test='Deposit Status']");
        static By withdrawOrder = By.CssSelector("div[data-test='Withdraw Status']");
        static By askpriceinorderbook = By.CssSelector("div.flex-table__column.orderbook__table-price.orderbook__table-price--sell > span");
        static By quantityinorderbook = By.CssSelector("div.flex-table__column.orderbook__table-qty.orderbook__table-qty--sell > span");
        static By advanceOrderButton = By.XPath("//div[text()='« Advanced Orders']");

        static By selectServer = By.CssSelector("select[name='tradingServer']");
        static By userLoginName = By.CssSelector("input[name='username']");
        static By userLoginPassword = By.CssSelector("input[name='password']");
        static By userLoginButton = By.CssSelector("button.ap-button__btn.ap-button__btn--additive.login-form__btn.login-form__btn--additive");
        static By exchangeButton = By.CssSelector("a[href='/exchange'] span:nth-of-type(2)");
        static By orderEntryTab = By.CssSelector("div[data-test='Order Entry']");
        static By advancedOrderLink = By.XPath("//div[text()='« Advanced Orders']");
        static By messageDisplayed = By.XPath("//div[contains(@class,'snackbar snackbar')]/div");
        static By toastMessageAdmin = By.XPath("//div[@id='messages']/div/div/div/span");
        static By closeIconAdvancedOrder = By.CssSelector("div.ap-sidepane__close-button.advanced-order-sidepane__close-button>span");
        static By loggedInUserName = By.CssSelector("button.user-summary__popover-menu-trigger.page-header-user-summary__popover-menu-trigger");
        static By userSignOutButton = By.XPath("//span[contains(@class,'popover-menu__item-label') and text()='Sign Out']");
        static By cancelAllOrder = By.XPath("//div[@class='bulk-cancel-buttons']//span[text()='All']");
        static By getFeeSpan = By.CssSelector("span[data-test=Fees]");
        static By getMarketPriceSpan = By.CssSelector("span[data-test='Market Price']");
        static By getOrderTotalSpan = By.CssSelector("span[data-test='Order Total']");
        static By getNetSpan = By.CssSelector("span[data-test='Net']");
        static By cancelBuyOrder = By.XPath("//div[@class='orderbook__spread-row']//following::div[1]//div/div[4]");
        static By cancelSellOrder = By.XPath("//div[@class='orderbook__spread-row']//preceding::div[@class='flex-table__column orderbook__cancel-btn']");
        static By kycPageBtn = By.CssSelector("a.ap-logo__link.standalone-layout__logo__link");

        public static IWebElement AskPrice(IWebDriver driver)
        {
            return driver.FindElement(askPrice);
        }

        public static IWebElement GetFeeSpan(IWebDriver driver)
        {
            return driver.FindElement(getFeeSpan);
        }

        public static IWebElement GetMarketPriceSpan(IWebDriver driver)
        {
            return driver.FindElement(getMarketPriceSpan);
        }

        public static IWebElement GetOrderTotalSpan(IWebDriver driver)
        {
            return driver.FindElement(getOrderTotalSpan);
        }

        public static IWebElement GetNetSpan(IWebDriver driver)
        {
            return driver.FindElement(getNetSpan);
        }

        public static IWebElement ConfirmPopUp(IWebDriver driver)
        {
            return driver.FindElement(confirmOrderPop);
        }

        //This method will click on exchange button
        public static IWebElement ExchangeButton(IWebDriver driver)
        {
            return driver.FindElement(exchangeButton);
        }

        //This method will click on Order Entry button
        public static IWebElement OrderEntryTab(IWebDriver driver)
        {
            return driver.FindElement(orderEntryTab);
        }

        //This method will click on Open Order Tab
        public static void OpenOrderTab(IWebDriver driver)
        {
            driver.FindElement(openOrder).Click();
        }

        //This method will click on filled Order Tab
        public static void FilledOrderTab(IWebDriver driver)
        {
            driver.FindElement(filledOrder).Click();
        }

        //This method will click on inactive Order Tab
        public static void InactiveTab(IWebDriver driver)
        {
            driver.FindElement(inactiveOrder).Click();
        }

        //This method will click on trade Order Tab
        public static void TradeTab(IWebDriver driver)
        {
            driver.FindElement(tradeOrder).Click();
        }

        //This method will click on deposit Order Tab
        public static void DepositTab(IWebDriver driver)
        {
            driver.FindElement(depositOrder).Click();
        }

        //This method will click on withdraw Order Tab
        public static void WithdrawTab(IWebDriver driver)
        {
            driver.FindElement(withdrawOrder).Click();
        }

        // This method cancels the Buy order through Order Book 
        public static void CancelOrderBookBuyOrder(IWebDriver driver)
        {
            try
            {
                IWebElement cancelBuyElement = driver.FindElement(cancelBuyOrder);
                if (cancelBuyElement.Displayed)
                {
                    UserSetFunctions.Click(cancelBuyElement);
                }
            }
            catch (NoSuchElementException)
            {
                throw;
            }
        }

        // This method cancels the Sell order through Order Book 
        public static void CancelOrderBookSellOrder(IWebDriver driver)
        {
            try
            {
                IWebElement cancelSellElement = driver.FindElement(cancelSellOrder);
                if (cancelSellElement.Displayed)
                {
                    UserSetFunctions.Click(cancelSellElement);
                }
            }
            catch (NoSuchElementException)
            {
                throw;
            }
        }

        public static void CancelAllOrders(IWebDriver driver)
        {
            try
            {
                ScrollingDownVertical(driver);
                IWebElement cancelElement = driver.FindElement(cancelAllOrder);
                if (cancelElement.Enabled)
                {
                    UserSetFunctions.Click(cancelElement);
                    Thread.Sleep(4000);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This method will scroll to down till pixel defined by user
        public static void ScrollingDownVertical(IWebDriver driver)
        {
            js=(IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollBy(0, 350)");
            Thread.Sleep(2000);
        }

        //This method scroll to up till pixel defined by user
        public static void ScrollingUpVertical(IWebDriver driver)
        {
            js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollBy(0, -600)");
        }

        //This method scroll to Horizontally right 
        public static void ScrollingRightHorizontally(IWebDriver driver)
        {
            js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollBy(500, 0)");
        }

        //This method scroll to up till pixel defined by user
        public static void ScrollingLeftHorizontally(IWebDriver driver)
        {
            js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollBy(-500, 0)");
        }

        //This method scroll to particular webElement
        public static void ScrollingToParticularElement(IWebDriver driver, IWebElement iwebElement)
        {
            js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", iwebElement);
        }

        //This method scroll till Particular Coordinates
        public static void ScrollingToParticularCoordinates(IWebDriver driver)
        {
            js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollBy(200,300)");
        }

        //This method is use to click on DashBoard button.
        public static void DashBoardMenuButton(IWebDriver driver)
        {
            for (int i = 0; i <= 2; i++)
            {
                try
                {
                    GenericUtils.WaitForElementVisibility(driver, dashBoardMenu, 30).Click();
                    break;
                }
                catch (StaleElementReferenceException)
                {
                    throw;
                }
            }

        }

        //This method is use select an Exchange.
        public static void SelectAnExchange(IWebDriver driver)
        {
            Thread.Sleep(2000);
            for (int i = 0; i <= 2; i++)
            {
                try
                {
                    GenericUtils.WaitForElementVisibility(driver, selectExchangeLink, 30).Click();
                    break;
                }
                catch (StaleElementReferenceException)
                {
                    throw;
                }
            }
        }

        //This method Navigates to Exchange selects the Instrument
        public static void SelectInstrumentFromExchange(string instrument, IWebDriver driver)
        {
            Thread.Sleep(2000);
            driver.FindElement(clickOnInstrument).Click();
            Thread.Sleep(1000);
            if (instrument.Equals(InstrumentName.DASCUSD.ToString()))
            {
                for (int i = 0; i <= 2; i++)
                {
                    try
                    {
                        GenericUtils.WaitForElementVisibility(driver, selectInstrumentDASCUSD, 30).Click();
                        break;
                    }
                    catch (StaleElementReferenceException)
                    {
                        throw;
                    }
                }
            }
            else if (instrument.Equals(InstrumentName.BTCUSD.ToString()))
            {

                for (int i = 0; i <= 2; i++)
                {
                    try
                    {
                        GenericUtils.WaitForElementVisibility(driver, selectInstrumentBTCUSD, 30).Click();
                        break;
                    }
                    catch (StaleElementReferenceException)
                    {
                        throw;
                    }
                }
            }
            else if (instrument.Equals(InstrumentName.ETHCUSD.ToString()))
            {
                try
                {
                    driver.FindElement(selectInstrumentETHCUSD).Click();
                }
                catch (StaleElementReferenceException)
                {
                    driver.FindElement(selectInstrumentETHCUSD).Click();
                }

            }
            else if (instrument.Equals(InstrumentName.LTCUSD.ToString()))
            {
                try
                {
                    driver.FindElement(selectInstrumentLTCUSD).Click();
                }
                catch (StaleElementReferenceException)
                {
                    driver.FindElement(selectInstrumentLTCUSD).Click();
                }
            }
            else if (instrument.Equals(InstrumentName.LTCBTC.ToString()))
            {
                try
                {
                    driver.FindElement(selectInstrumentLTCBTC).Click();
                }
                catch (StaleElementReferenceException)
                {
                    driver.FindElement(selectInstrumentLTCBTC).Click();
                }
            }
            else if (instrument.Equals(InstrumentName.BTCEUR.ToString()))
            {
                try
                {
                    driver.FindElement(selectInstrumentBTCEUR).Click();
                }
                catch (StaleElementReferenceException)
                {
                    driver.FindElement(selectInstrumentBTCEUR).Click();
                }
            }
            else if (instrument.Equals(InstrumentName.FUELBTC.ToString()))
            {
                try
                {
                    driver.FindElement(selectInstrumentFUELBTC).Click();
                }
                catch (StaleElementReferenceException)
                {
                    driver.FindElement(selectInstrumentFUELBTC).Click();
                }
            }
            else if (instrument.Equals(InstrumentName.ETHBTC.ToString()))
            {
                try
                {
                    driver.FindElement(selectInstrumentETHBTC).Click();
                }
                catch (StaleElementReferenceException)
                {
                    driver.FindElement(selectInstrumentETHBTC).Click();
                }
            }
        }



        //This method will click on "Buy & Sell" from Dashboard menu
        public static void NavigateToBuySell(IWebDriver driver)
        {
            GenericUtils.WaitForElementVisibility(driver, buyAndSell, 30).Click();
        }

        //This method will click on "User Settings" from Dashboard menu
        public static void NavigateToUserSetting(IWebDriver driver)
        {
            GenericUtils.WaitForElementVisibility(driver, userSetting, 30).Click();
        }

        //This method will click on "Wallets" from Dashboard menu
        public static void NavigateToWallets(IWebDriver driver)
        {
            GenericUtils.WaitForElementVisibility(driver, wallets, 30).Click();
        }

        //This method will click on "LogOut" in the page
        public static void LogOut(IWebDriver driver)
        {
            driver.FindElement(userLogoButton).Click();
            driver.FindElement(signOutButton).Click();
        }

        // This method will close the Browser
        public static void CloseBrowser(IWebDriver driver)
        {
            driver.Close();
        }

        //This method will click on advance order button
        public static void AdvanceOrder(IWebDriver driver)
        {
            ScrollingDownVertical(driver);
            driver.FindElement(advanceOrderButton).Click();
        }

        //This method will get Ask Price from OrderBook
        public static string GetAskPriceFromOrderBook(IWebDriver driver)
        {
            return driver.FindElement(askpriceinorderbook).Text;
        }

        //This method will get Quantity from Order Book
        public static string GetQuantityFromOrderBook(IWebDriver driver)
        {
            return driver.FindElement(quantityinorderbook).Text;
        }

        //This method will get the trailing price in case of Buy order
        public static string GetBuyOrderTrailingPrice(string limitPrice, string trailingAmount)
        {
            double trailingPrice;
            string limitPriceValue;
            string trailingAmountValue;
            limitPriceValue = GenericUtils.ConvertToDoubleFormat(Double.Parse(limitPrice));
            trailingAmountValue = GenericUtils.ConvertToDoubleFormat(Double.Parse(trailingAmount));
            trailingPrice = Double.Parse(limitPriceValue) + Double.Parse(trailingAmountValue);
            return trailingPrice.ToString();
        }

        //This method will get the trailing price in case of Sell order
        public static string GetSellOrderTrailingPrice(string limitPrice, string trailingAmount)
        {
            double trailingPrice;
            string limitPriceValue;
            string trailingAmountValue;
            limitPriceValue = GenericUtils.ConvertToDoubleFormat(Double.Parse(limitPrice));
            trailingAmountValue = GenericUtils.ConvertToDoubleFormat(Double.Parse(trailingAmount));
            trailingPrice = Double.Parse(limitPriceValue) - Double.Parse(trailingAmountValue);
            return trailingPrice.ToString();
        }

        //This method stores the Amount Balances for Order Entry Page
        public static Dictionary<string, string> StoreOrderEntryAmountBalances(IWebDriver driver)
        {
            string marketPrice = GetMarketPriceSpan(driver).Text;
            string fees = GetFeeSpan(driver).Text;
            string orderTotal = GetOrderTotalSpan(driver).Text;         
            string net = GetNetSpan(driver).Text;
            Dictionary<string, string> amountDetailsList = new Dictionary<string, string>();
            amountDetailsList.Add(Const.MarketPrice, marketPrice);
            amountDetailsList.Add(Const.Fees, fees);
            amountDetailsList.Add(Const.OrderTotal, orderTotal);
            amountDetailsList.Add(Const.Net, net);
            return amountDetailsList;
        }

        //This method stores the Amount Balances for Order Entry Page
        public static Dictionary<string, string> StoreBalancesFromExchangePage(IWebDriver driver, string user)
        {
            return new Dictionary<string, string>();
        }

       //This method returns the Success and Failure messages
        public static string GetTextOfMessage(IWebDriver driver, ProgressLogger logger)
        {
            try
            {
                IWebElement msgWebElement = GenericUtils.WaitForElementVisibility(driver, messageDisplayed, 10);
                string messageText = msgWebElement.Text;
                return messageText;
            }
            catch (Exception e)
            {
                logger.Error(LogMessage.FailedToRetrieveMessage, e);
                logger.Error(e.StackTrace);
                throw;
            }
        }

        //This method returns the text of toast message in Admin
        public static string GetTextOfToastMessageInAdmin(IWebDriver driver, ProgressLogger logger)
        {
            try
            {
                IWebElement msgWebElement = GenericUtils.WaitForElementPresence(driver, toastMessageAdmin, 20);
                string messageText = msgWebElement.Text;
                return messageText;
            }
            catch (Exception e)
            {
                logger.Error(LogMessage.FailedToRetrieveMessage, e);
                logger.Error(e.StackTrace);
                throw;
            }
        }

        //This method closes the Advanced Order Section
        public static void CloseAdvancedOrderSection(IWebDriver driver, ProgressLogger logger)
        {
            try
            {
                IWebElement closeAdvanced = GenericUtils.WaitForElementVisibility(driver, closeIconAdvancedOrder, 30);
                UserSetFunctions.Click(closeAdvanced);
                Thread.Sleep(2000);
            }
            catch (Exception e)
            {
                logger.Error(LogMessage.CloseAdvanceOrderSectFailureMsg, e);
                logger.Error(e.StackTrace);
                throw;
            }
        }

        //This method will click on "confirm Order" button
        public static void ConfirmWindowOrder(string askPrice, string limitPrice, IWebDriver driver)
        {
            Thread.Sleep(2000);
            double askPriceInt = Double.Parse(askPrice);
            double limitPriceInt = Double.Parse(limitPrice);
            try
            {
                if (askPriceInt > limitPriceInt)
                {
                    UserSetFunctions.Click(ConfirmPopUp(driver));
                }
            }
            catch (NoSuchElementException)
            {
                throw;
            }
        }

        //This method is a prerequisite for cancelling all existing orders and placing a sell limit order
        public string CancelAndPlaceLimitSellOrder(IWebDriver driver, string instrument, string sellTab, string orderSize, string limitPrice, string timeInForce)
        {
            string askPrice;
            logger.LogCheckPoint(String.Format(LogMessage.MarketSetupBegin, sellTab, orderSize, limitPrice));            
            OrderEntryPage orderEntryPage = new OrderEntryPage(driver, logger);
            orderEntryPage.PlaceLimitSellOrder(instrument, sellTab, orderSize, limitPrice, timeInForce);
            askPrice = AskPrice(driver).Text;
            return askPrice;
        }

        //This method is a prerequisite for cancelling all existing orders and placing a buy limit order
        public string CancelAndPlaceLimitBuyOrder(IWebDriver driver, string instrument, string buyTab, string orderSize, string limitPrice, string timeInForce)
        {
            string askPrice;
            logger.LogCheckPoint(String.Format(LogMessage.MarketSetupBegin, buyTab, orderSize, limitPrice));
            OrderEntryPage orderEntryPage = new OrderEntryPage(driver, logger);
            orderEntryPage.PlaceLimitBuyOrder(instrument, buyTab, orderSize, limitPrice, timeInForce);
            askPrice = AskPrice(driver).Text;
            return askPrice;
        }

        // This method places a buy and sell order with same order size and limit price to set the Last Price
        public void PlaceOrdersToSetLastPrice(IWebDriver driver, string instrument, string buyTab, string sellTab, string orderSize, string limitPrice, string timeInForce, string userBuyer, string userSeller)
        {
            UserFunctions userFunctions = new UserFunctions(logger);
            userFunctions.LogIn(logger, userBuyer);
            string buyAskPrice = CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, orderSize, limitPrice, timeInForce);
            ConfirmWindowOrder(buyAskPrice, limitPrice, driver);
            logger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, buyTab, orderSize, limitPrice));
            userFunctions.LogIn(logger, userSeller);
            string sellPrice = CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, orderSize, limitPrice, timeInForce);
            ConfirmWindowOrder(sellPrice, limitPrice, driver);
            logger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, sellTab, orderSize, limitPrice));
        }

        // Login with a user and cancell all placed orders
        public static void LoginAndCancelAllOrders(ProgressLogger logger, IWebDriver driver, string instrument, string loginUser)
        {
            UserFunctions userFunctions = new UserFunctions(logger);
            userFunctions.LogIn(logger, loginUser);
            Thread.Sleep(2000);
            DashBoardMenuButton(driver);
            SelectAnExchange(driver);
            SelectInstrumentFromExchange(instrument, driver);
            CancelAllOrders(driver);
        }

        // This method returns the value by splitting the Currency and value
        public static string GetSplitValue(string valueToSplit)
        {
            string valueAmount;            
            string[] splitValueList = valueToSplit.Split(" ");
            valueAmount = splitValueList[1];
            return valueAmount;
        }

        // This method is used to click on Alphapoint link on KYC page
        public void SelectLinkOnKYCPage(IWebDriver driver)
        {
            driver.FindElement(kycPageBtn).Click();
        }
    }
}

