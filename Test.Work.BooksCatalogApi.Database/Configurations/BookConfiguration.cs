using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.Work.BooksCatalogApi.BLL.Entities;
using Test.Work.BooksCatalogApi.Database.Configurations.Common;
using Test.Work.BooksCatalogApi.Database.Extensions;

namespace Test.Work.BooksCatalogApi.Database.Configurations;

internal class BookConfiguration : EntityBaseConfiguration<Book>
{
    public override void ConfigureChild(EntityTypeBuilder<Book> builder)
    {
        builder.HasComment("Книга");

        builder.ConfigureCreatedOn();

        builder.Property(x => x.Title)
            .HasComment("Название книги")
            .IsRequired();

        builder.Property(x => x.Description)
            .HasComment("Описание книги")
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

        builder.HasMany(x => x.СoAuthors)
            .WithOne(x => x.BookEntity)
            .HasForeignKey(x => x.BookId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.ClientCascade);

    }
}