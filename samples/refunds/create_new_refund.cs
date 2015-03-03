TransactionService transactionService = paymillContext.TransactionService;
Transaction transaction = transactionService.CreateWithTokenAsync(
    "098f6bcd4621d373cade4e832627b4f6",
    4200,
    "EUR",
    "For refund"
).Result;
RefundService refundService = paymillContext.RefundService;
Refund refund = refundService.RefundTransactionAsync(
    transaction,
    4200,
    "Sample Description"
).Result;
