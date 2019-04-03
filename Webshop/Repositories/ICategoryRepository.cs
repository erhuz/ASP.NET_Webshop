using System.Collections.Generic;
using Webshop.Models;


namespace Webshop.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> Get();

        Category Get(int id);

        void Add(Category category);

        void Delete(int id);
    }
}