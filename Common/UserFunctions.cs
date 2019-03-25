using System;
using System.Drawing;
using System.Threading;
using AlphaPoint_QA.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace AlphaPoint_QA.Common
{

    public class UserFunctions
    {
        private ProgressLogger logger;
        static Config data;
        public static IWebDriver driver;
        static string username;
        static string password;
        private string apexWebTitle;
       

        public UserFunctions(ProgressLogger logger)
        {
            this.logger = logger;
            data = ConfigManager.Instance;
            driver = AlphaPointWebDriver.GetInstanceOfAlphaPointWebDriver();
        }

        //Webelements defined here
        By selectServer = By.CssSelector("select[name = tradingServer");
        By userLoginName = By.CssSelector("input[name = username]"); 
        By userLoginPassword = By.CssSelector("input[name=password]");
        By userLoginButton = By.CssSelector("button.ap-button__btn.ap-button__btn--additive.login-form__btn.login-form__btn--additive");
        By loggedInUserName = By.XPath("//button[@class='user-summary__popover-menu-trigger page-header-user-summary__popover-menu-trigger']");
        By loggedInUserNameText = By.CssSelector("span[class='popover-menu__item-label user-summary page-header-user-summary__item-label']");
        By userSignOutButton = By.XPath("//span[contains(@class,'popover-menu__item-label') and text()='Sign Out']");
        By signUpLink = By.CssSelector("div.standalone-form__footer.login-form__footer a[href='/signup']");
        By signUpUserName = By.CssSelector("input[data-test=Username]");
        By signUpEmail = By.CssSelector("input[data-test=Email]");
        By signUpPassword = By.CssSelector("input[data-test=Password]");
        By signUpRetypePassword = By.CssSelector("input[data-test='Retype Password']");
        By signUpButton = By.CssSelector("button.ap-button__btn.ap-button__btn--additive.standalone-form.signup-form__btn.standalone-form.signup-form__btn--additive");
        By signUpSuccess = By.CssSelector("div.standalone-form__header.signup-form__header");

        //This method is used for User Login
        //If the user is already logged in, then this method logs out user and then logs in 
        public string LogIn(ProgressLogger logger, string userName = Const.USER5, bool changeServerOnly=false)
        {
            string username = null;
            apexWebTitle = TestData.GetData("HomePageTitle");
            string userUrl = data.UserPortal.PortalUrl;
            string userServerName = data.UserPortal.PortalServerUrl;
            driver.Navigate().GoToUrl(userUrl);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(20);
            driver.Manage().Window.Size = new Size(1366, 768);
            Thread.Sleep(1000);
            if (driver.Url.EndsWith(Const.LoginText))
            {
                try
                {
                    username = UserLogin(logger, userName, userServerName, changeServerOnly);
                }
                catch (Exception e)
                {
                    logger.TakeScreenshot();
                    logger.LogError(LogMessage.UserLoginFailed, e);
                    throw;
                }
            }
            else
            {
                try
                {
                    Thread.Sleep(2000);
                    UserSetFunctions.Click(GenericUtils.WaitForElementVisibility(driver, loggedInUserName, 30));
                    UserSetFunctions.Click(GenericUtils.WaitForElementVisibility(driver, userSignOutButton, 30));
                    Thread.Sleep(2000);
                    username=UserLogin(logger, userName, userServerName, changeServerOnly);
                }

                catch (Exception e)
                {
                    logger.LogError(LogMessage.UserLogoutFailed, e);
                    throw;
                }
            }
            return username;
        }

        private string UserLogin(ProgressLogger logger, string userName, string userServerName, bool changeServerOnly = false)
        {
            try
            {
                string defaultSelectedServer;
                username = data.UserPortal.Users[userName].UserName;
                password = data.UserPortal.Users[userName].Password;
                IWebElement serverWebElement = driver.FindElement(selectServer);
                SelectElement selectObj=  new SelectElement(serverWebElement);
                defaultSelectedServer = selectObj.SelectedOption.Text;
                Thread.Sleep(1000);
                if (!(userServerName.Equals(defaultSelectedServer)))
                {
                    UserSetFunctions.SelectDropdown(serverWebElement, userServerName);
                }                
                Thread.Sleep(1000);
                if (changeServerOnly)
                {
                    return null;
                }
                UserSetFunctions.EnterText(GenericUtils.WaitForElementPresence(driver, userLoginName, 15), username);
                UserSetFunctions.EnterText(GenericUtils.WaitForElementPresence(driver, userLoginPassword, 15), password);
                UserSetFunctions.Click(GenericUtils.WaitForElementPresence(driver, userLoginButton, 15));
                Assert.Equal(driver.Title.ToLower(), apexWebTitle.ToLower());
                logger.LogCheckPoint(string.Format(LogMessage.UserLoggedInSuccessfully, username));
                Thread.Sleep(2000);
                return username;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public string GetTextOfLoggedInUser()
        {
            try
            {
                UserSetFunctions.Click(driver.FindElement(loggedInUserName));
                return driver.FindElement(loggedInUserNameText).Text;
                
            }
            catch (Exception)
            {
                throw;
            }
        }


        //This method is used for User Logout
        public void LogOut()
        {
            try
            {
                Thread.Sleep(2000);
                if (!driver.Url.EndsWith(Const.LoginText))
                {
                    UserCommonFunctions.ScrollingUpVertical(driver);
                    UserSetFunctions.Click(driver.FindElement(loggedInUserName));
                    UserSetFunctions.Click(driver.FindElement(userSignOutButton));
                    logger.LogCheckPoint(string.Format(LogMessage.UserLoggedOutSuccessfully, username));
                }
                Thread.Sleep(2000);
            }
            catch (Exception e)
            {
                logger.LogError(LogMessage.UserLogoutFailed, e);
                throw;
            }
        }

        // This method is used to check the status of the logged in user
        private bool UserLoggedInStatus(IWebDriver driver)
        {
            bool flag;
            try
            {
                flag = driver.FindElement(loggedInUserName).Enabled;
            }
            catch (NoSuchElementException)
            {
                flag = false;
            }
            return flag;
        }

        // This method is used to login using the credentials captured from UI
        public bool LogInUsingCredsFromUI(ProgressLogger logger, string userName, string userPassword)
        {
            bool flag = false;

            try
            {
                UserSetFunctions.EnterText(GenericUtils.WaitForElementPresence(driver, userLoginName, 15), userName);
                UserSetFunctions.EnterText(GenericUtils.WaitForElementPresence(driver, userLoginPassword, 15), userPassword);
                UserSetFunctions.Click(GenericUtils.WaitForElementPresence(driver, userLoginButton, 15));
                if (driver.Title.ToLower().Equals(apexWebTitle.ToLower()))
                {
                    flag = true;
                }

                if(flag)
                {
                    logger.LogCheckPoint(String.Format(LogMessage.VerifyLoginPassed, userName));
                    logger.LogCheckPoint(String.Format(LogMessage.VerifyAddUserPassed));
                }
                else
                {
                    logger.LogCheckPoint(String.Format(LogMessage.VerifyLoginFailed, userName));
                }
                Thread.Sleep(2000);
            }
            catch(Exception)
            {
                throw;
            }
            return flag;
        }

        By loginErrorMsg = By.CssSelector("form.standalone-form__form.login-form__form>p");

        public IWebElement LoginErrorMsg()
        {
            return driver.FindElement(loginErrorMsg);
        }


        // This method is used to login using the credentials captured from UI after unassigning the account
        public bool LogInUsingCredsAfterUnassignAccount(ProgressLogger logger, string userName, string userPassword)
        {
            bool flag = false;

            try
            {
                UserSetFunctions.EnterText(GenericUtils.WaitForElementPresence(driver, userLoginName, 15), userName);
                UserSetFunctions.EnterText(GenericUtils.WaitForElementPresence(driver, userLoginPassword, 15), userPassword);
                UserSetFunctions.Click(GenericUtils.WaitForElementPresence(driver, userLoginButton, 15));

                //When trying to login using credential after unassigining the account then page is showing blank
                //So in case we have used below condition to check login.
                //Once login failed error messges shows then use below comment code in if condition
                //LoginErrorMsg().Text.Equals(Const.LoginErrroMsg)
                if (driver.Title.ToLower().Equals(apexWebTitle.ToLower()))
                {
                    flag = true;
                }

                if (flag)
                {
                    logger.LogCheckPoint(String.Format(LogMessage.VerifyLoginAfterUnassignAccountPassed, userName));
                }
                else
                {
                    logger.LogCheckPoint(String.Format(LogMessage.VerifyLoginAfterUnassignAccountFailed, userName));
                }               
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }

        // This method is used to login using the credentials captured from UI after assigning the account
        public bool LogInUsingCredsAfterAssignAccount(ProgressLogger logger, string userName, string userPassword)
        {
            bool flag = false;

            try
            {
                UserSetFunctions.EnterText(GenericUtils.WaitForElementPresence(driver, userLoginName, 15), userName);
                UserSetFunctions.EnterText(GenericUtils.WaitForElementPresence(driver, userLoginPassword, 15), userPassword);
                UserSetFunctions.Click(GenericUtils.WaitForElementPresence(driver, userLoginButton, 15));
                if (driver.Title.ToLower().Equals(apexWebTitle.ToLower()))
                {
                    flag = true;
                }

                if (flag)
                {
                    logger.LogCheckPoint(String.Format(LogMessage.VerifyLoginAfterAssignAccountPassed, userName));
                    Thread.Sleep(2000);
                    LogOut();
                }
                else
                {
                    logger.LogCheckPoint(String.Format(LogMessage.VerifyLoginAfterAssignAccountFailed, userName));
                }                               
            }            
            catch (Exception)
            {
                throw;
            }
            return flag;
        }

        // This method is used to register a new user
        public string RegisterNewUser(IWebDriver driver)
        {
            try
            {
                string randomString;
                string email;
                string randomUserName;
                string userUrl = data.UserPortal.PortalUrl;
                string userName = TestData.GetData("TC45_UserName");
                string emailDomain = Const.EmailDomain;
                string password = TestData.GetData("TC45_Password");
                string retypePassword = TestData.GetData("TC45_Password");
                randomString = GenericUtils.RandomString(Const.RandomStringLength);
                email = randomString + emailDomain;
                randomUserName = userName + randomString;
                logger.LogCheckPoint(String.Format(LogMessage.UserSignUpStarted, randomUserName, email));
                UserSetFunctions.Click(GenericUtils.WaitForElementPresence(driver, signUpLink, 15));
                UserSetFunctions.EnterText(GenericUtils.WaitForElementPresence(driver, signUpUserName, 15), randomUserName);
                UserSetFunctions.EnterText(GenericUtils.WaitForElementPresence(driver, signUpEmail, 15), email);
                UserSetFunctions.EnterText(GenericUtils.WaitForElementPresence(driver, signUpPassword, 15), password);
                UserSetFunctions.EnterText(GenericUtils.WaitForElementPresence(driver, signUpRetypePassword, 15), retypePassword);
                UserSetFunctions.Click(GenericUtils.WaitForElementPresence(driver, signUpButton, 15));
                GenericUtils.WaitForElementPresence(driver, signUpSuccess, 15);
                logger.LogCheckPoint(String.Format(LogMessage.UserSignUpSuccess, randomUserName, email));
                Thread.Sleep(2000);
                return randomUserName;
            }
            catch (NoSuchElementException ex)
            {
                logger.TakeScreenshot();
                logger.LogError(LogMessage.UserSignUpFailure, ex);
                throw;
            }
            catch (Exception e)
            {
                logger.TakeScreenshot();
                logger.LogError(LogMessage.UserSignUpFailure, e);
                throw;
            }
        }        
    }
}