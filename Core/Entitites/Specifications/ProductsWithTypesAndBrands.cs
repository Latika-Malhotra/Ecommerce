using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entitites.Specifications
{
    public class ProductsWithTypesAndBrands: BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrands()
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.productBrand);
        }

        public ProductsWithTypesAndBrands(int id):base(x=> x.Id ==id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.productBrand);
        }
    }
}
