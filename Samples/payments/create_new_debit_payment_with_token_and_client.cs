PaymentService paymentService = paymillContext.PaymentService;
Payment payment = paymentService.CreateWithTokenAndClientAsync(
    "12a46bcd462sd3r3care4e8336ssb4f5",
    "client_88a388d9dd48f86c3136"
).Result;
