using AlphaPoint_QA.Common;
using AlphaPoint_QA.Pages;
using AlphaPoint_QA.Utils;
using OpenQA.Selenium;
using System;
using Xunit;
using Xunit.Abstractions;

namespace AlphaPoint_QA.Test
{
    [Collection("Alphapoint_QA_USER")]
    public class BuyAndSellTest : TestBase
    {
        private string instrument;
        private string buyTab;
        private string sellTab;
        private string limitPrice;
        private string timeInForce;
        private string sellOrderSize;

        public BuyAndSellTest(ITestOutputHelper output) : base(output)
        {    
        }

        [Fact]
        public void TC42_VerifyBuyAndSell_BuyTest()
        {
            instrument = TestData.GetData("Instrument");
            sellTab = TestData.GetData("SellTab");
            buyTab = TestData.GetData("BuyTab");
            sellOrderSize = TestData.GetData("TC43_SellOrderSize");
            limitPrice = TestData.GetData("TC43_LimitPrice");
            timeInForce = TestData.GetData("TC43_TimeInForce");

            UserFunctions userfuntionality = new UserFunctions(TestProgressLogger);
            BuyAndSellPage objBuyAndSellPage = new BuyAndSellPage(TestProgressLogger);
            UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
            UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
            try
            {

                string askPrice;
                TestProgressLogger.StartTest();
                userFunctions.LogIn(TestProgressLogger, Const.USER8);

                // Setting up market by placing Limit sell Order
                askPrice = userCommonFunction.CancelAndPlaceLimitSellOrder(driver, instrument, sellTab, sellOrderSize, limitPrice, timeInForce);

                // Navigate to Buy&Sell page and place a buy order
                userfuntionality.LogIn(TestProgressLogger, Const.USER14);
                
                // Click on -> Dashboad Menu button -> Buy&Sell button
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.NavigateToBuySell(driver);

                // Verify "Make a Transaction" window
                Assert.True(objBuyAndSellPage.VerifyMakeATransaction());

                // Verify "Chart details" section
                Assert.True(objBuyAndSellPage.VerifyChart());

                // Verify "Buy" option is selected by default
                Assert.True(objBuyAndSellPage.VerifyBuyOption());

                // Verify 5th button with blank values is selected
                Assert.True(objBuyAndSellPage.VerifyFifthRadioButtonOption());
                Assert.True(objBuyAndSellPage.VerifyFifthWithBlankValues());

                // Place a buy order and verify the amount details
                objBuyAndSellPage.PlaceBuyOrder(instrument, buyTab);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BuyAndSell_BuyTestVerificationPassed, buyTab));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.BuyAndSell_BuyTestVerificationFailed, buyTab), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.BuyAndSell_BuyTestVerificationFailed, buyTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }


        [Fact]
        public void TC43_VerifyBuyAndSell_SellTest()
        {
            instrument = TestData.GetData("Instrument");
            sellTab = TestData.GetData("SellTab");
            buyTab = TestData.GetData("BuyTab");
            sellOrderSize = TestData.GetData("TC43_SellOrderSize");
            limitPrice = TestData.GetData("TC43_LimitPrice");
            timeInForce = TestData.GetData("TC11_TimeInForce");

            UserFunctions userfuntionality = new UserFunctions(TestProgressLogger);
            BuyAndSellPage objBuyAndSellPage = new BuyAndSellPage(TestProgressLogger);
            UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
            UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
            try
            {
                string askPrice;
                TestProgressLogger.StartTest();
                userFunctions.LogIn(TestProgressLogger, Const.USER8);

                // Setting up market by placing Limit buy Order
                askPrice = userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, sellTab, sellOrderSize, limitPrice, timeInForce);

                // Navigate to Buy&Sell page and place a sell order
                userfuntionality.LogIn(TestProgressLogger, Const.USER14);

                // Click on -> Dashboad Menu button -> Buy&Sell button
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.NavigateToBuySell(driver);

                // Click on "Sell" Button
                objBuyAndSellPage.SellBtn();

                // Verify "Make a Transaction" window
                Assert.True(objBuyAndSellPage.VerifyMakeATransaction());

                // Verify "Chart details" section
                Assert.True(objBuyAndSellPage.VerifyChart());

                // Verify "Sell" option is selected by default
                Assert.True(objBuyAndSellPage.VerifySellOption());

                // Verify 5th button with blank values is selected
                Assert.True(objBuyAndSellPage.VerifyFifthRadioButtonOption());
                Assert.True(objBuyAndSellPage.VerifyFifthWithBlankValues());

                // Place a sell order and verify the amount details
                objBuyAndSellPage.PlaceSellOrder(instrument, sellTab);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BuyAndSell_SellTestVerificationPassed, buyTab));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.BuyAndSell_SellTestVerificationFailed, buyTab), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.BuyAndSell_SellTestVerificationFailed, buyTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }
    }
}
