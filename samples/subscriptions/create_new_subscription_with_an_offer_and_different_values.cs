SubscriptionService subscriptionService = paymillContext.SubscriptionService;

subscriptionService.CreateAsync( Subscription.Create(
    "pay_95ba26ba2c613ebb0ca8",
    "offer_40237e20a7d5a231d99b" )
.WithAmount( 3000 )
.WithCurrency( "EUR" )
.WithInterval( Interval.periodWithChargeDay( 1, Interval.TypeUnit.WEEK, Interval.Weekday.MONDAY ) )
.WithPeriodOfValidity( Interval.period( 2, Interval.TypeUnit.YEAR ) )
.withStartDate( DateTime.Now.AddDays(5) )
);
