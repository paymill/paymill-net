namespace PaymillWrapper.Models
{
    public enum ResponseCode
    {
        /// <summary>
        /// General undefined response.
        /// </summary>
        UndefinedResponse = 10001,
        /// <summary>
        /// Waiting on something.
        /// </summary>
        Waiting = 10002,
        /// <summary>
        /// General success response.
        /// </summary>
        Success = 20000,
        /// <summary>
        /// General problem with data.
        /// </summary>
        GeneralProblem = 40000,
        /// <summary>
        /// General problem with payment data.
        /// </summary>
        GeneralProblemWithPaymentData = 40001,
        /// <summary>
        /// Problem with credit card data.
        /// </summary>
        ProblemWithCreditCardData = 40100,
        /// <summary>
        /// Problem with CVV.
        /// </summary>
        ProblemWithCVV = 40101,
        /// <summary>
        /// Card expired or not yet valid.
        /// </summary>
        CardExpiredOrNotYetValid = 40102,
        /// <summary>
        /// Card limit exceeded.
        /// </summary>
        LimitExceeded = 40103,
        /// <summary>
        /// Card invalid.
        /// </summary>
        CardInvalid = 40104,
        /// <summary>
        /// Expiry date not valid.
        /// </summary>
        ExpiryDateInvalid = 40105,
        /// <summary>
        /// Credit card brand required.
        /// </summary>
        BrandRequired = 40106,
        /// <summary>
        /// Problem with bank account data.
        /// </summary>
        ProblemWithAccountData = 40200,
        /// <summary>
        /// Bank account data combination mismatch.
        /// </summary>
        BankDataCombinationMismatch = 40201,
        /// <summary>
        /// User authentication failed.
        /// </summary>
        UserAuthenticationFailed = 402012,
        /// <summary>
        /// Problem with 3D Secure data.
        /// </summary>
        ProblemWith3DSecure = 40300,
        /// <summary>
        /// Currency / amount mismatch.
        /// </summary>
        CurrencyAmountMismatch = 40301,
        /// <summary>
        /// Problem with input data.
        /// </summary>
        ProblemWithInputData = 40400,
        /// <summary>
        /// Amount too low or zero.
        /// </summary>
        AmountTooLow = 40401,
        /// <summary>
        /// Usage field too long.
        /// </summary>
        UsageFieldTooLong = 40402,
        /// <summary>
        /// Currency not allowed.
        /// </summary>
        CurrencyNotAllowed = 40403,
        /// <summary>
        /// General problem with backend.
        /// </summary>
        GeneralProblemWithBackend = 50000,
        /// <summary>
        /// Country blacklisted.
        /// </summary>
        CountryBlacklisted = 50001,
        /// <summary>
        /// Technical error with credit card.
        /// </summary>
        TechnicalErrorWithCard = 50100,
        /// <summary>
        /// Error limit exceeded.
        /// </summary>
        ErrorLimitExceeded = 50101,
        /// <summary>
        /// Card declined by authorization system.
        /// </summary>
        CardDeclined = 50102,
        /// <summary>
        /// Manipulation or stolen card.
        /// </summary>
        StolenCard = 50103,
        /// <summary>
        /// Card restricted.
        /// </summary>
        CardRestricted = 50104,
        /// <summary>
        /// Invalid card configuration data.
        /// </summary>
        InvalidCardConfigurationData = 50105,
        /// <summary>
        /// Technical error with bank account.
        /// </summary>
        TechnicalErrorWithBankAccount = 50200,
        /// <summary>
        /// Card blacklisted.
        /// </summary>
        CardBlacklisted = 50201,
        /// <summary>
        /// Technical error with 3D Secure.
        /// </summary>
        TechnicalErrorWith3DSecure = 50300,
        /// <summary>
        /// Decline because of risk issues.
        /// </summary>
        DeclineBecauseOfRisk = 50400,
        /// <summary>
        /// General timeout.
        /// </summary>
        GeneralTimeout = 50500,
        /// <summary>
        /// Timeout on side of the acquirer.
        /// </summary>
        AcquirerTimeout = 50501,
        /// <summary>
        /// Risk management transaction timeout.
        /// </summary>
        RiskManagementTransactionTimeout = 50502,
        /// <summary>
        /// Duplicate transaction.
        /// </summary>
        DuplicateTransaction = 50600
    }
}
