using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Amado.Migrations
{
    /// <inheritdoc />
    public partial class updateColumnsInProductAndColor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "HexCode",
                table: "Colors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "#000000");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
               name: "Price",
               table: "Products");

            migrationBuilder.DropColumn(
                name: "HexCode",
                table: "Colors");
        }
    }
}
