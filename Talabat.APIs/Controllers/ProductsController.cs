using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;

namespace Talabat.APIs.Controllers
{
   
    public class ProductsController : APIBaseController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> ProductRepo , IMapper mapper) 
        {
            _productRepo = ProductRepo;
            _mapper = mapper;
        }


        // Get all products
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            #region Old Code Without Specifications
            //var products = await _productRepo.GetAllAsync();
            //OkObjectResult Res = new OkObjectResult(products);
            //return Res;
            #endregion


            #region With Specifications
            var Spec = new ProductWithBrandAndTypeSpecifications();
            var products = await _productRepo.GetAllWithSpacAsync(Spec);
            var MappingProducts = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDTO>>(products); 
            #endregion

            return Ok(MappingProducts);
        }


        //Get products by id

        [HttpGet("{id}")]

        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            #region Old Code Without Specifications
            //var products = await _productRepo.GetByIdAsync(id);
            //OkObjectResult Res = new OkObjectResult(products);
            //return Res;
            #endregion

            var Spec = new ProductWithBrandAndTypeSpecifications(id);
            var product = await _productRepo.GetByIdWithSpacAsync(Spec);
            var MappingProducts = _mapper.Map<Product,ProductToReturnDTO>(product);

            return Ok(MappingProducts);
        }
    }
}
