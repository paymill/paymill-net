SubscriptionService subscriptionService = paymillContext.SubscriptionService;

Subscription subscription = subscriptionService.GetAsync( "sub_dea86e5c65b2087202e3" ).Result;

subscription.Name = "Changed Subscription" ;
subscription.Offer = null; // Do not update Offer
subscription.Currency = null;// Do not update Currency
subscription.Interval = null;// Do not update Interval

Subscription updatedSubscription = subscriptionService.UpdateAsync(subscription).Result;
