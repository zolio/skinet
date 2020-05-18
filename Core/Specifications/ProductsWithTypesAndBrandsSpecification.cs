using Core.Entities;

namespace Core.Specifications
{
  public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
  {
    // constructor with no paramaters
    // || or else , x.Name.ToLower().Contains(productParams.Search)) to find products with the search term
    // First we check if there is an existing value
    public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productParams)
      : base(x =>
        (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
        (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
        // If false the right side will be executed
        (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
      )
    {
      AddInclude(x => x.ProductType);
      AddInclude(x => x.ProductBrand);
      AddInclude(x => x.Photos);
      // default ordering
      AddOrderBy(x => x.Name);
      ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

      if (!string.IsNullOrEmpty(productParams.Sort))
      {
        switch (productParams.Sort)
        {
          case "priceAsc":
            AddOrderBy(p => p.Price);
            break;
          case "priceDesc":
            AddOrderByDescending(p => p.Price);
            break;
          default:
            AddOrderBy(x => x.Name);
            break;
        }
      }
    }

    // First part is the paramater for our criteria, section part is the expression
    // The base creates a new instance of our BaseSpecification
    public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
    {
      AddInclude(x => x.ProductType);
      AddInclude(x => x.ProductBrand);
    }

    // the other constructor (with a spec paramater)
  }
}