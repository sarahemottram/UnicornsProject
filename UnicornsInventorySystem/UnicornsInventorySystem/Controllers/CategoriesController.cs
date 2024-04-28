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
        context.Categories.Add(category);
        return context.SaveChanges();    
    }

    [HttpGet(Name = "GetAllCategories")]
    public List<Category> GetAllCategories([FromServices] DatabaseContext context)
    {
        return context.Categories.ToList();
    }

    [HttpGet("{id}", Name = "GetCategory")]
    public Category? GetCategory([FromServices] DatabaseContext context, int id)
    {
        return context.Categories.Where(category => category.Id == id).FirstOrDefault();
    }

    [HttpPut(Name = "UpdateCategory")]
    public void UpdateCategory([FromServices] DatabaseContext context, Category category)
    {
       context.Categories.Update(category);
        context.SaveChanges();
    }

    [HttpDelete("{id}", Name = "DeleteCategory")]
    public void DeleteCategory([FromServices] DatabaseContext context, int id)
    {
        var existingCategory = context.Categories.FirstOrDefault(c => c.Id == id);
            if (existingCategory != null)
            {
                context.Categories.Remove(existingCategory);
                context.SaveChanges();
            }    
    }
}
