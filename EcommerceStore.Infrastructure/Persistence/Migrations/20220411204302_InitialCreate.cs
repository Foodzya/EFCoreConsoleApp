using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EcommerceStore.Infrastucture.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ecommerce");

            migrationBuilder.CreateTable(
                name: "brands",
                schema: "ecommerce",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    foundationyear = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_brands", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "productcategories",
                schema: "ecommerce",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    parentcategoryid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_productcategories", x => x.id);
                    table.ForeignKey(
                        name: "fk_productcategories_productcategories_parentcategoryid",
                        column: x => x.parentcategoryid,
                        principalSchema: "ecommerce",
                        principalTable: "productcategories",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "roles",
                schema: "ecommerce",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sections",
                schema: "ecommerce",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sections", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                schema: "ecommerce",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<double>(type: "double precision", nullable: false),
                    quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    image = table.Column<string>(type: "text", nullable: true),
                    brandid = table.Column<int>(type: "integer", nullable: false),
                    productcategoryid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_products", x => x.id);
                    table.ForeignKey(
                        name: "fk_products_brands_brandid",
                        column: x => x.brandid,
                        principalSchema: "ecommerce",
                        principalTable: "brands",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_products_productcategories_productcategoryid",
                        column: x => x.productcategoryid,
                        principalSchema: "ecommerce",
                        principalTable: "productcategories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "ecommerce",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    firstname = table.Column<string>(type: "text", nullable: true),
                    lastname = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: false),
                    phonenumber = table.Column<string>(type: "text", nullable: false),
                    roleid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_users_roles_roleid",
                        column: x => x.roleid,
                        principalSchema: "ecommerce",
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "productcategorysections",
                schema: "ecommerce",
                columns: table => new
                {
                    sectionid = table.Column<int>(type: "integer", nullable: false),
                    productcategoryid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_productcategorysections", x => new { x.sectionid, x.productcategoryid });
                    table.ForeignKey(
                        name: "fk_productcategorysections_productcategories_productcategoryid",
                        column: x => x.productcategoryid,
                        principalSchema: "ecommerce",
                        principalTable: "productcategories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_productcategorysections_sections_sectionid",
                        column: x => x.sectionid,
                        principalSchema: "ecommerce",
                        principalTable: "sections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "addresses",
                schema: "ecommerce",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    streetaddress = table.Column<string>(type: "text", nullable: true),
                    postcode = table.Column<int>(type: "integer", nullable: false),
                    city = table.Column<string>(type: "text", nullable: true),
                    userid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_addresses", x => x.id);
                    table.ForeignKey(
                        name: "fk_addresses_users_userid",
                        column: x => x.userid,
                        principalSchema: "ecommerce",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                schema: "ecommerce",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    modifieddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    userid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orders", x => x.id);
                    table.ForeignKey(
                        name: "fk_orders_users_userid",
                        column: x => x.userid,
                        principalSchema: "ecommerce",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reviews",
                schema: "ecommerce",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rating = table.Column<int>(type: "integer", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: true),
                    productid = table.Column<int>(type: "integer", nullable: false),
                    userid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reviews", x => x.id);
                    table.ForeignKey(
                        name: "fk_reviews_products_productid",
                        column: x => x.productid,
                        principalSchema: "ecommerce",
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_reviews_users_userid",
                        column: x => x.userid,
                        principalSchema: "ecommerce",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "productorders",
                schema: "ecommerce",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<double>(type: "double precision", nullable: false),
                    productid = table.Column<int>(type: "integer", nullable: false),
                    orderid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_productorders", x => x.id);
                    table.ForeignKey(
                        name: "fk_productorders_orders_orderid",
                        column: x => x.orderid,
                        principalSchema: "ecommerce",
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_productorders_products_productid",
                        column: x => x.productid,
                        principalSchema: "ecommerce",
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_addresses_userid",
                schema: "ecommerce",
                table: "addresses",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "ix_brands_name",
                schema: "ecommerce",
                table: "brands",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_orders_userid",
                schema: "ecommerce",
                table: "orders",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "ix_productcategories_name",
                schema: "ecommerce",
                table: "productcategories",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_productcategories_parentcategoryid",
                schema: "ecommerce",
                table: "productcategories",
                column: "parentcategoryid");

            migrationBuilder.CreateIndex(
                name: "ix_productcategorysections_productcategoryid",
                schema: "ecommerce",
                table: "productcategorysections",
                column: "productcategoryid");

            migrationBuilder.CreateIndex(
                name: "ix_productorders_orderid",
                schema: "ecommerce",
                table: "productorders",
                column: "orderid");

            migrationBuilder.CreateIndex(
                name: "ix_productorders_productid",
                schema: "ecommerce",
                table: "productorders",
                column: "productid");

            migrationBuilder.CreateIndex(
                name: "ix_products_brandid",
                schema: "ecommerce",
                table: "products",
                column: "brandid");

            migrationBuilder.CreateIndex(
                name: "ix_products_name",
                schema: "ecommerce",
                table: "products",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_products_productcategoryid",
                schema: "ecommerce",
                table: "products",
                column: "productcategoryid");

            migrationBuilder.CreateIndex(
                name: "ix_reviews_productid",
                schema: "ecommerce",
                table: "reviews",
                column: "productid");

            migrationBuilder.CreateIndex(
                name: "ix_reviews_userid",
                schema: "ecommerce",
                table: "reviews",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "ix_roles_name",
                schema: "ecommerce",
                table: "roles",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_sections_name",
                schema: "ecommerce",
                table: "sections",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                schema: "ecommerce",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_roleid",
                schema: "ecommerce",
                table: "users",
                column: "roleid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "addresses",
                schema: "ecommerce");

            migrationBuilder.DropTable(
                name: "productcategorysections",
                schema: "ecommerce");

            migrationBuilder.DropTable(
                name: "productorders",
                schema: "ecommerce");

            migrationBuilder.DropTable(
                name: "reviews",
                schema: "ecommerce");

            migrationBuilder.DropTable(
                name: "sections",
                schema: "ecommerce");

            migrationBuilder.DropTable(
                name: "orders",
                schema: "ecommerce");

            migrationBuilder.DropTable(
                name: "products",
                schema: "ecommerce");

            migrationBuilder.DropTable(
                name: "users",
                schema: "ecommerce");

            migrationBuilder.DropTable(
                name: "brands",
                schema: "ecommerce");

            migrationBuilder.DropTable(
                name: "productcategories",
                schema: "ecommerce");

            migrationBuilder.DropTable(
                name: "roles",
                schema: "ecommerce");
        }
    }
}
