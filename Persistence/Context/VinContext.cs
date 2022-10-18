using Microsoft.EntityFrameworkCore;
using Persistence.Models;

namespace Persistence.Context
{
    public class VinContext : DbContext
    {
        public VinContext(DbContextOptions<VinContext> options) : base(options) { }

        public DbSet<Vin> Vins { get; set; } 

        public DbSet<Etagere> Etageres { get; set; } 
    }
}

