using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderService.Models;

namespace OrderService.Repositories
{
    public class InMemoryOrderRepository : IOrderRepository
    {
        private readonly List<Order> _orders = new List<Order>();

        public Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return Task.FromResult<IEnumerable<Order>>(_orders);
        }

        public Task<Order> GetOrderByIdAsync(int id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            return Task.FromResult(order);
        }

        public Task AddOrderAsync(Order order)
        {
            order.Id = _orders.Any() ? _orders.Max(o => o.Id) + 1 : 1;
            _orders.Add(order);
            return Task.CompletedTask;
        }

        public Task UpdateOrderAsync(Order order)
        {
            var existingOrder = _orders.FirstOrDefault(o => o.Id == order.Id);
            if (existingOrder != null)
            {
                existingOrder.UserId = order.UserId;
                existingOrder.Product = order.Product;
                existingOrder.Quantity = order.Quantity;
                existingOrder.OrderDate = order.OrderDate;
            }
            return Task.CompletedTask;
        }

        public Task DeleteOrderAsync(int id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order != null)
            {
                _orders.Remove(order);
            }
            return Task.CompletedTask;
        }
    }
}
