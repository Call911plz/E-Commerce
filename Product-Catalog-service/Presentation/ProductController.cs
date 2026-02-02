using Microsoft.AspNetCore.Mvc;
using ProductCatalogMicroService.Application;

namespace ProductCatalogMicroService.Presentation;

[ApiController]
[Route("[controller]")]
public class ProductController(
    IProductService productService,
    ICompanyValidationService companyValidationService
) : ControllerBase
{
    private readonly IProductService _productService = productService;
    private readonly ICompanyValidationService _companyValidationService = companyValidationService;

    [HttpPost("{companyUuid}")]
    public async Task<ActionResult<ProductDto>> CreateProductAsync(
        string companyUuid,
        ProductDto info
    )
    {
        if (await _companyValidationService.ValidateCompanyAsync(companyUuid) == false)
            return NotFound("Company not found");

        return Ok(await _productService.CreateProductAsync(companyUuid, info));
    }

    [HttpGet]
    public async Task<ActionResult<List<CompanyProductsDto>>> GetAllProductsAsync()
    {
        return Ok(await _productService.GetAllProductsAsync());
    }

    [HttpGet("{companyUuid}")]
    public async Task<ActionResult<CompanyProductsDto>> GetAllCompanyProductsAsync(
        string companyUuid
    )
    {
        if (await _companyValidationService.ValidateCompanyAsync(companyUuid) == false)
            return NotFound("Company not found");

        return Ok(await _productService.GetAllCompanyProductsAsync(companyUuid));
    }

    [HttpGet("{companyUuid}/{productUuid}")]
    public async Task<ActionResult<ProductDto>?> GetProductDtoAsync(
        string companyUuid,
        string productUuid
    )
    {
        if (await _companyValidationService.ValidateCompanyAsync(companyUuid) == false)
            return NotFound("Company not found");

        var product = await _productService.GetProductDtoAsync(productUuid);
        return (product != null) ? Ok(product) : NotFound("Product not found");
    }

    [HttpPut("{companyUuid}/{productUuid}")]
    public async Task<ActionResult<ProductDto>?> UpdateProductAsync(
        string companyUuid,
        string productUuid,
        ProductDto info
    )
    {
        if (await _companyValidationService.ValidateCompanyAsync(companyUuid) == false)
            return NotFound("Company not found");

        var product = await _productService.UpdateProductAsync(companyUuid, productUuid, info);
        return (product != null) ? Ok(product) : NotFound("Product not found");
    }

    [HttpDelete("{productUuid}")]
    public async Task<ActionResult<ProductDto>?> DeleteProductAsync(string productUuid)
    {
        var product = await _productService.DeleteProductAsync(productUuid);
        return (product != null) ? Ok(product) : NotFound("Product not found");
    }
}
