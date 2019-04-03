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
    public class OrdersController : ControllerBase
    {

        private readonly OrderService _orderService;


        public OrdersController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ConnectionString");
            this._orderService = new OrderService(new OrderRepository(connectionString));
        }

        // GET api/orders/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Get(int id)
        {
            var order = this._orderService.Get(id);
            return Ok(order);
        }

        // POST api/orders
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] Order order)
        {
            if(this._orderService.Add(order))
            {
                return Ok();
            }
            
            return BadRequest();
        }

        // DELETE api/orders/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = this._orderService.Delete(id);

            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}