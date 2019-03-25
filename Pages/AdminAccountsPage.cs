using AlphaPoint_QA.Common;
using AlphaPoint_QA.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Xunit;

namespace AlphaPoint_QA.Test
{
    class AdminAccountsPage
    {
        IWebDriver driver;
        ProgressLogger logger;

        public AdminAccountsPage(ProgressLogger logger)
        {
            this.logger = logger;
            driver = AlphaPointWebDriver.GetInstanceOfAlphaPointWebDriver();
        }

        By accountsTabMenuLink = By.CssSelector("a[href='#/accounts']");
        By refreshButton = By.CssSelector("button[id=RefreshAccountsTable] > div");
        By refreshButtonInUserAccountSection = By.CssSelector("button[id=RefreshAccountData] >div");
        By editAccountInformationLink = By.CssSelector("a[id=OpenEditAccountInformation]");
        By accountName = By.CssSelector("input[name=AccountName]");
        By accountType = By.CssSelector("select[name=AccountType]");
        By riskType = By.CssSelector("select[name=RiskType]");
        By verificationLevel = By.CssSelector("select[name=VerificationLevel]");
        By saveButton = By.CssSelector("button[id=SaveAccountForm] > div");
        By exportCSVButton = By.CssSelector("button[id=OpenAccountsExportMenu]");
        By downloadIcon = By.CssSelector("span[id=ExportAllAccounts]");
        By accountNameOnDetails = By.CssSelector("div.details_container tbody > tr:nth-of-type(2) > td:nth-of-type(2)");
        By viewAll = By.CssSelector("button[id=ViewAllAccounts]");
        By manualWithdrawButton = By.CssSelector("button[id=OpenManualWithdraw]");
        By productDropDown = By.CssSelector("select[name=ProductId]");
        By accountIDForFilter = By.CssSelector("input[name=AccountId]");

        By amount = By.CssSelector("input[name=Amount]");
        By fullName = By.CssSelector("input[name=FullName]");
        By language = By.CssSelector("input[name=Language]");
        By comment = By.CssSelector("input[name=Comment]");
        By bankAddress = By.CssSelector("input[name=BankAddress]");
        By bankAccountNumber = By.CssSelector("input[name=BankAccountNumber]");
        By bankAccountName = By.CssSelector("input[name=BankAccountName]");
        By swiftCode = By.CssSelector("input[name=SwiftCode]");
        By createWithdrawTicketButton = By.CssSelector("button[id=SubmitManualWithdrawForm] > div");
        By withdrawLinkUnderTicketsSection = By.XPath("//a[text()='Withdraws']");
        By accontBadgeText = By.CssSelector("li[class='account-badge']");
        By accontBadgeCrossIcon = By.CssSelector("li[class='account-badge'] > svg");
        By addNewBadgeButton = By.CssSelector("button#OpenAddNewBadge");
        By badgeTextField = By.CssSelector("input#Badge");
        By submitCreateAccountBadgeButton = By.CssSelector("button#SubmitCreateAccountBadge");
        By yesButtonOfDeactivateAccountBatdge = By.CssSelector("div[class=mm-popup__box__footer__right-space] >button");
        By priceInOpenOrderSection = By.XPath("(//section[@class='secondary_container half_container'])[1]/div[2]/div/div/div[2]/div/div//div[3]");
        By quantityInOpenOrderSection = By.XPath("(//section[@class='secondary_container half_container'])[1]/div[2]/div/div/div[2]/div/div//div[4]");
        By showAllInOpenOrderSection = By.CssSelector("a[id=ShowAllAccountOrders]");
        By priceInAccountOrderPage = By.CssSelector("div.ReactVirtualized__Table:nth-of-type(1) > div:nth-of-type(2) > div > div > div:nth-of-type(5)");
        By quantityInAccountOrderPage = By.CssSelector("div.ReactVirtualized__Table:nth-of-type(1) > div:nth-of-type(2) > div > div > div:nth-of-type(6)");
        By orderIDInAccountOrderPage = By.CssSelector("div.ReactVirtualized__Table:nth-of-type(1) > div:nth-of-type(2) > div > div > div:nth-of-type(1)");
        By showAllInTradesSection = By.CssSelector("a[id=ShowAllAccountTrades]");
        By showAllInOrderHistorySection = By.CssSelector("a[id=ShowAllAccountOrderHistory]");
        By showAllInAccountActivitySection = By.CssSelector("a[id=ShowAllAccountTransactions]");
        By exportToCSVTrades = By.CssSelector("button[id=ExportAccountTrades]");
        By exportToCSVOpenOrder = By.CssSelector("button[id=ExportAccountOrders]");
        By exportToCSVAccountActivity = By.CssSelector("button[id=ExportAccountTransactions]");
        By exportToCSVOrderHistory = By.CssSelector("button[id=ExportAccountOrderHistory]");
        By userAccountTab = By.XPath("//span[text()='Account 260']");
        By usdAmountBalances = By.CssSelector("div.ReactVirtualized__Grid__innerScrollContainer>div:nth-of-type(2)>div:nth-of-type(2)");
        By accountIDLabel = By.CssSelector("section.secondary_container > div:nth-of-type(2) > table:nth-of-type(1) > tbody > tr:nth-of-type(1) > td:nth-of-type(1)");
        By accountNameLabel = By.CssSelector("section.secondary_container > div:nth-of-type(2) > table:nth-of-type(1) > tbody > tr:nth-of-type(2) > td:nth-of-type(1)");
        By accountTypeLabel = By.CssSelector("section.secondary_container > div:nth-of-type(2) > table:nth-of-type(1) > tbody > tr:nth-of-type(3) > td:nth-of-type(1)");
        By riskTypeLabel = By.CssSelector("section.secondary_container > div:nth-of-type(2) > table:nth-of-type(1) > tbody > tr:nth-of-type(4) > td:nth-of-type(1)");
        By verificationLevelLabel = By.CssSelector("section.secondary_container > div:nth-of-type(2) > table:nth-of-type(1) > tbody > tr:nth-of-type(5) > td:nth-of-type(1)");
        By feeProductLabel = By.CssSelector("section.secondary_container > div:nth-of-type(2) > table:nth-of-type(2) > tbody > tr:nth-of-type(1) > td:nth-of-type(1)");
        By feeProductTypeLabel = By.CssSelector("section.secondary_container > div:nth-of-type(2) > table:nth-of-type(2) > tbody > tr:nth-of-type(2) > td:nth-of-type(1)");
        By accountBadgeLabel = By.CssSelector("section.secondary_container > div:nth-of-type(2) > table:nth-of-type(2) > tbody > tr:nth-of-type(3) > td:nth-of-type(1)");
        By productLabelUnderBalances = By.XPath("(//div[@class='ReactVirtualized__Table__headerRow'])[1]/div[1]/span");
        By amountLabelUnderBalances = By.XPath("(//div[@class='ReactVirtualized__Table__headerRow'])[1]/div[2]/span");
        By holdAmountLabelUnderBalances = By.XPath("(//div[@class='ReactVirtualized__Table__headerRow'])[1]/div[3]/span");
        By pendingDepositLabelUnderBalances = By.XPath("(//div[@class='ReactVirtualized__Table__headerRow'])[1]/div[4]/span");
        By pendingWithdrawLabelUnderBalances = By.XPath("(//div[@class='ReactVirtualized__Table__headerRow'])[1]/div[5]/span");
        By dailyDepositsLabelUnderBalances = By.XPath("(//div[@class='ReactVirtualized__Table__headerRow'])[1]/div[6]/span");
        By dailyWithdrawLabelUnderBalances = By.XPath("(//div[@class='ReactVirtualized__Table__headerRow'])[1]/div[7]/span");
        By monthlyWithdrawLabelUnderBalances = By.XPath("(//div[@class='ReactVirtualized__Table__headerRow'])[1]/div[8]/span");
        By viewAllAccountBalances = By.CssSelector("#ViewAllAccountsBalances");


        By submitLedgerEntry = By.CssSelector("button[id=OpenSubmitLedgerEntry]");
        By productDropDownSubmitAccountLedgerEntry = By.CssSelector("select[id=ProductId]");
        By creditAmountSubmitAccountLedgerEntry = By.CssSelector("input[name=CR_Amt]");
        By commentSubmitAccountLedgerEntry = By.CssSelector("textarea[name=Comment]");
        By sendButtonSubmitAccountLedgerEntry = By.CssSelector("button[id=SubmitLedgerEntry] > div");
        By ticketIDLabel = By.CssSelector("span[title='Ticket Id']");
        By firstTicketID = By.XPath("(//section[@class='secondary_container half_container'])[6]/div[2]/div/div/div[2]/div/div[1]/div[1]");
        By exportAllBalancesButton = By.CssSelector("button#ExportAccountPositions >div");
        By toastMsg = By.CssSelector("#app > div > div > div.main_container > div:nth-child(3) > div > div > span");

        By selectDepositKeyProduct = By.CssSelector("#ProductId");
        By selectDepositKeyAccountProvider = By.CssSelector("#ProviderId");
        By createNewKeyButton = By.CssSelector("#CreateNewDepositKey2, #CreateNewDepositKey");
        By submitDepositKeyButton = By.CssSelector("#OpenDepositKeys");
        By closeModal = By.CssSelector("#CloseModal");
        By ledgerEntryCreditAmount = By.CssSelector("#CR_Amt");
        By ledgerEntryDebitAmount = By.CssSelector("#DR_Amt");
        By ledgerEntryComment = By.CssSelector("textarea[name = Comment]");
        By submitLedgerToastMessage = By.CssSelector("div.main_container>div:nth-of-type(3)>div>div>span");
        By createDepositKeyToastMessage = By.CssSelector("div.main_container>div:nth-child(4)>div>div>span");
        By submitLedgerInvalidToastMessage = By.CssSelector("div.main_container>div:nth-child(4)>div>div>span");
        By ledgerEntrySend = By.CssSelector("#SubmitLedgerEntry");
        By depositKeys = By.CssSelector("div.deposit-keys>div");
        By ledgerEntryProduct = By.CssSelector("#ProductId");
        By productIdList = By.CssSelector("select#ProductId>option");
        By paginationNext = By.CssSelector("#PaginationNext");
        By paginationPrev = By.CssSelector("#PaginationPrev");
        



        public string ClickOnExportAllBalancesButton()
        {
             string toastMessage=null;
             IWebElement element = driver.FindElement(exportAllBalancesButton);
             UserSetFunctions.Click(element);
            IWebElement toastElement = GenericUtils.WaitForElementPresence(driver, toastMsg, 20);
            toastMessage = toastElement.Text;
            return toastMessage;
        }


        public void CreditAmountInSubmintLedgerEntryModal(string currencyName,string amountValue,string commentValue)
        {
            try
            {
                GenericUtils.SelectDropDownByText(driver, productDropDownSubmitAccountLedgerEntry, currencyName);
                IWebElement creditAmountElement= driver.FindElement(creditAmountSubmitAccountLedgerEntry);
                UserSetFunctions.EnterText(creditAmountElement, amountValue);
                IWebElement commentElement = driver.FindElement(commentSubmitAccountLedgerEntry);
                UserSetFunctions.EnterText(commentElement, commentValue);
                IWebElement sendButton = driver.FindElement(sendButtonSubmitAccountLedgerEntry);
                UserSetFunctions.Click(sendButton);
                Thread.Sleep(2000);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void ClickOnSubmitLedgerEntryButton()
        {
            try
            {
                driver.FindElement(submitLedgerEntry).Click();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ClickOnSubmitDepositKeyButton()
        {
            driver.FindElement(submitDepositKeyButton).Click();
        }

        public IWebElement AccountsTabMenuLink(IWebDriver driver)
        {
            return driver.FindElement(accountsTabMenuLink);
        }

        public IWebElement ShowAllInOpenOrderSection(IWebDriver driver)
        {
            return driver.FindElement(showAllInOpenOrderSection);
        }

        public IWebElement SubmitCreateAccountBadgeButton(IWebDriver driver)
        {
            return driver.FindElement(submitCreateAccountBadgeButton);
        }

        public IWebElement BadgeTextField(IWebDriver driver)
        {
            return driver.FindElement(badgeTextField);
        }

        public IWebElement OpenAddNewBadgeButton(IWebDriver driver)
        {
            return driver.FindElement(addNewBadgeButton);
        }

        public IWebElement AccountIdForFilter(IWebDriver driver)
        {
            return driver.FindElement(accountIDForFilter);
        }

        public IWebElement EditAccountInformationLink(IWebDriver driver)
        {
            return driver.FindElement(editAccountInformationLink);
        }

        public IWebElement YesButtonOnDeactivateAccountBatdge(IWebDriver driver)
        {
            return driver.FindElement(yesButtonOfDeactivateAccountBatdge);
        }

        public IWebElement ExportCSVButton(IWebDriver driver)
        {
            return driver.FindElement(exportCSVButton);
        }

        public IWebElement DownloadIcon(IWebDriver driver)
        {
            return driver.FindElement(downloadIcon);
        }

        public IWebElement WithdrawLinkUnderTicketsSection(IWebDriver driver)
        {
            return driver.FindElement(withdrawLinkUnderTicketsSection);
        }

        public void ClickOnUserAccountTab(IWebDriver driver)
        {
            driver.FindElement(userAccountTab).Click();
        }
        
        public void ClickOnPaginationNext()
        {
            driver.FindElement(paginationNext).Click();
        }

        public void ClickOnPaginationPrev()
        {
            driver.FindElement(paginationPrev).Click();
        }

        public string USDAmountBalancesText()
        {
            return driver.FindElement(usdAmountBalances).Text;
        }

        public void ClickOnSubmitLedgerEntry()
        {
            driver.FindElement(submitLedgerEntry).Click();
        }
        
        public void CloseModal()
        {
            driver.FindElement(closeModal).Click();
        }
        
        public void ClickOnViewAllAccountBalances()
        {
            driver.FindElement(viewAllAccountBalances).Click();
        }

        public IWebElement LedgerEntryCreditAmountField()
        {
            return driver.FindElement(ledgerEntryCreditAmount);
        }

        public IWebElement LedgerEntryDebitAmountField()
        {
            return driver.FindElement(ledgerEntryDebitAmount);
        }

        public IWebElement LedgerEntryCommentField()
        {
            return driver.FindElement(ledgerEntryComment);
        }

        public string SubmitLedgerToastMessage()
        {
            IWebElement element = GenericUtils.WaitForElementPresence(driver, submitLedgerToastMessage, 15);
            return element.Text;
        }
        
        public string CreateDepositKeyToastMessage()
        {
            IWebElement element = GenericUtils.WaitForElementPresence(driver, createDepositKeyToastMessage, 15);
            return element.Text;
        }

        public string SubmitLedgerInvalidToastMessage()
        {
            IWebElement element = GenericUtils.WaitForElementPresence(driver, submitLedgerInvalidToastMessage, 15);
            return element.Text;
        }

        public void ClickOnLedgerEntrySendbutton()
        {
            driver.FindElement(ledgerEntrySend).Click();
        }

        public void SelectAccountLink(IWebDriver driver)
        {
            try
            {
                IWebElement element= AccountsTabMenuLink(driver);
                GenericUtils.Js_Click(driver, element);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string DoubleClickOnAccountName(IWebDriver driver, string accountId)
        {
            string actualAccountId = null;
            string actualAccountName = null;
            IWebElement we;
            Actions actions = new Actions(driver);
            try
            {
                IReadOnlyCollection<IWebElement> accounts = driver.FindElements(By.CssSelector("div.ReactVirtualized__Grid.ReactVirtualized__Table__Grid > div div"));
                int counts = accounts.Count;
                for (int i = 1; i <= counts - 1; i++)
                {
                    we = driver.FindElement(By.XPath("//div[@class='ReactVirtualized__Grid ReactVirtualized__Table__Grid']/div/div[" + i + "]/div"));
                    actualAccountId = we.Text;
                    if (actualAccountId.Equals(accountId))
                    {
                        IWebElement accountname = driver.FindElement(By.XPath("//div[@class='ReactVirtualized__Grid ReactVirtualized__Table__Grid']/div/div[" + i + "]/div[2]"));
                        actualAccountName = accountname.Text;
                        GenericUtils.DoubleClick(driver, accountname);
                        break;
                    }
                }
                return actualAccountName;
            }
            catch (Exception)
            {
                throw;
            }
        }


        // Enter account id for search.
        public void SearchByAccountID(IWebDriver driver,string accountId)
        {
            try
            {
                UserSetFunctions.EnterText(AccountIdForFilter(driver), accountId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Click on Edit Account Information link.
        public void EditAccountInformation(IWebDriver driver)
        {
            try
            {
                UserSetFunctions.Click(EditAccountInformationLink(driver));
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Click on yes button in Deactivate Account Batdge modal.
        public void ClickOnYesButton(IWebDriver driver)
        {
            try
            {
                UserSetFunctions.Click(YesButtonOnDeactivateAccountBatdge(driver));
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Click on Export CSV Button and download icon.
   
        public void ClickOnExportCSVOnAccountsPage()
        {
            try { 
            UserSetFunctions.Click(ExportCSVButton(driver));
            UserSetFunctions.Click(DownloadIcon(driver));
            Thread.Sleep(3000);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Click on withdraw link under ticket section.
        public void WithdrawLinkUnderTicket(IWebDriver driver)
        {
            try
            {
                UserSetFunctions.Click(WithdrawLinkUnderTicketsSection(driver));
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Edit account name in edit account information modal.
        public string EditAccountName(IWebDriver driver)
        {
            string accountNameValue;
            string randomValue;
            string updatedAccountName;
            try
            {
                IWebElement accname = driver.FindElement(accountName);
                accountNameValue = accname.GetAttribute("value");
                randomValue = GenericUtils.RandomString(2);
                updatedAccountName = accountNameValue + randomValue;
                accname.Clear();
                accname.SendKeys(updatedAccountName);
                return updatedAccountName;
            }
            catch (Exception)
            {
                throw;
            }
        }


        // Change account type in edit account information modal.
        public void ChangeAccountType(IWebDriver driver)
        {
            try
            {
                IWebElement accounttype = driver.FindElement(accountType);
                SelectElement selectobj = new SelectElement(accounttype);
                string selectedopton = selectobj.SelectedOption.Text;
            }
            catch (Exception)
            {
                throw;
            }
        }
        // Change risk type in edit account information modal.
        public void ChangeRiskType(IWebDriver driver)
        {
            try
            {
                IWebElement risktype = driver.FindElement(riskType);
                SelectElement selectobj = new SelectElement(risktype);
                string selectedopton = selectobj.SelectedOption.Text;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Change verification level in edit account information modal.
        public void ChangeVerificationLevel(IWebDriver driver)
        {
            try
            {
                IWebElement verificationlevel = driver.FindElement(verificationLevel);
                SelectElement selectobj = new SelectElement(verificationlevel);
                string selectedopton = selectobj.SelectedOption.Text;

            }
            catch (Exception)
            {
                throw;
            }
        }

        // click on save button in edit account information modal.
        public void ClickOnSaveButton(IWebDriver driver)
        {
            try
            {
                driver.FindElement(saveButton).Click();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Click on refresh button in particular user account.
        public void ClickOnRefreshInUserAccountSection(IWebDriver driver)
        {
            try
            {
                driver.FindElement(refreshButtonInUserAccountSection).Click();
                Thread.Sleep(2000);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Get text of accont name in account details page.
        public string GetAccountNameOnAccountDetails(IWebDriver driver)
        {
            try
            {
                return driver.FindElement(accountNameOnDetails).Text;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Click on ViewAll button if it is present.
        public void ClickOnViewAll(IWebDriver driver)
        {
            try
            {
                Thread.Sleep(1000);
                IWebElement viewall = driver.FindElement(viewAll);
                bool val = GenericUtils.VerifyPresentWebElement(viewall);
                if (val == true)
                {
                    viewall.Click();
                    Thread.Sleep(2000);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ClickOnManualWithdrawButton(IWebDriver driver)
        {
            try
            {
                IWebElement we= GenericUtils.WaitForElementPresence(driver, manualWithdrawButton, 15);
                UserSetFunctions.Click(we);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ManualWithdrawUSD(IWebDriver driver, string product, string amountValue, string fullNameValue, string languageValue, string commentValue, string bankAddressValue, string bankAccountNumValue, string bankAccountNameValue, string swiftCodeValue)
        {
            try
            {
                GenericUtils.SelectDropDownByText(driver, productDropDown, product);
                UserSetFunctions.EnterText(driver.FindElement(amount), amountValue);
                UserSetFunctions.EnterText(driver.FindElement(fullName), fullNameValue);
                UserSetFunctions.EnterText(driver.FindElement(language), languageValue);
                UserSetFunctions.EnterText(driver.FindElement(comment), commentValue);
                UserSetFunctions.EnterText(driver.FindElement(bankAddress), bankAddressValue);
                UserSetFunctions.EnterText(driver.FindElement(bankAccountNumber), bankAccountNumValue);
                UserSetFunctions.EnterText(driver.FindElement(bankAccountName), bankAccountNameValue);
                UserSetFunctions.EnterText(driver.FindElement(swiftCode), swiftCodeValue);
                UserSetFunctions.Click(driver.FindElement(createWithdrawTicketButton));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Dictionary<string, string> GetBalances(IWebDriver driver, string productName)
        {
            Dictionary<string, string> balancesData=null;
            try
            {
                
                Thread.Sleep(5000);
                IReadOnlyCollection<IWebElement> arr = driver.FindElements(By.XPath("(//section[@class='secondary_container'])[2]/div[2]/div/div/div[2]/div/div"));
                int arrcnt = arr.Count;
                for (int i = 1; i <= arrcnt - 1; i++)
                {
                    string product = driver.FindElement(By.XPath("(//section[@class='secondary_container'])[2]/div[2]/div/div/div[2]/div/div[" + i + "]/div[1]")).Text;
                    if (product.Equals(productName))
                    {
                        balancesData = new Dictionary<string, string>();
                        balancesData.Add("Amount", driver.FindElement(By.XPath("(//section[@class='secondary_container'])[2]/div[2]/div/div/div[2]/div/div[" + i + "]/div[2]")).Text);
                        balancesData.Add("Hold Amount", driver.FindElement(By.XPath("(//section[@class='secondary_container'])[2]/div[2]/div/div/div[2]/div/div[" + i + "]/div[3]")).Text);
                        balancesData.Add("Pending Deposits", driver.FindElement(By.XPath("(//section[@class='secondary_container'])[2]/div[2]/div/div/div[2]/div/div[" + i + "]/div[4]")).Text);
                        balancesData.Add("Pending Withdraws", driver.FindElement(By.XPath("(//section[@class='secondary_container'])[2]/div[2]/div/div/div[2]/div/div[" + i + "]/div[5]")).Text);
                        balancesData.Add("Daily Deposits", driver.FindElement(By.XPath("(//section[@class='secondary_container'])[2]/div[2]/div/div/div[2]/div/div[" + i + "]/div[6]")).Text);
                        balancesData.Add("Daily Withdraws", driver.FindElement(By.XPath("(//section[@class='secondary_container'])[2]/div[2]/div/div/div[2]/div/div[" + i + "]/div[7]")).Text);
                        balancesData.Add("Monthly Withdraws", driver.FindElement(By.XPath("(//section[@class='secondary_container'])[2]/div[2]/div/div/div[2]/div/div[" + i + "]/div[8]")).Text);
                        break;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return balancesData;
        }


        // Get Ticket id from ticket-Withdraw section.
        public string GetRecentTicketID(IWebDriver driver,string assetValue,string amountValue)
        {
            string asset;
            string amount;
            string ticketID=null;
            GenericUtils.ScrollDown(driver);
            try
            {
                IReadOnlyCollection<IWebElement> arr = driver.FindElements(By.XPath("(//section[@class='secondary_container half_container'])[6]/div[2]/div/div/div[2]/div/div"));
                int count=arr.Count;
                for (int i=1;i<=count;i++)
                {
                    asset = driver.FindElement(By.XPath("(//section[@class='secondary_container half_container'])[6]/div[2]/div/div/div[2]/div/div["+i+"]/div[2]")).Text;
                    amount = driver.FindElement(By.XPath("(//section[@class='secondary_container half_container'])[6]/div[2]/div/div/div[2]/div/div[" + i + "]/div[3]")).Text;
                    if (asset.Equals(assetValue)  && amount.Equals(amountValue))
                    {
                        ticketID = driver.FindElement(By.XPath("(//section[@class='secondary_container half_container'])[6]/div[2]/div/div/div[2]/div/div[" + i + "]/div[1]")).Text;
                        break;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ticketID;
        }

        // Get Ticket ids in list from ticket-Withdraw section.
        public List<string> GetTicketIDs()
        {
            List<string> list=new List<string>();
            string ticketID = null;
            GenericUtils.ScrollDown(driver);
            try
            {
                IReadOnlyCollection<IWebElement> arr = driver.FindElements(By.XPath("(//section[@class='secondary_container half_container'])[6]/div[2]/div/div/div[2]/div/div"));
                int count = arr.Count;
                for (int i = 1; i <= count-1; i++)
                {
                     ticketID = driver.FindElement(By.XPath("(//section[@class='secondary_container half_container'])[6]/div[2]/div/div/div[2]/div/div[" + i + "]/div[1]")).Text;
                    list.Add(ticketID);
                }
                return list;
            }
            catch (Exception)
            {
                throw;
            }
           
        }

        // Verify ticketid in decending order.
        public bool VerifyTicketsIdInDecendingOrder()
        {
            bool val = false;
            string firstTicketIdsText;
            string secondTicketIdsText;
            int firstTicketId;
            int secondTicketId;
            int next;
            GenericUtils.ScrollDown(driver);
                for (int i = 1; i <= 5; i++)
                {
                    firstTicketIdsText=driver.FindElement(By.XPath("(//section[@class='secondary_container half_container'])[6]/div[2]/div/div/div[2]/div/div[" + i + "]/div[1]")).Text;
                    firstTicketId = Int32.Parse(firstTicketIdsText);
                    next = i + 1;
                    secondTicketIdsText = driver.FindElement(By.XPath("(//section[@class='secondary_container half_container'])[6]/div[2]/div/div/div[2]/div/div[" + next + "]/div[1]")).Text;
                    secondTicketId = Int32.Parse(secondTicketIdsText);
                    Assert.True(firstTicketId > secondTicketId);
                val = true;
                }
            return val;
        }

        // Verify ticketId in ascending order.
        public bool VerifyTicketsIdInAscendingOrder()
        {
            bool val = false;
            string firstTicketIdsText;
            string secondTicketIdsText;
            int firstTicketId;
            int secondTicketId;
            int next;
            GenericUtils.ScrollDown(driver);
            ClickOnTicketIdLabel();
            for (int i = 1; i <= 5; i++)
            {
                firstTicketIdsText = driver.FindElement(By.XPath("(//section[@class='secondary_container half_container'])[6]/div[2]/div/div/div[2]/div/div[" + i + "]/div[1]")).Text;
                firstTicketId = Int32.Parse(firstTicketIdsText);
                next = i + 1;
                secondTicketIdsText = driver.FindElement(By.XPath("(//section[@class='secondary_container half_container'])[6]/div[2]/div/div/div[2]/div/div[" + next + "]/div[1]")).Text;
                secondTicketId = Int32.Parse(secondTicketIdsText);
                Assert.True(firstTicketId < secondTicketId);
                val = true;
            }
            return val;
        }
        
        public void ClickOnTicketIdLabel()
        {
            driver.FindElement(ticketIDLabel).Click();
        }

        // Delete account badges.
        public void DeleteAccountBadge(IWebDriver driver)
        {
            try
            {
                GenericUtils.HoverElement(driver, accontBadgeText);
                UserSetFunctions.Click(driver.FindElement(accontBadgeCrossIcon));
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Get text of Account Badges.
        public string GetTextOfAccountBadges(IWebDriver driver)
        {
            try
            {
                return driver.FindElement(accontBadgeText).Text;
            }
            catch (Exception)
            {
                throw;
            }
        }
       
        // Add new badge in account section.
        public void AddNewBadge(IWebDriver driver,string badgeNumber)
        {
            try
            {
                UserSetFunctions.Click(OpenAddNewBadgeButton(driver));
                UserSetFunctions.EnterText(BadgeTextField(driver), badgeNumber);
                Thread.Sleep(2000);
                UserSetFunctions.Click(SubmitCreateAccountBadgeButton(driver));
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Get price and quantity in open orders page.
        public Dictionary<string, string> GetOpenOrdersInAccountsPage(IWebDriver driver)
        {
            Dictionary<string, string> orderData = null;
            try
            {
                orderData = new Dictionary<string, string>();
                orderData.Add("Price", driver.FindElement(priceInOpenOrderSection).Text);
                orderData.Add("Quantity", driver.FindElement(quantityInOpenOrderSection).Text);
            }
            catch (Exception)
            {
                throw;
            }
            return orderData;
        }

        // Click on ShowAll link under Open Order Section.
        public void ClickShowAllUnderOpenOrderSection(IWebDriver driver)
        {
            try
            {
                Thread.Sleep(2000);
                UserSetFunctions.Click(ShowAllInOpenOrderSection(driver));
               
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Click on ShowAll link Under Order History Section.
        public void ClickShowAllUnderOrderHistorySection(IWebDriver driver)
        {
            try
            {
                IWebElement element= driver.FindElement(showAllInOrderHistorySection);
                UserSetFunctions.Click(element);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Click on ShowAll link Under Account Activity Section.
        public void ClickShowAllUnderAccountActivitySection(IWebDriver driver)
        {
            try
            {
                IWebElement element = driver.FindElement(showAllInAccountActivitySection);
                UserSetFunctions.Click(element);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Click on ShowAll link Under Trade Section.
        public void ClickShowAllUnderTradeSection(IWebDriver driver)
        {
            try
            {
                IWebElement element = driver.FindElement(showAllInTradesSection);
                UserSetFunctions.Click(element);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Click on  ExportToCSV button for Trade.
        public void ClickExportToCSVTrade(IWebDriver driver)
        {
            try
            {
                IWebElement element = driver.FindElement(exportToCSVTrades);
                UserSetFunctions.Click(element);
                Thread.Sleep(3000);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Click on  ExportToCSV button for open order.
        public void ClickExportToCSVOpenOrder(IWebDriver driver)
        {
            try
            {
                IWebElement element = driver.FindElement(exportToCSVOpenOrder);
                UserSetFunctions.Click(element);
                Thread.Sleep(3000);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Click on  ExportToCSV button for Account Activity.
        public void ClickExportToCSVAccountActivity(IWebDriver driver)
        {
            try
            {
                IWebElement element = driver.FindElement(exportToCSVAccountActivity);
                UserSetFunctions.Click(element);
                Thread.Sleep(3000);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Click on  ExportToCSV button for order history.
        public void ClickExportToCSVOrderHistory(IWebDriver driver)
        {
            try
            {
                IWebElement element = driver.FindElement(exportToCSVOrderHistory);
                UserSetFunctions.Click(element);
                Thread.Sleep(3000);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Get text of orderid in open order page after place buy limit order.
        public string  GetOrderIDInAccountOrderPage(IWebDriver driver)
        {
            try
            {
               return driver.FindElement(orderIDInAccountOrderPage).Text;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Get text of price and quantity in open order page after place buy limit order.
        public Dictionary<string, string> GetPriceAndQuantityInAccountOrderPage(IWebDriver driver)
        {
            Dictionary<string, string> orderData = null;
            try
            {
                orderData = new Dictionary<string, string>();
                orderData.Add("Price", driver.FindElement(priceInAccountOrderPage).Text);
                orderData.Add("Quantity", driver.FindElement(quantityInAccountOrderPage).Text);
            }
            catch (Exception)
            {
                throw;
            }
            return orderData;
        }


       // Read downloaded csv file.
    
        public List<KeyValuePair<string, string>> ReadDataCSVFile(string fileName)
        {
            try
            {
                bool isHeader = true;
                var date = GenericUtils.GetCurrentTimeWithHyphen();
                var path = Directory.GetCurrentDirectory() + "\\DataTest\\"+fileName+" (" + date + ").csv";
                var reader = new StreamReader(File.OpenRead(@path));
                List<string> headers = new List<string>();
                List<string> trimmedValues = new List<string>();
                List<KeyValuePair<string, string>> csvFileData = new List<KeyValuePair<string, string>>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    if (isHeader)
                    {
                        isHeader = false;
                        headers = values.ToList();
                        for (int i = 0; i < values.Length; i++)
                        {
                            trimmedValues.Add(values[i].Trim('"'));
                        }
                        headers = trimmedValues;
                    }
                    else
                    {
                        for (int i = 0; i < values.Length; i++)
                        {
                            KeyValuePair<string, string> myItem = new KeyValuePair<string, string>(headers[i], values[i]);
                            csvFileData.Add(myItem);
                        }
                    }
                }
                reader.Close();
                return csvFileData;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Get data from downloaded account open order csv file.
        public Dictionary<string, string> GetOpenOrderCSVData(string orderIdValue,string fileName)
        {
            List<KeyValuePair<string, string>> openOrderData;
            Dictionary<string, string> orderData = null;
            try
            {
                orderData = new Dictionary<string, string>();
                openOrderData =ReadDataCSVFile(fileName);
                for (int i = 0; i < openOrderData.Count; i++)
                {
                   if (openOrderData[i].Value == orderIdValue)
                   {
                      for (int j = i; j < i + 12; j++)
                       {
                         orderData.Add(openOrderData[j].Key, openOrderData[j].Value);
                       }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return orderData;
        }


        // verify all accounts CSV data.
        public bool VerifyAllAccountsCSVData(string fileName)
        {
            bool isHeader = true, flag = false;
            try
            {
                var date = GenericUtils.GetCurrentTimeWithHyphen();
                var path = Directory.GetCurrentDirectory() + "\\DataTest\\" + fileName + " (" + date + ").csv";
                var reader = new StreamReader(File.OpenRead(path));
                List<string> headers = new List<string>();
                List<string> trimmedValues = new List<string>();
                List<KeyValuePair<string, string>> csvFileData = new List<KeyValuePair<string, string>>();
                ArrayList filledOrderList = new ArrayList();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    if (isHeader)
                    {
                        isHeader = false;
                        headers = values.ToList();
                        //logger.LogCheckPoint("" + headers);
                        for (int i = 0; i < values.Length; i++)
                        {
                            trimmedValues.Add(values[i].Trim('"'));
                        }
                        headers = trimmedValues;

                        if (headers.Contains(Const.OMSIDText) && headers.Contains(Const.AccountIdText) && headers.Contains(Const.AccountNameText)
                            && headers.Contains(Const.AccountHandleText)
                            && headers.Contains(Const.FirmIdText) && headers.Contains(Const.FirmNameText)
                            && headers.Contains(Const.AccountTypeText) && headers.Contains(Const.FeeGroupIDText) &&
                            headers.Contains(Const.ParentIDText) && headers.Contains(Const.RiskTypeText) &&
                            headers.Contains(Const.VerificationLevelText) && headers.Contains(Const.FeeProductTypeText)
                            && headers.Contains(Const.FeeProductText) && headers.Contains(Const.RefererIdText)
                            && headers.Contains(Const.LoyaltyProductIdText) && headers.Contains(Const.LoyaltyEnabledText)
                            && headers.Contains(Const.SupportedVenueIdsText))
                        {
                            flag = true;
                            break;
                        }
                    }
                }
                return flag;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool VerifyAllBalancesCSVData(string fileName)
        {
            bool isHeader = true, flag = false;
            try
            {
                var date = GenericUtils.GetCurrentTimeWithHyphen();
                var path = Directory.GetCurrentDirectory() + "\\DataTest\\" + fileName + " (" + date + ").csv";
                var reader = new StreamReader(File.OpenRead(path));
                List<string> headers = new List<string>();
                List<string> trimmedValues = new List<string>();
                List<KeyValuePair<string, string>> csvFileData = new List<KeyValuePair<string, string>>();
                ArrayList filledOrderList = new ArrayList();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    if (isHeader)
                    {
                        isHeader = false;
                        headers = values.ToList();
                        //logger.LogCheckPoint("" + headers);
                        for (int i = 0; i < values.Length; i++)
                        {
                            trimmedValues.Add(values[i].Trim('"'));
                        }
                        headers = trimmedValues;
                        if (headers.Contains(Const.OMSIdText) && headers.Contains(Const.AccountIdText) && headers.Contains(Const.ProductSymbolText)
                            && headers.Contains(Const.ProductIdText)
                            && headers.Contains(Const.AmountText) && headers.Contains(Const.HoldText)
                            && headers.Contains(Const.PendingDepositsText) && headers.Contains(Const.PendingWithdrawsText) &&
                            headers.Contains(Const.TotalDayDepositsText) && headers.Contains(Const.TotalDayWithdrawsText) &&
                            headers.Contains(Const.TotalMonthWithdrawsText) && headers.Contains(Const.TotalYearWithdrawsText))
                        {
                            flag = true;
                            break;
                        }
                    }
                }
                return flag;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Verify data in account activity csv file.
        public bool VerifyAccountActivityCSVData(string fileName, string orderIdValue)
        {
            bool val = false;
            List<KeyValuePair<string, string>> accountActivityData;
            try
            {
                accountActivityData = ReadDataCSVFile(fileName);
                for (int i = 0; i < accountActivityData.Count; i++)
                {
                    if (accountActivityData[i].Value == orderIdValue)
                    {
                        val = true;
                    }
                }
                return val;
            }
            catch (Exception)
            {
                throw;
            }
        }

       
        //This method selects number of orders to be displayed on the OMS Orders page
        public void SelectLedgerEntryProduct(string product)
        {
            try
            {
                GenericUtils.SelectDropDownByText(driver, ledgerEntryProduct, product);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string ProductAmountBalancesBeforeUpdate()
        {
            string usdBalancesBeforeUpdate;
            usdBalancesBeforeUpdate = USDAmountBalancesText();
            return usdBalancesBeforeUpdate;
        }

        public string ProductAmountBalancesAfterUpdate()
        {
            string usdBalancesAfterUpdate;
            usdBalancesAfterUpdate = USDAmountBalancesText();
            return usdBalancesAfterUpdate;
        }

        public void VerifyLabelUnderAccountDetailsSection()
        {
            string accountIDLabelText;
            string accountNameLabelText;
            string accountTypeLabelText;
            string riskTypeLabelText;
            string verificationLevelLabelText;
            string feeProductLabelText;
            string feeProductTypeLabelText;
            string accountBadgeLabelText;
            try
            {
                accountIDLabelText = driver.FindElement(accountIDLabel).Text;
                Assert.Equal(Const.accountIDLabelText, accountIDLabelText);
                accountNameLabelText = driver.FindElement(accountNameLabel).Text;
                Assert.Equal(Const.accountNameLabelText, accountNameLabelText);
                accountTypeLabelText = driver.FindElement(accountTypeLabel).Text;
                Assert.Equal(Const.accountTypeLabelText, accountTypeLabelText);
                riskTypeLabelText = driver.FindElement(riskTypeLabel).Text;
                Assert.Equal(Const.riskTypeLabelText, riskTypeLabelText);
                verificationLevelLabelText = driver.FindElement(verificationLevelLabel).Text;
                Assert.Equal(Const.verificationLevelLabelText, verificationLevelLabelText);
                feeProductLabelText = driver.FindElement(feeProductLabel).Text;
                Assert.Equal(Const.feeProductLabelText, feeProductLabelText);
                feeProductTypeLabelText = driver.FindElement(feeProductTypeLabel).Text;
                Assert.Equal(Const.feeProductTypeLabelText, feeProductTypeLabelText);
                accountBadgeLabelText = driver.FindElement(accountBadgeLabel).Text;
                Assert.Equal(Const.accountBadgeLabelText, accountBadgeLabelText);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // This method submits ledger entry for Credit and Debit based on credit flag
        public void SubmitLedgerEntry(string product, bool creditFlag, string amount, string comment)
        {                      
            ClickOnSubmitLedgerEntry();
            SelectLedgerEntryProduct(product);
            if (creditFlag)
            {
                LedgerEntryCreditAmountField().SendKeys(amount);
            }
            else
            {
                LedgerEntryDebitAmountField().SendKeys(amount);
            }
            LedgerEntryCommentField().SendKeys(comment);
            ClickOnLedgerEntrySendbutton();
        }

        // This method verifies if the the difference between the previous and updated balace is equal to the amount credited
        public bool VerifyUpdatedCreditBalance(string amount, string productBalancesBeforeUpdate, string productBalancesAfterUpdate)
        {
            bool flag = false;
            Decimal amountCredited = Decimal.Parse(amount);
            Decimal difference = Decimal.Parse(productBalancesAfterUpdate) - Decimal.Parse(productBalancesBeforeUpdate);
            if (amountCredited== difference)
            {
                flag = true;
            }
            return flag;
        }


        // This method verifies if the the difference between the previous and updated balace is equal to the amount debited
        public bool VerifyUpdatedDebitBalance(string amount, string productBalancesBeforeUpdate, string productBalancesAfterUpdate)
        {
            bool flag = false;
            Decimal amountDebited = Decimal.Parse(amount);
            Decimal difference = Decimal.Parse(productBalancesBeforeUpdate) - Decimal.Parse(productBalancesAfterUpdate);
            if (amountDebited == difference)
            {
                flag = true;
            }
            return flag;
        }

        // This method is used to navigate to accounts page and select the accountId passed
        public void GetAccountDetailsByAccountId(string accountId)
        {
            AdminCommonFunctions admincommonfunctions = new AdminCommonFunctions(logger);
            // Navigate on Accounts page.
            admincommonfunctions.SelectAccountsMenu();
            admincommonfunctions.SelectAccountsTab();
            logger.LogCheckPoint(LogMessage.NavigateAccountPage);
            // Click on ViewAll and search by accountid and double click.
            ClickOnViewAll(driver);
            SearchByAccountID(driver, accountId);
            DoubleClickOnAccountName(driver, accountId);
            logger.LogCheckPoint(string.Format(LogMessage.NavigateAccountPage, accountId));
        }

        // This method is used to create a new Deposit Key
        public void SubmitDepositKeys(string product, string accountProvider)
        {
            ClickOnSubmitDepositKeyButton();
            GenericUtils.SelectDropDownByText(driver, selectDepositKeyProduct, product);
            GenericUtils.WaitForElementVisibility(driver, selectDepositKeyAccountProvider, 10);
            GenericUtils.SelectDropDownByText(driver, selectDepositKeyAccountProvider, accountProvider); 
            GenericUtils.WaitForElementVisibility(driver, createNewKeyButton, 10).Click();
        }

        // This method is used to verify that no FIAT Currency is present in the dropdown
        public bool VerifyFIATCurrencyIsNotPresent(string fiatCurrency)
        {
            bool flag = false;
            string productIdText;
            int countOfProductIds = driver.FindElements(productIdList).Count;
            ArrayList arrayList = new ArrayList();
            for(int i=1; i <= countOfProductIds; i++)
            {
                productIdText = driver.FindElement(By.XPath("//select[@id='ProductId']/option["+ i +"]")).Text;
                arrayList.Add(productIdText);
            }            
            if (!arrayList.Contains(fiatCurrency))
            {
                flag = true;
            }
            return flag;
        }

        // This method is used to verify the clicking on Create Key button generates another Deposit Key
        public bool VerifyMultipleDepositKey()
        {
            bool flag = false;
            int initialCountOfDepositKeys;
            int finalCountOfDepositKeys;
            // Get the count of initial number of Deposit key
            initialCountOfDepositKeys = driver.FindElements(depositKeys).Count;
            // Generate New Deposit key
            GenericUtils.WaitForElementVisibility(driver, createNewKeyButton, 10).Click();
            // Get the count of final number of Deposit key
            finalCountOfDepositKeys = driver.FindElements(depositKeys).Count;
            if((finalCountOfDepositKeys - initialCountOfDepositKeys) >= 1)
            {
                flag = true;
            }
            return flag;
        }

        // This method is used to verify the copy button is present against the Deposit Key and Copy functionality is working
        public bool VerifyCopyDepositKey()
        {
            bool flag = false;
            string copiedText;
            AdminCommonFunctions admincommonfunctions = new AdminCommonFunctions(logger);
            int depositKeysList = driver.FindElements(depositKeys).Count;
            int countOfDepositKeys = depositKeysList - 1;
            for (int i= countOfDepositKeys; i >= 1; i--)
            {
                string depositKeyText = driver.FindElement(By.XPath("//div[@class='deposit-keys']/div[" + i + "]/span")).Text;
                IWebElement copyElement = driver.FindElement(By.XPath("//div[@class='deposit-keys']/div[" + i + "]/button"));
                if (copyElement.Displayed){
                    copyElement.Click();
                    CloseModal();
                    // This is to verify the copy - paste functionality, paste the copied deposit key to the comments text box of ledger entry
                    ClickOnSubmitLedgerEntry();
                    LedgerEntryCommentField().SendKeys(Keys.Control + "v");
                    copiedText = LedgerEntryCommentField().GetAttribute("value");
                    CloseModal();
                    if (depositKeyText.Equals(copiedText))
                    {
                        flag = true;
                        break;
                    }
                }
            }
            return flag;    
        }

        // Verify that pagination functionality is working
        public bool VerifyAccountBalancesPagination()
        {
            string firstAccountIdText;
            int firstAccountIdValue;
            string accountIdText;
            int accountIdValue;
            bool flag = false;
            List<int> numberList = new List<int>();
            By firstAccountIdElement = By.CssSelector("div.ReactVirtualized__Grid__innerScrollContainer>div:nth-of-type(1)>div:nth-of-type(1)");
            firstAccountIdText = driver.FindElement(firstAccountIdElement).Text;
            firstAccountIdValue = Int32.Parse(firstAccountIdText);
            int loopCount = firstAccountIdValue / 100;
            GenericUtils.ScrollDown(driver);
            for(int i=1; i <= loopCount+1; i++)
            {              
                accountIdText = GenericUtils.WaitForElementPresence(driver, firstAccountIdElement, 10).Text;
                accountIdValue = Int32.Parse(accountIdText);
                numberList.Add(accountIdValue);
                ClickOnPaginationNext();
            }
            for(int j = 0; j < numberList.Count; j++)
            {
                if ((numberList[j]).Equals(firstAccountIdValue))
                {
                    firstAccountIdValue = firstAccountIdValue - 100;
                    flag = true;
                }
            }
            return flag;
        }

        // Verify that filter by product functionality is working
        public void VerifyFilterByProduct(string product)
        {
            ClickOnViewAllAccountBalances();
        }

        // This method submits ledger entry with negative value for Credit and Debit based on credit flag
        public void SubmitLedgerEntryWithNegativeValue(string product, bool creditFlag, string amount, string comment)
        {
            ClickOnSubmitLedgerEntry();
            SelectLedgerEntryProduct(product);
            if (creditFlag)
            {
                LedgerEntryCreditAmountField().SendKeys(amount);
            }
            else
            {
                LedgerEntryDebitAmountField().SendKeys(amount);
            }
            LedgerEntryCommentField().SendKeys(comment);
            ClickOnLedgerEntrySendbutton();
        }


        public void VerifyLabelsUnderBalancesSection()
        {
            string productLabelText;
            string amountLabelText;
            string holdAmountLabelText;
            string pendingDepositLabelText;
            string pendingWithdrawLabelText;
            string dailyDepositsLabelText;
            string dailyWithdrawLabelText;
            string monthlyWithdrawLabelText;
            try
            {
                productLabelText = driver.FindElement(productLabelUnderBalances).Text;
                Assert.Equal(Const.productLabelText, productLabelText);
                amountLabelText = driver.FindElement(amountLabelUnderBalances).Text;
                Assert.Equal(Const.amountLabelText, amountLabelText);
                holdAmountLabelText = driver.FindElement(holdAmountLabelUnderBalances).Text;
                Assert.Equal(Const.holdAmountLabelText, holdAmountLabelText);
                pendingDepositLabelText = driver.FindElement(pendingDepositLabelUnderBalances).Text;
                Assert.Equal(Const.pendingDepositLabelText, pendingDepositLabelText);
                pendingWithdrawLabelText = driver.FindElement(pendingWithdrawLabelUnderBalances).Text;
                Assert.Equal(Const.pendingWithdrawLabelText, pendingWithdrawLabelText);
                dailyDepositsLabelText = driver.FindElement(dailyDepositsLabelUnderBalances).Text;
                Assert.Equal(Const.dailyDepositsLabelText, dailyDepositsLabelText);
                dailyWithdrawLabelText = driver.FindElement(dailyWithdrawLabelUnderBalances).Text;
                Assert.Equal(Const.dailyWithdrawLabelText, dailyWithdrawLabelText);
                monthlyWithdrawLabelText = driver.FindElement(monthlyWithdrawLabelUnderBalances).Text;
                Assert.Equal(Const.monthlyWithdrawLabelText, monthlyWithdrawLabelText);
            }
            catch (Exception)
            {
                throw;
            }
        }

        



}
}
