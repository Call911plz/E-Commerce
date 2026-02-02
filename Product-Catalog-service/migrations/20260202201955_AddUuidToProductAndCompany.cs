using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Product_Catalog_service.migrations
{
    /// <inheritdoc />
    public partial class AddUuidToProductAndCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PublicHash",
                table: "Products",
                newName: "Uuid");

            migrationBuilder.AddColumn<string>(
                name: "Uuid",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Uuid",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "Uuid",
                table: "Products",
                newName: "PublicHash");
        }
    }
}
