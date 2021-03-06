using Core.Entitites;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandssAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products
                .Include(x => x.ProductType).Include(x => x.productBrand)
                .FirstOrDefaultAsync(x=>x.Id == id);
            
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _context.Products.Include(x=>x.ProductType).Include(x=>x.productBrand).ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypessAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }
    }
}
