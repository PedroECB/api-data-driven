using Microsoft.EntityFrameworkCore;
using ApiDataDriven.Models;

namespace ApiDataDriven.Data
{
    public class DataContext : DbContext
    {
        //Microsoft.EntityFrameworkCore.InMemory --version 3.0.0
        //Npgsql.EntityFrameworkCore.PostgreSQL --version 7.0.0
        //dotnet add package EFCore.NamingConventions --version 1.1.0

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSnakeCaseNamingConvention();
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Product { get; set; }

    }
}