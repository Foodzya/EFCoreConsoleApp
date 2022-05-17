using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceStore.Infrastructure.Persistence.Migrations
{
    public partial class AddIsDeletedForProductCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isdeleted",
                schema: "ecommerce",
                table: "productcategories",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isdeleted",
                schema: "ecommerce",
                table: "productcategories");
        }
    }
}
