using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project_ecommerce_admin.Migrations
{
    /// <inheritdoc />
    public partial class Fixtablecategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Level",
                table: "Categories",
                newName: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Categories",
                newName: "Level");
        }
    }
}
