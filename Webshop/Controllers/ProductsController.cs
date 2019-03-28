using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Webshop.Models;
using Webshop.Repositories;
using Webshop.Services;

namespace Webshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly ProductService _productService;


        public ProductsController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ConnectionString");
            this._productService = new ProductService(new ProductRepository(connectionString));
        }
        
        // GET api/products
        [HttpGet]
        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
        public ActionResult Get()
        {
            var products = this._productService.Get();
            
            return Ok(products);
        }

        // GET api/products/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Get(int id)
        {
            var product = this._productService.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // POST api/products
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] Product product)
        {
            var result = this._productService.Add(product);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        // DELETE api/products/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = this._productService.Delete(id);

            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}