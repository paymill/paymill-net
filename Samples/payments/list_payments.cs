PaymentService paymentService = paymillContext.PaymentService;
PaymillList<Payment> payments = paymentService.ListAsync().Result;
