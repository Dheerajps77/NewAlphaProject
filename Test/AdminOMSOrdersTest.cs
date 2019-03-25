using System;
using System.Collections.Generic;
using System.Text;
using AlphaPoint_QA.Common;
using AlphaPoint_QA.pages;
using AlphaPoint_QA.Pages;
using AlphaPoint_QA.Utils;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;

namespace AlphaPoint_QA.Test
{
    [Collection("Alphapoint_QA_USER")]
    public class AdminOMSOrdersTest : TestBase
    {
        private string instrument;
        private string buyTab;
        private string sellTab;
        private string buyOrderSize;
        private string sellOrderSize;
        private string buyLimitPrice;
        private string sellLimitPrice;
        private string timeInForce;
        private string numOfOrders;
        private string orderState;

        public AdminOMSOrdersTest(ITestOutputHelper output) : base(output)
        {

        }

        [Fact]
        public void TCAdmin13_OMSOpenOrders()
        { 
            try
            {
                instrument = TestData.GetData("Instrument");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                buyOrderSize = TestData.GetData("TCAdmin13_BuyOrderSize");
                sellOrderSize = TestData.GetData("TCAdmin13_SellOrderSize");
                buyLimitPrice = TestData.GetData("TCAdmin13_BuyLimitPrice");
                sellLimitPrice = TestData.GetData("TCAdmin13_SellLimitPrice");
                timeInForce = TestData.GetData("TCAdmin13_TimeInForce");
                numOfOrders = TestData.GetData("TCAdmin13_NumberOfOrdersToDisplay");

                AdminFunctions objAdminFunctions = new AdminFunctions(TestProgressLogger);
                AdminOMSOrdersPage adminOMSOrdersPage = new AdminOMSOrdersPage(TestProgressLogger);
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(TestProgressLogger);
                Dictionary<string, string> userDetailsDict = new Dictionary<string, string>();

                TestProgressLogger.StartTest();
                //userFunctions.LogIn(TestProgressLogger, Const.User8);
                // Create Sell and Buy limit orders such that the order are present in open orders tab
                //userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, limitPrice, timeInForce);
                //userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, limitPrice, timeInForce);
                //Login as admin 
                objAdminFunctions.AdminLogIn(TestProgressLogger);
                userDetailsDict = objAdminCommonFunctions.GetUserDetails("User_3");
                // User will select the instrument from the dropdown and verify the orders placed above are present OMS Open orders page or not
                Assert.True(adminOMSOrdersPage.VerifySelectOMSOpenOrdersInstrument(userDetailsDict, "BTCUSD", buyLimitPrice, sellLimitPrice, buyOrderSize, sellOrderSize, Const.Limit), LogMessage.VerifySelectOMSOrdersInstrumentFailed);
                // This will verify that numbers of orders displayed is not more than the number of orders selected
                Assert.True(adminOMSOrdersPage.VerifyNumOfOrdersDisplayed(numOfOrders, instrument), LogMessage.VerifyNumOfOrdersDisplayedFailed);
                // This will verify the the search functionality based on Account Id is working
                Assert.True(adminOMSOrdersPage.VerifySearchOMSOrdersByAcountId(instrument, userDetailsDict["AccountId"]), LogMessage.VerifySearchOMSOrdersByAcountIdFailed);
                // This will verify the the search functionality based on User Id is working
                Assert.True(adminOMSOrdersPage.VerifySearchOMSOrdersByUserId(instrument, userDetailsDict["UserId"]), LogMessage.VerifySearchOMSOrdersByUserIdFailed);
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(LogMessage.VerifyOMSOpenOrdersTestFailed, ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(LogMessage.VerifyOMSOpenOrdersTestFailed, e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }

        [Fact]
        public void TCAdmin14_OMSOrdersHistory()
        {
            try
            {
                instrument = TestData.GetData("Instrument");
                buyTab = TestData.GetData("BuyTab");
                sellTab = TestData.GetData("SellTab");
                buyOrderSize = TestData.GetData("TCAdmin14_BuyOrderSize");
                //sellOrderSize = TestData.GetData("TCAdmin13_SellOrderSize");
                buyLimitPrice = TestData.GetData("TCAdmin14_BuyLimitPrice");
                //sellLimitPrice = TestData.GetData("TCAdmin13_SellLimitPrice");
                timeInForce = TestData.GetData("TCAdmin14_TimeInForce");
                numOfOrders = TestData.GetData("TCAdmin14_NumberOfOrdersToDisplay");
                orderState = TestData.GetData("TCAdmin14_OrderState");

                AdminFunctions objAdminFunctions = new AdminFunctions(TestProgressLogger);
                AdminOMSOrdersPage adminOMSOrdersPage = new AdminOMSOrdersPage(TestProgressLogger);
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(TestProgressLogger);
                Dictionary<string, string> userDetailsDict = new Dictionary<string, string>();

                TestProgressLogger.StartTest();
                //userFunctions.LogIn(TestProgressLogger, Const.User8);
                // Create Buy limit order such that the order are present in open orders tab
                //userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, limitPrice, timeInForce);
                //Login as admin 
                objAdminFunctions.AdminLogIn(TestProgressLogger);
                //userDetailsDict = objAdminCommonFunctions.GetUserDetails("User_3");
                // User will select the instrument from the dropdown and verify the orders placed above are present OMS orders history page or not
                //Assert.True(adminOMSOrdersPage.VerifySelectOMSOrdersHistoryInstrument("BTCUSD", numOfOrders), LogMessage.VerifySelectOMSOrdersInstrumentFailed);
                // This will verify that numbers of orders displayed is not more than the number of orders selected
                //Assert.True(adminOMSOrdersPage.VerifyNumOfOrdersOnHistorypage(numOfOrders, instrument), LogMessage.VerifyNumOfOrdersDisplayedFailed);
                // This will verify the the search functionality based on Account Id is working
                //Assert.True(adminOMSOrdersPage.VerifySearchOMSOrdersHistoryByAcountId(instrument, numOfOrders, userDetailsDict["AccountId"]), LogMessage.VerifySearchOMSOrdersByAcountIdFailed);
                // This will verify the the search functionality based on Order Id is working
                //Assert.True(adminOMSOrdersPage.VerifySearchOMSOrdersHistoryByOrderId(instrument, numOfOrders), LogMessage.VerifySearchOMSOrdersByUserIdFailed);
                Assert.True(adminOMSOrdersPage.VerifySearchRejectedOrder(instrument, numOfOrders, orderState));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(LogMessage.VerifyOMSOpenOrdersTestFailed, ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(LogMessage.VerifyOMSOpenOrdersTestFailed, e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }
    }
}
