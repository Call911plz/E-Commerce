using Microsoft.EntityFrameworkCore;
using ProductCatalogMicroService.Application;
using ProductCatalogMicroService.Domain;

namespace ProductCatalogMicroService.Infrastructure;

public class CompanyRepository(ProductDbContext context) : ICompanyRepository
{
    private readonly ProductDbContext _context = context;

    public async Task<Company> CreateCompanyAsync(Company info)
    {
        var result = _context.Companies.Add(info);

        await _context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<List<Company>> GetAllCompaniesAsync()
    {
        return await _context.Companies.ToListAsync();
    }

    public async Task<Company?> GetCompanyAsync(int id)
    {
        return await _context.Companies.FindAsync(id);
    }

    public async Task<Company?> GetCompanyAsync(string uuid)
    {
        return await _context.Companies.FirstOrDefaultAsync(company => company.Uuid == uuid);
    }

    public async Task<Company> UpdateCompanyAsync(int id, Company info)
    {
        var company = await GetCompanyAsync(id);

        return await UpdateCompanyInternalAsync(company, info);
    }

    public async Task<Company> UpdateCompanyAsync(string uuid, Company info)
    {
        var company = await GetCompanyAsync(uuid);

        return await UpdateCompanyInternalAsync(company, info);
    }

    public async Task<Company> DeleteCompanyAsync(int id)
    {
        var company = await GetCompanyAsync(id);

        return await DeleteCompanyInternalAsync(company);
    }

    public async Task<Company> DeleteCompanyAsync(string uuid)
    {
        var company = await GetCompanyAsync(uuid);

        return await DeleteCompanyInternalAsync(company);
    }

    private async Task<Company> UpdateCompanyInternalAsync(Company? company, Company info)
    {
        if (company == null)
            throw new Exception("Invalid company id provided");

        company.Name = info.Name;
        company.Products = info.Products;

        await _context.SaveChangesAsync();

        return company;
    }

    private async Task<Company> DeleteCompanyInternalAsync(Company? company)
    {
        if (company == null)
            throw new Exception("Invalid company id provided");

        _context.Remove(company);

        await _context.SaveChangesAsync();

        return company;
    }
}
