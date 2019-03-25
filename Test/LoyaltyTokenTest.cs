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
    public class LoyaltyTokenTest : TestBase
    {
        private string instrument;
        private string accountID;
        private string buyMarketOrderAmount;
        private string sellMarketOrderAmount;
        private string buyTab;
        private string sellTab;

        public LoyaltyTokenTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void TC49_VerifyLoyalityFeeBuyMarketOrderTest()
        {
            instrument = TestData.GetData("Instrument");
            buyTab = TestData.GetData("BuyTab");          
            accountID = TestData.GetData("TC49_AccountID");
            buyMarketOrderAmount = TestData.GetData("TC49_BuyMarketOrderAmount");
           
            UserFunctions userFuntions = new UserFunctions(TestProgressLogger);
            AdminFunctions objAdminFunctions = new AdminFunctions(TestProgressLogger);
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(TestProgressLogger);
            UserCommonFunctions userCommonFunctions = new UserCommonFunctions(TestProgressLogger);
            LoyaltyTokenPage loyaltyTokenPage = new LoyaltyTokenPage(TestProgressLogger);
            OrderEntryPage orderEnteryPage = new OrderEntryPage(driver, TestProgressLogger);
            try
            {
                
                TestProgressLogger.StartTest();
                // The admin will enabled the loyalty fee(LTC) to the user
                // Login as admin
                objAdminFunctions.AdminLogIn(TestProgressLogger);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdminLoyaltyFeeCheckStart, accountID));
                objAdminCommonFunctions.SelectAccountsMenu();
                // Enter accountID  in "Open Account by ID" textbox of the user for which Loyalty Fee should be applied
                objAdminCommonFunctions.OpenAccountByIDText(accountID);
                // Click on "Open" button
                objAdminCommonFunctions.OpenAccountBtn();
                // Click on "Edit Account Information" link
                objAdminCommonFunctions.EditInformationButton();
                // Click on "Loyalty Fees Enabled(LTC)" checkbox button. This will enable Fees to be deducted int the form of LTC
                objAdminCommonFunctions.LoyaltyFeeCheckedOrUnchecked();
                objAdminCommonFunctions.SaveButton();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdminLoyaltyFeeCheckCompleted, accountID));
                objAdminCommonFunctions.UserMenuBtn();
                // Admin LogOut
                objAdminFunctions.AdminLogOut();

                // This will verify whether the Fee applied is in the form of LTC 
                // Login as User for which Loyalty Fees Enabled(LTC) was selected
                // Navigate to UserSetting -> Loyalty Token -> Select Radio Button for LTC
                userFuntions.LogIn(TestProgressLogger, Const.USER13);
                UserCommonFunctions.DashBoardMenuButton(driver); 
                UserCommonFunctions.NavigateToUserSetting(driver);
                loyaltyTokenPage.LoyaltyTokenButton(driver);
                loyaltyTokenPage.TradingFeeRadioButton(driver);
                loyaltyTokenPage.UserSettingMenuButton(driver);
                TestProgressLogger.LogCheckPoint(LogMessage.SelectLoyaltyTokenSuccess);
                // Navigate to Exchange -> Order Entry -> Enter Buy Amount 
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);                
                orderEnteryPage.MarketOrderTypeBtn();
                orderEnteryPage.EnterBuyAmount(buyMarketOrderAmount);

                // Verify Fees displayed is in the form of LTC 
                Assert.True(loyaltyTokenPage.GetFeeText());
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifyAppliedFeeIsLTC, buyTab));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.LoyaltyTokenSuccessMsg, buyTab));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.LoyaltyTokenFailureMsg, buyTab), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.LoyaltyTokenFailureMsg, buyTab), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }

        [Fact]
        public void TC50_VerifyLoyalityFeeSellMarketOrderTest()
        {
            instrument = TestData.GetData("Instrument");
            sellTab = TestData.GetData("SellTab");
            accountID = TestData.GetData("TC50_AccountID");           
            sellMarketOrderAmount = TestData.GetData("TC50_SellMarketOrderAmount");


            UserFunctions userFuntions = new UserFunctions(TestProgressLogger);
            AdminFunctions objAdminFunctions = new AdminFunctions(TestProgressLogger);
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(TestProgressLogger);
            UserCommonFunctions userCommonFunctions = new UserCommonFunctions(TestProgressLogger);
            LoyaltyTokenPage loyaltyTokenPage = new LoyaltyTokenPage(TestProgressLogger);
            OrderEntryPage orderEnteryPage = new OrderEntryPage(driver, TestProgressLogger);
            try
            {

                TestProgressLogger.StartTest();

                //The admin will enabled the loyalty fee(LTC) to the user
                // Login as admin
                objAdminFunctions.AdminLogIn(TestProgressLogger);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdminLoyaltyFeeCheckStart, accountID));
                objAdminCommonFunctions.SelectAccountsMenu();
                // Enter accountID  in "Open Account by ID" textbox of the user for which Loyalty Fee should be applied
                objAdminCommonFunctions.OpenAccountByIDText(accountID);
                // Click on "Open" button
                objAdminCommonFunctions.OpenAccountBtn();
                // Click on "Edit Account Information" link
                objAdminCommonFunctions.EditInformationButton();
                // Click on "Loyalty Fees Enabled(LTC)" checkbox button. This will enable Fees to be deducted int the form of LTC
                objAdminCommonFunctions.LoyaltyFeeCheckedOrUnchecked();
                objAdminCommonFunctions.SaveButton();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdminLoyaltyFeeCheckCompleted, accountID));
                objAdminCommonFunctions.UserMenuBtn();
                // Admin LogOut
                objAdminFunctions.AdminLogOut();

                // This will verify whether the Fee applied is in the form of LTC 
                // Login as User for which Loyalty Fees Enabled(LTC) was selected
                // Navigate to UserSetting -> Loyalty Token -> Select Radio Button for LTC
                userFuntions.LogIn(TestProgressLogger, Const.USER13);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.NavigateToUserSetting(driver);
                loyaltyTokenPage.LoyaltyTokenButton(driver);
                loyaltyTokenPage.TradingFeeRadioButton(driver);
                loyaltyTokenPage.UserSettingMenuButton(driver);
                TestProgressLogger.LogCheckPoint(LogMessage.SelectLoyaltyTokenSuccess);
                // Navigate to Exchange -> Order Entry -> Enter Sell Amount 
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifyAppliedFeeIsLTC, sellTab));
                orderEnteryPage.SellOrderEntryBtn();
                orderEnteryPage.MarketOrderTypeBtn();
                orderEnteryPage.EnterSellAmount(sellMarketOrderAmount);

                // Verify Fees displayed is in the form of LTC 
                Assert.True(loyaltyTokenPage.GetFeeText());
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.LoyaltyTokenSuccessMsg, sellTab));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.LoyaltyTokenFailureMsg, sellTab), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.LoyaltyTokenFailureMsg, sellTab), e);
                throw e;
            }
            finally
            {
               TestProgressLogger.EndTest();
            }
        }
    }
}
