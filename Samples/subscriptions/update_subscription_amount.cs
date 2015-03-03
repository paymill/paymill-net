SubscriptionService subscriptionService = paymillContext.SubscriptionService;

Subscription updatedSubscription = subscriptionService.ChangeAmountTemporaryAsync( "sub_dea86e5c65b2087202e3" , 1234 ).Result;
