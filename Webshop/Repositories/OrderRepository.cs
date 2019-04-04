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
            using (var connection = new MySqlConnection(this._connectionString))
            {
                order = connection.Query<Order>("SELECT * FROM orders WHERE id=@id", new {id}).FirstOrDefault();

                order.Items = connection.Query<Product>("SELECT p.id, p.categoryId, p.title, p.description, p.price FROM products AS p LEFT JOIN order_rows AS orr ON p.id = orr.productId LEFT JOIN orders AS o ON orr.orderId = o.id WHERE o.id = @id",
                    new {id}).ToList();
            }

            return order;
        }

        public void Add(Order order)
        {
            using (var connection = new MySqlConnection(this._connectionString))
            {
                connection.Execute("INSERT INTO orders(email, firstName, lastName, address) VALUES (@Email, @FirstName, @LastName, @Address)",
                    order);
                order.Id = connection.Query<int>("SELECT LAST_INSERT_ID();").FirstOrDefault();

                foreach (var product in order.Items)
                {
                    connection.Execute("INSERT INTO order_rows(productId, orderid) VALUES (@ProductId, @OrderId)",
                        new {ProductId = product.Id, OrderId = order.Id});
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = new MySqlConnection(this._connectionString))
            {
                connection.Execute("DELETE FROM order_rows WHERE orderId=@id; DELETE FROM orders WHERE id=@id", new {id});
            }
        }
    }
}