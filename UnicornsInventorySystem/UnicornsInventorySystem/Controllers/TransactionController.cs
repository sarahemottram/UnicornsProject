using Microsoft.AspNetCore.Mvc;
using UnicornsInventorySystem.Database;
using UnicornsInventorySystem.Entities;

namespace UnicornsInventorySystem.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionController : ControllerBase
{
    [HttpPost(Name = "CreateTransaction")]
    public Transaction CreateTransaction([FromServices] DatabaseContext context, int customerid, int productid, TransactionType type)
    {
        context.Customers.Find(customerid); //Useless?
        var customer = context.Customers.FirstOrDefault(customer => customer.Id == customerid);
        if (customer == null)
        {
            throw new ArgumentException("Customer does not exist");
        }

        context.Products.Find(customerid); //Useless?
        var product = context.Products.FirstOrDefault(product => product.Id == productid);
        if (product == null)
        {
            throw new ArgumentException("Product does not exist");
        }

        if (type == TransactionType.Sale && product.QuantityInStock == 0)
        {
            throw new InvalidOperationException("Product is out of stock");
        }

        var transaction = new Transaction
        {
            CustomerId = customerid,
            ProductId = productid,
            Price = product.Price,
            Date = DateTime.Now,
            Type = type
        };

        if (type == TransactionType.Sale)
        {
            product.QuantityInStock--;
        }
        else if (type == TransactionType.Return)
        {
            product.QuantityInStock++;
        }

        context.Transactions.Add(transaction);
        context.Products.Update(product);
        context.SaveChanges();

        return transaction;
    }

    [HttpGet(Name = "GetAllTransactions")]
    public List<Transaction> GetAllTransactions([FromServices] DatabaseContext context)
    {
        return context.Transactions.ToList();
    }

    [HttpGet("{id}", Name = "GetTransaction")]
    public Transaction? GetTransaction([FromServices] DatabaseContext context, int id)
    {
        return context.Transactions.FirstOrDefault(Transaction => Transaction.Id == id);
    }

    [HttpGet("ByCustomer/{customerid}", Name = "GetAllUserTransactions")]
    public List<Transaction> GetAllUserTransactions([FromServices] DatabaseContext context, int customerId)
    {
        return context.Transactions.Where(Transaction => Transaction.CustomerId == customerId).ToList();
    }

    [HttpGet("ByProduct/{productid}", Name = "GetAllProductTransactions")]
    public List<Transaction> GetAllProductTransactions([FromServices] DatabaseContext context, int productId)
    {
        return context.Transactions.Where(Transaction => Transaction.ProductId == productId).ToList();
    }
}