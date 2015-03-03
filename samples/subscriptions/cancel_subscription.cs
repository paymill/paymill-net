SubscriptionService subscriptionService = paymillContext.SubscriptionService;

Subscription updatedSubscription = subscriptionService.CancelAsync( "sub_dea86e5c65b2087202e3" ).Result;
