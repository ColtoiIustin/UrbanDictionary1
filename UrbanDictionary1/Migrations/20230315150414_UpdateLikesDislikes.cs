using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrbanDictionary1.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLikesDislikes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Dislikes",
                table: "Expressions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Expressions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dislikes",
                table: "Expressions");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Expressions");
        }
    }
}
