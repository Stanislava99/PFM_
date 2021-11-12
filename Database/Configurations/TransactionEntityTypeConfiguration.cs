using Microsoft.EntityFrameworkCore;
using pfm.Database.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace pfm.Database.Configurations
{
    public class TransactionEntityTypeConfiguration : IEntityTypeConfiguration<TransactionEntity>
    {
        public void Configure(EntityTypeBuilder<TransactionEntity> builder)
        {
            builder.ToTable("transactions");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.BeneficiaryName).HasColumnName("");
        }
    }
}