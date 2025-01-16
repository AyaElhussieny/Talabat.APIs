using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;

namespace Talabat.APIs.Controllers
{
   
    public class ProductsController : APIBaseController
    {
        private readonly IGenericRepository<Product> _productRepo;

        public ProductsController(IGenericRepository<Product> ProductRepo) 
        {
            _productRepo = ProductRepo;
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
            #endregion

            return Ok(products);
        }


        //Get products by id

        [HttpGet("{id}")]

        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productRepo.GetByIdAsync(id);
            return Ok(product);
        }
    }
}
