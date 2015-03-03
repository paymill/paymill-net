OfferService offerService = paymillContext.OfferService;
boolean removeWithSubscriptions = true;
Boolean result = offerService.DeleteAsync(
    "offer_40237e20a7d5a231d99b",
    removeWithSubscriptions
).Result;
