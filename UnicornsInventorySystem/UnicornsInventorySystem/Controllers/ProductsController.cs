using Microsoft.AspNetCore.Mvc;
using UnicornsInventorySystem.Database;
using UnicornsInventorySystem.Entities;

namespace UnicornsInventorySystem.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{

    [HttpPost(Name = "AddProduct")]
    public int AddProduct([FromServices] DatabaseContext context, Product product)
    {
        throw new NotImplementedException();
    }

    [HttpGet(Name = "GetAllProducts")]
    public List<Product> GetAllProducts([FromServices] DatabaseContext context)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id}", Name = "GetProduct")]
    public Product? GetProduct([FromServices] DatabaseContext context, int id)
    {
        throw new NotImplementedException();
    }

    [HttpPut(Name = "UpdateProduct")]
    public void UpdateProduct([FromServices] DatabaseContext context, Product product)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id}", Name = "DeleteProduct")]
    public void DeleteProduct([FromServices] DatabaseContext context, int id)
    {
        throw new NotImplementedException();
    }
}
