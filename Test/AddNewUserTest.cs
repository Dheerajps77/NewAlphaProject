using AlphaPoint_QA.Common;
using AlphaPoint_QA.Pages;
using AlphaPoint_QA.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Xunit;
using Xunit.Abstractions;

namespace AlphaPoint_QA.Test
{
    [Collection("Alphapoint_QA_USER")]
    public class AddNewUserTest : TestBase
    {

        private string userName;
        private string userEmail;
        private string userPassword;
        private string userConfirmPassword;
        private string depositPermission;
        private string tradingPermission;
        private string withdrawPermission;
        private string verificationLevel;

        public AddNewUserTest(ITestOutputHelper output) : base(output)
        {

        }
        
        [Fact]
        public void DownloadFileTest()
        {    
            AdminFunctions objAdminFunctions = new AdminFunctions(TestProgressLogger);
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(TestProgressLogger);
            AdminUsersPage objAdminUsersPage = new AdminUsersPage(TestProgressLogger);
            UserFunctions userFunctions = new UserFunctions(TestProgressLogger);
            GenericUtils genericUtils = new GenericUtils(TestProgressLogger);
            try
            {
                List<KeyValuePair<string, string>> superUsersData;
                string superUsersList = "";
                string date;
                TestProgressLogger.StartTest();

                //Login as admin -> Click on "Users" menu button
                genericUtils.DeleteAllFiles();
                objAdminFunctions.AdminLogIn(TestProgressLogger);
                objAdminCommonFunctions.ClickOnUsersMenuLink();
                objAdminCommonFunctions.UsersTabBtn();
                objAdminCommonFunctions.ClickOnExportButton();
                Thread.Sleep(2000);
                objAdminCommonFunctions.ExportSuperUsers();
                Thread.Sleep(3000);
                date=GenericUtils.GetCurrentTimeWithHyphen();
                var path = Directory.GetCurrentDirectory() + "\\DataTest\\Superusers ("+date+").csv";
                superUsersData = genericUtils.ReadDataFromCSV(@path);
                for(int i = 0; i< superUsersData.Count; i++)
                {
                    if (superUsersData[i].Key == "UserId")
                    {
                        if(superUsersData[i].Value == "123")
                        {
                            for(int j = i; j < i+12; j++)
                            {
                                superUsersList = superUsersList + " || " + superUsersData[j].Key + ":" + superUsersData[j].Value;                                                            
                            }
                            TestProgressLogger.LogCheckPoint(superUsersList);
                            break;
                        }                      
                    }
                }
                TestProgressLogger.LogCheckPoint("Passed");
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
