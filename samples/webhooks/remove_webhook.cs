WebhookService webhookService = paymillContext.WebhookService;
Webhook webhook = webhookService.DeleteAsync("hook_40237e20a7d5a231d99b").Result;
