using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.EntityFrameworkCore;
using UnicornsInventorySystem.Controllers;
using UnicornsInventorySystem.Database;
using UnicornsInventorySystem.Entities;

namespace UnicornsInventorySystemTests;

public class Products_Should
{

    private static (Mock<DatabaseContext> mockDatabaseContext, List<Product> products) GetMockDatabase()
    {
        var mockDatabaseContext = new Mock<DatabaseContext>();
        var products = new List<Product>()
        {
            new Product()
            {
                Id = 1,
                Name = "Cheeto",
                Price = 5,
                CategoryId = 1,
                QuantityInStock = 3,
                Sku = "1234"
            },
            new Product()
            {
                Id = 2,
                Name = "Mountain dew",
                Price = 6,
                CategoryId = 1,
                QuantityInStock = 9,
                Sku = "5678"
            }
        };
        mockDatabaseContext.Setup(context => context.Products).ReturnsDbSet(products);
        return (mockDatabaseContext, products);
    }

    [Fact]
    public void AddProduct_Should_DatabaseAddProduct()
    {
        //Arrange
        var (mockDatabaseContext, products) = GetMockDatabase();
        var systemUnderText = new ProductsController();
        var newProduct = new Product()
        {
            Name = "Dorito",
            CategoryId = 1,
            Price = 10,
            QuantityInStock = 10,
            Sku = "112233"
        };

        //Act
        var result = systemUnderText.AddProduct(mockDatabaseContext.Object, newProduct);

        //Assert
        mockDatabaseContext.Verify(context => context.Products.Add(It.IsAny<Product>()), Times.Once);
        mockDatabaseContext.Verify(context => context.SaveChanges(), Times.Once);
    }


    [Fact]
    public void GetAllProducts_Should_ReturnAllProducts()
    {
        //Act
        var (mockDatabaseContext, products) = GetMockDatabase();
        var systemUnderText = new ProductsController();
        var result = systemUnderText.GetAllProducts(mockDatabaseContext.Object);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(products.Count, result.Count);
        Assert.Equal(products, result);
    }

    [Fact]
    public void GetProduct_Should_ReturnProduct_WhenItExists()
    {
        //Arrange
        var (mockDatabaseContext, products) = GetMockDatabase();
        var product = products.First();
        var systemUnderTest = new ProductsController();

        //Act
        var result = systemUnderTest.GetProduct(mockDatabaseContext.Object, product.Id);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(result.Id, product.Id);
    }

    [Fact]
    public void GetProduct_Should_ReturnNull_WhenItDoesNotExist()
    {
        //Arrange
        var (mockDatabaseContext, _) = GetMockDatabase();
        var productId = 69;
        var systemUnderTest = new ProductsController();

        //Act
        var result = systemUnderTest.GetProduct(mockDatabaseContext.Object, productId);

        //Assert
        Assert.Null(result);
    }

    [Fact]
    public void UpdateProduct_Should_DatabaseUpdateProduct()
    {
        //Arrange
        //Arrange
        var (mockDatabaseContext, products) = GetMockDatabase();
        var systemUnderTest = new ProductsController();

        var product = new Product
        {
            Id = products.First().Id,
            Name = "Coke",
            Price = 3,
            CategoryId = 1,
            QuantityInStock = 7,
            Sku = "9112"
        };

        //Act
        systemUnderTest.UpdateProduct(mockDatabaseContext.Object, product);

        //Assert
        mockDatabaseContext.Verify(context => context.Products.Update(It.IsAny<Product>()), Times.Once);
        mockDatabaseContext.Verify(context => context.SaveChanges(), Times.Once);
    }

    [Fact]
    public void DeleteProduct_Should_DatabaseDeleteProduct()
    {
        //Arrange
        var (mockDatabaseContext, products) = GetMockDatabase();
        var systemUnderTest = new ProductsController();

        var product = products.First().Id;

        //Act
        systemUnderTest.DeleteProduct(mockDatabaseContext.Object, product);

        //Assert
        mockDatabaseContext.Verify(context => context.Products.Remove(It.IsAny<Product>()), Times.Once);
        mockDatabaseContext.Verify(context => context.SaveChanges(), Times.Once);
    }
}