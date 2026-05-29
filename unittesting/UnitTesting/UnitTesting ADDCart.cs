using Xunit; // Use xUnit's assertion library
using NewEra.BLL;
using NewEra.Domain.Models;
using NewEra.Domain.Interface;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace UnitTesting
{
    // No [TestClass] attribute needed for xUnit
    public class CartSystemTests
    {
        private readonly CartSystem _cartSystem;
        private readonly Mock<ICart> _mockCartRepository;

        // Use the constructor for setup instead of [TestInitialize]
        public CartSystemTests()
        {
            _mockCartRepository = new Mock<ICart>();
            _cartSystem = new CartSystem(_mockCartRepository.Object);
        }

        [Fact]
        public void AddProduct_Should_CreateCart_And_AddItem_When_CartIsNull()
        {
            // Arrange
            List<Cart> currentCart = null;
            var product = new Product { Name = "Laptop", Price = 1200.00m };
            int quantity = 1;
            int userId = 1;

            // Act
            var updatedCart = _cartSystem.addProduct(currentCart, product, quantity, userId);

            // Assert
            Assert.NotNull(updatedCart); // xUnit uses Assert.NotNull
            Assert.Single(updatedCart); // A more specific assertion for a single item list

            var cartItem = updatedCart.First();
            Assert.Equal("Laptop", cartItem.ProductName); // xUnit uses Assert.Equal
            Assert.Equal(1, cartItem.Quantity);
            Assert.Equal(1200.00m, cartItem.Price);
            Assert.Equal(1, cartItem.UserId);
        }

        [Fact]
        public void AddProduct_Should_AddNewItem_To_ExistingCart()
        {
            // Arrange
            var product1 = new Product { Name = "Laptop", Price = 1200.00m };
            var initialCart = new List<Cart>
            {
                new Cart { ProductName = "Mouse", Quantity = 1, Price = 25.00m, UserId = 1 }
            };
            int quantity = 1;
            int userId = 1;

            // Act
            var updatedCart = _cartSystem.addProduct(initialCart, product1, quantity, userId);

            // Assert
            Assert.Equal(2, updatedCart.Count); // xUnit uses Assert.Equal
            var newItem = updatedCart.FirstOrDefault(p => p.ProductName == "Laptop");
            Assert.NotNull(newItem);
            Assert.Equal(1, newItem.Quantity);
        }

        [Fact]
        public void AddProduct_Should_UpdateQuantity_Of_ExistingItem()
        {
            // Arrange
            var product = new Product { Name = "Laptop", Price = 1200.00m };
            var initialCart = new List<Cart>
            {
                new Cart { ProductName = "Laptop", Quantity = 1, Price = 1200.00m, UserId = 1 }
            };
            int quantityToAdd = 2;
            int userId = 1;

            // Act
            var updatedCart = _cartSystem.addProduct(initialCart, product, quantityToAdd, userId);

            // Assert
            Assert.Single(updatedCart); // Assert that the list still contains only one item
            
            var updatedItem = updatedCart.First();
            Assert.Equal(3, updatedItem.Quantity); // xUnit uses Assert.Equal
            // Note: Your current logic uses += on TotalPrice.
            // 1200 + (1200 * 2) = 3600
            Assert.Equal(3600.00m, updatedItem.Price);
        }
    }
}