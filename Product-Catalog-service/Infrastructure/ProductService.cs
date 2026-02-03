using ProductCatalogMicroService.Application;
using ProductCatalogMicroService.Domain; // Using for testing. Delete.

namespace ProductCatalogMicroService.Infrastructure;

public class ProductService(
    IProductRepository productRepository,
    ICompanyRepository companyRepository
) : IProductService
{
    private readonly IProductRepository _repo = productRepository;
    private readonly ICompanyRepository _companyRepository = companyRepository;

    public async Task<ProductDto> CreateProductAsync(string companyUuid, ProductDto info)
    {
        // Throwing error since validating company access should have been done before hand
        var company =
            await _companyRepository.GetCompanyAsync(companyUuid)
            ?? throw new Exception("Invalid company Id given");

        // Building product to insert
        var uuid = Guid.NewGuid().ToString();
        var product = info.ToProduct(company, uuid: uuid);
        var createdProduct = await _repo.CreateProductAsync(company, product);

        return createdProduct.ToDto();
    }

    public async Task<List<ProductDto>> GetAllProductsAsync()
    {
        var products = await _repo.GetAllProductsAsync();

        return products.Select(product => product.ToDto()).ToList();
    }

    public async Task<CompanyProductsDto> GetAllCompanyProductsAsync(string companyUuid)
    {
        var company =
            await _companyRepository.GetCompanyAsync(companyUuid)
            ?? throw new Exception("Invalid company Id given");
        var products = (await _repo.GetAllProductsAsync()).Where(product =>
            product.Company.Uuid == companyUuid
        );

        return new CompanyProductsDto()
        {
            Company = company.ToDto(),
            Products = products.Select(product => product.ToDto()).ToList(),
        };
    }

    public async Task<ProductDto?> GetProductDtoAsync(string productUuid)
    {
        var product = await _repo.GetProductAsync(productUuid);

        return (product == null) ? null : product.ToDto();
    }

    public async Task<ProductDto?> UpdateProductAsync(
        string companyUuid,
        string productUuid,
        ProductDto info
    )
    {
        var company =
            await _companyRepository.GetCompanyAsync(companyUuid)
            ?? throw new Exception("Invalid company Id given");

        if (await _repo.GetProductAsync(productUuid) == null)
            return null;

        var product = info.ToProduct(company, uuid: productUuid);

        var updatedProduct = await _repo.UpdateProductAsync(productUuid, product);
        return updatedProduct.ToDto();
    }

    public async Task<ProductDto?> DeleteProductAsync(string productUuid)
    {
        if (await _repo.GetProductAsync(productUuid) == null)
            return null;

        var deletedProduct = await _repo.DeleteProductAsync(productUuid);

        return deletedProduct.ToDto();
    }
}
