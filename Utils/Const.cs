    namespace AlphaPoint_QA.Utils
{
    public static class Const
    {
        /// <summary>
        /// Config Files here
        /// </summary>

        public const string ConfigFileName = "appsettings.json";

        public const string TestDataFileName = "sharedsettings.json";

        public const string LogFileName = "log4net.config";

        /// <summary>
        /// Users defined here.
        /// </summary>

        //dhirender
        public const string USER1 = "User1";

        //magic1
        public const string USER2 = "User2";

        //magic2
        public const string USER3 = "User3";

        //magic3
        public const string USER4 = "User4";

        //User_1
        public const string USER5 = "User5";

        //User_2
        public const string USER6 = "User6";

        //magic4
        public const string USER7 = "User7";

        //User_3
        public const string USER8 = "User8";

        //User_4
        public const string USER9 = "User9";

        //User_5
        public const string USER10 = "User10";

        //User_6
        public const string USER11 = "User11";

        //User_7
        public const string USER12 = "User12";

        //User_8
        public const string USER13 = "User13";

        //Test_1
        public const string USER14 = "User14";

        //Test_2
        public const string USER15 = "User15";

        //AdminTestTIC0
        public const string USER16 = "User16";

        //UserAccount
        public const string USER17 = "User17";

        //Admin-dhirender
        public const string ADMIN1 = "Admin";

        public const string Headless = "headless";

        public const string FireFoxHeadless = "-headless";

        public const string LinuxHeadless = "--headless";

        //Web drivers path

        //Linux
        public const string ChromeLinuxPath = @"Drivers\Linux\Chrome\";
        public const string FirefoxLinuxPath = @"Drivers\Linux\Firefox\";
        public const string IELinuxPath = @"Drivers\Linux\IE\";

        //Windows
        public const string ChromeWindowPath = @"Drivers\Windows\Chrome\";
        public const string FirefoxWindowPath = @"Drivers\Windows\Firefox\";
        public const string IEWindowPath = @"Drivers\Windows\IE\";


        /// <summary>
        /// User Settings page constants
        /// </summary>

        public const string LoginText = "login";

        public const string APIPermissions = "Permissions";

        public const string APIKey = "Key";

        public const string APISecret = "Secret";

        public const string APIDeleteButton = "Delete";

        public const string APITradingPermission = "Trading";

        public const string APIDepositsPermission = "Deposits";

        public const string APIWithdrawlsPermission = "Withdrawls";

        public const string StopMarket = "StopMarket";

        public const string StopLimit = "StopLimit";

        public const string TrailingStopLimit = "TrailingStopLimit";

        public const string Limit = "Limit";

        public const string Market = "Market";

        public const string TrailingStopMarket = "TrailingStopMarket";

        public const string CancelledStatus = "Canceled";

        public const string LoyaltyToken = "LTC";

        public const string MarketPrice = "Market Price";

        public const string Fees = "Fees";

        public const string OrderTotal = "Order Total";

        public const string Net = "Net";

        public const string Exchange = "exchange";

        public const string EmailDomain = "@mailinator.com";

        public const string Dash = "-";

        public const string ZeroValue = "0.00000000";

        public const string NotAllowedCursorValue = "not-allowed";

        public const string InstrumentMsg = "BTCUSD";  
        
        public const string LockedInChecked = "Yes";

        public const string LockedInUnChecked = "No";

        public const string ReportBlockTradeMsg = "Report Block Trade";

        public const string ProductBoughtTextMsg = "Product Bought:";

        public const string CounterPartyIDTextMsg = "Counterparty:";

        public const string ProductSoldTextMsg = "Product Sold:";

        public const string FeeTextMsg = "Fees:";

        public const string ProductBoughtTextFieldAttributeValue = "value";

        public const string ProductSoldTextFieldAttributeValue = "value";

        public static string TransferApproved = "Transfer Approved";

        public const string RandomChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public const string ScreenshotUnderScore = "_";

        public const string ScreenshotImageFormat = ".png";

        public const string AttributeValue = "type";
        public const string CheckBoxTextValue = "checkbox";

        public const string AddWhiteSpace = " ";
        public const string RemoveWhiteSpace = "";
        public const string AddDollarSign = "$";

        public const string OrSign = " || ";
        public const string SemicolnSign = ":";

        public const string verifiedPassedMsg = "✓ Verified";
        public const string verifiedFailedMsg = "✕ Not verified";


        /// <summary>
        /// Assertion statements defined here. (Success MSG)
        /// </summary>
        public const string WithdrawSuccessMsg = "Your withdraw has been successfully added";

        public const string TransferSuccessMsg = "Transfer succeeded";

        public const string RequestTransferSuccessMsg = "Request transfer succeeded";
        
        public const string CopyAddressSuccessMsg = "The address has been copied to the clipboard.";

        public const string OrderSuccessMsg = "Your order has been successfully added";

        public const string OrderCancelledMsg = "Your Order has been Canceled";

        public const string AffiliateProgramSuccessMsg = "Affiliate Program verified successfully";

        public const string AffiliateProgramFailureMsg = "Affiliate Program verification failed";
      
        public const string APIKeyCreationFailureMsg = "Unable to create API Key";

        public const string APIKeyCreationSuccessMsg = "API Key Created successfully";

        public const string ConfirmationModalFailureMsg = "Confirmation Modal is not displayed";
       
        public const string ClickedOnAPIKeyButton = "Clicked on API Key Button successfully";

        public const string APIKeyButtonDisabled = "Unable to click on API Key Button";

        public const string SelectedAPIKeyCheckboxes = "Checkboxes[Trading, deposits and withdrawls] are selected";

        public const string APIKeyConfirmationModalIsPresent = "Confirmation Modal is displayed with values []";

        public const string APIKeyCheckboxesAreNotPresent = "Checkboxes[Trading, deposits and withdraws] are not present";

        public const string APIKeyAddedIsPresentInTheList = "API key added is present in the Existing API List";

        public const string VerifiedSelectedPermissions = "Successfully verified the permissions added to the API Key";

        public const string APIKeyAddedIsNotPresentInTheList = "API key added is not present in the Existing API List";

        public const string SecretKeyVerificationFailed = "Verification failed: Secret Key is displayed in the API Keys List";

        public const string SecretKeyVerificationPassed = "Verification passed: Secret Key is not displayed in the API Keys List";        

        public const string DeleteAPIKeyIsPresent = "Verification Passed: Delete button is present";

        public const string DeleteAPIKeyIsNotPresent = "Verification Passed: Delete button is present"; 

        public const string IOCOrderTypeSuccessMsg = "Verfiy Place By Order with Immediate Or Cancel Order Type passed successfully";

        public const string LimitBuyOrderFailureMsg = "Limit buy order Failed";

        public const string MarketSellOrderFailureMsg = "Market Sell Order Failed";

        /// <summary>
        /// Assertion statements defined here. (Failed MSG)
        /// </summary>

        public const string AskPrice = "Ask Price";        

        public const string MarketOrderVerifiedInFilledOrders = "Market Order successfully verified in Filled Orders tab";

        public const string StatusIdNotFound = "Status Id Not Found in Ticket-> Withdraw Page.";

        /// <summary>
        ///Admin Constants
        /// </summary>
        public const string AdminLoginTitle = "AlphaPoint | Admin";
        public const string CreateAffiliateTagBtn = "CREATE";
        public const int RandomStringLength = 4;
        public const string SuccessBuySellOrderMsg = "Your order was filled successfully!";
        public const string SingleTradeReportMsg = "Single report was successfully created";
        public const string CyclicTradeReportMsg = "Cyclical report was successfully created";
        public const string noReportAvailableMsg = "No Report Is Available";
        public const string PermissionSuccessMsg = "User permission added successfully";
        public const string InvalidAccountIDErrorMsg = "sThe entered account does not exist. Please try again.";
        public const string LoginErrroMsg = "Invalid username or password";
        public const string UserIdTextValue = "UserId";
        public const string UsernameTextValue = "Username";
        public const string EmailTextValue = "EmailFilter";
        public const string AccountIdTextValue = "AccountId";
        public const string Id = "id";
        public const string Value = "value";
        public const string AddUserKeySuccessMessage = "User API key added successfully";
        public const string DeleteUserKeySuccessMessage = "User API key removed successfully";
        public const string CopyUserKeySuccessMessage = "Copied to clipboard";

        public const string TCA4_WithdrawTicketSuccessfullyMSG= "Withdraw ticket created successfully";
        public const string TCAdmin22_AllAccountBalancesDownloadSuccessfullyMSG = "All accounts balances export complete";
        public const string RejectReasonTitle = "REJECT REASON"; 
        public const string UserId = "UserId";        
        public const string UserName = "UserName";
        public const string EmailVerified = "EmailVerified";
        public const string Email = "Email";
        public const string AccountId = "AccountId";
        public const string DateTimeCreated = "DateTimeCreated";
        public const string AffiliateId = "AffiliateId";
        public const string RefererId = "RefererId";
        public const string OMSId = "OMSId";
        public const string Use2FA = "Use2FA";
        public const string LoginId = "LoginId";
        public const string Permissions = "Permissions";


        public const string idValueTextKey = "Trade id";
        public const string pairValueTextKey = "Product pair code";
        public const string priceTextKey = "Price";
        public const string sizeValueTextKey = "Qantity";
        public const string executionIdValueTextKey = "Execution id";
        public const string sideValueTextKey = "Side";
        public const string LedgerEntrySuccessMsg = "Ledger entry submitted successfully";
		public const string LedgerEntryInvalidRequestMsg = "Invalid Request";
        public const string DepositKeySuccessMsg = "New deposit key created";

        //Accounts labels
        public const string accountIDLabelText = "ACCOUNT ID";
        public const string accountNameLabelText = "ACCOUNT NAME";
        public const string accountTypeLabelText = "ACCOUNT TYPE";
        public const string riskTypeLabelText = "RISK TYPE";
        public const string verificationLevelLabelText = "VERIFICATION LEVEL";
        public const string feeProductLabelText = "FEE PRODUCT";
        public const string feeProductTypeLabelText = "FEE PRODUCT TYPE";
        public const string accountBadgeLabelText = "ACCOUNT BADGES";

        public const string productLabelText = "PRODUCT";
        public const string amountLabelText = "AMOUNT";
        public const string holdAmountLabelText = "HOLD AMOUNT";
        public const string pendingDepositLabelText = "PENDING DEPOSITS";
        public const string pendingWithdrawLabelText = "PENDING WITHDRAWS";
        public const string dailyDepositsLabelText = "DAILY DEPOSITS";
        public const string dailyWithdrawLabelText = "DAILY WITHDRAWS";
        public const string monthlyWithdrawLabelText = "MONTHLY WITHDRAWS";

        public const string OMSIDText = "OMSID";
        public const string AccountIdText = "AccountId";
        public const string AccountNameText = "AccountName";
        public const string AccountHandleText = "AccountHandle";
        public const string FirmIdText = "FirmId";
        public const string FirmNameText = "FirmName";
        public const string AccountTypeText = "AccountType";
        public const string FeeGroupIDText = "FeeGroupID";
        public const string ParentIDText = "ParentID";
        public const string RiskTypeText = "RiskType";
        public const string VerificationLevelText = "VerificationLevel";
        public const string FeeProductTypeText = "FeeProductType";
        public const string FeeProductText = "FeeProduct";
        public const string RefererIdText = "RefererId";
        public const string LoyaltyProductIdText = "LoyaltyProductId";
        public const string LoyaltyEnabledText = "LoyaltyEnabled";
        public const string SupportedVenueIdsText = "SupportedVenueIds";


        public const string OMSIdText = "OMSId";
        public const string ProductSymbolText = "ProductSymbol";
        public const string ProductIdText = "ProductId";
        public const string AmountText = "Amount";
        public const string HoldText = "Hold";
        public const string PendingDepositsText = "PendingDeposits";
        public const string PendingWithdrawsText = "PendingWithdraws";
        public const string TotalDayDepositsText = "TotalDayDeposits";
        public const string TotalDayWithdrawsText = "TotalDayWithdraws";
        public const string TotalMonthWithdrawsText = "TotalMonthWithdraws";
        public const string TotalYearWithdrawsText = "TotalYearWithdraws";
    }
}
