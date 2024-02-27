using UnicornsInventorySystem.Controllers;
using UnicornsInventorySystem.Database;
using UnicornsInventorySystem.Entities;

namespace UnicornsInventorySystemTests;

public class Categories_Should
{
    private static (Mock<DatabaseContext> mockDatabaseContext, List<Category> categories) GetMockDatabase()
    {
        var mockDatabaseContext = new Mock<DatabaseContext>();
        var categories = new List<Category>()
        {
            new Category()
            {
                Id = 1,
                Name = "Chips",
                Description = ""
            },
            new Category()
            {
                Id = 2,
                Name = "Drinks",
                Description = ""
            }
        };
        mockDatabaseContext.Setup(context => context.Categories).ReturnsDbSet(categories);
        return (mockDatabaseContext, categories);
    }

    [Fact]
    public void AddCategory_Should_DatabaseAddCategory()
    {
        //Arrange
        var (mockDatabaseContext, categories) = GetMockDatabase();
        var systemUnderText = new CategoriesController();
        var newCategory = new Category()
        {
            Id = 3,
            Name = "Pastries",
            Description = ""
        };

        //Act
        var result = systemUnderText.AddCategory(mockDatabaseContext.Object, newCategory);

        //Assert
        mockDatabaseContext.Verify(context => context.Categories.Add(It.IsAny<Category>()), Times.Once);
        mockDatabaseContext.Verify(context => context.SaveChanges(), Times.Once);
    }


    [Fact]
    public void GetAllCategories_Should_ReturnAllCategories()
    {
        //Act
        var (mockDatabaseContext, categories) = GetMockDatabase();
        var systemUnderText = new CategoriesController();
        var result = systemUnderText.GetAllCategories(mockDatabaseContext.Object);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(categories.Count, result.Count);
        Assert.Equal(categories, result);
    }

    [Fact]
    public void GetCategory_Should_ReturnCategory_WhenItExists()
    {
        //Arrange
        var (mockDatabaseContext, categories) = GetMockDatabase();
        var category = categories.First();
        var systemUnderTest = new CategoriesController();

        //Act
        var result = systemUnderTest.GetCategory(mockDatabaseContext.Object, category.Id);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(result.Id, category.Id);
    }

    [Fact]
    public void GetCategory_Should_ReturnNull_WhenItDoesNotExist()
    {
        //Arrange
        var (mockDatabaseContext, _) = GetMockDatabase();
        var categoryId = 7;
        var systemUnderTest = new CategoriesController();

        //Act
        var result = systemUnderTest.GetCategory(mockDatabaseContext.Object, categoryId);

        //Assert
        Assert.Null(result);
    }

    [Fact]
    public void UpdateCategory_Should_DatabaseUpdatecategory()
    {
        //Arrange
        var (mockDatabaseContext, categories) = GetMockDatabase();
        var systemUnderTest = new CategoriesController();

        var category = new Category
        {
            Id = categories.First().Id,
            Name = "Salty Snacks",
            Description = "Snacks that are salty in taste and not sweet"
        };

        //Act
        systemUnderTest.UpdateCategory(mockDatabaseContext.Object, category);

        //Assert
        mockDatabaseContext.Verify(context => context.Categories.Update(It.IsAny<Category>()), Times.Once);
        mockDatabaseContext.Verify(context => context.SaveChanges(), Times.Once);
    }

    [Fact]
    public void DeleteCategory_Should_DatabaseDeletecategory()
    {
        //Arrange
        var (mockDatabaseContext, categories) = GetMockDatabase();
        var systemUnderTest = new CategoriesController();

        var category = categories.First().Id;

        //Act
        systemUnderTest.DeleteCategory(mockDatabaseContext.Object, category);

        //Assert
        mockDatabaseContext.Verify(context => context.Categories.Remove(It.IsAny<Category>()), Times.Once);
        mockDatabaseContext.Verify(context => context.SaveChanges(), Times.Once);
    }
}