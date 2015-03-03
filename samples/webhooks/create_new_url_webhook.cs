WebhookService webhookService = paymillContext.WebhookService;
EventType[] eventTypes = new EventType[] {
    EventType.TRANSACTION_SUCCEEDED,
    EventType.TRANSACTION_FAILED
}.Result;
Webhook webhook = webhookService.CreateUrlWebhookAsync(
    "<your-webhook-url>",
    eventTypes
).Result;
