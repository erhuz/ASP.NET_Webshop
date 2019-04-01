using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;
using Webshop.Models;

namespace Webshop.Repositories
{
    public class CategoryRepository
    {
        private readonly string _connectionString;

        public CategoryRepository(string connectionsString)
        {
            this._connectionString = connectionsString;
        }

        public List<Category> Get()
        {
            using (var connection = new MySqlConnection(this._connectionString))
            {
                return connection.Query<Category>("SELECT * FROM categories").ToList();
            }
        }

        public Category Get(int id)
        {
            using (var connection = new MySqlConnection(this._connectionString))
            {
                return connection.QuerySingleOrDefault<Category>("SELECT * FROM categories WHERE id = @id", new {id});
            }
        }

        public void Add(Category category)
        {
            using (var connection = new MySqlConnection(this._connectionString))
            {
                connection.Execute(
                    "INSERT INTO categories (title, description) VALUES(@Title, @Description)",
                    category);
            }
        }

        public void Delete(int id)
        {
            using (var connection = new MySqlConnection(this._connectionString))
            {
                connection.Execute("DELETE FROM categories WHERE id = @id", new {id});
            }
        }
    }
}