using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entitites.Specifications
{
    class ProductWithFilterForCountSpecification: BaseSpecification<Product>
    {
        public ProductWithFilterForCountSpecification(ProductSpecParams productParams)
            : base(x =>
        (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
        (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId))
        {

        }

    }
}
