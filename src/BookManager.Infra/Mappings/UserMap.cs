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

        builder.OwnsOne(u => u.Address, ConfigureAddress);
    }

    private static void ConfigureAddress<T>(OwnedNavigationBuilder<T, Address> addressBuilder) where T : class
    {
            addressBuilder.Property(x => x.Street).HasColumnName("Street");
            addressBuilder.Property(x => x.Number).HasColumnName("Number");
            addressBuilder.Property(x => x.Neighborhood).HasColumnName("Neighborhood");
            addressBuilder.Property(x => x.City).HasColumnName("City");
            addressBuilder.Property(x => x.ZipCode).HasColumnName("ZipCode");
    }
}
