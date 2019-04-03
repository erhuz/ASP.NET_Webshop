using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;
using Webshop.Models;

namespace Webshop.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(string connectionsString)
        {
            this._connectionString = connectionsString;
        }

        public List<Product> Get()
        {
            using (var connection = new MySqlConnection(this._connectionString))
            {
                return connection.Query<Product>("SELECT * FROM products").ToList();
            }
        }

        public Product Get(int id)
        {
            using (var connection = new MySqlConnection(this._connectionString))
            {
                return connection.QuerySingleOrDefault<Product>("SELECT * FROM products WHERE id = @id", new {id});
            }
        }

        public void Add(Product product)
        {
            using (var connection = new MySqlConnection(this._connectionString))
            {
                connection.Execute(
                    "INSERT INTO products (categoryId, title, description, price) VALUES(@CategoryId, @Title, @Description, @Price)",
                    product);
            }
        }

        public void Delete(int id)
        {
            using (var connection = new MySqlConnection(this._connectionString))
            {
                connection.Execute("DELETE FROM products WHERE id = @id", new {id});
            }
        }
    }
}