OfferService offerService = paymillContext.OfferService;
PaymillList<Offer> offers = offerService.ListAsync().Result;
