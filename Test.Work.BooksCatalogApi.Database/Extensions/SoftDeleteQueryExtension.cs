using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.Work.BooksCatalogApi.BLL.Entities.Common;

namespace Test.Work.BooksCatalogApi.Database.Extensions
{
    /// <summary>
	/// Расширения для soft delete
	/// </summary>
	public static class SoftDeleteQueryExtension
    {
        /// <summary>
        /// Добавить глобальный фильтр для сущности
        /// </summary>
        /// <param name="entityData">Тип сущности</param>
        public static void AddSoftDeleteQueryFilter(this IMutableEntityType entityData)
        {
            var methodToCall = typeof(SoftDeleteQueryExtension)
                .GetMethod(
                    nameof(GetSoftDeleteFilter),
                    BindingFlags.NonPublic | BindingFlags.Static)
                ?.MakeGenericMethod(entityData.ClrType);

            if (methodToCall == null)
                throw new InvalidOperationException($"не удалось вызвать метод {nameof(GetSoftDeleteFilter)}");

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            var filter = (LambdaExpression)methodToCall.Invoke(null, Array.Empty<object>());
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            entityData.SetQueryFilter(filter);
        }

        /// <summary>
        /// Добавить глобальный фильтр для сущности
        /// </summary>
        /// <param name="entityType">Тип сущности</param>
        public static void AddDeletedAtField(this IMutableEntityType entityType)
        {
            var builder = new EntityTypeBuilder<ISoftDeletable>(entityType);

            var isCommentNotDefined = string.IsNullOrEmpty(
                builder
                    .Property(x => x.DeletedAt)
                    .Metadata.GetComment());

            if (isCommentNotDefined)
                builder.Property(x => x.DeletedAt)
                    .HasComment("Дата удаления записи");
        }

        private static LambdaExpression GetSoftDeleteFilter<TEntity>()
            where TEntity : class, ISoftDeletable
                => (Expression<Func<TEntity, bool>>)(x => !x.DeletedAt.HasValue);
    }
}
