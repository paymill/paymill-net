TransactionService transactionService = paymillContext.TransactionService;
Transaction transaction = transactionService.CreateWithPaymentAndClientAsync(
    "pay_a818b847db6ce5ff636f",
    "client_c781b1d2f7f0f664b4d9",
    4200,
    "EUR"
).Result;
