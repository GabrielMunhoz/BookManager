using BookManager.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookManager.Infra.Mappings;

public class BookMap : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.Property(b => b.Id).IsRequired();
        builder.Property(b => b.Title).IsRequired();
        builder.Property(b => b.Autor).IsRequired();
        builder.Property(b => b.ISBN).IsRequired();
        builder.Property(b => b.ReleaseYear).IsRequired();
    }
}
