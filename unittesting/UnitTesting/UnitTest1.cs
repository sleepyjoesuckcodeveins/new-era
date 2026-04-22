using NewEra.Domain.Interface;
using NewEra.Domain.Models;
using NewEra.BLL;
using Moq;

namespace UnitTesting;

public class UnitTest1
{
    [Fact]
    public void SearchProduct_WithValidName_ReturnsMatchingProducts()
    {
        // Arrange
        var mockProductRepository = new Mock<IProduct>();
        mockProductRepository.Setup(repo => repo.getAllProducts()).Returns(new List<Product>
        {
            new Product { Id = 1, Name = "Apple", Price = 0.99m },
            new Product { Id = 2, Name = "Banana", Price = 0.59m },
            new Product { Id = 3, Name = "Orange", Price = 0.79m }
        });

        var productService = new NeweraProductService(mockProductRepository.Object);

        // Act
        var result = productService.SearchProduct("apple");

        // Assert
        Assert.Single(result);
        Assert.Equal("Apple", result[0].Name);
        System.Console.WriteLine($"Test passed: Found product - {result[0].Name} with price {result[0].Price}");
        
    }
}
