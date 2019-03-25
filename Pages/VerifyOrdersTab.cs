using AlphaPoint_QA.Common;
using AlphaPoint_QA.pages;
using AlphaPoint_QA.Utils;
using OpenQA.Selenium;
using System;
using System.Collections;
using System.Threading;

namespace AlphaPoint_QA.Pages
{
    class VerifyOrdersTab
    {
        IWebDriver driver;
        ProgressLogger logger;

        public VerifyOrdersTab(IWebDriver driver, ProgressLogger logger)
        {
            this.driver = driver;
            this.logger = logger;
        }

        By orderRows = By.CssSelector("div.flex-table__body.order-history-table__body > div");
        By amountCurrencyName = By.CssSelector("div.form-group.ap-input__input-wrapper.order-entry__input-wrapper:nth-of-type(2) div span.label-in-input.ap-input__label-in-input.order-entry__label-in-input");
        By priceCurrencyName = By.CssSelector("div.form-group.ap-input__input-wrapper.order-entry__input-wrapper:nth-of-type(3) div span.label-in-input.ap-input__label-in-input.order-entry__label-in-input");
        By countOfTradeOrderColumnList = By.CssSelector("div.flex-table__body.order-history-table__body > div.flex-table__row.order-history-table__row > div");
        By cancelBlockTradeReportOrderMsg = By.CssSelector("div.snackbar__text.snackbar__text--success.custom-snackbar__text.custom-snackbar__text--success");
        By cancelButton = By.CssSelector("div.flex-table__body.order-history-table__body > div:nth-of-type(1) > div:nth-of-type(8) button");

        public int CountOfOrderRows()
        {
            return driver.FindElements(orderRows).Count;
        }

        public string AmountCurrencyNameText()
        {
            return driver.FindElement(amountCurrencyName).Text;
        }

        public string PriceCurrencyNameText()
        {
            return driver.FindElement(priceCurrencyName).Text;
        }

        public int CountOfTradeOrderColumnList()
        {
            return driver.FindElements(countOfTradeOrderColumnList).Count;
        }

        public IWebElement OpenOrdersCancelButton()
        {
            Thread.Sleep(2000);
            return driver.FindElement(cancelButton);
        }

        // This method will verify the order placed in Filled orders tab through Order Entry 
        public bool VerifyFilledOrdersTab(string instrument, string side, double size, string fee, string placeOrderTime, string placeOrderTimePlusOneMin)
        {
            string lastPrice;
            double doubleLastPrice;
            string expectedRow_1;
            string currencyText;
            string totalAmountCalculated;
            string buyAmountValue;
            double feeValueInDouble;
            string feeValue;
            string expectedRow_2;
            try
            {
                var flag = false;
                OrderEntryPage orderEntryPage = new OrderEntryPage(driver, logger);
                buyAmountValue = GenericUtils.ConvertToDoubleFormat(size);
                feeValueInDouble = Double.Parse(fee);
                feeValue = GenericUtils.ConvertToDoubleFormat(feeValueInDouble);
                if (side.Equals(TestData.GetData("BuyTab")))
                {
                    orderEntryPage.SelectBuyLimitButton();
                    currencyText = AmountCurrencyNameText();
                }
                else
                {
                    orderEntryPage.SelectSellLimitButton();
                    currencyText = PriceCurrencyNameText();
                }

                lastPrice = orderEntryPage.GetLastPrice();
                doubleLastPrice = Convert.ToDouble(lastPrice);
                totalAmountCalculated = GenericUtils.FilledOrdersTotalAmount(size, doubleLastPrice);
                Thread.Sleep(2000);
                UserCommonFunctions.FilledOrderTab(driver);
                expectedRow_1 = instrument + " || " + side + " || " + buyAmountValue + " || " + lastPrice + " || " + totalAmountCalculated + " || " + feeValue + " " + currencyText;
                expectedRow_2 = instrument + " || " + side + " || " + buyAmountValue + " || " + lastPrice + " || " + totalAmountCalculated + " || " + feeValue + " " + currencyText;
                var filledOrdersList = GetListOfFilledOrders();
                if (filledOrdersList.Contains(expectedRow_1) || filledOrdersList.Contains(expectedRow_2))
                {
                    flag = true;
                }
                if (flag)
                {
                    logger.LogCheckPoint(String.Format(LogMessage.OrderVerifiedInFilledOrdersTab, side));
                }
                else
                {
                    logger.LogCheckPoint(String.Format(LogMessage.OrderNotFoundInFilledOrdersTab, side));
                }
                return flag;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // This method will verify the order placed in Filled orders tab through Order Entry 
        public bool VerifyFilledOrdersTabForTrailingOrders(string instrument, string side, double size, string lastPrice, string fee, string placeOrderTime, string placeOrderTimePlusOneMin)
        {
            var flag = false;
            string currencyText;
            double doubleLastPrice;
            string totalAmountCalculated;
            string buyAmountValue;
            double feeValueInDouble;
            string feeValue;
            try
            {
                OrderEntryPage orderEntryPage = new OrderEntryPage(driver, logger);
                buyAmountValue = GenericUtils.ConvertToDoubleFormat(size);
                feeValueInDouble = Double.Parse(fee);
                feeValue = GenericUtils.ConvertToDoubleFormat(feeValueInDouble);
                if (side.Equals(TestData.GetData("BuyTab")))
                {
                    orderEntryPage.SelectBuyLimitButton();
                    currencyText = AmountCurrencyNameText();
                }
                else
                {
                    orderEntryPage.SelectSellLimitButton();
                    currencyText = PriceCurrencyNameText();
                }
                doubleLastPrice = Convert.ToDouble(lastPrice);
                totalAmountCalculated = GenericUtils.FilledOrdersTotalAmount(size, doubleLastPrice);
                Thread.Sleep(2000);
                UserCommonFunctions.FilledOrderTab(driver);
                string expectedRow_1 = instrument + " || " + side + " || " + buyAmountValue + " || " + lastPrice + " || " + totalAmountCalculated + " || " + feeValue + " " + currencyText;
                string expectedRow_2 = instrument + " || " + side + " || " + buyAmountValue + " || " + lastPrice + " || " + totalAmountCalculated + " || " + feeValue + " " + currencyText;
                var filledOrdersList = GetListOfFilledOrders();

                // This will verify the expected details with actual in filled orders tab
                if (filledOrdersList.Contains(expectedRow_1) || filledOrdersList.Contains(expectedRow_2))
                {
                    flag = true;
                }
                if (flag)
                {
                    logger.LogCheckPoint(String.Format(LogMessage.OrderVerifiedInFilledOrdersTab, side));
                }
                else
                {
                    logger.LogCheckPoint(String.Format(LogMessage.OrderNotFoundInFilledOrdersTab, side));
                }
                return flag;
            }
            catch (Exception)
            {
                throw;
            }
        }


        // This method will verify the order placed in Filled orders tab through Order Entry 
        public bool VerifyFilledOrdersTabForBuyAndSell(string instrument, string side, double size, string fee, string placeOrderTime, string placeOrderTimePlusOneMin)
        {
            var flag = false;
            string currencyText;
            string buyAmountValue;
            string lastPrice;
            double doubleLastPrice;
            string totalAmountCalculated;
            string expectedRow_1;
            try
            {
                OrderEntryPage orderEntryPage = new OrderEntryPage(driver, logger);
                buyAmountValue = GenericUtils.ConvertToDoubleFormat(size);
                if (side.Equals(TestData.GetData("BuyTab")))
                {
                    orderEntryPage.SelectBuyLimitButton();
                    currencyText = AmountCurrencyNameText();
                }
                else
                {
                    orderEntryPage.SelectSellLimitButton();
                    currencyText = PriceCurrencyNameText();
                }

                lastPrice = orderEntryPage.GetLastPrice();
                doubleLastPrice = Convert.ToDouble(lastPrice);
                totalAmountCalculated = GenericUtils.FilledOrdersTotalAmount(size, doubleLastPrice);
                Thread.Sleep(2000);

                // Click on Filled order tab
                UserCommonFunctions.FilledOrderTab(driver);
                expectedRow_1 = instrument + " || " + side + " || " + buyAmountValue + " || " + lastPrice + " || " + totalAmountCalculated + " || " + fee + " " + currencyText;
                var filledOrdersList = GetListOfFilledOrders();
                if (filledOrdersList.Contains(expectedRow_1))
                {
                    flag = true;
                }

                // This will verify the expected details with actual in filled orders tab
                if (flag)
                {
                    logger.LogCheckPoint(String.Format(LogMessage.OrderPresentInFilledOrderTabPassed, side));
                }
                else
                {
                    logger.LogCheckPoint(String.Format(LogMessage.OrderPresentInFilledOrderTabFailed, side));
                }
                return flag;
            }
            catch (Exception)
            {
                throw;
            }

        }

        // This method verify order placed in Open orders tab through Order Entry 
        public bool VerifyOpenOrdersTab(string instrument, string side, string type, double size, string price, string placeOrderTime, string placeOrderTimePlusOneMin)
        {
            var flag = false;
            string orderSize;
            string priceValue;
            string expectedRow_1;
            try
            {
                orderSize = GenericUtils.ConvertToDoubleFormat(size);
                priceValue = GenericUtils.ConvertToDoubleFormat(Double.Parse(price));

                UserCommonFunctions.OpenOrderTab(driver);
                expectedRow_1 = instrument + " || " + side + " || " + type + " || " + orderSize + " || " + priceValue;
                var listOfFilledOrders = GetListOfOpenOrders();
                //This will verify the expected details with actual in open orders tab
                if (listOfFilledOrders.Contains(expectedRow_1))
                {
                    logger.LogCheckPoint(string.Format(LogMessage.OpenOrdersVerifiedSuccessfully, side, expectedRow_1));
                    flag = true;
                }
                return flag;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // This method returns the list of all filled orders 
        public ArrayList GetListOfFilledOrders()
        {
            ArrayList filledOrderList = new ArrayList();
            int countOfFilledOrders = CountOfOrderRows();
            for (int i = 1; i <= countOfFilledOrders; i++)
            {
                String textFinal = "";
                int countItems = driver.FindElements(By.XPath("(//div[@class='flex-table__body order-history-table__body']/div)[" + i + "]/div")).Count;
                for (int j = 2; j <= (countItems-1); j++)
                {
                    String text = driver.FindElement(By.XPath("(//div[@class='flex-table__body order-history-table__body']/div)[" + i + "]/div[" + j + "]")).Text;
                    if (j == 2)
                    {
                        textFinal = text;
                    }
                    else
                    {
                        if (j == 8)
                        {
                            continue;
                        }
                        textFinal = textFinal + " || " + text;
                    }

                }
                filledOrderList.Add(textFinal);
            }
            return filledOrderList;
        }

        // This method returns the list of all open orders 
        public ArrayList GetListOfOpenOrders()
        {
            ArrayList openOrderList = new ArrayList();
            int countOfOpenOrders = CountOfOrderRows();
            for (int i = 1; i <= countOfOpenOrders; i++)
            {
                String textFinal = "";
                int countItems = driver.FindElements(By.XPath("(//div[@class='flex-table__body order-history-table__body']/div)[" + i + "]/div")).Count;
                for (int j = 1; j <= (countItems - 3); j++)
                {
                    String text = driver.FindElement(By.XPath("(//div[@class='flex-table__body order-history-table__body']/div)[" + i + "]/div[" + j + "]")).Text;
                    if (j == 1)
                    {
                        textFinal = text;
                    }
                    else
                    {
                        textFinal = textFinal + " || " + text;
                    }

                }
                openOrderList.Add(textFinal);
            }
            return openOrderList;
        }

        // This method will verify the order placed in inactive orders tab through Order Entry 
        public bool VerifyInactiveOrdersTab(string instrument, string side, string type, double size, string price, string placeOrderTime, string placeOrderTimePlusOneMin, string status)
        {
            var flag = false;
            string orderSize;
            string priceValue;
            string expectedRow_1;
            string expectedRow_2;
            try
            {
                orderSize = GenericUtils.ConvertToDoubleFormat(size);
                priceValue = GenericUtils.ConvertToDoubleFormat(Double.Parse(price));

                UserCommonFunctions.InactiveTab(driver);
                expectedRow_1 = instrument + " || " + side + " || " + type + " || " + orderSize + " || " + priceValue + " || " + status;
                expectedRow_2 = instrument + " || " + side + " || " + type + " || " + orderSize + " || " + priceValue + " || " + status;

                var listOfInactiveOrders = GetListOfInactiveOrders();

                //This will verify the expected details with actual in inactive orders tab
                if (listOfInactiveOrders.Contains(expectedRow_1) || listOfInactiveOrders.Contains(expectedRow_2))
                {
                    flag = true;
                }
                if (flag)
                {
                    logger.LogCheckPoint(String.Format(LogMessage.OrderVerifiedInInactiveOrdersTab, side));
                }
                else
                {
                    logger.LogCheckPoint(String.Format(LogMessage.OrderNotFoundInInactiveOrdersTab, side));
                }
                return flag;
            }
            catch (Exception) 
            {
                throw;
            }
        }

        // This method returns the list of all Inactive orders 
        public ArrayList GetListOfInactiveOrders()
        {
            ArrayList inactiveOrderList = new ArrayList();
            int countOfInactiveOrders = CountOfOrderRows();
            for (int i = 1; i <= countOfInactiveOrders; i++)
            {
                String textFinal = "";
                int countItems = driver.FindElements(By.XPath("(//div[@class='flex-table__body order-history-table__body']/div)[" + i + "]/div")).Count;
                for (int j = 1; j <= (countItems); j++)
                {
                    String text = driver.FindElement(By.XPath("(//div[@class='flex-table__body order-history-table__body']/div)[" + i + "]/div[" + j + "]")).Text;
                    if (j == 1)
                    {
                        textFinal = text;
                    }
                    else if (j == 6)
                    {
                        continue;
                    }
                    else
                    {
                        textFinal = textFinal + " || " + text;
                    }                                       
                }
                inactiveOrderList.Add(textFinal);
            }
            return inactiveOrderList;
        }



        // This method is used to wait for disabled button.
        public ArrayList WaitForButtonDisable(String buttonTitle)
        {

            ArrayList dateTimeList = new ArrayList();

            String dateTime = "";
            String dateTimeMinusOne = "";
            for (int i = 0; i <= 100; i++)
            {

                String cssCursorValue = driver.FindElement(By.XPath("//button[text()='" + buttonTitle + "']")).GetCssValue("cursor");
                if (cssCursorValue.Equals(Const.NotAllowedCursorValue))
                {
                    dateTimeList.Add(dateTime);
                    dateTimeList.Add(dateTimeMinusOne);
                    break;
                }
                Thread.Sleep(100);
            }
            return dateTimeList;
        }

        // This method will verify the order placed in Trade Reports tab through Report Block Trade
        public bool VerifyTradeReportsTab(string instrument, string side, string size, string price, string fee, string placeOrderTime, string placeOrderTimePlusOneMin, string status)
        {
            string orderSize;
            string priceValue;
            string feeValue;
            string expectedRow_1;
            string expectedRow_2;
            try
            {
                Thread.Sleep(2000);
                var flag = false;
                orderSize = GenericUtils.ConvertToDoubleFormat(Double.Parse(size));
                priceValue = GenericUtils.ConvertToDoubleFormat(Double.Parse(price));
                feeValue = GenericUtils.ConvertToDoubleFormat(Double.Parse(fee));
                Thread.Sleep(2000);
                UserCommonFunctions.TradeTab(driver);
                expectedRow_1 = instrument + " || " + side +  " || " + orderSize + " || " + priceValue + " || " + feeValue + " || " + placeOrderTime + " || " + status;
                expectedRow_2 = instrument + " || " + side +  " || " + orderSize + " || " + priceValue + " || " + feeValue + " || " + placeOrderTimePlusOneMin + " || " + status;

                var listOfInactiveOrders = GetListOfTradeReports();

                //This will verify the expected details with actual in trade reports tab
                if (listOfInactiveOrders.Contains(expectedRow_1) || listOfInactiveOrders.Contains(expectedRow_2))
                {
                    flag = true;
                }
                if (flag)
                {
                    logger.LogCheckPoint(String.Format(LogMessage.OrderVerifiedInTradeReportsTab, side));
                }
                else
                {
                    logger.LogCheckPoint(String.Format(LogMessage.OrderNotFoundInTradeReportsTab, side));
                }
                return flag;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // This method returns the list of all Trade Reports
        public ArrayList GetListOfTradeReports()
        {
            ArrayList inactiveOrderList = new ArrayList();
            int countOfInactiveOrders = CountOfOrderRows();
            for (int i = 1; i <= countOfInactiveOrders; i++)
            {
                String textFinal = "";
                int countItems = driver.FindElements(By.XPath("(//div[@class='flex-table__body order-history-table__body']/div)[" + i + "]/div")).Count;
                for (int j = 1; j <= (countItems); j++)
                {
                    String text = driver.FindElement(By.XPath("(//div[@class='flex-table__body order-history-table__body']/div)[" + i + "]/div[" + j + "]")).Text;
                    if (j == 1)
                    {
                        textFinal = text;
                    }
                    else
                    {
                        textFinal = textFinal + " || " + text;
                    }

                }
                inactiveOrderList.Add(textFinal);
            }
            return inactiveOrderList;
        }

        // This method returns the list of block trade orders in open orders tab and will perform the cancel on transacted order
        public bool VerifyCancelBlockTradeOrdersInOpenOrderTab()
        {
            bool flag = false;
            int countOfReportBlockTradeOrders;
            try
            {
                flag = true;
                ArrayList reportBlockOrderList = new ArrayList();
                countOfReportBlockTradeOrders = CountOfOrderRows();
                for (int i = 1; i <= countOfReportBlockTradeOrders; i++)
                {
                    String textFinal = "";
                    int countItems = CountOfTradeOrderColumnList();
                    for (int j = 1; j <= countItems; j++)
                    {
                        String text = driver.FindElement(By.XPath("(//div[@class='flex-table__body order-history-table__body']/div)[" + i + "]/div[" + j + "]")).Text;
                        if (j == 8)
                        {
                            UserSetFunctions.Click(OpenOrdersCancelButton());
                            Thread.Sleep(4000);
                            break;
                        }
                        else
                        {
                            textFinal = textFinal + " || " + text;
                        }
                        reportBlockOrderList.Add(textFinal);
                    }
                    break;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return flag;
        }

        // This method returns the list of all open orders 
        public ArrayList GetListOfOpenOrdersForTradeReports()
        {
            ArrayList openOrderList = new ArrayList();
            int countOfOpenOrders = CountOfOrderRows();
            for (int i = 1; i <= countOfOpenOrders; i++)
            {
                String textFinal = "";
                int countItems = driver.FindElements(By.XPath("(//div[@class='flex-table__body order-history-table__body']/div)[" + i + "]/div")).Count;
                for (int j = 1; j <= countItems; j++)
                {
                    String text = driver.FindElement(By.XPath("(//div[@class='flex-table__body order-history-table__body']/div)[" + i + "]/div[" + j + "]")).Text;
                    if (j == 1)
                    {
                        textFinal = text;
                    }
                    else if (j == 6)
                    {
                        continue;
                    }
                    else
                    {
                        textFinal = textFinal + " || " + text;
                    }

                    if (j == 8)
                    {
                        if (OpenOrdersCancelButton().Displayed)
                        {
                            logger.LogCheckPoint(String.Format(LogMessage.CancelButtonPassed));
                        }
                        else
                        {
                            logger.LogCheckPoint(String.Format(LogMessage.CancelButtonFailed));
                        }
                    }
                }
                openOrderList.Add(textFinal);
            }
            return openOrderList;
        }

        // This method will verify the order placed in Trade Reports tab through Report Block Trade
        public bool VerifyBlockTradeReportsInOpenOrderTab(string instrument, string side, string type, string size, string price, string statusWorking, string cancel)
        {
            var flag = false;
            string orderSize;
            string priceValue;
            string expectedRow;
            try
            {
                Thread.Sleep(2000);
                orderSize = GenericUtils.ConvertToDoubleFormat(Double.Parse(size));
                priceValue = GenericUtils.ConvertToDoubleFormat(Double.Parse(price));                
                UserCommonFunctions.OpenOrderTab(driver);
                expectedRow = instrument + " || " + side + " || " + type + " || " + orderSize + " || " + price + " || " + statusWorking + " || " + cancel;
                var listOfInactiveOrders = GetListOfOpenOrdersForTradeReports();
                if (listOfInactiveOrders.Contains(expectedRow))
                {
                    flag = true;
                }
                if (flag)
                {
                    logger.LogCheckPoint(String.Format(LogMessage.OrderVerifiedInOpenOrdersTabPassed, side));
                }
                else
                {
                    logger.LogCheckPoint(String.Format(LogMessage.OrderVerifiedInOpenOrdersTabFailed, side));
                }
                return flag;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
