using Moq;
using Shared.Models;
using Shared.Interfaces;
using Shared.Services;
using Shared.Enums;
using Newtonsoft.Json;

namespace Shared.Tests.UnitTests;

public class ProductService_Tests
{
    private readonly Mock<IFileService> _mockFileService;
    private readonly IProductService _productService;

    public ProductService_Tests()
    {
        _mockFileService = new Mock<IFileService>();
        _productService = new ProductService(_mockFileService.Object);
    }

    [Fact]
    public void CreateProduct__ShouldReturnSuccess__WhenProductIsCreated()
    {
        // Arrange
        var product = new Product { Id = Guid.NewGuid().ToString(), Name = "Socker", Price = 22 };

        // Act
        StatusCodes result = _productService.CreateProduct(product);
        var products = _productService.GetAllProductsFromList();

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
        var list = new List<Product> { updatedProduct };
        var json = JsonConvert.SerializeObject(list);

        _mockFileService.Setup(x => x.LoadFromFile()).Returns(json);

        //Act
        StatusCodes result = _productService.CreateProduct(product);
        var products = _productService.GetAllProductsFromList();
        StatusCodes updateResult = _productService.Update(updatedProduct);
        
        // Assert
        Assert.Equal(StatusCodes.Success, updateResult);
        
    }

    [Fact]
    public void GetAllProductsFromList__ShouldReturnListOfProducts()
    {
        // Arrange
        var product = new Product { Id = Guid.NewGuid().ToString(), Name = "Socker", Price = 22 };
        var productList = new List<Product> { product };

        // Act
        var result = _productService.GetAllProductsFromList();

        // Assert
        // Assert.True(result);
    }


}
