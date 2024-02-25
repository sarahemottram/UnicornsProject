using Microsoft.EntityFrameworkCore;
using UnicornsInventorySystem.Entities;

namespace UnicornsInventorySystem.Database;

public class DatabaseContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite("Data Source=database.db3");

    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Supplier> Suppliers { get; set; }
    public virtual DbSet<Transaction> Transactions { get; set; }
    public virtual DbSet<Customer> Customers { get; set; }
}