using BookManager.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BookManager.Infra.Extensions;

public static class ModelBuilderConfig
{
    public static ModelBuilder ApplyGlobalConfiguration(this ModelBuilder builder)
    {
        foreach (IMutableEntityType entityType in builder.Model.GetEntityTypes())
        {
            foreach (IMutableProperty property in entityType.GetProperties())
            {
                switch (property.Name)
                {
                    case nameof(BaseEntity.Id):
                        property.IsKey();
                        break;
                    case nameof(BaseEntity.UpdateDate):
                        property.IsNullable = true;
                        break;
                    case nameof(BaseEntity.CreateDate):
                        property.IsNullable = false;
                        break;
                    default:
                        break;
                }
            }
        }
        return builder;
    }

    public static ModelBuilder SeedData(this ModelBuilder builder)
    {
        builder.Entity<UserBook>().HasData(new UserBook
        {
            Id = Guid.Parse("d0f606a2-622c-46b8-a844-ae0e817b1839"),
            Name = "Gabriel Munhoz",
            //Password = "2E6F9B0D5885B6010F9167787445617F553A735F",//teste
            Email = "gabrielmunhoz@bookmanager.com",
            CreateDate = new DateTime(2025, 9, 13),
            UpdateDate = null
        });

        return builder;
    }

}
