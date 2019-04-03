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

        public Cart Get(int id)
        {
            Cart cart = new Cart();
            cart.Id = id;
            using (var connection = new MySqlConnection(this._connectionString))
            {
                cart.Items = connection.Query<Product>("SELECT p.id, p.categoryId, p.title, p.description, p.price FROM products AS p LEFT JOIN cart_rows AS cr ON p.id = cr.productId LEFT JOIN carts AS c ON cr.cartId = c.id WHERE c.id = @id",
                    new {id}).ToList();
            }
            
            return cart;
        }

        public bool Exists(int? id)
        {
            int? result;
            
            using (var connection = new MySqlConnection(this._connectionString))
            {
                result = connection.Query<int?>("SELECT id FROM carts WHERE id = @id", new {id}).FirstOrDefault();
            }

            return result != null;
        }

        public int Create()
        {
            using (var connection = new MySqlConnection(this._connectionString))
            {
                // Insert cart_row to w/ cart.id, if no card.id is provided, create cart and return cart.id
                connection.Execute("INSERT INTO carts() VALUES ()");
                return connection.Query<int>("SELECT LAST_INSERT_ID();").FirstOrDefault();
            }
        }

        public void Add(CartItem cartItem)
        {
            int productId = cartItem.ProductId;
            int? cartId = cartItem.CartId;

            using (var connection = new MySqlConnection(this._connectionString))
            {
                connection.Execute("INSERT INTO cart_rows(productId, cartId) VALUES (@productId, @cartId)",
                    cartItem);
            }
        }

        public void Delete(int id)
        {
            using (var connection = new MySqlConnection(this._connectionString))
            {
                connection.Execute("DELETE FROM cart_rows WHERE cartId=@id; DELETE FROM carts WHERE id=@id", new {id});
            }
        }
    }
}