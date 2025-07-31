using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyArabic.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddContentToChapters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Chapters",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Chapters");
        }
    }
}
