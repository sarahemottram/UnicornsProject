using Microsoft.AspNetCore.Mvc;
using UnicornsInventorySystem.Database;
using UnicornsInventorySystem.Entities;

namespace UnicornsInventorySystem.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriesController : ControllerBase
{

    [HttpPost(Name = "AddCategory")]
    public int AddCategory([FromServices] DatabaseContext context, Category category)
    {
        throw new NotImplementedException();
    }

    [HttpGet(Name = "GetAllCategories")]
    public List<Category> GetAllCategories([FromServices] DatabaseContext context)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id}", Name = "GetCategory")]
    public Category? GetCategory([FromServices] DatabaseContext context, int id)
    {
        throw new NotImplementedException();
    }

    [HttpPut(Name = "UpdateCategory")]
    public void UpdateCategory([FromServices] DatabaseContext context, Category category)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id}", Name = "DeleteCategory")]
    public void DeleteCategory([FromServices] DatabaseContext context, int id)
    {
        throw new NotImplementedException();
    }
}
