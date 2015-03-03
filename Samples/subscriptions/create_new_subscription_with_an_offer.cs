SubscriptionService subscriptionService = paymillContext.SubscriptionService;

subscriptionService.CreateAsync( Subscription.Create(
    "pay_95ba26ba2c613ebb0ca8",
    "offer_40237e20a7d5a231d99b" )
.WithPeriodOfValidity( Interval.period( 2, Interval.TypeUnit.YEAR ) )
.WithStartDate( DateTime.Now.AddDays(5) )
);
