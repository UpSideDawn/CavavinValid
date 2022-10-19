using Microsoft.EntityFrameworkCore;
using Persistence.Models;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace Persistence.Context
{
    public class VinContext : DbContext
    {
        public VinContext(DbContextOptions<VinContext> options) : base(options) { }

        public DbSet<Vin> Vins { get; set; }

        public DbSet<Etagere> Etageres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Etagere>()
                .HasMany(e => e.Vins)
                .WithOne(v => v.Etagere);

        }
    }
}

