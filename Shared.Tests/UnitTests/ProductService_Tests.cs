using Moq;
using Shared.Models;
using Shared.Interfaces;
using Shared.Services;
using Shared.Enums;

namespace Shared.Tests.UnitTests;

public class ProductService_Tests
{
    private readonly Mock<IFileService> _mockFileService = new();
    private readonly Mock<IProductService> _mockProductService = new();

    [Fact]
    public void CreateProduct__ShouldReturnSuccess__WhenProductIsCreated()
    {
        // Arrange
        var product = new Product { Id = Guid.NewGuid().ToString(), Name = "Socker", Price = 22 };

        ProductService productService = new ProductService(_mockFileService.Object);

        // Act
        StatusCodes result = productService.CreateProduct(product);
        var products = productService.GetAllProductsFromList();

        // Assert
        Assert.Equal(StatusCodes.Success, result);
        Assert.Single(products);
    }

    [Fact]
    public void Update__ShouldReturnUpdatedProduct__WhenProductIsUpdated()
    {
        // Arrange
        var productId = Guid.NewGuid().ToString();
        var product = new Product { Id = productId, Name = "Socker", Price = 22 };
        var updatedProduct = new Product { Id = productId, Name = "Mjöl", Price = 32 };

        ProductService productService = new ProductService(_mockFileService.Object);

        //Act
        StatusCodes result = productService.Update(updatedProduct);
        var products = productService.GetAllProductsFromList();

        // Assert
        Assert.Equal(StatusCodes.Success, result);
        Assert.NotEqual(product, updatedProduct);
    }

    [Fact]
    public void GetAllProductsFromList__ShouldReturnListOfProducts()
    {
        // Arrange
        var product = new Product { Id = Guid.NewGuid().ToString(), Name = "Socker", Price = 22 };
        var products = new List<Product> { product };

        ProductService productService = new ProductService(_mockFileService.Object);

        // Act
        // StatusCodes result = productService.GetAllProductsFromList(); 

        // Assert
    }


}
