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
    public class UserSettingsTest : TestBase
    {

        public UserSettingsTest(ITestOutputHelper output) : base(output)
        {

        }
      
        [Fact]
        public void TC44_VerifyCreateAPIKey()
        {
            try
            {
                TestProgressLogger.StartTest();
                UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
                UserSettingPage userSettingsPage = new UserSettingPage(driver, TestProgressLogger);
                userFunctions.LogIn(TestProgressLogger, Const.USER1);
                // Login -> navigate to User Settings and Select API Key
                Assert.True((userSettingsPage.SelectAPIKey()), LogMessage.CreateAPIKeyBtnIsNotPresent);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.CreateAPIKeyBtnIsPresent));
                // Verify that the checkboxes are present
                Assert.True((userSettingsPage.VerifyAPIKeyCheckboxesArePresent()), LogMessage.APIKeyCheckboxesAreNotPresent);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.APIKeyCheckboxesArePresent));
                // Create API key and verify the key is created successfully
                Assert.True((userSettingsPage.CreateAndVerifyAPIKey()), LogMessage.APIKeyCreatedFailureMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.APIKeyCreatedSuccessMsg));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(LogMessage.CreateAPIKeyFailed, ex);

                throw;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(LogMessage.CreateAPIKeyFailed, e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }

        [Fact]
        public void TC45_VerifyAffiliateProgram()
        {
            string userByID = TestData.GetData("TC45_UserByID");
            string affiliateTagID = TestData.GetData("TC45_AffiliateTagID");
            string verificationLevel = TestData.GetData("TC45_VerificationLevel");
            AdminFunctions objAdminFunctions = new AdminFunctions(TestProgressLogger);
            UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(TestProgressLogger);
            UserSettingPage userSettingsPage = new UserSettingPage(driver, TestProgressLogger);            
            try
            {
                TestProgressLogger.StartTest();
                // Login as admin 
                objAdminFunctions.AdminLogIn(TestProgressLogger);
                // Select user by entering UserID 
                objAdminCommonFunctions.ClickOnUsersMenuLink();
                objAdminCommonFunctions.UserByIDText(userByID);
                objAdminCommonFunctions.OpenUserButton();
                // Create Affiliate tag for the user
                objAdminCommonFunctions.AffiliateTagCreation(affiliateTagID);
                objAdminCommonFunctions.UserMenuBtn();
                objAdminFunctions.AdminLogOut();  
                
                // Login as the user mentioned above and verify Affiliate program functionality
                userFunctions.LogIn(TestProgressLogger, Const.USER12);
                Assert.True(userSettingsPage.VerifyAffiliateProgramFunctionality(driver, verificationLevel), LogMessage.AffiliateProgramFailureMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.AffiliateProgramSuccessMsg));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(LogMessage.AffiliateProgramFailureMsg, ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(LogMessage.AffiliateProgramFailureMsg, e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }
        }

        [Fact]
        public void TC46_VerifyDeleteAPIKey()
        {
            UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
            UserSettingPage userSettingsPage = new UserSettingPage(driver, TestProgressLogger);
            try
            {
                TestProgressLogger.StartTest();
                // Login as user -> Create and Delete the API Key
                userFunctions.LogIn(TestProgressLogger, Const.USER2);                
                Assert.True(userSettingsPage.DeleteAPIKey(driver), LogMessage.DeleteAPIKeyFailureMsg);
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.DeleteAPIKeySuccessMsg));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(LogMessage.DeleteAPIKeyFailureMsg, ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(LogMessage.DeleteAPIKeyFailureMsg , e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();
            }

        }
    }
}
