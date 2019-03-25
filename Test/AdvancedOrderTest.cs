using AlphaPoint_QA.Common;
using AlphaPoint_QA.pages;
using AlphaPoint_QA.Pages;
using AlphaPoint_QA.Utils;
using OpenQA.Selenium;
using System;
using Xunit;
using Xunit.Abstractions;

namespace AlphaPoint_QA.Test
{
    [Collection("Alphapoint_QA_USER")]
    public class AdvancedOrderTest : TestBase
    {
        private string instrument;
        private string orderType;
        private string menuTab;
        private string buyTab;
        private string sellTab;
        private string orderSize;
        private string limitPrice;
        private string marketOrder;
        private string buyOrderLimitPrice;
        private string sellOrderLimitPrice;
        private string timeInForce;
        private string feeComponent;
        private string buyOrderSize;
        private string sellOrderSize;
        private string reserveOrder;
        private string buyOrderDisplayQty;
        private string sellOrderDisplayQty;
        private string stopPrice;
        private string orderTypeDropdown;
        private string limitPriceEqualsStop;
        private string trailingAmount;
        private string pegPriceDropdown;
        private string buyOrderFeeValue;
        private string sellOrderFeeValue;
        private string incSellOrderSize;
        private string decSellOrderSize;
        private string incBuyOrderSize;
        private string decBuyOrderSize;
        private string stopLimitPrice;
        private string equalSellOrderSize;
        private string equalBuyOrderSize;
        private string stopTimeInForce;
        private string triggerTrailingPrice;
        private string finalTrailingPrice;
        private string limitOffset;
        private string pegPrice;
        private string setMarketPrice;
        private string buyLimitPrice1;
        private string buyLimitPrice2;
        private string buyLimitPrice3;
        private string buyLimitPrice4;
        private string buyLimitPrice5;
        private string sellLimitPrice1;
        private string sellLimitPrice2;
        private string sellLimitPrice3;
        private string sellLimitPrice4;
        private string sellLimitPrice5;
        private string iocTimeInForce;
        private string fokTimeInForce;

        public AdvancedOrderTest(ITestOutputHelper output) : base(output)
        {

        }

        [Fact]
        public void TC9_VerifyMarketOrderTypeAdvanceBuyOrder()
        {
            try
            {
                string askPrice;
                string buyOrderSuccessMsg;
                string feeValue;
                instrument = TestData.GetData("Instrument");
                marketOrder = TestData.GetData("MarketOrder");
                menuTab = TestData.GetData("MenuTab");
                sellTab = TestData.GetData("SellTab");
                buyTab = TestData.GetData("BuyTab");
                buyOrderSize = TestData.GetData("TC9_BuyOrderSize");
                sellOrderSize = TestData.GetData("TC9_SellOrderSize");
                limitPrice = TestData.GetData("TC9_LimitPrice");
                timeInForce = TestData.GetData("TC9_TimeInForce");
                feeComponent = TestData.GetData("FeeComponent");
                // Get buy order fee value based on buyOrderSize, feeComponent
                feeValue = GenericUtils.FeeAmount(buyOrderSize, feeComponent);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                AdvancedOrderPage advanceorder = new AdvancedOrderPage(TestProgressLogger);                
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);

                // Create Buy and Sell Order to set the last price
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, limitPrice));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, limitPrice, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, limitPrice));
                // Place sell order to set up market
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, limitPrice, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, sellTab, sellOrderSize, limitPrice));

                // Place Advance Market Buy Order
                // Cancel all previous orders of the User 
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER9);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CancelOrders));
                // Navigate to Advance order section
                UserCommonFunctions.AdvanceOrder(driver);
                advanceorder.SelectBuyOrSellTab(buyTab);
                advanceorder.SelectInstrumentsAndOrderType(instrument, marketOrder);
                var placeMarketBuyOrder = advanceorder.PlaceMarketBuyOrder(buyOrderSize);
                buyOrderSuccessMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                // Verify the success message
                Assert.Equal(Const.OrderSuccessMsg, buyOrderSuccessMsg);
                TestProgressLogger.LogCheckPoint(buyOrderSuccessMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvanceMarketOrderSuccessMsg, buyTab, buyOrderSize));
                // Close the Advance order section
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);

                // Verify the order placed in Filled Orders Tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, buyTab, double.Parse(buyOrderSize), feeValue, placeMarketBuyOrder["PlaceOrderTime"], placeMarketBuyOrder["PlaceOrderTimePlusOneMin"]));

            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.AdvanceMarketOrderFailureMsg, buyTab, buyOrderSize), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.AdvanceMarketOrderFailureMsg, buyTab, buyOrderSize), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }


        [Fact]
        public void TC10_VerifyMarketOrderTypeAdvanceSellOrder()
        {
            try
            {
                string feeValue;
                string sellOrderSuccessMsg;
                instrument = TestData.GetData("Instrument");
                marketOrder = TestData.GetData("MarketOrder");
                menuTab = TestData.GetData("MenuTab");
                sellTab = TestData.GetData("SellTab");
                buyTab = TestData.GetData("BuyTab");
                buyOrderSize = TestData.GetData("TC10_BuyOrderSize");
                sellOrderSize = TestData.GetData("TC10_SellOrderSize");
                limitPrice = TestData.GetData("TC10_LimitPrice");
                timeInForce = TestData.GetData("TC10_TimeInForce");
                feeComponent = TestData.GetData("FeeComponent");
                feeValue = GenericUtils.SellFeeAmount(buyOrderSize, limitPrice, feeComponent);

                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                AdvancedOrderPage advanceorder = new AdvancedOrderPage(TestProgressLogger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);

                TestProgressLogger.StartTest();
                // Login and Place Limit Buy order to set market
                userFunctions.LogIn(TestProgressLogger, Const.USER8);                
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, limitPrice, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, buyTab, buyOrderSize, limitPrice));
                // Login and Place Advance Sell order
                // Cancel all previous orders of the User 
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER9);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CancelOrders));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvanceMarketOrderSetUpStarted, sellTab, sellOrderSize));
                // Navigate to Advance order section
                UserCommonFunctions.AdvanceOrder(driver);
                advanceorder.SelectBuyOrSellTab(sellTab);
                advanceorder.SelectInstrumentsAndOrderType(instrument, marketOrder);
                var placeMarketSellOrder = advanceorder.PlaceMarketSellOrder(sellOrderSize);
                sellOrderSuccessMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                // Verify the success message
                Assert.Equal(Const.OrderSuccessMsg, sellOrderSuccessMsg);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvanceMarketOrderSuccessMsg, sellTab, sellOrderSize));
                // Verify that the order is present in Filled orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, sellTab, double.Parse(sellOrderSize), feeValue, placeMarketSellOrder["PlaceOrderTime"], placeMarketSellOrder["PlaceOrderTimePlusOneMin"]));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.AdvanceMarketOrderFailureMsg, sellTab, sellOrderSize), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.AdvanceMarketOrderFailureMsg, sellTab, sellOrderSize), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }

        [Fact]
        public void TC11_VerifyIOCAdvLimitBuyOrderMultiScenario()
        {
            try
            {
                string successMsg;
                string askPrice;
                string orderSizeDifference;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("TC11_OrderType");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                buyOrderSize = TestData.GetData("TC11_BuyOrderSize");
                sellOrderSize = TestData.GetData("TC11_SellOrderSize");
                incSellOrderSize = TestData.GetData("TC11_IncSellOrderSize");
                decSellOrderSize = TestData.GetData("TC11_DecSellOrderSize");
                limitPrice = TestData.GetData("TC11_LimitPrice");
                timeInForce = TestData.GetData("TC11_TimeInForce");
                feeComponent = TestData.GetData("FeeComponent");
                // Get buy order fee value based on buyOrderSize, feeComponent
                buyOrderFeeValue = GenericUtils.FeeAmount(buyOrderSize, feeComponent);
                sellOrderFeeValue = GenericUtils.SellFeeAmount(sellOrderSize, limitPrice, feeComponent);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);

                // Creating Buy and Sell Order to set the last price
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, limitPrice));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, limitPrice, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, limitPrice));

                // Scenario 1: Place Buy IOC order with buy order size = sell order size
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                // Place Limit Sell Order to set the market
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, limitPrice, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, sellTab, sellOrderSize, limitPrice));
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                // Navigate to Advance order section and place IOC Buy order with order size = sellOrderSize
                advanceOrder.NavigateToAdvanceOrdersSection(driver, buyTab, instrument, orderType);
                var placeIOCBuyOrderTime = advanceOrder.PlaceBuyOrderWithImmediateOrCancelType(buyOrderSize, limitPrice);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                // Verify success message
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedOrderPlacedSuccessfully, buyTab, buyOrderSize, limitPrice));
                // Verify that the order is present in Filled orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, buyTab, double.Parse(buyOrderSize), buyOrderFeeValue, placeIOCBuyOrderTime["PlaceOrderTime"], placeIOCBuyOrderTime["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedEntryInFilledOrdersTab, instrument, buyTab, buyOrderSize, placeIOCBuyOrderTime));

                // Scenario 2: Place Buy IOC order with buy order size < sell order size
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                // Place Limit Sell Order to set the market
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, incSellOrderSize, limitPrice, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, sellTab, incSellOrderSize, limitPrice));
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, buyTab, instrument, orderType);
                // Navigate to Advance order section and place IOC Buy order with order size < sellOrderSize
                var placeIOCBuyOrderTime2 = advanceOrder.PlaceBuyOrderWithImmediateOrCancelType(buyOrderSize, limitPrice);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                // Verify success message
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedOrderPlacedSuccessfully, buyTab, buyOrderSize, limitPrice));
                // Verify that the order is present in Filled orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, buyTab, double.Parse(buyOrderSize), buyOrderFeeValue, placeIOCBuyOrderTime2["PlaceOrderTime"], placeIOCBuyOrderTime2["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedEntryInFilledOrdersTab, instrument, buyTab, buyOrderSize, placeIOCBuyOrderTime2));

                // Scenario 3: Place Buy IOC order with buy order size > sell order size
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                // Place Limit Sell Order to set the market
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, decSellOrderSize, limitPrice, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, sellTab, decSellOrderSize, limitPrice));
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                // Navigate to Advance order section and place IOC Buy order with order size > sellOrderSize
                advanceOrder.NavigateToAdvanceOrdersSection(driver, buyTab, instrument, orderType);
                var placeIOCBuyOrderTime3 = advanceOrder.PlaceBuyOrderWithImmediateOrCancelType(buyOrderSize, limitPrice);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                Assert.Equal(Const.OrderCancelledMsg, successMsg);
                // Get Fee value based on decreased sell order size
                buyOrderFeeValue = GenericUtils.FeeAmount(decSellOrderSize, feeComponent);
                // Get order size difference between buyOrderSize and decreased sellOrderSize
                orderSizeDifference = GenericUtils.GetDifferenceFromStringAfterSubstraction(buyOrderSize, decSellOrderSize);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedOrderPlacedSuccessfully, buyTab, buyOrderSize, limitPrice));
                // Verify that the order is present in Filled orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, buyTab, double.Parse(decSellOrderSize), buyOrderFeeValue, placeIOCBuyOrderTime3["PlaceOrderTime"], placeIOCBuyOrderTime3["PlaceOrderTimePlusOneMin"]));
                // Verify that the order not fulfilled is present in inactive orders tab
                Assert.True(objVerifyOrdersTab.VerifyInactiveOrdersTab(instrument, buyTab, Const.Limit, Double.Parse(buyOrderSize), limitPrice, placeIOCBuyOrderTime3["PlaceOrderTime"], placeIOCBuyOrderTime3["PlaceOrderTimePlusOneMin"], Const.CancelledStatus));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedEntryInFilledOrdersTab, instrument, buyTab, buyOrderSize, placeIOCBuyOrderTime3));
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.IOCOrderTypeFailedMsg, buyTab), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.IOCOrderTypeFailedMsg, buyTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }

        [Fact] 
        public void TC12_VerifyIOCAdvLimitSellOrderMultiScenario()
        {
            try
            {
                string successMsg;
                string askPrice;
                string orderSizeDifference;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("TC12_OrderType");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                buyOrderSize = TestData.GetData("TC12_BuyOrderSize");
                sellOrderSize = TestData.GetData("TC12_SellOrderSize");
                incBuyOrderSize = TestData.GetData("TC12_IncSellOrderSize");
                decBuyOrderSize = TestData.GetData("TC12_DecSellOrderSize");
                limitPrice = TestData.GetData("TC12_LimitPrice");
                timeInForce = TestData.GetData("TC12_TimeInForce");
                feeComponent = TestData.GetData("FeeComponent");
                // Get buy order fee value based on buyOrderSize, feeComponent
                buyOrderFeeValue = GenericUtils.FeeAmount(buyOrderSize, feeComponent);
                sellOrderFeeValue = GenericUtils.SellFeeAmount(sellOrderSize, limitPrice, feeComponent);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);

                // Creating Buy and Sell Order to set the last price
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, limitPrice));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, limitPrice, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, limitPrice));

                // Scenario 1: Place Sell IOC order with sell order size = buy order size
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                // Place Limit Buy Order to set the market
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, limitPrice, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, buyTab, buyOrderSize, limitPrice));
                // Navigate to Advance order section and place IOC Sell order with sell order size = buy order size
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, sellTab, instrument, orderType);
                var placeIOCBuyOrderTime = advanceOrder.PlaceSellOrderWithImmediateOrCancelType(sellOrderSize, limitPrice);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                // Verify the success message
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedOrderPlacedSuccessfully, sellTab, sellOrderSize, limitPrice));
                // Verify that the order is present in Filled orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, sellTab, double.Parse(sellOrderSize), sellOrderFeeValue, placeIOCBuyOrderTime["PlaceOrderTime"], placeIOCBuyOrderTime["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedEntryInFilledOrdersTab, instrument, sellTab, sellOrderSize, placeIOCBuyOrderTime));

                // Scenario 2: Place Sell IOC order with sell order size < buy order size
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                // Place Limit Buy Order to set the market
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, incBuyOrderSize, limitPrice, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, buyTab, incBuyOrderSize, limitPrice));
                // Navigate to Advance order section and place IOC Sell order with sell order size < buy order size
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, sellTab, instrument, orderType);
                var placeIOCBuyOrderTime2 = advanceOrder.PlaceSellOrderWithImmediateOrCancelType(sellOrderSize, limitPrice);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                // Verify the success message
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedOrderPlacedSuccessfully, sellTab, sellOrderSize, limitPrice));
                // Verify that the order is present in Filled orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, sellTab, double.Parse(sellOrderSize), sellOrderFeeValue, placeIOCBuyOrderTime2["PlaceOrderTime"], placeIOCBuyOrderTime2["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedEntryInFilledOrdersTab, instrument, sellTab, sellOrderSize, placeIOCBuyOrderTime2));
                
                // Scenario 3: Place Sell IOC order with sell order size > buy order size
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                // Place Limit Buy Order to set the market
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, decBuyOrderSize, limitPrice, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, buyTab, decBuyOrderSize, limitPrice));
                // Navigate to Advance order section and place IOC Sell order with sell order size > buy order size
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, sellTab, instrument, orderType);
                var placeIOCBuyOrderTime3 = advanceOrder.PlaceSellOrderWithImmediateOrCancelType(sellOrderSize, limitPrice);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                // Verify the success message
                Assert.Equal(Const.OrderCancelledMsg, successMsg);
                // Get the fee value based on decreased buyOrderSize, limitPrice and feeComponent
                sellOrderFeeValue = GenericUtils.SellFeeAmount(decBuyOrderSize, limitPrice, feeComponent);
                // Get the difference between the sellOrderSize and decreased buyOrderSize
                orderSizeDifference = GenericUtils.GetDifferenceFromStringAfterSubstraction(sellOrderSize, decBuyOrderSize);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedOrderPlacedSuccessfully, sellTab, sellOrderSize, limitPrice));
                // Verify that the order is present in Filled orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, sellTab, double.Parse(decBuyOrderSize), sellOrderFeeValue, placeIOCBuyOrderTime3["PlaceOrderTime"], placeIOCBuyOrderTime3["PlaceOrderTimePlusOneMin"]));
                // Verify that the order not fulfilled is present in inactive orders tab
                Assert.True(objVerifyOrdersTab.VerifyInactiveOrdersTab(instrument, sellTab, Const.Limit, Double.Parse(sellOrderSize), limitPrice, placeIOCBuyOrderTime3["PlaceOrderTime"], placeIOCBuyOrderTime3["PlaceOrderTimePlusOneMin"], Const.CancelledStatus));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedEntryInFilledOrdersTab, instrument, sellTab, sellOrderSize, placeIOCBuyOrderTime3));
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.IOCOrderTypeFailedMsg, sellTab), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.IOCOrderTypeFailedMsg, sellTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }

        [Fact] 
        public void TC15_VerifyFOKAdvStopLimitBuyOrderMultiScenario()
        {
            try
            {
                string successMsg;
                string askPrice;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("StopLimitOrder");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                buyOrderSize = TestData.GetData("TC15_BuyOrderSize");
                sellOrderSize = TestData.GetData("TC15_SellOrderSize");
                incSellOrderSize = TestData.GetData("TC15_IncSellOrderSize");
                decSellOrderSize = TestData.GetData("TC15_DecSellOrderSize");
                limitPrice = TestData.GetData("TC15_LimitPrice");
                timeInForce = TestData.GetData("TC15_TimeInForce");
                stopPrice = TestData.GetData("TC15_StopPrice");
                limitPriceEqualsStop = TestData.GetData("TC15_LimitPriceEqualsStopPrice");
                feeComponent = TestData.GetData("FeeComponent");
                stopLimitPrice = TestData.GetData("TC15_StopLimitPrice");
                equalSellOrderSize = TestData.GetData("TC15_EqualSellOrderSize");
                stopTimeInForce = TestData.GetData("TC15_StopTimeInForce");

                // Get buy order fee value based on buyOrderSize, feeComponent
                buyOrderFeeValue = GenericUtils.FeeAmount(buyOrderSize, feeComponent);
                sellOrderFeeValue = GenericUtils.SellFeeAmount(sellOrderSize, limitPrice, feeComponent);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                OrderEntryPage orderEntryPage = new OrderEntryPage(driver, TestProgressLogger);

                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
                // Creating Buy and Sell Order to set the last price
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, limitPrice));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, limitPrice, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, limitPrice));
                
                // Scenario 1: Place Buy Stop Limit order with buy order size = sell order size
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                // Place limit sell order to set the market
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, limitPrice, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, sellTab, sellOrderSize, limitPrice));
                // Navigate to Advance orders section and Place Stop LimitBuyOrder with stopLimitPrice > stopPrice > limitPrice
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, buyTab, instrument, orderType);
                var placeStopLimitBuyOrderTime = advanceOrder.PlaceStopLimitBuyOrder(buyOrderSize, stopLimitPrice, stopPrice, stopTimeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                // Verify success message
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedStopLimitOrderPlacedSuccessfully, buyTab, buyOrderSize, stopLimitPrice, stopPrice, stopTimeInForce));
                // Verify the order is present in Open orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, buyTab, Const.StopLimit, Double.Parse(buyOrderSize), stopPrice, placeStopLimitBuyOrderTime["PlaceOrderTime"], placeStopLimitBuyOrderTime["PlaceOrderTimePlusOneMin"]));                
                // Place market buy order so that the sell order gets filled
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                orderEntryPage.PlaceMarketBuyOrder(instrument, buyTab, Double.Parse(buyOrderSize));
                // Place limit buy order with limitPrice=StopPrice
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, limitPriceEqualsStop, timeInForce);
                // Place limit sell order with limitPrice=StopPrice and order size such that Stop LimitBuyOrder gets filled
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, equalSellOrderSize, limitPriceEqualsStop, timeInForce);

                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                // Verify that the order is present in Filled orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, buyTab, Double.Parse(buyOrderSize), buyOrderFeeValue, placeStopLimitBuyOrderTime["PlaceOrderTime"], placeStopLimitBuyOrderTime["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvStopLimitFilledOrder, buyTab, buyOrderSize, stopLimitPrice, stopPrice, stopTimeInForce));

                // Scenario 2: Place Buy Stop Limit order with buy order size < sell order size
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, limitPrice));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, limitPrice, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, limitPrice));
                // Place limit sell order to set the market
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, limitPrice, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, sellTab, sellOrderSize, limitPrice));
                // Navigate to Advance orders section and Place Stop LimitBuyOrder with stopLimitPrice > stopPrice > limitPrice
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, buyTab, instrument, orderType);
                var placeStopLimitBuyOrderTime2 = advanceOrder.PlaceStopLimitBuyOrder(buyOrderSize, stopLimitPrice, stopPrice, stopTimeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                // Verify success message
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedStopLimitOrderPlacedSuccessfully, buyTab, buyOrderSize, stopLimitPrice, stopPrice, stopTimeInForce));
                // Verify the order is present in Open orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, buyTab, Const.StopLimit, Double.Parse(buyOrderSize), stopPrice, placeStopLimitBuyOrderTime2["PlaceOrderTime"], placeStopLimitBuyOrderTime2["PlaceOrderTimePlusOneMin"]));
                // Place market buy order so that the sell order gets filled
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                orderEntryPage.PlaceMarketBuyOrder(instrument, buyTab, Double.Parse(buyOrderSize));
                // Place limit buy order with limitPrice=StopPrice
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, limitPriceEqualsStop, timeInForce);
                // Place limit sell order with limitPrice=StopPrice and increased order size 
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, incSellOrderSize, limitPriceEqualsStop, timeInForce);

                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                // Verify that the order is present in Filled orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, buyTab, double.Parse(buyOrderSize), buyOrderFeeValue, placeStopLimitBuyOrderTime2["PlaceOrderTime"], placeStopLimitBuyOrderTime2["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvStopLimitFilledOrder, buyTab, buyOrderSize, stopLimitPrice, stopPrice, stopTimeInForce));
                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER7);

                // Scenario 3: Place Buy Stop Limit order with buy order size > sell order size
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, limitPrice));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, limitPrice, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, limitPrice));
                // Place limit sell order to set the market
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, limitPrice, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, sellTab, sellOrderSize, limitPrice));
                // Navigate to Advance orders section and Place Stop LimitBuyOrder with stopLimitPrice > stopPrice > limitPrice
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, buyTab, instrument, orderType);
                var placeStopLimitBuyOrderTime3 = advanceOrder.PlaceStopLimitBuyOrder(buyOrderSize, stopLimitPrice, stopPrice, stopTimeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                // Verify success message
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedStopLimitOrderPlacedSuccessfully, buyTab, buyOrderSize, stopLimitPrice, stopPrice, stopTimeInForce));
                // Verify the order is present in Open orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, buyTab, Const.StopLimit, Double.Parse(buyOrderSize), stopPrice, placeStopLimitBuyOrderTime3["PlaceOrderTime"], placeStopLimitBuyOrderTime3["PlaceOrderTimePlusOneMin"]));
                // Place market buy order so that the sell order gets filled
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                orderEntryPage.PlaceMarketBuyOrder(instrument, buyTab, Double.Parse(buyOrderSize));
                // Place limit buy order with limitPrice=StopPrice
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, limitPriceEqualsStop, timeInForce);
                // Place limit sell order with limitPrice=StopPrice and decreased order size 
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, decSellOrderSize, limitPriceEqualsStop, timeInForce);

                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                // Verify that the order not fulfilled is present in inactive orders tab
                Assert.True(objVerifyOrdersTab.VerifyInactiveOrdersTab(instrument, buyTab, Const.StopLimit, Double.Parse(buyOrderSize), stopPrice, placeStopLimitBuyOrderTime3["PlaceOrderTime"], placeStopLimitBuyOrderTime3["PlaceOrderTimePlusOneMin"], Const.CancelledStatus));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvStopLimitInactiveOrder, buyTab, Const.StopLimit, buyOrderSize, stopPrice));
                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER7);
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.FOKOrderTypeFailedMsg, buyTab), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.FOKOrderTypeFailedMsg, buyTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }

        [Fact]
        public void TC16_VerifyFOKAdvStopLimitSellOrderMultiScenario()
        {
            try
            {
                string successMsg;
                string askPrice;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("StopLimitOrder");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                buyOrderSize = TestData.GetData("TC16_BuyOrderSize");
                sellOrderSize = TestData.GetData("TC16_SellOrderSize");
                incBuyOrderSize = TestData.GetData("TC16_IncBuyOrderSize");
                decBuyOrderSize = TestData.GetData("TC16_DecBuyOrderSize");
                limitPrice = TestData.GetData("TC16_LimitPrice");
                timeInForce = TestData.GetData("TC16_TimeInForce");
                stopPrice = TestData.GetData("TC16_StopPrice");
                limitPriceEqualsStop = TestData.GetData("TC16_LimitPriceEqualsStopPrice");
                feeComponent = TestData.GetData("FeeComponent");
                stopLimitPrice = TestData.GetData("TC16_StopLimitPrice");
                equalBuyOrderSize = TestData.GetData("TC16_EqualBuyOrderSize");
                stopTimeInForce = TestData.GetData("TC16_StopTimeInForce");

                // Get buy order fee value based on buyOrderSize, feeComponent
                buyOrderFeeValue = GenericUtils.FeeAmount(buyOrderSize, feeComponent);
                sellOrderFeeValue = GenericUtils.SellFeeAmount(sellOrderSize, stopPrice, feeComponent);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                OrderEntryPage orderEntryPage = new OrderEntryPage(driver, TestProgressLogger);
            
                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
                // Creating Buy and Sell Order to set the last price
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, limitPrice));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, limitPrice, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, limitPrice));

                // Scenario 1: Place Sell Stop Limit order with sell order size = buy order size
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                // Place Limit buy Order to set the market
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, limitPrice, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, buyTab, buyOrderSize, limitPrice));
                // Navigate to Advance orders section and Place Stop LimitSellOrder with stopLimitPrice < stopPrice < limitPrice
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, sellTab, instrument, orderType);
                var placeStopLimitBuyOrderTime = advanceOrder.PlaceStopLimitSellOrder(sellOrderSize, stopLimitPrice, stopPrice, stopTimeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                // Verify success message
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedStopLimitOrderPlacedSuccessfully, sellTab, sellOrderSize, stopLimitPrice, stopPrice, stopTimeInForce));
                // Verify the order is present in Open orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, sellTab, Const.StopLimit, Double.Parse(sellOrderSize), stopPrice, placeStopLimitBuyOrderTime["PlaceOrderTime"], placeStopLimitBuyOrderTime["PlaceOrderTimePlusOneMin"]));
                // Place market sell order so that the buy order gets filled
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                orderEntryPage.PlaceMarketSellOrder(instrument, buyTab, Double.Parse(sellOrderSize), Double.Parse(feeComponent));
                // Place limit sell order with limitPrice=StopPrice
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, limitPriceEqualsStop, timeInForce);
                // Place limit sell order with limitPrice=StopPrice and order size such that Stop LimitSellOrder gets filled
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, equalBuyOrderSize, limitPriceEqualsStop, timeInForce);

                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                // Verify that the order is present in Filled orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, sellTab, double.Parse(sellOrderSize), sellOrderFeeValue, placeStopLimitBuyOrderTime["PlaceOrderTime"], placeStopLimitBuyOrderTime["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvStopLimitFilledOrder, sellTab, sellOrderSize, stopLimitPrice, stopPrice, stopTimeInForce));

                // Scenario 2: Place Sell Stop Limit order with sell order size < buy order size

                // Creating Buy and Sell Order to set the last price
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, limitPrice));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, limitPrice, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, limitPrice));
                // Place Limit buy Order to set the market
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, limitPrice, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, buyTab, buyOrderSize, limitPrice));
                // Navigate to Advance orders section and Place Stop LimitSellOrder with stopLimitPrice < stopPrice < limitPrice
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, sellTab, instrument, orderType);
                var placeStopLimitBuyOrderTime2 = advanceOrder.PlaceStopLimitSellOrder(sellOrderSize, stopLimitPrice, stopPrice, stopTimeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                // Verify success message
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedStopLimitOrderPlacedSuccessfully, sellTab, sellOrderSize, stopLimitPrice, stopPrice, stopTimeInForce));
                // Verify the order is present in Open orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, sellTab, Const.StopLimit, Double.Parse(sellOrderSize), stopPrice, placeStopLimitBuyOrderTime2["PlaceOrderTime"], placeStopLimitBuyOrderTime2["PlaceOrderTimePlusOneMin"]));
                // Place market sell order so that the buy order gets filled
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                orderEntryPage.PlaceMarketSellOrder(instrument, buyTab, Double.Parse(sellOrderSize), Double.Parse(feeComponent));
                // Place limit sell order with limitPrice=StopPrice
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, limitPriceEqualsStop, timeInForce);
                // Place limit sell order with limitPrice=StopPrice and increased order size 
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, incBuyOrderSize, limitPriceEqualsStop, timeInForce);

                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                // Verify that the order is present in Filled orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, sellTab, double.Parse(sellOrderSize), sellOrderFeeValue, placeStopLimitBuyOrderTime2["PlaceOrderTime"], placeStopLimitBuyOrderTime2["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvStopLimitFilledOrder, sellTab, sellOrderSize, stopLimitPrice, stopPrice, stopTimeInForce));
                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER7);

                // Scenario 3: Place Sell Stop Limit order with sell order size > buy order size

                // Creating Buy and Sell Order to set the last price
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, limitPrice));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, limitPrice, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, limitPrice));
                // Place Limit buy Order to set the market
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, limitPrice, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, buyTab, buyOrderSize, limitPrice));
                // Navigate to Advance orders section and Place Stop LimitSellOrder with stopLimitPrice < stopPrice < limitPrice
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, sellTab, instrument, orderType);
                var placeStopLimitBuyOrderTime3 = advanceOrder.PlaceStopLimitSellOrder(sellOrderSize, stopLimitPrice, stopPrice, stopTimeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                // Verify success message
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedStopLimitOrderPlacedSuccessfully, sellTab, sellOrderSize, stopLimitPrice, stopPrice, stopTimeInForce));
                // Verify the order is present in Open orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, sellTab, Const.StopLimit, Double.Parse(sellOrderSize), stopPrice, placeStopLimitBuyOrderTime3["PlaceOrderTime"], placeStopLimitBuyOrderTime3["PlaceOrderTimePlusOneMin"]));
                // Place market sell order so that the buy order gets filled
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                orderEntryPage.PlaceMarketSellOrder(instrument, buyTab, Double.Parse(sellOrderSize), Double.Parse(feeComponent));
                // Place limit sell order with limitPrice=StopPrice
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, limitPriceEqualsStop, timeInForce);
                // Place limit sell order with limitPrice=StopPrice and decreased order size
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, decBuyOrderSize, limitPriceEqualsStop, timeInForce);

                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                // Verify that the order not fulfilled is present in inactive orders tab
                Assert.True(objVerifyOrdersTab.VerifyInactiveOrdersTab(instrument, sellTab, Const.StopLimit, Double.Parse(sellOrderSize), stopPrice, placeStopLimitBuyOrderTime3["PlaceOrderTime"], placeStopLimitBuyOrderTime3["PlaceOrderTimePlusOneMin"], Const.CancelledStatus));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvStopLimitInactiveOrder, buyTab, Const.StopLimit, buyOrderSize, stopPrice));
                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER7);
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.FOKOrderTypeFailedMsg, sellTab), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.FOKOrderTypeFailedMsg, sellTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }

        [Fact] 
        public void TC19_VerifyAdvTrailingStopLimitBuyOrderMultiScenario()
        {
            try
            {
                string successMsg;
                string askPrice;
                string initialTrailingPrice;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("TrailingStopLimit");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                setMarketPrice = TestData.GetData("TC19_SetMarketPrice");
                buyOrderSize = TestData.GetData("TC19_BuyOrderSize");
                sellOrderSize = TestData.GetData("TC19_SellOrderSize");
                incSellOrderSize = TestData.GetData("TC19_IncSellOrderSize");
                decSellOrderSize = TestData.GetData("TC19_DecSellOrderSize");
                equalSellOrderSize = TestData.GetData("TC19_EqualSellOrderSize");
                timeInForce = TestData.GetData("TC19_TimeInForce");
                trailingAmount = TestData.GetData("TC19_TrailingAmount");
                triggerTrailingPrice = TestData.GetData("TC19_TrailingPrice");
                limitOffset = TestData.GetData("TC19_LimitOffset");
                pegPrice = TestData.GetData("TC19_PegPrice");
                feeComponent = TestData.GetData("FeeComponent");
                buyLimitPrice1 = TestData.GetData("TC19_BuyLimitPrice_1");
                buyLimitPrice2 = TestData.GetData("TC19_BuyLimitPrice_2");
                buyLimitPrice3 = TestData.GetData("TC19_BuyLimitPrice_3");
                buyLimitPrice4 = TestData.GetData("TC19_BuyLimitPrice_4");
                buyLimitPrice5 = TestData.GetData("TC19_BuyLimitPrice_5");
                sellLimitPrice1 = TestData.GetData("TC19_SellLimitPrice_1");
                sellLimitPrice2 = TestData.GetData("TC19_SellLimitPrice_2");
                sellLimitPrice3 = TestData.GetData("TC19_SellLimitPrice_3");
                sellLimitPrice4 = TestData.GetData("TC19_SellLimitPrice_4");
                sellLimitPrice5 = TestData.GetData("TC19_SellLimitPrice_5");

                // Get buy order fee value based on buyOrderSize, feeComponent
                buyOrderFeeValue = GenericUtils.FeeAmount(buyOrderSize, feeComponent);
                // Get sell order fee value based on sellOrderSize, setMarketPrice, feeComponent
                sellOrderFeeValue = GenericUtils.SellFeeAmount(sellOrderSize, setMarketPrice, feeComponent);
                // Get initial trailing price based on setMarketPrice, trailingAmount
                initialTrailingPrice = GenericUtils.GetSumFromStringAfterAddition(setMarketPrice, trailingAmount);
                // Get final trailing price based on triggerTrailingPrice, limitOffset
                finalTrailingPrice = GenericUtils.GetSumFromStringAfterAddition(triggerTrailingPrice, limitOffset);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                OrderEntryPage orderEntryPage = new OrderEntryPage(driver, TestProgressLogger);

                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
                // Creating Buy and Sell Order to set the last price
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, setMarketPrice));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, setMarketPrice, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, setMarketPrice));

                // Scenario 1: Place Buy Trailing Stop Limit order with buy order size = sell order size

                // Place 5 Buy order and 5 Sell order with a difference of 1(in Limit price) and same order size using the same user
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BuyTrailingMarketSetupEnd, buyTab, buyOrderSize, buyLimitPrice1, buyLimitPrice2, buyLimitPrice3, buyLimitPrice4, buyLimitPrice5));
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.SellTrailingMarketSetupEnd, sellTab, sellOrderSize, sellLimitPrice1, sellLimitPrice2, sellLimitPrice3, sellLimitPrice4, sellLimitPrice5));
                
                // Place Trailing Stop Limit Buy Order 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, buyTab, instrument, orderType);
                var placeTrailingStopLimitBuyOrder = advanceOrder.PlaceTrailingStopLimitBuyOrder(buyOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                // Verify success message
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedTrailingLimitOrderPlacedSuccessfully, buyTab, buyOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce));
                
                // Verify the order is present in Open orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, buyTab, Const.TrailingStopLimit, Double.Parse(buyOrderSize), initialTrailingPrice, placeTrailingStopLimitBuyOrder["PlaceOrderTime"], placeTrailingStopLimitBuyOrder["PlaceOrderTimePlusOneMin"]));
                
                // Place sell orders to reduce market price
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, buyLimitPrice5, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, buyLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, buyLimitPrice3, timeInForce);

                // Place Sell Order to match the trailing stop limit order
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, equalSellOrderSize, finalTrailingPrice, timeInForce);
                
                // Place Buy Order to Increase the market
                userFunctions.LogIn(TestProgressLogger, Const.USER10);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, sellLimitPrice1, timeInForce);
                
                // As soon as the market changes it direction the Trailing order should get placed 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                // Verify that the order is present in Filled orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTabForTrailingOrders(instrument, buyTab, double.Parse(buyOrderSize), finalTrailingPrice, buyOrderFeeValue, placeTrailingStopLimitBuyOrder["PlaceOrderTime"], placeTrailingStopLimitBuyOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvTrailingLimitFilledOrder, buyTab, buyOrderSize, finalTrailingPrice));


                // Scenario 2: Place Buy Trailing Stop Limit order with buy order size < sell order size
                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
                // Creating Buy and Sell Order to set the last price
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, setMarketPrice));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, setMarketPrice, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, setMarketPrice));
                // Place 5 Buy order and 5 Sell order with a difference of 1(in Limit price) and same order size using the same user
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BuyTrailingMarketSetupEnd, buyTab, buyOrderSize, buyLimitPrice1, buyLimitPrice2, buyLimitPrice3, buyLimitPrice4, buyLimitPrice5));
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.SellTrailingMarketSetupEnd, sellTab, sellOrderSize, sellLimitPrice1, sellLimitPrice2, sellLimitPrice3, sellLimitPrice4, sellLimitPrice5));

                // Place Trailing Stop Limit Buy Order 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, buyTab, instrument, orderType);
                var placeTrailingStopLimitBuyOrder2 = advanceOrder.PlaceTrailingStopLimitBuyOrder(buyOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                // Verify success message
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedTrailingLimitOrderPlacedSuccessfully, buyTab, buyOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce));
                
                // Verify the order is present in Open orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, buyTab, Const.TrailingStopLimit, Double.Parse(buyOrderSize), initialTrailingPrice, placeTrailingStopLimitBuyOrder2["PlaceOrderTime"], placeTrailingStopLimitBuyOrder2["PlaceOrderTimePlusOneMin"]));

                // Place sell order to reduce market price
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, buyLimitPrice5, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, buyLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, buyLimitPrice3, timeInForce);

                // Place Sell Order to match the trailing stop limit order
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, incSellOrderSize, finalTrailingPrice, timeInForce);

                // Place Buy Order to Increase the market
                userFunctions.LogIn(TestProgressLogger, Const.USER10);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, sellLimitPrice1, timeInForce);
                // As soon as the market changes it direction the Trailing order should get placed 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                // Verify that the order is present in Filled orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTabForTrailingOrders(instrument, buyTab, double.Parse(buyOrderSize), finalTrailingPrice, buyOrderFeeValue, placeTrailingStopLimitBuyOrder2["PlaceOrderTime"], placeTrailingStopLimitBuyOrder2["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvTrailingLimitFilledOrder, buyTab, buyOrderSize, finalTrailingPrice));

                // Scenario 3: Place Buy Trailing Stop Limit order with buy order size > sell order size
                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER7);
                // Creating Buy and Sell Order to set the last price
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, setMarketPrice));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, setMarketPrice, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, setMarketPrice));
                // Place 5 Buy order and 5 Sell order with a difference of 1(in Limit price) and same order size using the same user
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BuyTrailingMarketSetupEnd, buyTab, buyOrderSize, buyLimitPrice1, buyLimitPrice2, buyLimitPrice3, buyLimitPrice4, buyLimitPrice5));
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.SellTrailingMarketSetupEnd, sellTab, sellOrderSize, sellLimitPrice1, sellLimitPrice2, sellLimitPrice3, sellLimitPrice4, sellLimitPrice5));

                // Place Trailing Stop Limit Buy Order 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, buyTab, instrument, orderType);
                var placeTrailingStopLimitBuyOrder3 = advanceOrder.PlaceTrailingStopLimitBuyOrder(buyOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                // Verify success message
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedTrailingLimitOrderPlacedSuccessfully, buyTab, buyOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce));
                // Verify the order is present in Open orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, buyTab, Const.TrailingStopLimit, Double.Parse(buyOrderSize), initialTrailingPrice, placeTrailingStopLimitBuyOrder3["PlaceOrderTime"], placeTrailingStopLimitBuyOrder3["PlaceOrderTimePlusOneMin"]));

                // Place sell order to reduce market price
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, buyLimitPrice5, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, buyLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, buyLimitPrice3, timeInForce);

                // Place Sell Order to match the trailing stop limit order
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, decSellOrderSize, finalTrailingPrice, timeInForce);
                
                // Place Buy Order to Increase the market
                userFunctions.LogIn(TestProgressLogger, Const.USER10);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, sellLimitPrice1, timeInForce);
                // As soon as the market changes it direction the Trailing order should get placed 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                var filledOrderSize = GenericUtils.GetDifferenceFromStringAfterSubstraction(decSellOrderSize, sellOrderSize);
                var unfilledOrderSize = GenericUtils.GetDifferenceFromStringAfterSubstraction(buyOrderSize, filledOrderSize);
                buyOrderFeeValue = GenericUtils.FeeAmount(filledOrderSize, feeComponent);
                // Verify that the order is present in Filled orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTabForTrailingOrders(instrument, buyTab, double.Parse(filledOrderSize), finalTrailingPrice, buyOrderFeeValue, placeTrailingStopLimitBuyOrder3["PlaceOrderTime"], placeTrailingStopLimitBuyOrder3["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvTrailingLimitFilledOrder, buyTab, filledOrderSize, finalTrailingPrice));
                // Verify the order is present in Open orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, buyTab, Const.TrailingStopLimit, Double.Parse(unfilledOrderSize), triggerTrailingPrice, placeTrailingStopLimitBuyOrder3["PlaceOrderTime"], placeTrailingStopLimitBuyOrder3["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvTrailingLimitOpenOrder, buyTab, Const.TrailingStopLimit, unfilledOrderSize, triggerTrailingPrice));
                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER9);
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifyAdvTrailingLimitOrderFailed, buyTab), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifyAdvTrailingLimitOrderFailed, buyTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }

        [Fact] 
        public void TC20_VerifyAdvTrailingStopLimitSellOrderMultiScenario()
        {
            try
            {
                string successMsg;
                string askPrice;
                string initialTrailingPrice;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("TrailingStopLimit");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                setMarketPrice = TestData.GetData("TC20_SetMarketPrice");
                buyOrderSize = TestData.GetData("TC20_BuyOrderSize");
                sellOrderSize = TestData.GetData("TC20_SellOrderSize");
                incBuyOrderSize = TestData.GetData("TC20_IncBuyOrderSize");
                decBuyOrderSize = TestData.GetData("TC20_DecBuyOrderSize");
                equalBuyOrderSize = TestData.GetData("TC20_EqualBuyOrderSize");
                timeInForce = TestData.GetData("TC20_TimeInForce");
                trailingAmount = TestData.GetData("TC20_TrailingAmount");
                triggerTrailingPrice = TestData.GetData("TC20_TrailingPrice");
                limitOffset = TestData.GetData("TC20_LimitOffset");
                pegPrice = TestData.GetData("TC20_PegPrice");
                feeComponent = TestData.GetData("FeeComponent");
                buyLimitPrice1 = TestData.GetData("TC20_BuyLimitPrice_1");
                buyLimitPrice2 = TestData.GetData("TC20_BuyLimitPrice_2");
                buyLimitPrice3 = TestData.GetData("TC20_BuyLimitPrice_3");
                buyLimitPrice4 = TestData.GetData("TC20_BuyLimitPrice_4");
                buyLimitPrice5 = TestData.GetData("TC20_BuyLimitPrice_5");
                sellLimitPrice1 = TestData.GetData("TC20_SellLimitPrice_1");
                sellLimitPrice2 = TestData.GetData("TC20_SellLimitPrice_2");
                sellLimitPrice3 = TestData.GetData("TC20_SellLimitPrice_3");
                sellLimitPrice4 = TestData.GetData("TC20_SellLimitPrice_4");
                sellLimitPrice5 = TestData.GetData("TC20_SellLimitPrice_5");

                // Get buy order fee value based on buyOrderSize, feeComponent
                buyOrderFeeValue = GenericUtils.FeeAmount(buyOrderSize, feeComponent);
                // Get sell order fee value based on sellOrderSize, setMarketPrice, feeComponent
                sellOrderFeeValue = GenericUtils.SellFeeAmount(sellOrderSize, setMarketPrice, feeComponent);
                // Get initial trailing price based on setMarketPrice, trailingAmount
                initialTrailingPrice = GenericUtils.GetDifferenceFromStringAfterSubstraction(setMarketPrice, trailingAmount);
                // Get final trailing price based on triggerTrailingPrice, limitOffset
                finalTrailingPrice = GenericUtils.GetDifferenceFromStringAfterSubstraction(triggerTrailingPrice, limitOffset);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                OrderEntryPage orderEntryPage = new OrderEntryPage(driver, TestProgressLogger);

                // Creating Buy and Sell Order to set the last price
                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
                // Creating Buy and Sell Order to set the last price
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, setMarketPrice));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, setMarketPrice, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, setMarketPrice));

                // Scenario 1: Place Sell Trailing Stop Limit order with sell order size = buy order size

                // Place 5 Buy order and 5 Sell order with a difference of 1(in Limit price) and same order size using the same user
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BuyTrailingMarketSetupEnd, buyTab, buyOrderSize, buyLimitPrice1, buyLimitPrice2, buyLimitPrice3, buyLimitPrice4, buyLimitPrice5));
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.SellTrailingMarketSetupEnd, sellTab, sellOrderSize, sellLimitPrice1, sellLimitPrice2, sellLimitPrice3, sellLimitPrice4, sellLimitPrice5));
                
                // Place Trailing Stop Limit Sell Order 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, sellTab, instrument, orderType);
                var placeTrailingStopLimitSellOrder = advanceOrder.PlaceTrailingStopLimitSellOrder(sellOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                // Verify success message
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedTrailingLimitOrderPlacedSuccessfully, sellTab, sellOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce));
                // Verify the order is present in Open orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, sellTab, Const.TrailingStopLimit, Double.Parse(sellOrderSize), initialTrailingPrice, placeTrailingStopLimitSellOrder["PlaceOrderTime"], placeTrailingStopLimitSellOrder["PlaceOrderTimePlusOneMin"]));
                
                // Place buy order to increase market price
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, sellLimitPrice3, timeInForce);

                // Place Buy Order to match the trailing stop limit order - at price 50.70
                userFunctions.LogIn(TestProgressLogger, Const.USER11);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, equalBuyOrderSize, finalTrailingPrice, timeInForce);

                // Place sell Order to reduce the market
                userFunctions.LogIn(TestProgressLogger, Const.USER10);
                userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, buyLimitPrice1, timeInForce);
                // As soon as the market changes it direction the Trailing order should get placed 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                // Get fee value based on the final Trailing price
                sellOrderFeeValue = GenericUtils.SellFeeAmount(sellOrderSize, finalTrailingPrice, feeComponent);
                // Verify that the order is present in Filled orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTabForTrailingOrders(instrument, sellTab, double.Parse(sellOrderSize), finalTrailingPrice, sellOrderFeeValue, placeTrailingStopLimitSellOrder["PlaceOrderTime"], placeTrailingStopLimitSellOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvTrailingLimitFilledOrder, sellTab, sellOrderSize, finalTrailingPrice));

                // Scenario 2: Place Sell Trailing Stop Limit order with sell order size < buy order size

                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
                // Creating Buy and Sell Order to set the last price
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, setMarketPrice));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, setMarketPrice, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, setMarketPrice));
                // Place 5 Buy order and 5 Sell order with a difference of 1(in Limit price) and same order size using the same user
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BuyTrailingMarketSetupEnd, buyTab, buyOrderSize, buyLimitPrice1, buyLimitPrice2, buyLimitPrice3, buyLimitPrice4, buyLimitPrice5));
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.SellTrailingMarketSetupEnd, sellTab, sellOrderSize, sellLimitPrice1, sellLimitPrice2, sellLimitPrice3, sellLimitPrice4, sellLimitPrice5));

                // Place Trailing Stop Limit Sell Order 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, sellTab, instrument, orderType);
                var placeTrailingStopLimitSellOrder2 = advanceOrder.PlaceTrailingStopLimitSellOrder(sellOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                // Verify success message
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedTrailingLimitOrderPlacedSuccessfully, sellTab, sellOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce));
                // Verify the order is present in Open orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, sellTab, Const.TrailingStopLimit, Double.Parse(sellOrderSize), initialTrailingPrice, placeTrailingStopLimitSellOrder["PlaceOrderTime"], placeTrailingStopLimitSellOrder["PlaceOrderTimePlusOneMin"]));

                // Place buy order to increase market price
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, sellLimitPrice3, timeInForce);

                // Place Buy Order to match the trailing stop limit order - at price 50.70
                userFunctions.LogIn(TestProgressLogger, Const.USER11);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, incBuyOrderSize, finalTrailingPrice, timeInForce);

                // Place sell Order to reduce the market
                userFunctions.LogIn(TestProgressLogger, Const.USER10);
                userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, buyLimitPrice1, timeInForce);
                // As soon as the market changes it direction the Trailing order should get placed 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                // Get the fee value based on the finalTrailingPrice
                sellOrderFeeValue = GenericUtils.SellFeeAmount(sellOrderSize, finalTrailingPrice, feeComponent);
                // Verify that the order is present in Filled orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTabForTrailingOrders(instrument, sellTab, double.Parse(sellOrderSize), finalTrailingPrice, sellOrderFeeValue, placeTrailingStopLimitSellOrder["PlaceOrderTime"], placeTrailingStopLimitSellOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvTrailingLimitFilledOrder, sellTab, sellOrderSize, finalTrailingPrice));

                // Scenario 3: Place Sell Trailing Stop Limit order with sell order size > buy order size

                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER11);
                // Creating Buy and Sell Order to set the last price
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, setMarketPrice));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, setMarketPrice, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, setMarketPrice));
                // Place 5 Buy order and 5 Sell order with a difference of 1(in Limit price) and same order size using the same user
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BuyTrailingMarketSetupEnd, buyTab, buyOrderSize, buyLimitPrice1, buyLimitPrice2, buyLimitPrice3, buyLimitPrice4, buyLimitPrice5));
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.SellTrailingMarketSetupEnd, sellTab, sellOrderSize, sellLimitPrice1, sellLimitPrice2, sellLimitPrice3, sellLimitPrice4, sellLimitPrice5));

                // Place Trailing Stop Limit Sell Order 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, sellTab, instrument, orderType);
                var placeTrailingStopLimitSellOrder3 = advanceOrder.PlaceTrailingStopLimitSellOrder(sellOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                // Verify success message
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedTrailingLimitOrderPlacedSuccessfully, sellTab, sellOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce));
                // Verify the order is present in Open orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, sellTab, Const.TrailingStopLimit, Double.Parse(sellOrderSize), initialTrailingPrice, placeTrailingStopLimitSellOrder3["PlaceOrderTime"], placeTrailingStopLimitSellOrder3["PlaceOrderTimePlusOneMin"]));

                // Place buy order to increase market price
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, sellLimitPrice3, timeInForce);

                // Place Buy Order to match the trailing stop limit order - at price 50.70
                userFunctions.LogIn(TestProgressLogger, Const.USER11);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, decBuyOrderSize, finalTrailingPrice, timeInForce);

                // Place sell Order to reduce the market
                userFunctions.LogIn(TestProgressLogger, Const.USER10);
                userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, buyLimitPrice1, timeInForce);
                // As soon as the market changes it direction the Trailing order should get placed 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                var filledOrderSize = GenericUtils.GetDifferenceFromStringAfterSubstraction(decBuyOrderSize, sellOrderSize);
                var unfilledOrderSize = GenericUtils.GetDifferenceFromStringAfterSubstraction(sellOrderSize, filledOrderSize);
                // Get the fee value based on the finalTrailingPrice
                sellOrderFeeValue = GenericUtils.SellFeeAmount(filledOrderSize, finalTrailingPrice, feeComponent);
                // Verify that the order is present in Filled orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTabForTrailingOrders(instrument, sellTab, double.Parse(filledOrderSize), finalTrailingPrice, sellOrderFeeValue, placeTrailingStopLimitSellOrder3["PlaceOrderTime"], placeTrailingStopLimitSellOrder3["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvTrailingLimitFilledOrder, sellTab, decSellOrderSize, finalTrailingPrice));
                // Verify the order is present in Open orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, sellTab, Const.TrailingStopLimit, Double.Parse(unfilledOrderSize), triggerTrailingPrice, placeTrailingStopLimitSellOrder3["PlaceOrderTime"], placeTrailingStopLimitSellOrder3["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvTrailingLimitOpenOrder, sellTab, Const.TrailingStopLimit, decSellOrderSize, triggerTrailingPrice));
                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER9);
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifyAdvTrailingLimitOrderFailed, sellTab), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifyAdvTrailingLimitOrderFailed, sellTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }

        [Fact]
        public void TC21_VerifyAdvTrailingStopLimitBuyOrderMultiScenario()
        {
            try
            {
                string successMsg;
                string askPrice;
                string initialTrailingPrice;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("TrailingStopLimit");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                setMarketPrice = TestData.GetData("TC21_SetMarketPrice");
                buyOrderSize = TestData.GetData("TC21_BuyOrderSize");
                sellOrderSize = TestData.GetData("TC21_SellOrderSize");
                incSellOrderSize = TestData.GetData("TC21_IncSellOrderSize");
                decSellOrderSize = TestData.GetData("TC21_DecSellOrderSize");
                equalSellOrderSize = TestData.GetData("TC21_EqualSellOrderSize");
                timeInForce = TestData.GetData("TimeInForce");
                iocTimeInForce = TestData.GetData("TC21_TimeInForce");
                trailingAmount = TestData.GetData("TC21_TrailingAmount");
                triggerTrailingPrice = TestData.GetData("TC21_TrailingPrice");
                limitOffset = TestData.GetData("TC21_LimitOffset");
                pegPrice = TestData.GetData("TC21_PegPrice");
                feeComponent = TestData.GetData("FeeComponent");
                buyLimitPrice1 = TestData.GetData("TC21_BuyLimitPrice_1");
                buyLimitPrice2 = TestData.GetData("TC21_BuyLimitPrice_2");
                buyLimitPrice3 = TestData.GetData("TC21_BuyLimitPrice_3");
                buyLimitPrice4 = TestData.GetData("TC21_BuyLimitPrice_4");
                buyLimitPrice5 = TestData.GetData("TC21_BuyLimitPrice_5");
                sellLimitPrice1 = TestData.GetData("TC21_SellLimitPrice_1");
                sellLimitPrice2 = TestData.GetData("TC21_SellLimitPrice_2");
                sellLimitPrice3 = TestData.GetData("TC21_SellLimitPrice_3");
                sellLimitPrice4 = TestData.GetData("TC21_SellLimitPrice_4");
                sellLimitPrice5 = TestData.GetData("TC21_SellLimitPrice_5");

                // Get buy order fee value based on buyOrderSize, feeComponent
                buyOrderFeeValue = GenericUtils.FeeAmount(buyOrderSize, feeComponent);
                // Get sell order fee value based on sellOrderSize, setMarketPrice, feeComponent
                sellOrderFeeValue = GenericUtils.SellFeeAmount(sellOrderSize, setMarketPrice, feeComponent);
                // Get initial trailing price based on setMarketPrice, trailingAmount
                initialTrailingPrice = GenericUtils.GetSumFromStringAfterAddition(setMarketPrice, trailingAmount);
                // Get final trailing price based on triggerTrailingPrice, limitOffset
                finalTrailingPrice = GenericUtils.GetSumFromStringAfterAddition(triggerTrailingPrice, limitOffset);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                OrderEntryPage orderEntryPage = new OrderEntryPage(driver, TestProgressLogger);

                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
                // Creating Buy and Sell Order to set the last price
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, setMarketPrice));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, setMarketPrice, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, setMarketPrice));

                // Scenario 1: Place Buy Trailing Stop Limit order with buy order size = sell order size

                // Place 5 Buy order and 5 Sell order with a difference of 1(in Limit price) and same order size using the same user
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BuyTrailingMarketSetupEnd, buyTab, buyOrderSize, buyLimitPrice1, buyLimitPrice2, buyLimitPrice3, buyLimitPrice4, buyLimitPrice5));
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.SellTrailingMarketSetupEnd, sellTab, sellOrderSize, sellLimitPrice1, sellLimitPrice2, sellLimitPrice3, sellLimitPrice4, sellLimitPrice5));
                
                // Place Trailing Stop Limit Buy Order 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, buyTab, instrument, orderType);
                var placeTrailingStopLimitBuyOrder = advanceOrder.PlaceTrailingStopLimitBuyOrder(buyOrderSize, trailingAmount, limitOffset, pegPrice, iocTimeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                // Verify success message
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedTrailingLimitOrderPlacedSuccessfully, buyTab, buyOrderSize, trailingAmount, limitOffset, pegPrice, iocTimeInForce));
                // Get initial trailing price based on setMarketPrice, trailingAmount
                initialTrailingPrice = GenericUtils.GetSumFromStringAfterAddition(setMarketPrice, trailingAmount);
                // Verify the order is present in Open orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, buyTab, Const.TrailingStopLimit, Double.Parse(buyOrderSize), initialTrailingPrice, placeTrailingStopLimitBuyOrder["PlaceOrderTime"], placeTrailingStopLimitBuyOrder["PlaceOrderTimePlusOneMin"]));
                
                // Place sell order to reduce market price
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, buyLimitPrice3, timeInForce);

                // Place Sell Order to match the trailing stop limit order
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, equalSellOrderSize, finalTrailingPrice, timeInForce);
                
                // Place Buy Order to Increase the market
                userFunctions.LogIn(TestProgressLogger, Const.USER10);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, sellLimitPrice1, timeInForce);
                // As soon as the market changes it direction the Trailing order should get placed                 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                // Verify that the order is present in Filled orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTabForTrailingOrders(instrument, buyTab, double.Parse(buyOrderSize), finalTrailingPrice, buyOrderFeeValue, placeTrailingStopLimitBuyOrder["PlaceOrderTime"], placeTrailingStopLimitBuyOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvTrailingLimitFilledOrder, buyTab, buyOrderSize, finalTrailingPrice));

                // Scenario 2: Place Buy Trailing Stop Limit order with buy order size < sell order size

                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
                // Creating Buy and Sell Order to set the last price
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, setMarketPrice));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, setMarketPrice, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, setMarketPrice));
                // Place 5 Buy order and 5 Sell order with a difference of 1(in Limit price) and same order size using the same user
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BuyTrailingMarketSetupEnd, buyTab, buyOrderSize, buyLimitPrice1, buyLimitPrice2, buyLimitPrice3, buyLimitPrice4, buyLimitPrice5));
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.SellTrailingMarketSetupEnd, sellTab, sellOrderSize, sellLimitPrice1, sellLimitPrice2, sellLimitPrice3, sellLimitPrice4, sellLimitPrice5));

                // Place Trailing Stop Limit Buy Order 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, buyTab, instrument, orderType);
                var placeTrailingStopLimitBuyOrder2 = advanceOrder.PlaceTrailingStopLimitBuyOrder(buyOrderSize, trailingAmount, limitOffset, pegPrice, iocTimeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                // Verify success message
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedTrailingLimitOrderPlacedSuccessfully, buyTab, buyOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce));
                // Get initial trailing price based on setMarketPrice, trailingAmount
                initialTrailingPrice = GenericUtils.GetSumFromStringAfterAddition(setMarketPrice, trailingAmount);
                // Verify the order is present in Open orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, buyTab, Const.TrailingStopLimit, Double.Parse(buyOrderSize), initialTrailingPrice, placeTrailingStopLimitBuyOrder2["PlaceOrderTime"], placeTrailingStopLimitBuyOrder2["PlaceOrderTimePlusOneMin"]));

                // Place sell order to reduce market price
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, buyLimitPrice3, timeInForce);

                // Place Sell Order to match the trailing stop limit order
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, incSellOrderSize, finalTrailingPrice, timeInForce);

                // Place Buy Order to Increase the market
                userFunctions.LogIn(TestProgressLogger, Const.USER10);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, sellLimitPrice1, timeInForce);
                // As soon as the market changes it direction the Trailing order should get placed 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                // Verify that the order is present in Filled orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTabForTrailingOrders(instrument, buyTab, Double.Parse(buyOrderSize), finalTrailingPrice, buyOrderFeeValue, placeTrailingStopLimitBuyOrder2["PlaceOrderTime"], placeTrailingStopLimitBuyOrder2["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvTrailingLimitFilledOrder, buyTab, buyOrderSize, finalTrailingPrice));

                // Scenario 3: Place Buy Trailing Stop Limit order with buy order size > sell order size
                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER7);
                // Creating Buy and Sell Order to set the last price
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, setMarketPrice));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, setMarketPrice, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, setMarketPrice));
                // Place 5 Buy order and 5 Sell order with a difference of 1(in Limit price) and same order size using the same user
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BuyTrailingMarketSetupEnd, buyTab, buyOrderSize, buyLimitPrice1, buyLimitPrice2, buyLimitPrice3, buyLimitPrice4, buyLimitPrice5));
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.SellTrailingMarketSetupEnd, sellTab, sellOrderSize, sellLimitPrice1, sellLimitPrice2, sellLimitPrice3, sellLimitPrice4, sellLimitPrice5));

                // Place Trailing Stop Limit Buy Order 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, buyTab, instrument, orderType);
                var placeTrailingStopLimitBuyOrder3 = advanceOrder.PlaceTrailingStopLimitBuyOrder(buyOrderSize, trailingAmount, limitOffset, pegPrice, iocTimeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                // Verify success message
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedTrailingLimitOrderPlacedSuccessfully, buyTab, buyOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce));
                // Get initial trailing price based on setMarketPrice, trailingAmount
                initialTrailingPrice = GenericUtils.GetSumFromStringAfterAddition(setMarketPrice, trailingAmount);
                // Verify the order is present in Open orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, buyTab, Const.TrailingStopLimit, Double.Parse(buyOrderSize), initialTrailingPrice, placeTrailingStopLimitBuyOrder3["PlaceOrderTime"], placeTrailingStopLimitBuyOrder3["PlaceOrderTimePlusOneMin"]));
                
                // Place sell order to reduce market price
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, buyLimitPrice3, timeInForce);

                // Place Sell Order to match the trailing stop limit order
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, decSellOrderSize, finalTrailingPrice, timeInForce);

                // Place Buy Order to Increase the market
                userFunctions.LogIn(TestProgressLogger, Const.USER10);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, sellLimitPrice1, timeInForce);
                // As soon as the market changes it direction the Trailing order should get placed 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                var filledOrderSize = GenericUtils.GetDifferenceFromStringAfterSubstraction(decSellOrderSize, buyOrderSize);
                // Get the fee value based on filled OrderSize
                buyOrderFeeValue = GenericUtils.FeeAmount(filledOrderSize, feeComponent);
                // Verify that the order is present in Filled orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTabForTrailingOrders(instrument, buyTab, Double.Parse(filledOrderSize), finalTrailingPrice, buyOrderFeeValue, placeTrailingStopLimitBuyOrder3["PlaceOrderTime"], placeTrailingStopLimitBuyOrder3["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvTrailingLimitFilledOrder, buyTab, filledOrderSize, finalTrailingPrice));
                // Verify that the order not fulfilled is present in inactive orders tab
                Assert.True(objVerifyOrdersTab.VerifyInactiveOrdersTab(instrument, buyTab, Const.TrailingStopLimit, Double.Parse(buyOrderSize), triggerTrailingPrice, placeTrailingStopLimitBuyOrder3["PlaceOrderTime"], placeTrailingStopLimitBuyOrder3["PlaceOrderTimePlusOneMin"], Const.CancelledStatus));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvTrailingLimitInactiveOrder, buyTab, Const.TrailingStopLimit, buyOrderSize, triggerTrailingPrice));
                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER9);
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifyAdvTrailingLimitOrderFailed, buyTab), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifyAdvTrailingLimitOrderFailed, buyTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }

        [Fact] 
        public void TC22_VerifyAdvTrailingStopLimitSellOrderMultiScenario()
        {
            try
            {
                string successMsg;
                string askPrice;
                string initialTrailingPrice;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("TrailingStopLimit");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                setMarketPrice = TestData.GetData("TC22_SetMarketPrice");
                buyOrderSize = TestData.GetData("TC22_BuyOrderSize");
                sellOrderSize = TestData.GetData("TC22_SellOrderSize");
                incBuyOrderSize = TestData.GetData("TC22_IncBuyOrderSize");
                decBuyOrderSize = TestData.GetData("TC22_DecBuyOrderSize");
                equalBuyOrderSize = TestData.GetData("TC22_EqualBuyOrderSize");
                timeInForce = TestData.GetData("TimeInForce");
                iocTimeInForce = TestData.GetData("TC21_TimeInForce");
                trailingAmount = TestData.GetData("TC22_TrailingAmount");
                triggerTrailingPrice = TestData.GetData("TC22_TrailingPrice");
                limitOffset = TestData.GetData("TC22_LimitOffset");
                pegPrice = TestData.GetData("TC22_PegPrice");
                feeComponent = TestData.GetData("FeeComponent");
                buyLimitPrice1 = TestData.GetData("TC22_BuyLimitPrice_1");
                buyLimitPrice2 = TestData.GetData("TC22_BuyLimitPrice_2");
                buyLimitPrice3 = TestData.GetData("TC22_BuyLimitPrice_3");
                buyLimitPrice4 = TestData.GetData("TC22_BuyLimitPrice_4");
                buyLimitPrice5 = TestData.GetData("TC22_BuyLimitPrice_5");
                sellLimitPrice1 = TestData.GetData("TC22_SellLimitPrice_1");
                sellLimitPrice2 = TestData.GetData("TC22_SellLimitPrice_2");
                sellLimitPrice3 = TestData.GetData("TC22_SellLimitPrice_3");
                sellLimitPrice4 = TestData.GetData("TC22_SellLimitPrice_4");
                sellLimitPrice5 = TestData.GetData("TC22_SellLimitPrice_5");

                // Get initial trailing price based on setMarketPrice, trailingAmount
                initialTrailingPrice = GenericUtils.GetDifferenceFromStringAfterSubstraction(setMarketPrice, trailingAmount);
                // Get final trailing price based on triggerTrailingPrice, limitOffset
                finalTrailingPrice = GenericUtils.GetDifferenceFromStringAfterSubstraction(triggerTrailingPrice, limitOffset);
                // Get buy order fee value based on buyOrderSize, feeComponent
                buyOrderFeeValue = GenericUtils.FeeAmount(buyOrderSize, feeComponent);
                // Get sell order fee value based on sellOrderSize, setMarketPrice, feeComponent
                sellOrderFeeValue = GenericUtils.SellFeeAmount(sellOrderSize, setMarketPrice, feeComponent);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                OrderEntryPage orderEntryPage = new OrderEntryPage(driver, TestProgressLogger);

                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
                // Creating Buy and Sell Order to set the last price
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, setMarketPrice));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, setMarketPrice, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, setMarketPrice));

                // Scenario 1: Place Sell Trailing Stop Limit order with sell order size = buy order size

                // Place 5 Buy order and 5 Sell order with a difference of 1(in Limit price) and same order size using the same user
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BuyTrailingMarketSetupEnd, buyTab, buyOrderSize, buyLimitPrice1, buyLimitPrice2, buyLimitPrice3, buyLimitPrice4, buyLimitPrice5));
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.SellTrailingMarketSetupEnd, sellTab, sellOrderSize, sellLimitPrice1, sellLimitPrice2, sellLimitPrice3, sellLimitPrice4, sellLimitPrice5));

                // Place Trailing Stop Limit Sell Order 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, sellTab, instrument, orderType);
                var placeTrailingStopLimitSellOrder = advanceOrder.PlaceTrailingStopLimitSellOrder(sellOrderSize, trailingAmount, limitOffset, pegPrice, iocTimeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                // Verify success message
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedTrailingLimitOrderPlacedSuccessfully, sellTab, sellOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce));
                // Verify the order is present in Open orders tab                
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, sellTab, Const.TrailingStopLimit, Double.Parse(sellOrderSize), initialTrailingPrice, placeTrailingStopLimitSellOrder["PlaceOrderTime"], placeTrailingStopLimitSellOrder["PlaceOrderTimePlusOneMin"]));

                // Place buy order to increase market price
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, sellLimitPrice3, timeInForce);
                
                // Place Buy Order to match the trailing stop limit order - at price 50.70
                userFunctions.LogIn(TestProgressLogger, Const.USER11);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, equalBuyOrderSize, finalTrailingPrice, timeInForce);

                // Place sell Order to reduce the market
                userFunctions.LogIn(TestProgressLogger, Const.USER10);
                userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, buyLimitPrice1, timeInForce);
                // As soon as the market changes it direction the Trailing order should get placed 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                sellOrderFeeValue = GenericUtils.SellFeeAmount(sellOrderSize, finalTrailingPrice, feeComponent);
                // Verify that the order is present in Filled orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTabForTrailingOrders(instrument, sellTab, Double.Parse(sellOrderSize), finalTrailingPrice, sellOrderFeeValue, placeTrailingStopLimitSellOrder["PlaceOrderTime"], placeTrailingStopLimitSellOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvTrailingLimitFilledOrder, sellTab, sellOrderSize, finalTrailingPrice));

                // Scenario 2: Place Sell Trailing Stop Limit order with sell order size < buy order size

                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
                // Creating Buy and Sell Order to set the last price
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, setMarketPrice));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, setMarketPrice, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, setMarketPrice));
                // Place 5 Buy order and 5 Sell order with a difference of 1(in Limit price) and same order size using the same user
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BuyTrailingMarketSetupEnd, buyTab, buyOrderSize, buyLimitPrice1, buyLimitPrice2, buyLimitPrice3, buyLimitPrice4, buyLimitPrice5));
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.SellTrailingMarketSetupEnd, sellTab, sellOrderSize, sellLimitPrice1, sellLimitPrice2, sellLimitPrice3, sellLimitPrice4, sellLimitPrice5));
                
                // Place Trailing Stop Limit Sell Order 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, sellTab, instrument, orderType);
                var placeTrailingStopLimitSellOrder2 = advanceOrder.PlaceTrailingStopLimitSellOrder(sellOrderSize, trailingAmount, limitOffset, pegPrice, iocTimeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                // Verify success message
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedTrailingLimitOrderPlacedSuccessfully, sellTab, sellOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce));
                // Verify the order is present in Open orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, sellTab, Const.TrailingStopLimit, Double.Parse(sellOrderSize), initialTrailingPrice, placeTrailingStopLimitSellOrder2["PlaceOrderTime"], placeTrailingStopLimitSellOrder2["PlaceOrderTimePlusOneMin"]));
                
                // Place buy order to increase market price
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, sellLimitPrice3, timeInForce);                

                // Place Buy Order to match the trailing stop limit order - at price 50.70
                userFunctions.LogIn(TestProgressLogger, Const.USER11);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, incBuyOrderSize, finalTrailingPrice, timeInForce);

                // Place sell Order to reduce the market
                userFunctions.LogIn(TestProgressLogger, Const.USER10);
                userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, buyLimitPrice1, timeInForce);
                // As soon as the market changes it direction the Trailing order should get placed 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                // Get the fee value based on final Trailing Price
                sellOrderFeeValue = GenericUtils.SellFeeAmount(sellOrderSize, finalTrailingPrice, feeComponent);
                // Verify that the order is present in Filled orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTabForTrailingOrders(instrument, sellTab, Double.Parse(sellOrderSize), finalTrailingPrice, sellOrderFeeValue, placeTrailingStopLimitSellOrder2["PlaceOrderTime"], placeTrailingStopLimitSellOrder2["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvTrailingLimitFilledOrder, sellTab, sellOrderSize, finalTrailingPrice));

                // Scenario 3: Place Sell Trailing Stop Limit order with sell order size > buy order size

                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER11);
                // Creating Buy and Sell Order to set the last price
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, setMarketPrice));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, setMarketPrice, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, setMarketPrice));
                // Place 5 Buy order and 5 Sell order with a difference of 1(in Limit price) and same order size using the same user
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BuyTrailingMarketSetupEnd, buyTab, buyOrderSize, buyLimitPrice1, buyLimitPrice2, buyLimitPrice3, buyLimitPrice4, buyLimitPrice5));
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.SellTrailingMarketSetupEnd, sellTab, sellOrderSize, sellLimitPrice1, sellLimitPrice2, sellLimitPrice3, sellLimitPrice4, sellLimitPrice5));
                
                // Place Trailing Stop Limit Sell Order 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, sellTab, instrument, orderType);
                var placeTrailingStopLimitSellOrder3 = advanceOrder.PlaceTrailingStopLimitSellOrder(sellOrderSize, trailingAmount, limitOffset, pegPrice, iocTimeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                // Verify success message
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedTrailingLimitOrderPlacedSuccessfully, sellTab, sellOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce));
                // Verify the order is present in Open orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, sellTab, Const.TrailingStopLimit, Double.Parse(sellOrderSize), initialTrailingPrice, placeTrailingStopLimitSellOrder3["PlaceOrderTime"], placeTrailingStopLimitSellOrder3["PlaceOrderTimePlusOneMin"]));
                
                // Place buy order to increase market price
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, sellLimitPrice3, timeInForce);

                // Place Buy Order to match the trailing stop limit order - at price 50.70
                userFunctions.LogIn(TestProgressLogger, Const.USER11);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, decBuyOrderSize, finalTrailingPrice, timeInForce);

                // Place sell Order to reduce the market
                userFunctions.LogIn(TestProgressLogger, Const.USER10);
                userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, buyLimitPrice1, timeInForce);
                // As soon as the market changes it direction the Trailing order should get placed 
                finalTrailingPrice = GenericUtils.GetDifferenceFromStringAfterSubstraction(triggerTrailingPrice, limitOffset);
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                var filledOrderSize = GenericUtils.GetDifferenceFromStringAfterSubstraction(decBuyOrderSize, sellOrderSize);
                // Get the fee value based on final Trailing Price
                sellOrderFeeValue = GenericUtils.SellFeeAmount(filledOrderSize, finalTrailingPrice, feeComponent);
                // Verify that the order is present in Filled orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTabForTrailingOrders(instrument, sellTab, double.Parse(filledOrderSize), finalTrailingPrice, sellOrderFeeValue, placeTrailingStopLimitSellOrder3["PlaceOrderTime"], placeTrailingStopLimitSellOrder3["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvTrailingLimitFilledOrder, sellTab, decSellOrderSize, finalTrailingPrice));
                // Verify that the order not fulfilled is present in inactive orders tab
                Assert.True(objVerifyOrdersTab.VerifyInactiveOrdersTab(instrument, sellTab, Const.TrailingStopLimit, Double.Parse(sellOrderSize), triggerTrailingPrice, placeTrailingStopLimitSellOrder3["PlaceOrderTime"], placeTrailingStopLimitSellOrder3["PlaceOrderTimePlusOneMin"], Const.CancelledStatus));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvStopLimitInactiveOrder, sellTab, Const.TrailingStopLimit, sellOrderSize, triggerTrailingPrice));
                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER9);
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifyAdvTrailingLimitOrderFailed, sellTab), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifyAdvTrailingLimitOrderFailed, sellTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }

        [Fact] 
        public void TC23_VerifyAdvTrailingStopLimitBuyOrderMultiScenario()
        {
            try
            {
                string successMsg;
                string askPrice;
                string initialTrailingPrice;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("TrailingStopLimit");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                setMarketPrice = TestData.GetData("TC23_SetMarketPrice");
                buyOrderSize = TestData.GetData("TC23_BuyOrderSize");
                sellOrderSize = TestData.GetData("TC23_SellOrderSize");
                incSellOrderSize = TestData.GetData("TC23_IncSellOrderSize");
                decSellOrderSize = TestData.GetData("TC23_DecSellOrderSize");
                equalSellOrderSize = TestData.GetData("TC23_EqualSellOrderSize");
                timeInForce = TestData.GetData("TimeInForce");
                fokTimeInForce = TestData.GetData("TC23_TimeInForce");
                trailingAmount = TestData.GetData("TC23_TrailingAmount");
                triggerTrailingPrice = TestData.GetData("TC23_TrailingPrice");
                limitOffset = TestData.GetData("TC23_LimitOffset");
                pegPrice = TestData.GetData("TC23_PegPrice");
                feeComponent = TestData.GetData("FeeComponent");
                buyLimitPrice1 = TestData.GetData("TC23_BuyLimitPrice_1");
                buyLimitPrice2 = TestData.GetData("TC23_BuyLimitPrice_2");
                buyLimitPrice3 = TestData.GetData("TC23_BuyLimitPrice_3");
                buyLimitPrice4 = TestData.GetData("TC23_BuyLimitPrice_4");
                buyLimitPrice5 = TestData.GetData("TC23_BuyLimitPrice_5");
                sellLimitPrice1 = TestData.GetData("TC23_SellLimitPrice_1");
                sellLimitPrice2 = TestData.GetData("TC23_SellLimitPrice_2");
                sellLimitPrice3 = TestData.GetData("TC23_SellLimitPrice_3");
                sellLimitPrice4 = TestData.GetData("TC23_SellLimitPrice_4");
                sellLimitPrice5 = TestData.GetData("TC23_SellLimitPrice_5");

                // Get buy order fee value based on buyOrderSize, feeComponent
                buyOrderFeeValue = GenericUtils.FeeAmount(buyOrderSize, feeComponent);
                // Get sell order fee value based on sellOrderSize, setMarketPrice, feeComponent
                sellOrderFeeValue = GenericUtils.SellFeeAmount(sellOrderSize, setMarketPrice, feeComponent);
                // Get initial trailing price based on setMarketPrice, trailingAmount
                initialTrailingPrice = GenericUtils.GetSumFromStringAfterAddition(setMarketPrice, trailingAmount);
                // Get final trailing price based on triggerTrailingPrice, limitOffset
                finalTrailingPrice = GenericUtils.GetSumFromStringAfterAddition(triggerTrailingPrice, limitOffset);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                OrderEntryPage orderEntryPage = new OrderEntryPage(driver, TestProgressLogger);

                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
                // Creating Buy and Sell Order to set the last price
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, setMarketPrice));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, setMarketPrice, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, setMarketPrice));

                // Scenario 1: Place Buy Trailing Stop Limit order with buy order size = sell order size

                // Place 5 Buy order and 5 Sell order with a difference of 1(in Limit price) and same order size using the same user
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BuyTrailingMarketSetupEnd, buyTab, buyOrderSize, buyLimitPrice1, buyLimitPrice2, buyLimitPrice3, buyLimitPrice4, buyLimitPrice5));
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.SellTrailingMarketSetupEnd, sellTab, sellOrderSize, sellLimitPrice1, sellLimitPrice2, sellLimitPrice3, sellLimitPrice4, sellLimitPrice5));
                
                // Place Trailing Stop Limit Buy Order 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, buyTab, instrument, orderType);
                var placeTrailingStopLimitBuyOrder = advanceOrder.PlaceTrailingStopLimitBuyOrder(buyOrderSize, trailingAmount, limitOffset, pegPrice, fokTimeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                // Verify success message
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedTrailingLimitOrderPlacedSuccessfully, buyTab, buyOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce));
                // Verify the order is present in Open orders tab                
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, buyTab, Const.TrailingStopLimit, Double.Parse(buyOrderSize), initialTrailingPrice, placeTrailingStopLimitBuyOrder["PlaceOrderTime"], placeTrailingStopLimitBuyOrder["PlaceOrderTimePlusOneMin"]));

                // Place sell order to reduce market price
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, buyLimitPrice3, timeInForce);

                // Place Sell Order to match the trailing stop limit order
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, equalSellOrderSize, finalTrailingPrice, timeInForce);

                // Place Buy Order to Increase the market
                userFunctions.LogIn(TestProgressLogger, Const.USER10);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, sellLimitPrice1, timeInForce);
                // As soon as the market changes it direction the Trailing order should get placed 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                // Verify that the order is present in Filled orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTabForTrailingOrders(instrument, buyTab, Double.Parse(buyOrderSize), finalTrailingPrice, buyOrderFeeValue, placeTrailingStopLimitBuyOrder["PlaceOrderTime"], placeTrailingStopLimitBuyOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvTrailingLimitFilledOrder, buyTab, buyOrderSize, finalTrailingPrice));

                // Scenario 2: Place Buy Trailing Stop Limit order with buy order size < sell order size

                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
                // Creating Buy and Sell Order to set the last price
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, setMarketPrice));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, setMarketPrice, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, setMarketPrice));
                // Place 5 Buy order and 5 Sell order with a difference of 1(in Limit price) and same order size using the same user
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BuyTrailingMarketSetupEnd, buyTab, buyOrderSize, buyLimitPrice1, buyLimitPrice2, buyLimitPrice3, buyLimitPrice4, buyLimitPrice5));
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.SellTrailingMarketSetupEnd, sellTab, sellOrderSize, sellLimitPrice1, sellLimitPrice2, sellLimitPrice3, sellLimitPrice4, sellLimitPrice5));

                // Place Trailing Stop Limit Buy Order 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, buyTab, instrument, orderType);
                var placeTrailingStopLimitBuyOrder2 = advanceOrder.PlaceTrailingStopLimitBuyOrder(buyOrderSize, trailingAmount, limitOffset, pegPrice, fokTimeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                // Verify success message
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedTrailingLimitOrderPlacedSuccessfully, buyTab, buyOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce));
                
                // Verify the order is present in Open orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, buyTab, Const.TrailingStopLimit, Double.Parse(buyOrderSize), initialTrailingPrice, placeTrailingStopLimitBuyOrder2["PlaceOrderTime"], placeTrailingStopLimitBuyOrder2["PlaceOrderTimePlusOneMin"]));

                // Place sell order to reduce market price
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, buyLimitPrice3, timeInForce);

                // Place Sell Order to match the trailing stop limit order
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, incSellOrderSize, finalTrailingPrice, timeInForce);

                // Place Buy Order to Increase the market
                userFunctions.LogIn(TestProgressLogger, Const.USER10);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, sellLimitPrice1, timeInForce);
                // As soon as the market changes it direction the Trailing order should get placed 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                // Verify that the order is present in Filled orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTabForTrailingOrders(instrument, buyTab, Double.Parse(buyOrderSize), finalTrailingPrice, buyOrderFeeValue, placeTrailingStopLimitBuyOrder2["PlaceOrderTime"], placeTrailingStopLimitBuyOrder2["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvTrailingLimitFilledOrder, buyTab, buyOrderSize, finalTrailingPrice));

                // Scenario 3: Place Buy Trailing Stop Limit order with buy order size > sell order size

                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER7);
                // Creating Buy and Sell Order to set the last price
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, setMarketPrice));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, setMarketPrice, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, setMarketPrice));
                // Place 5 Buy order and 5 Sell order with a difference of 1(in Limit price) and same order size using the same user
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BuyTrailingMarketSetupEnd, buyTab, buyOrderSize, buyLimitPrice1, buyLimitPrice2, buyLimitPrice3, buyLimitPrice4, buyLimitPrice5));
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.SellTrailingMarketSetupEnd, sellTab, sellOrderSize, sellLimitPrice1, sellLimitPrice2, sellLimitPrice3, sellLimitPrice4, sellLimitPrice5));

                // Place Trailing Stop Limit Buy Order 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, buyTab, instrument, orderType);
                var placeTrailingStopLimitBuyOrder3 = advanceOrder.PlaceTrailingStopLimitBuyOrder(buyOrderSize, trailingAmount, limitOffset, pegPrice, fokTimeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                // Verify success message
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedTrailingLimitOrderPlacedSuccessfully, buyTab, buyOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce));
                // Verify the order is present in Open orders tab
                initialTrailingPrice = GenericUtils.GetSumFromStringAfterAddition(setMarketPrice, trailingAmount);
                // Verify the order is present in Open orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, buyTab, Const.TrailingStopLimit, Double.Parse(buyOrderSize), initialTrailingPrice, placeTrailingStopLimitBuyOrder3["PlaceOrderTime"], placeTrailingStopLimitBuyOrder3["PlaceOrderTimePlusOneMin"]));

                // Place sell order to reduce market price
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, buyLimitPrice3, timeInForce);

                // Place Sell Order to match the trailing stop limit order
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, decSellOrderSize, finalTrailingPrice, timeInForce);

                // Place Buy Order to Increase the market
                userFunctions.LogIn(TestProgressLogger, Const.USER10);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, sellLimitPrice1, timeInForce);
                // As soon as the market changes it direction the Trailing order should get placed 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                var filledOrderSize = GenericUtils.GetDifferenceFromStringAfterSubstraction(decSellOrderSize, buyOrderSize);
                // Get fee value based on filled OrderSize
                buyOrderFeeValue = GenericUtils.FeeAmount(filledOrderSize, feeComponent);
                // Verify that the order is present in Filled orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTabForTrailingOrders(instrument, buyTab, Double.Parse(filledOrderSize), finalTrailingPrice, buyOrderFeeValue, placeTrailingStopLimitBuyOrder3["PlaceOrderTime"], placeTrailingStopLimitBuyOrder3["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvTrailingLimitFilledOrder, buyTab, filledOrderSize, finalTrailingPrice));
                // Verify that the order not fulfilled is present in inactive orders tab
                Assert.True(objVerifyOrdersTab.VerifyInactiveOrdersTab(instrument, buyTab, Const.TrailingStopLimit, Double.Parse(buyOrderSize), triggerTrailingPrice, placeTrailingStopLimitBuyOrder3["PlaceOrderTime"], placeTrailingStopLimitBuyOrder3["PlaceOrderTimePlusOneMin"], Const.CancelledStatus));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvStopLimitInactiveOrder, buyTab, Const.TrailingStopLimit, buyOrderSize, triggerTrailingPrice));
                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER9);
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifyAdvTrailingLimitOrderFailed, buyTab), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifyAdvTrailingLimitOrderFailed, buyTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }

        [Fact]
        public void TC24_VerifyAdvTrailingStopLimitSellOrderMultiScenario()
        {
            try
            {
                string successMsg;
                string askPrice;
                string initialTrailingPrice;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("TrailingStopLimit");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                setMarketPrice = TestData.GetData("TC24_SetMarketPrice");
                buyOrderSize = TestData.GetData("TC24_BuyOrderSize");
                sellOrderSize = TestData.GetData("TC24_SellOrderSize");
                incBuyOrderSize = TestData.GetData("TC24_IncBuyOrderSize");
                decBuyOrderSize = TestData.GetData("TC24_DecBuyOrderSize");
                equalBuyOrderSize = TestData.GetData("TC24_EqualBuyOrderSize");
                timeInForce = TestData.GetData("TimeInForce");
                fokTimeInForce = TestData.GetData("TC21_TimeInForce");
                trailingAmount = TestData.GetData("TC24_TrailingAmount");
                triggerTrailingPrice = TestData.GetData("TC24_TrailingPrice");
                limitOffset = TestData.GetData("TC24_LimitOffset");
                pegPrice = TestData.GetData("TC24_PegPrice");
                feeComponent = TestData.GetData("FeeComponent");
                buyLimitPrice1 = TestData.GetData("TC24_BuyLimitPrice_1");
                buyLimitPrice2 = TestData.GetData("TC24_BuyLimitPrice_2");
                buyLimitPrice3 = TestData.GetData("TC24_BuyLimitPrice_3");
                buyLimitPrice4 = TestData.GetData("TC24_BuyLimitPrice_4");
                buyLimitPrice5 = TestData.GetData("TC24_BuyLimitPrice_5");
                sellLimitPrice1 = TestData.GetData("TC24_SellLimitPrice_1");
                sellLimitPrice2 = TestData.GetData("TC24_SellLimitPrice_2");
                sellLimitPrice3 = TestData.GetData("TC24_SellLimitPrice_3");
                sellLimitPrice4 = TestData.GetData("TC24_SellLimitPrice_4");
                sellLimitPrice5 = TestData.GetData("TC24_SellLimitPrice_5");

                // Get initial trailing price based on setMarketPrice, trailingAmount
                initialTrailingPrice = GenericUtils.GetDifferenceFromStringAfterSubstraction(setMarketPrice, trailingAmount);
                // Get final trailing price based on triggerTrailingPrice, limitOffset
                finalTrailingPrice = GenericUtils.GetDifferenceFromStringAfterSubstraction(triggerTrailingPrice, limitOffset);
                // Get buy order fee value based on buyOrderSize, feeComponent
                buyOrderFeeValue = GenericUtils.FeeAmount(buyOrderSize, feeComponent);
                // Get sell order fee value based on sellOrderSize, setMarketPrice, feeComponent
                sellOrderFeeValue = GenericUtils.SellFeeAmount(sellOrderSize, setMarketPrice, feeComponent);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                OrderEntryPage orderEntryPage = new OrderEntryPage(driver, TestProgressLogger);

                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
                // Creating Buy and Sell Order to set the last price
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, setMarketPrice));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, setMarketPrice, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, setMarketPrice));

                // Scenario 1: Place Sell Trailing Stop Limit order with sell order size = buy order size

                // Place 5 Buy order and 5 Sell order with a difference of 1(in Limit price) and same order size using the same user
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BuyTrailingMarketSetupEnd, buyTab, buyOrderSize, buyLimitPrice1, buyLimitPrice2, buyLimitPrice3, buyLimitPrice4, buyLimitPrice5));
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.SellTrailingMarketSetupEnd, sellTab, sellOrderSize, sellLimitPrice1, sellLimitPrice2, sellLimitPrice3, sellLimitPrice4, sellLimitPrice5));

                // Place Trailing Stop Limit Sell Order 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, sellTab, instrument, orderType);
                var placeTrailingStopLimitSellOrder = advanceOrder.PlaceTrailingStopLimitSellOrder(sellOrderSize, trailingAmount, limitOffset, pegPrice, fokTimeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                // Verify success message
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedTrailingLimitOrderPlacedSuccessfully, sellTab, sellOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce));
                // Verify the order is present in Open orders tab                
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, sellTab, Const.TrailingStopLimit, Double.Parse(sellOrderSize), initialTrailingPrice, placeTrailingStopLimitSellOrder["PlaceOrderTime"], placeTrailingStopLimitSellOrder["PlaceOrderTimePlusOneMin"]));

                // Place buy order to increase market price
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, sellLimitPrice3, timeInForce);

                // Place Buy Order to match the trailing stop limit order - at price 50.70
                userFunctions.LogIn(TestProgressLogger, Const.USER11);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, equalBuyOrderSize, finalTrailingPrice, timeInForce);

                // Place sell Order to reduce the market
                userFunctions.LogIn(TestProgressLogger, Const.USER10);
                userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, buyLimitPrice1, timeInForce);
                // As soon as the market changes it direction the Trailing order should get placed 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                // Get fee value based on final TrailingPrice
                sellOrderFeeValue = GenericUtils.SellFeeAmount(sellOrderSize, finalTrailingPrice, feeComponent);
                // Verify that the order is present in Filled orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTabForTrailingOrders(instrument, sellTab, Double.Parse(sellOrderSize), finalTrailingPrice, sellOrderFeeValue, placeTrailingStopLimitSellOrder["PlaceOrderTime"], placeTrailingStopLimitSellOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvTrailingLimitFilledOrder, sellTab, sellOrderSize, finalTrailingPrice));

                // Scenario 2: Place Sell Trailing Stop Limit order with sell order size < buy order size

                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
                // Creating Buy and Sell Order to set the last price
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, setMarketPrice));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, setMarketPrice, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, setMarketPrice));
                // Place 5 Buy order and 5 Sell order with a difference of 1(in Limit price) and same order size using the same user
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BuyTrailingMarketSetupEnd, buyTab, buyOrderSize, buyLimitPrice1, buyLimitPrice2, buyLimitPrice3, buyLimitPrice4, buyLimitPrice5));
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.SellTrailingMarketSetupEnd, sellTab, sellOrderSize, sellLimitPrice1, sellLimitPrice2, sellLimitPrice3, sellLimitPrice4, sellLimitPrice5));

                // Place Trailing Stop Limit Sell Order 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, sellTab, instrument, orderType);
                var placeTrailingStopLimitSellOrder2 = advanceOrder.PlaceTrailingStopLimitSellOrder(sellOrderSize, trailingAmount, limitOffset, pegPrice, fokTimeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                // Verify success message
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedTrailingLimitOrderPlacedSuccessfully, sellTab, sellOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce));
                // Verify the order is present in Open orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, sellTab, Const.TrailingStopLimit, Double.Parse(sellOrderSize), initialTrailingPrice, placeTrailingStopLimitSellOrder2["PlaceOrderTime"], placeTrailingStopLimitSellOrder2["PlaceOrderTimePlusOneMin"]));

                // Place buy order to increase market price
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, sellLimitPrice3, timeInForce);
                
                // Place Buy Order to match the trailing stop limit order - at price 50.70
                userFunctions.LogIn(TestProgressLogger, Const.USER11);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, incBuyOrderSize, finalTrailingPrice, timeInForce);
                
                // Place sell Order to reduce the market
                userFunctions.LogIn(TestProgressLogger, Const.USER10);
                userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, buyLimitPrice1, timeInForce);
                // As soon as the market changes it direction the Trailing order should get placed 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                // Get fee value based on final TrailingPrice
                sellOrderFeeValue = GenericUtils.SellFeeAmount(sellOrderSize, finalTrailingPrice, feeComponent);
                // Verify that the order is present in Filled orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTabForTrailingOrders(instrument, sellTab, Double.Parse(sellOrderSize), finalTrailingPrice, sellOrderFeeValue, placeTrailingStopLimitSellOrder2["PlaceOrderTime"], placeTrailingStopLimitSellOrder2["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvTrailingLimitFilledOrder, sellTab, sellOrderSize, finalTrailingPrice));

                // Scenario 3: Place Sell Trailing Stop Limit order with sell order size > buy order size

                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER11);
                // Creating Buy and Sell Order to set the last price
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, setMarketPrice));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, setMarketPrice, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, setMarketPrice));
                // Place 5 Buy order and 5 Sell order with a difference of 1(in Limit price) and same order size using the same user
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, buyLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, buyLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BuyTrailingMarketSetupEnd, buyTab, buyOrderSize, buyLimitPrice1, buyLimitPrice2, buyLimitPrice3, buyLimitPrice4, buyLimitPrice5));
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice3, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice4, timeInForce);
                orderEntryPage.PlaceMultipleLimitSellOrder(instrument, sellTab, sellOrderSize, sellLimitPrice5, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.SellTrailingMarketSetupEnd, sellTab, sellOrderSize, sellLimitPrice1, sellLimitPrice2, sellLimitPrice3, sellLimitPrice4, sellLimitPrice5));
                
                // Place Trailing Stop Limit Sell Order 
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                advanceOrder.NavigateToAdvanceOrdersSection(driver, sellTab, instrument, orderType);
                var placeTrailingStopLimitSellOrder3 = advanceOrder.PlaceTrailingStopLimitSellOrder(sellOrderSize, trailingAmount, limitOffset, pegPrice, fokTimeInForce);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                // Verify success message
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedTrailingLimitOrderPlacedSuccessfully, sellTab, sellOrderSize, trailingAmount, limitOffset, pegPrice, timeInForce));
                // Verify the order is present in Open orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, sellTab, Const.TrailingStopLimit, Double.Parse(sellOrderSize), initialTrailingPrice, placeTrailingStopLimitSellOrder3["PlaceOrderTime"], placeTrailingStopLimitSellOrder3["PlaceOrderTimePlusOneMin"]));
                
                // Place buy order to increase market price
                userFunctions.LogIn(TestProgressLogger, Const.USER7);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, sellLimitPrice1, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, sellLimitPrice2, timeInForce);
                orderEntryPage.PlaceMultipleLimitBuyOrder(instrument, buyTab, buyOrderSize, sellLimitPrice3, timeInForce);

                // Place Buy Order to match the trailing stop limit order - at price 50.70
                userFunctions.LogIn(TestProgressLogger, Const.USER11);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, decBuyOrderSize, finalTrailingPrice, timeInForce);

                // Place sell Order to reduce the market
                userFunctions.LogIn(TestProgressLogger, Const.USER10);
                userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, buyLimitPrice1, timeInForce);
                // As soon as the market changes it direction the Trailing order should get placed 
                finalTrailingPrice = GenericUtils.GetDifferenceFromStringAfterSubstraction(triggerTrailingPrice, limitOffset);
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                var filledOrderSize = GenericUtils.GetDifferenceFromStringAfterSubstraction(decBuyOrderSize, sellOrderSize);
                // Get fee value based on final TrailingPrice
                sellOrderFeeValue = GenericUtils.SellFeeAmount(filledOrderSize, finalTrailingPrice, feeComponent);
                // Verify that the order not fulfilled is present in inactive orders tab
                Assert.True(objVerifyOrdersTab.VerifyInactiveOrdersTab(instrument, sellTab, Const.TrailingStopLimit, Double.Parse(sellOrderSize), triggerTrailingPrice, placeTrailingStopLimitSellOrder3["PlaceOrderTime"], placeTrailingStopLimitSellOrder3["PlaceOrderTimePlusOneMin"], Const.CancelledStatus));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedAdvStopLimitInactiveOrder, sellTab, Const.TrailingStopLimit, sellOrderSize, triggerTrailingPrice));
                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER11);
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifyAdvTrailingLimitOrderFailed, sellTab), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifyAdvTrailingLimitOrderFailed, sellTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }

        [Fact]
        public void TC25_VerifyIOCAdvanceBuyOrder()
        {
            try
            {
                string feeValue;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("OrderType");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                orderSize = TestData.GetData("TC25_OrderSize");
                limitPrice = TestData.GetData("TC25_LimitPrice");
                timeInForce = TestData.GetData("TimeInForce");
                feeComponent = TestData.GetData("FeeComponent");

                // Get fee value based on orderSize
                feeValue = GenericUtils.FeeAmount(orderSize, feeComponent);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                // Creating Buy and Sell Order to set the last price
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, limitPrice));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, orderSize, limitPrice, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, limitPrice));
                // Place Sell order to set market
                userFunctions.LogIn(TestProgressLogger, Const.USER8);                
                userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, orderSize, limitPrice, timeInForce);                
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, sellTab, orderSize, limitPrice));
                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER9);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CancelOrders));
                // Navigate to Advance Orders section and Place IOC BuyOrder
                UserCommonFunctions.AdvanceOrder(driver);               
                advanceOrder.SelectBuyOrSellTab(buyTab);
                advanceOrder.SelectInstrumentsAndOrderType(instrument, orderType);
                var placeIOCBuyOrderTime = advanceOrder.PlaceBuyOrderWithImmediateOrCancelType(orderSize, limitPrice);
                string successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                // Verify success message
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedOrderPlacedSuccessfully, buyTab, orderSize, limitPrice));
                // Verify that the order is present in Filled orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, buyTab, double.Parse(orderSize), feeValue, placeIOCBuyOrderTime["PlaceOrderTime"], placeIOCBuyOrderTime["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedEntryInFilledOrdersTab, instrument, buyTab, orderSize, placeIOCBuyOrderTime));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.IOCOrderTypeFailedMsg, buyTab), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.IOCOrderTypeFailedMsg, buyTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }

        [Fact]
        public void TC26_VerifyIOCAdvanceBuyOrderLimitAskPrice()
        {
            try
            {
                string feeValue;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("OrderType");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                orderSize = TestData.GetData("OrderSize");
                buyOrderLimitPrice = TestData.GetData("LimitPrice");
                sellOrderLimitPrice = TestData.GetData("TC26_SellOrderLimitPrice");
                timeInForce = TestData.GetData("TimeInForce");
                feeComponent = TestData.GetData("FeeComponent");

                // Get fee value based on orderSize
                feeValue = GenericUtils.FeeAmount(orderSize, feeComponent);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                // Creating Buy and Sell Order to set the last price
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, buyOrderLimitPrice));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, orderSize, buyOrderLimitPrice, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, buyOrderLimitPrice));
                // Place Sell order to set market
                userFunctions.LogIn(TestProgressLogger, Const.USER8);              
                userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, orderSize, sellOrderLimitPrice, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, sellTab, orderSize, sellOrderLimitPrice));
                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER9);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CancelOrders));
                // Navigate to Advance Orders section and Place IOC BuyOrder
                UserCommonFunctions.AdvanceOrder(driver);               
                advanceOrder.SelectBuyOrSellTab(buyTab);
                advanceOrder.SelectInstrumentsAndOrderType(instrument, orderType);
                var placeIOCBuyOrder = advanceOrder.PlaceBuyOrderWithImmediateOrCancelType(orderSize, buyOrderLimitPrice);
                string cancelledMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                // Verify cancelled message
                Assert.Equal(Const.OrderCancelledMsg, cancelledMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedOrderPlacedSuccessfully, buyTab, orderSize, buyOrderLimitPrice));
                // Verify that the order not fulfilled is present in inactive orders tab
                Assert.True(objVerifyOrdersTab.VerifyInactiveOrdersTab(instrument, buyTab, Const.Limit, Double.Parse(orderSize), buyOrderLimitPrice, placeIOCBuyOrder["PlaceOrderTime"], placeIOCBuyOrder["PlaceOrderTimePlusOneMin"], Const.CancelledStatus));
                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.AdvanceIOCOrderFailureMsg, buyTab), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.AdvanceIOCOrderFailureMsg, buyTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }

        }

        [Fact]
        public void TC27_VerifyPartiallyIOCAdvanceBuyOrderLimitAskPrice()
        {
            try
            {
                string feeValue;
                string askPrice;
                string cancelledMsg;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("OrderType");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                buyOrderLimitPrice = TestData.GetData("TC27_BuyOrderLimitPrice");
                sellOrderLimitPrice = TestData.GetData("TC27_SellOrderLimitPrice");
                buyOrderSize = TestData.GetData("TC27_BuyOrderSize");
                sellOrderSize = TestData.GetData("TC27_SellOrderSize");
                timeInForce = TestData.GetData("TimeInForce");
                feeComponent = TestData.GetData("FeeComponent");

                // Get fee value based on orderSize
                feeValue = GenericUtils.FeeAmount(sellOrderSize, feeComponent);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);

                // Creating Buy and Sell Order to set the last price
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, buyOrderLimitPrice));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, sellOrderSize, sellOrderLimitPrice, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, buyOrderLimitPrice));
                // Place Sell order to set market
                userFunctions.LogIn(TestProgressLogger, Const.USER8);               
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, sellOrderLimitPrice, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, sellTab, sellOrderSize, sellOrderLimitPrice));
               
                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER9);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CancelOrders));
                // Place advance buy order with order size > sell order size
                UserCommonFunctions.AdvanceOrder(driver);
                advanceOrder.SelectBuyOrSellTab(buyTab);
                advanceOrder.SelectInstrumentsAndOrderType(instrument, orderType);
                var placeIOCBuyOrder = advanceOrder.PlaceBuyOrderWithImmediateOrCancelType(buyOrderSize, buyOrderLimitPrice);
                cancelledMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                // Verify cancelled message
                Assert.Equal(Const.OrderCancelledMsg, cancelledMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedOrderPlacedSuccessfully, buyTab, buyOrderSize, buyOrderLimitPrice));
                
                // Verify that the order is present in Filled orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, buyTab, double.Parse(sellOrderSize), feeValue, placeIOCBuyOrder["PlaceOrderTime"], placeIOCBuyOrder["PlaceOrderTimePlusOneMin"]));
                // Verify that the order not fulfilled is present in inactive orders tab
                Assert.True(objVerifyOrdersTab.VerifyInactiveOrdersTab(instrument, buyTab, Const.Limit, Double.Parse(buyOrderSize), buyOrderLimitPrice, placeIOCBuyOrder["PlaceOrderTime"], placeIOCBuyOrder["PlaceOrderTimePlusOneMin"], Const.CancelledStatus));
                // Cancel all the remaining orders post verification
                UserCommonFunctions.CancelAllOrders(driver);
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.AdvanceIOCOrderFailureMsg, buyTab), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.AdvanceIOCOrderFailureMsg, buyTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }

        }

        [Fact]
        public void TC28_VerifyIOCAdvanceSellOrder()
        {
            try
            {
                string feeValue;
                string askPrice;
                string successMsg;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("OrderType");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                orderSize = TestData.GetData("OrderSize");
                limitPrice = TestData.GetData("LimitPrice");
                timeInForce = TestData.GetData("TimeInForce");
                feeComponent = TestData.GetData("FeeComponent");
                feeValue = GenericUtils.SellFeeAmount(orderSize, limitPrice, feeComponent);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                // Creating Buy and Sell Order to set the last price
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, limitPrice));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, orderSize, limitPrice, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, limitPrice));
                // Place Limit Buy Order to set the market
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, orderSize, limitPrice, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, buyTab, orderSize, limitPrice));
                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER9);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CancelOrders));
                // Navigate to Advance Orders section and Place IOC SellOrder
                UserCommonFunctions.AdvanceOrder(driver);
                advanceOrder.SelectBuyOrSellTab(sellTab);
                advanceOrder.SelectInstrumentsAndOrderType(instrument, orderType);
                var placeIOCSellOrderTime = advanceOrder.PlaceSellOrderWithImmediateOrCancelType(orderSize, limitPrice);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                // Verify success message
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedOrderPlacedSuccessfully, sellTab, orderSize, limitPrice));
                // Verify that the order is present in Filled orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, sellTab, double.Parse(orderSize), feeValue, placeIOCSellOrderTime["PlaceOrderTime"], placeIOCSellOrderTime["PlaceOrderTimePlusOneMin"]));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.AdvanceIOCOrderFailureMsg, sellTab), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.AdvanceIOCOrderFailureMsg, sellTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }

        }

        [Fact]
        public void TC29_VerifyIOCAdvanceSellOrderMoreThanBidPrice()
        {
            try
            {
                string feeValue;
                string askPrice;
                string cancelledMsg;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("OrderType");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                orderSize = TestData.GetData("TC29_OrderSize");
                buyOrderLimitPrice = TestData.GetData("TC29_BuyOrderLimitPrice");
                sellOrderLimitPrice = TestData.GetData("TC29_SellOrderLimitPrice");
                timeInForce = TestData.GetData("TimeInForce");
                feeComponent = TestData.GetData("FeeComponent");

                // Get fee value based on orderSize, sell Order LimitPrice
                feeValue = GenericUtils.SellFeeAmount(orderSize, sellOrderLimitPrice, feeComponent);                
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);

                TestProgressLogger.StartTest();
                // Creating Buy and Sell Order to set the last price
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, buyOrderLimitPrice));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, orderSize, buyOrderLimitPrice, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, buyOrderLimitPrice));
                // Place Limit Buy Order to set the market
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, orderSize, buyOrderLimitPrice, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, buyTab, orderSize, buyOrderLimitPrice));
                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER9);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CancelOrders));
                // Navigate to Advance Orders section and Place IOC SellOrder
                UserCommonFunctions.AdvanceOrder(driver);                
                advanceOrder.SelectBuyOrSellTab(sellTab);
                advanceOrder.SelectInstrumentsAndOrderType(instrument, orderType);
                var placeIOCSellOrder = advanceOrder.PlaceSellOrderWithImmediateOrCancelType(orderSize, sellOrderLimitPrice);
                cancelledMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                // Verify cancelled message
                Assert.Equal(Const.OrderCancelledMsg, cancelledMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedOrderPlacedSuccessfully, sellTab, orderSize, sellOrderLimitPrice));
                // Verify that the order not fulfilled is present in inactive orders tab
                Assert.True(objVerifyOrdersTab.VerifyInactiveOrdersTab(instrument, sellTab, Const.Limit, Double.Parse(orderSize), sellOrderLimitPrice, placeIOCSellOrder["PlaceOrderTime"], placeIOCSellOrder["PlaceOrderTimePlusOneMin"], Const.CancelledStatus));
                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.AdvanceIOCOrderFailureMsg, sellTab), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.AdvanceIOCOrderFailureMsg, sellTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }

        }

        [Fact]
        public void TC30_VerifyPartiallyIOCAdvanceSellOrderMoreThanBidPrice()
        {
            try
            {
                string feeValue;
                string askPrice;
                string cancelledMsg;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("OrderType");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                buyOrderLimitPrice = TestData.GetData("TC30_BuyOrderLimitPrice");
                sellOrderLimitPrice = TestData.GetData("TC30_SellOrderLimitPrice");
                buyOrderSize = TestData.GetData("TC30_BuyOrderSize");
                sellOrderSize = TestData.GetData("TC30_SellOrderSize");
                timeInForce = TestData.GetData("TimeInForce");
                feeComponent = TestData.GetData("FeeComponent");
                feeValue = GenericUtils.SellFeeAmount(buyOrderSize, buyOrderLimitPrice, feeComponent);
              
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);

                TestProgressLogger.StartTest();
                // Creating Buy and Sell Order to set the last price
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, buyOrderLimitPrice));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, buyOrderLimitPrice, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, buyOrderLimitPrice));
                // Place Limit Buy Order to set the market
                userFunctions.LogIn(TestProgressLogger, Const.USER8);               
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, buyOrderLimitPrice, timeInForce);         
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, buyTab, buyOrderSize, buyOrderLimitPrice));
                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER9);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CancelOrders));
                // Navigate to Advance Orders section and Place IOC SellOrder
                UserCommonFunctions.AdvanceOrder(driver);                
                advanceOrder.SelectBuyOrSellTab(sellTab);
                advanceOrder.SelectInstrumentsAndOrderType(instrument, orderType);
                var placeIOCSellOrder = advanceOrder.PlaceSellOrderWithImmediateOrCancelType(sellOrderSize, sellOrderLimitPrice);
                cancelledMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                // Verify cancelled message
                Assert.Equal(Const.OrderCancelledMsg, cancelledMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvancedOrderPlacedSuccessfully, sellTab, sellOrderSize, sellOrderLimitPrice));
                // Verify that the order is present in Filled orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, sellTab, double.Parse(buyOrderSize), feeValue, placeIOCSellOrder["PlaceOrderTime"], placeIOCSellOrder["PlaceOrderTimePlusOneMin"]));
                // Verify that the order not fulfilled is present in inactive orders tab
                Assert.True(objVerifyOrdersTab.VerifyInactiveOrdersTab(instrument, sellTab, Const.Limit, Double.Parse(sellOrderSize), sellOrderLimitPrice, placeIOCSellOrder["PlaceOrderTime"], placeIOCSellOrder["PlaceOrderTimePlusOneMin"], Const.CancelledStatus));
                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.AdvanceIOCOrderFailureMsg, sellTab), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.AdvanceIOCOrderFailureMsg, sellTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }

        }

        [Fact]
        public void TC31_VerifyPlaceBuyOrderWithReservOrderType()
        {
            try
            {
                string successMsg;
                string type = Const.Limit;
                instrument = TestData.GetData("Instrument");
                reserveOrder = TestData.GetData("ReserveOrder");
                buyTab = TestData.GetData("BuyTab");
                buyOrderLimitPrice = TestData.GetData("TC31_BuyOrderLimitPrice");
                buyOrderSize = TestData.GetData("TC31_BuyOrderSize");
                buyOrderDisplayQty = TestData.GetData("TC31_BuyOrderDisplayQty");
                
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                AdvancedOrderPage advanceorder = new AdvancedOrderPage(TestProgressLogger);

                TestProgressLogger.StartTest();
                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CancelOrders));
                // Navigate to Advance Orders section and Place Reserved Buy Order
                UserCommonFunctions.AdvanceOrder(driver);
                advanceorder.SelectBuyOrSellTab(buyTab);
                advanceorder.SelectInstrumentsAndOrderType(instrument, reserveOrder);
                var placeReserveBuyOrder = advanceorder.PlaceBuyOrderWithReserveOrderType(buyOrderSize, buyOrderLimitPrice, buyOrderDisplayQty);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                // Verify success message
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvanceReserveOrderSuccessMsg, buyTab, buyOrderSize, buyOrderLimitPrice, buyOrderDisplayQty));
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                // Verify the order is present in Open orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, buyTab, type, Double.Parse(buyOrderSize), buyOrderLimitPrice, placeReserveBuyOrder["PlaceOrderTime"], placeReserveBuyOrder["PlaceOrderTimePlusOneMin"]));
                UserCommonFunctions.CancelAllOrders(driver);
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.AdvanceReserveOrderFailureMsg, buyTab), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.AdvanceReserveOrderFailureMsg, buyTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }

        [Fact]
        public void TC32_VerifyPlaceSellOrderWithReserveOrderType()
        {
            try
            {
                string successMsg;
                string type = Const.Limit;
                instrument = TestData.GetData("Instrument");
                reserveOrder = TestData.GetData("ReserveOrder");
                sellTab = TestData.GetData("SellTab");
                sellOrderLimitPrice = TestData.GetData("TC32_SellOrderLimitPrice");
                sellOrderSize = TestData.GetData("TC32_SellOrderSize");
                sellOrderDisplayQty = TestData.GetData("TC32_SellOrderDisplayQty");

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                AdvancedOrderPage advanceorder = new AdvancedOrderPage(TestProgressLogger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);

                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CancelOrders));
                // Navigate to Advance Orders section and Place Reserved Sell Order
                UserCommonFunctions.AdvanceOrder(driver);
                advanceorder.SelectBuyOrSellTab(sellTab);
                advanceorder.SelectInstrumentsAndOrderType(instrument, reserveOrder);
                var placeReserveSellOrder = advanceorder.PlaceSellOrderWithReserveOrderType(sellOrderSize, sellOrderLimitPrice, sellOrderDisplayQty);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                // Verify success message
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvanceReserveOrderSuccessMsg, sellTab, sellOrderSize, sellOrderLimitPrice, sellOrderDisplayQty));
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                // Verify the order is present in Open orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, sellTab, type, Double.Parse(sellOrderSize), sellOrderLimitPrice, placeReserveSellOrder["PlaceOrderTime"], placeReserveSellOrder["PlaceOrderTimePlusOneMin"]));
                UserCommonFunctions.CancelAllOrders(driver);
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.AdvanceReserveOrderFailureMsg, sellTab), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.AdvanceReserveOrderFailureMsg, sellTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }

        [Fact]
        public void TC13_VerifyStopMarketBuyOrder()
        {
            try
            {
                string placeBuyOrderTime;
                string placeBuyOrderTimePlusOneMin;
                string placeSellOrderTime;
                string placeSellOrderTimePlusOneMin;
                string successMsg;
                string feeValue;
                string askPrice;
                string sellOrderPrice;
                string type = Const.StopMarket;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("OrderType");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                orderSize = TestData.GetData("TC13_SellOrderSize");
                limitPrice = TestData.GetData("TC13_LimitPrice");
                timeInForce = TestData.GetData("TC13_TimeInForce");
                stopPrice = TestData.GetData("TC13_StopPrice");
                feeComponent = TestData.GetData("FeeComponent");
                orderTypeDropdown = TestData.GetData("StopMarketOrder");
                limitPriceEqualsStop = TestData.GetData("TC13_LimitPriceEqualsStop");
                feeValue = GenericUtils.FeeAmount(orderSize, feeComponent);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);

                // Create Buy and Sell Order to set the last price
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, limitPrice));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, orderSize, limitPrice, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, limitPrice));

                // Place sell order to set up market
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, orderSize, limitPrice, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, sellTab, orderSize, limitPrice));

                // Creating Advance Stop Market Order - Buy Order
                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER9);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CancelOrders));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvanceStopMarketOrderSetUp, buyTab, orderSize, stopPrice));
                // Navigate to Advance order and Place Stop MarketBuyOrder with stop price > limit price
                UserCommonFunctions.AdvanceOrder(driver);
                advanceOrder.SelectBuyOrSellTab(buyTab);
                advanceOrder.SelectInstrumentsAndOrderType(instrument, orderTypeDropdown);
                var placeStopMarketBuyOrder = advanceOrder.PlaceStopMarketBuyOrder(orderSize, stopPrice);
                placeBuyOrderTime = placeStopMarketBuyOrder["PlaceOrderTime"];
                placeBuyOrderTimePlusOneMin = placeStopMarketBuyOrder["PlaceOrderTimePlusOneMin"];
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                // Verify success message
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);

                // Verify the order in Open orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, buyTab, type, Double.Parse(orderSize), stopPrice, placeStopMarketBuyOrder["PlaceOrderTime"], placeStopMarketBuyOrder["PlaceOrderTimePlusOneMin"]));

                // Creating Buy and Sell Order to match last price to Stop Price
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, limitPriceEqualsStop));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, orderSize, limitPriceEqualsStop, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, limitPriceEqualsStop));

                // Create a sell order to execute the Stop buy order
                userFunctions.LogIn(TestProgressLogger, Const.USER1);
                sellOrderPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, orderSize, limitPriceEqualsStop, timeInForce);
                UserCommonFunctions.ConfirmWindowOrder(sellOrderPrice, limitPrice, driver);
                placeSellOrderTime = GenericUtils.GetCurrentTime();
                placeSellOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, sellTab, orderSize, limitPriceEqualsStop));

                // Verify that the Stop order is executed
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                // Verify that the order is present in Filled orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, buyTab, double.Parse(orderSize), feeValue, placeSellOrderTime, placeSellOrderTimePlusOneMin));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvanceStopMarketOrderSuccessMsg, buyTab, orderSize));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.AdvanceStopMarketOrderFailureMsg, buyTab, orderSize), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.AdvanceStopMarketOrderFailureMsg, buyTab, orderSize), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }

        [Fact]
        public void TC14_VerifyStopMarketSellOrder()
        {
            try
            {
                string type;
                string feeValue;
                string placeBuyOrderTime;
                string placeBuyOrderTimePlusOneMin;
                string placeSellOrderTime;
                string placeSellOrderTimePlusOneMin;
                string successMsg;
                string askPrice;
                string buyOrderPrice;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("OrderType");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                orderSize = TestData.GetData("TC14_SellOrderSize");
                limitPrice = TestData.GetData("TC14_LimitPrice");
                timeInForce = TestData.GetData("TC14_TimeInForce");
                stopPrice = TestData.GetData("TC14_StopPrice");
                feeComponent = TestData.GetData("FeeComponent");
                orderTypeDropdown = TestData.GetData("StopMarketOrder");
                limitPriceEqualsStop = TestData.GetData("TC14_LimitPriceEqualsStop");

                type = Const.StopMarket;
                feeValue = GenericUtils.SellFeeAmount(orderSize, stopPrice, feeComponent);

                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);

                TestProgressLogger.StartTest();
                // Create Buy and Sell Order to set the last price
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, limitPrice));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, orderSize, limitPrice, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, limitPrice));

                // Place Limit Buy Order to set the market
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, orderSize, limitPrice, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, buyTab, orderSize, limitPrice));

                // Creating Advance Stop Market Order - Sell Order
                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER9);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CancelOrders));
                // Navigate to Advance orders section to place Stop MarketSellOrder with stop price < limit price
                UserCommonFunctions.AdvanceOrder(driver);
                advanceOrder.SelectBuyOrSellTab(sellTab);
                advanceOrder.SelectInstrumentsAndOrderType(instrument, orderTypeDropdown);
                var placeStopMarketSellOrder = advanceOrder.PlaceStopMarketSellOrder(orderSize, stopPrice);
                placeSellOrderTime = placeStopMarketSellOrder["PlaceOrderTime"];
                placeSellOrderTimePlusOneMin = placeStopMarketSellOrder["PlaceOrderTimePlusOneMin"];
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                // Verify success message
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);

                // Verify the order in Open orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, sellTab, type, Double.Parse(orderSize), stopPrice, placeStopMarketSellOrder["PlaceOrderTime"], placeStopMarketSellOrder["PlaceOrderTimePlusOneMin"]));

                // Creating Buy and Sell Order to set the last price to match Stop Price
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, limitPriceEqualsStop));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, orderSize, limitPriceEqualsStop, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, limitPriceEqualsStop));

                // Create a buyer to execute the Stop order
                userFunctions.LogIn(TestProgressLogger, Const.USER1);
                buyOrderPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, orderSize, limitPriceEqualsStop, timeInForce);
                placeBuyOrderTime = GenericUtils.GetCurrentTime();
                placeBuyOrderTimePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, sellTab, orderSize, limitPriceEqualsStop));

                // Verify that the Stop order is executed
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                // Verify that the order is present in Filled orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, sellTab, double.Parse(orderSize), feeValue, placeBuyOrderTime, placeBuyOrderTimePlusOneMin));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvanceStopMarketOrderSuccessMsg, sellTab, orderSize));
                // Login and cancel all the previous orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER1);
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.AdvanceStopMarketOrderFailureMsg, sellTab, orderSize), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.AdvanceStopMarketOrderFailureMsg, sellTab, orderSize), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }

        [Fact]
        public void TC17_VerifyTrailingStopMarketBuyOrder()
        {
            try
            {
                string type;
                string feeValue;
                string successMsg;
                string askPrice;
                string trailingPrice;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("OrderType");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                orderSize = TestData.GetData("TC17_BuyOrderSize");
                limitPrice = TestData.GetData("TC17_LimitPrice");
                timeInForce = TestData.GetData("TC17_TimeInForce");
                trailingAmount = TestData.GetData("TC17_TrailingAmount");
                pegPriceDropdown = TestData.GetData("TC17_PegPrice");
                feeComponent = TestData.GetData("FeeComponent");
                orderTypeDropdown = TestData.GetData("TrailingStopMarket");

                type = Const.TrailingStopMarket;
                feeValue = GenericUtils.FeeAmount(orderSize, feeComponent);

                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);

                // Creating Buy and Sell Order to set the last price
                TestProgressLogger.StartTest();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, limitPrice));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, orderSize, limitPrice, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, limitPrice));

                // Place sell order to set up market
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, orderSize, limitPrice, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, sellTab, orderSize, limitPrice));

                // Cancel all previous orders of the User  
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER9);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CancelOrders));
                // Navigate to Advance orders section and place Trailing Stop Market BuyOrder
                UserCommonFunctions.AdvanceOrder(driver);
                advanceOrder.SelectBuyOrSellTab(buyTab);
                advanceOrder.SelectInstrumentsAndOrderType(instrument, orderTypeDropdown);
                var placeTrailingStopMarketBuyOrder = advanceOrder.PlaceTrailingStopMarketBuyOrder(driver, orderSize, trailingAmount, pegPriceDropdown);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                // Verify success message
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);
                // Get Trailing Price in case of Buy Order 
                trailingPrice = UserCommonFunctions.GetBuyOrderTrailingPrice(limitPrice, trailingAmount);
                // Verify the order is present in Open orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, buyTab, type, Double.Parse(orderSize), trailingPrice, placeTrailingStopMarketBuyOrder["PlaceOrderTime"], placeTrailingStopMarketBuyOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvanceTrailingStopMarketOrderSuccessMsg, buyTab, orderSize));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.AdvanceTrailingStopMarketOrderFailureMsg, buyTab, orderSize), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.AdvanceTrailingStopMarketOrderFailureMsg, buyTab, orderSize), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }

        [Fact]
        public void TC18_VerifyTrailingStopMarketSellOrder()
        {
            try
            {
                string type;
                string successMsg;
                string askPrice;
                string trailingPrice;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("OrderType");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                orderSize = TestData.GetData("TC18_BuyOrderSize");
                limitPrice = TestData.GetData("TC18_LimitPrice");
                timeInForce = TestData.GetData("TC18_TimeInForce");
                trailingAmount = TestData.GetData("TC18_TrailingAmount");
                pegPriceDropdown = TestData.GetData("TC18_PegPrice");
                orderTypeDropdown = TestData.GetData("TrailingStopMarket");

                type = Const.TrailingStopMarket;

                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                AdvancedOrderPage advanceOrder = new AdvancedOrderPage(TestProgressLogger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);

                // Creating Buy and Sell Order to set the last price
                TestProgressLogger.StartTest();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, limitPrice));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, orderSize, limitPrice, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, limitPrice));
                // Place Limit Buy Order to set the market
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, orderSize, limitPrice, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, buyTab, orderSize, limitPrice));

                // Cancel all previous orders of the User  
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER9);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CancelOrders));
                // Navigate to Advance orders section and place Trailing Stop Market SellOrder
                UserCommonFunctions.AdvanceOrder(driver);
                advanceOrder.SelectBuyOrSellTab(sellTab);
                advanceOrder.SelectInstrumentsAndOrderType(instrument, orderTypeDropdown);
                var placeTrailingStopMarketSellOrder = advanceOrder.PlaceTrailingStopMarketSellOrder(orderSize, trailingAmount, pegPriceDropdown);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                // Verify success message
                Assert.Equal(Const.OrderSuccessMsg, successMsg);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, TestProgressLogger);

                // Get Trailing Price in case of Sell Order 
                trailingPrice = UserCommonFunctions.GetSellOrderTrailingPrice(limitPrice, trailingAmount);
                // Verify the order is present in Open orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, sellTab, type, Double.Parse(orderSize), trailingPrice, placeTrailingStopMarketSellOrder["PlaceOrderTime"], placeTrailingStopMarketSellOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdvanceTrailingStopMarketOrderSuccessMsg, sellTab, orderSize));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.AdvanceTrailingStopMarketOrderFailureMsg, sellTab, orderSize), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.AdvanceTrailingStopMarketOrderFailureMsg, sellTab, orderSize), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }
    }
}
