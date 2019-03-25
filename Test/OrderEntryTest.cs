using AlphaPoint_QA.Common;
using AlphaPoint_QA.pages;
using AlphaPoint_QA.Pages;
using AlphaPoint_QA.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;
using Xunit.Abstractions;

namespace AlphaPoint_QA.Test
{

    [Collection("Alphapoint_QA_USER")]
    public class OrderEntryTest:TestBase
    {
        private string instrument;
        private string orderType;
        private string menuTab;
        private string buyTab;
        private string sellTab;
        private string orderSize;
        private string limitPrice;
        private string timeInForce;
        private string marketOrderBuyAmount;
        private string marketOrderSellAmount;
        private string feeComponent;
        private string stopPrice;
        private string sellStopPrice;
        private string sellOrderSize;
        private string buyOrderSize;
        private string incSellOrderSize;
        private string decSellOrderSize;
        private string incBuyOrderSize;
        private string decBuyOrderSize;
        private string orderBook;
        private string openOrders;
        private string filledOrders;
        private string InactiveOrders;
        private string tradeReports;
        private string depositStatus;
        private string withdrawStatus;
        private string pairValue;
        private string sideValue;
        private string typeValue;
        private string sizeValue;
        private string priceValue;
        private string dateTimeValue;
        private string statusValue;
        private string actionValue;
        private string actionsValue;
        private string idValue;
        private string totalValue;
        private string feeValue;
        private string executionValue;
        private string productValue;
        private string amountValue;
        private string createdValue;
        private string priceChartValue;
        private string availableBalanceValue;
        private string holdValue;
        private string pendingDepositsValue;
        private string totalBalanceValue;
        private string recentTradesPriceValue;
        private string recentTradesQtyValue;
        private string recentTradesTimeValue;
        private string orderBookPriceValue;
        private string orderBookQtyValue;
        private string orderBookMySizeValue;

        private string orderEntryMarketValue;
        private string orderEntryLimitValue;
        private string orderEntryStopValue;

        public OrderEntryTest(ITestOutputHelper output):base(output)
        {           
        }

        [Fact]
        public void TC1_VerifyDetailsOnLandingPageTest()
        {
            instrument = TestData.GetData("Instrument");
            menuTab = TestData.GetData("MenuTab");
            buyTab = TestData.GetData("BuyTab");
            sellTab = TestData.GetData("SellTab");
            orderBook = TestData.GetData("OrderBookValue");
            openOrders = TestData.GetData("OpenOrdersValue");
            filledOrders = TestData.GetData("FilledOrdersValue");
            InactiveOrders = TestData.GetData("InactiveOrdersValue");
            tradeReports = TestData.GetData("TradeReportsValue");
            depositStatus = TestData.GetData("DepositStatusValue");
            withdrawStatus = TestData.GetData("WithdrawStatusValue");
            pairValue = TestData.GetData("TC1_PairValue");
            sideValue = TestData.GetData("TC1_SideValue");
            typeValue = TestData.GetData("TC1_TypeValue");
            sizeValue = TestData.GetData("TC1_SizeValue");
            priceValue = TestData.GetData("TC1_PriceValue");
            dateTimeValue = TestData.GetData("TC1_DateTimeValue");
            statusValue = TestData.GetData("TC1_StatusValue");
            actionValue = TestData.GetData("TC1_ActionValue");
            actionsValue = TestData.GetData("TC1_ActionsValue");
            idValue = TestData.GetData("TC1_IDValue");
            totalValue = TestData.GetData("TC1_TotalValue");
            feeValue = TestData.GetData("TC1_FeeValue");
            executionValue = TestData.GetData("TC1_ExecutionIDValue");
            productValue = TestData.GetData("TC1_ProductValue");
            amountValue = TestData.GetData("TC1_AmountValue");
            createdValue = TestData.GetData("TC1_CreatedValue");
            priceChartValue = TestData.GetData("TC1_PriceChartValue");
            availableBalanceValue = TestData.GetData("TC1_AvailableBalanceValue");
            holdValue = TestData.GetData("TC1_HoldValue");
            pendingDepositsValue = TestData.GetData("TC1_PendingDepositsValue");
            totalBalanceValue = TestData.GetData("TC1_TotalBalanceValue");
            recentTradesPriceValue = TestData.GetData("TC1_RecentTradesPriceValue");
            recentTradesQtyValue = TestData.GetData("TC1_RecentTradesQtyValue");
            recentTradesTimeValue = TestData.GetData("TC1_RecentTradesTimeValue");
            orderBookPriceValue = TestData.GetData("TC1_OrderBookPriceValue");
            orderBookQtyValue = TestData.GetData("TC1_OrderBookQtyValue");
            orderBookMySizeValue = TestData.GetData("TC1_OrderBookMySizeValue");
            orderEntryMarketValue = TestData.GetData("TC1_OrderEntryMarketValue");
            orderEntryLimitValue = TestData.GetData("TC1_OrderEntryLimitValue");
            orderEntryStopValue = TestData.GetData("TC1_OrderEntryStopValue");

            DetailsOnLandingPage objDetailsOnLandingPage = new DetailsOnLandingPage(TestProgressLogger);
            try
            {
                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);

                // Login in user portal
                userFunctions.LogIn(TestProgressLogger, Const.USER14);

                // Click on "Dashboard" menu button --> select an exchange
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);

                // Verify "Exchange" menu button
                Assert.True(objDetailsOnLandingPage.ExchangeLinkButton());

               // Select an instrument BTCUSD
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);

                // Verify the sub labels present in Open orders tab
                Assert.True(objDetailsOnLandingPage.VerifyOpenOrdersTab(openOrders, pairValue, sideValue, typeValue, sizeValue, priceValue, dateTimeValue, statusValue, actionValue));

                // Verify the sub labels present in filled orders tab
                Assert.True(objDetailsOnLandingPage.VerifyFilledOrdersTab(filledOrders, idValue, pairValue, sideValue, sizeValue, priceValue, totalValue, feeValue, executionValue, dateTimeValue));

                // Verify the sub labels present in Inactive orders tab
                Assert.True(objDetailsOnLandingPage.VerifyInactiveOrdersTab(filledOrders, pairValue, sideValue, typeValue, sizeValue, priceValue, dateTimeValue, statusValue));

                // Verify the sub labels present in trade reports tab
                Assert.True(objDetailsOnLandingPage.VerifyTradeReportTab(tradeReports, pairValue, sideValue, sizeValue, priceValue, feeValue, dateTimeValue, statusValue));

                // Verify the sub labels present in deposit status tab
                Assert.True(objDetailsOnLandingPage.VerifyDepositStatusTab(depositStatus, productValue, amountValue, statusValue, createdValue, feeValue));

                // Verify the sub labels present in withdraw status tab
                Assert.True(objDetailsOnLandingPage.VerifyWithdrawStatusTab(withdrawStatus, productValue, amountValue, statusValue, createdValue, feeValue, actionsValue));

                // Verify price chart label name is present
                Assert.True(objDetailsOnLandingPage.PriceChartTxt(priceChartValue));

                // Verify order entry With buy option label name is present
                Assert.True(objDetailsOnLandingPage.OrderEntryWithBuyOption());

                // Verify order entry With sell option label name is present
                Assert.True(objDetailsOnLandingPage.OrderEntryWithSellOption());

                // Click on "Balances" button
                objDetailsOnLandingPage.ClickOnBalancesButton();

                // Verify available balance label name is present
                Assert.True(objDetailsOnLandingPage.AvailableBalanceTxt(availableBalanceValue));

                // Verify available hold label name is present
                Assert.True(objDetailsOnLandingPage.HoldTxt(holdValue));

                // Verify pending deposits label name is present
                Assert.True(objDetailsOnLandingPage.PendingDepositsTxt(pendingDepositsValue));

                // Verify total balance label name is present
                Assert.True(objDetailsOnLandingPage.TotalBalanceTxt(totalBalanceValue));

                // Verify the sub labels present in order book menu tab
                Assert.True(objDetailsOnLandingPage.VerifyOrderBookMenuTab(orderBookPriceValue, orderBookQtyValue, orderBookMySizeValue));

                // Verify the sub labels present in recent trades menu tab
                Assert.True(objDetailsOnLandingPage.VerifyRecentTradesMenuTab(recentTradesPriceValue, recentTradesQtyValue, recentTradesTimeValue));

                //Click on "Order Entry" button
                objDetailsOnLandingPage.OrderEntryBtn();
                Thread.Sleep(2000);

                // Verify the sub labels present in buy order entry menu tab
                Assert.True(objDetailsOnLandingPage.VerifyBuyOrderEntryMenuAndSubMenuTab(buyTab, orderEntryMarketValue, orderEntryLimitValue, orderEntryStopValue));

                // Verify the sub labels present in sell order entry menu tab
                Assert.True(objDetailsOnLandingPage.VerifySellOrderEntryMenuAndSubMenuTab(sellTab, orderEntryMarketValue, orderEntryLimitValue, orderEntryStopValue));

                // Verify various fields in order book section
                Assert.True(objDetailsOnLandingPage.FieldsInOrderBookSection());

                // Verify advance button is present
                Assert.True(objDetailsOnLandingPage.AdvanceOrderBtn());

                // Verify order entry button is present
                Assert.True(objDetailsOnLandingPage.OrderEntryButn());

                // Verify balance button button is present
                Assert.True(objDetailsOnLandingPage.VerifyBalancesButton());

                // verify various option in the price chart
                Assert.True(objDetailsOnLandingPage.VariouOptionInPriceChart());
                TestProgressLogger.LogCheckPoint(LogMessage.VerifiedDetailInLandingPagePassed);
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifiedDetailInLandingPageFailed), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifiedDetailInLandingPageFailed), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }

        [Fact] 
        public void TC2_VerifyAmountPersistence()
        {
            try
            {
                instrument = TestData.GetData("Instrument");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);         
                OrderEntryPage orderEntryPage = new OrderEntryPage(driver, TestProgressLogger);
                userFunctions.LogIn(TestProgressLogger, Const.USER5);
                // Verify that the Amount entered in the Order Size field gets persisted
                Assert.True(orderEntryPage.VerifyOrderEntryAmountPersistence(instrument, TestData.GetData("PersistenceTestAmount")), LogMessage.DataNotBeingPersisted);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AmountPersistenceSuccessMsg, buyTab, sellTab));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(LogMessage.AmountPersistenceFailureMsg, ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(LogMessage.AmountPersistenceFailureMsg, e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }

        [Fact]
        public void TC3_VerifyBuyMarketOrder()
        {
            try
            {
                instrument = TestData.GetData("Instrument");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                marketOrderBuyAmount = TestData.GetData("TC3_MarketOrderBuyAmount");
                feeComponent = TestData.GetData("FeeComponent");
                sellOrderSize = TestData.GetData("TC3_SellOrderSize");
                limitPrice = TestData.GetData("TC3_LimitPrice");
                timeInForce = TestData.GetData("TC3_TimeInForce");
                string feeValue;
                Dictionary<string, string> placeMarketBuyOrder;

                // Get fee based on buy amount and fee component
                feeValue = GenericUtils.FeeAmount(marketOrderBuyAmount, feeComponent);
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
                OrderEntryPage orderEntryPage = new OrderEntryPage(driver, TestProgressLogger);

                TestProgressLogger.StartTest();
                // Place buy and sell order to set the last price
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, limitPrice));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, sellOrderSize, limitPrice, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, limitPrice));

                // Place Limit sell order to set the market
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, limitPrice, timeInForce);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, sellTab, sellOrderSize, limitPrice));

                userFunctions.LogIn(TestProgressLogger, Const.USER11);
                // Place Market buy order with the same quantity as that of sell limit order placed
                placeMarketBuyOrder = orderEntryPage.PlaceMarketBuyOrder(instrument, buyTab, Double.Parse(marketOrderBuyAmount));
                // Verify order is present in Filled Orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(placeMarketBuyOrder["Instrument"], placeMarketBuyOrder["Side"], Double.Parse(placeMarketBuyOrder["BuyAmount"]), feeValue, placeMarketBuyOrder["PlaceOrderTime"], placeMarketBuyOrder["PlaceOrderTimePlusOneMin"]), Const.MarketOrderVerifiedInFilledOrders);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketOrderTestPassed, buyTab));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.MarketOrderTestFailed, buyTab), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.MarketOrderTestFailed, buyTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }

        [Fact]
        public void TC4_VerifySellMarketOrder()
        {
            try
            {
                instrument = TestData.GetData("Instrument");
                marketOrderSellAmount = TestData.GetData("TC4_MarketOrderSellAmount");
                feeComponent = TestData.GetData("FeeComponent");
                orderType = TestData.GetData("OrderType");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                buyOrderSize = TestData.GetData("TC4_MarketOrderBuyAmount");
                limitPrice = TestData.GetData("TC4_LimitPrice");
                timeInForce = TestData.GetData("TimeInForce");
                string feeValue;
                Dictionary<string, string> placeMarketSellOrder;
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                OrderEntryPage orderEntryPage = new OrderEntryPage(driver, TestProgressLogger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);

                // Get fee for sell order based on buy amount, limit price and fee component
                feeValue = GenericUtils.SellFeeAmount(buyOrderSize, limitPrice, feeComponent);

                TestProgressLogger.StartTest();
                // Place buy and sell order to set the last price
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, limitPrice));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, limitPrice, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, limitPrice));

                // Place Limit Buy order to set the market
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, limitPrice, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, buyTab, buyOrderSize, limitPrice));

                userFunctions.LogIn(TestProgressLogger, Const.USER11);
                // Place Market Sell order with the same quantity as that of buy limit order placed
                placeMarketSellOrder = orderEntryPage.PlaceMarketSellOrder(instrument, sellTab, Double.Parse(marketOrderSellAmount), Double.Parse(feeComponent));               
                // Verify order is present in Filled Orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(placeMarketSellOrder["Instrument"], placeMarketSellOrder["Side"], Double.Parse(placeMarketSellOrder["SellAmount"]), feeValue, placeMarketSellOrder["PlaceOrderTime"], placeMarketSellOrder["PlaceOrderTimePlusOneMin"]), Const.MarketOrderVerifiedInFilledOrders);
                TestProgressLogger.LogCheckPoint(string.Format(LogMessage.SellMarketOrderSuccessMsg));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.MarketOrderTestFailed, sellTab), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.MarketOrderTestFailed, sellTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }

        [Fact] 
        public void TC5_VerifyBuyLimitOrder()
        {
            try
            {
                string type;
                string buyOrderFeeValue;
                string sellOrderFeeValue;
                Dictionary<string, string> placeLimitBuyOrder;
                Dictionary<string, string> placeLimitSellOrder;
                string orderSizeDifference;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("OrderType");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                buyOrderSize = TestData.GetData("TC5_BuyOrderSize");
                sellOrderSize = TestData.GetData("TC5_SellOrderSize");
                incSellOrderSize = TestData.GetData("TC5_IncreasedSellOrderSize");
                decSellOrderSize = TestData.GetData("TC5_DecreasedSellOrderSize");
                limitPrice = TestData.GetData("TC5_LimitPrice");
                timeInForce = TestData.GetData("TC5_TimeInForce");
                feeComponent = TestData.GetData("FeeComponent");
                type = Const.Limit;
                // Get fee for buy order based on buy amount and fee component
                buyOrderFeeValue = GenericUtils.FeeAmount(buyOrderSize, feeComponent);
                // Get fee for sell order based on buy amount, limit price and fee component
                sellOrderFeeValue = GenericUtils.SellFeeAmount(sellOrderSize, limitPrice, feeComponent);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                OrderEntryPage orderEntryPage = new OrderEntryPage(driver, TestProgressLogger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);

                // Place buy and sell order to set the last price
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, limitPrice));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, limitPrice, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, limitPrice));
                
                // Scenario 1: Sell order S1 with same price is available and S1 quantity is = B1.
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                // Place Buy order to set the market
                placeLimitBuyOrder = orderEntryPage.PlaceLimitBuyOrder(instrument, buyTab, buyOrderSize, limitPrice, timeInForce);
                // Verify that the order is present in the Open Orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, buyTab, type, Double.Parse(buyOrderSize), limitPrice, placeLimitBuyOrder["PlaceOrderTime"], placeLimitBuyOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(string.Format(LogMessage.LimitOrderSuccessMsg, buyTab, buyOrderSize, limitPrice));
               
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                // Place Limit sell order with the same price and quantity as Buy order to verify the order gets filled
                placeLimitSellOrder = orderEntryPage.PlaceLimitSellOrder(instrument, sellTab, sellOrderSize, limitPrice, timeInForce);
                // Verify sell order is present in Filled Orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, sellTab, Double.Parse(sellOrderSize), sellOrderFeeValue, placeLimitSellOrder["PlaceOrderTime"], placeLimitSellOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(string.Format(LogMessage.LimitOrderSuccessMsg, sellTab, sellOrderSize, limitPrice));
                
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                orderEntryPage.NavigateToHomePage(instrument);
                // Verify buy order is present in Filled Orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, buyTab, Double.Parse(buyOrderSize), buyOrderFeeValue, placeLimitSellOrder["PlaceOrderTime"], placeLimitSellOrder["PlaceOrderTimePlusOneMin"]));
               
                // Scenario 2: Sell order S1 with same price is available and S1 quantity is > B1.
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                // Place Buy order to set the market
                Dictionary<string, string> placeLimitBuyOrderS2 = orderEntryPage.PlaceLimitBuyOrder(instrument, buyTab, buyOrderSize, limitPrice, timeInForce);
                // Verify that the order is present in the Open Orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, buyTab, type, Double.Parse(buyOrderSize), limitPrice, placeLimitBuyOrderS2["PlaceOrderTime"], placeLimitBuyOrderS2["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(string.Format(LogMessage.LimitOrderSuccessMsg, buyTab, buyOrderSize, limitPrice));
             
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                // Place Limit sell order with the same price and quantity > Buy order to verify the order gets filled
                Dictionary<string, string> placeLimitSellOrderS2 = orderEntryPage.PlaceLimitSellOrder(instrument, sellTab, incSellOrderSize, limitPrice, timeInForce);
                // Verify sell order is present in Filled Orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, sellTab, Double.Parse(buyOrderSize), sellOrderFeeValue, placeLimitSellOrderS2["PlaceOrderTime"], placeLimitSellOrderS2["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(string.Format(LogMessage.LimitOrderSuccessMsg, sellTab, buyOrderSize, limitPrice));
              
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                orderEntryPage.NavigateToHomePage(instrument);
                // Verify buy order is present in Filled Orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, buyTab, Double.Parse(buyOrderSize), buyOrderFeeValue, placeLimitSellOrderS2["PlaceOrderTime"], placeLimitSellOrderS2["PlaceOrderTimePlusOneMin"]));
                
                // Scenario 3: Sell order S1 with same price is available and S1 quantity is < B1.
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER9);              
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                // Place Buy order to set the market
                Dictionary<string, string> placeLimitBuyOrderS3 = orderEntryPage.PlaceLimitBuyOrder(instrument, buyTab, buyOrderSize, limitPrice, timeInForce);
                // Verify that the order is present in the Open Orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, buyTab, type, Double.Parse(buyOrderSize), limitPrice, placeLimitBuyOrderS3["PlaceOrderTime"], placeLimitBuyOrderS3["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(string.Format(LogMessage.LimitOrderSuccessMsg, buyTab, buyOrderSize, limitPrice));
               
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                sellOrderFeeValue = GenericUtils.SellFeeAmount(decSellOrderSize, limitPrice, feeComponent);
                // Place Limit sell order with the same price and quantity < Buy order to verify the order gets filled
                Dictionary<string, string> placeLimitSellOrderS3 = orderEntryPage.PlaceLimitSellOrder(instrument, sellTab, decSellOrderSize, limitPrice, timeInForce);
                // Verify order is present in Filled Orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, sellTab, Double.Parse(decSellOrderSize), sellOrderFeeValue, placeLimitSellOrderS3["PlaceOrderTime"], placeLimitSellOrderS3["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(string.Format(LogMessage.LimitOrderSuccessMsg, sellTab, decSellOrderSize, limitPrice));
                
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                orderEntryPage.NavigateToHomePage(instrument);
                // This will get the fee value based on decreased order size
                buyOrderFeeValue = GenericUtils.FeeAmount(decSellOrderSize, feeComponent);
                // Get the difference between the buy and sell order sizes
                orderSizeDifference = GenericUtils.GetDifferenceFromStringAfterSubstraction(buyOrderSize, decSellOrderSize);
                // Verify that the order is present in the Open Orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, buyTab, type, Double.Parse(orderSizeDifference), limitPrice, placeLimitSellOrderS3["PlaceOrderTime"], placeLimitSellOrderS3["PlaceOrderTimePlusOneMin"]));
                // Verify order is present in Filled Orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, buyTab, Double.Parse(decSellOrderSize), buyOrderFeeValue, placeLimitSellOrderS3["PlaceOrderTime"], placeLimitSellOrderS3["PlaceOrderTimePlusOneMin"]));
                
                // This step cancels the remaining order and verifies the same in Open orders tab
                UserCommonFunctions.CancelOrderBookBuyOrder(driver);
                UserCommonFunctions.OpenOrderTab(driver);
                // Verify that the order is not present in the Open Orders tab
                Assert.False(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, buyTab, type, Double.Parse(orderSizeDifference), limitPrice, placeLimitSellOrderS3["PlaceOrderTime"], placeLimitSellOrderS3["PlaceOrderTimePlusOneMin"]));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.LimitOrderTestFailed, buyTab), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.LimitOrderTestFailed, buyTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }

        [Fact] 
        public void TC6_VerifySellLimitOrder()
        {
            try
            {
                string type;
                string buyOrderFeeValue;
                string sellOrderFeeValue;
                string orderSizeDifference;
                instrument = TestData.GetData("Instrument");
                orderType = TestData.GetData("OrderType");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                buyOrderSize = TestData.GetData("TC6_BuyOrderSize");
                sellOrderSize = TestData.GetData("TC6_SellOrderSize");
                incBuyOrderSize = TestData.GetData("TC6_IncreasedBuyOrderSize");
                decBuyOrderSize = TestData.GetData("TC6_DecreasedBuyOrderSize");
                limitPrice = TestData.GetData("TC6_LimitPrice");
                timeInForce = TestData.GetData("TC6_TimeInForce");
                feeComponent = TestData.GetData("FeeComponent");

                type = Const.Limit;
                buyOrderFeeValue = GenericUtils.FeeAmount(buyOrderSize, feeComponent);
                sellOrderFeeValue = GenericUtils.SellFeeAmount(sellOrderSize, limitPrice, feeComponent);

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                OrderEntryPage orderEntryPage = new OrderEntryPage(driver, TestProgressLogger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);

                // Place buy and sell order to set the last price
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, limitPrice));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, limitPrice, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, limitPrice));

                // Scenario 1: Buy order B1 with same price is available and B1 quantity is = S1.
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                // Place Limit sell order to set the market
                Dictionary<string, string> placeLimitSellOrder = orderEntryPage.PlaceLimitSellOrder(instrument, sellTab, sellOrderSize, limitPrice, timeInForce);
                // Verify that the order is present in the Open Orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, sellTab, type, Double.Parse(sellOrderSize), limitPrice, placeLimitSellOrder["PlaceOrderTime"], placeLimitSellOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(string.Format(LogMessage.LimitOrderSuccessMsg, sellTab, sellOrderSize, limitPrice));

                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                // Place Limit buy order with the same price and quantity as Sell order to verify the order gets filled
                Dictionary<string, string> placeLimitBuyOrder = orderEntryPage.PlaceLimitBuyOrder(instrument, buyTab, buyOrderSize, limitPrice, timeInForce);
                // Verify order is present in Filled Orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, buyTab, Double.Parse(buyOrderSize), buyOrderFeeValue, placeLimitBuyOrder["PlaceOrderTime"], placeLimitBuyOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(string.Format(LogMessage.LimitOrderSuccessMsg, buyTab, buyOrderSize, limitPrice));

                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                orderEntryPage.NavigateToHomePage(instrument);
                // Verify order is present in Filled Orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, sellTab, Double.Parse(sellOrderSize), sellOrderFeeValue, placeLimitSellOrder["PlaceOrderTime"], placeLimitSellOrder["PlaceOrderTimePlusOneMin"]));
                
                // Scenario 2: Buy order B1 with same price is available and B1 quantity is > S1.
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                // Place Limit sell order to set the market
                Dictionary<string, string> placeLimitSellOrderS2 = orderEntryPage.PlaceLimitSellOrder(instrument, sellTab, sellOrderSize, limitPrice, timeInForce);
                // Verify that the order is present in the Open Orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, sellTab, type, Double.Parse(sellOrderSize), limitPrice, placeLimitSellOrderS2["PlaceOrderTime"], placeLimitSellOrderS2["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(string.Format(LogMessage.LimitOrderSuccessMsg, sellTab, sellOrderSize, limitPrice));
                
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                // Place Limit buy order with the same price and quantity > Sell order to verify the order gets filled
                Dictionary<string, string> placeLimitBuyOrderS2 = orderEntryPage.PlaceLimitBuyOrder(instrument, buyTab, incBuyOrderSize, limitPrice, timeInForce);
                // Verify order is present in Filled Orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, buyTab, Double.Parse(sellOrderSize), buyOrderFeeValue, placeLimitSellOrderS2["PlaceOrderTime"], placeLimitSellOrderS2["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(string.Format(LogMessage.LimitOrderSuccessMsg, buyTab, incBuyOrderSize, limitPrice));
                
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                orderEntryPage.NavigateToHomePage(instrument);
                // Verify order is present in Filled Orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, sellTab, Double.Parse(sellOrderSize), sellOrderFeeValue, placeLimitSellOrderS2["PlaceOrderTime"], placeLimitSellOrderS2["PlaceOrderTimePlusOneMin"]));
               

                // Scenario 3: Buy order B1 with same price is available and B1 quantity is < S1.
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER9);
                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                // Place Limit sell order to set the market
                Dictionary<string, string> placeLimitSellOrderS3 = orderEntryPage.PlaceLimitSellOrder(instrument, sellTab, sellOrderSize, limitPrice, timeInForce);
                // Verify that the order is present in the Open Orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, sellTab, type, Double.Parse(sellOrderSize), limitPrice, placeLimitSellOrderS3["PlaceOrderTime"], placeLimitSellOrderS3["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(string.Format(LogMessage.LimitOrderSuccessMsg, sellTab, sellOrderSize, limitPrice));
                
                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                buyOrderFeeValue = GenericUtils.FeeAmount(decBuyOrderSize, feeComponent);
                // Place Limit buy order with the same price and quantity < Sell order to verify the order gets filled
                Dictionary<string, string> placeLimitBuyOrderS3 = orderEntryPage.PlaceLimitBuyOrder(instrument, buyTab, decBuyOrderSize, limitPrice, timeInForce);
                // Verify order is present in Filled Orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, buyTab, Double.Parse(decBuyOrderSize), buyOrderFeeValue, placeLimitSellOrderS3["PlaceOrderTime"], placeLimitSellOrderS3["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(string.Format(LogMessage.LimitOrderSuccessMsg, buyTab, decBuyOrderSize, limitPrice));

                userFunctions.LogIn(TestProgressLogger, Const.USER8);
                orderEntryPage.NavigateToHomePage(instrument);
                // This will get the sell fee value based on decreased order size
                sellOrderFeeValue = GenericUtils.SellFeeAmount(decBuyOrderSize, limitPrice, feeComponent);
                // Get the difference between the sell order size and decreased buy order size 
                orderSizeDifference = GenericUtils.GetDifferenceFromStringAfterSubstraction(sellOrderSize, decBuyOrderSize);
                // Verify that the order is present in the Open Orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, sellTab, type, Double.Parse(orderSizeDifference), limitPrice, placeLimitSellOrderS3["PlaceOrderTime"], placeLimitSellOrderS3["PlaceOrderTimePlusOneMin"]));
                // Verify order is present in Filled Orders tab
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab(instrument, sellTab, Double.Parse(decBuyOrderSize), sellOrderFeeValue, placeLimitSellOrderS3["PlaceOrderTime"], placeLimitSellOrderS3["PlaceOrderTimePlusOneMin"]));

                // This step cancels the remaining order and verifies the same in Open orders tab
                UserCommonFunctions.CancelOrderBookSellOrder(driver);
                UserCommonFunctions.OpenOrderTab(driver);
                // Verify that the order is present in the Open Orders tab
                Assert.False(objVerifyOrdersTab.VerifyOpenOrdersTab(instrument, sellTab, type, Double.Parse(orderSizeDifference), limitPrice, placeLimitSellOrderS3["PlaceOrderTime"], placeLimitSellOrderS3["PlaceOrderTimePlusOneMin"]));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.LimitOrderTestFailed, buyTab), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.LimitOrderTestFailed, buyTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }

        [Fact]
        public void TC7_VerifyBuyStopOrder()
        {
            try
            {
                string type = Const.StopMarket;
                instrument = TestData.GetData("Instrument");
                feeComponent = TestData.GetData("FeeComponent");
                orderType = TestData.GetData("OrderType");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                orderSize = TestData.GetData("TC7_OrderSize");
                limitPrice = TestData.GetData("TC7_LimitPrice");
                timeInForce = TestData.GetData("TimeInForce");
                stopPrice = TestData.GetData("TC7_StopPrice");
                Dictionary<string, string> placeStopBuyOrder;
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                OrderEntryPage orderEntryPage = new OrderEntryPage(driver, TestProgressLogger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);

                TestProgressLogger.StartTest();
                // Place buy and sell order to set the last price
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, limitPrice));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, orderSize, limitPrice, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, limitPrice));

                // Place Limit sell order to set the market
                userFunctions.LogIn(TestProgressLogger, Const.USER8);               
                string askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, orderSize, limitPrice, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, sellTab, orderSize, limitPrice));

                userFunctions.LogIn(TestProgressLogger, Const.USER9);
                // Place Stop Buy order with Stop Price > market price
                placeStopBuyOrder = orderEntryPage.PlaceStopBuyOrder(instrument, buyTab, Double.Parse(orderSize), Double.Parse(feeComponent), Double.Parse(stopPrice));
                // Verify that the order is present in the Open Orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(placeStopBuyOrder["Instrument"], placeStopBuyOrder["Side"], type, Double.Parse(placeStopBuyOrder["BuyAmount"]), placeStopBuyOrder["StopPrice"], placeStopBuyOrder["PlaceOrderTime"], placeStopBuyOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(string.Format(LogMessage.BuyStopOrderSuccessMsg, buyTab));
                
                // This cancels all the previous open orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER8);
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER9);
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.StopOrderTestFailed, buyTab), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.StopOrderTestFailed, buyTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }

        }

        [Fact]
        public void TC8_VerifySellStopOrder()
        {
            try
            {
                string type = Const.StopMarket;
                instrument = TestData.GetData("Instrument");
                feeComponent = TestData.GetData("FeeComponent");
                orderType = TestData.GetData("OrderType");
                menuTab = TestData.GetData("MenuTab");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                orderSize = TestData.GetData("TC8_OrderSize");
                limitPrice = TestData.GetData("TC8_LimitPrice");
                timeInForce = TestData.GetData("TimeInForce");
                sellStopPrice = TestData.GetData("TC8_StopPrice");
                Dictionary<string, string> placeStopSellOrder;
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                OrderEntryPage orderEntryPage = new OrderEntryPage(driver, TestProgressLogger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);

                TestProgressLogger.StartTest();
                // Place buy and sell order to set the last price
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, limitPrice));
                userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, orderSize, limitPrice, timeInForce, Const.USER10, Const.USER11);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, limitPrice));

                // Place Limit Buy order to set the market
                userFunctions.LogIn(TestProgressLogger, Const.USER11);               
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, orderSize, limitPrice, timeInForce);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.MarketSetupEnd, buyTab, orderSize, limitPrice));

                userFunctions.LogIn(TestProgressLogger, Const.USER5);
                // Place Stop Buy order with Stop Price < market price
                placeStopSellOrder = orderEntryPage.PlaceStopSellOrder(instrument, sellTab, Double.Parse(orderSize), Double.Parse(feeComponent), Double.Parse(sellStopPrice));
                
                // Verify that the order is present in the Open Orders tab
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(placeStopSellOrder["Instrument"], placeStopSellOrder["Side"], type, Double.Parse(placeStopSellOrder["SellAmount"]), placeStopSellOrder["StopPrice"], placeStopSellOrder["PlaceOrderTime"], placeStopSellOrder["PlaceOrderTimePlusOneMin"]));
                TestProgressLogger.LogCheckPoint(string.Format(LogMessage.BuyStopOrderSuccessMsg, sellTab));
                
                // This cancels all the previous open orders
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER11);
                UserCommonFunctions.LoginAndCancelAllOrders(TestProgressLogger, driver, instrument, Const.USER5);
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.StopOrderTestFailed, sellTab), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.StopOrderTestFailed, sellTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }

    }
}
