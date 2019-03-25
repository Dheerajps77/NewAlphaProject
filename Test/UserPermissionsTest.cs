using AlphaPoint_QA.Common;
using AlphaPoint_QA.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace AlphaPoint_QA.Test
{
    [Collection("Alphapoint_QA_USER")]
    public class UserPermissionsTest : TestBase
    {
        private string submitBlockTradePermission;

        public UserPermissionsTest(ITestOutputHelper output) : base(output)
        {

        }

        [Fact]
        public void TCAdmin5_VerifyUserPermissionConfigureTest()
        {
            submitBlockTradePermission = TestData.GetData("TCAdmin5_SubmitBlockTradePermission");

            string username;
            AdminFunctions objAdminFunctions = new AdminFunctions(TestProgressLogger);
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(TestProgressLogger);
            try
            {
                TestProgressLogger.StartTest();

                //Login as admin -> Click on "Users" menu button
                objAdminFunctions.AdminLogIn(TestProgressLogger);
                objAdminCommonFunctions.ClickOnUsersMenuLink();
                objAdminCommonFunctions.UsersTabBtn();
                username=objAdminCommonFunctions.getUserNameFromUserList();
                objAdminCommonFunctions.SelectUserFromUserList(driver, username);
                objAdminCommonFunctions.UserPermissionButton();
                objAdminCommonFunctions.AddSubmitBlockTradePermissions(submitBlockTradePermission);

            }

            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifyAddUserPassed), ex);
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
                TestProgressLogger.EndTest();
            }


        }
    }
}
