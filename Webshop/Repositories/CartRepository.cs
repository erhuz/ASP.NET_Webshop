using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;
using Webshop.Models;

namespace Webshop.Repositories
{
    public class ProductRepository
    {
        private readonly string connectionString;

        public ProductRepository(string connectionsString)
        {
            this.connectionString = connectionsString;
        }

        public List<Product> Get()
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                return connection.Query<Product>("SELECT * FROM products").ToList();
            }
        }

        public Product Get(int id)
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                return connection.QuerySingleOrDefault<Product>("SELECT * FROM products WHERE id = @id", new {id});
            }
        }

        public void Add(Product product)
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                connection.Execute(
                    "INSERT INTO products (category_id, title, description, price) VALUES(@category_id, @title, @description, @price)",
                    product);
            }
        }

        public void Delete(int id)
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                connection.Execute("DELETE FROM products WHERE id = @id", new {id});
            }
        }
    }
}