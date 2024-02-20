using Microsoft.AspNetCore.Mvc;
using UnicornsInventorySystem.Entities;

namespace UnicornsInventorySystem.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    [HttpPost(Name = "AddProduct")]
    public int AddProduct(Product product)
    {
        throw new NotImplementedException();
    }

    [HttpGet(Name = "GetAllProducts")]
    public List<Product> GetAllProducts()
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id}", Name = "GetProduct")]
    public Product? GetProduct(int id)
    {
        throw new NotImplementedException();
    }

    [HttpPut(Name = "UpdateProduct")]
    public void UpdateProduct(Product product)
    {

    }

    [HttpDelete("{id}", Name = "DeleteProduct")]
    public void DeleteProduct(int id)
    {

    }
}
