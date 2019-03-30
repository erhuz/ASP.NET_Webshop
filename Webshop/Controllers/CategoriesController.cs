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
    public class CategoriesController : ControllerBase
    {

        private readonly CategoryService _categoryService;


        public CategoriesController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ConnectionString");
            this._categoryService = new CategoryService(new CategoryRepository(connectionString));
        }
        
        // GET api/categories
        [HttpGet]
        [ProducesResponseType(typeof(List<Category>), StatusCodes.Status200OK)]
        public ActionResult Get()
        {
            var categories = this._categoryService.Get();
            
            return Ok(categories);
        }

        // GET api/categories/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Get(int id)
        {
            var category = this._categoryService.Get(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // POST api/categories
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] Category category)
        {
            var result = this._categoryService.Add(category);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        // DELETE api/categories/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = this._categoryService.Delete(id);

            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}