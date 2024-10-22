using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.Work.BooksCatalogApi.BLL.Entities;
using Test.Work.BooksCatalogApi.Database.Configurations.Common;
using Test.Work.BooksCatalogApi.Database.Extensions;

namespace Test.Work.BooksCatalogApi.Database.Configurations;

internal class AuthorConfiguration : EntityBaseConfiguration<Author>
{
    public override void ConfigureChild(EntityTypeBuilder<Author> builder)
    {
        builder.HasComment("Автор");

        builder.ConfigureCreatedOn();

        builder.Property(x => x.FirstName)
            .HasComment("Имя автора")
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasComment("Фамилия автора")
            .IsRequired();

        builder.Property(x => x.CreatedByUserId)
            .HasComment("ID пользователя создавшего запись")
            .IsRequired();

        builder.Property(x => x.ModifiedByUserId)
            .HasComment("ID пользователя редактировавшего запись")
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasComment("Дата создания записи")
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .HasComment("Дата последнего изменения записи")
            .IsRequired();

    }
}