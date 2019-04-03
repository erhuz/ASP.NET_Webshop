using System.Collections.Generic;
using System.Transactions;
using Webshop.Models;
using Webshop.Repositories;

namespace Webshop.Services
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;

        public OrderService(OrderRepository orderRepository)
        {
            this._orderRepository = orderRepository;
        }

        public Order Get(int id)
        {
            return this._orderRepository.Get(id);
        }

        public bool Add(Order order)
        {
            
            // Validate order

            this._orderRepository.Add(order);
            return true;
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