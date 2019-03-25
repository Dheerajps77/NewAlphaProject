using AlphaPoint_QA.Common;
using AlphaPoint_QA.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace AlphaPoint_QA.Pages
{
    class BuyAndSellPage
    {
        private string value;
        ProgressLogger logger;
        static Config data;
        public static IWebDriver driver;

        By bTCBalances = By.CssSelector("span[data-test=' Bitcoin:']");
        By usdBalances = By.CssSelector("span[data-test=' USD:']");
        By radiouButtonOf100USD = By.CssSelector("div.buy-sell__amount-container div.buy-sell__amount-row:nth-of-type(1) label");
        By buyButtonOption = By.CssSelector("div.buy-sell__tab.buy-sell__tab--buy-selected");
        By sellButtonOption = By.CssSelector("div.buy-sell__tab:nth-of-type(3)");
        By makeTransactionOption = By.CssSelector("div.buy-sell-make-transaction");
        By chartOption = By.CssSelector("div.chart.price-history");
        By buySelltextField = By.CssSelector("div.buy-sell__amount-row.buy-sell__amount-row--two-inputs-layout div:nth-of-type(2) input");
        By fifthRadioButton = By.CssSelector("label[for='custom']>span:nth-of-type(2)");
        By radioButton100Dollar = By.CssSelector("div.ap-radio label[for='100'] span:nth-of-type(2)");
        By radioButton200Dollar = By.CssSelector("div.ap-radio label[for='200'] span:nth-of-type(2)");
        By priceValue = By.CssSelector("span[data-test=Price]");
        By transactionCostValue = By.CssSelector("span[data-test='Transaction Cost']");
        By btcToBuyValue = By.CssSelector("span[data-test='BTC to Buy']");
        By btcToSellValue = By.CssSelector("span[data-test='BTC to Sell']");
        By feeValue = By.CssSelector("span[data-test=Fee]");
        By selected100DollarUSD = By.CssSelector("div.buy-sell__amount-row:nth-of-type(1)>div:nth-of-type(2)");
        By selectedBTCToBuy = By.CssSelector("div.buy-sell__amount-row:nth-of-type(1) div:nth-of-type(3)");
        By buyBitcoinWithUSDButton = By.CssSelector("button.ap-button__btn.ap-button__btn--additive.buy-sell__btn.buy-sell__btn--additive");
        By sellBitcoinWithUSDButton = By.CssSelector("button.ap-button__btn.ap-button__btn--subtractive.buy-sell__btn.buy-sell__btn--subtractive");
        By confirmBuyOrderButton = By.CssSelector("div.ap-modal__footer.buy-sell-order-confirmation__modal__footer>button");
        By confirmSellOrderButton = By.CssSelector("div.ap-modal__footer.buy-sell-order-confirmation__modal__footer>button");
        By oKBuyButton = By.CssSelector("div.ap-modal__footer.buy-sell-order-confirmation__modal__footer button");
        By okSellButton = By.CssSelector("button.ap-button__btn.ap-button__btn--general.ap-modal.buy-sell-order-confirmation__modal__btn.ap-modal.buy-sell-order-confirmation__modal__btn--general");
        By buyAndSellMenuButton = By.CssSelector("div.page-header-nav__menu-toggle");
        By buyAndSellButton = By.CssSelector("a[href='/buy-sell']");
        By sellButton = By.CssSelector("span.isvg.loaded.ap-icon.ap-icon--sell.ap-icon--big.buy-sell__icon.buy-sell__icon--sell.buy-sell__icon--big");
        By selectedBTCToSell = By.CssSelector("div.buy-sell__amount-row:nth-of-type(1) div:nth-of-type(3)");
        By successMsg = By.CssSelector("h3.buy-sell-order-confirmation__header");
        By btcToBuyValueOnConfirmOrder = By.CssSelector("div.ap-label-with-text.buy-sell-order-confirmation__lwt-container span[data-test='BTC to Buy']");
        By btcToSellValueOnConfirmOrder = By.CssSelector("div.ap-label-with-text.buy-sell-order-confirmation__lwt-container span[data-test='BTC to Sell']");
        By LimitPriceValueOnConfirmOrder = By.CssSelector("span[data-test='Limit Price']");
        By feeValueOnConfirmOrder = By.CssSelector("div.ap-label-with-text.buy-sell-order-confirmation__lwt-container span[data-test=Fee]");
        By finalQtyValueOnConfirmOrder = By.CssSelector("span[data-test='Final Quantity']");
        By dateTimeValueOnConfirmOrder = By.CssSelector("span[data-test='Date']");

        public object Testdata { get; private set; }

        public BuyAndSellPage(ProgressLogger logger)
        {
            this.logger = logger;
            data = ConfigManager.Instance;
            driver = AlphaPointWebDriver.GetInstanceOfAlphaPointWebDriver();
        }

        public IWebElement BtcToBuyValueOnConfirmOrder()
        {
            return driver.FindElement(btcToBuyValueOnConfirmOrder);
        }

        public IWebElement SuccessMsg()
        {
            return driver.FindElement(successMsg);
        }

        public IWebElement BtcToSellValueOnConfirmOrder()
        {
            return driver.FindElement(btcToSellValueOnConfirmOrder);
        }

        public IWebElement BuyLimitPriceValueOnConfirmOrder()
        {
            return driver.FindElement(LimitPriceValueOnConfirmOrder);
        }

        public IWebElement FeeValueOnConfirmOrder()
        {
            return driver.FindElement(feeValueOnConfirmOrder);
        }

        public IWebElement FinalQtyValueOnConfirmOrder()
        {
            return driver.FindElement(finalQtyValueOnConfirmOrder);
        }

        public IWebElement DateTimeValueOnConfirmOrder()
        {
            return driver.FindElement(dateTimeValueOnConfirmOrder);
        }

        public IWebElement SellButton()
        {
            return driver.FindElement(sellButton);
        }

        public IWebElement BuyAndSellButton()
        {
            return driver.FindElement(buyAndSellButton);
        }

        public IWebElement SelectedBTCToSell()
        {
            return driver.FindElement(selectedBTCToSell);
        }

        public IWebElement SellBitcoinWithUSDButton()
        {
            return driver.FindElement(sellBitcoinWithUSDButton);
        }

        public IWebElement BuyBitcoinWithUSDButton()
        {
            return driver.FindElement(buyBitcoinWithUSDButton);
        }

        public IWebElement BuyAndSellMenuButton()
        {
            return driver.FindElement(buyAndSellMenuButton);
        }

        public IWebElement OKBuyButton()
        {
            return driver.FindElement(oKBuyButton);
        }

        public IWebElement OKSellButton()
        {
            return driver.FindElement(okSellButton);
        }

        public IWebElement ConfirmBuyOrderButton()
        {
            return driver.FindElement(confirmBuyOrderButton);
        }

        public IWebElement ConfirmSellOrderButton()
        {
            return driver.FindElement(confirmSellOrderButton);
        }

        public IWebElement selected100USD()
        {
            return driver.FindElement(selected100DollarUSD);
        }
        public IWebElement SelectedBTCToBuy()
        {
            return driver.FindElement(selectedBTCToBuy);
        }

        public IWebElement PriceValue()
        {
            return driver.FindElement(priceValue);
        }
        public IWebElement TransactionCostValue()
        {
            return driver.FindElement(transactionCostValue);
        }
        public IWebElement BTCToBuyValue()
        {
            return driver.FindElement(btcToBuyValue);
        }

        public IWebElement BTCToSellValue()
        {
            return driver.FindElement(btcToSellValue);
        }

        public IWebElement FeeValue()
        {
            return driver.FindElement(feeValue);
        }

        public IWebElement BTCBalances()
        {
            Thread.Sleep(2000);
            return driver.FindElement(bTCBalances);
        }

        public IWebElement RadioButton100Dollar()
        {
            return driver.FindElement(radioButton100Dollar);
        }

        public IWebElement RadioButton200Dollar()
        {
            return driver.FindElement(radioButton200Dollar);
        }

        public IWebElement FifthRadioButton()
        {
            return driver.FindElement(fifthRadioButton);
        }

        public IWebElement USDBalances()
        {
            return driver.FindElement(usdBalances);
        }

        public IWebElement RadiouButtonOf100USD()
        {
            return driver.FindElement(radiouButtonOf100USD);
        }

        public IWebElement BuyButtonOption()
        {
            return driver.FindElement(buyButtonOption);
        }

        public IWebElement SellButtonOption()
        {
            return driver.FindElement(sellButtonOption);
        }

        public IWebElement MakeTransactionOption()
        {
            return driver.FindElement(makeTransactionOption);
        }

        public IWebElement ChartOption()
        {
            return driver.FindElement(chartOption);
        }

        public IWebElement BuySelltextField()
        {
            return driver.FindElement(buySelltextField);
        }


        // This method click on sell button in Buy&Sell page
        public void SellBtn()
        {
            try
            {
                Thread.Sleep(2000);
                UserSetFunctions.Click(SellButton());
            }
            catch (Exception)
            {
                throw;
            }
        }
        // This method will click on first radio button with having option $100
        private void RadioBtn()
        {
            try
            {
                Actions action = new Actions(driver);
                action.MoveToElement(RadioButton100Dollar()).Build().Perform();
                UserSetFunctions.Click(RadioButton100Dollar());

                Thread.Sleep(1000);

                action.MoveToElement(RadioButton200Dollar()).Build().Perform();
                UserSetFunctions.Click(RadioButton200Dollar());

                Thread.Sleep(1000);
                action.MoveToElement(RadioButton100Dollar()).Build().Perform();
                UserSetFunctions.Click(RadioButton100Dollar());
            }
            catch (Exception)
            {
                throw;
            }
        }

        // (Sell order)This method will calculate the fee(BTCToBuy amount * feeConstant)
        private String CalculateFee(string selectedBTCToBuyStringValue)
        {
            try
            {
                double feeConstant = Double.Parse(TestData.GetData("FeeComponent"));
                var fee = Double.Parse(selectedBTCToBuyStringValue) * feeConstant;
                var feeDouble = GenericUtils.ConvertToDoubleFormat(fee);
                string fees = feeDouble.ToString().Substring(0, 10);
                return fees;
            }
            catch(Exception)
            {
                throw;
            }
        }

        // (Buy order)This method will calculate the final buy BTC quantity()
        private String GetBuyBitcoinQuantity(double bTCBalanceInDoubleValue, double selectedBTCToBuyDoubleValue, string feeValue)
        {
            try
            {
                double fee = double.Parse(feeValue);
                string finalBTCQty;
                var finalBTCQuantity = (bTCBalanceInDoubleValue + selectedBTCToBuyDoubleValue) - fee;
                finalBTCQty = GenericUtils.ConvertToDoubleFormat(finalBTCQuantity);
                return GenericUtils.RemoveCommaFromString(finalBTCQty);
            }
            catch (Exception)
            {
                throw;
            }
        }


        // (Buy Order)This method will calculate and get the final buy USD balances
        private string GetBuyUSDBalances(string usdBalance)
        {
            try
            {
                Decimal usdDeductionAmount = Decimal.Parse(TestData.GetData("TC42_USDDeductionAmount"));
                Decimal usdBalanceDouble = Decimal.Parse(usdBalance);
                var finalUSDBal = usdBalanceDouble - usdDeductionAmount;
                return finalUSDBal.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }


        // (Sell order)This method will calculate the final sell BTC quantity
        private string GetSellBitcoinQuantity(double bTCBalanceInDoubleValue, double selectedBTCToSellDoubleValue)
        {
            try
            {
                double finalBTCQuantity;
                string finalBTCQty;
                finalBTCQuantity = bTCBalanceInDoubleValue - selectedBTCToSellDoubleValue;
                finalBTCQty = GenericUtils.ConvertToDoubleFormat(finalBTCQuantity);
                return GenericUtils.RemoveCommaFromString(finalBTCQty);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // (Sell Order)This method will calculate and get the final Sell USD balances
        private string GetSellUSDBalances(string usdBalance, string feeValue)
        {
            try
            {
                Decimal fee = Decimal.Parse(feeValue);
                Decimal usdDeductionAmount = Decimal.Parse(TestData.GetData("TC42_USDDeductionAmount"));
                Decimal usdBalanceDouble = Decimal.Parse(usdBalance);

                var FinalUSDBal = ((usdBalanceDouble + usdDeductionAmount) - (fee));

                return FinalUSDBal.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }

        // (Buy Order)This method will hold the text labels name in the dictionary
        public Dictionary<string, string> GetBuyConfirmationOverView()
        {
            try
            {
                Dictionary<string, string> buyConfirmationOverViewDict = new Dictionary<string, string>();
                Thread.Sleep(2000);
                buyConfirmationOverViewDict.Add("BTCToBuy", BtcToBuyValueOnConfirmOrder().Text);
                buyConfirmationOverViewDict.Add("LimitPrice", TransactionCostValue().Text);
                buyConfirmationOverViewDict.Add("Fee", FeeValueOnConfirmOrder().Text);
                buyConfirmationOverViewDict.Add("FinalQantity", FinalQtyValueOnConfirmOrder().Text);
                buyConfirmationOverViewDict.Add("Date", DateTimeValueOnConfirmOrder().Text);
                return buyConfirmationOverViewDict;
            }
            catch (Exception)
            {

                throw;
            }
        }

        // (Sell Order)This method will hold the text labels name in the dictionary
        public Dictionary<string, string> GetSellConfirmationOverView()
        {
            try
            {
                Dictionary<string, string> sellConfirmationOverViewDict = new Dictionary<string, string>();
                Thread.Sleep(2000);
                sellConfirmationOverViewDict.Add("BTCToSell", BtcToSellValueOnConfirmOrder().Text);
                sellConfirmationOverViewDict.Add("LimitPrice", TransactionCostValue().Text);
                sellConfirmationOverViewDict.Add("Fee", FeeValueOnConfirmOrder().Text);
                sellConfirmationOverViewDict.Add("FinalQantity", FinalQtyValueOnConfirmOrder().Text);
                sellConfirmationOverViewDict.Add("Date", DateTimeValueOnConfirmOrder().Text);
                return sellConfirmationOverViewDict;
            }
            catch (Exception)
            {

                throw;
            }
        }

        // (Buy Order)This method will hold the text labels name from transaction overview in Buy&Sell page
        public Dictionary<string, string> GetBuyTransactionOverView()
        {
            try
            {
                Dictionary<string, string> transactionOverviewDetailsDict = new Dictionary<string, string>();
                Thread.Sleep(2000);
                transactionOverviewDetailsDict.Add("Price", PriceValue().Text);
                transactionOverviewDetailsDict.Add("TransactionCost", TransactionCostValue().Text);
                transactionOverviewDetailsDict.Add("BTCToBuy", BTCToBuyValue().Text);
                transactionOverviewDetailsDict.Add("Fee", FeeValue().Text);
                return transactionOverviewDetailsDict;
            }
            catch (Exception)
            {

                throw;
            }
        }

        // (Sell Order)This method will hold the text labels name from transaction overview in Buy&Sell page
        public Dictionary<string, string> GetSellTransactionOverView()
        {
            try
            {
                Dictionary<string, string> transactionOverviewDetailsDict = new Dictionary<string, string>();
                Thread.Sleep(2000);
                transactionOverviewDetailsDict.Add("Price", PriceValue().Text);
                transactionOverviewDetailsDict.Add("TransactionCost", TransactionCostValue().Text);
                transactionOverviewDetailsDict.Add("BTCToSell", BTCToSellValue().Text);
                transactionOverviewDetailsDict.Add("Fee", FeeValue().Text);
                return transactionOverviewDetailsDict;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // This will place an buy order in BuyAndSell page
        public void PlaceBuyOrder(string instrument, string side)
        {
            VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, logger);
            try
            {
                // Get BTC balance from Balances section on UI and convert into Double format
                string bTCBalanceTextValue = BTCBalances().Text;
                Double bTCBalanceInDoubleValue = Double.Parse(bTCBalanceTextValue);
                string actualBTCBalance = GenericUtils.ConvertToDoubleFormat(bTCBalanceInDoubleValue);
                // Get USD balance from Balances section on UI and convert into Double format
                string usdBalanceTextValue = USDBalances().Text;
                string usdBalance = usdBalanceTextValue.Split(Const.AddDollarSign)[1];
                Thread.Sleep(1000);
                // This method will click on first radio button having option - $100
                RadioBtn();
                Thread.Sleep(1000);
                // Store all the details present in TransactionOverView Section
                var transactionOverviewDetails = GetBuyTransactionOverView();
                // Get price text from transactionOverviewDetails and split based on white space
                var priceText = transactionOverviewDetails["Price"].Split(Const.AddWhiteSpace)[0];
                // Gets "USD" as string to append with currency value for comparison
                string USDCurrency = TestData.GetData("USDCurrency");
                // Get 100DollarTextValue from Amount section on UI and convert into Double format
                string selected100DollarTextValue = selected100USD().Text;
                string selected100DollarPrice = selected100DollarTextValue.Split(Const.AddDollarSign)[1];
                Double selected100DollarDoublePrice = Double.Parse(selected100DollarPrice);
                string selected100DollarPriceValue = GenericUtils.ConvertToDoubleFormat(selected100DollarDoublePrice) + Const.AddWhiteSpace + USDCurrency;
                // Get BTCToBuyTextValue from Amount section on UI and convert into Double format
                string selectedBTCToBuyTextValue = SelectedBTCToBuy().Text;
                string feeCurrency = TestData.GetData("CurrencyName");
                Double selectedBTCToBuyDoubleValue = Double.Parse(selectedBTCToBuyTextValue);
                string selectedBTCToBuyStringValue = GenericUtils.ConvertToDoubleFormat(selectedBTCToBuyDoubleValue);
                string feeValue = CalculateFee(selectedBTCToBuyStringValue);
                string feeValues = feeValue + Const.AddWhiteSpace + feeCurrency;
                Thread.Sleep(1000);

                // This will verify the details of "TransactionCost", "BTCToBuy" and "Fee" components in Buy&Sell page
                if (transactionOverviewDetails["TransactionCost"].Contains(selected100DollarPriceValue)
                    && transactionOverviewDetails["BTCToBuy"].Contains(selectedBTCToBuyStringValue)
                    && transactionOverviewDetails["Fee"].Contains(feeValues))
                {
                    logger.LogCheckPoint(String.Format(LogMessage.TransactionOverviewDetailsPassed, side));
                    UserSetFunctions.Click(BuyBitcoinWithUSDButton());
                }
                else
                {
                    logger.LogCheckPoint(String.Format(LogMessage.TransactionOverviewDetailsFailed, side));
                }

                string placeOrderTime = GenericUtils.GetCurrentTime();
                string placeOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                // This gets the limit Price On Confirmation Order Value
                string limitPriceOnConfirmationOrderValue = GenericUtils.ConvertDoubleToString(selected100DollarDoublePrice) + Const.AddWhiteSpace + USDCurrency;
                string feeOnConfirmationOrderValue = feeCurrency + Const.AddWhiteSpace + feeValue;

                // Get the final quantity amount(btcToBuyAmount-feeValue)
                string finalQtyDiff = GenericUtils.GetDifferenceFromStringAfterSubstraction(selectedBTCToBuyTextValue, feeValue);
                var finalQtyConfirmationOrderValues = feeCurrency + Const.AddWhiteSpace + Decimal.Parse(finalQtyDiff).ToString("0.00");

                Thread.Sleep(2000);
                var buyConfirmationDetails = GetBuyConfirmationOverView();

                // This will verify the details of "BTCToBuy", "LimitPrice", "Fee", "FinalQantity" and "Date" in the confirmation window
                if (buyConfirmationDetails["BTCToBuy"].Contains(selectedBTCToBuyTextValue)
                 && buyConfirmationDetails["LimitPrice"].Contains(selected100DollarPriceValue)
                 && buyConfirmationDetails["Fee"].Contains(feeOnConfirmationOrderValue)
                 && buyConfirmationDetails["FinalQantity"].Contains(finalQtyConfirmationOrderValues)
                 && (buyConfirmationDetails["Date"].Contains(placeOrderTime) || buyConfirmationDetails["Date"].Contains(placeOrderTimePlusOneMin)))
                {
                    logger.LogCheckPoint(String.Format(LogMessage.ConfirmationModalDetailsPassed, side));
                }
                else
                {
                    logger.LogCheckPoint(String.Format(LogMessage.ConfirmationModalDetailsFailed, side));
                }

                // Click on "Confirm Buy Order" button
                UserSetFunctions.Click(ConfirmBuyOrderButton());
                string orderPlacedSuccessMsg = SuccessMsg().Text;

                // This will verify the success message of placed buy order
                try
                {
                    Assert.Equal(orderPlacedSuccessMsg, Const.SuccessBuySellOrderMsg);
                    logger.LogCheckPoint(String.Format(LogMessage.BuySellOrderPassedMsg, side));
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(String.Format(LogMessage.BuySellOrderFailedMsg, side));
                }
                Thread.Sleep(1000);
                for (int i = 0; i <= 2; i++)
                {
                    try
                    {
                        // Click on "OK" button
                        UserSetFunctions.Click(OKBuyButton());
                        break;
                    }
                    catch (StaleElementReferenceException)
                    {

                    }
                }
                for (int i = 0; i <= 2; i++)
                {
                    try
                    {
                        UserSetFunctions.Click(BuyAndSellMenuButton());
                        break;
                    }
                    catch (StaleElementReferenceException)
                    {

                    }
                }

                // Click on exchange Menu button
                UserCommonFunctions.SelectAnExchange(driver);

                //Scroll down to the Filled order tabs
                UserCommonFunctions.ScrollingDownVertical(driver);

                // verify the order placed in Filled orders tab through Order Entry
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTabForBuyAndSell(instrument, side, Double.Parse(selectedBTCToBuyStringValue), feeValue, placeOrderTime, placeOrderTimePlusOneMin));

                Thread.Sleep(1000);

                //Click on Dashboard menu button --> Buy&Sell menu button
                UserCommonFunctions.DashBoardMenuButton(driver);
                Thread.Sleep(1000);
                UserSetFunctions.Click(BuyAndSellButton());
                Thread.Sleep(1000);

                // Calculate the buy BTC amount
                string btcQty = GetBuyBitcoinQuantity(bTCBalanceInDoubleValue, selectedBTCToBuyDoubleValue, feeValue);
                // Calculate the buy USD amount
                string usdBal = Const.AddDollarSign + GetBuyUSDBalances(usdBalance);

                Thread.Sleep(1000);
                // BTC balance from the Balances section post transaction
                string currentbTCQuantity = BTCBalances().Text;
                string finalbTCQuantity = GenericUtils.RemoveCommaFromString(currentbTCQuantity);
                // USD balance from the Balances section post transaction
                string currentUSDBalance = USDBalances().Text;
                string finalUSDBalance = GenericUtils.RemoveCommaFromString(currentUSDBalance);
                string usdDeductionAmount = TestData.GetData("TC42_USDDeductionAmount");

                // This will verify the value of final btc amount with the caluclated amount
                try
                {
                    Assert.Equal(finalbTCQuantity, btcQty);
                    logger.LogCheckPoint(String.Format(LogMessage.FinalBTCQantityPassed, side));
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(String.Format(LogMessage.FinalBTCQantityPassed, side));
                    throw;
                }

                // This will verify the value of final USD balance with the caluclated amount
                try
                {
                    Assert.Equal(finalUSDBalance, usdBal);
                    logger.LogCheckPoint(String.Format(LogMessage.FinalUSDBalancePassed, side, usdDeductionAmount));
                }

                catch (Exception)
                {
                    logger.LogCheckPoint(String.Format(LogMessage.FinalUSDBalanceFailed, side, usdDeductionAmount));
                    throw;
                }
            }

            catch (Exception)
            {
                throw;
            }
        }
            
        // This method place an sell order in BuyAndSell page
        public void PlaceSellOrder(string instrument, string side)
        {
            string bTCBalanceTextValue;
            string feeFactor;
            string actualBTCBalance;
            string usdBalanceTextValue;
            string usdBalance;
            string USDCurrency;
            string selected100DollarTextValue;
            string selected100DollarPrice;
            Double selected100DollarDoublePrice;
            Double selectedBTCToSellDoubleValue;
            string selected100DollarPriceValue;
            string selectedBTCToSellTextValue;
            string feeCurrency;
            string selectedBTCToSellStringValue;
            string feeValue;
            string feeValues;
            string orderPlacedSuccessMsg;
            string placeOrderTime;
            string placeOrderTimePlusOneMin;
            string limitPriceOnConfirmationOrderValue;
            string feeOnConfirmationOrderValue;
            string btcToSellAmount;
            string btcQty;
            string usdBal;
            string currentbTCQuantity;
            string finalbTCQuantity;
            string currentUSDBalance;
            string finalUSDBalance;
            string usdDeductionAmount;
            VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, logger);

            try
            {
                // Get BTC balance from Balances section on UI and convert into Double format
                bTCBalanceTextValue = BTCBalances().Text;
                feeFactor = TestData.GetData("FeeComponent");
                Double bTCBalanceInDoubleValue = Double.Parse(bTCBalanceTextValue);
                actualBTCBalance = GenericUtils.ConvertToDoubleFormat(bTCBalanceInDoubleValue);

                // Get USD balance from Balances section on UI and convert into Double format
                usdBalanceTextValue = USDBalances().Text;
                usdBalance = usdBalanceTextValue.Split(Const.AddDollarSign)[1];
                Thread.Sleep(1000);

                // This method will click on first radio button having option - $100
                RadioBtn();
                Thread.Sleep(1000);

                // Store all the details present in TransactionOverView Section
                var transactionOverviewDetails = GetSellTransactionOverView();

                // Get price text from transactionOverviewDetails and split based on white space
                var priceText = transactionOverviewDetails["Price"].Split(Const.AddWhiteSpace)[0];
                // Gets "USD" as string to append with currency value for comparison
                USDCurrency = TestData.GetData("USDCurrency");
                // Get 100DollarTextValue from Amount section on UI and convert into Double format
                selected100DollarTextValue = selected100USD().Text;
                selected100DollarPrice = selected100DollarTextValue.Split(Const.AddDollarSign)[1];

                selected100DollarDoublePrice = Double.Parse(selected100DollarPrice);
                selected100DollarPriceValue = GenericUtils.ConvertToDoubleFormat(selected100DollarDoublePrice) + Const.AddWhiteSpace + USDCurrency;

                // Get BTCToSellTextValue from Amount section on UI and convert into Double format
                selectedBTCToSellTextValue = SelectedBTCToSell().Text;
                feeCurrency = TestData.GetData("CurrencyName");

                selectedBTCToSellDoubleValue = Double.Parse(selectedBTCToSellTextValue);
                selectedBTCToSellStringValue = GenericUtils.ConvertToDoubleFormat(selectedBTCToSellDoubleValue);

                // Get calcualted fee
                feeValue = GenericUtils.SellFeeAmount(selectedBTCToSellStringValue, priceText, feeFactor);
                feeValues = feeValue + Const.AddWhiteSpace + USDCurrency;
                Thread.Sleep(1000);

                // This will verify the details of "TransactionCost", "BTCToSell" and "Fee" components in Buy&Sell page
                if (transactionOverviewDetails["TransactionCost"].Contains(selected100DollarPriceValue) && transactionOverviewDetails["BTCToSell"].Contains(selectedBTCToSellStringValue)
                 && transactionOverviewDetails["Fee"].Contains(feeValues))
                {
                    logger.LogCheckPoint(String.Format(LogMessage.TransactionOverviewDetailsPassed, side));
                    UserSetFunctions.Click(SellBitcoinWithUSDButton());
                }
                else
                {
                    logger.LogCheckPoint(String.Format(LogMessage.TransactionOverviewDetailsFailed, side));
                }

                placeOrderTime = GenericUtils.GetCurrentTime();
                placeOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                limitPriceOnConfirmationOrderValue = GenericUtils.ConvertToDoubleFormat(selected100DollarDoublePrice) + Const.AddWhiteSpace + USDCurrency;
                feeOnConfirmationOrderValue = USDCurrency + Const.AddWhiteSpace + feeValue;
                btcToSellAmount = BtcToSellValueOnConfirmOrder().Text;

                // Calculated Sell BTC amount
                var finalQtyConfirmationOrderValues = selected100DollarDoublePrice - double.Parse(feeValue);

                Thread.Sleep(1000);
                // Calculated buy USD amount
                var finalQtyConfirmationOrderValue = USDCurrency + Const.AddWhiteSpace + GenericUtils.ConvertTo8DigitAfterDecimal(finalQtyConfirmationOrderValues);
                Thread.Sleep(2000);

                // Store all the details present in TransactionOverView Section
                var sellConfirmationDetails = GetSellConfirmationOverView();

                // This will verify the details of "BTCToSell", "LimitPrice", "Fee", "FinalQantity" and "Date" in the confirmation window
                if (sellConfirmationDetails["BTCToSell"].Contains(btcToSellAmount)
                    && sellConfirmationDetails["LimitPrice"].Contains(limitPriceOnConfirmationOrderValue)
                    && sellConfirmationDetails["Fee"].Contains(feeOnConfirmationOrderValue)
                    && sellConfirmationDetails["FinalQantity"].Contains(finalQtyConfirmationOrderValue)
                    && (sellConfirmationDetails["Date"].Contains(placeOrderTime) || sellConfirmationDetails["Date"].Contains(placeOrderTimePlusOneMin)))
                {
                    logger.LogCheckPoint(String.Format(LogMessage.ConfirmationModalDetailsPassed, side));
                }
                else
                {
                    logger.LogCheckPoint(String.Format(LogMessage.ConfirmationModalDetailsFailed, side));
                }

                //Click on "Confirm Sell Order" button
                UserSetFunctions.Click(ConfirmSellOrderButton());
                orderPlacedSuccessMsg = SuccessMsg().Text;

                // This will verify the success message of placed sell order
                try
                {
                    Assert.Equal(orderPlacedSuccessMsg, Const.SuccessBuySellOrderMsg);
                    logger.LogCheckPoint(String.Format(LogMessage.BuySellOrderPassedMsg, side));
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(String.Format(LogMessage.BuySellOrderFailedMsg, side));
                }
                Thread.Sleep(3000);

                // Click on "OK" button
                UserSetFunctions.Click(OKSellButton());
                Thread.Sleep(1000);

                // Click on "Buy&Sell" menu button --> exchange button--> scroll down to filled order tab
                UserSetFunctions.Click(BuyAndSellMenuButton());
                Thread.Sleep(1000);
                UserCommonFunctions.SelectAnExchange(driver);
                Thread.Sleep(1000);
                UserCommonFunctions.ScrollingDownVertical(driver);

                // This will verify filled orders tab after placing successfull sell order
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTabForBuyAndSell(instrument, side, Double.Parse(selectedBTCToSellStringValue), feeValue, placeOrderTime, placeOrderTimePlusOneMin));
                Thread.Sleep(1000);
                UserCommonFunctions.DashBoardMenuButton(driver);
                Thread.Sleep(1000);
                UserSetFunctions.Click(BuyAndSellButton());
                Thread.Sleep(1000);

                // Get the final sell quantity amount(btcBalance-btcToSellAmount)

                btcQty = GetSellBitcoinQuantity(bTCBalanceInDoubleValue, selectedBTCToSellDoubleValue);
                usdBal = Const.AddDollarSign + GetSellUSDBalances(usdBalance, feeValue);

                Thread.Sleep(1000);

                // BTC balance from the Balances section post transaction
                currentbTCQuantity = BTCBalances().Text;
                finalbTCQuantity = GenericUtils.RemoveCommaFromString(currentbTCQuantity);

                // USD balance from the Balances section post transaction
                currentUSDBalance = USDBalances().Text;
                finalUSDBalance = GenericUtils.RemoveCommaFromString(currentUSDBalance);
                usdDeductionAmount = TestData.GetData("TC42_USDDeductionAmount");

                // This will verify the value of final btc quantity
                try
                {
                    Assert.Equal(finalbTCQuantity, btcQty);
                    logger.LogCheckPoint(String.Format(LogMessage.FinalBTCQantityPassed, side));
                }

                catch (Exception)
                {
                    logger.LogCheckPoint(String.Format(LogMessage.FinalBTCQantityFailed, side));
                    throw;
                }

                // This will verify the value of final USD balance
                try
                {
                    Assert.Equal(finalUSDBalance, usdBal);
                    logger.LogCheckPoint(String.Format(LogMessage.FinalUSDBalancePassed, side, usdDeductionAmount));
                }

                catch (Exception)
                {
                    logger.LogCheckPoint(String.Format(LogMessage.FinalUSDBalanceFailed, side, usdDeductionAmount));
                    throw;
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        // This method verify whether "Make A Transaction" option is present or not
        public bool VerifyMakeATransaction()
        {
            bool flag = false;
            
            try
            {
                flag = true;
                if(MakeTransactionOption().Displayed)
                {
                    logger.LogCheckPoint(String.Format(LogMessage.MakeATransactionOptionPassed));
                }
            }
            catch (Exception)
            {
                logger.LogCheckPoint(String.Format(LogMessage.MakeATransactionOptionFailed));
                throw;
            }
            return flag;
        }

        // This method verify  whether "Chart" option is present or not
        public bool VerifyChart()
        {
            bool flag = false;

            try
            {
                flag = true;
                if (ChartOption().Displayed)
                {
                    logger.LogCheckPoint(String.Format(LogMessage.ChartOptionPassed));
                }
            }
            catch (Exception)
            {
                logger.LogCheckPoint(String.Format(LogMessage.ChartOptionFailed));
                throw;
            }
            return flag;
        }

        // This method  verify  whether "Buy" option is selected by default or not
        public bool VerifyBuyOption()
        {
            bool flag = false;

            try
            {
                flag = true;
                if (BuyButtonOption().Displayed)
                {
                    logger.LogCheckPoint(String.Format(LogMessage.BuyOptionPassed));
                }
            }
            catch (Exception)
            {
                logger.LogCheckPoint(String.Format(LogMessage.BuyOptionFailed));
                throw;
            }
            return flag;
        }

        // This method verify whether "Sell" option is selected by default or not
        public bool VerifySellOption()
        {
            bool flag = false;

            try
            {
                flag = true;
                if (SellButtonOption().Displayed)
                {
                    logger.LogCheckPoint(String.Format(LogMessage.SellOptionPassed));
                }
            }
            catch (Exception)
            {
                logger.LogCheckPoint(String.Format(LogMessage.SellOptionFailed));
                throw;
            }
            return flag;
        }

        // This method verify whether "fith Radio button" option is present or not
        public bool VerifyFifthRadioButtonOption()
        {
            bool flag = false;
            try
            {
                flag = true;
                if (FifthRadioButton().Enabled)
                {
                    logger.LogCheckPoint(String.Format(LogMessage.FifthRadioButtonPassed));
                }
                else
                {
                    logger.LogCheckPoint(String.Format(LogMessage.FifthRadioButtonFailed));
                }
            }
            catch (Exception)
            {               
                throw;
            }
            return flag;
        }

        // This method verify whether fith value with blank value is selected or not
        public bool VerifyFifthWithBlankValues()
        {
            value = TestData.GetData("TC42_Value");
            bool flag = false;

            try
            {
                flag = true;
                string str = BuySelltextField().GetAttribute(value);
                if (str.Contains(Const.RemoveWhiteSpace))
                {
                    logger.LogCheckPoint(String.Format(LogMessage.FifthWithBlankValuesPassed));
                }
                else
                {
                    logger.LogCheckPoint(String.Format(LogMessage.FifthWithBlankValuesFailed));
                }
            }
            catch (Exception)
            {                
                throw;
            }
            return flag;
        }
    }
}
