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
    public class TradeReportsTest : TestBase
    {

        public TradeReportsTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void TC47_VerifySingleReportTradeActivities()
        {
            string reportTypeValue; 
            string startDate;
            string endDate;
            reportTypeValue = TestData.GetData("TC47_SingleReportTradeActivityValue");
            TradeReportsPage objTradeReportsPage = new TradeReportsPage(driver, TestProgressLogger);
            try
            {                
                TestProgressLogger.StartTest();
                UserFunctions objUserFunctionality = new UserFunctions(TestProgressLogger);
                objUserFunctionality.LogIn(TestProgressLogger, Const.USER14);                                
                startDate = GenericUtils.GetCurrentDateMinusOne();
                endDate = GenericUtils.GetCurrentDate();

                //This will verify trade activities of single report and their details
                Assert.True(objTradeReportsPage.VerifySingleReportData(reportTypeValue, startDate, endDate));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifySingleReportTradeActivitiesPassed, reportTypeValue));
            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifySingleReportTradeActivitiesFailed, reportTypeValue), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifySingleReportTradeActivitiesFailed, reportTypeValue), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();               
            }
        }

        [Fact]
        public void TC48_VerifyCyclicReportTradeActivities()
        {
            string reportTypeValue = TestData.GetData("TC48_SingleReportTradeActivityValue");
            UserFunctions objUserFunctionality = new UserFunctions(TestProgressLogger);
            TradeReportsPage objTradeReportsPage = new TradeReportsPage(driver, TestProgressLogger);
            try
            {
                TestProgressLogger.StartTest();
                objUserFunctionality.LogIn(TestProgressLogger, Const.USER14);                
                string startDate = GenericUtils.GetCurrentDate();

                //This will verify trade activities of cyclic report and their details
                Assert.True(objTradeReportsPage.VerifyCyclicReportData(reportTypeValue, startDate));
                TestProgressLogger.LogCheckPoint(String.Format(LogMessage.VerifyCyclicReportTradeActivitiesPassed, reportTypeValue));

            }
            catch (NoSuchElementException ex)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(ex.Message + ex.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifyCyclicReportTradeActivitiesFailed, reportTypeValue), ex);
                throw ex;
            }
            catch (Exception e)
            {
                TestProgressLogger.TakeScreenshot();
                TestProgressLogger.LogCheckPoint(e.Message + e.StackTrace);
                TestProgressLogger.LogError(String.Format(LogMessage.VerifyCyclicReportTradeActivitiesFailed, reportTypeValue), e);
                throw e;
            }
            finally
            {
                TestProgressLogger.EndTest();               
            }
        }
    }
}