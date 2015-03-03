PaymentService paymentService = paymillContext.PaymentService;
Payment payment = paymentService.CreateWithTokenAndClientAsync(
    "098f6bcd4621d373cade4e832627b4f6",
    "client_88a388d9dd48f86c3136"
).Result;
