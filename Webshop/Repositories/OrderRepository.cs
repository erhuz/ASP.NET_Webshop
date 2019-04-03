using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;
using Webshop.Models;

namespace Webshop.Repositories
{
    public class OrderRepository
    {
        private readonly string _connectionString;

        public OrderRepository(string connectionsString)
        {
            this._connectionString = connectionsString;
        }

        public Order Get(int id)
        {
            Order order = new Order();
            order.Id = id;
            using (var connection = new MySqlConnection(this._connectionString))
            {
                order.Items = connection.Query<Product>("SELECT p.id, p.categoryId, p.title, p.description, p.price FROM products AS p LEFT JOIN order_rows AS cr ON p.id = cr.productId LEFT JOIN orders AS c ON cr.orderId = c.id WHERE c.id = @id",
                    new {id}).ToList();
            }
            
            return order;
        }

        public int Create()
        {
            using (var connection = new MySqlConnection(this._connectionString))
            {
                // Insert order_row to w/ order.id, if no card.id is provided, create order and return order.id
                connection.Execute("INSERT INTO orders() VALUES ()");
                return connection.Query<int>("SELECT LAST_INSERT_ID();").FirstOrDefault();
            }
        }

        public bool Exists(int? id)
        {
            int? result;
            
            using (var connection = new MySqlConnection(this._connectionString))
            {
                result = connection.Query<int?>("SELECT id FROM orders WHERE id = @id", new {id}).FirstOrDefault();
            }

            return result != null;
        }

        public void Add(Order order)
        {
            using (var connection = new MySqlConnection(this._connectionString))
            {
                connection.Execute("",
                    order);
            }
        }

        public void Delete(int id)
        {
            using (var connection = new MySqlConnection(this._connectionString))
            {
                connection.Execute("DELETE FROM orders WHERE id=@id", new {id});
            }
        }
    }
}