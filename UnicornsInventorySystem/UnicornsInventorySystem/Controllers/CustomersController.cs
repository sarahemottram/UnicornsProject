using Microsoft.AspNetCore.Mvc;
using UnicornsInventorySystem.Database;
using UnicornsInventorySystem.Entities;

namespace UnicornsInventorySystem.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomersController : ControllerBase
{

    [HttpPost(Name = "AddCustomer")]
    public int AddCustomer([FromServices] DatabaseContext context, Customer Customer)
    {
        throw new NotImplementedException();
    }

    [HttpGet(Name = "GetAllCustomers")]
    public List<Customer> GetAllCustomers([FromServices] DatabaseContext context)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id}", Name = "GetCustomer")]
    public Customer? GetCustomer([FromServices] DatabaseContext context, int id)
    {
        throw new NotImplementedException();
    }

    [HttpPut(Name = "UpdateCustomer")]
    public void UpdateCustomer([FromServices] DatabaseContext context, Customer Customer)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id}", Name = "DeleteCustomer")]
    public void DeleteCustomer([FromServices] DatabaseContext context, int id)
    {
        throw new NotImplementedException();
    }
}
