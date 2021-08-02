
using AutoMapper;
using Core.Entitites;
using Core.Entitites.Specifications;
using Core.Interfaces;
using ECommerce.Dtos;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IMapper _mapper;
        public ProductController(IGenericRepository<Product> productRepo, IGenericRepository<ProductBrand> productBrandRepo, IGenericRepository<ProductType> productTypeRepo,IMapper mapper)
        {
            _productRepo = productRepo;
            _productTypeRepo = productTypeRepo;
            _productBrandRepo = productBrandRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrands();
            var products = await _productRepo.ListAsync(spec);

            return products.Select(product => new ProductToReturnDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                PictureUrl = product.PictureUrl,
                Price = product.Price,
                ProductBrand = product.productBrand.Name,
                ProductType = product.ProductType.Name

            }).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProductById(int Id)
        {
            var spec = new ProductsWithTypesAndBrands(Id);
            var products = await _productRepo.GetEntityWithSpec(spec);
            return _mapper.Map<Product, ProductToReturnDto>(products);
            //return new ProductToReturnDto
            //{
            //    Id = products.Id,
            //    Name = products.Name,
            //    Description = products.Description,
            //    PictureUrl = products.PictureUrl,
            //    Price = products.Price,
            //    ProductBrand = products.productBrand.Name,
            //    ProductType = products.ProductType.Name
            //};
            //return Ok(products);
        }



        [HttpGet("{types}")]
        public async Task<ActionResult<ProductType>> GetProductTypes()
        {

            var productTypes= await _productTypeRepo.ListAllAsync();
            return Ok(productTypes);
        }
        
        [HttpGet("{brands}")]
        public async Task<ActionResult<ProductBrand>> GetProductBrand()
        {

            var productBrands = await _productBrandRepo.ListAllAsync();
            return Ok(productBrands);
        }
    }


}