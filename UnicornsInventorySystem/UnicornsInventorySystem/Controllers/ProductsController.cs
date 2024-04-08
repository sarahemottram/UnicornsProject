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
        context.Products.Add(product);
        return context.SaveChanges();
    }

    [HttpGet(Name = "GetAllProducts")]
    public List<Product> GetAllProducts([FromServices] DatabaseContext context)
    {
        return context.Products.ToList();
    }

    [HttpGet("{id}", Name = "GetProduct")]
    public Product? GetProduct([FromServices] DatabaseContext context, int id)
    {
        return context.Products.FirstOrDefault(product => product.Id == id);
    }

    [HttpPut(Name = "UpdateProduct")]
    public void UpdateProduct([FromServices] DatabaseContext context, Product product)
    {
         context.Products.Update(product);
        context.SaveChanges();
    }

    [HttpDelete("{id}", Name = "DeleteProduct")]
    public void DeleteProduct([FromServices] DatabaseContext context, int id)
    {
        var existingProduct = context.Products.FirstOrDefault(p => p.Id == id);
        if (existingProduct != null)
        {
            context.Products.Remove(existingProduct);
            context.SaveChanges();
        }
    }
}
