using ProductCatalogMicroService.Application;

namespace ProductCatalogMicroService.Infrastructure;

public class CompanyValidationService(ICompanyService companyService) : ICompanyValidationService
{
    private readonly ICompanyService _companyService = companyService;

    public async Task<bool> ValidateCompanyAsync(string uuid)
    {
        return (await _companyService.GetCompanyDtoAsync(uuid) != null);
    }
}
