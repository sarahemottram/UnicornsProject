using Moq;
using Moq.EntityFrameworkCore;
using UnicornsInventorySystem.Controllers;
using UnicornsInventorySystem.Database;
using UnicornsInventorySystem.Entities;

namespace UnicornsInventorySystemTests;

public class Customers_Should
{
    private static (Mock<DatabaseContext> mockDatabaseContext, List<Customer> customers) GetMockDatabase()
    {
        var mockDatabaseContext = new Mock<DatabaseContext>();
        var customers = new List<Customer>()
        {
            new Customer()
            {
                Id = 1,
                Name = "Taylor Lightbourne",
                Email = "taylorlightbourne@mail.com"
            },
            new Customer()
            {
                Id = 2,
                Name = "Sarah Mottram",
                Email = "sarahmottram@mail.com"
            }
        };
        mockDatabaseContext.Setup(context => context.Customers).ReturnsDbSet(customers);
        return (mockDatabaseContext, customers);
    }

    [Fact]
    public void AddCustomer_Should_DatabaseAddcustomer()
    {
        //Arrange
        var (mockDatabaseContext, customers) = GetMockDatabase();
        var systemUnderText = new CustomersController();
        var newCustomer = new Customer()
        {
            Id = 3,
            Name = "John Smith",
            Email = "johnsmith@mail.com"
        };

        //Act
        var result = systemUnderText.AddCustomer(mockDatabaseContext.Object, newCustomer);

        //Assert
        mockDatabaseContext.Verify(context => context.Customers.Add(It.IsAny<Customer>()), Times.Once);
        mockDatabaseContext.Verify(context => context.SaveChanges(), Times.Once);
    }


    [Fact]
    public void GetAllCustomers_Should_ReturnAllcustomers()
    {
        //Act
        var (mockDatabaseContext, customers) = GetMockDatabase();
        var systemUnderText = new CustomersController();
        var result = systemUnderText.GetAllCustomers(mockDatabaseContext.Object);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(customers.Count, result.Count);
        Assert.Equal(customers, result);
    }

    [Fact]
    public void GetCustomer_Should_Returncustomer_WhenItExists()
    {
        //Arrange
        var (mockDatabaseContext, customers) = GetMockDatabase();
        var customer = customers.First();
        var systemUnderTest = new CustomersController();

        //Act
        var result = systemUnderTest.GetCustomer(mockDatabaseContext.Object, customer.Id);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(result.Id, customer.Id);
    }

    [Fact]
    public void GetCustomer_Should_ReturnNull_WhenItDoesNotExist()
    {
        //Arrange
        var (mockDatabaseContext, _) = GetMockDatabase();
        var customerId = 10;
        var systemUnderTest = new CustomersController();

        //Act
        var result = systemUnderTest.GetCustomer(mockDatabaseContext.Object, customerId);

        //Assert
        Assert.Null(result);
    }

    [Fact]
    public void UpdateCustomer_Should_DatabaseUpdateCustomer()
    {
        //Arrange
        var (mockDatabaseContext, customers) = GetMockDatabase();
        var systemUnderTest = new CustomersController();

        var customer = new Customer
        {
            Id = customers.First().Id,
            Name = "Taylor Lightbourne",
            Email = "tlightb1@student.com"
        };

        //Act
        systemUnderTest.UpdateCustomer(mockDatabaseContext.Object, customer);

        //Assert
        mockDatabaseContext.Verify(context => context.Customers.Update(It.IsAny<Customer>()), Times.Once);
        mockDatabaseContext.Verify(context => context.SaveChanges(), Times.Once);
    }

    [Fact]
    public void DeleteCustomer_Should_DatabaseDeletecustomer()
    {
        //Arrange
        var (mockDatabaseContext, customers) = GetMockDatabase();
        var systemUnderTest = new CustomersController();

        var customer = customers.First().Id;

        //Act
        systemUnderTest.DeleteCustomer(mockDatabaseContext.Object, customer);

        //Assert
        mockDatabaseContext.Verify(context => context.Customers.Remove(It.IsAny<Customer>()), Times.Once);
        mockDatabaseContext.Verify(context => context.SaveChanges(), Times.Once);
    }
}