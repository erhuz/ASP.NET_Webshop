using System.Collections.Generic;
using Webshop.Models;


namespace Webshop.Repositories
{
    public interface IProductRepository
    {
        List<Product> Get();

        Product Get(int id);

        void Add(Product product);

        void Delete(int id);
    }
}