PreauthorizationService preauthorizationService = paymillContext.PreauthorizationService;
Preauthorization preauthorization = preauthorizationService.GetAsync(
    "preauth_31eb90495837447f76b7"
).Result;
