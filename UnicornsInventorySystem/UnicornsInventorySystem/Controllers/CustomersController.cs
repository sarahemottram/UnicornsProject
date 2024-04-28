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
        context.Customers.Add(Customer);
        return context.SaveChanges();   
            
    }

    [HttpGet(Name = "GetAllCustomers")]
    public List<Customer> GetAllCustomers([FromServices] DatabaseContext context)
    {
        return context.Customers.ToList();
    }

    [HttpGet("{id}", Name = "GetCustomer")]
    public Customer? GetCustomer([FromServices] DatabaseContext context, int id)
    {
        return context.Customers.FirstOrDefault(customer => customer.Id == id);
    }

    [HttpPut(Name = "UpdateCustomer")]
    public void UpdateCustomer([FromServices] DatabaseContext context, Customer Customer)
    {
        context.Customers.Update(Customer);
        context.SaveChanges();
    }

    [HttpDelete("{id}", Name = "DeleteCustomer")]
    public void DeleteCustomer([FromServices] DatabaseContext context, int id)
    {
        var existingCustomer = context.Customers.FirstOrDefault(c => c.Id == id);
            if (existingCustomer != null)
            {
                context.Customers.Remove(existingCustomer);
                context.SaveChanges();
            }    
    }
}
