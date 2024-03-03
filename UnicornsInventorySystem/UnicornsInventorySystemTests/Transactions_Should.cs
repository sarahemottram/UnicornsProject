using Moq;
using Moq.EntityFrameworkCore;
using UnicornsInventorySystem.Controllers;
using UnicornsInventorySystem.Database;
using UnicornsInventorySystem.Entities;

namespace UnicornsInventorySystemTests;

public class Transactions_Should
{
    //Mock Database for Testing
    private static (Mock<DatabaseContext> mockDatabaseContext, List<Transaction> transactions, List<Product> products, List<Customer> customers) GetMockDatabase()
    {
        var mockDatabaseContext = new Mock<DatabaseContext>();
        var transactions = new List<Transaction>()
        {
            new Transaction()
            {
                Id = 1,
                ProductId = 1,
                CustomerId = 1,
                Price = 50,
                Date = DateTime.Now,
                Type = TransactionType.Sale
            },
            new Transaction()
            {
                Id = 2,
                ProductId = 1,
                CustomerId = 1,
                Price = 50,
                Date = DateTime.Now,
                Type = TransactionType.Return
            },
            new Transaction()
            {
                Id = 3,
                ProductId = 1,
                CustomerId = 2,
                Price = 5,
                Date = DateTime.Now,
                Type = TransactionType.Sale
            },
            new Transaction()
            {
                Id = 4,
                ProductId = 2,
                CustomerId = 2,
                Price = 15,
                Date = DateTime.Now,
                Type = TransactionType.Sale
            },
        };

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
            },
            new Product()
            {
                Id = 3,
                Name = "PlayStation 5",
                Price = 600,
                CategoryId = 3,
                QuantityInStock = 0,
                Sku = "1011"
            }
        };

        var customers = new List<Customer>()
        {
            new Customer()
            {
                Id = 1,
                Name = "Shinji",
                Email = "getintherobot@NERV.com"
            },
            new Customer()
            {
                Id = 2,
                Name = "Asuka",
                Email = "ningyojanai@NERV.com"
            }
        };

        mockDatabaseContext.Setup(context => context.Transactions).ReturnsDbSet(transactions);
        mockDatabaseContext.Setup(context => context.Products).ReturnsDbSet(products);
        mockDatabaseContext.Setup(context => context.Customers).ReturnsDbSet(customers);
        return (mockDatabaseContext, transactions, products, customers);
    }

    [Fact]
    public void CreateTransaction_Should_CreateValidTransaction()
    {
        //Arange
        var (mockDatabaseContext, _, _, _) = GetMockDatabase();
        var systemUnderTest = new TransactionController();

        //Act
        var result = systemUnderTest.CreateTransaction(mockDatabaseContext.Object, 1, 1, TransactionType.Sale);

        //Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void CreateTransaction_Should_GetExistingCustomer()
    {
        //Arange
        var (mockDatabaseContext, _, _, _) = GetMockDatabase();
        var systemUnderTest = new TransactionController();
        var customerId = 1;
        var productId = 1;

        //Act
        var result = systemUnderTest.CreateTransaction(mockDatabaseContext.Object, customerId, productId, TransactionType.Sale);

        //Assert
        Assert.NotNull(result);
        mockDatabaseContext.Verify(context => context.Customers.Find(It.IsAny<int>()), Times.Once);
    }

    [Fact]
    public void CreateTransaction_Should_ThrowException_IfCustomerDoesntExist()
    {
        //Arange
        var (mockDatabaseContext, _, _, _) = GetMockDatabase();
        var systemUnderTest = new TransactionController();
        var customerId = 420;

        //Act
        var exception = Record.Exception(() => systemUnderTest.CreateTransaction(mockDatabaseContext.Object, customerId, 1, TransactionType.Sale));

        //Assert
        Assert.NotNull(exception);
        Assert.True(exception.Message.Contains("Customer does not exist", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void CreateTransaction_Should_GetExistingProduct()
    {
        //Arange
        var (mockDatabaseContext, _, _, _) = GetMockDatabase();
        var systemUnderTest = new TransactionController();
        var customerId = 1;
        var productId = 1;

        //Act
        var result = systemUnderTest.CreateTransaction(mockDatabaseContext.Object, customerId, productId, TransactionType.Sale);

        //Assert
        Assert.NotNull(result);
        mockDatabaseContext.Verify(context => context.Products.Find(It.IsAny<int>()), Times.Once);
    }

    [Fact]
    public void CreateTransaction_Should_ThrowException_IfProductDoesntExist()
    {
        //Arange
        var (mockDatabaseContext, _, _, _) = GetMockDatabase();
        var systemUnderTest = new TransactionController();
        var productId = 420;

        //Act
        var exception = Record.Exception(() => systemUnderTest.CreateTransaction(mockDatabaseContext.Object, 1, productId, TransactionType.Sale));

        //Assert
        Assert.NotNull(exception);
        Assert.True(exception.Message.Contains("Product does not exist", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void CreateTransaction_Should_RemoveFromProductInventoryOnSale()
    {
        //Arange
        var (mockDatabaseContext, _, products, _) = GetMockDatabase();
        var systemUnderTest = new TransactionController();
        var product = products.First();
        var productId = product.Id;
        var transactionType = TransactionType.Sale;
        var productInventory = product.QuantityInStock;

        //Act
        systemUnderTest.CreateTransaction(mockDatabaseContext.Object, 1, productId, transactionType);

        //Assert
        Assert.Equal(productInventory - 1, product.QuantityInStock);
        mockDatabaseContext.Verify(context => context.Products.Update(It.IsAny<Product>()), Times.Once);
        mockDatabaseContext.Verify(context => context.SaveChanges(), Times.Once);
    }

    [Fact]
    public void CreateTransaction_Should_AddToProductInventoryOnReturn()
    {
        //Arange
        var (mockDatabaseContext, _, products, _) = GetMockDatabase();
        var systemUnderTest = new TransactionController();
        var product = products.First();
        var productId = product.Id;
        var transactionType = TransactionType.Return;
        var productInventory = product.QuantityInStock;

        //Act
        systemUnderTest.CreateTransaction(mockDatabaseContext.Object, 1, productId, transactionType);

        //Assert
        Assert.Equal(productInventory + 1, product.QuantityInStock);
        mockDatabaseContext.Verify(context => context.Products.Update(It.IsAny<Product>()), Times.Once);
        mockDatabaseContext.Verify(context => context.SaveChanges(), Times.Once);
    }

    [Fact]
    public void CreateTransaction_Should_ThrowException_IfOutOfStockItemIsSold()
    {
        //Arange
        var (mockDatabaseContext, _, _, _) = GetMockDatabase();
        var systemUnderTest = new TransactionController();
        var outOfStockId = mockDatabaseContext.Object.Products.First(x => x.QuantityInStock == 0).Id;
        var transactionType = TransactionType.Sale;

        //Act
        var exception = Record.Exception(() => systemUnderTest.CreateTransaction(mockDatabaseContext.Object, 1, outOfStockId, transactionType));

        //Assert
        Assert.NotNull(exception);
        Assert.True(exception.Message.Contains("Product is out of stock", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void CreateTransaction_Should_GetCorrectPrice()
    {
        //Arange
        var (mockDatabaseContext, transactions, _, _) = GetMockDatabase();
        var systemUnderTest = new TransactionController();
        var customerId = 1;
        var productId = 1;
        var transactionType = TransactionType.Sale;
        var price = mockDatabaseContext.Object.Products.First().Price;

        //Act
        var result = systemUnderTest.CreateTransaction(mockDatabaseContext.Object, customerId, productId, transactionType).Price;

        //Assert
        Assert.Equal(price, result);
    }

    [Fact]
    public void CreateTransaction_Should_GetCorrectTime()
    {
        //Arange
        var (mockDatabaseContext, transactions, _, _) = GetMockDatabase();
        var systemUnderTest = new TransactionController();
        var customerId = 1;
        var productId = 1;
        var transactionType = TransactionType.Sale;
        var date = DateTime.Now;

        //Act
        var result = systemUnderTest.CreateTransaction(mockDatabaseContext.Object, customerId, productId, transactionType).Date;

        //Assert
        Assert.Equal(date, result, TimeSpan.FromSeconds(5));
    }

    [Fact]
    public void CreateTransaction_Should_DatabaseAddTransaction()
    {
        //Arrange
        var (mockDatabaseContext, transactions, _, _) = GetMockDatabase();
        var systemUnderTest = new TransactionController();

        //Act
        var result = systemUnderTest.CreateTransaction(mockDatabaseContext.Object, 1, 1, TransactionType.Sale);

        //Assert
        mockDatabaseContext.Verify(context => context.Transactions.Add(It.IsAny<Transaction>()), Times.Once);
        mockDatabaseContext.Verify(context => context.SaveChanges(), Times.Once);
    }

    [Fact]
    public void GetAllTransactions_Should_GetAllTransactions()
    {
        //Act
        var (mockDatabaseContext, transactions, _, _) = GetMockDatabase();
        var systemUnderTest = new TransactionController();
        var result = systemUnderTest.GetAllTransactions(mockDatabaseContext.Object);

        //Assert
        Assert.NotNull(result);
        Assert.Equal<Transaction>(transactions, result);
    }

    [Fact]
    public void GetTransaction_Should_GetTransaction_WhenItExists()
    {
        //Arrange
        var (mockDatabaseContext, transactions, _, _) = GetMockDatabase();
        var transaction = transactions.First();
        var systemUnderTest = new TransactionController();

        //Act
        var result = systemUnderTest.GetTransaction(mockDatabaseContext.Object, transaction.Id);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(transaction.Id, result.Id);
    }

    [Fact]
    public void GetTransaction_Should_ReturnNull_WhenItDoesNotExist()
    {
        //Arrange
        var (mockDatabaseContext, _, _, _) = GetMockDatabase();
        var transactionId = 69;
        var systemUnderTest = new TransactionController();

        //Act
        var result = systemUnderTest.GetTransaction(mockDatabaseContext.Object, transactionId);

        //Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetAllUserTransactions_Should_GetAllUserTransactions()
    {
        //Arrange
        var (mockDatabaseContext, transactions, _, _) = GetMockDatabase();
        var customerId = transactions.First().CustomerId;
        var systemUnderTest = new TransactionController();

        //Act
        var result = systemUnderTest.GetAllUserTransactions(mockDatabaseContext.Object, customerId);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(transactions.Where(t => t.CustomerId == customerId), result);
    }

    [Fact]
    public void GetAllProductTransactions_Should_GetAllProductTransactions()
    {
        //Arrange
        var (mockDatabaseContext, transactions, _, _) = GetMockDatabase();
        var productId = transactions.First().ProductId;
        var systemUnderTest = new TransactionController();

        //Act
        var result = systemUnderTest.GetAllProductTransactions(mockDatabaseContext.Object, productId);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(transactions.Where(t => t.ProductId == productId), result);
    }
}
