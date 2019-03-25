using AlphaPoint_QA.Common;
using AlphaPoint_QA.Pages;
using AlphaPoint_QA.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace AlphaPoint_QA.Test
{
    [Collection("Alphapoint_QA_USER")]
    public class AdminTradesTest : TestBase
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
        private string buyOrderSize;
        private string sellOrderSize;
        private string userId;
        private string tradeIdValueTextValue;
        private string productPairValueTextValue;
        private string sideValueTextValue;
        private string qantityValueTextValue;
        private string executionIdValueTextValue;
        private string accountId;

        public AdminTradesTest(ITestOutputHelper output) : base(output)
        {

        }

        //inprogress
        [Fact]
        public void TCAdmin11_VerifyTradesUnderTradesTabTest()
        {
            instrument = TestData.GetData("Instrument");
            marketOrder = TestData.GetData("MarketOrder");
            menuTab = TestData.GetData("MenuTab");
            sellTab = TestData.GetData("SellTab");
            buyTab = TestData.GetData("BuyTab");
            buyOrderSize = TestData.GetData("TC9_BuyOrderSize");
            sellOrderSize = TestData.GetData("TC9_SellOrderSize");
            limitPrice = TestData.GetData("TC9_LimitPrice");
            timeInForce = TestData.GetData("TC9_TimeInForce");

            tradeIdValueTextValue = TestData.GetData("TCAdmin11_TradeIdValueTextValue");
            productPairValueTextValue = TestData.GetData("TCAdmin11_ProductPairValueTextValue");
            sideValueTextValue = TestData.GetData("TCAdmin11_SideValueTextValue");
            qantityValueTextValue = TestData.GetData("TCAdmin11_QantityValueTextValue");
            executionIdValueTextValue = TestData.GetData("TCAdmin11_ExecutionIdValueTextValue");


            AdminFunctions objAdminFunctions = new AdminFunctions(TestProgressLogger);
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(TestProgressLogger);
            AdminUsersPage objAdminUsersPage = new AdminUsersPage(TestProgressLogger);
            UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
            UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
            AdminTradePage objAdminTradePage = new AdminTradePage(TestProgressLogger);

            try
            {
                TestProgressLogger.StartTest();

                // Create Buy and Sell Order to set the last price
                //TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketBegin, limitPrice));
                //userCommonFunction.PlaceOrdersToSetLastPrice(driver, instrument, buyTab, sellTab, buyOrderSize, limitPrice, timeInForce, Const.USER10, Const.USER11);
                //TestProgressLogger.LogCheckPoint(String.Format(LogMessage.PlaceOrderToSetMarketEnd, limitPrice));


                //Login as admin -> Click on "Trades" menu button
                objAdminFunctions.AdminLogIn(TestProgressLogger);
                objAdminCommonFunctions.SelectTradeMenu();

                //Select an instrument under trade tab
                Assert.True(objAdminTradePage.VerifyTradeOrderUnderTradesTab());
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
               // TestProgressLogger.LogError(String.Format(LogMessage.VerifyRevokeUserPermissionFailed), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                //TestProgressLogger.LogError(String.Format(LogMessage.VerifyRevokeUserPermissionFailed), e);
                throw e;
            }
            finally
            {
                objAdminCommonFunctions.UserMenuBtn();
                objAdminFunctions.AdminLogOut();
                TestProgressLogger.EndTest();
            }
        }

        [Fact]
        public void TCAdmin12_VerifyAllTradesTakenPlaceUnderTradesTabTest()
        {
            accountId = TestData.GetData("TCAdmin12_AccountIdValue");
            userId = TestData.GetData("TCAdmin12_UserIdValue");

            AdminFunctions objAdminFunctions = new AdminFunctions(TestProgressLogger);
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(TestProgressLogger);
            AdminUsersPage objAdminUsersPage = new AdminUsersPage(TestProgressLogger);
            UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
            UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
            AdminTradePage objAdminTradePage = new AdminTradePage(TestProgressLogger);

            try
            {
                //Login as admin -> Click on "Trades" menu button
                objAdminFunctions.AdminLogIn(TestProgressLogger);
                objAdminCommonFunctions.SelectTradeMenu();
                // This method with will verify the AccountId textfield
                //Assert.True(objAdminTradePage.VerifySearchByAccountId(accountId));

                // This method with will verify the UserId textfield
                Assert.True(objAdminTradePage.VerifySearchByUserId(userId));

            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifyRevokeUserPermissionFailed), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifyRevokeUserPermissionFailed), e);
                throw e;
            }
            finally
            {
                objAdminCommonFunctions.UserMenuBtn();
                objAdminFunctions.AdminLogOut();
                TestProgressLogger.EndTest();
            }
        }


    }
}
