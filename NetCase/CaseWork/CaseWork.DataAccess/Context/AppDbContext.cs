using Microsoft.EntityFrameworkCore;
using CaseWork.Entities;
using System.Threading;
using CaseWork.Entities.Models;

namespace CaseWork.DataAccess.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Carrier> Carriers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CarrierConfiguration> CarrierConfigurations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
        }
    }
}
