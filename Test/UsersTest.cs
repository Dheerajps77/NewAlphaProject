using AlphaPoint_QA.Common;
using AlphaPoint_QA.Pages;
using AlphaPoint_QA.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Xunit;
using Xunit.Abstractions;

namespace AlphaPoint_QA.Test
{
    [Collection("Alphapoint_QA_USER")]
    public class UsersTest : TestBase
    {
        private string addUserPermission;
        private string revokeUserPermission;
        private string userName;
        private string userEmail;
        private string userPassword;
        private string userConfirmPassword;
        private string depositPermission;
        private string tradingPermission;
        private string withdrawPermission;
        private string verificationLevel;
        private string invalidAccountID;
        private string enterUserId;
        private string userIdText;
        private string userNameText;
        private string accountIdText;
        private string EmailText;
        private string AccountIdText;
        private string entereUserName;
        private string entereEmail;
        private string entereAccountId;
        private string selectAllUsersOption;
        private string selectSuperusersOption;
        private string selectByPermissionOption;
        private string selectUserPermission;
        private string newUserPassword;
        private string userIdValue;
        private string byPermissionUserIdValue;
        private string superusersIdValue;

        public UsersTest(ITestOutputHelper output) : base(output)
        {

        }

        [Fact]
        public void TCAdmin1_VerifyAddNewUserTest()
        {
            string userLoginName;
            userName = TestData.GetData("TCAdmin1_UserName");
            userPassword = TestData.GetData("TCAdmin1_UserPassword");
            userConfirmPassword = TestData.GetData("TCAdmin1_UserConfirmPassword");
            depositPermission = TestData.GetData("TCAdmin1_DepositPermission");
            tradingPermission = TestData.GetData("TCAdmin1_DepositTrading");
            withdrawPermission = TestData.GetData("TCAdmin1_UserWithdraw");
            verificationLevel = TestData.GetData("TCAdmin1_VerificationLevel");

            AdminFunctions objAdminFunctions = new AdminFunctions(TestProgressLogger);
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(TestProgressLogger);
            AdminUsersPage objAdminUsersPage = new AdminUsersPage(TestProgressLogger);
            UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
            try
            {
                TestProgressLogger.StartTest();

                //Login as admin -> Click on "Users" menu button
                objAdminFunctions.AdminLogIn(TestProgressLogger);
                objAdminCommonFunctions.ClickOnUsersMenuLink();
                objAdminCommonFunctions.UsersTabBtn();

                //Verify whether new user created 
                Assert.True(objAdminUsersPage.CreateNewUser(userName, userPassword, userConfirmPassword, verificationLevel));

                //Verify that the new user created is having deposit, trading and withdraw permissions   
                Assert.True(objAdminUsersPage.VerifyUserCreatedWithPermission(depositPermission, tradingPermission, withdrawPermission));

                objAdminCommonFunctions.UserMenuBtn();
                userLoginName = objAdminCommonFunctions.UserNameTextValue();

                // Admin LogOut
                objAdminFunctions.AdminLogOut();

                //Login using the credentials of the User created above               
                // This will change only the server URL 
                userFunctions.LogIn(TestProgressLogger, changeServerOnly: true);
                Assert.True(userFunctions.LogInUsingCredsFromUI(TestProgressLogger, userLoginName, userPassword));
            }

            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifyAddUserFailed), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifyAddUserFailed), e);
                throw e;
            }
            finally
            {
                userFunctions.LogOut();
                TestProgressLogger.EndTest();
            }
        }

        [Fact]
        public void TCAdmin2_VerifyUsersAccountAssignedOrUnassignedTest()
        {
            userPassword = TestData.GetData("TCAdmin2_UserPassword");
            invalidAccountID = TestData.GetData("TCAdmin2_InvalidAccountID");
            userName = TestData.GetData("TCAdmin1_UserName");
            newUserPassword = TestData.GetData("TCAdmin1_UserPassword");
            userConfirmPassword = TestData.GetData("TCAdmin1_UserConfirmPassword");
            verificationLevel = TestData.GetData("TCAdmin1_VerificationLevel");

            AdminFunctions objAdminFunctions = new AdminFunctions(TestProgressLogger);
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(TestProgressLogger);
            UserFunctions userfuntionality = new UserFunctions(TestProgressLogger);
            AdminUsersPage objAdminUsersPage = new AdminUsersPage(TestProgressLogger);
            try
            {
                TestProgressLogger.StartTest();

                ////Login as admin -> Click on "Users" menu button -> Users Tab
                objAdminUsersPage.SelectAdminUserTab();

                //pre-requites of creating new user to perform assign and unassign test case
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.NewUserCreationInitiated));
                objAdminUsersPage.CreateNewUser(userName, newUserPassword, userConfirmPassword, verificationLevel);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.NewUserCreationCompleted, userName));

                //Click on "Users" Tab button
                objAdminCommonFunctions.UsersTabBtn();

                //Verify the login after unassign account ID to user
                Assert.True(objAdminUsersPage.VerifyLoginUsingUnassignedAccount(userPassword));

                Thread.Sleep(2000);
                //Login as admin -> Click on "Users" menu button -> Users Tab
                objAdminUsersPage.SelectAdminUserTab();

                //Verify the login after assign account ID to user
                Assert.True(objAdminUsersPage.VerifyLoginUsingAssignedAccount(userPassword));

                Thread.Sleep(2000);
                //Login as admin -> Click on "Users" menu button -> Users Tab
                objAdminUsersPage.SelectAdminUserTab();

                //Verify the login after assign invalid account ID to user
                Assert.True(objAdminUsersPage.VerifyNonexistentAccount(invalidAccountID));

            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifyAssignedOrUnassignedAccountFailed), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifyAssignedOrUnassignedAccountFailed), e);
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
        public void TCAdmin5_VerifyConfigureUserPermissionTest()
        {
            addUserPermission = TestData.GetData("TCAdmin5_AddUserPermission");
            string userName;
            string userAccountID;
            AdminFunctions objAdminFunctions = new AdminFunctions(TestProgressLogger);
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(TestProgressLogger);
            try
            {
                TestProgressLogger.StartTest();

                //Login as admin -> Click on "Users" menu button
                objAdminFunctions.AdminLogIn(TestProgressLogger);
                objAdminCommonFunctions.ClickOnUsersMenuLink();
                objAdminCommonFunctions.UsersTabBtn();

                //This will get the user name and user account ID from the user list under Users Tab
                userName = objAdminCommonFunctions.getUserNameFromUserList();
                userAccountID = objAdminCommonFunctions.getUserAccountIDFromUserList();

                objAdminCommonFunctions.SelectUserFromUserList(driver, userName);

                //Click on "Add Permission" button -> verify all permissions with checkbox
                objAdminCommonFunctions.UserPermissionButton();
                try
                {
                    Assert.True(objAdminCommonFunctions.VerifyUserPermissionsList());
                    TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifyUserPermissionsListPassed));
                }
                catch (Exception)
                {
                    TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifyUserPermissionsListFailed));
                    throw;
                }
                //This will configure a permission and verify the success message
                objAdminCommonFunctions.AddUserPermissions(addUserPermission);

                //Close the permission window section
                objAdminCommonFunctions.ClosePermissionWindow();
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifyConfigureUserPermissionPassed));
            }

            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifyConfigureUserPermissionFailed), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifyConfigureUserPermissionFailed), e);
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
        public void TCAdmin6_VerifyRevokeUserPermissionTest()
        {
            revokeUserPermission = TestData.GetData("TCAdmin6_RevokeUserPermission");
            string username;

            AdminFunctions objAdminFunctions = new AdminFunctions(TestProgressLogger);
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(TestProgressLogger);
            AdminUsersPage objAdminUsersPage = new AdminUsersPage(TestProgressLogger);
            try
            {
                TestProgressLogger.StartTest();
                //Login as admin -> Click on "Users" menu button
                objAdminFunctions.AdminLogIn(TestProgressLogger);
                objAdminCommonFunctions.ClickOnUsersMenuLink();
                objAdminCommonFunctions.UsersTabBtn();

                ////This will get the user name from the user list under Users Tab and click on any user under Users Tab
                username = objAdminCommonFunctions.getUserNameFromUserList();
                objAdminCommonFunctions.SelectUserFromUserList(driver, username);

                //Click on "Revok" button under user permission window section and observed the message
                objAdminUsersPage.ClickOnRevokePermissionButton();

                //Click on "Add Permission" button -> verify all if selected reovked user permissions present in the list of user permission
                objAdminCommonFunctions.UserPermissionButton();
                objAdminCommonFunctions.RevokedUserPermissions(revokeUserPermission);

                //Close the permission window section
                objAdminCommonFunctions.ClosePermissionWindow();
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


        [Fact]
        public void TCAdmin16_VerifyFilterByUserId()
        {
            enterUserId = TestData.GetData("TCAdmin16_EnterUserId");
            userIdText = TestData.GetData("TCAdmin16_UserIdTextValue");
            EmailText = TestData.GetData("TCAdmin16_EmailTextValue");
            AccountIdText = TestData.GetData("TCAdmin16_AccountIdTextValue");

            AdminFunctions objAdminFunctions = new AdminFunctions(TestProgressLogger);
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(TestProgressLogger);
            AdminUsersPage objAdminUsersPage = new AdminUsersPage(TestProgressLogger);

            try
            {
                TestProgressLogger.StartTest();
                //Login as admin -> Click on "Users" menu button -> Users Tab
                objAdminUsersPage.SelectAdminUserTab();

                //Click on "ViewAll" button
                objAdminUsersPage.ClickOnViewAllButton();

                //Verify if entered value in userId textfield loads the values in userTable dynamically and load the page
                Assert.True(objAdminUsersPage.VerifyUserIdResultsLoad(enterUserId, userIdText));
            }

            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifyFilteredByFailed, userIdText), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifyFilteredByFailed, userIdText), e);
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
        public void TCAdmin17_VerifyFilterByUserName()
        {
            userNameText = TestData.GetData("TCAdmin17_UsernameTextValue");
            entereUserName = TestData.GetData("TCAdmin17_EnterUserName");

            AdminFunctions objAdminFunctions = new AdminFunctions(TestProgressLogger);
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(TestProgressLogger);
            AdminUsersPage objAdminUsersPage = new AdminUsersPage(TestProgressLogger);

            try
            {
                TestProgressLogger.StartTest();
                //Login as admin -> Click on "Users" menu button -> Users Tab
                objAdminUsersPage.SelectAdminUserTab();

                //Click on "ViewAll" button
                objAdminUsersPage.ClickOnViewAllButton();

                //Verify if entered value in userId textfield loads the values in userTable dynamically and load the page
                Assert.True(objAdminUsersPage.VerifyUserNameResultsLoad(entereUserName, userNameText));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifyFilteredByFailed, userNameText), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifyFilteredByFailed, userNameText), e);
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
        public void TCAdmin18_VerifyFilterByEmail()
        {
            EmailText = TestData.GetData("TCAdmin18_EmailTextValue");
            entereEmail = TestData.GetData("TCAdmin18_EnterEmail");

            AdminFunctions objAdminFunctions = new AdminFunctions(TestProgressLogger);
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(TestProgressLogger);
            AdminUsersPage objAdminUsersPage = new AdminUsersPage(TestProgressLogger);

            try
            {
                TestProgressLogger.StartTest();
                //Login as admin -> Click on "Users" menu button -> Users Tab
                objAdminUsersPage.SelectAdminUserTab();

                //Click on "ViewAll" button
                objAdminUsersPage.ClickOnViewAllButton();

                //Verify if entered value in email textfield loads the values in userTable dynamically and load the page
                Assert.True(objAdminUsersPage.VerifyEmailResultsLoad(entereEmail, EmailText));
            }

            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifyFilteredByFailed, EmailText), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifyFilteredByFailed, EmailText), e);
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
        public void TCAdmin19_VerifyFilterByAccountId()
        {

            accountIdText = TestData.GetData("TCAdmin19_AccountIdTextValue");
            entereAccountId = TestData.GetData("TCAdmin19_EnterAccountId");

            AdminFunctions objAdminFunctions = new AdminFunctions(TestProgressLogger);
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(TestProgressLogger);
            AdminUsersPage objAdminUsersPage = new AdminUsersPage(TestProgressLogger);

            try
            {
                TestProgressLogger.StartTest();
                //Login as admin -> Click on "Users" menu button -> Users Tab
                objAdminUsersPage.SelectAdminUserTab();

                //Click on "ViewAll" button
                objAdminUsersPage.ClickOnViewAllButton();

                //Verify if entered value in accountId textfield loads the values in userTable dynamically and load the page
                Assert.True(objAdminUsersPage.VerifyAccountResultsLoad(entereAccountId, accountIdText));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifyFilteredByFailed, accountIdText), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifyFilteredByFailed, accountIdText), e);
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
        public void TCAdmin37_VerifyUserAPIKeysCreationDeletionTest()
        {
            string username;
            accountIdText = TestData.GetData("TCAdmin19_AccountIdTextValue");
            entereAccountId = TestData.GetData("TCAdmin19_EnterAccountId");

            AdminFunctions objAdminFunctions = new AdminFunctions(TestProgressLogger);
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(TestProgressLogger);
            AdminUsersPage objAdminUsersPage = new AdminUsersPage(TestProgressLogger);

            try
            {
                TestProgressLogger.StartTest();
                //Login as admin -> Click on "Users" menu button -> Users Tab
                objAdminUsersPage.SelectAdminUserTab();

                //Select an user from the user list and click on it
                username = objAdminCommonFunctions.getUserNameFromUserList();
                objAdminCommonFunctions.SelectUserFromUserList(driver, username);

                //Verify if entered value in accountId textfield loads the values in userTable dynamically and load the page
                Assert.True(objAdminUsersPage.VerifyAPIKeys());
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifyCreationDeletionUserKeyFailed), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifyCreationDeletionUserKeyFailed), e);
                throw e;
            }
            finally
            {
                objAdminCommonFunctions.UserMenuBtn();
                objAdminFunctions.AdminLogOut();
                TestProgressLogger.EndTest();
            }
        }

        //This test case completed till to download the exported excel sheet file for "superusers"
        [Fact]
        public void TCAdmin26_VerifyExportedSuperUsersDataTest()
        {            
            selectSuperusersOption = TestData.GetData("TCAdmin26_SelectSuperusersOption");
            superusersIdValue = TestData.GetData("TCAdmin26_UserIdValue");

            AdminFunctions objAdminFunctions = new AdminFunctions(TestProgressLogger);
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(TestProgressLogger);
            AdminUsersPage objAdminUsersPage = new AdminUsersPage(TestProgressLogger);
            GenericUtils genericUtils = new GenericUtils(TestProgressLogger);

            try
            {
                TestProgressLogger.StartTest();

                // This method is used to Delete all files from the Folder
                genericUtils.DeleteAllFiles();
                //Login as admin -> Click on "Users" menu button -> Users Tab
                objAdminUsersPage.SelectAdminUserTab();
                // Verify the "Superusers" exported csv file in the Admin UI
                Assert.True(objAdminUsersPage.VerifyExportSuperuser(selectSuperusersOption, superusersIdValue));

            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifySuperusersExportedCSVFileFailed), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifySuperusersExportedCSVFileFailed), e);
                throw e;
            }
            finally
            {
                objAdminCommonFunctions.UserMenuBtn();
                objAdminFunctions.AdminLogOut();
                TestProgressLogger.EndTest();
            }
        }

        //This test case completed till to download the exported excel sheet file for "All users"
        [Fact]
        public void TCAdmin27_VerifyExportedAllUsersDataTest()
        {
            selectAllUsersOption = TestData.GetData("TCAdmin27_SelectAllUsersOption");
            userIdValue = TestData.GetData("TCAdmin27_UserIdValue");

            AdminFunctions objAdminFunctions = new AdminFunctions(TestProgressLogger);
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(TestProgressLogger);
            AdminUsersPage objAdminUsersPage = new AdminUsersPage(TestProgressLogger);
            GenericUtils genericUtils = new GenericUtils(TestProgressLogger);

            try
            {
                TestProgressLogger.StartTest();

                // This method is used to Delete all files from the Folder
                genericUtils.DeleteAllFiles();
                // Login as admin -> Click on "Users" menu button -> Users Tab
                objAdminUsersPage.SelectAdminUserTab();
                // Verify the "All users" exported csv file in the Admin UI
                Assert.True(objAdminUsersPage.VerifyExportAllusers(selectAllUsersOption, userIdValue));

            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifyAllUsersExportedCSVFileFailed), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifyAllUsersExportedCSVFileFailed), e);
                throw e;
            }
            finally
            {
                objAdminCommonFunctions.UserMenuBtn();
                objAdminFunctions.AdminLogOut();
                TestProgressLogger.EndTest();
            }
        }

        // This test case completed till to download the exported excel sheet file for "By permissions"
        [Fact]
        public void TCAdmin28_VerifyExportedByPermissionsDataTest()
        {
            selectByPermissionOption = TestData.GetData("TCAdmin28_SelectByPermissionOption");
            selectUserPermission = TestData.GetData("TCAdmin28_SelectUserPermission");
            byPermissionUserIdValue= TestData.GetData("TCAdmin28_UserId");

            AdminFunctions objAdminFunctions = new AdminFunctions(TestProgressLogger);
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(TestProgressLogger);
            AdminUsersPage objAdminUsersPage = new AdminUsersPage(TestProgressLogger);
            GenericUtils genericUtils = new GenericUtils(TestProgressLogger);

            try
            {
                TestProgressLogger.StartTest();

                // This method is used to Delete all files from the Folder
                genericUtils.DeleteAllFiles();
                // Login as admin -> Click on "Users" menu button -> Users Tab
                objAdminUsersPage.SelectAdminUserTab();
                // Verify the "All users" exported csv file in the Admin UI
                Assert.True(objAdminUsersPage.VerifyExportByPermission(selectByPermissionOption, byPermissionUserIdValue, selectUserPermission));

            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifyByPermissionExportedCSVFileFailed), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifyByPermissionExportedCSVFileFailed), e);
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
