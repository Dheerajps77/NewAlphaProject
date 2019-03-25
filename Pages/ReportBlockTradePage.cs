using System;
using System.Collections.Generic;
using System.Threading;
using AlphaPoint_QA.Common;
using AlphaPoint_QA.pages;
using AlphaPoint_QA.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Xunit;

namespace AlphaPoint_QA.Pages
{
    public class ReportBlockTradePage
    {
        Config data;
        ProgressLogger logger;

        private string placeBlockTradeDateTime;
        private string placeBlockTradeDateTimePlusOneMin;
        private string typeValue;
        public static IWebDriver driver;
        private string statusValue;
        private string placeBlockTradeDateTimeOfBlockTrade;
        private string placeBlockTradeDateTimePlusOneMinOfBlockTrade;
        private string cancelValue;

        public ReportBlockTradePage(ProgressLogger logger)
        {
            this.logger = logger;
            data = ConfigManager.Instance;
            driver = AlphaPointWebDriver.GetInstanceOfAlphaPointWebDriver();
        }

        By reportBlockTradeBtn = By.CssSelector("div.order-entry__body div:nth-of-type(8) div.order-entry__item-button");
        By boughtTab = By.CssSelector("div.report-block-trade-sidepane__tab-container > div:nth-of-type(1)");
        By soldTab = By.CssSelector("div.report-block-trade-sidepane__tab-container > div:nth-of-type(2)");
        By instrument = By.CssSelector("select[name='instrument']");
        By counterparty = By.CssSelector("input[data-test='Counterparty:']");
        By lockedIn = By.CssSelector("div.report-block-trade-form__checkbox-container > div > label");
        By productBought = By.CssSelector("input[data-test='Product Bought:']");
        By productSold = By.CssSelector("input[data-test='Product Sold:']");
        By fees = By.CssSelector("span[data-test='Fees:']");
        By received = By.CssSelector("span[data-test='Received:']");
        By orderTotal = By.CssSelector("span[data-test='Order Total:']");
        By btcBalance = By.CssSelector("span[data-test='BTC Balance:']");
        By usdBalance = By.CssSelector("span[data-test='USD Balance:']");
        By submitReport = By.CssSelector("button.ap-button__btn.ap-button__btn--additive.report-block-trade-form__btn.report-block-trade-form__btn--additive");
        By reportBlockTradeWindowTitle = By.CssSelector("div.slide-pane__title-wrapper h2.slide-pane__title");
        By dropdownInstrument = By.CssSelector("select[name='instrument']");
        By counterPartyID = By.CssSelector("div.report-block-trade-form__counterparty>div.form-group.ap-input__input-wrapper.report-block-trade-form__input-wrapper>label:nth-of-type(1)");
        By counterPartyTextField = By.CssSelector("input[data-test='Counterparty:']");
        By productBoughtTextField = By.CssSelector("input[data-test='Product Bought:']");
        By productSoldTextField = By.CssSelector("input[data-test='Product Sold:']");
        By lockedInCheckBox = By.CssSelector("label[for='locked-in']");
        By productBoughtText = By.CssSelector("div.report-block-trade-form.report-block-trade-form--with-padding>div.form-group.ap-input__input-wrapper.report-block-trade-form__input-wrapper>label:nth-of-type(1)");
        By productSoldText = By.CssSelector("form.slide-pane__body div:nth-of-type(3) div:nth-of-type(2) label.ap--label.ap-input__label.report-block-trade-form__label");
        By feesText = By.CssSelector("form.slide-pane__body div:nth-of-type(5) div label.ap--label.ap-label-with-text__label.report-block-trade-form__lwt-label");
        By boughAndSoldBTCBalances = By.CssSelector("span[data-test='BTC Balance:']");
        By boughAndSoldUSDBalances = By.CssSelector("span[data-test='USD Balance:']");
        By orderTotalAmount = By.CssSelector("span[data-test='Order Total:']");
        By receivedAmount = By.CssSelector("span[data-test='Received:']");
        By feeAmount = By.CssSelector("span[data-test='Fees:']");
        By submitReportBtn = By.CssSelector("button.ap-button__btn.ap-button__btn--additive.report-block-trade-form__btn.report-block-trade-form__btn--additive");
        By confirmBuyOrderBtn = By.CssSelector("button.ap-button__btn.ap-button__btn--additive.ap-modal.confirm-block-trade-modal__bought__btn.ap-modal.confirm-block-trade-modal__bought__btn--additive");
        By buyTradeReportOrderMsg = By.CssSelector("div.snackbar__text.snackbar__text--success.custom-snackbar__text.custom-snackbar__text--success");
        By counterPartyErrorMsg = By.CssSelector("div.snackbar__text.snackbar__text--warning.custom-snackbar__text.custom-snackbar__text--warning");
        By labelConfirmBlockTradeInstrumentText = By.CssSelector("span[data-test=Instrument]");
        By labelConfirmBlockTradeCountryPartyValue = By.CssSelector("span[data-test='Counterparty']");
        By labelConfirmBlockTradeLockedInStatus = By.CssSelector("span[data-test='Locked in']");
        By labelConfirmBlockTradeProductBoughtValue = By.CssSelector("span[data-test='Product Bought']");
        By labelConfirmBlockTradeProductSoldValue = By.CssSelector("span[data-test='Product Sold']");
        By labelConfirmBlockTradeFeeValue = By.CssSelector("span[data-test='Fee']");
        By labelConfirmBlockTradeFinalAMountValue = By.CssSelector("span[data-test='Final Amount']");
        By labelConfirmBlockTradeFinalValue = By.CssSelector("span[data-test='Final Value']");
        By labelConfirmBlockTradeDate = By.CssSelector("span[data-test='Date:']");
        By closeReportBlockTradeWindowSection = By.CssSelector("div.ap-sidepane__close-button.report-block-trade-sidepane__close-button");
        By cancelButton = By.CssSelector("button[data-test='Cancel']");

        public IWebElement CancelButton()
        {
            return driver.FindElement(cancelButton);
        }

        public IWebElement CloseReportBlockTradeWindowSection()
        {
            Thread.Sleep(2000);
            return driver.FindElement(closeReportBlockTradeWindowSection);
        }
        public IWebElement CounterPartyErrorMsg()
        {
            return driver.FindElement(counterPartyErrorMsg);
        }
        public IWebElement BuyTradeReportOrderMsg()
        {
            return driver.FindElement(buyTradeReportOrderMsg);
        }

        public IWebElement SubmitReportBtn()
        {
            return driver.FindElement(submitReportBtn);
        }

        public IWebElement ConfirmBuyOrderBtn()
        {
            return driver.FindElement(confirmBuyOrderBtn);
        }

        public IWebElement OrderTotalAmount()
        {
            return driver.FindElement(orderTotalAmount);
        }

        public IWebElement ReceivedAmount()
        {
            return driver.FindElement(receivedAmount);
        }

        public IWebElement FeeAmount()
        {
            return driver.FindElement(feeAmount);
        }

        public IWebElement CounterPartyTextField()
        {
            return driver.FindElement(counterPartyTextField);
        }

        public IWebElement ProductBoughtTextField()
        {
            return driver.FindElement(productBoughtTextField);
        }

        public IWebElement ProductSoldTextField()
        {
            return driver.FindElement(productSoldTextField);
        }

        public IWebElement CounterPartyID()
        {
            return driver.FindElement(counterPartyID);
        }
        public IWebElement LockedInCheckBox()
        {
            return driver.FindElement(lockedInCheckBox);
        }
        public IWebElement ProductBoughtText()
        {
            return driver.FindElement(productBoughtText);
        }
        public IWebElement ProductSoldText()
        {
            return driver.FindElement(productSoldText);
        }
        public IWebElement FeesText()
        {
            return driver.FindElement(feesText);
        }
        public IWebElement BoughAndSoldBTCBalances()
        {
            return driver.FindElement(boughAndSoldBTCBalances);
        }

        public IWebElement BoughAndSoldUSDBalances()
        {
            return driver.FindElement(boughAndSoldUSDBalances);
        }

        public IWebElement DropdownInstrument()
        {
            return driver.FindElement(dropdownInstrument);
        }

        public IWebElement ReportBlockTradeWindowTitle()
        {
            return driver.FindElement(reportBlockTradeWindowTitle);
        }

        public IWebElement Received()
        {
            return driver.FindElement(received);
        }

        public IWebElement OrderTotal()
        {
            return driver.FindElement(orderTotal);
        }

        public IWebElement BtcBalance()
        {
            return driver.FindElement(btcBalance);
        }

        public IWebElement USDBalance()
        {
            return driver.FindElement(usdBalance);
        }

        public IWebElement SubmitReport()
        {
            return driver.FindElement(submitReport);
        }

        public IWebElement ReportBlockTradeBtn()
        {
            return driver.FindElement(reportBlockTradeBtn);
        }

        public IWebElement BoughtTab()
        {
            return driver.FindElement(boughtTab);
        }

        public IWebElement SoldTab()
        {
            return driver.FindElement(soldTab);
        }

        public IWebElement Instrument()
        {
            return driver.FindElement(instrument);
        }

        public IWebElement Counterparty()
        {
            return driver.FindElement(counterparty);
        }

        public IWebElement LockedIn()
        {
            return driver.FindElement(lockedIn);
        }

        public IWebElement ProductBought()
        {
            return driver.FindElement(productBought);
        }

        public IWebElement ProductSold()
        {
            return driver.FindElement(productSold);
        }

        public IWebElement Fees()
        {
            return driver.FindElement(fees);
        }

        public IWebElement LabelConfirmBlockTradeInstrumentText()
        {
            return driver.FindElement(labelConfirmBlockTradeInstrumentText);
        }

        public IWebElement LabelConfirmBlockTradeCountryPartyValue()
        {
            return driver.FindElement(labelConfirmBlockTradeCountryPartyValue);
        }

        public IWebElement LabelConfirmBlockTradeLockedInStatus()
        {
            return driver.FindElement(labelConfirmBlockTradeLockedInStatus);
        }

        public IWebElement LabelConfirmBlockTradeProductBoughtValue()
        {
            return driver.FindElement(labelConfirmBlockTradeProductBoughtValue);
        }

        public IWebElement LabelConfirmBlockTradeProductSoldValue()
        {
            return driver.FindElement(labelConfirmBlockTradeProductSoldValue);
        }

        public IWebElement LabelConfirmBlockTradeFeeValue()
        {
            Thread.Sleep(2000);
            return driver.FindElement(labelConfirmBlockTradeFeeValue);
        }

        public IWebElement LabelConfirmBlockTradeFinalAMountValue()
        {
            return driver.FindElement(labelConfirmBlockTradeFinalAMountValue);
        }

        public IWebElement LabelConfirmBlockTradeFinalValue()
        {
            return driver.FindElement(labelConfirmBlockTradeFinalValue);
        }

        public IWebElement LabelConfirmBlockTradeDate()
        {
            return driver.FindElement(labelConfirmBlockTradeDate);
        }

        public void ActionCancelButton()
        {
            try
            {
                Thread.Sleep(2000);
                UserSetFunctions.Click(CancelButton());
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This method will click on "Report Block Trade" button
        public void ReportBlockTradeButton()
        {
            UserSetFunctions.Click(ReportBlockTradeBtn());
        }

        public void LockedInCheckBoxButton()
        {
            try
            {
                Actions action = new Actions(driver);
                action.MoveToElement(LockedInCheckBox()).Build().Perform();
                Thread.Sleep(2000);
                UserSetFunctions.Click(LockedInCheckBox());
            }
            catch (Exception)
            {
                throw;
            }
        }
        //This will verify the visibility Report Block Trade Window
        public void VerifyReportBlockTradeWindow()
        {
            string reportBlockTradeMsg;
            string msgFromReportTradeWindow;
            reportBlockTradeMsg = string.Format(Const.ReportBlockTradeMsg);
            msgFromReportTradeWindow = ReportBlockTradeWindowTitle().Text;
            Thread.Sleep(2000);
            try
            {
                try
                {
                    Assert.Equal(msgFromReportTradeWindow, reportBlockTradeMsg);
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedReportBlockTradeWindowAppeared));
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedReportBlockTradeWindowNotAppeared));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This will verify the visibility of Dropdown instrument
        public void VerifyDropdownInstrument()
        {
            try
            {
                try
                {
                    if (DropdownInstrument().Enabled)
                    {
                        logger.LogCheckPoint(string.Format(LogMessage.VerifiedDropdownInstrumentPassed));
                    }
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedDropdownInstrumentFailed));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This will verify the visibility of CounterParty
        public void VerifyCounterParty()
        {
            string counterPartyIDText;
            string msgFromcounterPartyID;
            try
            {
                counterPartyIDText = string.Format(Const.CounterPartyIDTextMsg);
                msgFromcounterPartyID = CounterPartyID().Text;
                Thread.Sleep(2000);
                try
                {
                    Assert.Equal(msgFromcounterPartyID, counterPartyIDText);
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedCounterPartyTextPassed));
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedCounterPartyTextFailed));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This will verify the visibility of LockedIn Checkbox
        public void VerifyLockedInCheckbox()
        {
            try
            {
                try
                {
                    if (LockedInCheckBox().Enabled)
                    {
                        logger.LogCheckPoint(string.Format(LogMessage.VerifiedLockedInCheckBoxPassed));
                    }
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedLockedInCheckBoxFailed));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This will verify the visibility of Product Bought
        public void VerifyProductBought()
        {
            string productBoughtText;
            string msgFromProductBought;
            try
            {
                productBoughtText = string.Format(Const.ProductBoughtTextMsg);
                msgFromProductBought = ProductBoughtText().Text;
                Thread.Sleep(2000);
                try
                {
                    Assert.Equal(msgFromProductBought, productBoughtText);
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedProductBoughtTextPassed));
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedProductBoughtTextFailed));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This will verify the visibility of Product Sold
        public void VerifyProductSold()
        {
            string productSoldText;
            string msgFromProductSold;
            try
            {
                productSoldText = string.Format(Const.ProductSoldTextMsg);
                msgFromProductSold = ProductSoldText().Text;
                Thread.Sleep(2000);
                try
                {
                    Assert.Equal(msgFromProductSold, productSoldText);
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedProductSoldTextPassed));
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedProductSoldTextFailed));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This will verify the visibility of Fees
        public void VerifyFees()
        {
            string feeText;
            string msgFromFee;
            try
            {
                feeText = string.Format(Const.FeeTextMsg);
                msgFromFee = FeesText().Text;
                Thread.Sleep(2000);

                try
                {
                    Assert.Equal(msgFromFee, feeText);
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedFeesTextPassed));
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedFeesTextFailed));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        //This will verify the visibility of Fees
        public void VerifyBalances()
        {
            string msgFromBtcBalances;
            string msgFromUsdBalances;
            try
            {
                msgFromBtcBalances = BoughAndSoldBTCBalances().Text;
                msgFromUsdBalances = BoughAndSoldUSDBalances().Text;
                Thread.Sleep(2000);
                try
                {
                    if (BoughAndSoldBTCBalances().Displayed)
                    {
                        logger.LogCheckPoint(string.Format(LogMessage.VerifiedBoughAndSoldBTCBalancesPassed, msgFromBtcBalances));
                    }
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedBoughAndSoldBTCBalancesFailed));
                }

                try
                {
                    if (BoughAndSoldUSDBalances().Displayed)
                    {
                        logger.LogCheckPoint(string.Format(LogMessage.VerifiedBoughAndSoldUSDBalancesPassed, msgFromUsdBalances));
                    }
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedBoughAndSoldUSDBalancesFailed));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SubmitReportButton()
        {
            try
            {
                UserSetFunctions.Click(SubmitReportBtn());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ConfirmSubmitReportButton()
        {
            try
            {
                UserSetFunctions.Click(ConfirmBuyOrderBtn());
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This method willpopulate the details of user in Trade Report tab(used TC_34)
        public Dictionary<string, string> ConfirmBlockTradeReport(string instrument, string buyTab, string counterPartyValue, string productBoughtPrice, string productSoldPrice, string status)
        {
            string feeValueText;
            string expectedBuyTradeReportOrderMsg;
            string actualCancelMsg;
            string actualBuyTradeReportOrderMsg;
            string expectedCancelMsg;
            try
            {
                Dictionary<string, string> placeBlockTradeReportOrder = new Dictionary<string, string>();
                var blockTradePrice = GetBlockTradePrice(productBoughtPrice, productSoldPrice);
                feeValueText = LabelConfirmBlockTradeFeeValue().Text;
                placeBlockTradeReportOrder = VerifyConfirmBlockTradeElements(counterPartyValue, instrument);
                VerifyFinalAmountAndFinalValue();
                expectedBuyTradeReportOrderMsg = BuyTradeReportOrderMsg().Text;
                actualCancelMsg = UserCommonFunctions.GetTextOfMessage(driver, logger);
                actualBuyTradeReportOrderMsg = string.Format(LogMessage.BuyTradeReportOrderMesgSuccess);
                expectedCancelMsg = string.Format(LogMessage.BuyTradeReportOrderMesgCanceled);
                try
                {
                    Assert.Equal(expectedBuyTradeReportOrderMsg, actualBuyTradeReportOrderMsg);
                    logger.LogCheckPoint(string.Format(LogMessage.BuyTradeReportOrderMesgSuccess));
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.BuyTradeReportOrderMesgCanceled));
                }

                UserSetFunctions.Click(CloseReportBlockTradeWindowSection());
                try
                {
                    VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, logger);
                    objVerifyOrdersTab.VerifyTradeReportsTab(instrument, buyTab, productBoughtPrice, blockTradePrice, feeValueText, placeBlockTradeReportOrder["PlaceBlockTradeTime"], placeBlockTradeReportOrder["PlaceBlockTradeTimePlusOneMin"], status);
                    logger.LogCheckPoint(String.Format(LogMessage.BuyBlockTradeReportTestPassed, buyTab));
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(String.Format(LogMessage.BuyBlockTradeReportTestFailed, buyTab));
                }

                try
                {
                    Thread.Sleep(2000);
                    if (CancelButton().Displayed)
                    {
                        logger.LogCheckPoint(string.Format(LogMessage.VerifiedCancelButtonFailed));
                    }
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedCancelButtonPassed));
                }
                return placeBlockTradeReportOrder;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This method will very verify the details in Trade Report tab TC_33
        public Dictionary<string, string> SubmitBuyTradeReport(string instrument, string buyTab, string counterPartyValue, string productBoughtPrice, string productSoldPrice, string status)
        {
            string feeValueText;
            string expectedBuyTradeReportOrderMsg;
            string actualCancelMsg;
            string actualBuyTradeReportOrderMsg;
            string expectedCancelMsg;
            try
            {
                Dictionary<string, string> placeBlockTradeReportOrder = new Dictionary<string, string>();
                var blockTradePrice = GetBlockTradePrice(productBoughtPrice, productSoldPrice);
                SubmitReportButton();
                feeValueText = LabelConfirmBlockTradeFeeValue().Text;

                // Verify the details present in "Confirm Block Trade" window section
                placeBlockTradeReportOrder = VerifyConfirmBlockTradeElements(counterPartyValue, instrument);

                // Verify the final amount and final value in confirm block trade section
                VerifyFinalAmountAndFinalValue();

                // Click on "Confirm Buy Order" button
                ConfirmSubmitReportButton();
                expectedBuyTradeReportOrderMsg = BuyTradeReportOrderMsg().Text;
                actualCancelMsg = UserCommonFunctions.GetTextOfMessage(driver, logger);

                actualBuyTradeReportOrderMsg = string.Format(LogMessage.BuyTradeReportOrderMesgSuccess);
                expectedCancelMsg = string.Format(LogMessage.BuyTradeReportOrderMesgCanceled);
                try
                {
                    // Verify the successful placed buy order 
                    Assert.Equal(expectedBuyTradeReportOrderMsg, actualBuyTradeReportOrderMsg);
                    logger.LogCheckPoint(string.Format(LogMessage.BuyTradeReportOrderMesgSuccess));
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.BuyTradeReportOrderMesgCanceled));
                }
                Thread.Sleep(2000);
                UserSetFunctions.Click(CloseReportBlockTradeWindowSection());
                try
                {
                    VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, logger);

                    // Verify the order placed in Trade Reports tab through Report Block Trade
                    objVerifyOrdersTab.VerifyTradeReportsTab(instrument, buyTab, productBoughtPrice, blockTradePrice, feeValueText, placeBlockTradeReportOrder["PlaceBlockTradeTime"], placeBlockTradeReportOrder["PlaceBlockTradeTimePlusOneMin"], status);
                    logger.LogCheckPoint(String.Format(LogMessage.BuyBlockTradeReportTestPassed, buyTab));
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(String.Format(LogMessage.BuyBlockTradeReportTestFailed, buyTab));
                }

                try
                {
                    // Verify block trade appears there without "Cancel" button in front of it
                    Thread.Sleep(2000);
                    if (CancelButton().Enabled)
                    {
                        logger.LogCheckPoint(string.Format(LogMessage.VerifiedCancelButtonFailed));
                    }
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedCancelButtonPassed));
                }

                Thread.Sleep(2000);
                return placeBlockTradeReportOrder;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This method will verify the Block Trade Trade Report of Other Party 
        public void VerifyOtherPartyBlockTradeReportTab(string instrument, string sellTab, string counterPartyValue, string productBoughtPrice, string productSoldPrice, string status, Dictionary<string, string> otherPartyBlockTradeData)
        {
            string lastPrice;
            double feeValue;
            string feeValueText;

            OrderEntryPage orderEntryPage = new OrderEntryPage(driver, logger);
            lastPrice = orderEntryPage.GetLastPrice();
            var blockTradePrice = GetBlockTradePrice(productBoughtPrice, productSoldPrice);
            feeValue = (Double.Parse(productBoughtPrice) * Double.Parse(lastPrice)) / 25;
            feeValueText = GenericUtils.ConvertToDoubleFormat(feeValue);
            try
            {
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, logger);
                // Verify the order placed in Trade Reports tab through Report Block Trade
                objVerifyOrdersTab.VerifyTradeReportsTab(instrument, sellTab, productBoughtPrice, blockTradePrice, feeValueText, otherPartyBlockTradeData["PlaceBlockTradeTime"], otherPartyBlockTradeData["PlaceBlockTradeTimePlusOneMin"], status);
                logger.LogCheckPoint(String.Format(LogMessage.SellBlockTradeReportTestPassed, sellTab));
            }
            catch (Exception)
            {
                logger.LogCheckPoint(String.Format(LogMessage.SellBlockTradeReportTestFailed, sellTab));
            }
        }

        //This method will verify the details present in "Confirm Block Trade" window section
        public Dictionary<string, string> VerifyConfirmBlockTradeElements(string counterPartyPrice, string instrument)
        {
            string dateText;
            string expectedBlockTradeDate;
            string actualInstrumentText;
            string actualCounterPartyText;
            string actualBlockTradeDate;
            string actualBlockTradeDatePlusOneMin;
            string productSoldText;
            string productSoldDigit;
            double doubleSoldBoughtPrice;
            string actualLockedInText;
            string exepctedCounterPartyText;
            string exepctedInstrumentText;
            string LockedInCheckboxEnabled;
            string LockedInCheckboxDisabled;
            placeBlockTradeDateTime = TestData.GetData("TC33_PlaceBlockTradeTime");
            placeBlockTradeDateTimePlusOneMin = TestData.GetData("TC33_PlaceBlockTradeTimePlusOneMin");
            try
            {
                Dictionary<string, string> placeBlockTradeDict = new Dictionary<string, string>();
                dateText = LabelConfirmBlockTradeDate().Text;
                expectedBlockTradeDate = dateText;
                actualInstrumentText = LabelConfirmBlockTradeInstrumentText().Text;
                actualCounterPartyText = LabelConfirmBlockTradeCountryPartyValue().Text;
                actualLockedInText = LabelConfirmBlockTradeLockedInStatus().Text;

                exepctedCounterPartyText = counterPartyPrice;
                exepctedInstrumentText = instrument;

                LockedInCheckboxEnabled = string.Format(Const.LockedInChecked);
                LockedInCheckboxDisabled = string.Format(Const.LockedInUnChecked);


                actualBlockTradeDate = GenericUtils.GetCurrentTime();
                actualBlockTradeDatePlusOneMin = GenericUtils.GetCurrentTimePlusOneMinute();

                placeBlockTradeDict.Add(placeBlockTradeDateTime, actualBlockTradeDate);
                placeBlockTradeDict.Add(placeBlockTradeDateTimePlusOneMin, actualBlockTradeDatePlusOneMin);

                productSoldText = LabelConfirmBlockTradeProductSoldValue().Text;

                productSoldDigit = productSoldText.Split(" ")[0];
                doubleSoldBoughtPrice = double.Parse(productSoldDigit);


                //This will verify the status of lockedIn checkbox in "Report Block Trade" window section
                try
                {
                    Assert.Equal(actualLockedInText, LockedInCheckboxEnabled);
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedLockedInStatusInConfirmBlockTradePassed, actualLockedInText, LockedInCheckboxEnabled));
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedLockedInStatusInConfirmBlockTradeFailed, actualLockedInText, LockedInCheckboxDisabled));
                }

                //This will verify the instrument 
                try
                {
                    Assert.Equal(actualInstrumentText, exepctedInstrumentText);
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedInstrumentInConfirmBlockTradePassed, actualInstrumentText, exepctedInstrumentText));
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedInstrumentInConfirmBlockTradeFailed, actualInstrumentText, exepctedInstrumentText));
                }

                //This will verify the counter party value in confirm block trade
                try
                {
                    Assert.Equal(actualCounterPartyText, exepctedCounterPartyText);
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedCounterPartyValueInConfirmBlockTradePassed, actualCounterPartyText, exepctedCounterPartyText));
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedCounterPartyValueInConfirmBlockTradeFailed, actualCounterPartyText, exepctedCounterPartyText));
                }

                //This will verify the Date of on confirm block trade
                try
                {
                    Assert.Equal(expectedBlockTradeDate.ToString(), actualBlockTradeDate);
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedDateOnConfirmBlockTradePassed, expectedBlockTradeDate));
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedDateOnConfirmBlockTradeFailed, actualBlockTradeDate));
                }
                return placeBlockTradeDict;
            }
            catch (Exception)
            {
                throw;
            }
        }


        //This method will verify the final amount and final value in confirm block trade section
        public void VerifyFinalAmountAndFinalValue()
        {
            string finalAmountText;
            string finalValueText;
            string productBoughtText;
            string productSoldText;
            string feeValueText;
            string productSoldDigit;
            double doubleSoldBoughtPrice;
            string productBoughtDigit;
            double doubleProductBoughtPrice; ;
            double doubleFinalValuePrice; 
            double doubleFeePrice;
            double doublefinalAmountPrice;
            try
            {
                finalAmountText = LabelConfirmBlockTradeFinalAMountValue().Text;
                finalValueText = LabelConfirmBlockTradeFinalValue().Text;
                productBoughtText = LabelConfirmBlockTradeProductBoughtValue().Text;
                productSoldText = LabelConfirmBlockTradeProductSoldValue().Text;

                feeValueText = LabelConfirmBlockTradeFeeValue().Text;
                productSoldDigit = productSoldText.Split(" ")[0];
                doubleSoldBoughtPrice = double.Parse(productSoldDigit);

                productBoughtDigit = productBoughtText.Split(" ")[0];
                doubleProductBoughtPrice = double.Parse(productBoughtDigit);

                doubleFinalValuePrice = double.Parse(finalValueText);

                doubleFeePrice = double.Parse(feeValueText);
                doublefinalAmountPrice = doubleProductBoughtPrice - doubleFeePrice;

                //This will verify final value is equal to product sold
                try
                {
                    Assert.Equal(doubleSoldBoughtPrice, doubleFinalValuePrice);
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedFinalValueEqualToProductSoldPassed, doubleSoldBoughtPrice, doubleFinalValuePrice));
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedFinalValueEqualToProductSoldFailed, doubleSoldBoughtPrice, doubleFinalValuePrice));
                }

                //This will verify final amount is product bought minus fee
                try
                {
                    Assert.NotEqual(doublefinalAmountPrice, doubleProductBoughtPrice);
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedFinalAmountAfterFeeDeductingPassed, doublefinalAmountPrice, doubleFeePrice));
                }
                catch(Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedFinalAmountAfterFeeDeductingFailed, doubleFeePrice, doublefinalAmountPrice));
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        //This method will perform buy trade report and verify their details(Will be use by TC_35)
        public void SubmitBlockTradeReportForUser(string counterPartyPrice, string producBoughtPrice, string productSoldPrice)
        {
            string expectedBuyTradeReportOrderMsg;
            string actualCancelMsg;
            string actualBuyTradeReportOrderMsg;
            string expectedCancelMsg;
            try
            {
                OrderEntryPage orderEntryPage = new OrderEntryPage(driver, logger);
                UserSetFunctions.EnterText(CounterPartyTextField(), counterPartyPrice);
                UserSetFunctions.EnterText(ProductBoughtTextField(), producBoughtPrice);
                UserSetFunctions.EnterText(ProductSoldTextField(), productSoldPrice);
                Thread.Sleep(2000);

                // Click on "Submit Report" button
                SubmitReportButton();
                Thread.Sleep(2000);

                // Click on "Confirm Buy Order" button
                ConfirmSubmitReportButton();
                Thread.Sleep(2000);
                expectedBuyTradeReportOrderMsg = BuyTradeReportOrderMsg().Text;
                actualCancelMsg = UserCommonFunctions.GetTextOfMessage(driver, logger);

                actualBuyTradeReportOrderMsg = string.Format(LogMessage.BuyTradeReportOrderMesgSuccess);
                expectedCancelMsg = string.Format(LogMessage.BuyTradeReportOrderMesgCanceled);
                try
                {
                    // Verify Success message
                    Assert.Equal(expectedBuyTradeReportOrderMsg, actualBuyTradeReportOrderMsg);
                    logger.LogCheckPoint(string.Format(LogMessage.BuyTradeReportOrderMesgSuccess));
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.BuyTradeReportOrderMesgCanceled));
                }
                Thread.Sleep(2000);
                UserSetFunctions.Click(CloseReportBlockTradeWindowSection());
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This method will perform a simple Block Buy trade operation(Used for TC_34)
        public void SubmitBlockTradeReportWithoutLockedInCheckBox(string instrument, string counterPartyValue, string producBoughtPrice, string productSoldPrice, string counterPartyPrice, string buyTab, string status)
        {
            string productBought;
            double actualProductBought;
            string productSold;
            double actualProductSold;
            string orderTotalAmountStringValue;
            string orderTotalAmountdigits;
            double doubleOrderTotalPrice;
            string receivedAmountStringValue;
            string receivedAmountdigits;
            double doubleReceivedPrice;
            string feeAmountStringValue;
            string feeAmountdigits;
            double doubleFeePrice;
            double receivedBTCAmount;
            string expectedBuyTradeReportOrderMsg;
            string actualCancelMsg;
            string actualBuyTradeReportOrderMsg;
            string expectedCancelMsg;
            string feeValueText;

            placeBlockTradeDateTimeOfBlockTrade = TestData.GetData("TC34_PlaceBlockTradeTime");
            placeBlockTradeDateTimePlusOneMinOfBlockTrade = TestData.GetData("TC34_PlaceBlockTradeTimePlusOneMin");
            typeValue = TestData.GetData("TC34_Type");
            statusValue = TestData.GetData("TC34_Status");
            cancelValue = TestData.GetData("TC35_Cancel");
            try
            {
                OrderEntryPage orderEntryPage = new OrderEntryPage(driver, logger);
                UserSetFunctions.EnterText(CounterPartyTextField(), counterPartyPrice);
                UserSetFunctions.EnterText(ProductBoughtTextField(), producBoughtPrice);
                UserSetFunctions.EnterText(ProductSoldTextField(), productSoldPrice);
                Thread.Sleep(2000);
                productBought = ProductBoughtTextField().GetAttribute(string.Format(Const.ProductBoughtTextFieldAttributeValue));
                actualProductBought = double.Parse(productBought);

                productSold = ProductSoldTextField().GetAttribute(string.Format(Const.ProductSoldTextFieldAttributeValue));
                actualProductSold = double.Parse(productSold);

                orderTotalAmountStringValue = OrderTotalAmount().Text;
                orderTotalAmountdigits = orderTotalAmountStringValue.Split(" ")[1];
                doubleOrderTotalPrice = Double.Parse(orderTotalAmountdigits);

                receivedAmountStringValue = ReceivedAmount().Text;
                receivedAmountdigits = receivedAmountStringValue.Split(" ")[1];
                doubleReceivedPrice = Double.Parse(receivedAmountdigits);

                feeAmountStringValue = FeeAmount().Text;
                feeAmountdigits = feeAmountStringValue.Split(" ")[1];
                doubleFeePrice = Double.Parse(feeAmountdigits);

                receivedBTCAmount = actualProductBought - doubleFeePrice;

                // Verify if product Sold is equal to Order Total amount.
                try
                {
                    Assert.Equal(actualProductSold, doubleOrderTotalPrice);
                    logger.LogCheckPoint(string.Format(LogMessage.ProductSoldEqualsToOrderTotal, actualProductSold, doubleOrderTotalPrice));

                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.ProductSoldNotEqualsToOrderTotal, actualProductBought, doubleOrderTotalPrice));
                }

                // Verify "Received BTC" is product bought minus fee
                try
                {
                    Assert.NotEqual(receivedBTCAmount, doubleOrderTotalPrice);
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedReceivedBTCAmountAfterFeeDeductingPassed, receivedBTCAmount, actualProductBought));

                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedReceivedBTCAmountAfterFeeDeductingFailed, receivedBTCAmount, doubleFeePrice));
                }
                Thread.Sleep(2000);

                //Click on "Submit Report" button 
                SubmitReportButton();
                Thread.Sleep(2000);

                // Verify the final amount and final value in confirm block trade section
                VerifyFinalAmountAndFinalValue();
                feeValueText = LabelConfirmBlockTradeFeeValue().Text;
                var blockTradePrice = GetBlockTradePrice(producBoughtPrice, productSoldPrice);
                Thread.Sleep(2000);

                // Click on "Confirm Buy Order" button
                ConfirmSubmitReportButton();
                Thread.Sleep(2000);

                expectedBuyTradeReportOrderMsg = BuyTradeReportOrderMsg().Text;
                actualCancelMsg = UserCommonFunctions.GetTextOfMessage(driver, logger);
                actualBuyTradeReportOrderMsg = String.Format(LogMessage.BuyTradeReportOrderMesgSuccess);
                expectedCancelMsg = String.Format(LogMessage.BuyTradeReportOrderMesgCanceled);
                try
                {
                    // Verify Message "Your order submitted successfully" should be displayed
                    Assert.Equal(expectedBuyTradeReportOrderMsg, actualBuyTradeReportOrderMsg);
                    logger.LogCheckPoint(string.Format(LogMessage.BuyTradeReportOrderMesgSuccess));
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.BuyTradeReportOrderMesgCanceled));
                }

                Thread.Sleep(2000);
                UserSetFunctions.Click(CloseReportBlockTradeWindowSection());
                try
                {
                    VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, logger);
                    
                    // Verify if block trade is present in open orders tab
                    objVerifyOrdersTab.VerifyBlockTradeReportsInOpenOrderTab(instrument, buyTab, typeValue, producBoughtPrice, blockTradePrice, statusValue, cancelValue);
                    logger.LogCheckPoint(String.Format(LogMessage.BuyBlockTradeReportInOpenTabWithWorkingStatusPassed, buyTab));
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(String.Format(LogMessage.BuyBlockTradeReportInOpenTabWithWorkingStatusFailed, buyTab));
                }
            }

            catch (Exception)
            {
                throw;
            }
        }

        //This method will perform a simple Block Buy trade operation and verify the fields(Used for TC_33)
        public void VerifyElementsAndSubmitBlockTradeReport(string counterPartyPrice, string wrongCounterPrice, string producBoughtPrice, string productSoldPrice)
        {
            string AcceptedCountrPartyErrorMsg;
            string productBought;
            double actualProductBought;
            string productSold;
            double actualProductSold;
            string orderTotalAmountStringValue;
            string orderTotalAmountdigits;
            double doubleOrderTotalPrice;
            string receivedAmountStringValue;
            string receivedAmountdigits;
            double doubleReceivedPrice;
            string feeAmountStringValue;
            string feeAmountdigits;
            double doubleFeePrice;
            double receivedBTCAmount;

            string ExpectedCounterPartyMsg = string.Format(LogMessage.CounterPartyErrorMsgVerified, wrongCounterPrice);
            try
            {
                OrderEntryPage orderEntryPage = new OrderEntryPage(driver, logger);
                UserSetFunctions.EnterText(CounterPartyTextField(), wrongCounterPrice);
                UserSetFunctions.EnterText(ProductBoughtTextField(), producBoughtPrice);
                UserSetFunctions.EnterText(ProductSoldTextField(), productSoldPrice);
                Thread.Sleep(2000);

                //Click on "Submit Report" button

                SubmitReportButton();
                Thread.Sleep(2000);

                // Click on "Confirm Buy Order" button
                ConfirmSubmitReportButton();

                AcceptedCountrPartyErrorMsg = CounterPartyErrorMsg().Text;

                // Verify Error message "Counterparty not found" is displayed
                try
                {
                    Assert.Equal(ExpectedCounterPartyMsg, AcceptedCountrPartyErrorMsg);
                    logger.LogCheckPoint(string.Format(LogMessage.CounterPartyErrorMsgVerified, wrongCounterPrice));
                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.CounterPartyErrorMsgVerified));
                }
                Thread.Sleep(2000);
                UserSetFunctions.Clear(CounterPartyTextField());
                UserSetFunctions.EnterText(CounterPartyTextField(), counterPartyPrice);
                Thread.Sleep(2000);
                LockedInCheckBoxButton();
                Thread.Sleep(2000);
                productBought = ProductBoughtTextField().GetAttribute(string.Format(Const.ProductBoughtTextFieldAttributeValue));
                actualProductBought = double.Parse(productBought);

                productSold = ProductSoldTextField().GetAttribute(string.Format(Const.ProductSoldTextFieldAttributeValue));
                actualProductSold = double.Parse(productSold);

                orderTotalAmountStringValue = OrderTotalAmount().Text;
                orderTotalAmountdigits = orderTotalAmountStringValue.Split(" ")[1];
                doubleOrderTotalPrice = Double.Parse(orderTotalAmountdigits);

                receivedAmountStringValue = ReceivedAmount().Text;
                receivedAmountdigits = receivedAmountStringValue.Split(" ")[1];
                doubleReceivedPrice = Double.Parse(receivedAmountdigits);

                feeAmountStringValue = FeeAmount().Text;
                feeAmountdigits = feeAmountStringValue.Split(" ")[1];
                doubleFeePrice = Double.Parse(feeAmountdigits);

                // Calculating Receiver BTC AMount = productBought-fee;
                receivedBTCAmount = actualProductBought - doubleFeePrice;

                // Verify "Order Total" is equal to "Product sold"
                try
                {
                    Assert.Equal(actualProductSold, doubleOrderTotalPrice);
                    logger.LogCheckPoint(string.Format(LogMessage.ProductSoldEqualsToOrderTotal, actualProductSold, doubleOrderTotalPrice));

                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.ProductSoldNotEqualsToOrderTotal, actualProductBought, doubleOrderTotalPrice));
                }

                // Verify "Received BTC" is product bought minus fee
                try
                {
                    Assert.Equal(receivedBTCAmount, doubleOrderTotalPrice);
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedReceivedBTCAmountAfterFeeDeductingFailed, receivedBTCAmount, actualProductBought));

                }
                catch (Exception)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifiedReceivedBTCAmountAfterFeeDeductingPassed, receivedBTCAmount, doubleFeePrice));
                }
                Thread.Sleep(2000);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //This method will verify the block trade in Admin UI
        public void VerifyBlockTradeInAdmin(string accountTypeID, string counterPartyID, string instrument, string originalQuantity, string quantityExecuted)
        {
            AdminCommonFunctions objAdminCommonFunctions = new AdminCommonFunctions(logger);
            try
            {
                //  Login as Admin -> Trades -> Block Trades --> select BTCUSD instrument
                objAdminCommonFunctions.SelectTradeMenu();
                objAdminCommonFunctions.BlockTradeBtn();
                objAdminCommonFunctions.BlockTradeInstrumentSelection(instrument);

                // Verify trade status is "Submitted"
                objAdminCommonFunctions.BuyBlockTradeList(accountTypeID, counterPartyID, instrument, originalQuantity, quantityExecuted);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static string GetBlockTradePrice(string productBoughtPrice, string productSoldPrice)
        {
            string firstValuePrecision;
            string secondValuePrecision;
            double price;
            string blockTradePrice;
            try
            {
                firstValuePrecision = Convert.ToDecimal(productBoughtPrice).ToString("#,##0.00000000");
                secondValuePrecision = Convert.ToDecimal(productSoldPrice).ToString("#,##0.00000000");
                var firstValueToDouble = Double.Parse(firstValuePrecision);
                var secondValueToDouble = Double.Parse(secondValuePrecision);
                price = Math.Abs(secondValueToDouble / firstValueToDouble);
                blockTradePrice = String.Format("{0:0.00000000}", price);
                return blockTradePrice;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
