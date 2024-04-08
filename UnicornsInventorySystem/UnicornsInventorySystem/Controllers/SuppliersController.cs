using Microsoft.AspNetCore.Mvc;
using UnicornsInventorySystem.Database;
using UnicornsInventorySystem.Entities;

namespace UnicornsInventorySystem.Controllers;

[ApiController]
[Route("[controller]")]
public class SuppliersController : ControllerBase
{

    [HttpPost(Name = "AddSupplier")]
    public int AddSupplier([FromServices] DatabaseContext context, Supplier Supplier)
    {
        context.Suppliers.Add(Supplier);
        return context.SaveChanges();
    }

    [HttpGet(Name = "GetAllSuppliers")]
    public List<Supplier> GetAllSuppliers([FromServices] DatabaseContext context)
    {
        return context.Suppliers.ToList();
    }

    [HttpGet("{id}", Name = "GetSupplier")]
    public Supplier? GetSupplier([FromServices] DatabaseContext context, int id)
    {
        return context.Suppliers.FirstOrDefault(Supplier => Supplier.Id == id);
    }

    [HttpPut(Name = "UpdateSupplier")]
    public void UpdateSupplier([FromServices] DatabaseContext context, Supplier Supplier)
    {
        context.Suppliers.Update(Supplier);
        context.SaveChanges();
    }

    [HttpDelete("{id}", Name = "DeleteSupplier")]
    public void DeleteSupplier([FromServices] DatabaseContext context, int id)
    {
        var existingSupplier = context.Suppliers.FirstOrDefault(s => s.Id == id);
        if (existingSupplier != null)
        {
            context.Suppliers.Remove(existingSupplier);
            context.SaveChanges();
        }
    }
}
