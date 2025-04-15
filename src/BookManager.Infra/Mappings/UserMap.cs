using BookManager.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookManager.Infra.Mappings;

public class UserMap : IEntityTypeConfiguration<Users>
{
    public void Configure(EntityTypeBuilder<Users> builder)
    {
        builder.Property(u => u.Id).IsRequired();
        builder.Property(u => u.Name).IsRequired();
        builder.Property(u => u.Email).IsRequired();
    }
}
