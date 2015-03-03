TransactionService transactionService = paymillContext.TransactionService;
Transaction transaction = transactionService.GetAsync("tran_023d3b5769321c649435").Result;
transaction.Description = "My updated transaction description";
transactionService.UpdateAsync(transaction).Result;
