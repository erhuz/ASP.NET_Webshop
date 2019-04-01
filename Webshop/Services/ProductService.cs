using System.Collections.Generic;
using System.Transactions;
using Webshop.Models;
using Webshop.Repositories;

namespace Webshop.Services
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository;

        public ProductService(ProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

        public List<Product> Get()
        {
            return this._productRepository.Get();
        }

        public Product Get(int id)
        {
            return this._productRepository.Get(id);
        }

        public bool Add(Product product)
        {
            if (
                    product?.CategoryId == null ||
                    string.IsNullOrEmpty(product?.Title) ||
                    string.IsNullOrEmpty(product?.Description) ||
                    product?.Price == null
            ){
                return false;
            }
            
            this._productRepository.Add(product);
            
            return true;
        }

        public bool Delete(int id)
        {
            using (var transaction = new TransactionScope())
            {
                var productItem = this._productRepository.Get(id);

                if (productItem == null)
                {
                    return false;
                }
                
                this._productRepository.Delete(id);
                
                transaction.Complete();

                return true;
            }
        }
    }
}