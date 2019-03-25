using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Xunit.Abstractions;

namespace AlphaPoint_QA.Utils
{
    public class GenericUtils
    {
        ProgressLogger logger;
        static Config data;
        public static IWebDriver driver;

        private static string dayFormat = TestData.GetData("DayFormat");
        private static string dateFormat = TestData.GetData("DateFormat");
        private static string dateTimeFormat = TestData.GetData("DateTimeFormat");
        private static string dateTimeFormatWithHyphen = TestData.GetData("DateFormatWithHyphen");

        public GenericUtils(ProgressLogger logger)
        {
            this.logger = logger;
            data = ConfigManager.Instance;
            driver = AlphaPointWebDriver.GetInstanceOfAlphaPointWebDriver();
        }

        // This method returns current date in M-d-yyyy h:mmtt format
        public static string GetCurrentTime()
        {
            DateTime date = DateTime.Now;
            string formattedDate = date.ToString(dateTimeFormat).ToLower();
            return formattedDate;
        }

        // This method returns current date in M-d-yyyy h:mmtt format
        public static string GetCurrentTimeWithHyphen()
        {
            DateTime date = DateTime.Now;
            string formattedDate = date.ToString(dateTimeFormatWithHyphen).ToLower();
            return formattedDate;
        }

        // This method adds 1 minute to current date and returns in M-d-yyyy h:mmtt format
        public static string GetCurrentTimePlusOneMinute()
        {
            DateTime date = DateTime.Now.AddMinutes(1);
            string formattedDate = date.ToString(dateTimeFormat).ToLower();
            return formattedDate;
        }

        // This method returns current date in MM/dd/yyyy format
        public static string GetCurrentDate()
        {
            DateTime date = DateTime.Today;
            string formattedDate = date.ToString(dateFormat).ToLower();
            return formattedDate;
        }


        // This method subtracts 1 day from the current date and returns in MM/dd/yyyy format
        public static string GetCurrentDateMinusOne()
        {
            DateTime date = DateTime.Now.AddDays(-1);
            return date.ToString(dateFormat);
        }

        // This method adds 1 day to the current date and returns in MM/dd/yyyy format
        public static string GetCurrentDatePlusOne()
        {
            DateTime date = DateTime.Now.AddDays(1);
            return date.ToString(dateFormat);
        }

        // This method returns current day in 'dd' format
        public static int GetOnlyCurrentDate()
        {
            DateTime date = DateTime.Now;
            string formattedDate = date.ToString(dayFormat);
            return Int32.Parse(formattedDate);
        }

        // dateString should be in MM/DD/YYYY format 
        public static int GetOnlyDateFromDateString(string dateString)
        {
            string[] formats = { dateFormat };
            var dateTime = DateTime.ParseExact(dateString, formats, new CultureInfo("en-US"), DateTimeStyles.None);
            string formattedDate = dateTime.ToString(dayFormat);
            return Int32.Parse(formattedDate);
        }

        // This method returns the difference from two string values
        public static string GetDifferenceFromStringAfterSubstraction(string firstValue, string secondValue)
        {
            string firstValuePrecision = Convert.ToDecimal(firstValue).ToString("#,##0.00000000");
            string secondValuePrecision = Convert.ToDecimal(secondValue).ToString("#,##0.00000000");
            var firstValueToDouble = Double.Parse(firstValue);
            var secondValueToDouble = Double.Parse(secondValue);
            double difference = Math.Abs(firstValueToDouble - secondValueToDouble);
            string differenceInString = String.Format("{0:0.00000000}", difference);
            return differenceInString;
        }


        // This method returns the sum of two string values
        public static string GetSumFromStringAfterAddition(string firstValue, string secondValue)
        {
            string firstValuePrecision = Convert.ToDecimal(firstValue).ToString("#,##0.00000000");
            string secondValuePrecision = Convert.ToDecimal(secondValue).ToString("#,##0.00000000");
            var firstValueToDouble = Double.Parse(firstValue);
            var secondValueToDouble = Double.Parse(secondValue);
            double sum = Math.Abs(firstValueToDouble + secondValueToDouble);
            string sumInString = String.Format("{0:0.00000000}", sum);
            return sumInString;
        }

        // This method returns the string after adding upto 8 decimal places to amount
        public static String ConvertToDoubleFormat(double amount)
        {
            return Convert.ToDecimal(amount).ToString("#,##0.00000000");
        }

        public static double ConvertStringToDouble(string amount)
        {
            return Convert.ToDouble(amount);
        }

        public static String ConvertStringToDecimalFormat(string amount)
        {
            double doubleamount=Convert.ToDouble(amount);
            return Convert.ToDecimal(doubleamount).ToString("#,##0.00000000");
        }

        public static string ConvertDoubleToString(double amount)
        {
            return Convert.ToString(amount);
        }

        public static string ConvertTo8DigitAfterDecimal(double amount)
        {
            var amt = Math.Floor(amount * 100000000) / 100000000;
            return Convert.ToString(amt);
        }

        // This method removes commas from the string
        public static string RemoveCommaFromString(string str)
        {
            return str.Replace(@",", string.Empty);
        }

        // This method returns total amount after multiplication of two values
        public static String FilledOrdersTotalAmount(double size, double price)
        {
            double totalAmount = size * price;
            return Convert.ToDecimal(totalAmount).ToString("#,##0.00000000");
        }

        // This method performs the click operation using Actions class
        public static void ActionClick(IWebDriver driver, IWebElement we)
        {
            Actions actionobj = new Actions(driver);
            actionobj.Click(we).Build().Perform();
        }

        // This method performs the double click operation using Actions class
        public static void DoubleClick(IWebDriver driver, IWebElement we)
        {
            Actions actionobj = new Actions(driver);
            actionobj.DoubleClick(we).Build().Perform();
        }

       

        // This method is used to Turn off the implicit waits
        public static void TurnOffImplicitWaits(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
        }

        // This method is used to Turn on the implicit waits
        public static void TurnOnImplicitWaits(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        // This method is used to wait until expected condition: waits for element exists
        public static IWebElement WaitForElementVisibility(IWebDriver driver, By findByCondition, int waitInSeconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitInSeconds));
            wait.Until(ExpectedConditions.ElementExists(findByCondition));
            return driver.FindElement(findByCondition);
        }

        // This method is used to wait until expected condition: waits for element visibility
        public static IWebElement WaitForElementPresence(IWebDriver driver, By findByCondition, int waitInSeconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitInSeconds));
            wait.Until(ExpectedConditions.ElementIsVisible(findByCondition));
            return driver.FindElement(findByCondition);
        }

        // This method is used to wait until expected condition: waits for element to be clickable
        public static IWebElement WaitForElementClickable(IWebDriver driver, By findByCondition, int waitInSeconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitInSeconds));
            wait.Until(ExpectedConditions.ElementToBeClickable(findByCondition));
            return driver.FindElement(findByCondition);
        }

        // This method is used to wait until expected condition: waits for text present in dropdown.
        public static IWebElement WaitForTextInSelect(IWebDriver driver, By findByCondition, int waitInSeconds,string text)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitInSeconds));
            IWebElement we=driver.FindElement(findByCondition);
            wait.Until(ExpectedConditions.TextToBePresentInElement(we, text));
            return we;
        }

        // This method is used to close the current browser 
        public static void CloseCurrentBrowserTab(IWebDriver driver)
        {
            driver.Close();
        }

        // This method is used to check if element is displayed
        public static bool IsElementPresent(IWebDriver driver, By locator)
        {
            TurnOffImplicitWaits(driver);
            bool result = false;
            try
            {
                result = driver.FindElement(locator).Displayed;
            }
            catch (Exception)
            {
                TurnOnImplicitWaits(driver);
            }
            finally
            {
                TurnOnImplicitWaits(driver);
            }
            return result;
        }

        // This method is used to scroll into view
        public static IWebElement ScrollToViewElement(IWebDriver driver, By findByCondition)
        {
            IWebElement element = driver.FindElement(findByCondition);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
            return driver.FindElement(findByCondition);
        }

        // This method is used to quit the driver instance
        public static void CleanUp(IWebDriver driver)
        {
            if (driver != null)
            {
                driver.Quit();
            }
        }

        // This method is used to scroll upwards
        public static void ScrollUp(IWebDriver driver)
        {
            Thread.Sleep(2000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollBy(0,-250)", "");
        }

        // This method is used to scroll downwards
        public static void ScrollDown(IWebDriver driver)
        {
            Thread.Sleep(2000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollBy(0,2500)", "");
        }

        // This method is used to click using IJavaScriptExecutor.
        public static void Js_Click(IWebDriver driver,IWebElement element)
        {
            Thread.Sleep(2000);
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", element);
        }

        // This method is used to hover over an element
        public static Actions HoverElement(IWebDriver driver, By locator)
        {
            Actions action = new Actions(driver);
            IWebElement we = driver.FindElement(locator);
            action.MoveToElement(we).Build().Perform();
            return action;
        }

        // This method is used to get the screenshots
        public static string GetScreenshot(IWebDriver driver, string screenshotName)
        {
            try
            {
                // Get the current TimeStamp
                string timeStamp = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff");
                string screenShotPath = Directory.GetCurrentDirectory() + "\\Screenshot\\";
                if (!Directory.Exists(screenShotPath))
                {
                    Directory.CreateDirectory(screenShotPath);
                }
                string fileName = screenShotPath + screenshotName + Const.ScreenshotUnderScore + timeStamp + Const.ScreenshotImageFormat;
                // Take a screenshot
                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile(fileName, ScreenshotImageFormat.Png);
                // Return the filename of the screenshot
                return fileName;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // This method is used to refresh the page
        public static void RefreshPage(IWebDriver driver)
        {
            driver.Navigate().Refresh();
        }

        // This method is used to select the value from the dropdown
        public static void SelectDropDownByValue(IWebDriver driver, By locator, String value)
        {
            SelectElement dropdown = new SelectElement(driver.FindElement(locator));
            dropdown.SelectByValue(value);
        }

        // This method is used to select the text from the dropdown
        public static void SelectDropDownByText(IWebDriver driver, By locator, String value)
        {
            IWebElement we = GenericUtils.WaitForTextInSelect(driver, locator, 15,value);
           // IWebElement we= GenericUtils.WaitForElementPresence(driver, locator, 15);
            SelectElement dropdown = new SelectElement(we);
            dropdown.SelectByText(value);
        }

        // This method is used to navigate to url which is being passed as a parameter
        public static void OpenNewBrowserWindow(IWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        // This method is used to get reduced amount by 1
        public static string ReducedAmount(string amount)
        {
            return (ConvertStringToDouble(amount) - 1).ToString();
        }

        // This method is used to get increased amount by 1
        public static string IncreaseAmount(string amount)
        {
            return (ConvertStringToDouble(amount) + 1).ToString();
        }
        // This method is used to get substract two value and return in string format.
        public static string SubtractTwoValue(string firstValue, string secondValue)
        {
            try
            {
                var firstValueToDouble = Double.Parse(firstValue);
                var secondValueToDouble = Double.Parse(secondValue);
                double difference = Math.Abs(firstValueToDouble - secondValueToDouble);
                return difference.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // This method is used to get add two value and return in string format.
        public static string AddTwoValue(string firstValue,string secondValue)
        {
            try
            {
                var firstValueToDouble = Double.Parse(firstValue);
                var secondValueToDouble = Double.Parse(secondValue);
                double difference = Math.Abs(firstValueToDouble + secondValueToDouble);
                return difference.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // This method is used to get the Fee amount in case of Buy orders
        public static string FeeAmount(string amount, string feeComponent)
        {
            double orderSizeInDouble = Double.Parse(amount);
            double feeComponentInDouble = Double.Parse(feeComponent);
            double feeValue = orderSizeInDouble * feeComponentInDouble;
            return feeValue.ToString();
        }

        // This method is used to get the Fee amount in case of Sell orders
        public static string SellFeeAmount(string orderSize, string limitPrice, string feeComponent)
        {
            double orderSizeInDouble = Double.Parse(orderSize);
            double limitPriceInDouble = Double.Parse(limitPrice);
            double feeComponentInDouble = Double.Parse(feeComponent);
            double feeValue = (orderSizeInDouble * limitPriceInDouble * feeComponentInDouble);
            return feeValue.ToString("0.00000000");
        }

        // This method is used to get the difference of the two string values passed
        public string GetDifferenceFromString(string firstValue, string secondValue)
        {
            string firstValuePrecision = Convert.ToDecimal(firstValue).ToString("#,##0.00000000");
            string secondValuePrecision = Convert.ToDecimal(secondValue).ToString("#,##0.00000000");
            var firstValueToDouble = Double.Parse(firstValue);
            var secondValueToDouble = Double.Parse(secondValue);
            double difference = Math.Abs(firstValueToDouble - secondValueToDouble);
            string differenceInString = String.Format("{0:0.00000000}", difference);
            return differenceInString;
        }

        // This method Generates random string: takes length as a parameter and will generate a random string of the same length
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = Const.RandomChars;
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // This method returns the list of Key value pairs from the csv file
        public List<KeyValuePair<string, string>> ReadDataFromCSV(string filePath)
        {
            bool isHeader = true;
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
                    logger.LogCheckPoint(""+headers);    
                    for(int i=0; i < values.Length; i++)
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
            return csvFileData;
        }

        // This method is used to Delete all files from the Folder
        public void DeleteAllFiles()
        {
            try
            {
                string dataTestPath = Directory.GetCurrentDirectory() + "\\DataTest\\";
                if (!Directory.Exists(dataTestPath))
                {
                    Directory.CreateDirectory(dataTestPath);
                }
                System.IO.DirectoryInfo di = new DirectoryInfo(dataTestPath);
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static bool VerifyPresentWebElement(IWebElement we)
        {
            bool val = false;
            try
            {
               val=we.Displayed && we.Enabled;
               val = true;
            }
            catch (NoSuchElementException)
            {
                val = false;
            }
            return val;
        }




    }
}
