using AlphaPoint_QA.Common;
using AlphaPoint_QA.Pages;
using AlphaPoint_QA.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Xunit;
using Xunit.Abstractions;

namespace AlphaPoint_QA.Test
{
    public class AdminAccountsTest : TestBase
    {




        public AdminAccountsTest(ITestOutputHelper output) : base(output)
        {

        }

        [Fact]      //Admin_3
        public void UpdateAccountInformation()
        {
            try
            {
                string accountId;
                string updatedAccountName;
                string actualUpdatedAccountName;
                string accountNameOnAccontDetails;

                accountId = TestData.GetData("TCAdmin4_UserAccountID");

                AdminFunctions adminfunctions = new AdminFunctions(TestProgressLogger);
                AdminCommonFunctions admincommonfunctions = new AdminCommonFunctions(TestProgressLogger);
                AdminAccountsPage adminAccountPage = new AdminAccountsPage(TestProgressLogger);
                AdminFunctions adminFunctions = new AdminFunctions(TestProgressLogger);
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);

                // login in admin
                adminfunctions.AdminLogIn(TestProgressLogger, Const.ADMIN1);

                // Navigate on Accounts page.
                admincommonfunctions.SelectAccountsMenu();
                admincommonfunctions.SelectAccountsTab();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.NavigateWalletsPage));

                // Double click on particular user and edit account name.
                adminAccountPage.ClickOnViewAll(driver);
                adminAccountPage.SearchByAccountID(driver, accountId);
                adminAccountPage.DoubleClickOnAccountName(driver, accountId);
                adminAccountPage.EditAccountInformation(driver);
                updatedAccountName = adminAccountPage.EditAccountName(driver);
                adminAccountPage.ClickOnSaveButton(driver);

                // Verify updated account name on account details.
                accountNameOnAccontDetails = adminAccountPage.GetAccountNameOnAccountDetails(driver);
                Assert.Equal(updatedAccountName, accountNameOnAccontDetails);
                admincommonfunctions.UserMenuBtn();
                adminFunctions.AdminLogOut();

                // Log in user portal 
                userFunctions.LogIn(TestProgressLogger, Const.USER17);

                // Verify update account name on user portal.
                actualUpdatedAccountName = userFunctions.GetTextOfLoggedInUser();
                Assert.Equal(updatedAccountName, actualUpdatedAccountName);

            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.Error("", e);
                throw e;
            }
        }

        [Fact]      //Admin_4
        public void ManualWithdraw()
        {
            try
            {
                string accountId;
                string USDCurrency;
                string holdAmountField;
                string pendingWithdrawsField;
                string holdAmountBeforeWithdraw;
                string pendingWithdrawsAmtBeforeWithdraw;
                string expectedIncreasedPendingAmount;
                string pendingWithdrawsAmtAfterWithdraw;
                string amount;
                string fullName;
                string language;
                string comment;
                string bankAddress;
                string bankAccountNumber;
                string bankAccountName;
                string swiftCode;
                string toastMessage;
                string recentTicketID;
                string ticketIDValue;
                string dailyWithdrawAmountBeforeAccept;
                string dailyWithdrawAmountAfterAccept;
                string monthlyWithdrawAmountBeforeAccept;
                string monthlyWithdrawAmountAfterAccept;
                string dailyWithdrawsField;
                string monthlyWithdrawsField;
                string pendingWithdrawsAmtAfterAccept;

                accountId = TestData.GetData("TCAdmin4_UserAccountID");
                USDCurrency = TestData.GetData("USDCurrency");
                holdAmountField = TestData.GetData("TCAdmin4_HoldAmountField");
                pendingWithdrawsField = TestData.GetData("TCAdmin4_PendingWithdrawsField");
                dailyWithdrawsField = TestData.GetData("TCAdmin4_DailyWithdrawsField");
                monthlyWithdrawsField = TestData.GetData("TCAdmin4_MonthlyWithdrawsField");
                amount = TestData.GetData("TCAdmin4_Amount");
                fullName = TestData.GetData("TCAdmin4_FullName");
                language = TestData.GetData("TCAdmin4_Language");
                comment = TestData.GetData("TCAdmin4_Comment");
                bankAddress = TestData.GetData("TCAdmin4_BankAddress");
                bankAccountNumber = TestData.GetData("TCAdmin4_BankAccountNumber");
                bankAccountName = TestData.GetData("TCAdmin4_BankAccountName");
                swiftCode = TestData.GetData("TCAdmin4_SwiftCode");
                ticketIDValue = TestData.GetData("TCAdmin4_TicketIDValue");

                AdminFunctions adminfunctions = new AdminFunctions(TestProgressLogger);
                AdminCommonFunctions admincommonfunctions = new AdminCommonFunctions(TestProgressLogger);
                AdminAccountsPage adminAccountPage = new AdminAccountsPage(TestProgressLogger);
                AdminTicketsPage admintickets = new AdminTicketsPage();

                // login in admin
                adminfunctions.AdminLogIn(TestProgressLogger, Const.ADMIN1);

                // Navigate on Accounts page.
                admincommonfunctions.SelectAccountsMenu();
                admincommonfunctions.SelectAccountsTab();
                TestProgressLogger.LogCheckPoint(LogMessage.NavigateAccountPage);

                // Click on ViewAll and search by accountid and double click.
                adminAccountPage.ClickOnViewAll(driver);
                adminAccountPage.SearchByAccountID(driver, accountId);
                adminAccountPage.DoubleClickOnAccountName(driver, accountId);
                TestProgressLogger.LogCheckPoint(string.Format(LogMessage.NavigateAccountPage, accountId));

                // Get balances before manual withdraw.
                Dictionary<string, string> balancesData = adminAccountPage.GetBalances(driver, USDCurrency);
                holdAmountBeforeWithdraw = balancesData.GetValueOrDefault(holdAmountField);
                pendingWithdrawsAmtBeforeWithdraw = balancesData.GetValueOrDefault(pendingWithdrawsField);

                // Click on manual Withdraw button and enter details in modal.
                adminAccountPage.ClickOnManualWithdrawButton(driver);
                adminAccountPage.ManualWithdrawUSD(driver, USDCurrency, amount, fullName, language, comment, bankAddress, bankAccountNumber, bankAccountName, swiftCode);
                TestProgressLogger.LogCheckPoint(string.Format(LogMessage.ManualWithdrawUSD, amount));

                expectedIncreasedPendingAmount = GenericUtils.AddTwoValue(pendingWithdrawsAmtBeforeWithdraw, amount);

                // Verify withdraw success msg. 
                toastMessage = UserCommonFunctions.GetTextOfToastMessageInAdmin(driver, TestProgressLogger);
                Assert.Equal(Const.TCA4_WithdrawTicketSuccessfullyMSG, toastMessage);
                TestProgressLogger.LogCheckPoint(LogMessage.VerifyToastMSG);
                adminAccountPage.ClickOnRefreshInUserAccountSection(driver);

                // Get balances after manual withdraw.
                Dictionary<string, string> balancesDataAfter = adminAccountPage.GetBalances(driver, USDCurrency);
                pendingWithdrawsAmtAfterWithdraw = balancesDataAfter.GetValueOrDefault(pendingWithdrawsField);
                dailyWithdrawAmountBeforeAccept = balancesDataAfter.GetValueOrDefault(dailyWithdrawsField);
                monthlyWithdrawAmountBeforeAccept = balancesDataAfter.GetValueOrDefault(monthlyWithdrawsField);

                // Verify increased pending amount after manual withdraw.
                Assert.Equal(expectedIncreasedPendingAmount, pendingWithdrawsAmtAfterWithdraw);
                TestProgressLogger.LogCheckPoint(LogMessage.VerifyIncreasedPendingWithdrawAmt);

                recentTicketID = adminAccountPage.GetRecentTicketID(driver, USDCurrency, amount);

                // Navigate on Ticket-> Withdraw page and click on refresh button.
                admincommonfunctions.SelectTicketsMenu();
                admincommonfunctions.NavigateToWithdrawTicketsTab();
                admincommonfunctions.ClickOnRefreshButtonOnTicketsPage();
                TestProgressLogger.LogCheckPoint(LogMessage.NavigateTicketsPage);

                //Verify created ticket in tickets-> withdraw page.
                Dictionary<string, string> withdrawTicketsFields = admintickets.GetWithdrawTicketsFieldsByTicketID(driver, recentTicketID);
                Assert.Equal(recentTicketID, withdrawTicketsFields.GetValueOrDefault(ticketIDValue));
                admincommonfunctions.ClickOnTicketFromWithdrawTicketList(withdrawTicketsFields.GetValueOrDefault(ticketIDValue));
                admincommonfunctions.ClickOnAcceptButtonFromDepositsTicketModal();
                TestProgressLogger.LogCheckPoint(LogMessage.CreatedTicketsVerified);

                // Navigate on Accounts page.
                admincommonfunctions.SelectAccountsMenu();
                adminAccountPage.ClickOnRefreshInUserAccountSection(driver);
                TestProgressLogger.LogCheckPoint(LogMessage.NavigateAccountPage);

                // Get balances after accept ticket.
                Dictionary<string, string> balancesDataAfterAccept = adminAccountPage.GetBalances(driver, USDCurrency);
                pendingWithdrawsAmtAfterAccept = balancesDataAfterAccept.GetValueOrDefault(pendingWithdrawsField);
                dailyWithdrawAmountAfterAccept = balancesDataAfterAccept.GetValueOrDefault(dailyWithdrawsField);
                monthlyWithdrawAmountAfterAccept = balancesDataAfterAccept.GetValueOrDefault(monthlyWithdrawsField);

                // Verify pending withdraws, daily withdraw and monthly withdraw amount after accept ticket.
                Assert.Equal(GenericUtils.SubtractTwoValue(pendingWithdrawsAmtAfterWithdraw, amount), pendingWithdrawsAmtAfterAccept);
                Assert.Equal(GenericUtils.AddTwoValue(dailyWithdrawAmountBeforeAccept, amount), dailyWithdrawAmountAfterAccept);
                Assert.Equal(GenericUtils.AddTwoValue(monthlyWithdrawAmountBeforeAccept, amount), monthlyWithdrawAmountAfterAccept);
                TestProgressLogger.LogCheckPoint(LogMessage.VerifiedDailyAndMonthlyWithdraw);

                // Logout from admin.
                admincommonfunctions.UserMenuBtn();
                adminfunctions.AdminLogOut();
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(LogMessage.ManualWithdrawTestFailed, ex);

                throw;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(LogMessage.ManualWithdrawTestFailed, e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }

        [Fact]      //Admin_7
        public void AddBadgeToAccount()
        {
            try
            {
                string accountId;
                string badgeNumber;
                string actualBadgeNumber;

                accountId = TestData.GetData("TCAdmin4_UserAccountID");
                badgeNumber = TestData.GetData("TCAdmin7_BadgeNumber");


                AdminFunctions adminfunctions = new AdminFunctions(TestProgressLogger);
                AdminCommonFunctions admincommonfunctions = new AdminCommonFunctions(TestProgressLogger);
                AdminAccountsPage adminAccountPage = new AdminAccountsPage(TestProgressLogger);

                // login in admin
                adminfunctions.AdminLogIn(TestProgressLogger, Const.ADMIN1);

                // Navigate on Accounts page.
                admincommonfunctions.SelectAccountsMenu();
                admincommonfunctions.SelectAccountsTab();
                TestProgressLogger.LogCheckPoint(LogMessage.NavigateAccountPage);

                // Click on ViewAll and search by accountid and double click.
                adminAccountPage.ClickOnViewAll(driver);
                adminAccountPage.SearchByAccountID(driver, accountId);
                adminAccountPage.DoubleClickOnAccountName(driver, accountId);
                TestProgressLogger.LogCheckPoint(string.Format(LogMessage.NavigateAccountPage, accountId));

                // Add new badge.
                adminAccountPage.AddNewBadge(driver, badgeNumber);
                actualBadgeNumber=adminAccountPage.GetTextOfAccountBadges(driver);

                // Verify added badge.
                Assert.Equal(badgeNumber, actualBadgeNumber);
                TestProgressLogger.LogCheckPoint(string.Format(LogMessage.VerifiedBadgeAdded, accountId));

                // Delete Added badge.
                adminAccountPage.DeleteAccountBadge(driver);
                adminAccountPage.ClickOnYesButton(driver);

                // Logout from admin.
                admincommonfunctions.UserMenuBtn();
                adminfunctions.AdminLogOut();
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(LogMessage.AddBadgeToAccountTestFailed, ex);
                throw;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(LogMessage.AddBadgeToAccountTestFailed, e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }
        

        [Fact]      //Admin_8
        public void VerifyOpenOrderUnderOMSOpenOrders()
        {
            try
            {
                string accountId;
                string instrument;
                string buyTab;
                string buyOrderSize;
                string limitPrice;
                string timeInForce;
                string quantity;
                string price;

                instrument = TestData.GetData("Instrument");
                limitPrice = TestData.GetData("TCAdmin8_LimitPrice");
                timeInForce = TestData.GetData("TimeInForce");
                buyTab = TestData.GetData("BuyTab");
                buyOrderSize = TestData.GetData("TCAdmin8_BuyOrderSize");
                accountId = TestData.GetData("TCAdmin4_UserAccountID");
                quantity = TestData.GetData("TCAdmin8_QunatityField");
                price = TestData.GetData("TCAdmin8_PriceField");

                AdminFunctions adminfunctions = new AdminFunctions(TestProgressLogger);
                AdminCommonFunctions admincommonfunctions = new AdminCommonFunctions(TestProgressLogger);
                AdminAccountsPage adminAccountPage = new AdminAccountsPage(TestProgressLogger);
                UserFunctions userFunctions=new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);

                // Place sell order to set up market
                userFunctions.LogIn(TestProgressLogger, Const.USER17);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, limitPrice, timeInForce);
                TestProgressLogger.LogCheckPoint(LogMessage.PlaceBuyOrder);

                // login in admin
                adminfunctions.AdminLogIn(TestProgressLogger, Const.ADMIN1);

                // Navigate on Accounts page.
                admincommonfunctions.SelectAccountsMenu();
                admincommonfunctions.SelectAccountsTab();
                TestProgressLogger.LogCheckPoint(LogMessage.NavigateAccountPage);

                // Click on ViewAll and search by accountid and double click.
                adminAccountPage.ClickOnViewAll(driver);
                adminAccountPage.SearchByAccountID(driver, accountId);
                adminAccountPage.DoubleClickOnAccountName(driver, accountId);
                TestProgressLogger.LogCheckPoint(string.Format(LogMessage.NavigateAccountPage, accountId));

                // Verify price and quantity under open order section.
                Dictionary<string, string> orderData = adminAccountPage.GetOpenOrdersInAccountsPage(driver);
                Assert.Equal(GenericUtils.ConvertStringToDecimalFormat(buyOrderSize), orderData.GetValueOrDefault(quantity));
                Assert.Equal(limitPrice, orderData.GetValueOrDefault(price));
                TestProgressLogger.LogCheckPoint(LogMessage.VerifyPriceAndQuantityInOpenOrder);

                // Click on showall link under open order section.
                adminAccountPage.ClickShowAllUnderOpenOrderSection(driver);

                // Verify price and quantity in account order page.
                Dictionary<string, string> orderDataAccountOrder = adminAccountPage.GetPriceAndQuantityInAccountOrderPage(driver);
                Assert.Equal(GenericUtils.ConvertStringToDecimalFormat(buyOrderSize), orderDataAccountOrder.GetValueOrDefault(quantity));
                Assert.Equal(limitPrice, orderDataAccountOrder.GetValueOrDefault(price));
                TestProgressLogger.LogCheckPoint(LogMessage.VerifyPriceAndQuantityInAccountOrder);

                // Logout from admin.
                admincommonfunctions.UserMenuBtn();
                adminfunctions.AdminLogOut();
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(LogMessage.VerifyOpenOrderUnderOMSOpenOrdersTestFailed, ex);
                throw;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(LogMessage.VerifyOpenOrderUnderOMSOpenOrdersTestFailed, e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }

        [Fact]      //Admin_9
        public void VerifyReportsDownloadedExportToCSV()
        {
            try
            {
                string accountId;
                string instrument;
                string buyTab;
                string buyOrderSize;
                string limitPrice;
                string timeInForce;
                string quantity;
                string price;
                string orderID;
                string openOrderFileName;
                string accountActivityFileName;
                string orderHistoryFileName;
                bool accountActivityVal;

                instrument = TestData.GetData("Instrument");
                limitPrice = TestData.GetData("TCAdmin8_LimitPrice");
                timeInForce = TestData.GetData("TimeInForce");
                buyTab = TestData.GetData("BuyTab");
                buyOrderSize = TestData.GetData("TCAdmin8_BuyOrderSize");
                accountId = TestData.GetData("TCAdmin4_UserAccountID");
                quantity = TestData.GetData("TCAdmin8_QunatityField");
                price = TestData.GetData("TCAdmin8_PriceField");
                openOrderFileName = TestData.GetData("TCAdmin9_OpenOrderFileName");
                accountActivityFileName = TestData.GetData("TCAdmin9_AccountActivityFileName");
                orderHistoryFileName = TestData.GetData("TCAdmin9_AccountOrderHistoryFileName");

                AdminFunctions adminfunctions = new AdminFunctions(TestProgressLogger);
                AdminCommonFunctions admincommonfunctions = new AdminCommonFunctions(TestProgressLogger);
                AdminAccountsPage adminAccountPage = new AdminAccountsPage(TestProgressLogger);
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                GenericUtils genericUtils=new GenericUtils(TestProgressLogger);

                // Place sell order to set up market
                userFunctions.LogIn(TestProgressLogger, Const.USER17);
                userCommonFunction.CancelAndPlaceLimitBuyOrder(driver, instrument, buyTab, buyOrderSize, limitPrice, timeInForce);
                TestProgressLogger.LogCheckPoint(LogMessage.PlaceBuyOrder);

                // login in admin
                adminfunctions.AdminLogIn(TestProgressLogger, Const.ADMIN1);

                // Navigate on Accounts page.
                admincommonfunctions.SelectAccountsMenu();
                admincommonfunctions.SelectAccountsTab();
                TestProgressLogger.LogCheckPoint(LogMessage.NavigateAccountPage);

                // Click on ViewAll and search by accountid and double click.
                adminAccountPage.ClickOnViewAll(driver);
                adminAccountPage.SearchByAccountID(driver, accountId);
                adminAccountPage.DoubleClickOnAccountName(driver, accountId);
                TestProgressLogger.LogCheckPoint(string.Format(LogMessage.NavigateAccountPage, accountId));

                // Click on showall link under open order section.
                adminAccountPage.ClickShowAllUnderOpenOrderSection(driver);
                orderID=adminAccountPage.GetOrderIDInAccountOrderPage(driver);

                // Delete All previous file and download openorder csv file and verify data.
                genericUtils.DeleteAllFiles();
                adminAccountPage.ClickExportToCSVOpenOrder(driver);
                Dictionary<string, string> openOrderData = adminAccountPage.GetOpenOrderCSVData(orderID, openOrderFileName);
                Assert.Equal(limitPrice, openOrderData.GetValueOrDefault(price));
                Assert.Equal(buyOrderSize, openOrderData.GetValueOrDefault(quantity));
                TestProgressLogger.LogCheckPoint(LogMessage.VerifyDownloadCSVFileOfOpenOrder);

                // Click on showall link under account activity section.
                adminAccountPage.SelectAccountLink(driver);
                adminAccountPage.ClickShowAllUnderAccountActivitySection(driver);

                // Delete All previous file and download transactionhistory csv file and verify data.
                genericUtils.DeleteAllFiles();
                adminAccountPage.ClickExportToCSVAccountActivity(driver);
                accountActivityVal = adminAccountPage.VerifyAccountActivityCSVData(accountActivityFileName,orderID);
                Assert.True(accountActivityVal);
                TestProgressLogger.LogCheckPoint(LogMessage.VerifyDownloadCSVFileOfAccountActivity);

                // Click on showall link under order history section.
                adminAccountPage.SelectAccountLink(driver);
                adminAccountPage.ClickOnUserAccountTab(driver);
                adminAccountPage.ClickShowAllUnderOrderHistorySection(driver);

                // Delete All previous file and download order history csv file and verify data.
                genericUtils.DeleteAllFiles();
                adminAccountPage.ClickExportToCSVOrderHistory(driver);
                accountActivityVal = adminAccountPage.VerifyAccountActivityCSVData(orderHistoryFileName,orderID);
                Assert.True(accountActivityVal);
                TestProgressLogger.LogCheckPoint(LogMessage.VerifyDownloadCSVFileOfOrderHistory);

                // Click on showall link under order history section.
                adminAccountPage.SelectAccountLink(driver);
                adminAccountPage.ClickOnUserAccountTab(driver);
                adminAccountPage.ClickShowAllUnderTradeSection(driver);

                // Delete All previous file and download trade csv file
                genericUtils.DeleteAllFiles();
                adminAccountPage.ClickExportToCSVTrade(driver);

                TestProgressLogger.LogCheckPoint(LogMessage.VerifyDownloadCSVFileOfTrade);


                // Logout from admin.
                admincommonfunctions.UserMenuBtn();
                adminfunctions.AdminLogOut();
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(LogMessage.VerifyReportsDownloadedExportToCSVTestFailed, ex);
                throw;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(LogMessage.VerifyReportsDownloadedExportToCSVTestFailed, e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }

        [Fact]      //Admin_20
        public void VerifyAccountDetailsTabShowAllRelevantAccountInfo()
        {
            try
            {
                string accountId;
                string USDCurrency;
                string USDAmount;
                string commentValue;
                string amountField;
                string expectedAmount;
                string actualAmount;

                accountId = TestData.GetData("TCAdmin4_UserAccountID");
                USDCurrency = TestData.GetData("USDCurrency");
                USDAmount = TestData.GetData("USDAmount");
                commentValue = TestData.GetData("TC41_Comment");
                amountField = TestData.GetData("TCAdmin4_AmountField");

                AdminFunctions adminfunctions = new AdminFunctions(TestProgressLogger);
                AdminCommonFunctions admincommonfunctions = new AdminCommonFunctions(TestProgressLogger);
                AdminAccountsPage adminAccountPage = new AdminAccountsPage(TestProgressLogger);

                // login in admin
                adminfunctions.AdminLogIn(TestProgressLogger, Const.ADMIN1);

                // Navigate on Accounts page.
                admincommonfunctions.SelectAccountsMenu();
                admincommonfunctions.SelectAccountsTab();
                TestProgressLogger.LogCheckPoint(LogMessage.NavigateAccountPage);

                // Click on ViewAll and search by accountid and double click.
                adminAccountPage.ClickOnViewAll(driver);
                adminAccountPage.SearchByAccountID(driver, accountId);
                adminAccountPage.DoubleClickOnAccountName(driver, accountId);
                TestProgressLogger.LogCheckPoint(string.Format(LogMessage.NavigateAccountPage, accountId));
                
                // Verify labels under account details section.
                adminAccountPage.VerifyLabelUnderAccountDetailsSection();
                TestProgressLogger.LogCheckPoint(LogMessage.VerifyLabelsUnderAccountDetailsSection);

                // Verify labels under balances section.
                adminAccountPage.VerifyLabelsUnderBalancesSection();
                TestProgressLogger.LogCheckPoint(LogMessage.VerifyLabelsUnderBalancesSection);

                // Get balances of usd before credit amount.
                Dictionary<string, string> balancesData = adminAccountPage.GetBalances(driver, USDCurrency);

                // Credit usd amount using submit ledger entry.
                adminAccountPage.ClickOnSubmitLedgerEntryButton();
                adminAccountPage.CreditAmountInSubmintLedgerEntryModal(USDCurrency,USDAmount,commentValue);
                TestProgressLogger.LogCheckPoint(LogMessage.CreditAmountBySubmitLedgerEntry);

                // Get balances of usd after credit amount.
                adminAccountPage.ClickOnRefreshInUserAccountSection(driver);
                Dictionary<string, string> afterSubmitLedgerBalancesData = adminAccountPage.GetBalances(driver, USDCurrency);

                // Verify increased usd amount after credit by submit ledger entry. 
                expectedAmount=GenericUtils.GetSumFromStringAfterAddition(balancesData.GetValueOrDefault(amountField), USDAmount);
                actualAmount = GenericUtils.ConvertStringToDecimalFormat(afterSubmitLedgerBalancesData.GetValueOrDefault(amountField));
                Assert.Equal(expectedAmount, GenericUtils.RemoveCommaFromString(actualAmount));
                TestProgressLogger.LogCheckPoint(LogMessage.VerifyCreditedUSDAmount);
                
                // Verify all ticket ids in Decending order.
                Assert.True(adminAccountPage.VerifyTicketsIdInDecendingOrder());

                // Verify all ticket ids in ascending order.
                Assert.True(adminAccountPage.VerifyTicketsIdInAscendingOrder());

                // Logout from admin.
                admincommonfunctions.UserMenuBtn();
                adminfunctions.AdminLogOut();
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(LogMessage.AccountDetailsTabShowAllRelevantAccountInfoTestFailed, ex);
                throw;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(LogMessage.AccountDetailsTabShowAllRelevantAccountInfoTestFailed, e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }

        [Fact]      //Admin_21
        public void ExportCSVAllAccounts()
        {
            try
            {
                string allAccountFileName;

                allAccountFileName = TestData.GetData("TCAdmin21_AllAccountsFileName");

                AdminFunctions adminfunctions = new AdminFunctions(TestProgressLogger);
                AdminCommonFunctions admincommonfunctions = new AdminCommonFunctions(TestProgressLogger);
                AdminAccountsPage adminAccountPage = new AdminAccountsPage(TestProgressLogger);
                GenericUtils genericUtils=new GenericUtils(TestProgressLogger);

                // login in admin
                adminfunctions.AdminLogIn(TestProgressLogger, Const.ADMIN1);

                // Navigate on Accounts page.
                admincommonfunctions.SelectAccountsMenu();
                admincommonfunctions.SelectAccountsTab();
                TestProgressLogger.LogCheckPoint(LogMessage.NavigateAccountPage);

                // Delete all previous file and download allaccounts csv file.
                genericUtils.DeleteAllFiles();
                adminAccountPage.ClickOnExportCSVOnAccountsPage();
                TestProgressLogger.LogCheckPoint(LogMessage.DownloadAllAccountsCSVFile);

                // Verify data in allaccount csv file.
                Assert.True(adminAccountPage.VerifyAllAccountsCSVData(allAccountFileName));
                TestProgressLogger.LogCheckPoint(LogMessage.VerifyAllAccountsCSVFile);

                // Logout from admin.
                admincommonfunctions.UserMenuBtn();
                adminfunctions.AdminLogOut();
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(LogMessage.ExportCSVAllAccountsTestFailed, ex);
                throw;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(LogMessage.ExportCSVAllAccountsTestFailed, e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }


        [Fact]      //Admin_22
        public void ExportAllAccountsBalancesCSVFile()
        {
            try
            {
              
                string allBalancesFileName;
                string toastMessage;
                
                allBalancesFileName = TestData.GetData("TCAdmin22_AccountsBalancesFileName");

                AdminFunctions adminfunctions = new AdminFunctions(TestProgressLogger);
                AdminCommonFunctions admincommonfunctions = new AdminCommonFunctions(TestProgressLogger);
                AdminAccountsPage adminAccountPage = new AdminAccountsPage(TestProgressLogger);
                GenericUtils genericUtils = new GenericUtils(TestProgressLogger);

                // login in admin
                adminfunctions.AdminLogIn(TestProgressLogger, Const.ADMIN1);

                // Navigate on Accounts page.
                admincommonfunctions.SelectAccountsMenu();
                admincommonfunctions.SelectAccountsBalancesTab();
                TestProgressLogger.LogCheckPoint(LogMessage.NavigateAccountPage);

                // Delete all previous file and download allaccounts csv file.
                genericUtils.DeleteAllFiles();
                toastMessage = adminAccountPage.ClickOnExportAllBalancesButton();
                TestProgressLogger.LogCheckPoint(LogMessage.DownloadAllBalancessCSVFile);

                // Verify success toast msg.
                Assert.Equal(toastMessage,Const.TCAdmin22_AllAccountBalancesDownloadSuccessfullyMSG);

                // Verify data in all balances csv file.
                Assert.True(adminAccountPage.VerifyAllBalancesCSVData(allBalancesFileName));
                TestProgressLogger.LogCheckPoint(LogMessage.VerifyAllBalancesCSVFile);

                // Logout from admin.
                admincommonfunctions.UserMenuBtn();
                adminfunctions.AdminLogOut();
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(LogMessage.ExportAllBalancesCSVFileTestFailed, ex);
                throw;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(LogMessage.ExportAllBalancesCSVFileTestFailed, e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }






















        [Fact]      //Admin_29
        public void VerifyLedgerEntryCredit()
        {
            try
            {
                string accountId;
                string toastMessage;
                string product;
                bool creditFlag;
                string amount;
                string comment;
                string negativeAmount;
                string productBalancesBeforeUpdate;
                string productBalancesAfterUpdate;
                product = TestData.GetData("TCAdmin29_Product");
                amount = TestData.GetData("TCAdmin29_Amount");
                negativeAmount = TestData.GetData("TCAdmin29_NegativeAmount");
                comment = TestData.GetData("TCAdmin29_Comment");
                accountId = TestData.GetData("TCAdmin29_AccountID");
                AdminFunctions adminfunctions = new AdminFunctions(TestProgressLogger);
                AdminCommonFunctions admincommonfunctions = new AdminCommonFunctions(TestProgressLogger);
                AdminAccountsPage adminAccountPage = new AdminAccountsPage(TestProgressLogger);
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                GenericUtils genericUtils = new GenericUtils(TestProgressLogger);
                // Set creditFlag to True in case of credit amount
                creditFlag = true;

                // Login in admin
                adminfunctions.AdminLogIn(TestProgressLogger, Const.ADMIN1);
                // Navigate on Accounts page.
                admincommonfunctions.SelectAccountsMenu();
                admincommonfunctions.SelectAccountsTab();
                TestProgressLogger.LogCheckPoint(LogMessage.NavigateAccountPage);
                // Click on ViewAll and search by accountid and double click.
                adminAccountPage.ClickOnViewAll(driver);
                adminAccountPage.SearchByAccountID(driver, accountId);
                adminAccountPage.DoubleClickOnAccountName(driver, accountId);
                TestProgressLogger.LogCheckPoint(string.Format(LogMessage.NavigateAccountPage, accountId));
                // Submit Submit Ledger Entry with negative value
                adminAccountPage.SubmitLedgerEntryWithNegativeValue(product, creditFlag, negativeAmount, comment);
                // Verify submit ledger invalid request message
                toastMessage = adminAccountPage.SubmitLedgerInvalidToastMessage();
                Assert.Equal(Const.LedgerEntryInvalidRequestMsg, toastMessage);
                // Fetch the USD Account balance before update 
                productBalancesBeforeUpdate = adminAccountPage.ProductAmountBalancesBeforeUpdate();
                // Submit Submit Ledger Entry
                adminAccountPage.SubmitLedgerEntry(product, creditFlag, amount, comment);
                // Verify submit ledger success msg
                toastMessage = adminAccountPage.SubmitLedgerToastMessage();
                Assert.Equal(Const.LedgerEntrySuccessMsg, toastMessage);
                TestProgressLogger.LogCheckPoint(LogMessage.VerifyToastMSG);
                adminAccountPage.ClickOnRefreshInUserAccountSection(driver);
                // Fetch the USD Account balance after update 
                productBalancesAfterUpdate = adminAccountPage.ProductAmountBalancesAfterUpdate();
                // Verify that the balances are incremented by amount value
                Assert.True(adminAccountPage.VerifyUpdatedCreditBalance(amount, productBalancesBeforeUpdate, productBalancesAfterUpdate));
                // Logout from admin.
                admincommonfunctions.UserMenuBtn();
                adminfunctions.AdminLogOut();
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(LogMessage.VerifyLedgerEntryCreditTestFailed, ex);
                throw;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(LogMessage.VerifyLedgerEntryCreditTestFailed, e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }

        [Fact]      //Admin_30
        public void VerifyLedgerEntryDebit()
        {
            try
            {
                string accountId;
                string toastMessage;
                string product;
                bool creditFlag;
                string amount;
                string negativeAmount;
                string comment;
                string productBalancesBeforeUpdate;
                string productBalancesAfterUpdate;
                product = TestData.GetData("TCAdmin30_Product");
                amount = TestData.GetData("TCAdmin30_Amount");
                negativeAmount = TestData.GetData("TCAdmin30_NegativeAmount");
                comment = TestData.GetData("TCAdmin30_Comment");
                accountId = TestData.GetData("TCAdmin30_AccountID");
                AdminFunctions adminfunctions = new AdminFunctions(TestProgressLogger);
                AdminCommonFunctions admincommonfunctions = new AdminCommonFunctions(TestProgressLogger);
                AdminAccountsPage adminAccountPage = new AdminAccountsPage(TestProgressLogger);
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserCommonFunctions userCommonFunction = new UserCommonFunctions(TestProgressLogger);
                GenericUtils genericUtils = new GenericUtils(TestProgressLogger);
                // Set creditFlag to False in case of debit amount
                creditFlag = false;

                // Login in admin
                adminfunctions.AdminLogIn(TestProgressLogger, Const.ADMIN1);
                // Navigate on Accounts page.
                admincommonfunctions.SelectAccountsMenu();
                admincommonfunctions.SelectAccountsTab();
                TestProgressLogger.LogCheckPoint(LogMessage.NavigateAccountPage);
                // Click on ViewAll and search by accountid and double click.
                adminAccountPage.ClickOnViewAll(driver);
                adminAccountPage.SearchByAccountID(driver, accountId);
                adminAccountPage.DoubleClickOnAccountName(driver, accountId);
                TestProgressLogger.LogCheckPoint(string.Format(LogMessage.NavigateAccountPage, accountId));
                // Submit Submit Ledger Entry with negative value
                adminAccountPage.SubmitLedgerEntryWithNegativeValue(product, creditFlag, negativeAmount, comment);
                // Verify submit ledger invalid request message
                toastMessage = adminAccountPage.SubmitLedgerInvalidToastMessage();
                Assert.Equal(Const.LedgerEntryInvalidRequestMsg, toastMessage);
                // Fetch the USD Account balance before update 
                productBalancesBeforeUpdate = adminAccountPage.ProductAmountBalancesBeforeUpdate();
                // Submit Submit Ledger Entry
                adminAccountPage.SubmitLedgerEntry(product, creditFlag, amount, comment);
                // Verify submit ledger success msg
                toastMessage = adminAccountPage.SubmitLedgerToastMessage();
                Assert.Equal(Const.LedgerEntrySuccessMsg, toastMessage);
                TestProgressLogger.LogCheckPoint(LogMessage.VerifyToastMSG);
                adminAccountPage.ClickOnRefreshInUserAccountSection(driver);
                // Fetch the USD Account balance after update 
                productBalancesAfterUpdate = adminAccountPage.ProductAmountBalancesAfterUpdate();
                // Verify that the balances are incremented by amount value
                Assert.True(adminAccountPage.VerifyUpdatedDebitBalance(amount, productBalancesBeforeUpdate, productBalancesAfterUpdate));
                // Logout from admin.
                admincommonfunctions.UserMenuBtn();
                adminfunctions.AdminLogOut();
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(LogMessage.VerifyLedgerEntryDebitTestFailed, ex);
                throw;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(LogMessage.VerifyLedgerEntryDebitTestFailed, e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }

        [Fact]      //Admin_31
        public void VerifyShowDepositKeys()
        {
            try
            {
                string accountId;
                string toastMessage;
                string product;
                string accountProvider;
                string fiatCurrency;
                product = TestData.GetData("TCAdmin31_Product");
                accountProvider = TestData.GetData("TCAdmin31_AccountProvider");
                fiatCurrency = TestData.GetData("TCAdmin31_FiatCurrency");
                accountId = TestData.GetData("TCAdmin31_AccountID");
                AdminFunctions adminfunctions = new AdminFunctions(TestProgressLogger);
                AdminCommonFunctions admincommonfunctions = new AdminCommonFunctions(TestProgressLogger);
                AdminAccountsPage adminAccountPage = new AdminAccountsPage(TestProgressLogger);

                // Login in admin
                adminfunctions.AdminLogIn(TestProgressLogger, Const.ADMIN1);
                // This method is used to navigate to accounts page and select the accountId passed
                adminAccountPage.GetAccountDetailsByAccountId(accountId);
                // Submit Show Deposit Keys
                adminAccountPage.SubmitDepositKeys(product, accountProvider);
                // Verify Create DepositKey Toast success msg
                toastMessage = adminAccountPage.CreateDepositKeyToastMessage();
                Assert.Equal(Const.DepositKeySuccessMsg, toastMessage);
                TestProgressLogger.LogCheckPoint(LogMessage.VerifyToastMSG);
                // Verify No FIAT is listed in the product droplist
                Assert.True(adminAccountPage.VerifyFIATCurrencyIsNotPresent(fiatCurrency));
                // Verify multiple Deposit Key can be created
                Assert.True(adminAccountPage.VerifyMultipleDepositKey());
                // Verify Copy button functionality is working
                Assert.True(adminAccountPage.VerifyCopyDepositKey());
                // Logout from admin.
                admincommonfunctions.UserMenuBtn();
                adminfunctions.AdminLogOut();
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(LogMessage.VerifyShowDepositKeysTestFailed, ex);
                throw;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(LogMessage.VerifyShowDepositKeysTestFailed, e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }

        [Fact]      //Admin_33
        public void VerifyAccountBalances()
        {
            try
            {
                string accountId;
                string toastMessage;
                string product;
                string accountProvider;
                string fiatCurrency;
                product = TestData.GetData("TCAdmin31_Product");
                accountProvider = TestData.GetData("TCAdmin31_AccountProvider");
                fiatCurrency = TestData.GetData("TCAdmin31_FiatCurrency");
                accountId = TestData.GetData("TCAdmin31_AccountID");
                AdminFunctions adminfunctions = new AdminFunctions(TestProgressLogger);
                AdminCommonFunctions admincommonfunctions = new AdminCommonFunctions(TestProgressLogger);
                AdminAccountsPage adminAccountPage = new AdminAccountsPage(TestProgressLogger);

                // Login in admin
                adminfunctions.AdminLogIn(TestProgressLogger, Const.ADMIN1);
                // Preconditions: Submit Ledger Entry for an account and product combination
                // Navigate on Accounts page.
                admincommonfunctions.SelectAccountsMenu();
                admincommonfunctions.ClickOnAccountBalancesTab();
                adminAccountPage.VerifyAccountBalancesPagination();
                // Verify that filter by product functionality is working
                adminAccountPage.VerifyFilterByProduct("USD");
                // 

                // Logout from admin.
                admincommonfunctions.UserMenuBtn();
                adminfunctions.AdminLogOut();
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(LogMessage.VerifyShowDepositKeysTestFailed, ex);
                throw;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(LogMessage.VerifyShowDepositKeysTestFailed, e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }

       

    }
}
