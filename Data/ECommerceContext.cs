using EcommerceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Data
{
  public class ECommerceContext : DbContext
  {
    public ECommerceContext(DbContextOptions<ECommerceContext> options) : base(options){}
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
  }
}