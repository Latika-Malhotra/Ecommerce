
using AutoMapper;
using Core.Entitites;
using Core.Entitites.Specifications;
using Core.Interfaces;
using ECommerce.Dtos;
using ECommerce.Errors;
using ECommerce.Helpers;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseApiController
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
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams productParams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
            var countSpec = new ProductsWithTypesAndBrandsSpecification(productParams);
            var totalItems = await _productRepo.CountAsync(spec);
            
            var products = await _productRepo.ListAsync(spec);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex,productParams.PageSize,totalItems,data));
            
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),(StatusCodes.Status404NotFound))]
        public async Task<ActionResult<ProductToReturnDto>> GetProductById(int Id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(Id);
            var products = await _productRepo.GetEntityWithSpec(spec);

            if (products == null) return NotFound(new ApiResponse(404));
            
            return Ok(_mapper.Map<Product, ProductToReturnDto>(products));
            
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