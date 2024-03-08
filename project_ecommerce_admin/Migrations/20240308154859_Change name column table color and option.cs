using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project_ecommerce_admin.Migrations
{
    /// <inheritdoc />
    public partial class Changenamecolumntablecolorandoption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PricePlus",
                table: "Options",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "PricePlus",
                table: "Colors",
                newName: "Price");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Options",
                newName: "PricePlus");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Colors",
                newName: "PricePlus");
        }
    }
}
