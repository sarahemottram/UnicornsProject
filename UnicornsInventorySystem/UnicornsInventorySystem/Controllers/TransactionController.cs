using Microsoft.AspNetCore.Mvc;
using UnicornsInventorySystem.Database;
using UnicornsInventorySystem.Entities;

namespace UnicornsInventorySystem.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionController : ControllerBase
{

    [HttpPost(Name = "AddTransaction")]
    public int AddTransaction([FromServices] DatabaseContext context, Transaction transaction)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id}", Name = "GetTransaction")]
    public Transaction? GetTransaction([FromServices] DatabaseContext context, int id)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{customerid}", Name = "GetAllUserTransactions")]
    public List<Transaction> GetAllUserTransactions([FromServices] DatabaseContext context, int customerId)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{productid}", Name = "GetAllProductTransactions")]
    public List<Transaction> GetAllProductTransactions([FromServices] DatabaseContext context, int prodcutId)
    {
        throw new NotImplementedException();
    }
}
