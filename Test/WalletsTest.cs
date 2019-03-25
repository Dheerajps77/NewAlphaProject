using System;
using AlphaPoint_QA.Common;
using AlphaPoint_QA.Pages;
using AlphaPoint_QA.Utils;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;

namespace AlphaPoint_QA.Test
{

    [Collection("Alphapoint_QA_USER")]
    public class WalletsTest : TestBase
    {

        private string instrument;
        private string currencyName;
        private string comment;
        private string amountOfBtcToSend;
        private string withdrawStatus;
        private string user12_EmailAddress;
        private string user13_EmailAddress;

        private string amountOfUSDToWithdraw;
        private string fullName;
        private string language;
        private string bankAddress;
        private string bankAccountNumber;
        private string bankName;
        private string swiftCode;


        public WalletsTest(ITestOutputHelper output) : base(output)
        {

        }


        [Fact]
        public void TC36_SendExternalWallets()
        {
            try
            {
                string emailAddress;
                string gmailPassword;
                string successMsg;
                string holdBalance;
                string availableBalance;
                string btcAmount;
                string minerFees;
                string btcTotalaAmount;
                string withdrawSuccessMsg;
                string increasedHoldAmount;
                string incresedHoldBalance;
                string TotalBalance;
                string reducedAvailableBalance;
                string expectedReducedAvailableBalance;
                string statusID;
                string mailSubject;
                string withdrawSuccess;
                string acceptedticketStatus;
                string totalBalance;
                string expectedReducedHoldBalance;
                string expectedReducedTotalBalance;
                string linkUrl;
                string ticketStatusNew;

                instrument = TestData.GetData("Instrument");
                currencyName = TestData.GetData("CurrencyName");
                comment = TestData.GetData("Comment");
                amountOfBtcToSend = TestData.GetData("AmountOfBtcToSend");
                withdrawStatus = TestData.GetData("WithdrawStatus");
                emailAddress = TestData.GetData("User_14EmailAddress");
                gmailPassword = TestData.GetData("GmailUser_Test1Password");
                mailSubject = TestData.GetData("GmailMailSubject_ConfirmYourWithdraw");
                acceptedticketStatus = TestData.GetData("AcceptedTicketStatus");
                ticketStatusNew = TestData.GetData("TicketStatus");

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                WalletPage walletPage = new WalletPage();
                AdminFunctions adminFunctions = new AdminFunctions(TestProgressLogger);
                GmailCommonFunctions gmailObj = new GmailCommonFunctions();

                // Login and copy External Address of First user
                userFunctions.LogIn(TestProgressLogger, Const.USER12);
                UserCommonFunctions.DashBoardMenuButton(driver);
                // Navigate To Wallets Page.
                UserCommonFunctions.NavigateToWallets(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.NavigateWalletsPage));
                // Click on recive icon under BTC Section
                walletPage.ClickOnInstrumentReceiveButton(driver, currencyName);
                // Copy External Address.
                walletPage.CopyAddressToReceiveBTC(driver);
                successMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                // Verify copied success msg.
                Assert.Equal(Const.CopyAddressSuccessMsg, successMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.RecievedAddressCopied, Const.USER12));
                walletPage.CloseSendOrReciveSection(driver);

                // Login as Second User and send BTC to First User to the address copied
                // Verify Confirmation Modal and Balances.
                userFunctions.LogIn(TestProgressLogger, Const.USER14);
                UserCommonFunctions.DashBoardMenuButton(driver);
                // Navigate To Wallets Page.
                UserCommonFunctions.NavigateToWallets(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.NavigateWalletsPage));
                // Click on details link under BTC Section.
                walletPage.ClickInstrumentDetails(driver, currencyName);
                // Store Hold, Avalilable, Pending and Total Balance .
                walletPage.GetHoldAvailablePendingDepositTotalBalanceOnDetailsPage(driver);
                holdBalance = walletPage.HoldBalanceDetailsPage;
                availableBalance = walletPage.AvailableBalanceDetailsPage;
                // Click On Send Icon.
                walletPage.ClickSendButtonOnDetailsPage(driver);
                // Send BitCoin To First User.
                walletPage.SendBitCoinExternalWallet(driver, comment, amountOfBtcToSend);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.SendBitCoinSuccessfully, amountOfBtcToSend));  //0.1
                // Store BTC amount and miner fees from confirmation modal.
                btcAmount = walletPage.GetBtcAmountOnConfirmation(driver);
                minerFees = walletPage.GetMinerFeesOnConfirmation(driver);
                btcTotalaAmount = GenericUtils.GetSumFromStringAfterAddition(btcAmount, minerFees);
                // Click on Confirm button in confirmation modal.
                walletPage.ClickConfirmButton(driver);
                // Verify success msg after send bitcoin.
                withdrawSuccessMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                Assert.Equal(Const.WithdrawSuccessMsg, withdrawSuccessMsg);
                TestProgressLogger.LogCheckPoint(LogMessage.SuccessMassageVerified);
                increasedHoldAmount = GenericUtils.GetSumFromStringAfterAddition(holdBalance, btcTotalaAmount);
                // Get Hold, Avalilable, Pending and Total Balance after send bitcoin .
                walletPage.GetHoldAvailablePendingDepositTotalBalanceOnDetailsPage(driver);
                incresedHoldBalance = walletPage.HoldBalanceDetailsPage;
                TotalBalance = walletPage.TotalBalanceDetailsPage;
                reducedAvailableBalance = walletPage.AvailableBalanceDetailsPage;
                // Verify increased HoldAmount after send bitcoin.
                Assert.Equal(increasedHoldAmount, incresedHoldBalance);
                TestProgressLogger.LogCheckPoint(LogMessage.HoldAmountIncreasedSuccessfully);
                // Verify Reduced Available balance after send bitcoin.
                expectedReducedAvailableBalance = GenericUtils.GetDifferenceFromStringAfterSubstraction(availableBalance, btcTotalaAmount);
                Assert.Equal(expectedReducedAvailableBalance, GenericUtils.RemoveCommaFromString(reducedAvailableBalance));
                TestProgressLogger.LogCheckPoint(LogMessage.AvailableAmountReducedSuccessfully);
                // Get status id from recent activity
                statusID = walletPage.GetStatusID(driver);
                userFunctions.LogOut();

                // Login as Admin and verify Ticket Status
                adminFunctions.AdminLogIn(TestProgressLogger, Const.ADMIN1);
                AdminCommonFunctions admincommonfunctions = new AdminCommonFunctions(TestProgressLogger);
                admincommonfunctions.SelectTicketsMenu();
                // Verify withdraw status as "Pending2Fa".
                admincommonfunctions.VerifyStatus(driver, statusID, withdrawStatus);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CreatedTicketStatusVerified, statusID));
                admincommonfunctions.UserMenuBtn();
                adminFunctions.AdminLogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdminUserLogoutSuccessfully, Const.ADMIN1));

                // Login to Email using id of Second user and verify Withdraw Confirmed EMail
                linkUrl = gmailObj.Gmail(driver, emailAddress, gmailPassword, mailSubject);
                driver.Navigate().GoToUrl(linkUrl);
                withdrawSuccess = walletPage.GetWithdrawConfirmedMsg(driver);
                // Verify withdraw success msg on mail.
                Assert.Equal(LogMessage.WithdrawSuccessfullyConfirmMsg, withdrawSuccess);
                walletPage.ClickOnGoToExchange(driver);
                TestProgressLogger.LogCheckPoint(LogMessage.WithdrawConfirmedMassage);

                // Login as Admin and verify Ticket Status is New 
                adminFunctions.AdminLogIn(TestProgressLogger, Const.ADMIN1);
                admincommonfunctions.SelectTicketsMenu();
                // Verify Ticket status as New in Admin.
                admincommonfunctions.VerifyStatus(driver, statusID, ticketStatusNew);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedTicketStatus, ticketStatusNew));

                // Double click on created ticket.
                admincommonfunctions.DoubleClickOnCreatedDepositTicket(driver, statusID);
                // Click on Accept button.
                admincommonfunctions.ClickOnAcceptButtonFromDepositsTicketModal();
                // Verify ticket status as Accepted.
                admincommonfunctions.VerifyStatus(driver, statusID, acceptedticketStatus);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifiedTicketStatus, acceptedticketStatus));
                admincommonfunctions.UserMenuBtn();
                adminFunctions.AdminLogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdminUserLogoutSuccessfully, Const.ADMIN1));

                // Login as Second User and verify that the balances has been reduced
                userFunctions.LogIn(TestProgressLogger, Const.USER14);
                UserCommonFunctions.DashBoardMenuButton(driver);
                // Navigate on wallets page.
                UserCommonFunctions.NavigateToWallets(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.NavigateWalletsPage));     
                // Click on details link under BTC section.
                walletPage.ClickInstrumentDetails(driver, currencyName);
                // Get hold, available, pending and total balance.
                walletPage.GetHoldAvailablePendingDepositTotalBalanceOnDetailsPage(driver);
                holdBalance = walletPage.HoldBalanceDetailsPage;
                totalBalance = walletPage.TotalBalanceDetailsPage;
                // After accept ticket verify reduced hold balance by withdrawn amount.
                expectedReducedHoldBalance = GenericUtils.GetDifferenceFromStringAfterSubstraction(incresedHoldBalance, btcTotalaAmount);
                Assert.Equal(expectedReducedHoldBalance, GenericUtils.RemoveCommaFromString(holdBalance));
                TestProgressLogger.LogCheckPoint(LogMessage.HoldBalanceVerified);
                // After accept ticket verify reduced total balance by withdrawn amount.
                expectedReducedTotalBalance = GenericUtils.GetDifferenceFromStringAfterSubstraction(TotalBalance, btcTotalaAmount);
                Assert.Equal(expectedReducedTotalBalance, GenericUtils.RemoveCommaFromString(totalBalance));
                TestProgressLogger.LogCheckPoint(LogMessage.TotalBalanceVerified);
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(LogMessage.SendExternalWalletsTestFailed, ex);

                throw;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(LogMessage.SendExternalWalletsTestFailed, e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }


        [Fact]
        public void TC37_WalletsSendToEmailAddress()
        {
            try
            {
                string username;
                string secondUsername;
                string currentBalance;
                string withdrawSuccessMsg;
                string updatedCurrentBalance;
                string expectedupdateBalance;
                string availableBalance;
                string totalBalance;
                string expectedTotalBalanceAfterSent;
                string totalBalanceAfterSent;
                string availableBalanceAfterSent;
                string expectedAvailableBalanceAfterSent;
                string availableBalanceOfFirstUser;
                string totalBalanceOfFirstUser;

                currencyName = TestData.GetData("CurrencyName");
                comment = TestData.GetData("Comment");
                amountOfBtcToSend = TestData.GetData("AmountOfBtcToSend");
                withdrawStatus = TestData.GetData("WithdrawStatus");
                user12_EmailAddress = TestData.GetData("User_12EmailAddress");
                user13_EmailAddress = TestData.GetData("User_13EmailAddress");

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                WalletPage walletPage = new WalletPage();

                // Login as Receiver and store balances
                username = userFunctions.LogIn(TestProgressLogger, Const.USER12);
                UserCommonFunctions.DashBoardMenuButton(driver);
                // Navigate on wallets page.
                UserCommonFunctions.NavigateToWallets(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.NavigateWalletsPage));
                // Store current BTC balance of receiver.
                currentBalance = walletPage.GetInstrumentCurrentBalance(driver, currencyName);
                // Click on details link under BTC section.
                walletPage.ClickInstrumentDetails(driver, currencyName);
                // Store hold, available, pending and total balance of receiver.
                walletPage.GetHoldAvailablePendingDepositTotalBalanceOnDetailsPage(driver);
                availableBalanceOfFirstUser = walletPage.AvailableBalanceDetailsPage;
                totalBalanceOfFirstUser = walletPage.TotalBalanceDetailsPage;
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.StoreCurrentBalance, Const.USER12));

                // Login as Sender and send amount and verify balances.
                secondUsername = userFunctions.LogIn(TestProgressLogger, Const.USER13);
                UserCommonFunctions.DashBoardMenuButton(driver);
                // Navigate on wallets page.
                UserCommonFunctions.NavigateToWallets(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.NavigateWalletsPage));
                // Click on details link under BTC section.
                walletPage.ClickInstrumentDetails(driver, currencyName);
                // Store hold, available, pending and total balance before send amount.
                walletPage.GetHoldAvailablePendingDepositTotalBalanceOnDetailsPage(driver);
                availableBalance = walletPage.AvailableBalanceDetailsPage;
                totalBalance = walletPage.TotalBalanceDetailsPage;
                // Click on send button on details page.
                walletPage.ClickSendButtonOnDetailsPage(driver);
                // Click on ToEmailAddress tab.
                walletPage.ClickOnEmailAddressTab(driver);
                walletPage.SendBitCoinToEmailAddress(driver, comment, user12_EmailAddress, amountOfBtcToSend);
                // After enter details verify send details section right side of the page.
                walletPage.VerifySendDetailsBalances(driver);
                TestProgressLogger.LogCheckPoint(LogMessage.RemainingBalanceVerified);
                // Click on Send bitcoin button.
                walletPage.ClickOnSendBitCoin(driver);
                // Verify confirmation modal by given details.
                walletPage.VerifyConfirmationModalForRecipients(driver, user12_EmailAddress, amountOfBtcToSend);
                TestProgressLogger.LogCheckPoint(LogMessage.VerifiedDetailsOnConfirmModal);
                // Click on Confirm button.
                walletPage.ClickConfirmButton(driver);
                TestProgressLogger.LogCheckPoint(LogMessage.ConfirmationModalVerified);
                // Verify success msg in blue strip.
                withdrawSuccessMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                Assert.Equal(Const.TransferSuccessMsg, withdrawSuccessMsg);
                TestProgressLogger.LogCheckPoint(LogMessage.SuccessMassageVerified);
                walletPage.CloseSendOrReciveSection(driver);
                // Store hold, available, pending and total balance after send amount.
                walletPage.GetHoldAvailablePendingDepositTotalBalanceOnDetailsPage(driver);
                totalBalanceAfterSent = walletPage.TotalBalanceDetailsPage;
                expectedTotalBalanceAfterSent = GenericUtils.GetDifferenceFromStringAfterSubstraction(totalBalance, amountOfBtcToSend);
                // Verify Total Amount After Send -> amount should be reduced by the sent amount
                Assert.Equal(expectedTotalBalanceAfterSent, GenericUtils.RemoveCommaFromString(totalBalanceAfterSent));
                TestProgressLogger.LogCheckPoint(LogMessage.TotalBalanceVerified);
                availableBalanceAfterSent = walletPage.AvailableBalanceDetailsPage;
                // Verify available balance after send amount.
                expectedAvailableBalanceAfterSent = GenericUtils.GetDifferenceFromStringAfterSubstraction(availableBalance, amountOfBtcToSend);
                Assert.Equal(expectedAvailableBalanceAfterSent, GenericUtils.RemoveCommaFromString(availableBalanceAfterSent));
                TestProgressLogger.LogCheckPoint(LogMessage.RemainingBalanceVerified);
                walletPage.ClickRefreshTransfers(driver);
                // Verify send amount under sent transfer section.
                walletPage.VerifyAmountInTransferSection(driver, username, amountOfBtcToSend);
                TestProgressLogger.LogCheckPoint(LogMessage.VerifiedRequestUnderSentRequest);
                userFunctions.LogOut();

                // Login as Receiver and verify that balances is increased by the amount sent
                userFunctions.LogIn(TestProgressLogger, Const.USER12);
                UserCommonFunctions.DashBoardMenuButton(driver);
                // Navigate on wallets page.
                UserCommonFunctions.NavigateToWallets(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.NavigateWalletsPage));
                // Store upadated current balance of receiver.
                updatedCurrentBalance = walletPage.GetInstrumentCurrentBalance(driver, currencyName);
                // Verify increased BTC current balance by send amount.
                expectedupdateBalance = GenericUtils.GetSumFromStringAfterAddition(currentBalance, amountOfBtcToSend);
                Assert.Equal(expectedupdateBalance, GenericUtils.RemoveCommaFromString(updatedCurrentBalance));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BalanceUpdatedSuccessfully, Const.USER12));
                // Click on details link in BTC section.
                walletPage.ClickInstrumentDetails(driver, currencyName);
                // Get increased hold, available, pending and total balance of receiver.
                walletPage.GetHoldAvailablePendingDepositTotalBalanceOnDetailsPage(driver);
                totalBalanceAfterSent = walletPage.TotalBalanceDetailsPage;
                // Verify increased total balance of recivier.
                expectedTotalBalanceAfterSent = GenericUtils.GetSumFromStringAfterAddition(totalBalanceOfFirstUser, amountOfBtcToSend);
                Assert.Equal(expectedTotalBalanceAfterSent, GenericUtils.RemoveCommaFromString(totalBalanceAfterSent));
                TestProgressLogger.LogCheckPoint(LogMessage.TotalBalanceVerified);
                // Verify increased available balance of recivier.
                availableBalanceAfterSent = walletPage.AvailableBalanceDetailsPage;
                expectedAvailableBalanceAfterSent = GenericUtils.GetSumFromStringAfterAddition(availableBalanceOfFirstUser, amountOfBtcToSend);
                Assert.Equal(expectedAvailableBalanceAfterSent, GenericUtils.RemoveCommaFromString(availableBalanceAfterSent));
                TestProgressLogger.LogCheckPoint(LogMessage.RemainingBalanceVerified);
                walletPage.ClickReceivedTransferOnDetailsPage(driver);
                walletPage.ClickRefreshTransfers(driver);
                // Verify recivied amount in 'Received Transfer'.
                walletPage.VerifyAmountInTransferSection(driver, secondUsername, amountOfBtcToSend);
                TestProgressLogger.LogCheckPoint(LogMessage.VerifiedTransactionUnderReceivedTransfer);
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(LogMessage.WalletsSendToEmailAddressTestFailed, ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(LogMessage.WalletsSendToEmailAddressTestFailed, e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }

        [Fact]
        public void TC39_WalletsReceiveRequestbyEmail()
        {
            try
            {
                string username;
                string currentBalanceOfFirstUser;
                string currentBalanceOfSecondUser;
                string availablebalance;
                string totalbalance;
                string withdrawSuccessMsg;
                string updatedCurrentBalance;
                string expectedupdateBalance;
                string currentBalance;
                string expupdateBalance;
                string availablebalanceAfterApprove;


                instrument = TestData.GetData("Instrument");
                currencyName = TestData.GetData("CurrencyName");
                comment = TestData.GetData("Comment");
                amountOfBtcToSend = TestData.GetData("AmountOfBtcToSend");
                withdrawStatus = TestData.GetData("WithdrawStatus");
                user12_EmailAddress = TestData.GetData("User_12EmailAddress");
                user13_EmailAddress = TestData.GetData("User_13EmailAddress");

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                WalletPage walletPage = new WalletPage();

                // Login as first user and Store Current Balance.
                username = userFunctions.LogIn(TestProgressLogger, Const.USER12);
                UserCommonFunctions.DashBoardMenuButton(driver);
                // Navigates on wallets page.
                UserCommonFunctions.NavigateToWallets(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.NavigateWalletsPage));
                // Store Current Balance of first user.
                currentBalanceOfFirstUser = walletPage.GetInstrumentCurrentBalance(driver, currencyName);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.StoreCurrentBalance, Const.USER12));

                //  Login as second user -> Send BTC Request to other user and verify Balances remains the same
                userFunctions.LogIn(TestProgressLogger, Const.USER13);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.NavigateToWallets(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.NavigateWalletsPage));
                // Store current balance of second user.
                currentBalanceOfSecondUser = walletPage.GetInstrumentCurrentBalance(driver, currencyName);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.StoreCurrentBalance, Const.USER13));
                // Click on details link in BTC Section.
                walletPage.ClickInstrumentDetails(driver, currencyName);
                // Store hold, available, pending and total balance before send request.
                walletPage.GetHoldAvailablePendingDepositTotalBalanceOnDetailsPage(driver);
                availablebalance = walletPage.AvailableBalanceDetailsPage;
                totalbalance = walletPage.TotalBalanceDetailsPage;
                // Click on receive button on details page.
                walletPage.ClickReceiveButtonOnDetailsPage(driver);
                // Click on 'RequestByEmail' Tab.
                walletPage.ClickOnReceiveRequestByEmail(driver);
                // Send request to send bitcoin.
                walletPage.SendBitCoinRequestByEmail(driver, comment, user12_EmailAddress, amountOfBtcToSend);
                walletPage.ClickOnSendBitCoin(driver);
                // Verify Confirmation modal.
                walletPage.VerifyConfirmationModal(driver, user12_EmailAddress, amountOfBtcToSend);
                TestProgressLogger.LogCheckPoint(LogMessage.VerifiedDetailsOnConfirmModal);
                // Click on confirm button on confirmation modal.
                walletPage.ClickConfirmButton(driver);
                TestProgressLogger.LogCheckPoint(LogMessage.ConfirmationModalVerified);
                // Verify success msg in blue strip.
                withdrawSuccessMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                Assert.Equal(Const.RequestTransferSuccessMsg, withdrawSuccessMsg);
                TestProgressLogger.LogCheckPoint(LogMessage.SuccessMassageVerified);
                walletPage.CloseSendOrReciveSection(driver);
                // Get hold, available, pending and total balance after send request.
                walletPage.GetHoldAvailablePendingDepositTotalBalanceOnDetailsPage(driver);
                // Verify available balance after send request. It should be unchanged.
                Assert.Equal(availablebalance, walletPage.AvailableBalanceDetailsPage);
                TestProgressLogger.LogCheckPoint(LogMessage.VerifiedAvailableBalance);
                // Verify total balance after send request. It should be unchanged.
                Assert.Equal(totalbalance, walletPage.TotalBalanceDetailsPage);
                TestProgressLogger.LogCheckPoint(LogMessage.VerifiedTotalBalance);

                // Verify Amount Under Sent Requests Section
                walletPage.ClickRefreshTransfers(driver);
                walletPage.SelectSentRequests(driver);
                walletPage.VerifyAmountInTransferSentRequestsSection(driver, username, amountOfBtcToSend);
                TestProgressLogger.LogCheckPoint(LogMessage.VerifiedRequestUnderSentRequest);
                userFunctions.LogOut();

                //  Login as first user -> Accept Request and Verify Balances gets reduced by amount requested by first user
                userFunctions.LogIn(TestProgressLogger, Const.USER12);
                // Click on approve button for accept request.
                walletPage.ClickApproveButton(driver);
                // Verify success msg in blue strip.
                withdrawSuccessMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                Assert.Equal(Const.TransferApproved, withdrawSuccessMsg);
                TestProgressLogger.LogCheckPoint(LogMessage.SuccessMassageVerified);
                // After accept request verify approve and reject button should not displayed.
                Assert.False(walletPage.VerifyApproveButton(driver));
                Assert.False(walletPage.VerifyRejectButton(driver));
                TestProgressLogger.LogCheckPoint(LogMessage.VerifiedApproveAndRejectButton);
                UserCommonFunctions.DashBoardMenuButton(driver);
                // Navigate on wallets page.
                UserCommonFunctions.NavigateToWallets(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.NavigateWalletsPage));
                // Get current balance after accept request.
                updatedCurrentBalance = walletPage.GetInstrumentCurrentBalance(driver, currencyName);
                // Verify current balance after accept request.
                expectedupdateBalance = GenericUtils.GetDifferenceFromStringAfterSubstraction(currentBalanceOfFirstUser, amountOfBtcToSend);
                Assert.Equal(expectedupdateBalance, GenericUtils.RemoveCommaFromString(updatedCurrentBalance));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BalanceReducedSuccessfully, Const.USER12));
                userFunctions.LogOut();

                // Login as second user -> Verify Balances is Increased 
                userFunctions.LogIn(TestProgressLogger, Const.USER13);
                UserCommonFunctions.DashBoardMenuButton(driver);
                // Navigate on wallets page.
                UserCommonFunctions.NavigateToWallets(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.NavigateWalletsPage));
                // After accepted request verify increased current balance.
                currentBalance = walletPage.GetInstrumentCurrentBalance(driver, currencyName);
                expupdateBalance = GenericUtils.GetSumFromStringAfterAddition(currentBalanceOfSecondUser, amountOfBtcToSend);
                Assert.Equal(expupdateBalance, GenericUtils.RemoveCommaFromString(currentBalance));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.BalanceIncreasedSuccessfully, Const.USER13));
                // Click on detalis link and get balances.
                walletPage.ClickInstrumentDetails(driver, currencyName);
                walletPage.GetHoldAvailablePendingDepositTotalBalanceOnDetailsPage(driver);
                availablebalanceAfterApprove = walletPage.AvailableBalanceDetailsPage;
                // Verify increased available balance after accept request.
                expupdateBalance = GenericUtils.GetSumFromStringAfterAddition(availablebalance, amountOfBtcToSend);
                Assert.Equal(expupdateBalance, GenericUtils.RemoveCommaFromString(availablebalanceAfterApprove));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AvailableBalanceIncresedAfterApporve, Const.USER13));
                userFunctions.LogOut();
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(LogMessage.WalletsReceiveRequestByEmailTestFailed, ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(LogMessage.WalletsReceiveRequestByEmailTestFailed, e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }


        [Fact]
        public void TC40_WalletsWithdrawFiatcurrency()
        {
            try
            {
                string emailAddress;
                string gmailPassword;
                string amounttowithdraw;
                string currentUSDBalance;
                string fee;
                string remainingBalance;
                string amountToWithdrawAndFees;
                string expectedRemainingBalance;
                string holdBalance;
                string totalBalance;
                string availableBalance;
                string withdrawSuccessMsg;
                string holdBalanceAfterWithdraw;
                string availableBalanceAfterDeposit;
                string expectedAvailableBalanceAfterDeposit;
                string expectedHoldBalanceAfterDeposit;
                string statusID;
                string linkUrl;
                string ticketStatus;
                string withdrawSuccess;
                string mailSubject;
                string totalBalanceAfterDeposit;
                string expectedTotalBalanceAfterDeposit;

                currencyName = TestData.GetData("USDCurrency");
                comment = TestData.GetData("TC40_Comment");
                amountOfUSDToWithdraw = TestData.GetData("USDAmount");
                fullName = TestData.GetData("FullName");
                language = TestData.GetData("TC40_Language");
                bankAddress = TestData.GetData("TC40_BankAddress");
                bankAccountNumber = TestData.GetData("TC40_BankAccountNumber");
                bankName = TestData.GetData("TC40_BankName");
                swiftCode = TestData.GetData("TC40_SwiftCode");
                withdrawStatus = TestData.GetData("WithdrawStatus");
                emailAddress = TestData.GetData("User_14EmailAddress");
                gmailPassword = TestData.GetData("GmailUser_Test1Password");
                mailSubject = TestData.GetData("GmailMailSubject_ConfirmYourWithdraw");
                ticketStatus = TestData.GetData("FullyProcessedTicketStatus");

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                WalletPage walletPage = new WalletPage();
                AdminFunctions adminFunctions = new AdminFunctions(TestProgressLogger);
                GmailCommonFunctions gmailObj = new GmailCommonFunctions();

                // Login and Withdraw USD 
                // Verify Confirmation Modal, Available balance gets reduced and Hold balance increases
                userFunctions.LogIn(TestProgressLogger, Const.USER15);
                UserCommonFunctions.DashBoardMenuButton(driver);
                // Navigate on wallets page.
                UserCommonFunctions.NavigateToWallets(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.NavigateWalletsPage));
                // Click on details link under USD section.
                walletPage.ClickInstrumentDetails(driver, currencyName);

                // Store hold, available, pending and deposit balance before withdraw.
                walletPage.GetHoldAvailablePendingDepositTotalBalanceOnDetailsPage(driver);
                holdBalance = walletPage.HoldBalanceDetailsPage;
                availableBalance = walletPage.AvailableBalanceDetailsPage;

                // Click on withdraw button on details page.
                walletPage.ClickWithdrawButtonOnDetails(driver);
                // Enter details in withdraw fields.
                walletPage.WithdrawUSD(driver, amountOfUSDToWithdraw, fullName, language, comment, bankAddress, bankAccountNumber, bankName, swiftCode);
                // Get amount to withdraw, current usd balance, fee and remaining balance.
                amounttowithdraw = walletPage.GetAmountToWithdraw(driver);
                currentUSDBalance = walletPage.GetCurrentUSDBalance(driver);
                fee = walletPage.GetFee(driver);
                remainingBalance = walletPage.GetRemainingBalance(driver);
                amountToWithdrawAndFees = GenericUtils.GetSumFromStringAfterAddition(amounttowithdraw, fee);

                // After enter details verify remaining balance in right side of the page.
                expectedRemainingBalance = GenericUtils.GetDifferenceFromStringAfterSubstraction(currentUSDBalance, amountToWithdrawAndFees);
                Assert.Equal(expectedRemainingBalance, GenericUtils.RemoveCommaFromString(remainingBalance));
                TestProgressLogger.LogCheckPoint(LogMessage.RemainingBalanceVerifiedOnBalanceSection);
                // Click on WithdrawUSD button.
                walletPage.ClickOnWithdrawUSDButton(driver);
                // Verify confirmation modal by given details.
                walletPage.VerifyWithdrawUSDOnConfirmationModal(driver, amountOfUSDToWithdraw, fullName, language, comment, bankAddress, bankAccountNumber, bankName, swiftCode, fee);
                // Click on confirm button.
                walletPage.ClickOnConfirmUSDModalButton(driver);
                TestProgressLogger.LogCheckPoint(LogMessage.ConfirmationModalVerified);
                // Verify success msg in blue strip.
                withdrawSuccessMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                Assert.Equal(LogMessage.USDWithdrawSuccessMsg, withdrawSuccessMsg);
                TestProgressLogger.LogCheckPoint(LogMessage.SuccessMassageVerified);
                //  Get Hold, Available, Pending and TotalBalance after withdraw amount.
                walletPage.GetHoldAvailablePendingDepositTotalBalanceOnDetailsPage(driver);
                holdBalanceAfterWithdraw = walletPage.HoldBalanceDetailsPage;
                availableBalanceAfterDeposit = walletPage.AvailableBalanceDetailsPage;
                totalBalanceAfterDeposit = walletPage.TotalBalanceDetailsPage;
                // Verify reduced available balance after withdraw amount.
                expectedAvailableBalanceAfterDeposit = GenericUtils.GetDifferenceFromStringAfterSubstraction(availableBalance, amountToWithdrawAndFees);
                Assert.Equal(expectedAvailableBalanceAfterDeposit, GenericUtils.RemoveCommaFromString(availableBalanceAfterDeposit));
                TestProgressLogger.LogCheckPoint(LogMessage.RemainingBalanceVerified);
                // Verify increased hold balance after withdraw amount.
                expectedHoldBalanceAfterDeposit = GenericUtils.GetSumFromStringAfterAddition(holdBalance, amountToWithdrawAndFees);
                Assert.Equal(expectedHoldBalanceAfterDeposit, holdBalanceAfterWithdraw);
                TestProgressLogger.LogCheckPoint(LogMessage.HoldBalanceVerified);
                statusID = walletPage.GetStatusID(driver);

                // Login as Admin and verify Ticket Status is Pending2Fa 
                adminFunctions.AdminLogIn(TestProgressLogger, Const.ADMIN1);
                AdminCommonFunctions admincommonfunctions = new AdminCommonFunctions(TestProgressLogger);
                // Click on tickets.
                admincommonfunctions.SelectTicketsMenu();
                // Verify ticket status as 'Pending2Fa'
                admincommonfunctions.VerifyStatus(driver, statusID, withdrawStatus);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CreatedTicketStatusVerified, statusID));
                admincommonfunctions.UserMenuBtn();
                adminFunctions.AdminLogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdminUserLogoutSuccessfully, Const.ADMIN1));

                // Login to Email account and verify Withdraw Confirmed Message 
                linkUrl = gmailObj.Gmail(driver, emailAddress, gmailPassword, mailSubject);
                driver.Navigate().GoToUrl(linkUrl);
                withdrawSuccess = walletPage.GetWithdrawConfirmedMsg(driver);
                // Verify withdraw success msg using mail.
                Assert.Equal(LogMessage.WithdrawSuccessfullyConfirmMsg, withdrawSuccess);
                walletPage.ClickOnGoToExchange(driver);
                TestProgressLogger.LogCheckPoint(LogMessage.WithdrawConfirmedMassage);

                // Login as Admin and verify Ticket Status is FullyProcessed.
                adminFunctions.AdminLogIn(TestProgressLogger, Const.ADMIN1);
                admincommonfunctions.SelectTicketsMenu();
                // Verify ticket status after verify withdraw msg on mail.
                admincommonfunctions.VerifyStatus(driver, statusID, ticketStatus);
                TestProgressLogger.LogCheckPoint(LogMessage.VerifiedTicketStatus);
                admincommonfunctions.UserMenuBtn();
                adminFunctions.AdminLogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdminUserLogoutSuccessfully, Const.ADMIN1));

                // Login as user and verify Hold and Total Balance gets reduced
                userFunctions.LogIn(TestProgressLogger, Const.USER15);
                UserCommonFunctions.DashBoardMenuButton(driver);
                // Navigate on Wallets page.
                UserCommonFunctions.NavigateToWallets(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.NavigateWalletsPage));
                // Click on details link in USD section.
                walletPage.ClickInstrumentDetails(driver, currencyName);
                // Get hold, available, pending and total balance after accept ticket.
                walletPage.GetHoldAvailablePendingDepositTotalBalanceOnDetailsPage(driver);
                holdBalance = walletPage.HoldBalanceDetailsPage;
                totalBalance = walletPage.TotalBalanceDetailsPage;
                // Verify reduced total balance after accept the ticket.
                expectedTotalBalanceAfterDeposit = GenericUtils.GetDifferenceFromStringAfterSubstraction(totalBalanceAfterDeposit, amountToWithdrawAndFees);
                Assert.Equal(expectedTotalBalanceAfterDeposit, GenericUtils.RemoveCommaFromString(totalBalance));
                TestProgressLogger.LogCheckPoint(LogMessage.TotalBalanceVerified);
                // Verify reduced hold balance after accept the ticket.
                expectedHoldBalanceAfterDeposit = GenericUtils.GetDifferenceFromStringAfterSubstraction(holdBalanceAfterWithdraw, amountToWithdrawAndFees);
                Assert.Equal(expectedHoldBalanceAfterDeposit, holdBalance);
                TestProgressLogger.LogCheckPoint(LogMessage.HoldBalanceVerified);
                TestProgressLogger.LogCheckPoint(LogMessage.WalletsWithdrawFiatcurrencyTestPassed);
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(LogMessage.WalletsWithdrawFiatcurrencyTestFailed, ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(LogMessage.WalletsWithdrawFiatcurrencyTestFailed, e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }


        [Fact]
        public void TC41_WalletsDepositFiatcurrency()
        {
            try
            {
                string ticketStatus;
                string AcceptedticketStatus;
                string amount;
                string availableBalanceAfterDeposit;
                string availableBalanceAfterAccept;
                string totalBalance;
                string pendingBalance;
                string withdrawSuccessMsg;
                string pendingBalanceAfterDeposit;
                string expectedPendingBalanceAfterDeposit;
                string expectedPendingBalanceAfterAccept;
                string ticketID;
                string expectedAvailableBalanceAfterAccept;

                currencyName = TestData.GetData("USDCurrency");
                comment = TestData.GetData("TC41_Comment");
                amount = TestData.GetData("USDAmount");
                fullName = TestData.GetData("FullName");
                language = TestData.GetData("TC40_Language");
                bankAddress = TestData.GetData("TC40_BankAddress");
                bankAccountNumber = TestData.GetData("TC40_BankAccountNumber");
                bankName = TestData.GetData("TC40_BankName");
                swiftCode = TestData.GetData("TC40_SwiftCode");
                withdrawStatus = TestData.GetData("WithdrawStatus");
                ticketStatus = TestData.GetData("TicketStatus");
                AcceptedticketStatus = TestData.GetData("AcceptedTicketStatus");

                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                WalletPage walletPage = new WalletPage();
                AdminFunctions adminFunctions = new AdminFunctions(TestProgressLogger);

                // Login as User -> Deposit USD 
                // Verify Pending Amount is increased
                userFunctions.LogIn(TestProgressLogger, Const.USER15);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedInSuccessfully, Const.USER15));
                UserCommonFunctions.DashBoardMenuButton(driver);
                // Navigate on wallets page.
                UserCommonFunctions.NavigateToWallets(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.NavigateWalletsPage));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.StoreCurrentBalance, Const.USER15));
                // Click on details link in USD section.
                walletPage.ClickInstrumentDetails(driver, currencyName);
                // Store hold, available, pending and total balance before deposit.
                walletPage.GetHoldAvailablePendingDepositTotalBalanceOnDetailsPage(driver);
                pendingBalance = walletPage.PendingDepositDetailsPage;
                totalBalance = walletPage.TotalBalanceDetailsPage;
                walletPage.ClickDepositButtonOnDetails(driver);
                // Place deposit ticket.
                walletPage.SendUSDDeposit(driver, fullName, amount, comment);
                // Verify confirmation modal.
                walletPage.VerifyUSDDepositOnConfirmationModal(driver, fullName, amount, comment);
                walletPage.ClickOnConfirmUSDModalButton(driver);
                TestProgressLogger.LogCheckPoint(LogMessage.ConfirmationModalVerified);
                // Verify success msg after click confirm button.
                withdrawSuccessMsg = UserCommonFunctions.GetTextOfMessage(driver, TestProgressLogger);
                Assert.Equal(LogMessage.USDDepositSuccessMsg, withdrawSuccessMsg);
                TestProgressLogger.LogCheckPoint(LogMessage.SuccessMassageVerified);
                // Get ticket id from recent activity.
                ticketID = walletPage.GetDepositUSDTicketID(driver);
                // Refresh wallets page.
                GenericUtils.RefreshPage(driver);
                // Store hold, available, pending and total balance after deposit.
                walletPage.GetHoldAvailablePendingDepositTotalBalanceOnDetailsPage(driver);
                pendingBalanceAfterDeposit = walletPage.PendingDepositDetailsPage;
                availableBalanceAfterDeposit = walletPage.AvailableBalanceDetailsPage;
                // Verify increased pending balance after deposit.
                expectedPendingBalanceAfterDeposit = GenericUtils.GetSumFromStringAfterAddition(pendingBalance, amount);
                Assert.Equal(expectedPendingBalanceAfterDeposit, GenericUtils.RemoveCommaFromString(pendingBalanceAfterDeposit));
                TestProgressLogger.LogCheckPoint(LogMessage.PendingBalanceVerified);
                userFunctions.LogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedOutSuccessfully, Const.USER15));

                // Login as Admin and verify Deposit Ticket Status is New
                // Accept the ticket and verify that the status changes to Accepted
                adminFunctions.AdminLogIn(TestProgressLogger, Const.ADMIN1);
                AdminCommonFunctions admincommonfunctions = new AdminCommonFunctions(TestProgressLogger);
                admincommonfunctions.SelectTicketsMenu();
                // Navigate on deposit section.
                admincommonfunctions.NavigateToDepositTicketsTab();
                // Verify ticket status as new.
                admincommonfunctions.VerifyStatus(driver, ticketID, ticketStatus);
                TestProgressLogger.LogCheckPoint(LogMessage.VerifiedTicketStatusAsNew);
                // Double click on created deposit ticket.
                admincommonfunctions.DoubleClickOnCreatedDepositTicket(driver, ticketID);
                // Click on accept button.
                admincommonfunctions.ClickOnAcceptButtonFromDepositsTicketModal();
                // Verify ticket status as accepted.
                admincommonfunctions.VerifyStatus(driver, ticketID, AcceptedticketStatus);
                TestProgressLogger.LogCheckPoint(LogMessage.VerifiedTicketStatusAsAccepted);
                admincommonfunctions.UserMenuBtn();
                adminFunctions.AdminLogOut();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AdminUserLogoutSuccessfully, Const.ADMIN1));

                // Login as User and verify Available Balance is increased by deposit amount
                // Verify that the Pending balance is reduced by deposit amount
                userFunctions.LogIn(TestProgressLogger, Const.USER15);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.UserLoggedInSuccessfully, Const.USER15));
                UserCommonFunctions.DashBoardMenuButton(driver);
                // Navigate on Wallets page.
                UserCommonFunctions.NavigateToWallets(driver);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.NavigateWalletsPage));
                // Click on details link in USD section.
                walletPage.ClickInstrumentDetails(driver, currencyName);
                // Store hold, available, pending and total balance after accept ticket.
                walletPage.GetHoldAvailablePendingDepositTotalBalanceOnDetailsPage(driver);
                pendingBalance = walletPage.PendingDepositDetailsPage;
                availableBalanceAfterAccept = walletPage.AvailableBalanceDetailsPage;
                // Verify pending balance reduced after accept ticket.
                expectedPendingBalanceAfterAccept = GenericUtils.GetDifferenceFromStringAfterSubstraction(pendingBalanceAfterDeposit, amount);
                Assert.Equal(expectedPendingBalanceAfterAccept, GenericUtils.RemoveCommaFromString(pendingBalance));
                TestProgressLogger.LogCheckPoint(LogMessage.PendingBalanceVerified);
                // Verify available balance increased after accept ticket.
                expectedAvailableBalanceAfterAccept = GenericUtils.GetSumFromStringAfterAddition(availableBalanceAfterDeposit, amount);
                Assert.Equal(expectedAvailableBalanceAfterAccept, GenericUtils.RemoveCommaFromString(availableBalanceAfterAccept));
                TestProgressLogger.LogCheckPoint(LogMessage.AvailableBalanceVerified);
                TestProgressLogger.LogCheckPoint(LogMessage.WalletsDepositFiatcurrencyTestPassed);
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(LogMessage.WalletsDepositFiatcurrencyTestFailed, ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(LogMessage.WalletsDepositFiatcurrencyTestFailed, e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }
    }
}