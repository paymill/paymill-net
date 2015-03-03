ClientService clientService = paymillContext.ClientService;
Client client = clientService.GetAsync("client_88a388d9dd48f86c3136").Result;
client.Description = "My Lovely Client";
clientService.UpdateAsync( client ).Result;
