using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Test.Work.BooksCatalogApi.Database.Migrations
{
    public partial class UpdateCoAuthorsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_сo_author_authors_author_entity_id",
                schema: "public",
                table: "сo_author");

            migrationBuilder.DropForeignKey(
                name: "fk_сo_author_books_book_entity_id",
                schema: "public",
                table: "сo_author");

            migrationBuilder.RenameColumn(
                name: "author_entity_id",
                schema: "public",
                table: "сo_author",
                newName: "book_id");

            migrationBuilder.RenameIndex(
                name: "ix_сo_author_author_entity_id",
                schema: "public",
                table: "сo_author",
                newName: "ix_сo_author_book_id");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                schema: "public",
                table: "сo_author",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "author_id",
                schema: "public",
                table: "сo_author",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_сo_author_author_id",
                schema: "public",
                table: "сo_author",
                column: "author_id");

            migrationBuilder.AddForeignKey(
                name: "fk_сo_author_authors_author_id",
                schema: "public",
                table: "сo_author",
                column: "author_id",
                principalSchema: "public",
                principalTable: "authors",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_сo_author_books_book_id",
                schema: "public",
                table: "сo_author",
                column: "book_id",
                principalSchema: "public",
                principalTable: "books",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_сo_author_authors_author_id",
                schema: "public",
                table: "сo_author");

            migrationBuilder.DropForeignKey(
                name: "fk_сo_author_books_book_id",
                schema: "public",
                table: "сo_author");

            migrationBuilder.DropIndex(
                name: "ix_сo_author_author_id",
                schema: "public",
                table: "сo_author");

            migrationBuilder.DropColumn(
                name: "author_id",
                schema: "public",
                table: "сo_author");

            migrationBuilder.RenameColumn(
                name: "book_id",
                schema: "public",
                table: "сo_author",
                newName: "author_entity_id");

            migrationBuilder.RenameIndex(
                name: "ix_сo_author_book_id",
                schema: "public",
                table: "сo_author",
                newName: "ix_сo_author_author_entity_id");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                schema: "public",
                table: "сo_author",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "fk_сo_author_authors_author_entity_id",
                schema: "public",
                table: "сo_author",
                column: "author_entity_id",
                principalSchema: "public",
                principalTable: "authors",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_сo_author_books_book_entity_id",
                schema: "public",
                table: "сo_author",
                column: "id",
                principalSchema: "public",
                principalTable: "books",
                principalColumn: "id");
        }
    }
}
