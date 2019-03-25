using System;
using System.Collections.Generic;
using System.Threading;
using AlphaPoint_QA.Common;
using AlphaPoint_QA.Utils;
using OpenQA.Selenium;
using Xunit;

namespace AlphaPoint_QA.Pages
{
    public class WalletPage
    {
        public string AvailableBalanceDetailsPage { get; set; }
        public string HoldBalanceDetailsPage { get; set; }
        public string PendingDepositDetailsPage { get; set; }
        public string TotalBalanceDetailsPage { get; set; }
        public string AmountToBeDeducted { get; set; }

        By sendTab = By.XPath("//section[@class='send-receive__main-form']/header//div[contains(@class,'send')]//span[text()='Send']");
        By receiveTab = By.XPath("//section[@class='send-receive__main-form']/header//div[contains(@class,'send')]//span[text()='Receive']");
        By requestByEmailTab = By.CssSelector("section.receive > header > div:nth-of-type(2)");

        By externalWalletTab = By.CssSelector("section.send-form__send-to > header > div:nth-of-type(1)");
        By toEmailAddressTab = By.CssSelector("section.send-form__send-to > header > div:nth-of-type(2)");
        By comment = By.CssSelector("div.ap-input__input-box.send-address__input-box > input[name=Comment]");
        By externalAddress = By.CssSelector("div.ap-input__input-box.send-address__input-box > input[name=ExternalAddress]");
        By amountOfBitCoin = By.CssSelector("div.ap-input__input-box.send-form__input-box > input[name=Amount]");
        By amountOfBitCoinRequest = By.CssSelector("section.receive-form__amounts input[name=Amount]");
        By sendBitCoin = By.CssSelector("button[type=submit]");
        By recipientsEmailAddress = By.CssSelector("div.ap-input__input-box.send-form__input-box > input[name=ReceiverUsername]");
        By amountOfBitcoinToSend = By.CssSelector("div.ap-input__input-box.send-form__input-box > input[name=Amount]");
        By addNoteOfSend = By.CssSelector("textarea[name=Notes]");
        By emailAddressToRequestForm = By.CssSelector("input[name=ReceiverUsername]");
        By amountOfBitcoinToRequest = By.CssSelector("div.ap-input__input-box.receive-form__input-box input[name=Amount]");
        By addNoteOfRec = By.CssSelector("div.ap-input__input-box.receive-form__input-box > textarea");
        By copyLinkIcon = By.CssSelector("span.isvg.loaded.ap-icon.ap-icon--copy.receive-address__copy-icon.receive-address__copy-icon--copy");
        By addressLink = By.CssSelector("section.receive-address div:nth-of-type(2) > span.receive-address__address");
        By closeIcon = By.CssSelector("div.ap-sidepane__close-button.retail-sidepane-with-details__close-button > span");

        By btcAmount = By.CssSelector("span[data-test='BTC Amount']");
        By minerFees = By.CssSelector("span[data-test='Miner Fees']");
        By btcTotalAmount = By.CssSelector("div.ap-label-with-text.send-receive-confirm-modal__lwt-container span[data-test='BTC Total Amount']");
        By externalAdd = By.CssSelector("span[data-test='External Address']");
        By confirmButton = By.CssSelector("div.ap-modal__footer.send-receive-confirm-modal__footer > button");
        By sendIconOnDetailsPage = By.CssSelector("div[tooltip=Send]");
        By receiveIconOnDetailsPage = By.CssSelector("div[tooltip=Receive]");
        By holdBalanceOnDetailsPage = By.CssSelector("div.wallet-details > div:nth-of-type(2) > div:nth-of-type(2) > div:nth-of-type(2)");
        By pendingDepositOnDetailsPage = By.CssSelector("div.wallet-details > div:nth-of-type(2) > div:nth-of-type(3) > div:nth-of-type(2)");
        By availableBalanceOnDetailsPage = By.CssSelector("div.wallet-details > div:nth-of-type(2) > div:nth-of-type(1) > div:nth-of-type(2)");
        By totalBalanceOnDetailsPage = By.CssSelector("div.wallet-details > div:nth-of-type(2) > div:nth-of-type(4) > div:nth-of-type(2)");

        By statusIdOnDetailsPage = By.CssSelector("div.activity__status-id");
        By amountToSend = By.CssSelector("span[data-test='Amount to Send']");
        By yourCurrentBtcBalance = By.CssSelector("span[data-test='Your current BTC Balance']");
        By remainingBalance = By.CssSelector("span[data-test='Remaining Balance']");
        By btcAmountOnConfirm = By.CssSelector("span[data-test='BTC Amount']");
        By recipientsEmail = By.CssSelector("span[data-test='Recipient’s Email']");
        By requesteesEmail = By.CssSelector("span[data-test='Requestee’s Email']");
        By refreshTransfer = By.CssSelector("button.ap-inline-btn__btn.ap-inline-btn__btn--general.transfers__refresh-transfers__btn.transfers__refresh-transfers__btn--general");
        By sentRequestTab = By.CssSelector("label[data-test='Sent Requests']");
        By receivedTransfersTab = By.CssSelector("label[data-test='Received Transfers']");
        By approveButton = By.XPath("//div[@class='transfer-request-item__buttons']//button[text()='Approve']");
        By rejectButton = By.XPath("//div[@class='transfer-request-item__buttons']//button[text()='Reject']");

        By amountOfUsdToWithdraw = By.CssSelector("input[data-test='Amount of USD to Withdraw']");
        By fullName = By.CssSelector("input[data-test='Full Name']");
        By language = By.CssSelector("input[data-test=Language]");
        By withdrawComment = By.CssSelector("input[name=Comment]");
        By bankAddress = By.CssSelector("input[name=BankAddress]");
        By bankAccountNumber = By.CssSelector("input[name=BankAccountNumber]");
        By bankName = By.CssSelector("input[name=BankAccountName]");
        By swiftCode = By.CssSelector("input[name=SwiftCode]");
        By withdrawUSD = By.CssSelector("button[type=submit]");
        By withdrawButtonOnDetails = By.CssSelector("div[tooltip=Withdraw]");
        By depositButtonOnDetails = By.CssSelector("div[tooltip=Deposit]");

        By currentUsdBalance = By.CssSelector("span[data-test='Your current USD Balance']");
        By amountToWithdraw = By.CssSelector("span[data-test='Amount to Withdraw']");
        By fee = By.CssSelector("span[data-test='Fee']");
        By depositTicketID = By.CssSelector("div.activity__status > div:nth-of-type(1)");
        By OkButton = By.XPath("//button[text()='OK']");
        By fullNameOnDeposit = By.CssSelector("input[name=fullname]");
        By amountOnDeposit = By.CssSelector("input[name=amount]");
        By commentsOnDeposit = By.CssSelector("input[name=comments]");
        By placeDepositTicket = By.CssSelector("button[data-test='Place Deposit Ticket']");
        By fullNameOnConfirmaton = By.CssSelector("span[data-test='Full Name']");
        By amountToDepositOnConfirmaton = By.CssSelector("span[data-test='Amount to Deposit']");
        By noteOnConfirmaton = By.CssSelector("span[data-test=Note]");

        By amountToWithdrawOnConfirm = By.CssSelector("div.ap-label-with-text.fiat-withdraw-modal__lwt-container span[data-test='Amount to Withdraw']");
        By fullNameOnConfirm = By.CssSelector("div.ap-label-with-text.fiat-withdraw-modal__lwt-container span[data-test='Full Name']");
        By languageOnConfirm = By.CssSelector("div.ap-label-with-text.fiat-withdraw-modal__lwt-container span[data-test=Language]");
        By commentOnConfirm = By.CssSelector("div.ap-label-with-text.fiat-withdraw-modal__lwt-container span[data-test=Comment]");
        By bankAddressOnConfirm = By.CssSelector("div.ap-label-with-text.fiat-withdraw-modal__lwt-container span[data-test='Bank Address']");
        By bankAccountNumberOnConfirm = By.CssSelector("div.ap-label-with-text.fiat-withdraw-modal__lwt-container span[data-test='Bank Account Number']");
        By bankAccountNameOnConfirm = By.CssSelector("div.ap-label-with-text.fiat-withdraw-modal__lwt-container span[data-test='Bank Account Name']");
        By swiftCodeOnConfirm = By.CssSelector("div.ap-label-with-text.fiat-withdraw-modal__lwt-container span[data-test='Swift Code']");
        By feeOnConfirm = By.CssSelector("div.ap-label-with-text.fiat-withdraw-modal__lwt-container span[data-test=Fee]");
        By nextPagination = By.CssSelector("span.pagination__next.transfers-pagination__next > a:nth-of-type(2)");
        By withdrawUSDConfirmButton = By.XPath("//button[text()='Confirm']");

        //text
        By withdrawConfirmedMsg = By.CssSelector("div.standalone-modal__body-text");
        By goToExchangeButton = By.XPath("//a[text()='Go to Exchange']");

        // Click on send icon of instruments.
        public void ClickOnInstrumentSendButton(IWebDriver driver, string instrumentname)
        {
            try
            {
                Thread.Sleep(6000);
                IReadOnlyCollection<IWebElement> arr = driver.FindElements(By.XPath("//div[@class='wallet-card-grid']/div"));
                for (int i = 1; i <= arr.Count; i++)
                {
                    IWebElement div = driver.FindElement(By.XPath("//div[@class='wallet-card-grid']/div[" + i + "]/div//span"));
                    string instrument = div.Text;
                    if (instrument.Contains(instrumentname))
                    {
                        IWebElement sendicon = driver.FindElement(By.XPath("//div[@class='wallet-card-grid']/div[" + i + "]/div[3]/div[2]/div/span"));
                        UserSetFunctions.Click(sendicon);
                        break;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Get text of status id.
        public string GetStatusID(IWebDriver driver)
        {
            return driver.FindElement(statusIdOnDetailsPage).Text;
        }

        // Get text of current instrument balance.
        public string GetInstrumentCurrentBalance(IWebDriver driver, string instrumentname)
        {
            string currentbalance = null;
            try
            {
                Thread.Sleep(4000);
                IReadOnlyCollection<IWebElement> arr = driver.FindElements(By.XPath("//div[@class='wallet-card-grid']/div"));
                for (int i = 1; i <= arr.Count; i++)
                {
                    IWebElement div = GenericUtils.WaitForElementClickable(driver, By.XPath("//div[@class='wallet-card-grid']/div[" + i + "]/div//span"), 20);
                    string instrument = div.Text;
                    if (instrument.Contains(instrumentname))
                    {
                        IWebElement sendicon = GenericUtils.WaitForElementPresence(driver, By.XPath("//div[@class='wallet-card-grid']/div[" + i + "]/div[2]/div/div/span"), 20);
                        currentbalance = sendicon.Text;
                        break;
                    }
                }
                return currentbalance;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Click on details link under instruments.
        public void ClickInstrumentDetails(IWebDriver driver, string instrumentname)
        {
            try
            {
                Thread.Sleep(2000);
                IReadOnlyCollection<IWebElement> arr = driver.FindElements(By.XPath("//div[@class='wallet-card-grid']/div"));
                for (int i = 1; i <= arr.Count; i++)
                {
                    IWebElement div = GenericUtils.WaitForElementClickable(driver, By.XPath("//div[@class='wallet-card-grid']/div[" + i + "]/div//span"), 10);
                    string instrument = div.Text;
                    if (instrument.Contains(instrumentname))
                    {
                        IWebElement details = GenericUtils.WaitForElementClickable(driver, By.XPath("//div[@class='wallet-card-grid']/div[" + i + "]/div[3]/div/a"), 10);
                        GenericUtils.ActionClick(driver, details);
                        break;
                    }
                }
            }
            catch (StaleElementReferenceException)
            {
                Thread.Sleep(2000);
                IReadOnlyCollection<IWebElement> arr = driver.FindElements(By.XPath("//div[@class='wallet-card-grid']/div"));
                for (int i = 1; i <= arr.Count; i++)
                {
                    IWebElement div = GenericUtils.WaitForElementClickable(driver, By.XPath("//div[@class='wallet-card-grid']/div[" + i + "]/div//span"), 10);
                    string instrument = div.Text;
                    if (instrument.Contains(instrumentname))
                    {
                        IWebElement details = GenericUtils.WaitForElementClickable(driver, By.XPath("//div[@class='wallet-card-grid']/div[" + i + "]/div[3]/div/a"), 10);
                        GenericUtils.ActionClick(driver, details);
                        break;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        // Send bitcoin using external address.
        public void SendBitCoinExternalWallet(IWebDriver driver, string commenttext, string amtbitcoin)
        {
            try
            {
                driver.FindElement(comment).SendKeys(commenttext);
                driver.FindElement(externalAddress).SendKeys(OpenQA.Selenium.Keys.Control + "v");
                driver.FindElement(amountOfBitCoin).SendKeys(amtbitcoin);
                driver.FindElement(sendBitCoin).Click();
                Thread.Sleep(3000);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Send bitcoin using email address.
        public void SendBitCoinToEmailAddress(IWebDriver driver, string commenttext, string emailAddress, string amtbitcoin)
        {
            try
            {
                driver.FindElement(emailAddressToRequestForm).SendKeys(emailAddress);
                driver.FindElement(amountOfBitCoin).SendKeys(amtbitcoin);
                driver.FindElement(addNoteOfSend).SendKeys(commenttext);
                Thread.Sleep(1000);
            }
            catch (Exception)
            {
                throw;
            }
        }


        // Enter details for Withdraw USD amount
        public void WithdrawUSD(IWebDriver driver, string usd, string fullname, string languag, string comments, string bankaddres, string bankaccountNum, string bankname, string swiftcode)
        {
            try
            {
                driver.FindElement(amountOfUsdToWithdraw).SendKeys(usd);
                driver.FindElement(fullName).SendKeys(fullname);
                driver.FindElement(language).SendKeys(languag);
                driver.FindElement(withdrawComment).SendKeys(comments);
                driver.FindElement(bankAddress).SendKeys(bankaddres);
                driver.FindElement(bankAccountNumber).SendKeys(bankaccountNum);
                driver.FindElement(bankName).SendKeys(bankname);
                driver.FindElement(swiftCode).SendKeys(swiftcode);
                Thread.Sleep(1000);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Verify withdraw USD details on confirmation modal.
        public void VerifyWithdrawUSDOnConfirmationModal(IWebDriver driver, string usd, string fullname, string languag, string comments, string bankaddres, string bankaccountNum, string bankname, string swiftcode, string fee)
        {
            string amtToWithdraw;
            string actualFullName;
            string actualLanguage;
            string actualwithdrawcomment;
            string actualBankAddress;
            string actualBankAccountNumber;
            string actualBankName;
            string actualSwiftCode;
            string actualFee;
            try
            {
                amtToWithdraw = driver.FindElement(amountToWithdrawOnConfirm).Text.Split()[0];
                Assert.Equal(usd, amtToWithdraw);
                actualFullName = driver.FindElement(fullNameOnConfirm).Text;
                Assert.Equal(fullname, actualFullName);
                actualLanguage = driver.FindElement(languageOnConfirm).Text;
                Assert.Equal(languag, actualLanguage);
                actualwithdrawcomment = driver.FindElement(commentOnConfirm).Text;
                Assert.Equal(comments, actualwithdrawcomment);
                actualBankAddress = driver.FindElement(bankAddressOnConfirm).Text;
                Assert.Equal(bankaddres, actualBankAddress);
                actualBankAccountNumber = driver.FindElement(bankAccountNumberOnConfirm).Text;
                Assert.Equal(bankaccountNum, actualBankAccountNumber);
                actualBankName = driver.FindElement(bankAccountNameOnConfirm).Text;
                Assert.Equal(bankname, actualBankName);
                actualSwiftCode = driver.FindElement(swiftCodeOnConfirm).Text;
                Assert.Equal(swiftcode, actualSwiftCode);
                actualFee = driver.FindElement(feeOnConfirm).Text;
                Assert.Equal(Convert.ToDouble(fee).ToString(), Convert.ToDouble(actualFee).ToString());
                Thread.Sleep(1000);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Enter details for request bitcoin by email.
        public void SendBitCoinRequestByEmail(IWebDriver driver, string commenttext, string emailAddress, string amtbitcoin)
        {
            try
            {
                driver.FindElement(emailAddressToRequestForm).SendKeys(emailAddress);
                driver.FindElement(amountOfBitCoinRequest).SendKeys(amtbitcoin);
                driver.FindElement(addNoteOfSend).SendKeys(commenttext);
                Thread.Sleep(1000);
            }
            catch (Exception)
            {
                throw;
            }
        }


        // Get currentbalance, amounttosend and verify remaining balance.
        public void VerifySendDetailsBalances(IWebDriver driver)
        {
            string currentBalance;
            string amountToSend;
            string remaingBalance;
            string expectedRemaingBalance;
            try
            {
                currentBalance = driver.FindElement(yourCurrentBtcBalance).Text.Split(" ")[0];
                amountToSend = driver.FindElement(this.amountToSend).Text.Split(" ")[0];
                remaingBalance = driver.FindElement(remainingBalance).Text.Split(" ")[0];
                expectedRemaingBalance = GenericUtils.GetDifferenceFromStringAfterSubstraction(currentBalance, amountToSend);
                Assert.Equal(expectedRemaingBalance, remaingBalance.Replace(@",", string.Empty));
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Verify confirmation modal for requestees.
        public void VerifyConfirmationModal(IWebDriver driver, string email, string sendBtcAmount)
        {
            string btcAmount;
            string expectedEmail;
            string expectedBTC;
            try
            {
                btcAmount = driver.FindElement(btcAmountOnConfirm).Text.Split(" ")[0];
                expectedEmail = driver.FindElement(requesteesEmail).Text;
                Assert.Equal(expectedEmail, email);
                expectedBTC = GenericUtils.ConvertToDoubleFormat(GenericUtils.ConvertStringToDouble(sendBtcAmount));
                Assert.Equal(expectedBTC, btcAmount);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Verify confirmation modal for recipientsEmail.
        public void VerifyConfirmationModalForRecipients(IWebDriver driver, string email, string sendBtcAmount)
        {
            string btcAmount;
            string expectedEmail;
            string expectedBTC;
            try
            {
                btcAmount = driver.FindElement(btcAmountOnConfirm).Text.Split(" ")[0];
                expectedEmail = driver.FindElement(recipientsEmail).Text;
                Assert.Equal(expectedEmail, email);
                expectedBTC = GenericUtils.ConvertToDoubleFormat(GenericUtils.ConvertStringToDouble(sendBtcAmount));
                Assert.Equal(expectedBTC, btcAmount);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Enter details for Send USD deposit and click on place deposit button.
        public void SendUSDDeposit(IWebDriver driver, string fullname, string amount, string comments)
        {
            try
            {
                driver.FindElement(fullNameOnDeposit).SendKeys(fullname);
                driver.FindElement(amountOnDeposit).SendKeys(amount);
                driver.FindElement(commentsOnDeposit).SendKeys(comments);
                driver.FindElement(placeDepositTicket).Click();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Verify USD deposit details on confirmaion modal.
        public void VerifyUSDDepositOnConfirmationModal(IWebDriver driver, string fullname, string amount, string comments)
        {
            string actualFullName;
            string actualamt;
            string actualNote;
            try
            {
                actualFullName = driver.FindElement(fullNameOnConfirmaton).Text;
                Assert.Equal(fullname, actualFullName);
                actualamt = driver.FindElement(amountToDepositOnConfirmaton).Text;
                Assert.Equal(amount, actualamt);
                actualNote = driver.FindElement(noteOnConfirmaton).Text;
                Assert.Equal(comments, actualNote);
            }
            catch (Exception)
            {
                throw;
            }

        }

        // Get USD deposit ticket id.
        public string GetDepositUSDTicketID(IWebDriver driver)
        {
            try
            {
                string depositticktid = driver.FindElement(depositTicketID).Text;
                return depositticktid;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Get text of hold, pending, available and total balance.
        public void GetHoldAvailablePendingDepositTotalBalanceOnDetailsPage(IWebDriver driver)
        {
            try
            {
                Thread.Sleep(30000);
                HoldBalanceDetailsPage = driver.FindElement(holdBalanceOnDetailsPage).Text;
                if (HoldBalanceDetailsPage.Equals(Const.Dash))
                {
                    HoldBalanceDetailsPage = Const.ZeroValue;
                }
                PendingDepositDetailsPage = driver.FindElement(pendingDepositOnDetailsPage).Text;
                if (PendingDepositDetailsPage.Equals(Const.Dash))
                {
                    PendingDepositDetailsPage = Const.ZeroValue;
                }
                AvailableBalanceDetailsPage = driver.FindElement(availableBalanceOnDetailsPage).Text;
                TotalBalanceDetailsPage = driver.FindElement(totalBalanceOnDetailsPage).Text;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string UpdatedHoldBalance(IWebDriver driver, string expected)
        {
            string actualHold=null;
            for (int i = 1; i <= 40; i++)
            {
                Thread.Sleep(1000);
                actualHold = driver.FindElement(holdBalanceOnDetailsPage).Text;
                if (actualHold.Equals(expected))
                {
                    break;
                }
            }
            return actualHold;
        }
       
        // Click on Approve button.
        public void ClickApproveButton(IWebDriver driver)
        {
            try
            {
                IWebElement we = GenericUtils.WaitForElementClickable(driver, approveButton, 15);
                GenericUtils.ActionClick(driver, we);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Verify approve button is displayed or not.
        public bool VerifyApproveButton(IWebDriver driver)
        {
            bool val;
            try
            {
                Thread.Sleep(800);
                driver.FindElement(approveButton);
                val = true;
                return val;
            }
            catch (NoSuchElementException)
            {
                val = false;
                return val;
            }
        }

        // Verify reject button is displayed or not.
        public bool VerifyRejectButton(IWebDriver driver)
        {
            bool val;
            try
            {
                Thread.Sleep(1000);
                driver.FindElement(rejectButton);
                val = true;
                return val;
            }
            catch (NoSuchElementException)
            {
                val = false;
                return val;
            }
        }

        // Click on send bitcoin button.
        public void ClickOnSendBitCoin(IWebDriver driver)
        {
            driver.FindElement(sendBitCoin).Click();
        }

        // Click on confirm button.
        public void ClickConfirmButton(IWebDriver driver)
        {
            driver.FindElement(confirmButton).Click();
        }

        // Click on send button on wallet details page.
        public void ClickSendButtonOnDetailsPage(IWebDriver driver)
        {
            driver.FindElement(sendIconOnDetailsPage).Click();
        }

        // Click on receive button on wallet details page.
        public void ClickReceiveButtonOnDetailsPage(IWebDriver driver)
        {
            driver.FindElement(receiveIconOnDetailsPage).Click();
        }

        // Click on received transfer tab on wallet details page.
        public void ClickReceivedTransferOnDetailsPage(IWebDriver driver)
        {
            driver.FindElement(receivedTransfersTab).Click();
        }

        // Get text of BTC amount on confirmation modal.
        public string GetBtcAmountOnConfirmation(IWebDriver driver)
        {
            string btcAmtWithCurrency = driver.FindElement(btcAmount).Text;
            return btcAmtWithCurrency.Split()[0];
        }

        // Get text of miner fees on confirmation modal.
        public string GetMinerFeesOnConfirmation(IWebDriver driver)
        {
            string minerFeesWithCurrency = driver.FindElement(minerFees).Text;
            return minerFeesWithCurrency.Split()[0];
        }

        // Get text of BTC amount on confirmation modal.
        public string GetBtcTotalAmountOnConfirmation(IWebDriver driver)
        {
            string btcTotalAmountWithCurrency = driver.FindElement(btcTotalAmount).Text;
            return btcTotalAmountWithCurrency.Split()[0];
        }

        // Get text of external address on confirmation modal.
        public string GetExternalAddressOnConfirmation(IWebDriver driver)
        {
            string externalAddress = driver.FindElement(externalAdd).Text;
            return externalAddress;
        }

        // Get text of current USD balance.
        public string GetCurrentUSDBalance(IWebDriver driver)
        {
            string currentusdbalance = driver.FindElement(currentUsdBalance).Text.Split()[0];
            return currentusdbalance;
        }

        // Get text of fee from balance section.
        public string GetFee(IWebDriver driver)
        {
            string fees = driver.FindElement(fee).Text.Split()[0];
            return fees;
        }

        // Get text of remaining balance.
        public string GetRemainingBalance(IWebDriver driver)
        {
            string remainingbalance = driver.FindElement(remainingBalance).Text.Split()[0];
            return remainingbalance;
        }

        // Get text of amount to withdraw balance.
        public string GetAmountToWithdraw(IWebDriver driver)
        {
            string amounttowithdraw = driver.FindElement(amountToWithdraw).Text.Split()[0];
            return amounttowithdraw;
        }

        // Refresh Transfer section.
        public void ClickRefreshTransfers(IWebDriver driver)
        {
            IWebElement we = GenericUtils.WaitForElementPresence(driver, refreshTransfer, 15);
            we.Click();
        }

        // Verify amount in transfer section.
        public void VerifyAmountInTransferSection(IWebDriver driver, string userName, string receivedAmount)
        {
            string actualAmountFromUI;
            string usernameFromUI;
            string expectedRow;
            string actualRow = null;
            string receivedAmountInDouble;
            try
            {
                receivedAmountInDouble = GenericUtils.ConvertToDoubleFormat(GenericUtils.ConvertStringToDouble(receivedAmount));
                expectedRow = userName + " || " + receivedAmountInDouble;
                IWebElement pagination= driver.FindElement(nextPagination);
                if (pagination.Enabled)
                {
                    pagination.Click();
                }
                Thread.Sleep(6000);
                IReadOnlyCollection<IWebElement> arr = driver.FindElements(By.XPath("//div[@class='flex-table__body transfers__body']/div"));
                for (int i = 1; i <= arr.Count; i++)
                {
                    IWebElement div = driver.FindElement(By.XPath("//div[@class='flex-table__body transfers__body']/div[" + i + "]/div[1]/div/div[2]"));
                    usernameFromUI = div.Text.Split(" ")[1];
                    IWebElement amount = driver.FindElement(By.XPath("//div[@class='flex-table__body transfers__body']/div[" + i + "]/div[2]/div/div[1]"));
                    actualAmountFromUI = amount.Text;
                    actualRow = usernameFromUI + " || " + actualAmountFromUI;
                    if (actualRow.Equals(expectedRow))
                    {
                        break;
                    }
                }               
                Assert.Equal(expectedRow, actualRow);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Verify amount in sent request section.
        public void VerifyAmountInTransferSentRequestsSection(IWebDriver driver, string username, string recivedamount)
        {
            string actualrecivedamt = null;
            string expectedrecivedamount;
            try
            {
                Thread.Sleep(6000);
                IReadOnlyCollection<IWebElement> arr = driver.FindElements(By.XPath("//div[@class='flex-table__body transfers__body']/div"));
                for (int i = 1; i <= arr.Count; i++)
                {
                    IWebElement div = driver.FindElement(By.XPath("//div[@class='flex-table__body transfers__body']/div[" + i + "]/div[1]/div/div[2]"));
                    string instrument = div.Text;
                    if (instrument.Contains(username))
                    {
                        IWebElement amount = driver.FindElement(By.XPath("//div[@class='flex-table__body transfers__body']/div[" + i + "]/div[2]/div/div[1]"));
                        actualrecivedamt = amount.Text;
                        break;
                    }
                }
                expectedrecivedamount = GenericUtils.ConvertToDoubleFormat(GenericUtils.ConvertStringToDouble(recivedamount));
                Assert.Equal(expectedrecivedamount, actualrecivedamt);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Click on receive button of instruments.
        public void ClickOnInstrumentReceiveButton(IWebDriver driver, string instrumentname)
        {
            try
            {
                Thread.Sleep(6000);
                IReadOnlyCollection<IWebElement> arr = driver.FindElements(By.XPath("//div[@class='wallet-card-grid']/div"));
                for (int i = 1; i <= arr.Count; i++)
                {
                    IWebElement div = driver.FindElement(By.XPath("//div[@class='wallet-card-grid']/div[" + i + "]/div//span"));
                    string instrument = div.Text;
                    if (instrument.Contains(instrumentname))
                    {
                        IWebElement sendicon = driver.FindElement(By.XPath("//div[@class='wallet-card-grid']/div[" + i + "]/div[3]/div[2]/div[2]/span"));
                        UserSetFunctions.Click(sendicon);
                        break;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Click on Sent Requests Tab.
        public void SelectSentRequests(IWebDriver driver)
        {
            driver.FindElement(sentRequestTab).Click();
        }
        // Click on Withdraw USD Button.
        public void ClickOnWithdrawUSDButton(IWebDriver driver)
        {
            driver.FindElement(withdrawUSD).Click();
        }

        // Click on withdraw button on wallets details page.
        public void ClickWithdrawButtonOnDetails(IWebDriver driver)
        {
            driver.FindElement(withdrawButtonOnDetails).Click();
        }

        // Click on deposit button on wallets details page.
        public void ClickDepositButtonOnDetails(IWebDriver driver)
        {
            driver.FindElement(depositButtonOnDetails).Click();
        }

        // Click on confirm button for withdraw USD.
        public void ClickOnConfirmUSDModalButton(IWebDriver driver)
        {
            driver.FindElement(withdrawUSDConfirmButton).Click();
        }

        // Click on Email Address Tab.
        public void ClickOnEmailAddressTab(IWebDriver driver)
        {
            IWebElement we = GenericUtils.WaitForElementPresence(driver, toEmailAddressTab, 15);
            we.Click();
        }

        // Click on RequestByEmail tab.
        public void ClickOnReceiveRequestByEmail(IWebDriver driver)
        {
            IWebElement we = GenericUtils.WaitForElementPresence(driver, requestByEmailTab, 15);
            we.Click();
        }

        // Click on Received Transfers tab.
        public void SelectReceivedTransfers(IWebDriver driver)
        {
            driver.FindElement(receiveTab).Click();
        }

        // Click close icon.
        public void CloseSendOrReciveSection(IWebDriver driver)
        {
            driver.FindElement(closeIcon).Click();
        }

        // Click on external address link for copy address.
        public void CopyAddressToReceiveBTC(IWebDriver driver)
        {
            driver.FindElement(copyLinkIcon).Click();
        }

        // Get text of withdraw confirmed msg.
        public string GetWithdrawConfirmedMsg(IWebDriver driver)
        {
            IWebElement we = GenericUtils.WaitForElementPresence(driver, withdrawConfirmedMsg, 20);
            return we.Text;
        }
        // Click on GotoExchange button.
        public void ClickOnGoToExchange(IWebDriver driver)
        {
            driver.FindElement(goToExchangeButton).Click();
        }

    }
}
