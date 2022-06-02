using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceStore.Infrastructure.Persistence.Migrations
{
    public partial class PasswordHashForUserEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "passwordhash",
                schema: "ecommerce",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "passwordhash",
                schema: "ecommerce",
                table: "users");
        }
    }
}
