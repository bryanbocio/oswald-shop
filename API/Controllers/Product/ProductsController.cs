using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Interfaces;
using Core.Specification.Products;
using API.DTOs;
using AutoMapper;
using System.Collections.Generic;
using API.Errors;
using API.Helpers;

namespace API.Controllers.Products
{

   
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductBrand> _productBrandsRepository;
        private readonly IGenericRepository<ProductType> _productTypesRepository;
        private readonly IMapper _mapper;
        public ProductsController(
                            IGenericRepository<Product> productRepository, 
                            IGenericRepository<ProductBrand> productBrandsRepository,
                            IGenericRepository<ProductType> productTypesRepository,
                            IMapper mapper)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _productBrandsRepository = productBrandsRepository;
            _productTypesRepository = productTypesRepository;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductDto>>> GetProducts([FromQuery] ProductSpecificationParameters parameters)
        {

            var specifications = new ProductsWithTypesAndBrandsSpecification(parameters);
            var countSpecification=new ProductWithFiltersForCountSpecification(parameters);
            var totalItems = await _productRepository.CountAsync(countSpecification);
            var products= await _productRepository.listAsync(specifications);

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products);

            return Ok(new Pagination<ProductDto>(parameters.PageIndex, parameters.PageSize, totalItems, data));
            


        }

        [HttpGet("{id}")] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>>GetProduct(int id){
            
            var specifications = new ProductsWithTypesAndBrandsSpecification(id);

            var product =await _productRepository.getEntityWithSpecification(specifications);

            if (product == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<Product,ProductDto>(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _productBrandsRepository.listAllAsync());
        }

        [HttpGet("product-types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await _productTypesRepository.listAllAsync());
        }


    }
}