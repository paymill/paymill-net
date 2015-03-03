SubscriptionService subscriptionService = paymillContext.SubscriptionService;
PaymillList<Subscription> subscriptions = subscriptionService.ListAsync().Result;
