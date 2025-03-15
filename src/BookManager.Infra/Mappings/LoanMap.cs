using BookManager.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookManager.Infra.Mappings;

public class LoanMap : IEntityTypeConfiguration<Loan>
{
    public void Configure(EntityTypeBuilder<Loan> builder)
    {
        builder.Property(l => l.Id).IsRequired();
        builder.Property(l => l.LoanDate).IsRequired();

        builder.HasOne(l => l.UserBook).WithMany();
        builder.HasMany(l => l.Books).WithOne(); 
    }
}
