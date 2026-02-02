using ProductCatalogMicroService.Domain;

namespace ProductCatalogMicroService.Application;

public interface ICompanyRepository
{
    public Task<Company> CreateCompanyAsync(Company info);
    public Task<List<Company>> GetAllCompaniesAsync();

    public Task<Company?> GetCompanyAsync(int id);
    public Task<Company> UpdateCompanyAsync(int id, Company info);
    public Task<Company> DeleteCompanyAsync(int id);

    public Task<Company?> GetCompanyAsync(string uuid);
    public Task<Company> UpdateCompanyAsync(string uuid, Company info);
    public Task<Company> DeleteCompanyAsync(string uuid);
}
