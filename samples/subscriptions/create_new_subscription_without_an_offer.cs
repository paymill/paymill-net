SubscriptionService subscriptionService = paymillContext.SubscriptionService;

subscriptionService.CreateAsync( Subscription.Create(
    "pay_5e078197cde8a39e4908f8aa",
    3000,
    "EUR",
    Interval.periodWithChargeDay( 1, Interval.TypeUnit.WEEK, Interval.Weekday.MONDAY ) )
.WithName( "Example Subscription" )
.WithPeriodOfValidity( Interval.period( 2, Interval.TypeUnit.YEAR ) )
.WithStartDate(DateTime.Now.AddDays(5)
);
