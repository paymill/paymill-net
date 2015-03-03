SubscriptionService subscriptionService = paymillContext.SubscriptionService;

Subscription updatedSubscription = subscriptionService.PauseAsync( "sub_dea86e5c65b2087202e3" ).Result;
