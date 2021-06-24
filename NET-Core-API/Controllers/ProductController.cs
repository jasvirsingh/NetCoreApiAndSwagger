using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NET_Core_API.Data.Entities;
using NET_Core_API.Data.Repositories.Repos_Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NET_Core_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepo _productRepo;

        public ProductController(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }

        /// <summary>
        /// Gets list of all products
        /// </summary>
        /// <returns>The list of products</returns>
        [HttpGet]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<Product>>> Get()
        {
            try
            {
                var products = _productRepo.GetAll();

                return Ok(await products);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Database failure");
            }
        }

        /// <summary>
        /// Gets a specific product
        /// </summary>
        /// <param name="id">unique value of the product</param>
        /// <returns>returns a product object</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Product>> Get(int id)
        {
            try
            {
                var product = _productRepo.Get(id);

                return Ok(await product);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Database failure");
            }
        }

        /// <summary>
        /// Creates a product
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/Product
        ///     {        
        ///      "name": "Cargo Pants",
        ///      "description": "Nice Cargo Pants",  
        ///       "categories": [
        ///              {
        ///                  "id": 0,
        ///                  "productID": 0,
        ///                  "categoryID": 0,
        ///                  "category" :{
        ///                      "name":"Fashion"
        ///                  }
        ///              },
        ///              {
        ///                  "id": 0,
        ///                  "productID": 0,
        ///                  "categoryID": 0,
        ///                  "category":{
        ///                      "name":"Street"
        ///                  }
        ///              }
        ///         ]
        ///     }
        /// </remarks>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Product>> Post(Product product)
        {
            try
            {
                var existing = _productRepo.Get(product.ID);

                if (existing != null)
                    return BadRequest("Product in Use");

                await _productRepo.Save(product);

                return Created($"api/Product/{product.ID}", product);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Database failure");
            }


        }
        /// <summary>
        /// Update an exisitng product
        /// </summary>
        /// <param name="id">unique value of the product</param>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT api/Product
        ///     {
        ///      "id": "1",
        ///      "name": "Cargo Pants",
        ///      "description": "Nice Cargo Pants",  
        ///       "categories": [
        ///              {
        ///                  "id": 0,
        ///                  "productID": 1,
        ///                  "categoryID": 3,
        ///                  "category" :{
        ///                      "name":"Supplies"
        ///                  }
        ///              },
        ///              {
        ///                  "id": 0,
        ///                  "productID": 1,
        ///                  "categoryID": 2,
        ///                  "category":{
        ///                      "name":"Street"
        ///                  }
        ///              }
        ///         ]
        ///     }
        /// </remarks>
        /// <param name="product">201 OK</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Put(int id, Product product)
        {
            try
            {
                if (product.ID != id)
                {
                    return BadRequest("Query ID doesn't matches the object property");
                }

                await _productRepo.Save(product);

                return Ok(product);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Database failure");
            }
        }

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="id">unique value of the product</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var productFound = await _productRepo.Get(id);

                if (productFound == null)
                    return NotFound("couldn't find resource");

                var result = await _productRepo.Delete(productFound);
                if (result > 0)
                {
                    return Ok();
                }
                else
                {
                    return StatusCode
                        (StatusCodes.Status500InternalServerError,
                        "Database Failure");
                }
            }
            catch (Exception)
            {
                return StatusCode
                    (StatusCodes.Status500InternalServerError,
                    "Database failure");
            }
        }
    }
}
