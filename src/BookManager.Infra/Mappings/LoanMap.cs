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
        
        builder.HasMany(l => l.Books)
            .WithMany(b => b.Loans)
            .UsingEntity<Dictionary<string, object>>(
                   "LoanBooks",
                   j => j.HasOne<Book>().WithMany().HasForeignKey("BookId"),
                   j => j.HasOne<Loan>().WithMany().HasForeignKey("LoanId"),
                   j => j.HasKey("LoanId", "BookId") // Composite key
               );

    }
}
