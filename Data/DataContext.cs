using Microsoft.EntityFrameworkCore;
using ApiDataDriven.Models;

namespace ApiDataDriven.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
    }
}