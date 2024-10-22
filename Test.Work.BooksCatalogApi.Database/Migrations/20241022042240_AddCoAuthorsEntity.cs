using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test.Work.BooksCatalogApi.Database.Migrations
{
    public partial class AddCoAuthorsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_authors_books_book_id",
                schema: "public",
                table: "authors");

            migrationBuilder.DropIndex(
                name: "ix_authors_book_id",
                schema: "public",
                table: "authors");

            migrationBuilder.DropColumn(
                name: "book_id",
                schema: "public",
                table: "authors");

            migrationBuilder.CreateTable(
                name: "сo_author",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    author_entity_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_by_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_сo_author", x => x.id);
                    table.ForeignKey(
                        name: "fk_сo_author_authors_author_entity_id",
                        column: x => x.author_entity_id,
                        principalSchema: "public",
                        principalTable: "authors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_сo_author_books_book_entity_id",
                        column: x => x.id,
                        principalSchema: "public",
                        principalTable: "books",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_сo_author_author_entity_id",
                schema: "public",
                table: "сo_author",
                column: "author_entity_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "сo_author",
                schema: "public");

            migrationBuilder.AddColumn<int>(
                name: "book_id",
                schema: "public",
                table: "authors",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_authors_book_id",
                schema: "public",
                table: "authors",
                column: "book_id");

            migrationBuilder.AddForeignKey(
                name: "fk_authors_books_book_id",
                schema: "public",
                table: "authors",
                column: "book_id",
                principalSchema: "public",
                principalTable: "books",
                principalColumn: "id");
        }
    }
}
