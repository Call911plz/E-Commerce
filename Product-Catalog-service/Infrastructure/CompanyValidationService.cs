using ProductCatalogMicroService.Application;

namespace ProductCatalogMicroService.Infrastructure;

public class CompanyValidationService(ICompanyService companyService) : ICompanyValidationService
{
    private readonly ICompanyService _companyService = companyService;

    public bool ValidateCompany(int id)
    {
        return (_companyService.GetCompanyDto(id) != null);
    }
}
