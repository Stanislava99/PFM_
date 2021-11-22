using pfm.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace pfm.Database
{
    public class pfm_databaseContext : DbContext
    {
        public pfm_databaseContext(DbContextOptions<pfm_databaseContext> options) : base(options) {
            this.Database.SetCommandTimeout(90);
        }
        // Db Set 
        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<MccCodes> MccCodes { get; set; }
        public DbSet<SplitTransactions> SplitTransactions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(pfm_databaseContext).Assembly);
        }
    }
}