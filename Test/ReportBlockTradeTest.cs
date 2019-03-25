using AlphaPoint_QA.Common;
using AlphaPoint_QA.Pages;
using AlphaPoint_QA.Utils;
using OpenQA.Selenium;
using System;
using System.Threading;
using Xunit;
using Xunit.Abstractions;

namespace AlphaPoint_QA.Test
{
    [Collection("Alphapoint_QA_USER")]
    public class ReportBlockTradeTest : TestBase
    {
        private string instrument;
        private string orderType;
        private string menuTab;
        private string buyTab;
        private string sellTab;
        private string orderSize;
        private string limitPrice;
        private string timeInForce;
        private string counterPartyPrice;
        private string wrongCounterParty;
        private string productBoughtPrice;
        private string productSoldPrice;
        private string blocktradeReportStatus;
        private string userWithPermissions;
        private string userWithBadge;
        private string counterParty;
        private string submitBlockTradePermission;
        private string getOpenTradeReportsPermission;
        private string userByID;
        private string counterPartyAccountID;
        private string badgeIdNumber;
        private string buyerAccountID;
        private string state;
        private string firstPermission;
        private string SecondPermission;
        private string status;
        private string blockTradeCounterPartyID;

        public ReportBlockTradeTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void TC33_VerifyBuyBlockTradeWithLockedInTest()
        {
            instrument = TestData.GetData("Instrument");
            orderType = TestData.GetData("OrderType");
            menuTab = TestData.GetData("MenuTab");
            buyTab = TestData.GetData("BuyTab");
            sellTab = TestData.GetData("SellTab");
            orderSize = TestData.GetData("OrderSize");
            limitPrice = TestData.GetData("LimitPrice");
            timeInForce = TestData.GetData("TimeInForce");
            counterParty = TestData.GetData("TC33_CounterPartyPrice");
            counterPartyPrice = TestData.GetData("TC33_CounterPartyPrice");
            productBoughtPrice = TestData.GetData("TC33_ProductBoughtPrice");
            productSoldPrice = TestData.GetData("TC33_ProductSoldPrice");
            wrongCounterParty = TestData.GetData("TC33_IncorrectCounterParty");
            blocktradeReportStatus = TestData.GetData("TC33_TradeReportStatus");
            userWithBadge = TestData.GetData("TC33_UserWithBadge");
            userWithPermissions = TestData.GetData("TC33_UserWithPermissions");
            submitBlockTradePermission = TestData.GetData("TC33_SubmitBlockTradePermission");
            getOpenTradeReportsPermission = TestData.GetData("TC33_GetOpenTradeReportsPermission");
            userByID = TestData.GetData("TC33_UserByID");
            counterPartyAccountID = TestData.GetData("TC33_CounterPartyAccountID");
            buyerAccountID = TestData.GetData("TC33_BuyerAccountID");
            badgeIdNumber = TestData.GetData("TC33_BadgeNumber");
            state = TestData.GetData("TC33_State");
            firstPermission = TestData.GetData("TC33_GetOpenTradeReports");
            SecondPermission = TestData.GetData("TC33_SubmitBlockTrade");

            AdminFunctions objAdminFunctions = new AdminFunctions(TestProgressLogger);
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(TestProgressLogger);
            UserFunctions userfuntionality = new UserFunctions(TestProgressLogger);
            ReportBlockTradePage objReportBlockTradePage = new ReportBlockTradePage(TestProgressLogger);
            UserFunctions objUserFunctions = new UserFunctions(TestProgressLogger);

            try
            {
                TestProgressLogger.StartTest();

                // The admin will create an badge to user say XYZ and provide the submitblock, getopentradereport permission to user say ABC
                // Login as Admin
                objAdminFunctions.AdminLogIn(TestProgressLogger);

                // Enter "userID"(ex. 185) in the "OpenUserbyID" textfield
                objAdminCommonFunctions.UserByIDText(userByID);

                // Click on "Open" button --> click on "Add permission" button --> provide SubmitBlockTrade and GetOpenTradeReports
                objAdminCommonFunctions.OpenUserButton();
                objAdminCommonFunctions.UserPermissionButton();
                objAdminCommonFunctions.AddSubmitBlockTradePermissions(submitBlockTradePermission);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.FirstPermissionGivenPassed, firstPermission));
                objAdminCommonFunctions.ClearTextBox();
                objAdminCommonFunctions.AddGetOpenTradeReportsPermissions(getOpenTradeReportsPermission);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.SecondPermissionGivenPassed, SecondPermission));
                objAdminCommonFunctions.ClosePermissionWindow();
                Thread.Sleep(2000);

                // Click on "Accounts" menu button --> Enter "accountID"(194) in the "OpenAccountbyID" textfield --> Click on "Open" button
                objAdminCommonFunctions.SelectAccountsMenu();
                objAdminCommonFunctions.OpenAccountByIDText(counterPartyAccountID);
                objAdminCommonFunctions.OpenAccountBtn();

                // Click on "Add New Badge" button --> provide badge number and click on create button
                objAdminCommonFunctions.OpenAddNewBadgeButtonForUser();
                objAdminCommonFunctions.SubmitCreateAccountBadgeButton();
                Thread.Sleep(2000);
                objAdminCommonFunctions.UserBadgeIDValue(badgeIdNumber);
                objAdminCommonFunctions.CreateBadgeAccount();

                //Logout from Admin
                objAdminCommonFunctions.UserMenuBtn();
                objAdminFunctions.AdminLogOut();

                // Below will perform a submit block trade with using lockedIn(by user ABC) and verify the various functionalities
                userfuntionality.LogIn(TestProgressLogger, Const.USER6);
                Thread.Sleep(2000);

                // Click on "Dashboard" menu button --> select an exchange --> select an instrument BTCUSD                
                UserCommonFunctions.DashBoardMenuButton(driver);             
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                Thread.Sleep(3000);
                UserCommonFunctions.ScrollingDownVertical(driver);

                //Click on "Report Block Trade" Button
                objReportBlockTradePage.ReportBlockTradeButton();

                // Verify window for submitting block trade appears
                objReportBlockTradePage.VerifyReportBlockTradeWindow();

                // Verify drop down for "Instrument" is present
                objReportBlockTradePage.VerifyDropdownInstrument();

                // Verify "Counter Party" with locked in check box is present
                objReportBlockTradePage.VerifyCounterParty();
                objReportBlockTradePage.VerifyLockedInCheckbox();

                // Verify  "Product Bought" is present
                objReportBlockTradePage.VerifyProductBought();

                // Verify  "Product Sold" is present
                objReportBlockTradePage.VerifyProductSold();

                // Verify "Fee" is present 
                objReportBlockTradePage.VerifyFees();

                // Verify balances for both products(product bought and product sold)
                objReportBlockTradePage.VerifyBalances();

                // Verify the details presen in Report block trade window and submit a Buy Block trade transaction
                objReportBlockTradePage.VerifyElementsAndSubmitBlockTradeReport(counterPartyPrice, wrongCounterParty, productBoughtPrice, productSoldPrice);

                //verify the details in Trade Report tab
                var otherPartyBlockTradeData = objReportBlockTradePage.SubmitBuyTradeReport(instrument, buyTab, counterPartyPrice, productBoughtPrice, productSoldPrice, blocktradeReportStatus);

                //Logout from user portal
                objUserFunctions.LogOut();

                // Login as "ABC" i.e. other party of block trade
                userfuntionality.LogIn(TestProgressLogger, Const.USER5);
                Thread.Sleep(2000);

                // Click on "Dashboard" menu button --> select an exchange --> select an instrument BTCUSD  
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                Thread.Sleep(3000);
                UserCommonFunctions.ScrollingDownVertical(driver);

                // Verify the Block Trade Trade Report of Other Party 
                objReportBlockTradePage.VerifyOtherPartyBlockTradeReportTab(instrument, sellTab, counterPartyPrice, productBoughtPrice, productSoldPrice, blocktradeReportStatus, otherPartyBlockTradeData);
                objUserFunctions.LogOut();

                // The admin will verify if submitted block trade report is present in block trade tab under trade menu section
                //  Login as Admin -> Trades -> Block Trades --> select BTCUSD instrument
                objAdminFunctions.AdminLogIn(TestProgressLogger);
                objReportBlockTradePage.VerifyBlockTradeInAdmin(buyerAccountID, counterPartyAccountID, instrument, productBoughtPrice, productBoughtPrice);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BlockTradeWithLockedInTestPassedMsg, buyTab));
                TestProgressLogger.EndTest();
            }
            catch (NoSuchElementException e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogError(String.Format(LogMessage.BlockTradeWithLockedInTestFailedMsg, buyTab), e);
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                throw e;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogError(String.Format(LogMessage.BlockTradeWithLockedInTestFailedMsg, buyTab), e);
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                throw e;

            }
            finally
            {
                objAdminCommonFunctions.UserMenuBtn();
                objAdminFunctions.AdminLogOut();
            }
        }


        [Fact]
        public void TC34_VerifyBuyBlockTradeWithOutLockedInTest()
        {
            instrument = TestData.GetData("Instrument");
            orderType = TestData.GetData("OrderType");
            menuTab = TestData.GetData("MenuTab");
            buyTab = TestData.GetData("BuyTab");
            sellTab = TestData.GetData("SellTab");
            orderSize = TestData.GetData("OrderSize");
            limitPrice = TestData.GetData("LimitPrice");
            timeInForce = TestData.GetData("TimeInForce");
            counterParty = TestData.GetData("TC34_CounterPartyPrice");
            counterPartyPrice = TestData.GetData("TC34_CounterPartyPrice");
            productBoughtPrice = TestData.GetData("TC34_ProductBoughtPrice");
            productSoldPrice = TestData.GetData("TC34_ProductSoldPrice");
            blocktradeReportStatus = TestData.GetData("TC34_TradeReportStatus");
            userWithBadge = TestData.GetData("TC34_UserWithBadge");
            userWithPermissions = TestData.GetData("TC34_UserWithPermissions");
            submitBlockTradePermission = TestData.GetData("TC34_SubmitBlockTradePermission");
            getOpenTradeReportsPermission = TestData.GetData("TC34_GetOpenTradeReportsPermission");
            userByID = TestData.GetData("TC34_UserByID");
            counterPartyAccountID = TestData.GetData("TC34_CounterPartyAccountID");
            buyerAccountID = TestData.GetData("TC34_BuyerAccountID");
            badgeIdNumber = TestData.GetData("TC34_BadgeNumber");
            status = TestData.GetData("TC34_Status");
            blockTradeCounterPartyID = TestData.GetData("TC34_BLockTradeCounterPartyAccountID");

            AdminFunctions objAdminFunctions = new AdminFunctions(TestProgressLogger);
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(TestProgressLogger);
            UserFunctions userfuntionality = new UserFunctions(TestProgressLogger);
            ReportBlockTradePage objReportBlockTradePage = new ReportBlockTradePage(TestProgressLogger);
            UserFunctions objUserFunctions = new UserFunctions(TestProgressLogger);


            try
            {
                TestProgressLogger.StartTest();

                // The admin will create an badge to user say XYZ and provide the submitblock, getopentradereport permission to user say ABC
                // Login as Admin
                objAdminFunctions.AdminLogIn(TestProgressLogger);

                // Enter "userID"(ex. 185) in the "OpenUserbyID" textfield
                objAdminCommonFunctions.UserByIDText(userByID);

                // Click on "Open" button --> click on "Add permission" button --> provide SubmitBlockTrade and GetOpenTradeReports
                objAdminCommonFunctions.OpenUserButton();
                objAdminCommonFunctions.UserPermissionButton();
                objAdminCommonFunctions.AddSubmitBlockTradePermissions(submitBlockTradePermission);
                objAdminCommonFunctions.ClearTextBox();
                objAdminCommonFunctions.AddGetOpenTradeReportsPermissions(getOpenTradeReportsPermission);
                objAdminCommonFunctions.ClosePermissionWindow();
                Thread.Sleep(2000);

                // Click on "Accounts" menu button --> Enter "accountID"(195) in the "OpenAccountbyID" textfield --> Click on "Open" button
                objAdminCommonFunctions.SelectAccountsMenu();
                objAdminCommonFunctions.OpenAccountByIDText(counterPartyAccountID);
                objAdminCommonFunctions.OpenAccountBtn();

                // Click on "Add New Badge" button --> provide badge number and click on create button
                objAdminCommonFunctions.OpenAddNewBadgeButtonForUser();
                objAdminCommonFunctions.SubmitCreateAccountBadgeButton();
                Thread.Sleep(2000);
                objAdminCommonFunctions.UserBadgeIDValue(badgeIdNumber);
                objAdminCommonFunctions.CreateBadgeAccount();
                Thread.Sleep(2000);

                //Logout from Admin
                objAdminCommonFunctions.UserMenuBtn();
                objAdminFunctions.AdminLogOut();

                // The user XYZ will perform a submit block trade without using lockedIn and verify the various functionalities
                userfuntionality.LogIn(TestProgressLogger, Const.USER6);
                Thread.Sleep(2000);

                // Click on "Dashboard" menu button --> select an exchange --> select an instrument BTCUSD  
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                Thread.Sleep(3000);
                UserCommonFunctions.ScrollingDownVertical(driver);

                // Click on "Report Block Trade" button
                objReportBlockTradePage.ReportBlockTradeButton();

                // Verify window for submitting block trade appears
                objReportBlockTradePage.VerifyReportBlockTradeWindow();

                // Verify drop down for "Instrument" is present
                objReportBlockTradePage.VerifyDropdownInstrument();

                //Verify "Counter Party" is present
                objReportBlockTradePage.VerifyCounterParty();

                // Verify "Product Bought" is present
                objReportBlockTradePage.VerifyProductBought();

                // Verify "Product Sold" is present
                objReportBlockTradePage.VerifyProductSold();

                // Verify "Fee" is present
                objReportBlockTradePage.VerifyFees();

                // Verify balances for both products(product bought and product sold)
                objReportBlockTradePage.VerifyBalances();

                // Verify the details present in Report block trade window and submit a Buy Block trade transaction( by user2) without LockedIn checkbox
                objReportBlockTradePage.SubmitBlockTradeReportWithoutLockedInCheckBox(instrument, counterPartyPrice, productBoughtPrice, productSoldPrice, counterParty, buyTab, blocktradeReportStatus);

                // Logout from user portal
                objUserFunctions.LogOut();

                // The user ABC will perform similar block trade provided by XYZ without using lockedIn and  using "Badge" provided by "XYZ"                              
                userfuntionality.LogIn(TestProgressLogger, Const.USER5);
                Thread.Sleep(2000);

                // Click on "Dashboard" menu button --> select an exchange --> select an instrument BTCUSD  
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);
                Thread.Sleep(3000);
                UserCommonFunctions.ScrollingDownVertical(driver);

                //Click on "Report Block trade" button
                objReportBlockTradePage.ReportBlockTradeButton();

                // Verify the details present in Report block trade window and submit a Buy Block trade transaction( by user1) without LockedIn checkbox
                objReportBlockTradePage.SubmitBlockTradeReportWithoutLockedInCheckBox(instrument, counterPartyPrice, productBoughtPrice, productSoldPrice, counterParty, buyTab, blocktradeReportStatus);

                // Logout from user portal
                objUserFunctions.LogOut();

                // The admin will verify if submitted block trade report is present in block trade tab under trade menu section

                // Login as Admin -> Trades -> Block Trades --> select BTCUSD instrument
                objAdminFunctions.AdminLogIn(TestProgressLogger);                
                objAdminCommonFunctions.SelectTradeMenu();
                objAdminCommonFunctions.BlockTradeBtn();
                objAdminCommonFunctions.BlockTradeInstrumentSelection(instrument);

                // Verify the Trade status under Block trade tab
                objAdminCommonFunctions.VerifyBlockTradeList(blockTradeCounterPartyID, counterPartyAccountID, status, instrument, buyTab, productBoughtPrice);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BlockTradeWithOutLockedInTestPassedMsg, buyTab));
                TestProgressLogger.EndTest();
            }
            catch (NoSuchElementException e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.BlockTradeWithOutLockedInTestFailedMsg, buyTab), e);
                throw e;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.BlockTradeWithOutLockedInTestFailedMsg, buyTab), e);
                throw e;

            }
            finally
            {
                objAdminCommonFunctions.UserMenuBtn();
                objAdminFunctions.AdminLogOut();
            }
        }

        [Fact]
        public void TC35_VerifyCancelBlockTradeOrderWithoutLockedInTest()
        {
            instrument = TestData.GetData("Instrument");
            counterPartyPrice = TestData.GetData("TC33_CounterPartyPrice");
            productBoughtPrice = TestData.GetData("TC33_ProductBoughtPrice");
            productSoldPrice = TestData.GetData("TC33_ProductSoldPrice");
            UserFunctions userfuntionality = new UserFunctions(TestProgressLogger);
            DetailsOnLandingPage objDetailsOnLandingPage = new DetailsOnLandingPage(TestProgressLogger);
            ReportBlockTradePage objReportBlockTradePage = new ReportBlockTradePage(TestProgressLogger);
            VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, TestProgressLogger);
            try
            {
                // Below will perform a submit block trade, verify if order is appeared in open orders, check if cancel button is present
                TestProgressLogger.StartTest();

                // Login in user portal
                userfuntionality.LogIn(TestProgressLogger, Const.USER6);
                Thread.Sleep(2000);

                // Click on "Dashboard" menu button --> select an exchange --> select an instrument BTCUSD  
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);

                // Verify Exchange Menu
                Assert.True(objDetailsOnLandingPage.ExchangeLinkButton());
                UserCommonFunctions.SelectInstrumentFromExchange(instrument, driver);

                // Click on "Report block Trade" button
                objReportBlockTradePage.ReportBlockTradeButton();

                // Verify window for submitting block trade appears
                objReportBlockTradePage.VerifyReportBlockTradeWindow();

                // Perform a submit block trade transaction and verify the details
                objReportBlockTradePage.SubmitBlockTradeReportForUser(counterPartyPrice, productBoughtPrice, productSoldPrice);

                //verify cancel block trade order 
                Assert.True(objVerifyOrdersTab.VerifyCancelBlockTradeOrdersInOpenOrderTab());
                Thread.Sleep(3000);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedCancelOrderButtonPassed));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedBlockTradeWithoutLockedInCancelPassed));                
            }

            catch (NoSuchElementException e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogError(String.Format(LogMessage.VerifiedCancelOrderButtonFailed), e);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifiedBlockTradeWithoutLockedInCancelFailed), e);
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                throw e;

            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogError(String.Format(LogMessage.VerifiedCancelOrderButtonFailed), e);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifiedBlockTradeWithoutLockedInCancelFailed), e);
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                throw e;

            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }


    }
}
