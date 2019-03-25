using AlphaPoint_QA.Utils;
using OpenQA.Selenium;
using System;
using System.Drawing;
using System.Threading;
using Xunit;

namespace AlphaPoint_QA.Common
{
    class AdminFunctions
    {
        private ProgressLogger logger;
        static Config data;
        public static IWebDriver driver;
        static string username;
        static string password;

        public AdminFunctions(ProgressLogger logger)
        {
            this.logger = logger;
            data = ConfigManager.Instance;
            driver = AlphaPointWebDriver.GetInstanceOfAlphaPointWebDriver();
        }

        //Admin login interface locators
        By usernameLoginTextField = By.CssSelector("input[placeholder=Username]");
        By passwordLoginTextField = By.CssSelector("input[placeholder=Password]");
        By loginButton = By.CssSelector("button#login-btn");

        //Admin Logout window section locators
        
        By signOutButton = By.CssSelector("span#SignOut");

        public IWebElement AdminUserName()
        {
            return driver.FindElement(usernameLoginTextField);
        }

        public IWebElement AdminUserPassword()
        {
            return driver.FindElement(passwordLoginTextField);
        }

        public IWebElement AdminSignIn()
        {
            return driver.FindElement(loginButton);
        }

        public IWebElement AdminSignout()
        {
            return driver.FindElement(signOutButton);
        }

        //This method will login to admin portal
        public void AdminLogIn(ProgressLogger logger, string userName = Const.ADMIN1)
        {
            try
            {
                username = data.AdminPortal.Users[userName].UserName;
                password = data.AdminPortal.Users[userName].Password;
                string userUrl = data.AdminPortal.PortalUrl;
                string userServerName = data.AdminPortal.PortalServerUrl;

                driver.Navigate().GoToUrl(userUrl);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
                driver.Manage().Window.Size = new Size(1366, 768);

                UserSetFunctions.EnterText(AdminUserName(), username);
                UserSetFunctions.EnterText(AdminUserPassword(), password);
                UserSetFunctions.Click(AdminSignIn());
                Assert.Equal(driver.Title.ToLower(), Const.AdminLoginTitle.ToLower());
                logger.LogCheckPoint(String.Format(LogMessage.AdminLoginSuccessfullMsg, userName));
                Thread.Sleep(2000);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This method will click on Sign Out button
        public void AdminLogOut()
        {
            try
            {
                Thread.Sleep(2000);        
                UserSetFunctions.Click(AdminSignout());
                logger.LogCheckPoint(String.Format(LogMessage.AdminLogoutSuccessfullMsg));
                Thread.Sleep(2000);
            }
            catch (Exception)
            {
                throw;
            }
        }        
    }
}
