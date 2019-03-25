using System;
using System.Collections.Generic;
using System.Threading;
using AlphaPoint_QA.Common;
using AlphaPoint_QA.Utils;
using OpenQA.Selenium;
using Xunit;

namespace AlphaPoint_QA.Pages
{
    class AdvancedOrderPage
    {
        ProgressLogger logger;
        static Config data;
        public static IWebDriver driver;

        By buyTab = By.CssSelector("div.advanced-order-sidepane__tab-container > div:nth-of-type(1)");
        By sellTab = By.CssSelector("div.advanced-order-sidepane__tab-container > div:nth-of-type(2)");
        By instrument = By.CssSelector("select[name=instrument]");
        By orderType = By.CssSelector("select[name=orderType]");
        By orderSize = By.CssSelector("div.ap-input__input-box.advanced-order-form__input-box input[name=quantity]");
        By limitPrice = By.CssSelector("div.ap-input__input-box.advanced-order-form__input-box input[name=limitPrice]");
        By stopPrice = By.CssSelector("div.ap-input__input-box.advanced-order-form__input-box input[name=stopPrice]");
        By displayQuntity = By.CssSelector("div.ap-input__input-box.advanced-order-form__input-box input[name=displayQuantity]");
        By placeBuyOrder = By.CssSelector("button.ap-button__btn.ap-button__btn--additive.advanced-order-form__btn.advanced-order-form__btn--additive");
        By placeSellOrder = By.CssSelector("button.ap-button__btn.ap-button__btn--subtractive.advanced-order-form__btn.advanced-order-form__btn--subtractive");
        By askOrBidPrice = By.CssSelector("div.advanced-order-form__limit-price-block-value");
        By askOrBidPriceLabel = By.CssSelector("div.advanced-order-form__limit-price-block > div:nth-Child(1)");
        By pegPrice = By.CssSelector("select[name=pegPriceType]");
        By trailingAmount = By.CssSelector("input[data-test='Trailing Amount:']");
        By limitOffset = By.CssSelector("input[data-test='Limit Offset:']");
        By timeInForce = By.CssSelector("div.form-group.ap-select__select-wrapper.advanced-order-form__select-wrapper select[name=timeInForce]");

        public AdvancedOrderPage(ProgressLogger logger)
        {            
            this.logger = logger;
            data = ConfigManager.Instance;
            driver = AlphaPointWebDriver.GetInstanceOfAlphaPointWebDriver();
        }

        public IWebElement InstrumentDropDown(IWebDriver driver)
        {
            return driver.FindElement(instrument);
        }

        public IWebElement PegPriceDropDown(IWebDriver driver)
        {
            return driver.FindElement(pegPrice);
        }

        public IWebElement LimitOffset(IWebDriver driver)
        {
            return driver.FindElement(limitOffset);
        }

        public IWebElement OrderTypeDropDown(IWebDriver driver)
        {
            return driver.FindElement(orderType);
        }

        public IWebElement OrderSizeEditBox(IWebDriver driver)
        {
            return driver.FindElement(orderSize);
        }

        public IWebElement LimitPriceEditBox(IWebDriver driver)
        {
            return driver.FindElement(limitPrice);
        }

        public IWebElement StopPriceEditBox(IWebDriver driver)
        {
            return driver.FindElement(stopPrice);
        }

        public IWebElement TimeInForce(IWebDriver driver)
        {
            return driver.FindElement(timeInForce);
        }

        public IWebElement TrailingAmountEditBox(IWebDriver driver)
        {
            return driver.FindElement(trailingAmount);
        }

        public IWebElement DisplayQuntityEditBox(IWebDriver driver)
        {
            return driver.FindElement(displayQuntity);
        }

        public IWebElement PlaceBuyOrderButton(IWebDriver driver)
        {
            return driver.FindElement(placeBuyOrder);
        }

        public IWebElement PlaceSellOrderButton(IWebDriver driver)
        {
            return driver.FindElement(placeSellOrder);
        }

        public IWebElement AskOrBidPriceLabel(IWebDriver driver)
        {
            return driver.FindElement(askOrBidPrice);
        }


        public string GetAskOrBidPrice()
        {
            return driver.FindElement(askOrBidPrice).Text;
        }


        public void SelectInstrumentsAndOrderType(string instruments, string orderType)
        {
            try
            {
                UserSetFunctions.VerifyWebElement(InstrumentDropDown(driver));
                UserSetFunctions.SelectDropdown(InstrumentDropDown(driver), instruments);
                UserSetFunctions.VerifyWebElement(OrderTypeDropDown(driver));
                UserSetFunctions.SelectDropdown(OrderTypeDropDown(driver), orderType);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SelectPegPrice(string pegPriceType)
        {
            try
            {
                UserSetFunctions.VerifyWebElement(PegPriceDropDown(driver));
                UserSetFunctions.SelectDropdown(PegPriceDropDown(driver), pegPriceType);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SelectTimeInForce(string timeInForce)
        {
            try
            {
                UserSetFunctions.VerifyWebElement(TimeInForce(driver));
                UserSetFunctions.SelectDropdown(TimeInForce(driver), timeInForce);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void NavigateToAdvanceOrdersSection(IWebDriver driver, string side, string instrument, string orderType)
        {
            try
            {
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.CancelAllOrders(driver);
                logger.LogCheckPoint(String.Format(LogMessage.CancelOrders));
                UserCommonFunctions.AdvanceOrder(driver);
                SelectBuyOrSellTab(side);
                SelectInstrumentsAndOrderType(instrument, orderType);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SelectBuyOrSellTab(string buyOrSell)
        {
            Thread.Sleep(1000);
            try
            {
                if (buyOrSell.Equals(TestData.GetData("BuyTab")))
                {
                    string labeltext = driver.FindElement(askOrBidPriceLabel).Text;
                    if (!labeltext.Contains(Const.AskPrice))
                    {
                        driver.FindElement(buyTab).Click();
                    }
                }
                else if (buyOrSell.Equals(TestData.GetData("SellTab")))
                {
                    driver.FindElement(sellTab).Click();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Dictionary<string, string> PlaceBuyOrderWithReserveOrderType(string orderSize, string limitPrice, string displayQuantity)
        {
            try
            {
                string placeOrderTime;
                string placeOrderTimePlusOneMin;
                string successMsg;
                Dictionary<string, string> reserveBuyOrderDict = new Dictionary<string, string>();
                UserSetFunctions.VerifyWebElement(OrderSizeEditBox(driver));
                UserSetFunctions.EnterText(OrderSizeEditBox(driver), orderSize);
                UserSetFunctions.VerifyWebElement(LimitPriceEditBox(driver));
                UserSetFunctions.EnterText(LimitPriceEditBox(driver), limitPrice);
                UserSetFunctions.VerifyWebElement(DisplayQuntityEditBox(driver));
                UserSetFunctions.EnterText(DisplayQuntityEditBox(driver), displayQuantity);
                UserSetFunctions.VerifyWebElement(PlaceBuyOrderButton(driver));
                UserSetFunctions.Click(PlaceBuyOrderButton(driver));
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, logger);
                placeOrderTime = GenericUtils.GetCurrentTime();
                placeOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                reserveBuyOrderDict.Add("PlaceOrderTime", placeOrderTime);
                reserveBuyOrderDict.Add("PlaceOrderTimePlusOneMin", placeOrderTimePlusOneMin);
                Thread.Sleep(2000);
                return reserveBuyOrderDict;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Dictionary<string, string> PlaceSellOrderWithReserveOrderType(string orderSize, string limitPrice, string displayQuantity)
        {
            try
            {
                string placeOrderTime;
                string placeOrderTimePlusOneMin;
                string successMsg;
                Dictionary<string, string> reserveSellOrderDict = new Dictionary<string, string>();
                UserSetFunctions.VerifyWebElement(OrderSizeEditBox(driver));
                UserSetFunctions.EnterText(OrderSizeEditBox(driver), orderSize);
                UserSetFunctions.VerifyWebElement(LimitPriceEditBox(driver));
                UserSetFunctions.EnterText(LimitPriceEditBox(driver), limitPrice);
                UserSetFunctions.VerifyWebElement(DisplayQuntityEditBox(driver));
                UserSetFunctions.EnterText(DisplayQuntityEditBox(driver), displayQuantity);
                UserSetFunctions.VerifyWebElement(PlaceSellOrderButton(driver));
                UserSetFunctions.Click(PlaceSellOrderButton(driver));
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, logger);
                placeOrderTime = GenericUtils.GetCurrentTime();
                placeOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                reserveSellOrderDict.Add("PlaceOrderTime", placeOrderTime);
                reserveSellOrderDict.Add("PlaceOrderTimePlusOneMin", placeOrderTimePlusOneMin);
                Thread.Sleep(2000);
                return reserveSellOrderDict;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Dictionary<string, string> PlaceMarketBuyOrder(string orderSize)
        {
            try
            {
                Dictionary<string, string> placeBuyOrderDict = new Dictionary<string, string>();
                UserSetFunctions.EnterText(OrderSizeEditBox(driver), orderSize);
                UserSetFunctions.Click(PlaceBuyOrderButton(driver));
                string placeOrderTime = GenericUtils.GetCurrentTime();
                string placeOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                placeBuyOrderDict.Add("PlaceOrderTime", placeOrderTime);
                placeBuyOrderDict.Add("PlaceOrderTimePlusOneMin", placeOrderTimePlusOneMin);
                Thread.Sleep(2000);
                return placeBuyOrderDict;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Dictionary<string, string> PlaceMarketSellOrder(string orderSize)
        {
            try
            {
                Dictionary<string, string> placeSellOrderDict = new Dictionary<string, string>();
                UserSetFunctions.EnterText(OrderSizeEditBox(driver), orderSize);
                UserSetFunctions.Click(PlaceSellOrderButton(driver));
                string placeOrderTime = GenericUtils.GetCurrentTime();
                string placeOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                placeSellOrderDict.Add("PlaceOrderTime", placeOrderTime);
                placeSellOrderDict.Add("PlaceOrderTimePlusOneMin", placeOrderTimePlusOneMin);
                Thread.Sleep(2000);
                return placeSellOrderDict;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Dictionary<string, string> PlaceBuyOrderWithImmediateOrCancelType(string orderSize, string limitPrice)
        {
            try
            {
                Dictionary<string, string> placeBuyOrderDict = new Dictionary<string, string>();
                UserSetFunctions.EnterText(OrderSizeEditBox(driver), orderSize);
                UserSetFunctions.EnterText(LimitPriceEditBox(driver), limitPrice);
                UserSetFunctions.Click(PlaceBuyOrderButton(driver));
                string placeOrderTime = GenericUtils.GetCurrentTime();
                string placeOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                placeBuyOrderDict.Add("PlaceOrderTime", placeOrderTime);
                placeBuyOrderDict.Add("PlaceOrderTimePlusOneMin", placeOrderTimePlusOneMin);
                Thread.Sleep(2000);

                return placeBuyOrderDict;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Dictionary<string, string> PlaceSellOrderWithImmediateOrCancelType(string orderSize, string limitPrice)
        {
            try
            {
                Dictionary<string, string> placeSellOrderDict = new Dictionary<string, string>();
                UserSetFunctions.EnterText(OrderSizeEditBox(driver), orderSize);
                UserSetFunctions.EnterText(LimitPriceEditBox(driver), limitPrice);
                UserSetFunctions.Click(PlaceSellOrderButton(driver));
                string placeOrderTime = GenericUtils.GetCurrentTime();
                string placeOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                placeSellOrderDict.Add("PlaceOrderTime", placeOrderTime);
                placeSellOrderDict.Add("PlaceOrderTimePlusOneMin", placeOrderTimePlusOneMin);
                Thread.Sleep(2000);
                return placeSellOrderDict;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Dictionary<string, string> PlaceStopMarketBuyOrder(string orderSize, string stopPrice)
        {
            try
            {
                Dictionary<string, string> placeBuyOrderDict = new Dictionary<string, string>();
                UserSetFunctions.EnterText(OrderSizeEditBox(driver), orderSize);
                UserSetFunctions.EnterText(StopPriceEditBox(driver), stopPrice);
                UserSetFunctions.Click(PlaceBuyOrderButton(driver));
                string placeOrderTime = GenericUtils.GetCurrentTime();
                string placeOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                placeBuyOrderDict.Add("PlaceOrderTime", placeOrderTime);
                placeBuyOrderDict.Add("PlaceOrderTimePlusOneMin", placeOrderTimePlusOneMin);
                Thread.Sleep(2000);

                return placeBuyOrderDict;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Dictionary<string, string> PlaceStopMarketSellOrder(string orderSize, string stopPrice)
        {
            try
            {
                Dictionary<string, string> placeSellOrderDict = new Dictionary<string, string>();
                UserSetFunctions.EnterText(OrderSizeEditBox(driver), orderSize);
                UserSetFunctions.EnterText(StopPriceEditBox(driver), stopPrice);
                UserSetFunctions.Click(PlaceSellOrderButton(driver));
                string placeOrderTime = GenericUtils.GetCurrentTime();
                string placeOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                placeSellOrderDict.Add("PlaceOrderTime", placeOrderTime);
                placeSellOrderDict.Add("PlaceOrderTimePlusOneMin", placeOrderTimePlusOneMin);
                Thread.Sleep(2000);
                return placeSellOrderDict;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Dictionary<string, string> PlaceStopLimitBuyOrder(string orderSize, string limitPrice, string stopPrice, string timeInForce)
        {
            try
            {
                Dictionary<string, string> placeBuyOrderDict = new Dictionary<string, string>();
                UserSetFunctions.EnterText(OrderSizeEditBox(driver), orderSize);
                UserSetFunctions.EnterText(LimitPriceEditBox(driver), limitPrice);
                UserSetFunctions.EnterText(StopPriceEditBox(driver), stopPrice);
                UserSetFunctions.SelectDropdown(TimeInForce(driver), timeInForce);
                UserSetFunctions.Click(PlaceBuyOrderButton(driver));
                string placeOrderTime = GenericUtils.GetCurrentTime();
                string placeOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                placeBuyOrderDict.Add("PlaceOrderTime", placeOrderTime);
                placeBuyOrderDict.Add("PlaceOrderTimePlusOneMin", placeOrderTimePlusOneMin);
                Thread.Sleep(2000);
                return placeBuyOrderDict;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Dictionary<string, string> PlaceStopLimitSellOrder(string orderSize, string limitPrice, string stopPrice, string timeInForce)
        {
            try
            {
                Dictionary<string, string> placeSellOrderDict = new Dictionary<string, string>();
                UserSetFunctions.EnterText(OrderSizeEditBox(driver), orderSize);
                UserSetFunctions.EnterText(LimitPriceEditBox(driver), orderSize);
                UserSetFunctions.EnterText(StopPriceEditBox(driver), stopPrice);
                UserSetFunctions.SelectDropdown(TimeInForce(driver), timeInForce);
                UserSetFunctions.Click(PlaceSellOrderButton(driver));
                string placeOrderTime = GenericUtils.GetCurrentTime();
                string placeOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                placeSellOrderDict.Add("PlaceOrderTime", placeOrderTime);
                placeSellOrderDict.Add("PlaceOrderTimePlusOneMin", placeOrderTimePlusOneMin);
                Thread.Sleep(2000);
                return placeSellOrderDict;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public Dictionary<string, string> PlaceTrailingStopMarketBuyOrder(IWebDriver driver, string orderSize, string trailingAmount, string pegPriceValue)
        {
            try
            {
                Dictionary<string, string> placeBuyOrderDict = new Dictionary<string, string>();
                UserSetFunctions.EnterText(OrderSizeEditBox(driver), orderSize);
                UserSetFunctions.EnterText(TrailingAmountEditBox(driver), trailingAmount);
                SelectPegPrice(pegPriceValue);
                UserSetFunctions.Click(PlaceBuyOrderButton(driver));
                string placeOrderTime = GenericUtils.GetCurrentTime();
                string placeOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                placeBuyOrderDict.Add("PlaceOrderTime", placeOrderTime);
                placeBuyOrderDict.Add("PlaceOrderTimePlusOneMin", placeOrderTimePlusOneMin);
                Thread.Sleep(2000);
                return placeBuyOrderDict;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Dictionary<string, string> PlaceTrailingStopMarketSellOrder(string orderSize, string trailingAmount, string pegPriceValue)
        {
            try
            {
                Dictionary<string, string> placeSellOrderDict = new Dictionary<string, string>();
                UserSetFunctions.EnterText(OrderSizeEditBox(driver), orderSize);
                UserSetFunctions.EnterText(TrailingAmountEditBox(driver), trailingAmount);
                SelectPegPrice(pegPriceValue);
                UserSetFunctions.Click(PlaceSellOrderButton(driver));
                string placeOrderTime = GenericUtils.GetCurrentTime();
                string placeOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                placeSellOrderDict.Add("PlaceOrderTime", placeOrderTime);
                placeSellOrderDict.Add("PlaceOrderTimePlusOneMin", placeOrderTimePlusOneMin);
                Thread.Sleep(2000);
                return placeSellOrderDict;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Dictionary<string, string> PlaceTrailingStopLimitBuyOrder(string orderSize, string trailingAmount, string limitOffset, string pegPriceValue, string timeInForce)
        {
            try
            {
                Dictionary<string, string> placeBuyOrderDict = new Dictionary<string, string>();
                UserSetFunctions.EnterText(OrderSizeEditBox(driver), orderSize);
                UserSetFunctions.EnterText(TrailingAmountEditBox(driver), trailingAmount);
                UserSetFunctions.EnterText(LimitOffset(driver), limitOffset);
                SelectPegPrice(pegPriceValue);
                SelectTimeInForce(timeInForce);
                UserSetFunctions.Click(PlaceBuyOrderButton(driver));
                string placeOrderTime = GenericUtils.GetCurrentTime();
                string placeOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                placeBuyOrderDict.Add("PlaceOrderTime", placeOrderTime);
                placeBuyOrderDict.Add("PlaceOrderTimePlusOneMin", placeOrderTimePlusOneMin);
                Thread.Sleep(2000);
                return placeBuyOrderDict;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Dictionary<string, string> PlaceTrailingStopLimitSellOrder(string orderSize, string trailingAmount, string limitOffset, string pegPriceValue, string timeInForce)
        {
            try
            {
                Dictionary<string, string> placeSellOrderDict = new Dictionary<string, string>();
                UserSetFunctions.EnterText(OrderSizeEditBox(driver), orderSize);
                UserSetFunctions.EnterText(TrailingAmountEditBox(driver), trailingAmount);
                UserSetFunctions.EnterText(LimitOffset(driver), limitOffset);
                SelectPegPrice(pegPriceValue);
                SelectTimeInForce(timeInForce);
                UserSetFunctions.Click(PlaceSellOrderButton(driver));
                string placeOrderTime = GenericUtils.GetCurrentTime();
                string placeOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                placeSellOrderDict.Add("PlaceOrderTime", placeOrderTime);
                placeSellOrderDict.Add("PlaceOrderTimePlusOneMin", placeOrderTimePlusOneMin);
                Thread.Sleep(2000);
                return placeSellOrderDict;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}