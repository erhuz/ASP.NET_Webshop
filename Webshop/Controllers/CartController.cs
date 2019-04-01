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

        //private readonly CartService _CartService;
        private readonly string _connectionString;


        public CartsController(IConfiguration configuration)
        {
            this._connectionString = configuration.GetConnectionString("ConnectionString");
            //this._CartService = new CartService(new CartRepository(connectionString));
        }

        // GET api/carts/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Get(int id)
        {
            Cart cart = new Cart();
            cart.Id = id;
            using (var connection = new MySqlConnection(this._connectionString))
            {
                var query =
                    "SELECT p.id, p.category_id, p.title, p.description, p.price FROM products AS p LEFT JOIN cart_rows AS cr ON p.id = cr.product_id LEFT JOIN carts AS c ON cr.cart_id = c.id WHERE c.id = @id";
                cart.Items = connection.Query<Product>(query,
                    new {id}).ToList();
            }
            
            return Ok(cart);
        }

        // POST api/carts
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] CartItem cartItem)
        {
            int productId = cartItem.ProductId;
            int? cartId = cartItem.CartId;
            
            if (cartId == null)
            {
                using (var connection = new MySqlConnection(this._connectionString))
                {
                    // Insert cart_row to w/ cart.id, if no card.id is provided, create cart and return cart.id
                    connection.Execute("INSERT INTO carts() VALUES ()");
                    cartItem.CartId = connection.Query<int>("SELECT LAST_INSERT_ID();").FirstOrDefault();
                }
            }
            
            using (var connection = new MySqlConnection(this._connectionString))
            {
                connection.Execute("INSERT INTO cart_rows(product_id, cart_id) VALUES (@productId, @cartId)",
                    cartItem);
            }

            return Ok();
        }

        // DELETE api/carts/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}