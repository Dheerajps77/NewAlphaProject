using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using AlphaPoint_QA.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace AlphaPoint_QA.Common
{
    public class AdminCommonFunctions
    {
        Config data;
        public IWebDriver driver;
        private ProgressLogger logger;

        public AdminCommonFunctions(ProgressLogger logger)
        {
            this.logger = logger;
            data = ConfigManager.Instance;
            driver = AlphaPointWebDriver.GetInstanceOfAlphaPointWebDriver();
        }

        //Below locators is used to fetch all the row details
        By elementList = By.CssSelector("div.ReactVirtualized__Table__row");

        //Admin Logout window section locators
        By exchangesAdminButton = By.CssSelector("span#OpenExchangesAdmin");
        By operatorAdminButton = By.CssSelector("span#OpenOperatorAdmin");
        By aMAdminButton = By.CssSelector("span#OpenAMAdmin");
        By oMSAdminButton = By.CssSelector("span#OpenOMSAdmin");
        By openUsersMenuButton = By.CssSelector("button#OpenUserMenu");

        //Locators for Admin-->OMS Admin
        By openCreateOMSButton = By.CssSelector("button#OpenCreateOMSForm");
        By oMSAdiminstrationList = By.CssSelector("div.ReactVirtualized__Grid__innerScrollContainer>div[aria-label=row]");
        By apexQa20msRowButtonLink = By.CssSelector("div.ReactVirtualized__Grid__innerScrollContainer>div[aria-label=row]>div:nth-of-type(2)");
        By oMSNameTextField = By.CssSelector("input#OMSName");
        By selectMarginMarketTypbutton = By.CssSelector("select#MarginMarketType");
        By permissionMarketDataCheckboxButton = By.CssSelector("input[name=PermissionedMarketData]");
        By assetManagerCoreUserTextField = By.CssSelector("input#AssetManagerCoreUser");
        By assetManagerCorePasswordTextField = By.CssSelector("input#AssetManagerCorePassword");
        By assetManagerIdTextField = By.CssSelector("input#AssetManagerId");
        By submitOMSFormButton = By.CssSelector("button#SubmitOMSForm");
        By editOMSWindow = By.CssSelector("a#CloseModal");
        By updateLoyalityFeeConfigButton = By.CssSelector("button#OpenLoyaltyFeeConfig");
        By loyaltyDiscountTextField = By.CssSelector("input#LoyaltyDiscount");
        By selectReferenceProductIdLink = By.CssSelector("select#ReferenceProductId");
        By referenceProductPriceTextField = By.CssSelector("input#ReferenceProductPrice");
        By isEnabledButton = By.CssSelector("input#IsEnabled");
        By deleteLoyaltyFeeConfigButton = By.CssSelector("button#DeleteLoyaltyFeeConfig");
        By updateLoyaltyFeeConfigButton = By.CssSelector("button#UpdateLoyaltyFeeConfigForm");
        By configureLoyaltyTokenWindow = By.CssSelector("header.modal-header>a");

        //locators for Admin-->Users Menu
        By usersTab = By.CssSelector("a#SelectTab0");
        By usersMenuLink = By.CssSelector("a[href='#/users']");
        By selectUser = By.CssSelector("div[title='magic3']");
        By openAddUserPermissionButton = By.CssSelector("button#OpenAddPermissionForm");
        By userPermissionList = By.CssSelector("div.ReactVirtualized__Grid__innerScrollContainer>div.ReactVirtualized__Table__row");
        By refreshUsersTableButton = By.CssSelector("button#RefreshUsersTable");
        By openUserByIdTextField = By.CssSelector("input#OpenUserByIdInput");
        By openUserByIdButton = By.CssSelector("button#OpenUserById");
        By OpenAddNewUserButton = By.CssSelector("button#OpenAddNewUserForm");
        By newUserNameTextField = By.CssSelector("input#UserName");
        By newUserEmailTextField = By.CssSelector("input#Email");
        By newUserPasswordTextField = By.CssSelector("input#passwordHash");
        By newUserConfirmPasswordTextField = By.CssSelector("input#passwordHashConfirmation");
        By newUserSubmitButton = By.CssSelector("button#SubmitUserForm");
        By searchTextBox = By.CssSelector("input[placeholder='Search...']");
        By submitBlockTradeCheckboxButton = By.CssSelector("input[name='SubmitBlockTrade']");
        By getOpenTradeReportsCheckboxButton = By.CssSelector("input[name='GetOpenTradeReports']");
        By permissionAddSuccessMessage = By.CssSelector("#messages > div > div > div > span");
        By permissionRevokedSuccessMessage = By.CssSelector("#messages > div > div > div > span");
        By userNameAccountButton = By.XPath("//section[@class='secondary_container']//p[text()='User Accounts']//following::table[1]//thead[1]//tr[1]//following::tbody[1]//tr[1]//td[1]");
        By showMoreLink = By.XPath("//span[text()='+ Show more' and @class='listToggle']");
        By showLessLink = By.XPath("//span[text()='– Show less' and @class='listToggle']");
        By closeUserPermissionWindowSection = By.CssSelector("header.modal-header>a");
        By createUserAffiliateTagButton = By.CssSelector("button#OpenCreateUserAffiliateTag");
        By affiliateTagTextField = By.CssSelector("input#AffiliateTag");
        By submitAffiliateTagButton = By.CssSelector("button#SubmitAffiliateTagForm");
        By affiliateWindowSection = By.CssSelector("header.modal-header>a");       
        By addMarketDataPermissionButton = By.CssSelector("button#OpenMarketDataPermissionsForm");
        By selectInstrumentForMarketData = By.CssSelector("select#InstrumentId");
        By selectPermissionForMarketData = By.CssSelector("select#permissions");
        By submitMarketDataPermissionsButton = By.CssSelector("button#SubmitMarketDataPermissionsForm");
        By editUserInformationLink = By.CssSelector("a#OpenEditUserForm");
        By userName = By.CssSelector("input#UserName");
        By userEmail = By.CssSelector("input#Email");
        By emailVerifyCheckBoxButton = By.CssSelector("input#EmailVerified");
        By selectAccountDropwdownValue = By.CssSelector("select#AccountId");
        By use2FACheckboxButton = By.CssSelector("input#Use2FA");
        By submitUserDetailsButton = By.CssSelector("button#SubmitUserDetailsForm");
        By closeEditUserInformationWindow = By.XPath("//header[@class='modal-header']//p[text()='Edit user information']//following::a");
        By passwordResetEmailLink = By.CssSelector("a#SendPasswordResetEmail");
        By openAssignAccountButton = By.CssSelector("button#OpenAssignAccountForm");
        By accountIDTextField = By.CssSelector("input[name='AccountId']");
        By submitAssignOrUnassignAccountToUserButton = By.CssSelector("button#SubmitAssignAccountToUserForm");
        By openUnassignAccountButton = By.CssSelector("button#OpenUnassignAccountForm");
        By assignAccountWindow = By.CssSelector("header.modal-header>a");        
        By usersListInContainer = By.CssSelector("div.ReactVirtualized__Grid__innerScrollContainer");
        By singleRowList = By.CssSelector("div.ReactVirtualized__Grid__innerScrollContainer>div");
        By singleUserdetials = By.CssSelector("div.ReactVirtualized__Grid__innerScrollContainer>div>div");
        By badgeExitCreationMsg = By.CssSelector("#app > div > div > div:nth-of-type(1) > div:nth-of-type(4)");
        By badgeExitOnAontherAccountMsg = By.CssSelector("#app > div > div > div:nth-of-type(1) > div:nth-of-type(4)");
        By closeAddNewBadgeWindow = By.CssSelector("header.modal-header>a");
        By userByID = By.CssSelector("input#OpenUserByIdInput");
        By openUserByButton = By.CssSelector("button#OpenUserById");
        By affiliateTagAddedText = By.CssSelector("div.details_container tr:nth-of-type(5)>td:nth-of-type(2)>div>span");
        By usersList = By.CssSelector("div.ReactVirtualized__Grid__innerScrollContainer>div");
        By affiliateLabelText = By.CssSelector("div.details_container table tbody tr:nth-of-type(5) td.subtitle");
        By accountNameText = By.CssSelector("div.details_container table:nth-of-type(1) tbody tr:nth-of-type(2) td:nth-of-type(2)");
        By userNameText = By.CssSelector("div.details_container table:nth-of-type(1) tbody tr:nth-of-type(1) td:nth-of-type(2)");        
        By selectUserNameUnderUserTab = By.CssSelector("div.ReactVirtualized__Grid__innerScrollContainer > div:nth-of-type(1) > div:nth-of-type(2)");
        By selectUserAccountUnderUserTab = By.CssSelector("div.ReactVirtualized__Grid__innerScrollContainer > div:nth-of-type(1) > div:nth-of-type(5)");
        By selectAccountID = By.CssSelector("select[id='AccountId']");
        By exportButton = By.CssSelector("button#OpenUsersExportMenu>div");
        By exportAllUsersButton = By.CssSelector("span#ExportAllUsers");
        By exportSuperUsersButton = By.CssSelector("span#ExportSuperusers");
        By exportByPermissionsButton = By.CssSelector("span#OpenExportUsersByPermissionsForm");
        By enterAccountIDValue = By.CssSelector("input[name=AccountId]");
        By defaultAccountID = By.CssSelector("#OpenUserDefaultAccount"); 

        //Locators for Admin-->Accounts
        By accountsTabMenuLink = By.CssSelector("a[href='#/accounts']");
        By accountsTab = By.CssSelector("a#SelectTab0");
        By accountBalancesTab = By.CssSelector("a#SelectTab1");
        By refreshAccountsTableButton = By.CssSelector("button#RefreshAccountsTable");
        By refreshAccountsBalancesTableButton = By.CssSelector("button#RefreshAccountsBalancesTable");
        By openEditAccountInformationLink = By.CssSelector("a#OpenEditAccountInformation");
        By accountNameTextField = By.CssSelector("input[name=AccountName]");
        By selectAccountType = By.CssSelector("select[name='AccountType']");
        By selectRiskType = By.CssSelector("select[name='RiskType']");
        By selectVerificationLevel = By.CssSelector("select[name='VerificationLevel']");
        By loyaltyFeesEnabledCheckboxButton = By.CssSelector("input[name='LoyaltyEnabled']");
        By saveAccountButton = By.CssSelector("button#SaveAccountForm");
        By accountEditInformationWindow = By.CssSelector("header.modal-header>a");
        By addconfigurationButton = By.CssSelector("button#OpenAddAccountSettings");
        By keyTextField = By.CssSelector("input#Key");
        By valueTextField = By.CssSelector("input#Value");
        By saveAddAccountButton = By.CssSelector("button#SaveAccountSettings");
        By addAccountConfigWindow = By.CssSelector("header.modal-header>a");
        By accountList = By.CssSelector("div.ReactVirtualized__Table__row");
        By singleUserAccountLink = By.CssSelector("div.ReactVirtualized__Grid__innerScrollContainer>div[aria-label='row']>div");
        By openAddNewBadgeButton = By.CssSelector("button#OpenAddNewBadge");
        By submitCreateAccountBadgeButton = By.CssSelector("button#SubmitCreateAccountBadge");
        By badgeTextField = By.CssSelector("input#Badge");
        By badgeAccountList = By.CssSelector("ul.account-badges-list");
        By openAccountByID = By.CssSelector("input#OpenAccountByIdInput");
        By openAccountButton = By.CssSelector("button#OpenAccountById");

        //locators for Admin-->Trades Menu
        By tradeMenuLink = By.CssSelector("a[href='#/trades']");
        By counterPartyID = By.CssSelector("div.report-block-trade-form__counterparty>div.form-group.ap-input__input-wrapper.report-block-trade-form__input-wrapper>label:nth-of-type(1)");
        By blockTradesButton = By.CssSelector("ul.tabs>li:nth-of-type(2) a#SelectTab1");       
        By accountIdTextField = By.CssSelector("input[name='AccountId']");
        By userIdTextField = By.CssSelector("input[name='UserId']");
        By tradeIdTextField = By.CssSelector("input[name='TradeId']");
        By executionIdTextField = By.CssSelector("input[name='ExecutionId']");
        By searchTradesButton = By.CssSelector("button[id='SearchTrades']");
        By refreshTradeReportsTableButton = By.CssSelector("button#RefreshTradeReportsTable");
        By selectOrders = By.CssSelector("select[name=Depth]");
        By selectInstrument = By.CssSelector("select[name=InstrumentId]");          
        By closeOrderHistoryModalWindow = By.CssSelector("button#CloseOrderHistoryModal");
        By selectAnInstrumentInBlocTradeTab = By.CssSelector("select[name=InstrumentId]");
        
        //locators for Admin-->OMS Orders
        By omsOrdersMenuLink = By.CssSelector("a[href='#/orders']");
        By omsOpenOrdersTab = By.CssSelector("ul.tabs>li:nth-of-type(1) a#SelectTab0");
        By omsOrdersHistoryTab = By.CssSelector("ul.tabs>li:nth-of-type(2) a#SelectTab1");
        By buySideBookWindow = By.CssSelector("div.container section:nth-of-type(1)>div.head>p");
        By sellSideBookWindow = By.CssSelector("div.container section:nth-of-type(2)>div.head>p");       
        By executeOrderButton = By.CssSelector("button#ExecuteOrder");
        By cancelOrderButton = By.CssSelector("button#CancelOrder");
        By omsOrdersInstrumentDropDown = By.CssSelector("select#instrument");        
        By abortActionButtonForOrder = By.CssSelector("button.mm-popup__btn.mm-popup__btn--mm-popup__btn.mm-popup__btn--inactive");
        By cancelOrderButtonForOrder = By.CssSelector("button.mm-popup__btn.mm-popup__btn--mm-popup__btn.mm-popup__btn--primary");

        //locators for Admin-->Tickets
        By ticketsMenuLink = By.CssSelector("a[href='#/tickets']");
        By showTicketsSearchButton = By.XPath("//span[text()='Show Search']");
        By withdrawsTab = By.CssSelector("a#SelectTab0");
        By depositsTab = By.CssSelector("a#SelectTab1");
        By assetManagerWalletTab = By.CssSelector("a#SelectTab2");
        //we can use below locators for for deposits and for asset manager wallet Tab
        By refreshTicketsTableButton = By.CssSelector("button#RefreshTicketsTable");
        //we can use below locators for for deposits and for asset manager wallet Tab to fetch the list
        By withdrawTicketWindow = By.CssSelector("header.modal-header>a");
        By depositsWindow = By.CssSelector("header.modal-header>a");
        By ticketID = By.CssSelector("div.ReactVirtualized__Grid__innerScrollContainer>div>div[title='480']");
        By depositeID = By.CssSelector("div.ReactVirtualized__Grid__innerScrollContainer>div>div[title='556']");

        By feeAmount = By.CssSelector("input[name=FeeAmt]");
        By acceptDepositeTicketButton = By.CssSelector("button#AcceptTicket");
        By declineDepositeTicketButton = By.CssSelector("button#DeclineTicket");
        By pendingDepositeTicketButton = By.CssSelector("button#PendingTicket");

        //locators for Admin-->Users Verifications
        By userVerificationMenuLink = By.CssSelector("a[href='#/usersVerification']");
        By usersVerificationTab = By.CssSelector("ul.tabs>li:nth-of-type(1) a#SelectTab0");        
        By refreshUsersVerificationTableButton = By.CssSelector("button#RefreshUsersVerificationTable");
        By selectUnderReviewDropdownValue = By.CssSelector("select[name=status]");
        By userConfigWindow = By.CssSelector("header.modal-header>a");
        By usersConfigurationRejectButton = By.CssSelector("button#UsersConfigurationReject");
        By usersConfigurationAcceptButton = By.CssSelector("button#UsersConfigurationAccept");
        By userPermissionSuccessMsg = By.CssSelector("div#messages>div>div>div>span");




        public IWebElement AccountBalancesTab()
        {
            return driver.FindElement(accountBalancesTab);
        }

        public IWebElement RefreshButtonForTicket()
        {
            return driver.FindElement(refreshTicketsTableButton);
        }

        public IWebElement FeeAmount()
        {
            return driver.FindElement(feeAmount);
        }


        public IWebElement EnterAccountIDValue()
        {
            return driver.FindElement(enterAccountIDValue);
        }

        public IWebElement SelectAccountID()
        {
            return driver.FindElement(selectAccountID);
        }

        public IWebElement DefaultAccountID()
        {
            return driver.FindElement(defaultAccountID);
        }

        public IWebElement SubmitAssignOrUnassignAccountToUserButton()
        {
            return driver.FindElement(submitAssignOrUnassignAccountToUserButton);
        }

        public IWebElement OpenUnassignAccountButton()
        {
            return driver.FindElement(openUnassignAccountButton);
        }

        public IWebElement OpenAssignAccountButton()
        {
            return driver.FindElement(openAssignAccountButton);
        }

        public IWebElement UserPermissionSuccessMsg()
        {
            return driver.FindElement(userPermissionSuccessMsg);
        }

        public IWebElement SelectAnInstrumentInBlocTradeTab()
        {
            return driver.FindElement(selectAnInstrumentInBlocTradeTab);
        }

        public IWebElement OpenEditAccountInformationLink()
        {
            return driver.FindElement(openEditAccountInformationLink);
        }

        public IWebElement OpenAccountByID()
        {
            return driver.FindElement(openAccountByID);
        }

        public IWebElement OpenAccountButton()
        {
            return driver.FindElement(openAccountButton);
        }

        public IWebElement UserByID()
        {
            return driver.FindElement(userByID);
        }

        public IWebElement OpenUserByButton()
        {
            return driver.FindElement(openUserByButton);
        }       
             
        public IWebElement UsersTab()
        {
            return driver.FindElement(usersTab);
        }

        public IWebElement UsersConfigurationAcceptButton()
        {
            return driver.FindElement(usersConfigurationAcceptButton);
        }

        public IWebElement UsersConfigurationRejectButton()
        {
            return driver.FindElement(usersConfigurationRejectButton);
        }

        public IWebElement OMSAdminButton()
        {
            return driver.FindElement(oMSAdminButton);
        }

        public IWebElement AMAdmin()
        {
            return driver.FindElement(aMAdminButton);
        }

        public IWebElement UsersMenuLink()
        {
            return driver.FindElement(usersMenuLink);
        }

        public IWebElement OperatorAdmin()
        {
            return driver.FindElement(operatorAdminButton);
        }

        public IWebElement ExchangesAdmin()
        {
            return driver.FindElement(exchangesAdminButton);
        }

        public IWebElement RefereshUsersTableButton()
        {
            return driver.FindElement(refreshUsersTableButton);
        }

        public void ClickOnRefreshUserTableButton()
        {
            try
            {
                Thread.Sleep(1000);
                UserSetFunctions.Click(RefereshUsersTableButton());
                Thread.Sleep(2000);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public int SingleRowList()
        {
            return driver.FindElements(singleRowList).Count;
        }

        public IWebElement AddNewUser()
        {
            return driver.FindElement(OpenAddNewUserButton);
        }

        public IWebElement NewUserName()
        {
            return driver.FindElement(newUserNameTextField);
        }

        public IWebElement NewUserEmail()
        {
            return driver.FindElement(newUserEmailTextField);
        }

        public IWebElement AddNewUsers()
        {
            return driver.FindElement(OpenAddNewUserButton);
        }

        public IWebElement NewUserPassword()
        {
            return driver.FindElement(newUserPasswordTextField);
        }

        public IWebElement SelectVerificationLevel()
        {
            return driver.FindElement(selectVerificationLevel);
        }

        public IWebElement NewUserConfirmPassword()
        {
            return driver.FindElement(newUserConfirmPasswordTextField);
        }

        public IWebElement NewUserSubmit()
        {
            return driver.FindElement(newUserSubmitButton);
        }

        public IWebElement OpenAddUserPermissionButton()
        {
            return driver.FindElement(openAddUserPermissionButton);
        }

        public IWebElement SearchPermissionTextBox()
        {
            return driver.FindElement(searchTextBox);
        }

        public IWebElement EmailVerifyCheckBoxButton()
        {
            return GenericUtils.WaitForElementPresence(driver, emailVerifyCheckBoxButton, 15);
        }
        
        public IWebElement SubmitBlockTradeCheckboxButton()
        {
            return driver.FindElement(submitBlockTradeCheckboxButton);
        }

        public IWebElement CloseUserPermissionWindowSection()
        {
            return driver.FindElement(closeUserPermissionWindowSection);
        }

        public IWebElement GetOpenTradeReportsCheckboxButton()
        {
            return driver.FindElement(getOpenTradeReportsCheckboxButton);
        }

        public IWebElement CreateUserAffiliateTagButton()
        {
            return driver.FindElement(createUserAffiliateTagButton);
        }

        public IWebElement AffiliateTagTextField()
        {
            return driver.FindElement(affiliateTagTextField);
        }

        public IWebElement SubmitAffiliateTagButton()
        {
            return driver.FindElement(submitAffiliateTagButton);
        }

        public IWebElement AffiliateWindowSection()
        {
            return driver.FindElement(affiliateWindowSection);
        }

        public IWebElement TicketsMenuLink()
        {
            return driver.FindElement(ticketsMenuLink);
        }

        public IWebElement WithdrawsTab()
        {
            return driver.FindElement(withdrawsTab);
        }

        public IWebElement DepositsTab()
        {
            return driver.FindElement(depositsTab);
        }

        public IWebElement TicketID()
        {
            return driver.FindElement(ticketID);
        }

        public IWebElement WithdrawTicketWindow()
        {
            return driver.FindElement(withdrawTicketWindow);
        }

        public IWebElement DepositsWindow()
        {
            return driver.FindElement(depositsWindow);
        }

        public IWebElement AcceptDepositeTicketButton()
        {
            return driver.FindElement(acceptDepositeTicketButton);
        }

        public IWebElement DeclineDepositeTicketButton()
        {
            return driver.FindElement(declineDepositeTicketButton);
        }

        public IWebElement PendingDepositeTicketButton()
        {
            return driver.FindElement(pendingDepositeTicketButton);
        }

        public IWebElement DepositeID()
        {
            return driver.FindElement(depositeID);
        }

        public IWebElement AffiliateTagAddedText()
        {
            return driver.FindElement(affiliateTagAddedText);
        }
        
        public IWebElement ConfigureLoyaltyTokenWindow()
        {
            return driver.FindElement(configureLoyaltyTokenWindow);
        }

        public IWebElement EditOMSWindow()
        {
            return driver.FindElement(editOMSWindow);
        }

        public IWebElement EditUserInformationLink()
        {
            return driver.FindElement(editUserInformationLink);
        }

        public IWebElement UserName()
        {
            return driver.FindElement(userName);
        }

        public IWebElement UserEmail()
        {
            return driver.FindElement(userEmail);
        }

        public IWebElement SubmitUserDetailsButton()
        {
            return driver.FindElement(submitUserDetailsButton);
        }

        public IWebElement ApexQa20msRowButtonLink()
        {
            return driver.FindElement(apexQa20msRowButtonLink);
        }

        public IWebElement SelectUserNameUnderUserTab()
        {
            return driver.FindElement(selectUserNameUnderUserTab);
        }


        public IWebElement SelectUserAccountUnderUserTab()
        {
            return driver.FindElement(selectUserAccountUnderUserTab);
        }

        public IWebElement UpdateLoyalityFeeConfigButton()
        {
            return driver.FindElement(updateLoyalityFeeConfigButton);
        }

        public IWebElement IsEnabledButton()
        {
            return driver.FindElement(isEnabledButton);
        }

        public IWebElement UserMenuSection()
        {
            return GenericUtils.WaitForElementPresence(driver, openUsersMenuButton, 15);           
        }

        public IWebElement AccountsTabMenuLink()
        {
            return driver.FindElement(accountsTabMenuLink);
        }

        public IWebElement AccountsTab()
        {
            return driver.FindElement(accountsTab);
        }


        public IWebElement SingleUserAccountLink()
        {
            return driver.FindElement(singleUserAccountLink);
        }

        public IWebElement OpenAddNewBadgeButton()
        {
            return driver.FindElement(openAddNewBadgeButton);
        }

        public IWebElement BadgeTextField()
        {
            return driver.FindElement(badgeTextField);
        }

        public IWebElement SubmitCreateAccountBadgeButton()
        {
            return driver.FindElement(submitCreateAccountBadgeButton);
        }

        public IWebElement BlockTradesTabButton()
        {
            return driver.FindElement(blockTradesButton);            
        }
      
        public IWebElement CloseOrderHistoryModalWindow()
        {
            return driver.FindElement(closeOrderHistoryModalWindow);
        }

        public IWebElement ExportButton()
        {
            return driver.FindElement(exportButton);
        }

        public IWebElement ExportAllUsersButton()
        {
            return driver.FindElement(exportAllUsersButton);
        }

        public IWebElement ExportSuperUsersButton()
        {
            return GenericUtils.WaitForElementClickable(driver, exportSuperUsersButton, 10);           
        }

        public IWebElement ExportByPermissionsButton()
        {
            return driver.FindElement(exportByPermissionsButton);
        }

        public IWebElement TradeMenuLink()
        {
            return driver.FindElement(tradeMenuLink);
        }

        public IWebElement OMSOrdersInstrumentDropDown()
        {
            return driver.FindElement(omsOrdersInstrumentDropDown);
        }
                
        public IWebElement UserVerificationMenuLink()
        {
            return driver.FindElement(userVerificationMenuLink);
        }

        public IWebElement LoyaltyFeesEnabledCheckboxButton()
        {
            return driver.FindElement(loyaltyFeesEnabledCheckboxButton);
        }

        public IWebElement SaveAccountButton()
        {
            return driver.FindElement(saveAccountButton);
        }
        
        public IWebElement UserNameAccountButton()
        {
            return driver.FindElement(userNameAccountButton);
        }

        public IWebElement AccountNameText()
        {
            return driver.FindElement(accountNameText);
        }

        public IWebElement UserNameText()
        {
            return driver.FindElement(userNameText);
        }

        public IWebElement OMSOrdersMenuLink()
        {
            return driver.FindElement(omsOrdersMenuLink);
        }

        // This method click on Account Balances Tab
        public void ClickOnAccountBalancesTab()
        {
            driver.FindElement(accountBalancesTab).Click();
        }

        // This method click on OMS Orders Menu Link
        public void ClickOnOMSOrdersMenuLink()
        {
            UserSetFunctions.Click(OMSOrdersMenuLink());
        }

        // This method click on Export All users to download CSV
        public void ExportAllUsers()
        {
            UserSetFunctions.Click(ExportAllUsersButton());           
        }

        // This method click on Export Super users to download CSV
        public void ExportSuperUsers()
        {
            UserSetFunctions.Click(ExportSuperUsersButton());
        }

        // This method Click On Refresh Button On Tickets Page.
        public void ClickOnRefreshButtonOnTicketsPage()
        {
            UserSetFunctions.Click(RefreshButtonForTicket());
        }

        // This method click on Export by permissions to download CSV
        public void ExportByPermissions()
        {
            UserSetFunctions.Click(ExportByPermissionsButton());
        }
                
        // Get text of Fee amount from withdraw ticket.
        public string GetTextOfFeeAmount()
        {
            return FeeAmount().GetAttribute("value");
        }
        public void ClickOnUserMenuSection()
        {
            try
            {
                UserSetFunctions.Click(UserMenuSection());
            }
            catch (Exception)
            {
                throw;
            }            
        }

        //This method select an account ID under unassign account window
        public void SelectAnAccountID(string accountID)
        {
            try
            {
                Thread.Sleep(2000);
                GenericUtils.SelectDropDownByValue(driver, selectAccountID, accountID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This method selects an instrument on the OMS Orders page
        public void SelectOMSOrdersInstrument(string instrument)
        {
            try
            {
                GenericUtils.SelectDropDownByText(driver, omsOrdersInstrumentDropDown, instrument);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This method enter an account ID under assign account window
        public void EnterAnAccountID(string accountID)
        {
            try
            {
                //Thread.Sleep(2000);
                UserSetFunctions.EnterText(EnterAccountIDValue(), accountID);
                Thread.Sleep(2000);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This will click on Assign Account
        public void ClickOnAssignAccountButton()
        {
            try
            {
                GenericUtils.WaitForElementClickable(driver, openAssignAccountButton, 15).Click();
            }
            catch(Exception)
            {
                throw;
            }
        }

        //This will click on "Unassign" button to submit
        public void ClickOnSubmitAssignOrUnassignAccountButton()
        {
            try
            {
                GenericUtils.WaitForElementClickable(driver, submitAssignOrUnassignAccountToUserButton, 15).Click();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This will click on Unassign Account
        public void ClickOnUnassignAccountButton()
        {
            try
            {
                GenericUtils.WaitForElementClickable(driver, openUnassignAccountButton, 15).Click();
            }
            catch (Exception)
            {
                throw;
            }
        }


        //This will verify if "assign account" button is present
        public bool VerifyAssignAccountButton()
        {
            bool flag = false;
            try
            {
                if (OpenAssignAccountButton().Enabled)
                {
                    flag = true;
                    return flag;
                }
            }
            catch (Exception)
            {            
                throw;
            }
            return flag;
        }


        //This will verify if "Unassign account" button is present
        public bool VerifyUnassignAccountButton()
        {
            bool flag = false;
            try
            {
                if (!OpenUnassignAccountButton().Enabled)
                {
                    Thread.Sleep(2000);
                    flag = true;
                    return flag;
                }
            }
            catch (Exception)
            {                
                throw;
            }
            return flag;
        }

        public string AccountNameTextValue()
        {
            string accountName;
            try
            {
                accountName = AccountNameText().Text;
            }
            catch (Exception)
            {
                throw;
            }

            return accountName;
        }

        public string UserNameTextValue()
        {
            string userName;
            try
            {
                userName = UserNameText().Text;
            }
            catch (Exception)
            {
                throw;
            }

            return userName;
        }

        //This method will click on "Users" menu button on admin home page
        public void ClickOnUsersMenuLink()
        {
            try
            {
                Thread.Sleep(1000);
                UserSetFunctions.Click(UsersMenuLink());
                Thread.Sleep(1000);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This method will click on "Save" button in edit account information window section
        public void SaveButton()
        {
            try
            {
                Thread.Sleep(2000);
                UserSetFunctions.Click(SaveAccountButton());
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This functions if loyality fee checkBox is enabled or disabled
        public void LoyaltyFeeCheckedOrUnchecked()
        {
            Thread.Sleep(2000);
            try
            {
                if(!LoyaltyFeesEnabledCheckboxButton().Selected)
                {
                    UserSetFunctions.Click(LoyaltyFeesEnabledCheckboxButton());                    
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This functions will select the Instrument in block trade Tab
        public void BlockTradeInstrumentSelection(string instrument)
        {
            Thread.Sleep(2000);
            try
            {
                SelectElement select = new SelectElement(SelectAnInstrumentInBlocTradeTab());
                Thread.Sleep(2000);
                select.SelectByText(instrument);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This method will click on "Open" button while searching the accountID
        public void OpenAccountBtn()
        {
            try
            {
                Thread.Sleep(2000);
                UserSetFunctions.Click(OpenAccountButton());
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This method Search account by putting the user id in the text field
        public void UserByIDText(string UserID)
        {
            try
            {
                Thread.Sleep(2000);
                UserSetFunctions.EnterText(UserByID(), UserID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This method will click on "Open" button while searching the User
        public void OpenUserButton()
        {
            try
            {
                Thread.Sleep(2000);
                UserSetFunctions.Click(OpenUserByButton());
                GenericUtils.WaitForElementVisibility(driver, affiliateLabelText, 10);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This functions will click on "Edit Account Information" in Account Tab
        public void EditInformationButton()
        {
            Thread.Sleep(2000);
            try
            {
                UserSetFunctions.Click(OpenEditAccountInformationLink());
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This method will create an Affiliate Tag on user
        public string AffiliateTagCreation(string tagName)
        {
            string affiliateTagText = null;
            string randomString;
            try
            {
                randomString = GenericUtils.RandomString(Const.RandomStringLength);
                if(CreateUserAffiliateTagButton().Text.Equals(Const.CreateAffiliateTagBtn))
                {
                    UserSetFunctions.Click(CreateUserAffiliateTagButton());
                    UserSetFunctions.EnterText(AffiliateTagTextField(), tagName + randomString);
                    UserSetFunctions.Click(SubmitAffiliateTagButton());
                    Thread.Sleep(2000);
                    affiliateTagText = AffiliateTagAddedText().Text;
                    logger.LogCheckPoint(String.Format(LogMessage.AffiliateTagCreated, affiliateTagText));
                }                
            }            
            catch (NoSuchElementException)
            {
                affiliateTagText = AffiliateTagAddedText().Text;
                logger.LogCheckPoint(String.Format(LogMessage.AffiliateTagIsPresent, affiliateTagText));
            }
            catch (Exception)
            {
                throw;
            }
            return affiliateTagText;
        }

        //This method Search account by putting the account id in the text field
        public void OpenAccountByIDText(string UserID)
        {
            try
            {
                Thread.Sleep(3000);
                UserSetFunctions.EnterText(OpenAccountByID(), UserID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This method Search account by putting the account id in the text field
        public void UserBadgeIDValue(string ID)
        {
            try
            {
                Thread.Sleep(2000);
                UserSetFunctions.EnterText(BadgeTextField(), ID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This method will click on "Add badge" in Account Tab
        public void OpenAddNewBadgeButtonForUser()
            {
            try
            {
                Thread.Sleep(2000);
                UserSetFunctions.Click(OpenAddNewBadgeButton());
            }
            catch (Exception)
            {
                throw;
            }
        }


        //This method will click on "Add permission" button
        public void UserPermissionButton()
        {
            try
            {
                Thread.Sleep(2000);
                UserSetFunctions.Click(OpenAddUserPermissionButton());
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This method will click on Reject Button under user Configuration
        public void RejectUserConfig()
        {
            try
            {
                UserSetFunctions.Click(UsersConfigurationRejectButton());
            }
            catch(Exception)
            {
                throw;
            }
        }

        //This method will click on Accept Button under user Configuration
        public void AcceptUserConfig()
        {
            try
            {
                UserSetFunctions.Click(UsersConfigurationAcceptButton());
            }
            catch (Exception)
            {
                throw;
            }
        }
        //This method will clear the userPermission textbox field
        public void ClearTextBox()
        {
            try
            {
                Thread.Sleep(2000);
                UserSetFunctions.Clear(SearchPermissionTextBox());
                Thread.Sleep(2000);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This method will create submitBlockTrade permission on user
        public void AddSubmitBlockTradePermissions(string userPermissionName)
        {
            try
            {
                UserSetFunctions.EnterText(SearchPermissionTextBox(), userPermissionName);
                if (!SubmitBlockTradeCheckboxButton().Selected)
                {                   
                    UserSetFunctions.Click(SubmitBlockTradeCheckboxButton());
                } 
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This method will create permission on user
        public void AddUserPermissions(string userPermissionName)
        {
            try
            {
                UserSetFunctions.EnterText(SearchPermissionTextBox(), userPermissionName);
                if (!SubmitBlockTradeCheckboxButton().Selected)
                {
                    UserSetFunctions.Click(SubmitBlockTradeCheckboxButton());
                    logger.LogCheckPoint(String.Format(LogMessage.VerifyAddedUserPermissionSuccessMessagePassed));
                }
                else if(SubmitBlockTradeCheckboxButton().Selected)
                {
                    UserSetFunctions.Click(SubmitBlockTradeCheckboxButton());
                    UserSetFunctions.Click(SubmitBlockTradeCheckboxButton());
                    logger.LogCheckPoint(String.Format(LogMessage.VerifyAddedUserPermissionSuccessMessageFailed));
                }
            }
            catch (Exception)
            {
                logger.LogCheckPoint(String.Format(LogMessage.VerifyAddedUserPermissionSuccessMessageFailed));
                throw;
            }
        }

        //This method will create permission on user
        public void RevokedUserPermissions(string userPermissionName)
        {
            try
            {
                UserSetFunctions.EnterText(SearchPermissionTextBox(), userPermissionName);
                if (!SubmitBlockTradeCheckboxButton().Selected)                    
                {
                    Thread.Sleep(2000);
                    logger.LogCheckPoint(String.Format(LogMessage.VerifyRevokedUserPermissionFromListPassed, userPermissionName));
                    logger.LogCheckPoint(String.Format(LogMessage.VerifyRevokeUserPermissionPassed));
                }
                else
                {
                    logger.LogCheckPoint(String.Format(LogMessage.VerifyRevokedUserPermissionFromListFailed, userPermissionName));
                }
            }
            catch (Exception)
            {
                logger.LogCheckPoint(String.Format(LogMessage.VerifyRevokedUserPermissionFromListFailed, userPermissionName));
                throw;
            }
        }

        //This method will create GetOpenTradeReports permission on user
        public void AddGetOpenTradeReportsPermissions(string userPermissionName)
        {
            try
            {
                UserSetFunctions.EnterText(SearchPermissionTextBox(), userPermissionName);
                if (!GetOpenTradeReportsCheckboxButton().Selected)
                {
                    UserSetFunctions.Click(GetOpenTradeReportsCheckboxButton());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This method will close the user permission window
        public void ClosePermissionWindow()
        {
            try
            {
                UserSetFunctions.Click(CloseUserPermissionWindowSection());
            }
            catch (Exception)
            {
                throw;
            }
        }
       
        public string getPermissionSuccessMsg()
        {
            string permissionMsg;
            try
            {
                permissionMsg = UserPermissionSuccessMsg().Text;
            }
            catch(Exception)
            {
                throw;
            }
            return permissionMsg;
        }

        //This method will create a new User
        public string AddNewUser(string userName, string userEmail, string passWord, string confirmPassword)
        {
            try
            {
                UserSetFunctions.Click(AddNewUsers());
                UserSetFunctions.EnterText(NewUserName(), userName);
                UserSetFunctions.EnterText(NewUserEmail(), userEmail);
                UserSetFunctions.EnterText(NewUserPassword(), passWord);
                UserSetFunctions.EnterText(NewUserConfirmPassword(), confirmPassword);
                UserSetFunctions.Click(NewUserSubmit());
                logger.LogCheckPoint(String.Format(LogMessage.CreatedUserWithoutTrailingWhiteSpacePassed));
                return userName;

            }
            catch (Exception)
            {
                logger.LogCheckPoint(String.Format(LogMessage.CreatedUserWithoutTrailingWhiteSpaceFailed));
                throw;
            }
        }

        //This method will click on "OMS Admin" button
        public void SelectOMSAdminOption()
        {
            try
            {
                UserSetFunctions.Click(OMSAdminButton());
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This method will click on UserMenu button from the right top corner
        public void UserMenuBtn()
        {
            try
            {
                Thread.Sleep(3000);
                UserSetFunctions.Click(UserMenuSection());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UsersVerificationMenuBtn()
        {
            try
            {
                UserSetFunctions.Click(UserVerificationMenuLink());
            }
            catch(Exception)
            {
                throw;
            }
        }

        //This method will click on Trade Menu button
        public void SelectTradeMenu()
        {
            try
            {
                UserSetFunctions.Click(TradeMenuLink());
            }
            catch(Exception)
            {
                throw;
            }
        }
        //This method will click on "AM Admin" button
        public void SelectAMAdimOption()
        {
            try
            {
                UserSetFunctions.Click(AMAdmin());
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This method will click on "Operator Admin" button
        public void SelectOperatorAdminOption()
        {
            try
            {
                UserSetFunctions.Click(OperatorAdmin());
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This method will click on Tickets Menu
        public void SelectTicketsMenu()
        {
            try
            {
                UserSetFunctions.Click(TicketsMenuLink());
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This method will click on Accounts Menu
        public void SelectAccountsMenu()
        {
            try
            { 
                UserSetFunctions.Click(AccountsTabMenuLink());
            }
            catch (Exception)
            {
                throw;
            }
        }
        //This method will click on Accounts Menu
        public void SelectAccountsTab()
        {
            try
            {
                UserSetFunctions.Click(AccountsTab());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //This method will click on Accounts Menu
        public void SelectAccountsBalancesTab()
        {
            try
            {
                UserSetFunctions.Click(AccountBalancesTab());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //This method will click on Withdraw Tab in Tickets
        public void NavigateToWithdrawTicketsTab()
        {
            try
            {
                UserSetFunctions.Click(WithdrawsTab());
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This method will click on Deposits Tab in Tickets
        public void NavigateToDepositTicketsTab()
        {
            try
            {
                UserSetFunctions.Click(DepositsTab());
            }
            catch (Exception)
            {
                throw;
            }
        }


        //This method will the withdraw Tab window
        public void CloseWithdrawTicketWindow()
        {
            try
            {
                UserSetFunctions.Click(WithdrawTicketWindow());
            }
            catch (Exception)
            {

                throw;
            }
        }

        //This method will the Deposits Tab window
        public void CloseDepositsWindow()
        {
            try
            {
                UserSetFunctions.Click(DepositsWindow());
            }
            catch (Exception)
            {

                throw;
            }
        }

        //This method will click on accept button in Deposite Tab
        public void ClickOnAcceptButtonFromDepositsTicketModal()
        {
            try
            {
                UserSetFunctions.Click(AcceptDepositeTicketButton());
            }
            catch (Exception)
            {

                throw;
            }
        }

        //This method will click on decline button in Deposite Tab
        public void ClickOnDeclineButtonFromDepositsTicketModal()
        {
            try
            {
                UserSetFunctions.Click(DeclineDepositeTicketButton());
            }
            catch (Exception)
            {

                throw;
            }
        }

        //This method will click on pending button in Deposite Tab
        public void ClickOnPendingButtonFromDepositsTicketModal()
        {
            try
            {
                UserSetFunctions.Click(PendingDepositeTicketButton());
            }
            catch (Exception)
            {

                throw;
            }
        }

        //This method will click on user account on User page
        public void OpenAccountFromUserPage()
        {
            try
            {
                Thread.Sleep(2000);
                Actions actions = new Actions(driver);
                actions.DoubleClick(UserNameAccountButton()).Build().Perform();
                Thread.Sleep(2000);
            }
            catch (Exception)
            {

                throw;
            }
        }

        //This method will edit/update the Email verified status of particular user and submit the information
        public void EditUserVerificationLevel(string verificationLevel)
        {
            try
            {
                EditInformationButton();
                Thread.Sleep(1000);
                UserSetFunctions.SelectDropdown(SelectVerificationLevel(), verificationLevel);
                Thread.Sleep(1000);
                SaveButton();
            }
            catch (Exception)
            {

                throw;
            }
        }

        //This method will edit/update the Email verified status of particular user and submit the information
        public void EditUserEmailStatus(string userName)
        {
            try
            {
                UserSetFunctions.Click(EditUserInformationLink());
                Thread.Sleep(2000);
                UserSetFunctions.Click(EmailVerifyCheckBoxButton());
                Thread.Sleep(1000);
                UserSetFunctions.Click(SubmitUserDetailsButton());
                Thread.Sleep(2000);
            }
            catch (Exception)
            {

                throw;
            }
        }

        //This method will click on the first ticket in withdraw Tab
        public void ClickOnTicketFromWithdrawTicketList(string ticketID)
        {
            try
            {
                int accountArrayList = driver.FindElements(elementList).Count;
                for (int i = 1; i <= accountArrayList; i++)
                {
                    IWebElement webElement = driver.FindElement(By.XPath("//div[@class='ReactVirtualized__Table__row'][" + i + "]/div[1]"));
                    string webElementtext = webElement.Text;
                    if (webElementtext.Equals(ticketID))
                    {
                        Thread.Sleep(2000);
                        Actions actions = new Actions(driver);
                        actions.DoubleClick(webElement).Build().Perform();
                        Thread.Sleep(3000);
                        break;
                    }
                }
            }

            catch (Exception)
            {
                throw;
            }
        }

        //This method will click on the first ticket in Deposits Tab
        public void ClickOnTicketFromDepositTicketList(string depositsID)
        {
            try
            {
                int accountArrayList = driver.FindElements(elementList).Count;
                for (int i = 1; i <= accountArrayList; i++)
                {
                    IWebElement webElement = driver.FindElement(By.XPath("//div[@class='ReactVirtualized__Table__row'][" + i + "]/div[1]"));
                    string webElementtext = webElement.Text;
                    if (webElementtext.Equals(depositsID))
                    {
                        Thread.Sleep(2000);
                        Actions actions = new Actions(driver);
                        actions.DoubleClick(webElement).Build().Perform();
                        Thread.Sleep(3000);
                        break;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This method will click on the Users menu button in Admin
        public void SelectUserFromUsersList(string userName)
        {          
            try
            {               
                int accountArrayList = driver.FindElements(elementList).Count;
                for (int i = 1; i <= accountArrayList; i++)
                {
                    IWebElement webElement = driver.FindElement(By.XPath("//div[@class='ReactVirtualized__Table__row'][" + i + "]/div[2]"));
                    string webElementtext = webElement.Text;
                    if (webElementtext.Equals(userName))
                    {
                        Thread.Sleep(2000);

                        Actions action = new Actions(driver);
                        action.DoubleClick(webElement).Build().Perform();
                        Thread.Sleep(2000);
                        break;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
      
        //This method will close the Loyality window
        public void CloseLoyaltyTokenWindow()
        {
            UserSetFunctions.Click(ConfigureLoyaltyTokenWindow());
        }

        //This method will close the Order History of block trade window section
        public void CloseBlockTradeWindow()
        {
            try
            {
                UserSetFunctions.Click(CloseOrderHistoryModalWindow());
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This method will close the Edit OMS window
        public void CloseEditOMSWindow()
        {
            try
            {
                UserSetFunctions.Click(EditOMSWindow());
            }
            catch (Exception)
            {

                throw;
            }
        }        

        //This method will edit/update the username of particular user and submit the information
        public void EditUserAccountInformation(string userName)
        {
            try
            {
                UserSetFunctions.Click(EditUserInformationLink());
                UserSetFunctions.EnterText(UserName(), userName);
                UserSetFunctions.Click(SubmitUserDetailsButton());
            }
            catch (Exception)
            {

                throw;
            }
        }

        //This method will click on username and check whether fee is enabled or disabled
        public void OMSAdminstrationLoyalityFee()
        {
            try
            {
                Actions action = new Actions(driver);
                action.DoubleClick(ApexQa20msRowButtonLink()).Build().Perform();
                UserSetFunctions.Click(UpdateLoyalityFeeConfigButton());
                if (IsEnabledButton().Selected)
                {
                    logger.LogCheckPoint(LogMessage.LoyalityFeeEnabled);
                }
                else
                {
                    logger.LogCheckPoint(LogMessage.LoyalityFeeDisabled);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        

        //This method will get this Message "Badge already exists"
        public IWebElement BadgeExitCreationMsg()
        {
            try
            {
                return driver.FindElement(badgeExitCreationMsg);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IWebElement CloseAddNewBadgeWindow()
        {
            try
            {
                return driver.FindElement(closeAddNewBadgeWindow);
            }
            catch (Exception)
            {

                throw;
            }
        }

        //This method will get this Message "Badge Already Exists on another account"
        public IWebElement BadgeExitOnAontherAccountMsg()
        {
            try
            {
                return driver.FindElement(badgeExitOnAontherAccountMsg);
            }
            catch (Exception)
            {

                throw;
            }
        }       

        //This method will create badge for user
        public void AddAccountBadge(string userName, string badgeNumber)
        {
            try
            {
                int accountArrayList = driver.FindElements(accountList).Count;
                for (int i = 1; i <= accountArrayList; i++)
                {
                    IWebElement accountNameElement = driver.FindElement(By.XPath("//div[@class='ReactVirtualized__Table__row'][" + i + "]/div[2]"));
                    string accountName = accountNameElement.Text;
                    if (accountName.Equals(userName))
                    {
                        Actions action = new Actions(driver);
                        action.DoubleClick(accountNameElement).Build().Perform();
                        break;
                    }
                }
                UserSetFunctions.Click(OpenAddNewBadgeButton());
                Thread.Sleep(2000);
                UserSetFunctions.EnterText(BadgeTextField(), badgeNumber);
                Thread.Sleep(2000);
                UserSetFunctions.Click(SubmitCreateAccountBadgeButton());
                Thread.Sleep(2000);
                CheckAndCreateBadge(userName, badgeNumber);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ArrayList GetListOfBlockTradeReports()
        {
            ArrayList tradeReportsOrderList = new ArrayList();
            int countOfTradeReports = driver.FindElements(By.XPath("//div[@class='ReactVirtualized__Table__row']")).Count;

            for (int i = 1; i <= countOfTradeReports; i++)
            {
                String textFinal = "";
                int countItems = driver.FindElements(By.XPath("(//div[@class='ReactVirtualized__Table__row'])[" + i + "]/div")).Count;
                for (int j = 2; j <= (countItems)-2; j++)
                {
                    String text = driver.FindElement(By.XPath("(//div[@class='ReactVirtualized__Table__row'])[" + i + "]/div[" + j + "]")).Text;
                    if (j == 2)
                    {
                        textFinal = text;
                    }
                    else
                    {
                        textFinal = textFinal + " || " + text;
                    }

                }
                tradeReportsOrderList.Add(textFinal);
            }
            return tradeReportsOrderList;
        }

        //This method return the buy block trade list and perfom click action on it
        public bool BuyBlockTradeList(string accountTypeID, string counterPartyID, string instrument, string originalQuantity, string quantityExecuted)
        {
            try
            {
                bool flag = false;
                string originalQtyValue;
                string quantityExecutedValue;

                originalQtyValue = GenericUtils.ConvertToDoubleFormat(Double.Parse(originalQuantity));
                quantityExecutedValue = GenericUtils.ConvertToDoubleFormat(Double.Parse(quantityExecuted));

                string expectedRow_1 = accountTypeID + " || " + counterPartyID + " || " + "FullyExecuted" + " || " + instrument + " || " + "Buy" + " || " + originalQtyValue + " || " + quantityExecutedValue;
                string expectedRow_2 = counterPartyID + " || " + accountTypeID + " || " + "FullyExecuted" + " || " + instrument + " || " + "Sell" + " || " + originalQtyValue + " || " + quantityExecutedValue;

                var tradeReportsOrderList = GetListOfBlockTradeReports();
                if (tradeReportsOrderList.Contains(expectedRow_1) && tradeReportsOrderList.Contains(expectedRow_2))
                {
                    flag = true;
                }
                if (flag)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedTradesPassed,"Buy"));
                }
                else
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedTradesFailed, "Sell"));
                }
                return flag;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void CreateBadgeAccount()
        {
            try
            {
                UserSetFunctions.Click(SubmitCreateAccountBadgeButton());
            }
            catch (Exception)
            {

                throw;
            }
        }

        //This method will check same badge created or not
        public bool CheckAndCreateBadge(string username, string badgeNumber)
        {
            bool flag = false;
            try
            {
                OpenAddNewBadgeButtonForUser();
                UserBadgeIDValue(badgeNumber);
                CreateBadgeAccount();

                if (BadgeExitOnAontherAccountMsg().Displayed || BadgeExitCreationMsg().Displayed)
                {
                    flag = true;
                    Thread.Sleep(2000);
                    UserSetFunctions.Click(CloseAddNewBadgeWindow());
                    IWebElement ele= driver.FindElement(By.CssSelector("svg[fill='#000000']"));
                    Actions action = new Actions(driver);
                    action.MoveToElement(ele).Build().Perform();
                    Thread.Sleep(2000);
                    GenericUtils.WaitForElementVisibility(driver, By.CssSelector("svg[fill='#000000']"), 15);
                    Thread.Sleep(2000);
                    ele.Click();
                    driver.FindElement(By.CssSelector("button.mm-popup__btn.mm-popup__btn--mm-popup__btn.mm-popup__btn--primary"));
                    OpenAddNewBadgeButtonForUser();
                    UserBadgeIDValue(badgeNumber);
                    CreateBadgeAccount();
                    Thread.Sleep(2000);
                }

                return flag;
            }
            
            catch (NoSuchElementException)
            {
                throw;
            }
        }

        //This method will click on the Block Trade Tab
        public void BlockTradeBtn()
        {
            try
            {
               // UserSetFunctions.Click(BlockTradesButton());
                UserSetFunctions.Click(BlockTradesTabButton());
            }
            catch (Exception)
            {
                throw;
            }
        }        

        
        public void TradeUserDetails(string executionID)
        {
            try
            {
                int accountArrayList = driver.FindElements(elementList).Count;
                for (int i = 1; i <= accountArrayList; i++)
                {
                    IWebElement webElement = driver.FindElement(By.XPath("//div[@class='ReactVirtualized__Table__row'][" + i + "]/div[2]"));
                    string webElementtext = webElement.Text;
                    if (webElementtext.Equals(counterPartyID))
                    if (webElementtext.Equals(executionID))
                    {
                        Thread.Sleep(2000);
                        Actions action = new Actions(driver);
                        action.DoubleClick(webElement).Build().Perform();
                        Thread.Sleep(2000);                        
                        break;
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        //This will click on Users Tab under User Menu
        public void UsersTabBtn()
        {
            UserSetFunctions.Click(UsersTab());
            Thread.Sleep(2000);
        }

        //This will click on Export Button on User page
        public void ClickOnExportButton()
        {
            UserSetFunctions.Click(ExportButton());
        }

        // This method selects a user from the user list based on username 
        public void SelectUserFromUserList(IWebDriver driver, string userName)
        {
            IWebElement userNameFromListElement;
            string userNameToSelect;
            Actions action = new Actions(driver);
            try
            {
                GenericUtils.WaitForElementPresence(driver, usersList, 15);
                int count = driver.FindElements(usersList).Count;
                for (int i = 1; i <= count; i++)
                {
                    userNameFromListElement = driver.FindElement(By.XPath("//div[@class='ReactVirtualized__Grid__innerScrollContainer']/div[" + i + "]/div[2]"));
                    userNameToSelect = userNameFromListElement.Text;
                    if (userNameToSelect.Equals(userNameToSelect))
                    {
                        action.DoubleClick(userNameFromListElement).Build().Perform();
                        Thread.Sleep(2000);
                        break;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        //This will return the username from the user list under User Tab
        public string getUserNameFromUserList()
        {
            string userName;
            try
            {
                userName = SelectUserNameUnderUserTab().Text;
            }
            catch (Exception)
            {
                throw;
            }
            return userName;
        }

        //This will return the user account ID from the user list under User Tab
        public string getUserAccountIDFromUserList()
        {
            string userAccountID;
            try
            {
                userAccountID = SelectUserAccountUnderUserTab().Text;
            }
            catch (Exception)
            {
                throw;
            }
            return userAccountID;
        }


        public void VerifyStatus(IWebDriver driver, string statusid, String status)
        {
            ArrayList myList = new ArrayList();
            Thread.Sleep(4000);
            try
            {
                int count = driver.FindElements(By.XPath("//div[@class='ReactVirtualized__Grid__innerScrollContainer']/div")).Count;
                for (int i = 1; i <= count - 1; i++)
                {
                    string statusId = driver.FindElement(By.XPath("//div[@class='ReactVirtualized__Grid__innerScrollContainer']/div[" + i + "]/div[5]")).Text;
                    myList.Add(statusId);
                    if (statusId.Equals(statusId))
                    {
                        string actualStatus = driver.FindElement(By.XPath("//div[@class='ReactVirtualized__Grid__innerScrollContainer']/div[" + i + "]/div[7]")).Text;
                        Assert.Equal(status, actualStatus);
                        break;
                    }
                }
                Assert.True(myList.Contains(statusid), Const.StatusIdNotFound);
            }
            catch (Exception)
            {
                throw;
            }

        }

        //This method will click in the order in tradeOrder History list
        public void UserNameTabUnderUserVerification(string username)
        {
            try
            {
                int accountArrayList = driver.FindElements(elementList).Count;
                for (int i = 1; i <= accountArrayList; i++)
                {
                    IWebElement webElement = driver.FindElement(By.XPath("//div[@class='ReactVirtualized__Table__row'][" + i + "]/div[2]"));
                    string webElementtext = webElement.Text;
                    if (webElementtext.Equals(username))
                    if (webElementtext.Equals(counterPartyID))
                    {
                        Thread.Sleep(2000);
                        Actions action = new Actions(driver);
                        action.DoubleClick(webElement).Build().Perform();
                        Thread.Sleep(2000);
                        break;
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DoubleClickOnCreatedDepositTicket(IWebDriver driver, string statusid)
        {
            try
            {
                int count = driver.FindElements(By.XPath("//div[@class='ReactVirtualized__Grid__innerScrollContainer']/div")).Count;
                for (int i = 1; i <= count - 1; i++)
                {
                    IWebElement we = driver.FindElement(By.XPath("//div[@class='ReactVirtualized__Grid__innerScrollContainer']/div[" + i + "]/div[5]"));
                    string requestcode = we.Text;
                    if (requestcode.Equals(statusid))
                    {
                        GenericUtils.DoubleClick(driver, we);
                        Thread.Sleep(4000);
                        break;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This method will return all list except las 2 column(used by TC_34)
        public ArrayList GetListOfBlockTradeReport()
        {
            ArrayList tradeReportsOrderList = new ArrayList();
            int countOfTradeReports = driver.FindElements(By.XPath("//div[@class='ReactVirtualized__Table__row']")).Count;

            for (int i = 1; i <= countOfTradeReports; i++)
            {
                String textFinal = "";
                int countItems = driver.FindElements(By.XPath("(//div[@class='ReactVirtualized__Table__row'])[" + i + "]/div")).Count;
                for (int j = 2; j <= (countItems) - 3; j++)
                {
                    String text = driver.FindElement(By.XPath("(//div[@class='ReactVirtualized__Table__row'])[" + i + "]/div[" + j + "]")).Text;
                    if (j == 2)
                    {
                        textFinal = text;
                    }
                    else
                    {
                        textFinal = textFinal + " || " + text;
                    }

                }
                tradeReportsOrderList.Add(textFinal);
            }
            return tradeReportsOrderList;
        }

        //This method return the buy block trade list and perfom click action on it(Used for TC_34)
        public bool VerifyBlockTradeList(string accountTypeID, string counterPartyID, string status, string instrument, string side, string originalQuantity)
        {
            try
            {
                bool flag = false;
                string originalQtyValue;

                originalQtyValue = GenericUtils.ConvertToDoubleFormat(Double.Parse(originalQuantity));

                string expectedRow_1 = accountTypeID + " || " + counterPartyID + " || " + status + " || " + instrument + " || " + side + " || " + originalQtyValue;
                string expectedRow_2 = counterPartyID + " || " + counterPartyID + " || " + status + " || " + instrument + " || " + side + " || " + originalQtyValue;

                var tradeReportsOrderList = GetListOfBlockTradeReport();
                if (tradeReportsOrderList.Contains(expectedRow_1) && tradeReportsOrderList.Contains(expectedRow_2))
                {
                    flag = true;
                }
                if (flag)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedBlockTradeTradesPassed, status, side));
                }
                else
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedBlockTradeTradesFailed, status, side));
                }
                return flag;
            }
            catch (Exception)
            {
                throw;
            }
        }


        //Verify all permission in "edit user permissions" section modal window
        public bool VerifyUserPermissionsList()
        {
            int countItems;
            bool checkBoxExists;
            bool flag = false;
            try
            {
                countItems = driver.FindElements(By.XPath("//div[@class='form-group']//input")).Count;
                IList ListOfItems = driver.FindElements(By.XPath("//div[@class='form-group']//input[@type='checkbox']"));
                for (int i = 1; i <= countItems; i++)
                {
                    checkBoxExists =  driver.FindElement(By.XPath("//div[@class='form account-form']//input[@placeholder='Search...']//following::div["+i+"]//input")).Displayed;
                    if (checkBoxExists)
                    {
                        flag = true;                 
                    }                  
                }
                return flag;
            }            
            catch (Exception)
            {
                throw;
            }
        }

        // Users Scroll to select a User from the list
        public string ScrollAndSelectUser(string userName)
        {
            string userNameText;
            string userId = null;
            string totalUsersText;
            int totalCount;
            int count;
            Actions actions = new Actions(driver);
            By totalUsersCount = By.XPath("//div[@class='ReactVirtualized__Grid__innerScrollContainer']/div[1]/div[1]");
            ClickOnUsersMenuLink();
            UsersTabBtn();
            totalUsersText = driver.FindElement(totalUsersCount).Text;
            totalCount = Int32.Parse(totalUsersText);
            EventFiringWebDriver evw = new EventFiringWebDriver(driver);
            count = totalCount / 8;
            for (int j = 1; j <= count; j++)
            {
                for (int i = 1; i <= 8; i++)
                {
                    IWebElement userIdElement = driver.FindElement(By.XPath("//div[@class='ReactVirtualized__Grid__innerScrollContainer']/div[" + i + "]/div[1]"));
                    IWebElement userNameElement = driver.FindElement(By.XPath("//div[@class='ReactVirtualized__Grid__innerScrollContainer']/div[" + i + "]/div[2]"));
                    userNameText = userNameElement.Text;
                    userId = userIdElement.Text;
                    if (userNameText.Equals(userName))
                    {
                        actions.DoubleClick(userNameElement).Build().Perform();
                        return userId;
                        //goto Found;
                    }
                }
                var queryString = "document.querySelector('div.ReactVirtualized__Grid.ReactVirtualized__Table__Grid').scrollTop=";
                evw.ExecuteScript(queryString + (j * 352));
                Thread.Sleep(1000);
            }
            //Found: Console.WriteLine("Out");
            return userId;
        }

        // This method select a user from Admin UI and returns User Details 
        public Dictionary<string, string> GetUserDetails(string userName)
        {
            string userId;
            Dictionary<string, string> userDetailsDict = new Dictionary<string, string>();
            userId = ScrollAndSelectUser(userName);
            userDetailsDict.Add("UserId", userId);
            userDetailsDict.Add("AccountId", DefaultAccountID().Text);
            return userDetailsDict;
        }


    }
}
