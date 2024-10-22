using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Test.Work.BooksCatalogApi.Database.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "authors",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор записи")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false, comment: "Имя автора"),
                    last_name = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false, comment: "Фамилия автора"),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()", comment: "Дата создания записи"),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Дата последнего изменения записи"),
                    created_by_user_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "ID пользователя создавшего запись"),
                    modified_by_user_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "ID пользователя редактировавшего запись"),
                    deleted_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    book_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_authors", x => x.id);
                },
                comment: "Автор");

            migrationBuilder.CreateTable(
                name: "books",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор записи")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, comment: "Название книги"),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false, comment: "Описание книги"),
                    сhief_author_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()", comment: "Дата создания записи"),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Дата последнего изменения записи"),
                    created_by_user_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "ID пользователя создавшего запись"),
                    modified_by_user_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "ID пользователя редактировавшего запись"),
                    deleted_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_books", x => x.id);
                    table.ForeignKey(
                        name: "fk_books_authors_сhief_author_id",
                        column: x => x.сhief_author_id,
                        principalSchema: "public",
                        principalTable: "authors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Книга");

            migrationBuilder.CreateIndex(
                name: "ix_authors_book_id",
                schema: "public",
                table: "authors",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "ix_books_сhief_author_id",
                schema: "public",
                table: "books",
                column: "сhief_author_id");

            migrationBuilder.AddForeignKey(
                name: "fk_authors_books_book_id",
                schema: "public",
                table: "authors",
                column: "book_id",
                principalSchema: "public",
                principalTable: "books",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_authors_books_book_id",
                schema: "public",
                table: "authors");

            migrationBuilder.DropTable(
                name: "books",
                schema: "public");

            migrationBuilder.DropTable(
                name: "authors",
                schema: "public");
        }
    }
}
