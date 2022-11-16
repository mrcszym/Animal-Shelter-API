using AnimalShelter.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimalShelter.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Animal> Animals  { get; set; }
    }
}
