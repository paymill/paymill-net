OfferService offerService = paymillContext.OfferService;

Offer offer = offerService.GetAsync( "offer_40237e20a7d5a231d99b" ).Result;

offer.Name = "Extended Special";
offer.Interval = Interval.period(1, Interval.TypeUnit.MONTH);
offer.Amount = 3333;
offer.Currency = "USD";
offer.TrialPeriodDays  = 33;

boolean updateSubscriptions = true;

Offer updatedOffer = offerService.UpdateAsync(offer, updateSubscriptions ).Result;
