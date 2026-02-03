using Microsoft.EntityFrameworkCore;
using ProductCatalogMicroService.Application;
using ProductCatalogMicroService.Domain;

namespace ProductCatalogMicroService.Infrastructure;

public class ProductRepository(ProductDbContext context) : IProductRepository
{
    private readonly ProductDbContext _context = context;

    public async Task<Product> CreateProductAsync(Company company, Product info)
    {
        info.Company = company;
        var result = await _context.Products.AddAsync(info);

        await _context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product?> GetProductAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<Product?> GetProductAsync(string uuid)
    {
        return await _context.Products.FirstOrDefaultAsync(product => product.Uuid == uuid);
    }

    public async Task<Product> UpdateProductAsync(int id, Product info)
    {
        var product = await GetProductAsync(id);

        return await UpdateProductInternalAsync(product, info);
    }

    public async Task<Product> UpdateProductAsync(string uuid, Product info)
    {
        var product = await GetProductAsync(uuid);

        return await UpdateProductInternalAsync(product, info);
    }

    public async Task<Product> DeleteProductAsync(int id)
    {
        var product = await GetProductAsync(id);

        return await DeleteProductInternalAsync(product);
    }

    public async Task<Product> DeleteProductAsync(string uuid)
    {
        var product = await GetProductAsync(uuid);

        return await DeleteProductInternalAsync(product);
    }

    private async Task<Product> UpdateProductInternalAsync(Product? product, Product info)
    {
        if (product == null)
            throw new Exception("Invalid Product id provided");

        product.Company = info.Company;
        product.Name = info.Name;
        product.Cost = info.Cost;
        product.Description = info.Description;

        await _context.SaveChangesAsync();

        return product;
    }

    private async Task<Product> DeleteProductInternalAsync(Product? product)
    {
        if (product == null)
            throw new Exception("Invalid Product id provided");

        _context.Remove(product);

        await _context.SaveChangesAsync();

        return product;
    }
}
