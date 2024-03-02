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
        throw new NotImplementedException();
    }

    [HttpGet(Name = "GetAllTransactions")]
    public List<Transaction> GetAllTransactions([FromServices] DatabaseContext context)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id}", Name = "GetTransaction")]
    public Transaction? GetTransaction([FromServices] DatabaseContext context, int id)
    {
        throw new NotImplementedException();
    }

    [HttpGet("ByCustomer/{customerid}", Name = "GetAllUserTransactions")]
    public List<Transaction> GetAllUserTransactions([FromServices] DatabaseContext context, int customerId)
    {
        throw new NotImplementedException();
    }

    [HttpGet("ByProduct/{productid}", Name = "GetAllProductTransactions")]
    public List<Transaction> GetAllProductTransactions([FromServices] DatabaseContext context, int productId)
    {
        throw new NotImplementedException();
    }
}
