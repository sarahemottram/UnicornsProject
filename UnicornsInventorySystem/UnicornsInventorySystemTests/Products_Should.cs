using UnicornsInventorySystem.Controllers;
using UnicornsInventorySystem.Entities;

namespace UnicornsInventorySystemTests;

public class Products_Should
{
    [Fact]
    public void Generate_Id_On_Create()
    {
        //Arrange
        var systemUnderTest = new ProductsController();
        var toAdd = new Product()
        {
            Id = -1,
            Name = "Cheeto",
            CategoryId = 1,
            Price = 7.50M,
            QuantityInStock = 100,
            Sku = "SKUbydoo"
        };

        //Act
        var addedId = systemUnderTest.AddProduct(toAdd);

        //Assert
        Assert.NotEqual(toAdd.Id, addedId);
    }

    [Fact]
    public void Create_And_Load_Name()
    {
        //Arrange
        var systemUnderTest = new ProductsController();
        var toAdd = new Product()
        {
            Name = "Cheeto",
            CategoryId = 1,
            Price = 7.50M,
            QuantityInStock = 100,
            Sku = "SKUbydoo"
        };

        //Act
        var addedProductId = systemUnderTest.AddProduct(toAdd);
        var loaded = systemUnderTest.GetProduct(addedProductId);

        //Assert
        Assert.Equal(toAdd.Name, loaded.Name);
    }

    [Fact]
    public void Create_And_Load_Price()
    {
        //Arrange
        var systemUnderTest = new ProductsController();
        var toAdd = new Product()
        {
            Name = "Cheeto",
            CategoryId = 1,
            Price = 7.50M,
            QuantityInStock = 100,
            Sku = "SKUbydoo"
        };

        //Act
        var addedProductId = systemUnderTest.AddProduct(toAdd);
        var loaded = systemUnderTest.GetProduct(addedProductId);

        //Assert
        Assert.Equal(toAdd.Price, loaded.Price);
    }
}