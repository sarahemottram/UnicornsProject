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
        throw new NotImplementedException();
    }

    [HttpGet(Name = "GetAllSuppliers")]
    public List<Supplier> GetAllSuppliers([FromServices] DatabaseContext context)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id}", Name = "GetSupplier")]
    public Supplier? GetSupplier([FromServices] DatabaseContext context, int id)
    {
        throw new NotImplementedException();
    }

    [HttpPut(Name = "UpdateSupplier")]
    public void UpdateSupplier([FromServices] DatabaseContext context, Supplier Supplier)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id}", Name = "DeleteSupplier")]
    public void DeleteSupplier([FromServices] DatabaseContext context, int id)
    {
        throw new NotImplementedException();
    }
}
