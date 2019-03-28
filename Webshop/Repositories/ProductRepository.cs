using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
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
            using (var connection = new SqlConnection(this.connectionString))
            {
                return connection.Query<Product>("SELECT * FROM Products").ToList();
            }
        }

        public Product Get(int id)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                return connection.QuerySingleOrDefault<Product>("SELECT * FROM Products WHERE id = @id", new {id});
            }
        }

        public void Add(Product product)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Execute(
                    "INSERT INTO Products (category_id, title, description, price) VALUES(@categry_id, @title, @description, @price)",
                    product);
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Execute("DELETE FROM Products WHERE id = @id", new {id});
            }
        }
    }
}