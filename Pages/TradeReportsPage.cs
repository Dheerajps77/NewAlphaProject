using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using AlphaPoint_QA.Common;
using AlphaPoint_QA.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace AlphaPoint_QA.Pages
{
    class TradeReportsPage
    {
        IWebDriver driver;
        ProgressLogger logger;

        By tradeReportsLink = By.CssSelector("li[data-test='Trade Reports']");
        By singleReportButton = By.CssSelector("div.activity-reporting__actions-holder > button:nth-of-type(1)");
        By cyclicReportButton = By.CssSelector("div.activity-reporting__actions-holder > button:nth-of-type(2)");
        By singleReportText = By.CssSelector("div[data-test='Single Report']");
        By cyclicReportText = By.CssSelector("div[data-test='Cyclical Report']");
        By singleReportsTab = By.CssSelector("div.ap-tab__menu.reports-sidepane__menu > div:nth-of-type(1)");
        By cyclicReportsTab = By.CssSelector("div.ap-tab__menu.reports-sidepane__menu > div:nth-of-type(2)");
        By singleReportsTabSelected = By.CssSelector("div.ap-tab__menu-item.ap-tab__menu-item--active.reports-sidepane__menu-item.reports-sidepane__menu-item--active.ap-tab__menu-item.reports-sidepane__menu-item");
        By cyclicReportsTabSelected = By.CssSelector("div[data-test='Cyclical Report']");
        By accountsCheckbox = By.CssSelector("div.ap-checkbox__checkbox.report-form__checkbox");
        By reportType = By.CssSelector(".form-control.ap-select__select.report-form__select");
        By startDate = By.CssSelector("input[name=startDate]");
        By endDate = By.CssSelector("input[name=endDate]");
        By frequencyCyclicReport = By.CssSelector("select[name=frequency]");
        By createTradeReportButton = By.CssSelector(".ap-button__btn.ap-button__btn--additive.report-form__btn.report-form__btn--additive");
        By confirmationModalCreateReportButton = By.CssSelector("button.ap-button__btn.ap-button__btn--additive.ap-modal.confirm-report-modal__btn.ap-modal.confirm-report-modal__btn--additive");
        By dateWidgetFormElement = By.CssSelector("table.pika-table > tbody");
        By confirmSingleReport = By.CssSelector("div.confirm-single-report p");
        By reportTypeActivity = By.CssSelector("span[data-test='Report Type:']");
        By startDateActivity = By.CssSelector("span[data-test='Start Date:']");
        By endDateActivity = By.CssSelector("span[data-test='End Date:']");
        By tradeActivityText = By.CssSelector("select.form-control.ap-select__select.report-form__select option[value=TradeActivity]");
        By transactionActivityText = By.CssSelector("select.form-control.ap-select__select.report-form__select option[value=Transaction]");
        By treasuryActivityText = By.CssSelector("select.form-control.ap-select__select.report-form__select option[value=Treasury]");
        By startDateOnConfirmationSingleReportModal = By.CssSelector("span[data-test='Start Date:']");
        By endDateOnConfirmationSingleReportModal = By.CssSelector("span[data-test='End Date:']");
        By refreshReportsButton = By.CssSelector("button.ap-inline-btn__btn.ap-inline-btn__btn--general.download-report__refresh-reports__btn.download-report__refresh-reports__btn--general");
        By reportSummaryText = By.CssSelector("div.flex-table__row.download-report__row:nth-of-type(1) div:nth-of-type(1)");
        By reportCreatedDateTimeText = By.CssSelector("div.flex-table__row.download-report__row:nth-of-type(1) div:nth-of-type(2)");
        By downloadReport = By.CssSelector("div.flex-table__row.download-report__row:nth-of-type(1) div:nth-of-type(3) button");
        By cancelReport = By.CssSelector("div.flex-table__row.schedule-report-list__row:nth-of-type(1) div:nth-of-type(4) button");
        By countOfSingleTradeReports = By.CssSelector("div.flex-table__body.download-report__body > div");
        By countOfCyclicTradeReports = By.CssSelector("div.flex-table__row.schedule-report-list__row");
        By reportCreatedSuccessMsg = By.CssSelector("div.snackbar__text.snackbar__text--success.custom-snackbar__text.custom-snackbar__text--success");
        By frquencyDropdown = By.CssSelector("[name=frequency]");
        By cyclicReportSummaryText = By.CssSelector("div.flex-table__row.schedule-report-list__row:nth-of-type(1) div:nth-of-type(1)");
        By cyclicReportFrequencyText = By.CssSelector("div.flex-table__row.schedule-report-list__row:nth-of-type(1) div:nth-of-type(2)");
        By cyclicReportCreatedDateTimeText = By.CssSelector("div.flex-table__row.schedule-report-list__row:nth-of-type(1) div:nth-of-type(3)");
        By cyclicReportActionText = By.CssSelector("div.flex-table__row.schedule-report-list__row:nth-of-type(1) div:nth-of-type(4)");
        By noReportIsAvailableText = By.CssSelector("div.flex-table__column.flex-table__column--empty.schedule-report-list__column-empty");
        By createCyclicReportButton = By.CssSelector("button.ap-button__btn.ap-button__btn--additive.report-form__btn.report-form__btn--additive");

        public TradeReportsPage(IWebDriver driver, ProgressLogger logger)
        {
            this.driver = driver;
            this.logger = logger;
        }

        public IWebElement ReportCreatedSuccessMsg()
        {
            return driver.FindElement(reportCreatedSuccessMsg);
        }

        public IWebElement CreateCyclicReportButton()
        {
            return driver.FindElement(createCyclicReportButton);
        }

        public IWebElement FrquencyDropdown()
        {
            return driver.FindElement(frquencyDropdown);
        }

        public IWebElement NoReportIsAvailableText()
        {
            return driver.FindElement(noReportIsAvailableText);
        }

        public IWebElement ReportSummaryText()
        {
            return driver.FindElement(reportSummaryText);
        }

        public IWebElement ReportCreatedDateTimeText()
        {
            return driver.FindElement(reportCreatedDateTimeText);
        }

        public IWebElement CyclicReportSummaryText()
        {
            return driver.FindElement(cyclicReportSummaryText);
        }

        public IWebElement CyclicReportFrequencyText()
        {
            return driver.FindElement(cyclicReportFrequencyText);
        }

        public IWebElement CyclicReportCreatedDateTimeText()
        {
            return driver.FindElement(cyclicReportCreatedDateTimeText);
        }

        public IWebElement CyclicReportActionText()
        {
            return driver.FindElement(cyclicReportActionText);
        }

        public int CountOfCyclicTradeReports()
        {
            return driver.FindElements(countOfCyclicTradeReports).Count;
        }

        public int CountOfSingleTradeReports()
        {
            return driver.FindElements(countOfSingleTradeReports).Count;
        }
        public IWebElement DownloadReport()
        {
            return driver.FindElement(downloadReport);
        }

        public IWebElement RefreshReportsButton()
        {
            return driver.FindElement(refreshReportsButton);
        }

        public IWebElement StartDateOnConfirmationSingleReportModal()
        {
            return driver.FindElement(startDateOnConfirmationSingleReportModal);
        }

        public IWebElement ConfirmationModalCreateReportButton()
        {
            return driver.FindElement(confirmationModalCreateReportButton);
        }

        public IWebElement EndDateOnConfirmationSingleReportModal()
        {
            return driver.FindElement(endDateOnConfirmationSingleReportModal);
        }

        public IWebElement TradeActivityText()
        {
            return driver.FindElement(tradeActivityText);
        }

        public IWebElement TransactionActivityText()
        {
            return driver.FindElement(transactionActivityText);
        }

        public IWebElement TreasuryActivityText()
        {
            return driver.FindElement(treasuryActivityText);
        }

        public IWebElement ReportTypeActivity()
        {
            return driver.FindElement(reportTypeActivity);
        }

        public IWebElement StartDateActivity()
        {
            return driver.FindElement(startDateActivity);
        }

        public IWebElement EndDateActivity()
        {
            return driver.FindElement(endDateActivity);
        }

        public IWebElement ConfirmSingleReport()
        {
            return driver.FindElement(confirmSingleReport);
        }

        public IWebElement DateWidgetFormElement()
        {
            return driver.FindElement(dateWidgetFormElement);
        }

        public IWebElement TradeReportsLink()
        {
            return driver.FindElement(tradeReportsLink);
        }

        public IWebElement SingleReportButton()
        {
            return driver.FindElement(singleReportButton);
        }

        public IWebElement CyclicReportButton()
        {
            return driver.FindElement(cyclicReportButton);
        }

        public IWebElement SingleReportText()
        {
            return driver.FindElement(singleReportText);
        }

        public IWebElement CyclicReportText()
        {
            return driver.FindElement(cyclicReportText);
        }

        public IWebElement SingleReportTab()
        {
            return driver.FindElement(singleReportsTab);
        }

        public IWebElement CyclicReportTab()
        {
            return driver.FindElement(cyclicReportsTab);
        }

        
        public IWebElement CancelReport()
        {
            return driver.FindElement(cancelReport);
        }

        public IWebElement SingleReportsTabSelected()
        {
            return driver.FindElement(singleReportsTabSelected);
        }

        public IWebElement CyclicReportsTabSelected()
        {
            return driver.FindElement(cyclicReportsTabSelected);
        }

        public IWebElement AccountsCheckbox()
        {
            return driver.FindElement(accountsCheckbox);
        }

        public IWebElement ReportType()
        {
            return driver.FindElement(reportType);
        }

        public IWebElement StartDate()
        {
            return driver.FindElement(startDate);
        }

        public IWebElement EndDate()
        {
            return driver.FindElement(endDate);
        }

        public IWebElement FrequencyCyclicReport()
        {
            return driver.FindElement(frequencyCyclicReport);
        }

        public IWebElement CreateTradeReportButton()
        {
            return driver.FindElement(createTradeReportButton);
        }


        public void ClickRefreshReportsButton()
        {
            try
            {
                Thread.Sleep(1000);
                UserSetFunctions.Click(RefreshReportsButton());
                Thread.Sleep(1000);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This method Verify the details of "report type", "on demand" and "from and to date" of Report summary"
        public void VerifySingleReportSummaryDetails()
        {
            string singleReportTradeActivityValue = TestData.GetData("TC47_SingleReportTradeActivityValue");
            string singleReportTransactionActivityValue = TestData.GetData("TC47_SingleReportTransactionActivityValue");
            string singleReportTreasuryActivitytValue = TestData.GetData("TC47_SingleReportTreasuryActivitytValue");


            string TradeActivity = TradeActivityText().Text;
            string TransactionActivity = TransactionActivityText().Text;
            string TreasuryActivity = TreasuryActivityText().Text;

            try
            {
                if (TradeActivity.Equals(singleReportTradeActivityValue))
                {
                    try
                    {
                        logger.LogCheckPoint(String.Format(LogMessage.SingleReportTradeActivityPassed, TradeActivity));
                    }
                    catch (Exception)
                    {
                        logger.Error(String.Format(LogMessage.SingleReportTradeActivityFailed, TradeActivity));
                    }
                }
                else if (TransactionActivity.Equals(singleReportTransactionActivityValue))
                {
                    try
                    {
                        logger.LogCheckPoint(String.Format(LogMessage.SingleReportTransactionActivityPassed, TransactionActivity));
                    }
                    catch (Exception)
                    {
                        logger.Error(String.Format(LogMessage.SingleReportTransactionActivityFailed, TransactionActivity));
                    }
                }
                else if (TreasuryActivity.Equals(singleReportTreasuryActivitytValue))
                {
                    try
                    {
                        logger.LogCheckPoint(String.Format(LogMessage.SingleReportTreasuryActivityPassed, TreasuryActivity));
                    }
                    catch (Exception)
                    {
                        logger.Error(String.Format(LogMessage.SingleReportTreasuryActivityFailed, TreasuryActivity));
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        // This method verifies that the Single and Cyclic Report tabs are present
        // By default Single Reports tab is selected
        public bool VerifySingleReportTabIsSelectedAndCyclicReportAndSingleReportTabPresent()
        {
            string singleReport = TestData.GetData("TC47_SingleReportValue");
            string cyclicReport = TestData.GetData("TC47_CyclicReportValue");
            bool flag = false;
            Thread.Sleep(1000);

            try
            {
                if (SingleReportText().Text.Equals(singleReport) && CyclicReportText().Text.Equals(cyclicReport))
                {
                    if (SingleReportsTabSelected().Displayed)
                    {
                        flag = true;
                        logger.LogCheckPoint(String.Format(LogMessage.SingleReportAndCyclicReportPassed, singleReport, cyclicReport));
                        return flag;
                    }
                    else
                    {
                        UserSetFunctions.Click(SingleReportTab());
                        logger.Error(String.Format(LogMessage.SingleReportAndCyclicReportFailed, singleReport, cyclicReport));
                    }
                }
                return flag;
            }
                        
            catch(Exception)
            {
                throw;
            }
        }

        // This method verifies that the "Verify below fields are present: accounts, report type and start date, end date"
        public bool VerifySingleReportFields()
        {
            bool flag = false;
            Thread.Sleep(1000);

            try
            {
                if (AccountsCheckbox().Displayed && ReportType().Displayed && StartDate().Displayed && EndDate().Displayed)
                {
                    flag = true;
                    logger.LogCheckPoint(String.Format(LogMessage.FieldsOnSingleReportPassed));
                    return flag;
                }
                else
                {
                    logger.Error(String.Format(LogMessage.FieldsOnSingleReportFailed));
                }
                return flag;
            }
            catch(Exception)
            {
                throw;
            }
        }

        // This method verifies Current Date - 1 and all previous dates are selectable         
        public bool VerifyStartDateSelectableDatesForSingleReport()
        {
            int today;
            bool flag = false;
            today = GenericUtils.GetOnlyCurrentDate();
            string currentDate = GenericUtils.GetCurrentDate();

            // Click on "Start Date"
            UserSetFunctions.Click(StartDate());
            IWebElement dateWidgetFrom = driver.FindElement(By.XPath("//table[@class='pika-table']/tbody"));
            // This are the columns of the from date picker table
            List<IWebElement> columns = new List<IWebElement>();
            var dateColumn = dateWidgetFrom.FindElements(By.ClassName("is-disabled"));
            foreach (IWebElement fields in dateColumn)
            {
                string disabledFields = fields.Text;
                int fieldValueInInt = Int32.Parse(disabledFields);
                if (fieldValueInInt >= today)
                {
                    flag = true;
                    logger.LogCheckPoint(String.Format(LogMessage.StartCurrentDateAndPreviousDatePassed, currentDate));
                    return flag;
                }
                else
                {
                    logger.Error(String.Format(LogMessage.StartCurrentDateAndPreviousDateFailed, currentDate));
                    return flag;
                }
            }
            return flag;
        }


        // This method verifies Current Date - 1 and all futures dates are selectable         
        public bool VerifyStartDateSelectableDatesForCyclicReport(string startDate)
        {
            int today;
            bool flag = false;
            today = GenericUtils.GetOnlyCurrentDate();
            string currentDate = GenericUtils.GetCurrentDate();

            try
            {
                //Click on "Start Date"
                UserSetFunctions.Click(StartDate());
                IWebElement dateWidgetFrom = driver.FindElement(By.XPath("//table[@class='pika-table']/tbody"));
                //This are the columns of the from date picker table
                List<IWebElement> columns = new List<IWebElement>();
                if (today == Int32.Parse(TestData.GetData("TC48_FirstDayOfMonth")))
                {
                    StartDate().SendKeys(startDate);
                }
                else
                {
                    var dateColumn = dateWidgetFrom.FindElements(By.ClassName("is-disabled"));
                    foreach (IWebElement fields in dateColumn)
                    {
                        string disabledFields = fields.Text;
                        int fieldValueInInt = Int32.Parse(disabledFields);
                        if (fieldValueInInt < today)
                        {
                            flag = true;
                            logger.LogCheckPoint(String.Format(LogMessage.StartCurrentDateAndFutureDatePassed, currentDate));
                            StartDate().SendKeys(startDate);
                            return flag;
                        }
                        else
                        {
                            logger.Error(String.Format(LogMessage.StartCurrentDateAndFutureDateFailed, currentDate));
                            return flag;
                        }
                    }
                }
                return flag;
            }
            catch(Exception)
            {
                throw;
            }
        }
        // This method verifies selectable dates are greater than start date and equal to current date
        public bool VerifyEndDateSelectableDates(string startDate)
        {
            int today;
            string disabledFields;
            int startDateSelected = GenericUtils.GetOnlyDateFromDateString(startDate);
            bool flag = false;

            try
            {
                today = GenericUtils.GetOnlyCurrentDate();
                Thread.Sleep(2000);
                UserSetFunctions.Click(EndDate());
                IWebElement dateWidgetEnd = driver.FindElement(By.XPath("//table[@class='pika-table' and @aria-labelledby='pika-title-zk']/tbody"));
                //This are the columns of the from date picker table
                List<IWebElement> columns = new List<IWebElement>();
                var dateColumn = dateWidgetEnd.FindElements(By.ClassName("is-disabled"));

                foreach (IWebElement fields in dateColumn)
                {
                    disabledFields = fields.Text;
                    int fieldValueInInt = Int32.Parse(disabledFields);
                    if (fieldValueInInt > today)
                    {
                        logger.LogCheckPoint(String.Format(LogMessage.EndCurrentDateGreaterThanStartDatePassed));//fieldValueInInt
                    }
                    else
                    {
                        logger.LogCheckPoint(String.Format(LogMessage.EndCurrentDateGreaterThanStartDateFailed));
                    }
                    if (fieldValueInInt < startDateSelected)
                    {
                        logger.LogCheckPoint(String.Format(LogMessage.LessThanStartDatePassed, fieldValueInInt));
                    }
                    else
                    {
                        logger.LogCheckPoint(String.Format(LogMessage.LessThanStartDateFailed, fieldValueInInt));
                    }
                }
                return flag;
            }
            catch(Exception)
            {
                throw;
            }
        }

        //This method will verify the confirmation modal window for single report
        public void VerifySingleReportModalConfirmation()
        {
            string actualConfirmationSingleReport;
            string expectedConfirmationSingleReport = TestData.GetData("TC47_Confirm");
            string confirmationReportReportModalValue = TestData.GetData("TC47_SingleReportConfirmationModal");
            try
            {
                actualConfirmationSingleReport = ConfirmSingleReport().Text;
                Assert.Equal(actualConfirmationSingleReport, expectedConfirmationSingleReport);
                logger.LogCheckPoint(String.Format(LogMessage.ConfirmationModalPassed, confirmationReportReportModalValue));
            }
            catch (Exception)
            {
                logger.LogCheckPoint(String.Format(LogMessage.ConfirmationModalFailed, confirmationReportReportModalValue));
            }
        }

        //This method returns the list of all Cyclic trade reports
        public ArrayList GetListOfCyclicTradeReports()
        {
            ArrayList cyclicTradeReportList = new ArrayList();
            int countOfCyclicTradeReports = CountOfCyclicTradeReports();
            for (int i = 1; i <= countOfCyclicTradeReports; i++)
            {
                String textFinal = "";

                int countItems = driver.FindElements(By.XPath("(//div[@class='flex-table__row schedule-report-list__row'])[" + i + "]")).Count;
                int countItems1 = driver.FindElements(By.XPath("(//div[@class='flex-table__row schedule-report-list__row'])/div")).Count;
                for (int j = 1; j <= countItems1; j++)
                {
                    String text = driver.FindElement(By.XPath("(//div[@class='flex-table__row schedule-report-list__row'])[" + i + "]/div[" + j + "]")).Text;
                    if (j == 1)
                    {
                        textFinal = text;
                    }
                    else
                    {
                        textFinal = textFinal + " || " + text;
                    }

                }
                cyclicTradeReportList.Add(textFinal);
            }
            return cyclicTradeReportList;
        }

        //This method will verify the single report in under cyclic reports 
        public bool VerifyTopMostOfCyclicTradeReports(string reportType, string frequency, string createdDateTime, string action)
        {
            try
            {
                var flag = false;

                string expectedRow_1 = reportType + " || " + frequency + " || " + createdDateTime + " || " + action;

                var listOfTradeReports = GetListOfCyclicTradeReports();
                if (listOfTradeReports.Contains(expectedRow_1))
                {
                    flag = true;
                }
                if (flag)
                {
                    logger.LogCheckPoint(String.Format(LogMessage.VerifyAvailableCyclicTradeReportsPassed, reportType, frequency, createdDateTime, action));
                }
                else
                {
                    logger.LogCheckPoint(String.Format(LogMessage.VerifyAvailableCyclicTradeReportsFailed, reportType, frequency, createdDateTime, action));
                }

                Thread.Sleep(1000);

                // Verify the "cancel report" button to cancel the report
                if (CancelReport().Enabled)
                {
                    logger.LogCheckPoint(String.Format(LogMessage.VerifycancelReportOfCyclicReportPassed, action));
                }
                else
                {
                    logger.LogCheckPoint(String.Format(LogMessage.VerifycancelReportOfCyclicReportFailed, action));
                }
                return flag;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This method returns the list of all Single trade reports
        public ArrayList GetListOfSingleTradeReports()
        {
            ArrayList singleTradeReportList = new ArrayList();
            int countOfSingleTradeReports = CountOfSingleTradeReports();
            for (int i = 1; i <= countOfSingleTradeReports; i++)
            {
                String textFinal = "";
                int countItems = driver.FindElements(By.XPath("(//div[@class='flex-table__body download-report__body']/div)[" + i + "]/div")).Count;
                for (int j = 1; j <= countItems; j++)
                {
                    String text = driver.FindElement(By.XPath("(//div[@class='flex-table__body download-report__body']/div)[" + i + "]/div[" + j + "]")).Text;
                    if (j == 1)
                    {
                        textFinal = text;
                    }
                    else
                    {
                        textFinal = textFinal + " || " + text;
                    }

                }
                singleTradeReportList.Add(textFinal);
            }
            return singleTradeReportList;
        }

        //This method will verify the single report in under report available to download 
        public bool VerifyDownloadableTradeReports(string tradeActivity, string createdDateTime, string downloadBtnText)
        {
            try
            {
                var flag = false;

                string expectedRow_1 = tradeActivity + " || " + createdDateTime + " || " + downloadBtnText;

                var listOfTradeReports = GetListOfSingleTradeReports();
                if (listOfTradeReports.Contains(expectedRow_1))
                {
                    flag = true;
                }
                if (flag)
                {
                    logger.LogCheckPoint(String.Format(LogMessage.VerifyAvailableSingleTradeReportsPassed, tradeActivity, createdDateTime, downloadBtnText));
                }
                else
                {
                    logger.Error(String.Format(LogMessage.VerifyAvailableSingleTradeReportsFailed, tradeActivity, createdDateTime, downloadBtnText));
                }

                Thread.Sleep(1000);

                // Verify "Download Report" button to download the report
                if (DownloadReport().Enabled)
                {
                    logger.LogCheckPoint(String.Format(LogMessage.VerifyDownloadReportOfSingleReportPassed, downloadBtnText));
                }
                else
                {
                    logger.Error(String.Format(LogMessage.VerifyDownloadReportOfSingleReportFailed, downloadBtnText));
                }
                return flag;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This method will verify the trade activites of Single Report 
        public bool VerifySingleReportData(string reportTypeValue, string startDate, string endDate)
        {
            bool flag = false;
            string ConfirmationSingleReportModalValue = TestData.GetData("TC47_SingleReportConfirmationModal");

            try
            {
                // Click on "Dashboard" menu button --> "UserSetting" menu button --> "Trade Reports" button
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.NavigateToUserSetting(driver);
                Thread.Sleep(2000);
                UserSetFunctions.Click(TradeReportsLink());
                Thread.Sleep(1000);

                // Click on "Single Report" button
                UserSetFunctions.Click(SingleReportButton());

                // Verify if Single Report tab is selected bydefault and single, Cyclic tab is present
                if (VerifySingleReportTabIsSelectedAndCyclicReportAndSingleReportTabPresent())
                {
                    string confirmationModalStartDate;
                    string confirmationModalEndDate;
                    string expectedConfirmationModalEndDate;
                    string singleReportMsg;
                    string reportSummaryTextValue;
                    string createdDateTimeTextValue;
                    string downloadReportBtnTextValue;


                    //Verify below fields are present:accounts, report type and start date, end date              
                    if (VerifySingleReportFields())
                    {
                        Thread.Sleep(1000);

                        UserSetFunctions.Click(SingleReportsTabSelected());
                        Thread.Sleep(1000);
                        UserSetFunctions.SelectDropdown(ReportType(), reportTypeValue);

                        // Verify Current Date - 1 and all previous dates are selectable 
                        if (VerifyStartDateSelectableDatesForSingleReport())
                        {
                            Thread.Sleep(2000);
                            if (startDate == null)
                            {
                                startDate = GenericUtils.GetCurrentDateMinusOne();
                            }
                            Thread.Sleep(1000);

                            // Enter "Start date"
                            StartDate().SendKeys(startDate);
                            Thread.Sleep(1000);
                            flag = true;
                        }

                        // Click on "End date"
                        UserSetFunctions.Click(EndDate());
                        Thread.Sleep(1000);
                        if (endDate == null)
                        {
                            endDate = GenericUtils.GetCurrentDate();
                        }
                        Thread.Sleep(2000);
                        // Enter "End date"
                        EndDate().SendKeys(endDate);
                        flag = true;

                        Thread.Sleep(1000);

                        // Click on "Create Report" button
                        UserSetFunctions.Click(CreateTradeReportButton());

                        // Verify confirmation modal is displayed
                        VerifySingleReportModalConfirmation();

                        confirmationModalStartDate = StartDateOnConfirmationSingleReportModal().Text;
                        confirmationModalEndDate = EndDateOnConfirmationSingleReportModal().Text;
                        expectedConfirmationModalEndDate = endDate.Trim();
                        var strtDate = DateTime.Parse(startDate.Trim());
                        var endDte = DateTime.Parse(expectedConfirmationModalEndDate);

                        // This will verify the start and end date in the confirmation window modal of single report
                        if (DateTime.Parse(confirmationModalStartDate).Equals(strtDate) && DateTime.Parse(confirmationModalEndDate).Equals(endDte))
                        {
                            logger.LogCheckPoint(String.Format(LogMessage.StartAndEndDateConfirmationModalPassed, strtDate.ToString("MM/dd/yyyy"), endDte.ToString("MM/dd/yyyy")));
                        }
                        else
                        {
                            logger.Error(String.Format(LogMessage.StartAndEndDateConfirmationModalFailed, strtDate.ToString("MM/dd/yyyy"), endDte.ToString("MM/dd/yyyy")));
                        }
                        UserSetFunctions.Click(ConfirmationModalCreateReportButton());
                        singleReportMsg = ReportCreatedSuccessMsg().Text;

                        // This will verify the success message after creating the a single rpeort
                        if (singleReportMsg.Equals(Const.SingleTradeReportMsg))
                        {
                            logger.LogCheckPoint(String.Format(LogMessage.SuccessMsgPassed, ConfirmationSingleReportModalValue));
                        }
                        else
                        {
                            logger.Error(String.Format(LogMessage.SuccessMsgFailed, ConfirmationSingleReportModalValue));
                        }

                        ClickRefreshReportsButton();
                        reportSummaryTextValue = ReportSummaryText().Text;
                        createdDateTimeTextValue = ReportCreatedDateTimeText().Text;
                        downloadReportBtnTextValue = DownloadReport().Text;

                        // Verify the "created time", Report summary is "report type" , on demand and from and to date.
                        // Verify the report is downloadable
                        VerifyDownloadableTradeReports(reportSummaryTextValue, createdDateTimeTextValue, downloadReportBtnTextValue);
                    }
                }
                return flag;
            }
            catch(Exception)
            {
                throw;
            }
        }


        // This method verifies that the Single and Cyclic Report tabs are present
        // By default Cyclic Reports tab is selected
        public bool VerifyCyclicReportTabIsSelectedAndSingleReportAndCyclicReportTabPresent()
        {
            string singleReport = TestData.GetData("TC48_SingleReportValue");
            string cyclicReport = TestData.GetData("TC48_CyclicReportValue");
            bool flag = false;
            Thread.Sleep(1000);

            try
            {
                if (SingleReportText().Text.Equals(singleReport) && CyclicReportText().Text.Equals(cyclicReport))
                {
                    if (CyclicReportsTabSelected().Displayed)
                    {
                        flag = true;
                        logger.LogCheckPoint(String.Format(LogMessage.CyclicReportAndSingleReportPassed, singleReport, cyclicReport));
                        return flag;
                    }
                    else
                    {
                        logger.Error(String.Format(LogMessage.CyclicReportAndSingleReportFailed, singleReport, cyclicReport));
                        UserSetFunctions.Click(CyclicReportTab());
                    }
                }
                return flag;
            }
            catch(Exception)
            {
                throw;
            }
        }

       
        // This method verifies the various fields : accounts, report type and start date, frequency visibility
        //of Cyclic report
        public bool VerifyCyclicReportFields()
        {

            bool flag = false;
            Thread.Sleep(1000);
            try
            {
                if (AccountsCheckbox().Displayed && ReportType().Displayed && StartDate().Displayed)
                {
                    flag = true;
                    logger.LogCheckPoint(String.Format(LogMessage.FieldsOnCyclicReportPassed));
                    return flag;
                }
                else
                {
                    logger.Error(String.Format(LogMessage.FieldsOnCyclicReportFailed));
                }
                return flag;
            }
            catch(Exception)
            {
                throw;
            }
        }

        //This method will verify the frequency dropdown values of cyclic report and select hour value from dropdown
        public bool VerifyCyclicReportFrequencyDropdownValues()
        {
            string hourly = TestData.GetData("TC48_HourlyFrequencyValue");
            string daily = TestData.GetData("TC48_DailyFrequencyValue");
            string weekly = TestData.GetData("TC48_WeeklyFrequencyValue");
            string monthly = TestData.GetData("TC48_MonthlyFrequencyValue");
            string annual = TestData.GetData("TC48_AnnualFrequencyValue");

            try
            {
                bool flag = false;
                ArrayList listOfAllDropdownValues = new ArrayList();

                listOfAllDropdownValues.Add(hourly);
                listOfAllDropdownValues.Add(daily);
                listOfAllDropdownValues.Add(weekly);
                listOfAllDropdownValues.Add(monthly);
                listOfAllDropdownValues.Add(annual);

                SelectElement select = new SelectElement(FrquencyDropdown());
                IList<IWebElement> option = select.Options;
                ArrayList selectDropdownValues = new ArrayList();

                // This will get the total count from Frequency dropdown list
                int itemsCount = option.Count;
                for (int i = 0; i < itemsCount; i++)
                {
                    selectDropdownValues.Add(option[i].Text);
                    flag = true;
                }
                if (flag)
                {
                    // Verify the if "hourly", "daily", "weekly", "monthly" and "annual "present in dropdown
                    if (selectDropdownValues.Contains(hourly)
                        && selectDropdownValues.Contains(daily)
                        && selectDropdownValues.Contains(weekly)
                        && selectDropdownValues.Contains(monthly)
                        && selectDropdownValues.Contains(annual))
                    {
                        logger.LogCheckPoint(String.Format(LogMessage.FrequencyDropdownValuesPassed, hourly, daily, weekly, monthly, annual));
                    }
                    else
                    {
                        logger.LogCheckPoint(String.Format(LogMessage.FrequencyDropdownValuesFailed, hourly, daily, weekly, monthly, annual));
                    }
                }
                Thread.Sleep(1000);

                // select hourly from dropdown
                select.SelectByText(hourly);
                return flag;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        //This method will verify the confirmation modal window of cyclic report
        public void VerifyCyclicReportModalConfirmation()
        {
            string expectedConfirmationCyclicReport = TestData.GetData("TC48_Confirm");
            string ConfirmationCyclicReportModalValue = TestData.GetData("TC48_CyclicReportConfirmationModal");
            try
            {
                string actualConfirmationCyclicReport = ConfirmSingleReport().Text;
                Assert.Equal(actualConfirmationCyclicReport, expectedConfirmationCyclicReport);
                logger.LogCheckPoint(String.Format(LogMessage.ConfirmationModalPassed, ConfirmationCyclicReportModalValue));
            }
            catch (Exception)
            {
                logger.LogCheckPoint(String.Format(LogMessage.ConfirmationModalFailed, ConfirmationCyclicReportModalValue));
            }
        }


        //This method will verify the trade activites of Cyclic Report 
        public bool VerifyCyclicReportData(string reportTypeValue, string startDate)
        {
            bool flag = false;
            string ConfirmationCyclicReportModalValue;
            string confirmationModalStartDate;
            string cyclicReportMSg;
            string cyclicReportSummaryTextValue;
            string cyclicReportFrequencyTextValue;
            string cyclicReportCreatedDateTimeTextValue;
            string cyclicReportActionTextValue;
            string noReportMsg;
            string reportSummaryTextValue;
            string createdDateTimeTextValue;
            string downloadReportBtnTextValue;

            ConfirmationCyclicReportModalValue = TestData.GetData("TC48_CyclicReportConfirmationModal");

            try
            {
                // Click on "Dashboard" menu button --> "UserSetting" menu button --> "Trade Reports" button
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.NavigateToUserSetting(driver);
                Thread.Sleep(2000);
                UserSetFunctions.Click(TradeReportsLink());
                Thread.Sleep(1000);

                // Click on "Cyclical Report" button
                UserSetFunctions.Click(CyclicReportButton());

                // Verify if Cyclic Report tab is selected bydefault and single, Cyclic tab is present
                if (VerifyCyclicReportTabIsSelectedAndSingleReportAndCyclicReportTabPresent())
                {
                    //Verify below fields are present:accounts, report type and start date, frequency
                    // Verify Frequency has dropdown values hourly, daily, weekly, monthly and annual
                    if (VerifyCyclicReportFields() && VerifyCyclicReportFrequencyDropdownValues())
                    {
                        Thread.Sleep(1000);
                        UserSetFunctions.Click(CyclicReportsTabSelected());
                        Thread.Sleep(1000);
                        UserSetFunctions.SelectDropdown(ReportType(), reportTypeValue);
                        if (VerifyStartDateSelectableDatesForCyclicReport(startDate))
                        {
                            Thread.Sleep(2000);
                            if (startDate == null)
                            {
                                startDate = GenericUtils.GetCurrentDatePlusOne();
                            }
                            flag = true;
                        }

                        // Click on "Create" button
                        UserSetFunctions.Click(CreateCyclicReportButton());

                        // Verify the details in cyclic report confirmation modal window
                        VerifyCyclicReportModalConfirmation();

                        confirmationModalStartDate = StartDateOnConfirmationSingleReportModal().Text;
                        var strtDate = DateTime.Parse(startDate.Trim());

                        // This will verify the start date in the confirmation window modal of cyclic report
                        if (DateTime.Parse(confirmationModalStartDate).Equals(strtDate))
                        {
                            logger.LogCheckPoint(String.Format(LogMessage.StartDateOnCyclicReportConfirmationModalPassed, strtDate.ToString("MM/dd/yyyy")));
                        }
                        else
                        {
                            logger.Error(String.Format(LogMessage.StartDateOnCyclicReportConfirmationModalFailed, strtDate.ToString("MM/dd/yyyy")));
                        }

                        // Click on "Schedule Report" button
                        UserSetFunctions.Click(ConfirmationModalCreateReportButton());
                        cyclicReportMSg = ReportCreatedSuccessMsg().Text;

                        // Verify the success message
                        if (cyclicReportMSg.Equals(Const.CyclicTradeReportMsg))
                        {
                            logger.LogCheckPoint(String.Format(LogMessage.SuccessMsgPassed, ConfirmationCyclicReportModalValue));
                        }
                        else
                        {
                            logger.Error(String.Format(LogMessage.SuccessMsgFailed, ConfirmationCyclicReportModalValue));
                        }

                        cyclicReportSummaryTextValue = CyclicReportSummaryText().Text;
                        cyclicReportFrequencyTextValue = CyclicReportFrequencyText().Text;
                        cyclicReportCreatedDateTimeTextValue = CyclicReportCreatedDateTimeText().Text;
                        cyclicReportActionTextValue = CyclicReportActionText().Text;

                        // Verify Report is available under "Cyclic reports" section, Report Summary is "Report Type"
                        // Verify the frequency and created date
                        // Verify delete button is available
                        VerifyTopMostOfCyclicTradeReports(cyclicReportSummaryTextValue, cyclicReportFrequencyTextValue, cyclicReportCreatedDateTimeTextValue, cyclicReportActionTextValue);

                        // Click on "Cancel Report" button
                        UserSetFunctions.Click(CancelReport());

                        noReportMsg = NoReportIsAvailableText().Text;

                        // Verify that the report is removed from "Cyclic reports" section
                        if (noReportMsg.Equals(Const.noReportAvailableMsg))
                        {
                            logger.LogCheckPoint(String.Format(LogMessage.VerifyNoReportAvailablePassed, noReportMsg));
                        }
                        else
                        {
                            logger.Error(String.Format(LogMessage.VerifyNoReportAvailableFailed, noReportMsg));
                        }
                    }

                    //Need to work for cyclic report and wait for an hour till the report is present in Report available window section
                    reportSummaryTextValue = ReportSummaryText().Text;
                    createdDateTimeTextValue = ReportCreatedDateTimeText().Text;
                    downloadReportBtnTextValue = DownloadReport().Text;
                    VerifyDownloadableTradeReports(reportSummaryTextValue, createdDateTimeTextValue, downloadReportBtnTextValue);
                    return true;
                }
                return flag;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
