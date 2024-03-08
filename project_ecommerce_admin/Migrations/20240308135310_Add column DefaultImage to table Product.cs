using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project_ecommerce_admin.Migrations
{
    /// <inheritdoc />
    public partial class AddcolumnDefaultImagetotableProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DefaultImage",
                table: "Products",
                type: "nvarchar(500)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultImage",
                table: "Products");
        }
    }
}
