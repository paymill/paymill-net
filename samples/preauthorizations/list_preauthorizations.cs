PreauthorizationService preauthorizationService = paymillContext.PreauthorizationService;
PaymillList<Preauthorization> preauthorizations = preauthorizationService.ListAsync().Result;
