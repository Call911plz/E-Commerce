using ProductCatalogMicroService.Domain;

namespace ProductCatalogMicroService.Application;

public interface IProductRepository
{
    public Task<Product> CreateProductAsync(Company company, Product info);
    public Task<List<Product>> GetAllProductsAsync();

    public Task<Product?> GetProductAsync(int id);
    public Task<Product> UpdateProductAsync(int id, Product info);
    public Task<Product> DeleteProductAsync(int id);

    public Task<Product?> GetProductAsync(string uuid);
    public Task<Product> UpdateProductAsync(string uuid, Product info);
    public Task<Product> DeleteProductAsync(string uuid);
}
