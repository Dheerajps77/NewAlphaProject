using AlphaPoint_QA.Common;
using AlphaPoint_QA.Utils;
using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Xunit;

namespace AlphaPoint_QA.Pages
{
    class AdminUsersPage
    {
        ProgressLogger logger;
        static Config data;
        public static IWebDriver driver;

        public AdminUsersPage(ProgressLogger logger)
        {
            this.logger = logger;
            data = ConfigManager.Instance;
            driver = AlphaPointWebDriver.GetInstanceOfAlphaPointWebDriver();
        }

        By userNameFromList = By.CssSelector("div.ReactVirtualized__Grid.ReactVirtualized__Table__Grid div:nth-of-type(1) div:nth-of-type(1) div:nth-of-type(2)");
        By depositPermissionText = By.XPath("//p[text()='User permissions']//following::button[@id='OpenAddPermissionForm']//div//following::table[1]//tbody//tr[1]//td[1]");
        By tradingPermissionText = By.XPath("//p[text()='User permissions']//following::button[@id='OpenAddPermissionForm']//div//following::table[1]//tbody//tr[2]//td[1]");
        By withdrawPermissionText = By.XPath("//p[text()='User permissions']//following::button[@id='OpenAddPermissionForm']//div//following::table[1]//tbody//tr[3]//td[1]");
        By accountName = By.XPath("//p[text()='User Accounts']//following::table[1]//tbody//tr[1]//td[1]");
        By verificationStatus = By.CssSelector("div.details_container table:nth-of-type(1) tbody tr:nth-of-type(2) td:nth-of-type(2) span:nth-of-type(2)");
        By revokePermissionButton = By.XPath("//p[text()='User permissions']//following::button[@id='OpenAddPermissionForm']//div//span//following::table[1]//tbody//tr[2]//td[2]//a");
        By invalidAccountIDMsg = By.XPath("//div[@class='form-group']//input[@name='AccountId']//following::div[1]//p");
        By viewAllButton = By.CssSelector("button[id='FetchAllUsers']");
        By userIdTextField = By.CssSelector("input[name='UserId']");
        By accountIdTextField = By.CssSelector("input[name='AccountId']");
        By userNameTextField = By.CssSelector("input[name='Username']");
        By emailTextField = By.CssSelector("input[name='Email']");
        By userAPIKeyCheckBoxButtonList = By.CssSelector("div.form.account-form>form>div.form-group>input");
        By submitButton = By.XPath("//button[@class='primary' and @type='submit']");
        By copyKeyButton = By.CssSelector("#app > div > div > div:nth-of-type(1) > div:nth-of-type(1) > div > div > div > div > div:nth-of-type(2) > section:nth-of-type(3) > div:nth-of-type(2) > div:nth-of-type(1) > div > div:nth-of-type(2) > div > div > div:nth-of-type(4) > div > svg:nth-of-type(1)");
        By deleteKeyButton = By.CssSelector("#app > div > div > div:nth-of-type(1) > div:nth-of-type(1) > div > div > div > div > div:nth-of-type(2) > section:nth-of-type(3) > div:nth-of-type(2) > div:nth-of-type(1) > div > div:nth-of-type(2) > div > div > div:nth-of-type(4) > div > svg:nth-of-type(2)");
        By userkeyMsg = By.CssSelector("#messages > div > div > div > span");
        By deleteKeyYesButton = By.XPath("//article[@class='mm-popup__box']//div[@class='mm-popup__box__body']//following::footer//div[2]//button");
        By addAPIKeyButton = By.CssSelector("button#OpenAPIKeysForm");
        By copiedAPIKeyValue = By.CssSelector("div.ReactVirtualized__Table__row div:nth-of-type(1)>span");
        By usersList = By.CssSelector("div.ReactVirtualized__Grid__innerScrollContainer>div");
        By userIdList = By.CssSelector("div.ReactVirtualized__Grid__innerScrollContainer > div > div:nth-of-type(1)");
        By userNameList = By.CssSelector("div.ReactVirtualized__Grid__innerScrollContainer > div > div:nth-of-type(2)");
        By emailList = By.CssSelector("div.ReactVirtualized__Grid__innerScrollContainer > div > div:nth-of-type(3)");
        By accountidList = By.CssSelector("div.ReactVirtualized__Grid__innerScrollContainer > div > div:nth-of-type(4)");
        By openAssignAccountButton = By.CssSelector("button#OpenAssignAccountForm");
        By openUnassignAccountButton = By.CssSelector("button#OpenUnassignAccountForm");
        By exportButton = By.CssSelector("button#OpenUsersExportMenu");
        By listOfSelectOptionToExport = By.XPath("/html/body/div[3]/div/div/div/div/div/div");
        By selectAllUsers = By.CssSelector("html > body > div:nth-of-type(3) > div > div > div > div > div > div:nth-of-type(1)");
        By selectSuperusers = By.CssSelector("html > body > div:nth-of-type(3) > div > div > div > div > div > div:nth-of-type(2)");
        By selectBypermissions = By.CssSelector("html > body > div:nth-of-type(3) > div > div > div > div > div > div:nth-of-type(3)");
        By allUsersText = By.CssSelector("html > body > div:nth-of-type(3) > div > div > div > div > div > div:nth-of-type(1) span > div > div");
        By superusersText = By.CssSelector("html > body > div:nth-of-type(3) > div > div > div > div > div > div:nth-of-type(2) span > div > div");
        By byPermissionsText = By.CssSelector("html > body > div:nth-of-type(3) > div > div > div > div > div > div:nth-of-type(3) span > div > div");
        By selectPermissions = By.CssSelector("div.select>select");
        By exportUsersByPermissionsFormButton = By.CssSelector("button#SubmitExportUsersByPermissionsForm");
        By closeAccountWindow = By.CssSelector("a[id='CloseModal']");
        By countOfPermissionList = By.XPath("//div[@class='half_container'][2]//section[1]//table/tbody/tr");

        public IWebElement CloseAccountWindow()
        {
            return driver.FindElement(closeAccountWindow);
        }
        public IWebElement SelectPermissions()
        {
            return driver.FindElement(selectPermissions);
        }
        public IWebElement SelectAllUsers()
        {
            return driver.FindElement(selectAllUsers);
        }
        public IWebElement SelectSuperusers()
        {
            return driver.FindElement(selectSuperusers);
        }
        public IWebElement SelectBypermissions()
        {
            return driver.FindElement(selectBypermissions);
        }

        public IWebElement AllUsersText()
        {
            return driver.FindElement(allUsersText);
        }

        public IWebElement SuperusersText()
        {
            return driver.FindElement(superusersText);
        }

        public IWebElement ByPermissionsText()
        {
            return driver.FindElement(byPermissionsText);
        }

        public IWebElement ClickOnExportButton()
        {
            return driver.FindElement(exportButton);
        }

        public IWebElement OpenAssignAccountButton()
        {
            return driver.FindElement(openAssignAccountButton);
        }

        public IWebElement OpenUnassignAccountButton()
        {
            return driver.FindElement(openUnassignAccountButton);
        }

        public IWebElement AddAPIKeyButton()
        {
            return driver.FindElement(addAPIKeyButton);
        }

        public IWebElement CopiedAPIKeyValue()
        {
            return driver.FindElement(copiedAPIKeyValue);
        }

        public IWebElement DeleteKeyYesButton()
        {
            return driver.FindElement(deleteKeyYesButton);
        }

        public IWebElement UserkeyMsg()
        {
            return driver.FindElement(userkeyMsg);
        }

        public IWebElement UserAPIKeyCheckBoxButtonList()
        {
            return driver.FindElement(userAPIKeyCheckBoxButtonList);
        }

        public IWebElement ClickOnCreateButton()
        {
            return driver.FindElement(submitButton);
        }

        public IWebElement ClickOnCopyKeyButton()
        {
            return driver.FindElement(copyKeyButton);
        }

        public IWebElement ClickOnDeleteKeyButton()
        {
            return driver.FindElement(deleteKeyButton);
        }
        public IWebElement ViewAllButton()
        {
            return driver.FindElement(viewAllButton);
        }

        public IWebElement UserIdTextField()
        {
            return driver.FindElement(userIdTextField);
        }

        public IWebElement AccountIdTextField()
        {
            return driver.FindElement(accountIdTextField);
        }

        public IWebElement UserNameTextField()
        {
            return driver.FindElement(userNameTextField);
        }

        public IWebElement EmailTextField()
        {
            return driver.FindElement(emailTextField);
        }

        public IWebElement InvalidAccountIDMsg()
        {
            return driver.FindElement(invalidAccountIDMsg);
        }

        public IWebElement RevokePermissionButton()
        {
            return driver.FindElement(revokePermissionButton);
        }

        public IWebElement UserNameFromList()
        {
            return driver.FindElement(userNameFromList);
        }

        public IWebElement VerificationStatus()
        {
            return driver.FindElement(verificationStatus);
        }

        public IWebElement AccountName()
        {
            return driver.FindElement(accountName);
        }

        public IWebElement DepositPermissionText()
        {
            return driver.FindElement(depositPermissionText);
        }

        public IWebElement TradingPermissionText()
        {
            return driver.FindElement(tradingPermissionText);
        }

        public IWebElement WithdrawPermissionText()
        {
            return driver.FindElement(withdrawPermissionText);
        }

        //This method will click on newly created user row
        public void ClickOnUserCreatedRow()
        {
            try
            {
                GenericUtils.DoubleClick(driver, UserNameFromList());
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This method will click on account name under "User Accounts" section
        public void ClickOnAccountName()
        {
            try
            {
                GenericUtils.DoubleClick(driver, AccountName());
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Click on "ViewAll" button under userTab
        public void ClickOnViewAllButton()
        {
            try
            {
                GenericUtils.WaitForElementClickable(driver, viewAllButton, 15).Click();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Enter email in textfield under userTab
        public void EnterEmail(string email)
        {
            try
            {
                UserSetFunctions.EnterText(EmailTextField(), email);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Enter username in textfield under userTab
        public void EnterUserName(string userName)
        {
            try
            {
                UserSetFunctions.EnterText(UserNameTextField(), userName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Enter accountId in textfield under userTab
        public void EnterAcountId(string accountId)
        {
            try
            {
                UserSetFunctions.EnterText(AccountIdTextField(), accountId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Enter userId in textfield under userTab
        public void EnterUserId(string userId)
        {
            try
            {
                UserSetFunctions.EnterText(UserIdTextField(), userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This method will return all the values under user tab
        public bool UserIdResultsLoad(string textValue, string userIdTextValue)
        {
            IWebElement userIdFromListElement;
            string userIdToSelect;
            bool flag = false;
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(logger);

            try
            {

                UserSetFunctions.Clear(EmailTextField());
                UserSetFunctions.Clear(UserNameTextField());
                Thread.Sleep(1000);
                EnterUserId(textValue);
                objAdminCommonFunctions.ClickOnRefreshUserTableButton();
                UserSetFunctions.Clear(EmailTextField());
                UserSetFunctions.Clear(UserNameTextField());

                GenericUtils.WaitForElementPresence(driver, usersList, 15);

                int count = driver.FindElements(usersList).Count;
                int userIdListcount = driver.FindElements(userIdList).Count;

                if (count.Equals(userIdListcount))
                {
                    logger.LogCheckPoint(String.Format(LogMessage.VerifyUsersPageLoadInUsersTablePassed, textValue, userIdTextValue));
                }
                else
                {
                    logger.LogCheckPoint(String.Format(LogMessage.VerifyUsersPageLoadInUsersTableFailed, textValue, userIdTextValue));
                }

                for (int i = 1; i <= count; i++)
                {
                    userIdFromListElement = driver.FindElement(By.XPath("//div[@class='ReactVirtualized__Grid__innerScrollContainer']/div[" + i + "]/div[1]"));
                    userIdToSelect = userIdFromListElement.Text;
                    if (userIdToSelect.StartsWith(textValue) || userIdToSelect.Equals(textValue))
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                        break;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }

        //This method will verify if userid results loaded dynamically under user Tab
        public bool VerifyUserIdResultsLoad(string textValue, string UserIdTextValue)
        {
            bool flag = false;

            try
            {
                if (UserIdResultsLoad(textValue, UserIdTextValue))
                {
                    flag = true;
                }
                if (flag)
                {
                    logger.LogCheckPoint(String.Format(LogMessage.VerifyFilteredValuesInUsersTablePassed, UserIdTextValue));
                    logger.LogCheckPoint(String.Format(LogMessage.VerifyFilteredByPassed, UserIdTextValue));
                }
                else
                {
                    logger.LogCheckPoint(String.Format(LogMessage.VerifyFilteredValuesInUsersTableFailed, UserIdTextValue));
                }
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }


        //This method will return all the username values under user tab
        public bool UserNameResultsLoad(string textValue, string userNameTextValue)
        {
            IWebElement userNameFromListElement;
            string userNameToSelect;
            bool flag = false;
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(logger);

            try
            {
                UserSetFunctions.Clear(UserIdTextField());
                UserSetFunctions.Clear(EmailTextField());
                objAdminCommonFunctions.ClickOnRefreshUserTableButton();
                Thread.Sleep(1000);
                EnterUserName(textValue);

                UserSetFunctions.Clear(UserIdTextField());
                UserSetFunctions.Clear(EmailTextField());

                GenericUtils.WaitForElementPresence(driver, usersList, 15);
                int count = driver.FindElements(usersList).Count;

                int userNameListcount = driver.FindElements(userNameList).Count;

                if (count.Equals(userNameListcount))
                {
                    logger.LogCheckPoint(String.Format(LogMessage.VerifyUsersPageLoadInUsersTablePassed, textValue, userNameTextValue));
                }
                else
                {
                    logger.LogCheckPoint(String.Format(LogMessage.VerifyUsersPageLoadInUsersTableFailed, textValue, userNameTextValue));
                }

                for (int i = 1; i <= count; i++)
                {
                    userNameFromListElement = driver.FindElement(By.XPath("//div[@class='ReactVirtualized__Grid__innerScrollContainer']/div[" + i + "]/div[2]"));
                    userNameToSelect = userNameFromListElement.Text;

                    if (userNameToSelect.StartsWith(textValue) || userNameToSelect.Equals(textValue))
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                        break;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }

        //This method will verify if username results loaded dynamically under user Tab
        public bool VerifyUserNameResultsLoad(string textValue, string userNameTextValue)
        {
            bool flag = false;
            try
            {
                if (UserNameResultsLoad(textValue, userNameTextValue))
                {
                    flag = true;
                }
                if (flag)
                {
                    logger.LogCheckPoint(String.Format(LogMessage.VerifyFilteredValuesInUsersTablePassed, userNameTextValue));
                    logger.LogCheckPoint(String.Format(LogMessage.VerifyFilteredByPassed, userNameTextValue));

                }
                else
                {
                    logger.LogCheckPoint(String.Format(LogMessage.VerifyFilteredValuesInUsersTableFailed, userNameTextValue));
                }
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }

        //This method will return all the email values under user tab
        public bool EmailResultsLoad(string textValue, string emailTextValue)
        {
            IWebElement emailFromListElement;
            string emailToSelect;
            bool flag = false;
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(logger);

            try
            {
                UserSetFunctions.Clear(UserIdTextField());
                UserSetFunctions.Clear(UserNameTextField());
                Thread.Sleep(1000);
                objAdminCommonFunctions.ClickOnRefreshUserTableButton();
                UserSetFunctions.Clear(UserIdTextField());
                UserSetFunctions.Clear(UserNameTextField());
                EnterEmail(textValue);
                UserSetFunctions.Clear(UserIdTextField());
                UserSetFunctions.Clear(UserNameTextField());

                GenericUtils.WaitForElementPresence(driver, usersList, 15);

                int count = driver.FindElements(usersList).Count;
                int emailListcount = driver.FindElements(emailList).Count;

                if (count.Equals(emailListcount))
                {
                    logger.LogCheckPoint(String.Format(LogMessage.VerifyUsersPageLoadInUsersTablePassed, textValue, emailTextValue));
                }
                else
                {
                    logger.LogCheckPoint(String.Format(LogMessage.VerifyUsersPageLoadInUsersTableFailed, textValue, emailTextValue));
                }

                for (int i = 1; i <= count; i++)
                {
                    emailFromListElement = driver.FindElement(By.XPath("//div[@class='ReactVirtualized__Grid__innerScrollContainer']/div[" + i + "]/div[3]"));
                    emailToSelect = emailFromListElement.Text;

                    if (emailToSelect.StartsWith(textValue) || emailToSelect.Equals(textValue))
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                        break;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }

        //This method will verify if email results loaded dynamically under user Tab
        public bool VerifyEmailResultsLoad(string textValue, string emailTextValue)
        {
            bool flag = false;

            try
            {
                if (EmailResultsLoad(textValue, emailTextValue))
                {
                    flag = true;
                }
                if (flag)
                {
                    logger.LogCheckPoint(String.Format(LogMessage.VerifyFilteredValuesInUsersTablePassed, emailTextValue));
                    logger.LogCheckPoint(String.Format(LogMessage.VerifyFilteredByPassed, emailTextValue));

                }
                else
                {
                    logger.LogCheckPoint(String.Format(LogMessage.VerifyFilteredValuesInUsersTableFailed, emailTextValue));
                }
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }


        //This method will return all the email values under user tab
        public bool AccountIdResultsLoad(string textValue, string accountIdTextValue)
        {
            IWebElement accountIdFromListElement;
            string accountIdToSelect;
            bool flag = false;

            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(logger);

            try
            {
                UserSetFunctions.Clear(UserIdTextField());
                UserSetFunctions.Clear(UserNameTextField());
                UserSetFunctions.Clear(EmailTextField());
                Thread.Sleep(1000);
                objAdminCommonFunctions.ClickOnRefreshUserTableButton();
                UserSetFunctions.Clear(UserIdTextField());
                UserSetFunctions.Clear(UserNameTextField());
                UserSetFunctions.Clear(EmailTextField());
                EnterAcountId(textValue);
                UserSetFunctions.Clear(UserIdTextField());
                UserSetFunctions.Clear(UserNameTextField());
                UserSetFunctions.Clear(EmailTextField());

                GenericUtils.WaitForElementPresence(driver, usersList, 15);

                int count = driver.FindElements(usersList).Count;
                int accountIdListcount = driver.FindElements(accountidList).Count;

                if (count.Equals(accountIdListcount))
                {
                    logger.LogCheckPoint(String.Format(LogMessage.VerifyUsersPageLoadInUsersTablePassed, textValue, accountIdTextValue));
                }
                else
                {
                    logger.LogCheckPoint(String.Format(LogMessage.VerifyUsersPageLoadInUsersTableFailed, textValue, accountIdTextValue));
                }

                for (int i = 1; i <= count; i++)
                {
                    accountIdFromListElement = driver.FindElement(By.XPath("//div[@class='ReactVirtualized__Grid__innerScrollContainer']/div[" + i + "]/div[5]"));
                    accountIdToSelect = accountIdFromListElement.Text;

                    if (accountIdToSelect.StartsWith(textValue) || accountIdToSelect.Equals(textValue))
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                        break;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }

        //This method will verify if email results loaded dynamically under user Tab
        public bool VerifyAccountResultsLoad(string textValue, string accountIdTextValue)
        {
            bool flag = false;

            try
            {
                if (AccountIdResultsLoad(textValue, accountIdTextValue))
                {
                    flag = true;
                }
                if (flag)
                {
                    logger.LogCheckPoint(String.Format(LogMessage.VerifyFilteredValuesInUsersTablePassed, accountIdTextValue));
                    logger.LogCheckPoint(String.Format(LogMessage.VerifyFilteredByPassed, accountIdTextValue));

                }
                else
                {
                    logger.LogCheckPoint(String.Format(LogMessage.VerifyFilteredValuesInUsersTableFailed, accountIdTextValue));
                }
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }


        //This method will click on newly created user row
        public void ClickOnRevokePermissionButton()
        {
            try
            {
                UserCommonFunctions.ScrollingDownVertical(driver);
                UserSetFunctions.Click(RevokePermissionButton());
                logger.LogCheckPoint(String.Format(LogMessage.VerifyRevokeUserPermissionSuccessMessagePassed));
            }
            catch (Exception)
            {
                logger.LogCheckPoint(String.Format(LogMessage.VerifyRevokeUserPermissionSuccessMessageFailed));
                throw;
            }
        }

        //This will create new user and verify if created user successful
        public bool CreateNewUser(string userName, string userPassword, string userConfirmPassword, string verificationLevel)
        {
            string createdUserName;
            string randomString;
            string email;
            string randomUserName;
            string emailDomain;
            string userNameWithSpaces;
            string emailWithSpaces;
            string verified;
            string createdUserNameWithoutWhiteSpace;
            string accountNameFromSite;

            bool flag = false;
            AdminFunctions objAdminFunctions = new AdminFunctions(logger);
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(logger);

            emailDomain = Const.EmailDomain;
            randomString = GenericUtils.RandomString(Const.RandomStringLength);
            email = randomString + emailDomain;
            randomUserName = userName + randomString;
            userNameWithSpaces = Const.AddWhiteSpace + randomUserName + Const.AddWhiteSpace;
            emailWithSpaces = Const.AddWhiteSpace + email + Const.AddWhiteSpace;
            verified = Const.verifiedPassedMsg;

            try
            {

                createdUserName = objAdminCommonFunctions.AddNewUser(userNameWithSpaces, emailWithSpaces, userPassword, userConfirmPassword);
                logger.LogCheckPoint(String.Format(LogMessage.KYCStarted, createdUserName.Trim()));
                objAdminCommonFunctions.ClickOnUsersMenuLink();
                objAdminCommonFunctions.SelectUserFromUserList(driver, createdUserName);
                // Edit the User Email Verified status to checked
                objAdminCommonFunctions.EditUserEmailStatus(createdUserName);
                // Scroll don to User Accounts section and open the user account
                UserCommonFunctions.ScrollingDownVertical(driver);
                objAdminCommonFunctions.OpenAccountFromUserPage();
                // Edit the Verification Level of the user to 3
                objAdminCommonFunctions.EditUserVerificationLevel(verificationLevel);
                logger.LogCheckPoint(String.Format(LogMessage.KYCSuccess, createdUserName.Trim()));
                createdUserNameWithoutWhiteSpace = createdUserName.Trim();
                accountNameFromSite = objAdminCommonFunctions.AccountNameTextValue();
                if (accountNameFromSite.Equals(createdUserNameWithoutWhiteSpace))
                {
                    flag = true;
                }
                if (flag)
                {
                    logger.LogCheckPoint(String.Format(LogMessage.AddNewUserPassed, randomUserName, email));
                }
                else
                {
                    logger.LogCheckPoint(String.Format(LogMessage.AddNewUserFailed));
                }
                objAdminCommonFunctions.ClickOnUsersMenuLink();
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }

        //This will verify if created user with permission deposit, trading and withdraw under user permission
        public bool VerifyUserCreatedWithPermission(string depositPermission, string tradingPermission, string withdrawPermission)
        {
            string depositPermissionTextValue;
            string tradingPermissionTextValue;
            string withdrawPermissionTextValue;

            depositPermissionTextValue = DepositPermissionText().Text;
            tradingPermissionTextValue = TradingPermissionText().Text;
            withdrawPermissionTextValue = WithdrawPermissionText().Text;

            bool flag = false;

            try
            {
                if (depositPermissionTextValue.Equals(depositPermission) && tradingPermissionTextValue.Equals(tradingPermission)
                     && withdrawPermissionTextValue.Equals(withdrawPermission))
                {
                    flag = true;
                }

                if (flag)
                {
                    logger.LogCheckPoint(String.Format(LogMessage.NewUserCreatedWithPermissionPassed, depositPermissionTextValue, tradingPermissionTextValue, withdrawPermissionTextValue));
                }

                else
                {
                    logger.LogCheckPoint(String.Format(LogMessage.NewUserCreatedWithPermissionFailed));
                }
            }
            catch (Exception)
            {
                throw;
            }

            return flag;
        }

        //Login as admin -> Click on "Users" menu button -> Users Tab
        public void SelectAdminUserTab()
        {
            AdminFunctions objAdminFunctions = new AdminFunctions(logger);
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(logger);

            try
            {
                objAdminFunctions.AdminLogIn(logger);
                objAdminCommonFunctions.ClickOnUsersMenuLink();
                objAdminCommonFunctions.UsersTabBtn();
            }
            catch (Exception)
            {
                throw;
            }
        }

        By accountIdValue = By.CssSelector("div.ReactVirtualized__Grid__innerScrollContainer div.ReactVirtualized__Table__row:nth-of-type(2) div:nth-of-type(5)");

        public IWebElement AccountIdValue()
        {
            return driver.FindElement(accountIdValue);
        }

        //This method will select an user -> verify the assign account and unassign account
        public bool SelectUser()
        {

            bool flag = false;
            string userName;
            string userAccountID;
            string accountIdTextValue;
            string accountIdConversionToString;
            int acocuntIdConversionToNumber;
            int addOneNumberToAccountId;

            accountIdTextValue = AccountIdValue().Text;
            acocuntIdConversionToNumber = Int32.Parse(accountIdTextValue);
            addOneNumberToAccountId = acocuntIdConversionToNumber + 1;
            accountIdConversionToString = addOneNumberToAccountId.ToString();

            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(logger);

            try
            {
                //This will get the user name and user account ID from the user list under Users Tab
                userName = objAdminCommonFunctions.getUserNameFromUserList();

                //Need to work here to get account id from UI               
                userAccountID = objAdminCommonFunctions.getUserAccountIDFromUserList();


                //select an user
                objAdminCommonFunctions.SelectUserFromUserList(driver, userName);

                if (OpenAssignAccountButton().Enabled && OpenUnassignAccountButton().Enabled)
                {
                    flag = true;
                    //Verify the Assigned account under User Accounts section on admin ui
                    objAdminCommonFunctions.VerifyAssignAccountButton();
                    //Click on "Unassign Account" button
                    objAdminCommonFunctions.ClickOnUnassignAccountButton();
                    //Select an account ID to unassign
                    objAdminCommonFunctions.SelectAnAccountID(userAccountID);
                    return flag;
                }
                else if (!OpenUnassignAccountButton().Enabled)
                {
                    flag = true;
                    //Verify the Unassigned account under User Accounts section on admin ui
                    objAdminCommonFunctions.VerifyUnassignAccountButton();
                    //Click on "Assign Account" button
                    objAdminCommonFunctions.ClickOnAssignAccountButton();
                    //Select an account ID to unassign
                    objAdminCommonFunctions.EnterAnAccountID(accountIdConversionToString);
                    return flag;
                }

            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }
        //This will verify the login after unassign the account
        public bool VerifyLoginUsingUnassignedAccount(string userPassword)
        {
            string userName;
            string userAccountID;

            bool flag = false;
            AdminFunctions objAdminFunctions = new AdminFunctions(logger);
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(logger);
            UserFunctions userfuntionality = new UserFunctions(logger);

            try
            {
                //This will get the user name and user account ID from the user list under Users Tab
                userName = objAdminCommonFunctions.getUserNameFromUserList();
                userAccountID = objAdminCommonFunctions.getUserAccountIDFromUserList();
                if (SelectUser())
                {
                    //This will click on "Unassign" button
                    objAdminCommonFunctions.ClickOnSubmitAssignOrUnassignAccountButton();

                    //Verify the Unassigned account under User Accounts section on admin ui                       
                    if (objAdminCommonFunctions.VerifyUnassignAccountButton())
                    {
                        flag = true;
                    }
                    if (flag)
                    {
                        logger.LogCheckPoint(String.Format(LogMessage.VerifyUnassignAccountPassed));
                    }
                    else
                    {
                        logger.LogCheckPoint(String.Format(LogMessage.VerifyUnassignAccountFailed));
                    }
                    //Login to user portal site and verify if user getting log in after unassigining the account ID
                    objAdminCommonFunctions.UserMenuBtn();
                    objAdminFunctions.AdminLogOut();
                    userfuntionality.LogIn(logger, changeServerOnly: true);
                    Assert.True(userfuntionality.LogInUsingCredsAfterUnassignAccount(logger, userName, userPassword));
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

        //This will verify the login after assign the account
        public bool VerifyLoginUsingAssignedAccount(string userPassword)
        {
            string userName;
            string userAccountID;
            AdminFunctions objAdminFunctions = new AdminFunctions(logger);
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(logger);
            UserFunctions userfuntionality = new UserFunctions(logger);

            bool flag = false;
            try
            {
                //This will get the user name and user account ID from the user list under Users Tab
                userName = objAdminCommonFunctions.getUserNameFromUserList();
                userAccountID = objAdminCommonFunctions.getUserAccountIDFromUserList();
                if (SelectUser())
                {
                    //This will click on "Assign" button to submit account ID
                    objAdminCommonFunctions.ClickOnSubmitAssignOrUnassignAccountButton();

                    //Verify the assigned account under User Accounts section on admin ui

                    if (objAdminCommonFunctions.VerifyAssignAccountButton())
                    {
                        flag = true;
                    }
                    if (flag)
                    {
                        logger.LogCheckPoint(String.Format(LogMessage.VerifyAssignAccountPassed));
                    }
                    else
                    {
                        logger.LogCheckPoint(String.Format(LogMessage.VerifyAssignAccountFailed));
                    }
                    //Login to user portal site and verify if user getting log in after assigining the account ID
                    objAdminCommonFunctions.UserMenuBtn();
                    objAdminFunctions.AdminLogOut();
                    userfuntionality.LogIn(logger, changeServerOnly: true);
                    Assert.True(userfuntionality.LogInUsingCredsAfterAssignAccount(logger, userName, userPassword));
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
        
        //This will verify an invalid-nonexistent account should show an error
        public bool VerifyNonexistentAccount(string invalidAccountID)
        {
            string userName;
            string invalidAccountIDTextMsg;
            bool flag = false;

            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(logger);

            try
            {
                //This will get the user name from the user list under Users Tab
                userName = objAdminCommonFunctions.getUserNameFromUserList();

                //select an user
                objAdminCommonFunctions.SelectUserFromUserList(driver, userName);

                //Click on "Assign Account" button
                objAdminCommonFunctions.ClickOnAssignAccountButton();

                //Enter an invalid-nonexistent account 
                objAdminCommonFunctions.EnterAnAccountID(invalidAccountID);

                //This will click on "Assign" button to submit account ID
                objAdminCommonFunctions.ClickOnSubmitAssignOrUnassignAccountButton();

                //This will capture the error message on entering an invalid account id
                //invalidAccountIDTextMsg = InvalidAccountIDMsg().Text;
                // if (invalidAccountIDTextMsg.Equals(Const.InvalidAccountIDErrorMsg))
                if (InvalidAccountIDMsg().Enabled)
                {
                    flag = true;
                }
                if (flag)
                {
                    logger.LogCheckPoint(String.Format(LogMessage.VerifyInvalidNonExistentAccountIDPassed, invalidAccountID));
                    logger.LogCheckPoint(String.Format(LogMessage.VerifyAssignedOrUnassignedAccountPassed));
                }
                else
                {
                    logger.LogCheckPoint(String.Format(LogMessage.VerifyInvalidNonExistentAccountIDFailed, invalidAccountID));

                }
                Thread.Sleep(1000);
                UserSetFunctions.Click(CloseAccountWindow());
                Thread.Sleep(1000);
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }

        //This method will select the permission accordingly
        public void SelectUsersPermissions(string permissionName)
        {
            try
            {
                GenericUtils.SelectDropDownByText(driver, selectPermissions, permissionName);
                GenericUtils.WaitForElementClickable(driver, exportUsersByPermissionsFormButton, 15).Click();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This will select an permission to create an key
        public bool ChoosePermissionForKey()
        {
            IList list;
            int count;
            bool checkBoxExists, flag = false;
            IWebElement element;
            try
            {
                count = driver.FindElements(userAPIKeyCheckBoxButtonList).Count;
                list = driver.FindElements(userAPIKeyCheckBoxButtonList);

                for (int i = 1; i <= count; i++)
                {

                    element = driver.FindElement(By.XPath("//div[@class='form account-form']//form//div[@class='form-group'][" + i + "]//input"));
                    checkBoxExists = element.Selected;
                    if (!checkBoxExists)
                    {
                        element.Click();
                        Thread.Sleep(1000);
                    }
                }
                flag = true;
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }

        //Verify the API keys created on user permission
        public bool VerifyAPIKeys()
        {
            string addUserkeyTextMsg;
            string copyUserkeyTextMsg;
            string deleteUserkeyTextMsg;
            string copiedAPIKeyTextValue;

            bool flag = false;
            try
            {
                UserCommonFunctions.ScrollingDownVertical(driver);
                GenericUtils.WaitForElementClickable(driver, addAPIKeyButton, 15).Click();
                if (ChoosePermissionForKey())
                {
                    GenericUtils.WaitForElementClickable(driver, submitButton, 15).Click();

                    addUserkeyTextMsg = UserkeyMsg().Text;
                    if (addUserkeyTextMsg.Equals(Const.AddUserKeySuccessMessage))
                    {
                        flag = true;
                    }

                    if (flag)
                    {
                        logger.LogCheckPoint(String.Format(LogMessage.VerifyAddUserKeyPassed));
                    }
                    else
                    {
                        logger.LogCheckPoint(String.Format(LogMessage.VerifyAddUserKeyFailed));
                    }

                    Thread.Sleep(2000);

                    GenericUtils.WaitForElementClickable(driver, copyKeyButton, 15).Click();
                    copyUserkeyTextMsg = UserkeyMsg().Text;
                    copiedAPIKeyTextValue = CopiedAPIKeyValue().Text;

                    if (copyUserkeyTextMsg.Equals(Const.CopyUserKeySuccessMessage))
                    {
                        flag = true;
                    }

                    if (flag)
                    {
                        logger.LogCheckPoint(String.Format(LogMessage.VerifyCopyUserKeyPassed, copiedAPIKeyTextValue));
                    }
                    else
                    {
                        logger.LogCheckPoint(String.Format(LogMessage.VerifyCopyUserKeyFailed));
                    }

                    GenericUtils.WaitForElementClickable(driver, deleteKeyButton, 15).Click();
                    Thread.Sleep(1000);
                    GenericUtils.WaitForElementClickable(driver, deleteKeyYesButton, 15).Click();

                    deleteUserkeyTextMsg = UserkeyMsg().Text;
                    Thread.Sleep(3000);
                    if (deleteUserkeyTextMsg.Equals(Const.DeleteUserKeySuccessMessage))
                    {
                        flag = true;
                    }

                    if (flag)
                    {
                        logger.LogCheckPoint(String.Format(LogMessage.VerifyDeleteUserKeyPassed));
                        logger.LogCheckPoint(String.Format(LogMessage.VerifyCreationDeletionUserKeyPassed));
                    }
                    else
                    {
                        logger.LogCheckPoint(String.Format(LogMessage.VerifyDeleteUserKeyFailed));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }

        //This method will export the user details as per their selection option i.e, "All Users", "Superusers" and "By permissions"
        public bool ExportUserData(string selectExportOption)
        {
            string allusersTextValue;
            string superusersTextValue;
            string byPermissionTextValue;
            bool flag = false;
            try
            {
                allusersTextValue = AllUsersText().Text;
                superusersTextValue = SuperusersText().Text;
                byPermissionTextValue = ByPermissionsText().Text;

                if (allusersTextValue.Equals(selectExportOption))
                {                   
                    GenericUtils.WaitForElementClickable(driver, selectAllUsers, 15).Click();
                    flag = true;                    

                    if (flag)
                    {
                        logger.LogCheckPoint(String.Format(LogMessage.VerifyExportFileGeneratedPassed, allusersTextValue));
                    }
                    else
                    {
                        logger.LogCheckPoint(String.Format(LogMessage.VerifyExportFileGeneratedFailed, allusersTextValue));
                    }
                    return flag;
                }
                else if (superusersTextValue.Equals(selectExportOption))
                {
                    GenericUtils.WaitForElementClickable(driver, selectSuperusers, 15).Click();                    
                    flag = true;

                    if (flag)
                    {
                        logger.LogCheckPoint(String.Format(LogMessage.VerifyExportFileGeneratedPassed, superusersTextValue));
                    }
                    else
                    {
                        logger.LogCheckPoint(String.Format(LogMessage.VerifyExportFileGeneratedFailed, byPermissionTextValue));
                    }
                    return flag;                    
                }
                else if (byPermissionTextValue.Equals(selectExportOption))
                {
                    GenericUtils.WaitForElementClickable(driver, selectBypermissions, 15).Click();                    
                    flag = true;
                    if (flag)
                    {
                        logger.LogCheckPoint(String.Format(LogMessage.VerifyExportFileGeneratedPassed, byPermissionTextValue));
                    }
                    else
                    {
                        logger.LogCheckPoint(String.Format(LogMessage.VerifyExportFileGeneratedFailed, byPermissionTextValue));
                    }
                    return flag;
                }                
                return flag;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This method will verify if "Superusers" export details downloaded
        public bool VerifyExportSuperuser(string selectSuperuserExportOption,string userId)
        {
            bool flag = true;
            ArrayList superusersData;
            ArrayList permissionsList = new ArrayList();
            int countOfPermissionsList;
            string date;
           
            try
            {
                GenericUtils.WaitForElementClickable(driver, exportButton, 15).Click();
                if (ExportUserData(selectSuperuserExportOption))
                {
                    Thread.Sleep(2000);
                    date = GenericUtils.GetCurrentTimeWithHyphen();
                    var path = Directory.GetCurrentDirectory() + "\\DataTest\\Superusers (" + date + ").csv";
                    superusersData = ReadSuperusersDataFromCSV(@path);
                    ClickOnViewAllButton();
                    EnterUserId(userId);
                    ClickOnUserCreatedRow();
                    UserCommonFunctions.ScrollingDownVertical(driver);
                    countOfPermissionsList = driver.FindElements(countOfPermissionList).Count;

                    for (int i = 1; i <= countOfPermissionsList; i++)
                    {
                        string text;                      
                        text = driver.FindElement(By.XPath("//div[@class='half_container'][2]//section[1]//table//tbody//tr[" + i + "]/td[1]")).Text;
                        if (!superusersData.Contains(text))
                        {
                            flag = false;
                        }
                    }
                    if (flag)
                    {
                        logger.LogCheckPoint(String.Format(LogMessage.VerifySuperusersPermissionsPassed, userId));
                        logger.LogCheckPoint(String.Format(LogMessage.VerifySuperusersExportedCSVFilePassed));
                    }
                    else
                    {
                        logger.LogCheckPoint(String.Format(LogMessage.VerifySuperusersPermissionsFailed, userId));
                    }
                }
                return flag;
            }
            catch (Exception)
            {
                throw;
            }          
        }

        //This method will verify if "All users" export details downloaded
        public bool VerifyExportAllusers(string selectAllUsersExportOption, string userIdValue)
        {
            bool flag = false;
            List<KeyValuePair<string, string>> allUsersData;
            string allUsersList = Const.RemoveWhiteSpace;
            string date;
            GenericUtils genericUtils = new GenericUtils(logger);
            try
            {
                GenericUtils.WaitForElementClickable(driver, exportButton, 15).Click();
                if (ExportUserData(selectAllUsersExportOption))
                    {
                    Thread.Sleep(2000);
                    date = GenericUtils.GetCurrentTimeWithHyphen();
                    var path = Directory.GetCurrentDirectory() + "\\DataTest\\AllUsers (" + date + ").csv";
                    allUsersData = ReadAllUsersDataFromCSV(@path);
                    for (int i = 0; i < allUsersData.Count; i++)
                    {
                        if (allUsersData[i].Key == Const.UserId)
                        {
                            flag = true;
                            if (allUsersData[i].Value == userIdValue)
                            {
                                for (int j = i; j < i + 12; j++)
                                {
                                    allUsersList = allUsersList + Const.OrSign + allUsersData[j].Key + Const.SemicolnSign + allUsersData[j].Value;
                                }
                                logger.LogCheckPoint(String.Format(LogMessage.UserDataPresenInCSVFile, userIdValue, allUsersList));
                                break;
                            }                            
                        }
                    }
                    if (flag)
                    {
                        logger.LogCheckPoint(String.Format(LogMessage.VerifyUserIdPassed, userIdValue));
                        logger.LogCheckPoint(String.Format(LogMessage.VerifyAllUsersExportedCSVFilePassed));
                    }
                    else
                    {
                        logger.LogCheckPoint(String.Format(LogMessage.VerifyUserIdFailed, userIdValue));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }

        //This method will verify if "By Permissions" export details downloaded
        public bool VerifyExportByPermission(string selectByPermissionsOption, string userId, string permissionName)
        {
            bool flag = false;
            List<KeyValuePair<string, string>> byPermissionsData;
            string byPermissionsList = Const.RemoveWhiteSpace;
            string date;
            GenericUtils genericUtils = new GenericUtils(logger);

            try
            {
                GenericUtils.WaitForElementClickable(driver, exportButton, 15).Click();
                if (ExportUserData(selectByPermissionsOption))
                {
                    SelectUsersPermissions(permissionName);
                    Thread.Sleep(2000);
                    date = GenericUtils.GetCurrentTimeWithHyphen();
                    var path = Directory.GetCurrentDirectory() + "\\DataTest\\Filtered Users (" + date + ").csv";
                    byPermissionsData = ReadByPermissionsDataFromCSV(@path);
                    for (int i = 0; i < byPermissionsData.Count; i++)
                    {
                        if (byPermissionsData[i].Key == Const.UserId)
                        {
                            flag = true;
                            if (byPermissionsData[i].Value == userId)
                            {
                                for (int j = i; j < i + 12; j++)
                                {
                                    byPermissionsList = byPermissionsList + Const.OrSign + byPermissionsData[j].Key + Const.SemicolnSign + byPermissionsData[j].Value;
                                }
                                logger.LogCheckPoint(String.Format(LogMessage.UserDataPresenInCSVFile, userId, byPermissionsList));
                                break;
                            }
                        }
                    }
                    if (flag)
                    {
                        logger.LogCheckPoint(String.Format(LogMessage.VerifyUserIdPassed, userId));
                        logger.LogCheckPoint(String.Format(LogMessage.VerifyByPermissionExportedCSVFilePassed));
                    }
                    else
                    {
                        logger.LogCheckPoint(String.Format(LogMessage.VerifyUserIdFailed, userId));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }

        // This method returns the list of Key value pairs from the Alluser csv file
        public List<KeyValuePair<string, string>> ReadAllUsersDataFromCSV(string filePath)
        {
            bool isHeader = true, flag = false;
            var reader = new StreamReader(File.OpenRead(filePath));
            List<string> headers = new List<string>();
            List<string> trimmedValues = new List<string>();
            List<KeyValuePair<string, string>> csvFileData = new List<KeyValuePair<string, string>>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var result = line.Split("[");
                var values = result[0].Split(',');
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

                    if (headers.Contains(Const.UserId) && headers.Contains(Const.UserName) && headers.Contains(Const.EmailVerified)
                        && headers.Contains(Const.AccountId) && headers.Contains(Const.DateTimeCreated) 
                        && headers.Contains(Const.AffiliateId) && headers.Contains(Const.RefererId) && 
                        headers.Contains(Const.OMSId) && headers.Contains(Const.Use2FA))
                    {
                        flag = true;
                    }
                        if(flag)
                        {
                            logger.LogCheckPoint(String.Format(LogMessage.VerifyCSVDataOfAllUserPassed, Const.UserId, Const.UserName,
                                Const.EmailVerified, Const.AccountId, Const.DateTimeCreated, Const.AffiliateId, Const.RefererId,
                                Const.OMSId, Const.Use2FA));
                        }
                        else
                        {
                        logger.LogCheckPoint(String.Format(LogMessage.VerifyCSVDataOfAllUserFailed, Const.UserId, Const.UserName,
                            Const.EmailVerified, Const.AccountId, Const.DateTimeCreated, Const.AffiliateId, Const.RefererId,
                            Const.OMSId, Const.Use2FA));
                    }
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
            return csvFileData;
        }

        // This method returns the list of Key value pairs from the By Permission csv file
        public List<KeyValuePair<string, string>> ReadByPermissionsDataFromCSV(string filePath)
        {
            bool isHeader = true, flag = false;
            var reader = new StreamReader(File.OpenRead(filePath));
            List<string> headers = new List<string>();
            List<string> trimmedValues = new List<string>();
            List<KeyValuePair<string, string>> csvFileData = new List<KeyValuePair<string, string>>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var result = line.Split("[");
                var values = result[0].Split(',');
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

                    if (headers.Contains(Const.UserId) && headers.Contains(Const.UserName) && headers.Contains(Const.EmailVerified)
                        && headers.Contains(Const.Email)
                        && headers.Contains(Const.AccountId) && headers.Contains(Const.DateTimeCreated)
                        && headers.Contains(Const.AffiliateId) && headers.Contains(Const.RefererId) &&
                        headers.Contains(Const.OMSId) && headers.Contains(Const.Use2FA) &&
                        headers.Contains(Const.LoginId) && headers.Contains(Const.Permissions))
                    {
                        flag = true;
                    }
                    if (flag)
                    {
                        logger.LogCheckPoint(String.Format(LogMessage.VerifyCSVDataOfByPermissionsPassed, Const.UserId, Const.UserName,
                            Const.EmailVerified, Const.Email, Const.AccountId, Const.DateTimeCreated, Const.AffiliateId, Const.RefererId,
                            Const.OMSId, Const.Use2FA, Const.LoginId, Const.Permissions));
                    }
                    else
                    {
                        logger.LogCheckPoint(String.Format(LogMessage.VerifyCSVDataOfByPermissionsFailed, Const.UserId, Const.UserName,
                            Const.EmailVerified, Const.Email, Const.AccountId, Const.DateTimeCreated, Const.AffiliateId, Const.RefererId,
                            Const.OMSId, Const.Use2FA, Const.LoginId, Const.Permissions));
                    }
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
            return csvFileData;
        }

        // This method returns the list of Users with permissions from the superusers csv file
        public ArrayList ReadSuperusersDataFromCSV(string filePath)
        {
            bool isHeader = true, flag = false;
            var reader = new StreamReader(File.OpenRead(filePath));
            List<string> headers = new List<string>();
            List<string> trimmedValues = new List<string>();
            ArrayList myItem = new ArrayList();
            List<KeyValuePair<string, string>> csvFileData = new List<KeyValuePair<string, string>>();
            ArrayList filledOrderList = new ArrayList();
            string lastPermission;
            String textFinal;

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var result = line.Split("[");
                var values = result[0].Split(',');

                if (isHeader)
                {
                    isHeader = false;
                    headers = values.ToList();
                    for (int i = 0; i < values.Length; i++)
                    {
                        trimmedValues.Add(values[i].Trim('"'));
                    }
                    headers = trimmedValues;

                    if (headers.Contains(Const.UserId) && headers.Contains(Const.UserName) && headers.Contains(Const.EmailVerified) 
                        && headers.Contains(Const.Email)
                        && headers.Contains(Const.AccountId) && headers.Contains(Const.DateTimeCreated)
                        && headers.Contains(Const.AffiliateId) && headers.Contains(Const.RefererId) &&
                        headers.Contains(Const.OMSId) && headers.Contains(Const.Use2FA) &&
                        headers.Contains(Const.LoginId) && headers.Contains(Const.Permissions))
                    {
                        flag = true;
                    }
                    if (flag)
                    {
                        logger.LogCheckPoint(String.Format(LogMessage.VerifyCSVDataOfSuperusersPassed, Const.UserId, Const.UserName,
                            Const.EmailVerified, Const.Email, Const.AccountId, Const.DateTimeCreated, Const.AffiliateId, Const.RefererId,
                            Const.OMSId, Const.Use2FA, Const.LoginId, Const.Permissions));
                    }
                    else
                    {
                        logger.LogCheckPoint(String.Format(LogMessage.VerifyCSVDataOfSuperusersFailed, Const.UserId, Const.UserName,
                            Const.EmailVerified, Const.Email, Const.AccountId, Const.DateTimeCreated, Const.AffiliateId, Const.RefererId,
                            Const.OMSId, Const.Use2FA, Const.LoginId, Const.Permissions));
                    }
                }
                else
                {
                    textFinal = values[0];                  
                    if (values[0] == TestData.GetData("TCAdmin26_UserIdValue"))
                    {
                        var permissionValues = result[1].Split(',');
                        for (int i = 0; i < permissionValues.Length; i++)
                        {
                            if (i == (permissionValues.Length - 1))
                            {
                                lastPermission = permissionValues[i];
                                lastPermission = lastPermission.Substring(0, lastPermission.Length - 2);
                                myItem.Add(lastPermission.Trim('"'));
                            }
                            else
                            {
                                myItem.Add(permissionValues[i].Trim('"'));
                            }
                        }
                    }
                }
            }
            return myItem;
        }
    }
}
