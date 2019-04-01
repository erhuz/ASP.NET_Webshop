using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;
using Webshop.Models;

namespace Webshop.Repositories
{
    public class CartRepository
    {
        private readonly string _connectionString;

        public CartRepository(string connectionsString)
        {
            this._connectionString = connectionsString;
        }

        //public Cart Get()
        //{
        //    using (var connection = new MySqlConnection(this.connectionString))
        //    {
        //        // Return all carts of items
        //        //return connection.Query<Product>("SELECT * FROM products").ToList();
        //    }
        //}

        public Cart Get(int id)
        {
            using (var connection = new MySqlConnection(this._connectionString))
            {
                
                // Return a cart with items
                return connection.QuerySingleOrDefault<Cart>("SELECT * FROM products WHERE id = @id", new {id});
            }
        }

        public void Add(int cartId,int productId)
        {
            using (var connection = new MySqlConnection(this._connectionString))
            {
                 connection.Execute(
                    "INSERT INTO cart_rows (product_id, cart_id) VALUES(@product_id, @cart_id)",
                    new {product_id = productId, cart_id = cartId});
            }
        }

        public void Delete(int id)
        {
            using (var connection = new MySqlConnection(this._connectionString))
            {
                // connection.Execute("DELETE FROM products WHERE id = @id", new {id});
            }
        }
    }
}