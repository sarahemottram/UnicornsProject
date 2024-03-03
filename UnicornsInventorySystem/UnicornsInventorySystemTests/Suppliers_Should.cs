using UnicornsInventorySystem.Controllers;
using UnicornsInventorySystem.Database;
using UnicornsInventorySystem.Entities;

namespace UnicornsInventorySystemTests;

public class Suppliers_Should
{
    private static (Mock<DatabaseContext> mockDatabaseContext, List<Supplier> suppliers) GetMockDatabase()
    {
        var mockDatabaseContext = new Mock<DatabaseContext>();
        var suppliers = new List<Supplier>()
        {
            new Supplier()
            {
                Id = 1,
                Name = "Lays",
                Email = "sales@fritolays.com"
            },
            new Supplier()
            {
                Id = 2,
                Name = "Coca Cola",
                Email = "coke@cocacola.com"
            }
        };
        mockDatabaseContext.Setup(context => context.Suppliers).ReturnsDbSet(suppliers);
        return (mockDatabaseContext, suppliers);
    }

    [Fact]
    public void AddSupplier_Should_DatabaseAddsupplier()
    {
        //Arrange
        var (mockDatabaseContext, suppliers) = GetMockDatabase();
        var systemUnderText = new SuppliersController();
        var newSupplier = new Supplier()
        {
            Id = 3,
            Name = "McKee Foods",
            Email = "contact@mckeefoods.com"
        };

        //Act
        var result = systemUnderText.AddSupplier(mockDatabaseContext.Object, newSupplier);

        //Assert
        mockDatabaseContext.Verify(context => context.Suppliers.Add(It.IsAny<Supplier>()), Times.Once);
        mockDatabaseContext.Verify(context => context.SaveChanges(), Times.Once);
    }


    [Fact]
    public void GetAllSuppliers_Should_ReturnAllsuppliers()
    {
        //Act
        var (mockDatabaseContext, suppliers) = GetMockDatabase();
        var systemUnderText = new SuppliersController();
        var result = systemUnderText.GetAllSuppliers(mockDatabaseContext.Object);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(suppliers.Count, result.Count);
        Assert.Equal(suppliers, result);
    }

    [Fact]
    public void GetSupplier_Should_Returnsupplier_WhenItExists()
    {
        //Arrange
        var (mockDatabaseContext, suppliers) = GetMockDatabase();
        var supplier = suppliers.First();
        var systemUnderTest = new SuppliersController();

        //Act
        var result = systemUnderTest.GetSupplier(mockDatabaseContext.Object, supplier.Id);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(result.Id, supplier.Id);
    }

    [Fact]
    public void GetSupplier_Should_ReturnNull_WhenItDoesNotExist()
    {
        //Arrange
        var (mockDatabaseContext, _) = GetMockDatabase();
        var supplierId = 11;
        var systemUnderTest = new SuppliersController();

        //Act
        var result = systemUnderTest.GetSupplier(mockDatabaseContext.Object, supplierId);

        //Assert
        Assert.Null(result);
    }

    [Fact]
    public void UpdateSupplier_Should_DatabaseUpdatesupplier()
    {
        //Arrange
        var (mockDatabaseContext, suppliers) = GetMockDatabase();
        var systemUnderTest = new SuppliersController();

        var supplier = new Supplier
        {
            Id = suppliers.First().Id,
            Name = "Frito Lays",
            Email = "sales@fritolays.com"
        };

        //Act
        systemUnderTest.UpdateSupplier(mockDatabaseContext.Object, supplier);

        //Assert
        mockDatabaseContext.Verify(context => context.Suppliers.Update(It.IsAny<Supplier>()), Times.Once);
        mockDatabaseContext.Verify(context => context.SaveChanges(), Times.Once);
    }

    [Fact]
    public void DeleteSupplier_Should_DatabaseDeletesupplier()
    {
        //Arrange
        var (mockDatabaseContext, suppliers) = GetMockDatabase();
        var systemUnderTest = new SuppliersController();

        var supplier = suppliers.First().Id;

        //Act
        systemUnderTest.DeleteSupplier(mockDatabaseContext.Object, supplier);

        //Assert
        mockDatabaseContext.Verify(context => context.Suppliers.Remove(It.IsAny<Supplier>()), Times.Once);
        mockDatabaseContext.Verify(context => context.SaveChanges(), Times.Once);
    }
}