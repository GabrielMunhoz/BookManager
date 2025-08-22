using BookManager.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookManager.Infra.Mappings;

public class LoanMap : IEntityTypeConfiguration<Loan>
{
    public void Configure(EntityTypeBuilder<Loan> builder)
    {
        builder.Property(l => l.Id).IsRequired();
        builder.Property(l => l.ReturnDate).IsRequired();
        builder.HasOne(l => l.User).WithMany();
        builder.Property(l => l.Status).HasConversion<int>().IsRequired();

        builder.HasMany(l => l.Books).WithOne();
        builder.HasOne(l => l.User).WithMany(); 
    }
}
