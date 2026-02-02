using ProductCatalogMicroService.Domain;

namespace ProductCatalogMicroService.Application;

public static class ProductExtensions
{
    public static ProductDto ToDto(this Product info)
    {
        return new ProductDto
        {
            Name = info.Name,
            Uuid = info.Uuid,
            Cost = info.Cost,
            Description = info.Description,
        };
    }

    public static Product ToProduct(
        this ProductDto dto,
        Company company,
        int id = default,
        string uuid = ""
    )
    {
        return new Product
        {
            Id = id,
            Company = company,
            Uuid = uuid,
            Name = dto.Name,
            Cost = dto.Cost,
            Description = dto.Description,
        };
    }

    // public static Product ToProduct(this ProductCreateDto dto, Company company, int id = default)
    // {
    //     return new Product
    //     {
    //         Id = id,
    //         Company = company,
    //         PublicHash = dto.PublicHash,
    //         Name = dto.Name,
    //         Cost = dto.Cost,
    //         Description = dto.Description,
    //     };
    // }
}

public static class CompanyExtensions
{
    public static CompanyDto ToDto(this Company info)
    {
        return new CompanyDto { Uuid = info.Uuid, Name = info.Name };
    }

    public static Company ToCompany(this CompanyDto dto, int id = default, string uuid = "")
    {
        return new Company
        {
            Id = id,
            Uuid = uuid,
            Name = dto.Name,
        };
    }
}
