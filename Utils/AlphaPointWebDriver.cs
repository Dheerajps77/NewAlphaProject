using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace AlphaPoint_QA.Utils
{

    public enum PlatformType
    {
        Linux,
        Window,
    }

    public enum BrowserType
    {
        Chrome,
        Firefox,
        IE
    }

    public class AlphaPointWebDriver : IWebDriver
    {

        public IWebDriver driver;

        public PlatformType Platform { get; private set; }

        public BrowserType Browser { get; private set; }

        public bool HeadLess { get; private set; }

        // instance of singleton class
        private static AlphaPointWebDriver instanceOfAlphaPointWebDriver = null;

        private AlphaPointWebDriver()
        {
            HeadLess = ConfigManager.Instance.HeadLess;
            Platform = Enum.Parse<PlatformType>(ConfigManager.Instance.Platform, true);
            Browser = Enum.Parse<BrowserType>(ConfigManager.Instance.Browser, true);
            driver = GetDriver(Platform, Browser);
        }

        // To create instance of class
        public static IWebDriver GetInstanceOfAlphaPointWebDriver()
        {
            if (instanceOfAlphaPointWebDriver == null)
            {
                instanceOfAlphaPointWebDriver = new AlphaPointWebDriver();
            }
            return instanceOfAlphaPointWebDriver.driver;
        }

        // This method destroys the instance
        public static void DestroyInstanceOfAlphaPointWebDriver(IWebDriver driver)
        {
            if (instanceOfAlphaPointWebDriver != null)
            {
                GenericUtils.CleanUp(driver);
                instanceOfAlphaPointWebDriver = null;
            }
        }

        //  To get driver
        private IWebDriver GetDriver(PlatformType platform, BrowserType browser)
        {
            switch (platform)
            {
                case PlatformType.Linux:
                    return GetLinuxDriver(browser);
                case PlatformType.Window:
                    return GetWindowDriver(browser);
                default:
                    return null;
            }
        }

        private IWebDriver GetLinuxDriver(BrowserType browser)
        {
            switch (browser)
            {
                case BrowserType.Chrome:
                    return GetChromeforLinux();
                case BrowserType.Firefox:
                    return GetFirefoxforLinux();
                case BrowserType.IE:
                    return GetIEforLinux();
                default:
                    return null;
            }
        }

        private IWebDriver GetChromeforLinux()
        {
            string chromeLinuxPath = Const.ChromeLinuxPath;
            if (HeadLess)
            {
                var chromeOptions = new ChromeOptions();
                chromeOptions.AddArguments(Const.LinuxHeadless);
                driver = new ChromeDriver(chromeLinuxPath, chromeOptions);
            }
            else
            {
                driver = new ChromeDriver(chromeLinuxPath);
            }
                return driver;
        }

        private IWebDriver GetFirefoxforLinux()
        {
            string firefoxLinuxPath = Const.FirefoxLinuxPath;
            if (HeadLess)
            {
                var firefoxoptions = new FirefoxOptions();
                firefoxoptions.AddArguments(Const.LinuxHeadless);
                driver = new FirefoxDriver(firefoxLinuxPath, firefoxoptions);
            }
            else
            {
                driver = new FirefoxDriver(firefoxLinuxPath);
            }
            driver = new FirefoxDriver(firefoxLinuxPath);
            return driver;
        }

        private IWebDriver GetIEforLinux()
        {
            string ieLinuxPath = Const.IELinuxPath;
            driver = new InternetExplorerDriver(ieLinuxPath);
            return driver;
        }

        private IWebDriver GetWindowDriver(BrowserType browser)
        {
            switch (browser)
            {
                case BrowserType.Chrome:
                    return GetChromeforWindow();
                case BrowserType.Firefox:
                    return GetFirefoxforWindow();
                case BrowserType.IE:
                    return GetIEforWindow();
                default:
                    return null;
            }
        }

        private IWebDriver GetChromeforWindow()
        {
            string chromeWindowPath = Const.ChromeWindowPath;

            if (HeadLess)
            {
                var chromeOptions = new ChromeOptions();
                chromeOptions.AddArguments(Const.Headless);

                driver = new ChromeDriver(chromeWindowPath, chromeOptions);
            }
            else
            {
                string dataTestPath = Directory.GetCurrentDirectory() + "\\DataTest\\";
                if (!Directory.Exists(dataTestPath))
                {
                    Directory.CreateDirectory(dataTestPath);
                }
                var chromeOptions = new ChromeOptions();
                chromeOptions.AddUserProfilePreference("download.default_directory", dataTestPath);
                chromeOptions.AddUserProfilePreference("download.prompt_for_download", false);
                chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");
                driver = new ChromeDriver(chromeWindowPath, chromeOptions);
            }
            return driver;
        }

        private IWebDriver GetFirefoxforWindow()
        {
            string firefoxWindowPath = Const.FirefoxWindowPath;

            if (HeadLess)
            {
                var firefoxoptions = new FirefoxOptions();
                firefoxoptions.AddArguments(Const.FireFoxHeadless);

                driver = new FirefoxDriver(firefoxWindowPath, firefoxoptions);
            }
            else
            {
                driver = new FirefoxDriver(firefoxWindowPath);
            }
            return driver;
        }

        private IWebDriver GetIEforWindow()
        {
            string ieWindowPath = Const.IEWindowPath;
            driver = new InternetExplorerDriver(ieWindowPath);
            return driver;
        }


        public string Url { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Title => throw new NotImplementedException();

        public string PageSource => throw new NotImplementedException();

        public string CurrentWindowHandle => throw new NotImplementedException();

        public ReadOnlyCollection<string> WindowHandles => throw new NotImplementedException();

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IWebElement FindElement(By by)
        {
            throw new NotImplementedException();
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            throw new NotImplementedException();
        }

        public IOptions Manage()
        {
            throw new NotImplementedException();
        }

        public INavigation Navigate()
        {
            throw new NotImplementedException();
        }

        public void Quit()
        {
            throw new NotImplementedException();
        }

        public ITargetLocator SwitchTo()
        {
            throw new NotImplementedException();
        }
    }
}
