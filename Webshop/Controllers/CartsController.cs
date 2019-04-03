using System;
using System.Net;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Dapper;
using Webshop.Models;
using Webshop.Repositories;
using Webshop.Services;

namespace Webshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {

        private readonly CartService _cartService;


        public CartsController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ConnectionString");
            this._cartService = new CartService(new CartRepository(connectionString));
        }

        // GET api/carts/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Get(int id)
        {
            var cart = this._cartService.Get(id);
            return Ok(cart);
        }

        // POST api/carts
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] CartItem cartItem)
        {
            if(this._cartService.Add(cartItem))
            {
                return Ok();
            }
            
            return BadRequest();
        }

        // DELETE api/carts/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = this._cartService.Delete(id);

            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}