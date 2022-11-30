using Microsoft.EntityFrameworkCore;
using ApiDataDriven.Models;

namespace ApiDataDriven.Data
{
    public class DataContext : DbContext
    {
        // Microsoft.EntityFrameworkCore.InMemory --version 3.0.0
        // Npgsql.EntityFrameworkCore.PostgreSQL --version 3.1.11
        // dotnet add package EFCore.NamingConventions --version 1.1.0
        // dotnet tool install --global dotnet-ef  => dotnet add packacge Microsoft.EntityFrameworkCore.Design
        // dotnet ef database update (Criar a migração inicial de banco)
        // dotnet add package Microsoft.AspNetCore.Authentication
        // dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
        // dotnet add package Swashbuckle.AspNetCore -v 5.0.0-rc4

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSnakeCaseNamingConvention();
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<User> User { get; set; }


    }
}