using Microsoft.EntityFrameworkCore;
namespace ProductCategories.Models{
    public class MyContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public MyContext(DbContextOptions options) : base(options) { }
        public DbSet<Product> products {get;set;}
        public DbSet<Categoria> categories {get;set;}
        public DbSet<Association> associations{get;set;}
    }
}