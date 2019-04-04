using System.Collections.Generic;
using System.Transactions;
using Webshop.Models;
using Webshop.Repositories;

namespace Webshop.Services
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;
        private readonly CartRepository _cartRepository;

        public OrderService(OrderRepository orderRepository, CartRepository cartRepository)
        {
            this._orderRepository = orderRepository;
            this._cartRepository = cartRepository;
        }

        public Order Get(int id)
        {
            return this._orderRepository.Get(id);
        }

        public Order Add(Order order)
        {
            // Validate order
            if (order.CartId <= 0)
            {
                return null;
            }

            if (!this._cartRepository.Exists(order.CartId))
            {
                return null;
            }

            Cart orderCart = this._cartRepository.Get(order.CartId);
            
            order.Items = orderCart.Items;
            
            this._orderRepository.Add(order);
            
            this._cartRepository.Delete(order.CartId);
            
            return this._orderRepository.Get(order.Id);
        }

        public bool Delete(int id)
        {
            using (var transaction = new TransactionScope())
            {
                var orderItem = this._orderRepository.Get(id);
                
                if (orderItem == null)
                {
                    return false;
                }

                this._orderRepository.Delete(id);
                
                transaction.Complete();
                
                return true;
            }
        }
    }
}