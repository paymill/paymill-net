Fee fee = new Fee();
fee.Amount =  420;
fee.Payment = "pay_3af44644dd6d25c820a8";
TransactionService transactionService = paymillContext.TransactionService;
Transaction transaction = transactionService.CreateWithTokenAndFeeAsync(
    "098f6bcd4621d373cade4e832627b4f6",
    4200,
    "EUR",
    fee
).Result;
