using System;
using System.Linq;
using System.Threading.Tasks;
using OrderService.Models;
using OrderService.Repositories;
using Xunit;

namespace OrderService.Tests.Repositories
{
    public class InMemoryOrderRepositoryTests
    {
        private readonly InMemoryOrderRepository _repository;

        public InMemoryOrderRepositoryTests()
        {
            _repository = new InMemoryOrderRepository();
        }

        [Fact]
        public async Task AddOrderAsync_ShouldAddOrder()
        {
            var order = new Order
            {
                UserId = 1,
                Product = "Test Product",
                Quantity = 2,
                OrderDate = DateTime.UtcNow
            };

            await _repository.AddOrderAsync(order);
            var orders = await _repository.GetAllOrdersAsync();

            Assert.Single(orders);
            Assert.Equal(order.Product, orders.First().Product);
        }

        [Fact]
        public async Task GetOrderByIdAsync_ShouldReturnOrder()
        {
            var order = new Order
            {
                Id = 1,
                UserId = 1,
                Product = "Test Product",
                Quantity = 2,
                OrderDate = DateTime.UtcNow
            };

            await _repository.AddOrderAsync(order);
            var retrievedOrder = await _repository.GetOrderByIdAsync(1);

            Assert.NotNull(retrievedOrder);
            Assert.Equal(order.Product, retrievedOrder.Product);
        }

        [Fact]
        public async Task DeleteOrderAsync_ShouldRemoveOrder()
        {
            var order = new Order
            {
                Id = 1,
                UserId = 1,
                Product = "Test Product",
                Quantity = 2,
                OrderDate = DateTime.UtcNow
            };

            await _repository.AddOrderAsync(order);
            await _repository.DeleteOrderAsync(1);
            var orders = await _repository.GetAllOrdersAsync();

            Assert.Empty(orders);
        }
    }
}
