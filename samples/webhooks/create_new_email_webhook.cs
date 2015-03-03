WebhookService webhookService = paymillContext.WebhookService;
EventType[] eventTypes = new EventType[] {
    EventType.TRANSACTION_SUCCEEDED,
    EventType.TRANSACTION_FAILED
};
Webhook webhook = webhookService.CreateEmailWebhookAsync(
    "<your-webhook-email>",
    eventTypes
).Result;
