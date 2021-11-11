using Microsoft.EntityFrameworkCore;
using pfm.Database.Entities;

namespace pfm.Database
{
    public class TransactionDBContext : DbContext
    {
        public DbSet<TransactionEntity> Transactions {get; set;}
        public TransactionDBContext(){}

        public TransactionDBContext(DbContextOptions options) : base(options)
        {

        }
    }
}