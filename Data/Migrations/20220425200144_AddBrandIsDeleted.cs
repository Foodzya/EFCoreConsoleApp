using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceStore.Data.Migrations
{
    public partial class AddBrandIsDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isdeleted",
                schema: "ecommerce",
                table: "brands",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isdeleted",
                schema: "ecommerce",
                table: "brands");
        }
    }
}
