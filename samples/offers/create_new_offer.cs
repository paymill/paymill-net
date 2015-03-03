OfferService offerService = paymillContext.OfferService;

Offer offer = offerService.CreateAsync(
    4200,
    "EUR",
    Interval.period(1, Interval.TypeUnit.WEEK),
    "Nerd Special",
    0
).Result;
