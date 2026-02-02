namespace ProductCatalogMicroService.Application;

public interface IProductService
{
    public Task<ProductDto> CreateProductAsync(string companyUuid, ProductDto info);
    public Task<List<ProductDto>> GetAllProductsAsync();
    public Task<CompanyProductsDto> GetAllCompanyProductsAsync(string companyUuid);
    public Task<ProductDto?> GetProductDtoAsync(string productUuid);
    public Task<ProductDto?> UpdateProductAsync(
        string companyUuid,
        string productUuid,
        ProductDto info
    );
    public Task<ProductDto?> DeleteProductAsync(string productUuid);
}
