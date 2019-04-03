using System.Collections.Generic;
using System.Transactions;
using Webshop.Models;
using Webshop.Repositories;

namespace Webshop.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        public List<Category> Get()
        {
            return this._categoryRepository.Get();
        }

        public Category Get(int id)
        {
            return this._categoryRepository.Get(id);
        }

        public bool Add(Category category)
        {
            if (
                    string.IsNullOrEmpty(category?.Title) ||
                    string.IsNullOrEmpty(category?.Description)
            ){
                return false;
            }
            
            this._categoryRepository.Add(category);
            
            return true;
        }

        public bool Delete(int id)
        {
            using (var transaction = new TransactionScope())
            {
                var categoryItem = this._categoryRepository.Get(id);

                if (categoryItem == null)
                {
                    return false;
                }
                
                this._categoryRepository.Delete(id);
                
                transaction.Complete();

                return true;
            }
        }
    }
}