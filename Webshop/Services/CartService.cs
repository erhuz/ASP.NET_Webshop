using System.Collections.Generic;
using System.Transactions;
using Webshop.Models;
using Webshop.Repositories;

namespace Webshop.Services
{
    public class CartService
    {
        private readonly CartRepository _cartRepository;

        public CartService(CartRepository cartRepository)
        {
            this._cartRepository = cartRepository;
        }

        public Cart Get(int id)
        {
            return this._cartRepository.Get(id);
        }

        public bool Add(CartItem cartItem)
        {
            if (cartItem.CartId != null && cartItem.CartId <= 0)
            {
                return false;
            }

            if (cartItem.CartId == null)
            {
                cartItem.CartId = this._cartRepository.Create();
            }

            if (!this._cartRepository.Exists(cartItem.CartId))
            {
                return false;
            }

            this._cartRepository.Add(cartItem);
            return true;
        }

        public bool Delete(int id)
        {
            using (var transaction = new TransactionScope())
            {
                var cartItem = this._cartRepository.Get(id);
                
                if (cartItem == null)
                {
                    return false;
                }

                this._cartRepository.Delete(id);
                
                transaction.Complete();
                
                return true;
            }
        }
    }
}