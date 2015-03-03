PaymentService paymentService = paymillContext.PaymentService;
Payment payment = paymentService.CreateWithTokenAsync(
    "098f6bcd4621d373cade4e832627b4f6"
).Result;
TransactionService transactionService = paymillContext.TransactionService;
Transaction transaction = transactionService.CreateWithTokenAsync(
    payment,
    4200,
    "EUR",
    "Test Transaction"
).Result;
