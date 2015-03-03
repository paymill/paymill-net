SubscriptionService subscriptionService = paymillContext.SubscriptionService;
Subscription subscription = subscriptionService.GetAsync("sub_dc180b755d10da324864").Result;
