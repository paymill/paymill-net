Offer offer = paymillContext.OfferService.GetAsync( "offer_d7e9813a25e89c5b78bd" ).Result;

SubscriptionService subscriptionService = paymillContext.SubscriptionService;
Subscription subscription = subscriptionService.GetAsync( "sub_dea86e5c65b2087202e3" ).Result;

Subscription updatedSubscription = subscriptionService.ChangeOfferChangeCaptureDateAndRefundAsync( subscription, offer ).Result;
