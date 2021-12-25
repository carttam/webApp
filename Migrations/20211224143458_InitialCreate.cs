using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webApp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attributes",
                columns: table => new
                {
                    AttributeID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    titles = table.Column<string>(type: "TEXT", nullable: true),
                    datas = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attributes", x => x.AttributeID);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    BrandID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    website = table.Column<string>(type: "TEXT", nullable: true),
                    icon = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.BrandID);
                });

            migrationBuilder.CreateTable(
                name: "Categoris",
                columns: table => new
                {
                    CategoriID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    title = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoris", x => x.CategoriID);
                });

            migrationBuilder.CreateTable(
                name: "disCountCodes",
                columns: table => new
                {
                    DisCountCodeID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    code = table.Column<string>(type: "TEXT", nullable: true),
                    gen_date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    exp_date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    discount = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_disCountCodes", x => x.DisCountCodeID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    username = table.Column<string>(type: "TEXT", maxLength: 80, nullable: false),
                    email = table.Column<string>(type: "TEXT", nullable: true),
                    first_name = table.Column<string>(type: "TEXT", maxLength: 80, nullable: true),
                    last_name = table.Column<string>(type: "TEXT", maxLength: 80, nullable: false),
                    address = table.Column<string>(type: "TEXT", nullable: false),
                    phone_number = table.Column<string>(type: "TEXT", nullable: false),
                    password = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "SubCategoris",
                columns: table => new
                {
                    SubCategoriID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    title = table.Column<string>(type: "TEXT", nullable: true),
                    CategoriID = table.Column<int>(type: "INTEGER", nullable: true),
                    image = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategoris", x => x.SubCategoriID);
                    table.ForeignKey(
                        name: "FK_SubCategoris_Categoris_CategoriID",
                        column: x => x.CategoriID,
                        principalTable: "Categoris",
                        principalColumn: "CategoriID");
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<int>(type: "INTEGER", nullable: true),
                    total_price = table.Column<double>(type: "REAL", nullable: true),
                    paymentCode = table.Column<string>(type: "TEXT", nullable: true),
                    discountCodeID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentID);
                    table.ForeignKey(
                        name: "FK_Payments_disCountCodes_discountCodeID",
                        column: x => x.discountCodeID,
                        principalTable: "disCountCodes",
                        principalColumn: "DisCountCodeID");
                    table.ForeignKey(
                        name: "FK_Payments_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    TokenID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    token = table.Column<string>(type: "TEXT", nullable: true),
                    gen_dateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    exp_dateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UserID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.TokenID);
                    table.ForeignKey(
                        name: "FK_Tokens_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Objects",
                columns: table => new
                {
                    ObjectID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    title = table.Column<string>(type: "TEXT", nullable: true),
                    description = table.Column<string>(type: "TEXT", nullable: true),
                    image = table.Column<string>(type: "TEXT", nullable: true),
                    price = table.Column<double>(type: "REAL", nullable: true),
                    discount = table.Column<int>(type: "INTEGER", nullable: true),
                    BrandID = table.Column<int>(type: "INTEGER", nullable: true),
                    SubCategoriID = table.Column<int>(type: "INTEGER", nullable: false),
                    AttributeID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objects", x => x.ObjectID);
                    table.ForeignKey(
                        name: "FK_Objects_Attributes_AttributeID",
                        column: x => x.AttributeID,
                        principalTable: "Attributes",
                        principalColumn: "AttributeID");
                    table.ForeignKey(
                        name: "FK_Objects_Brands_BrandID",
                        column: x => x.BrandID,
                        principalTable: "Brands",
                        principalColumn: "BrandID");
                    table.ForeignKey(
                        name: "FK_Objects_SubCategoris_SubCategoriID",
                        column: x => x.SubCategoriID,
                        principalTable: "SubCategoris",
                        principalColumn: "SubCategoriID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sells",
                columns: table => new
                {
                    SellID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ObjectID = table.Column<int>(type: "INTEGER", nullable: true),
                    UserID = table.Column<int>(type: "INTEGER", nullable: true),
                    PaymentID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sells", x => x.SellID);
                    table.ForeignKey(
                        name: "FK_Sells_Objects_ObjectID",
                        column: x => x.ObjectID,
                        principalTable: "Objects",
                        principalColumn: "ObjectID");
                    table.ForeignKey(
                        name: "FK_Sells_Payments_PaymentID",
                        column: x => x.PaymentID,
                        principalTable: "Payments",
                        principalColumn: "PaymentID");
                    table.ForeignKey(
                        name: "FK_Sells_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Objects_AttributeID",
                table: "Objects",
                column: "AttributeID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Objects_BrandID",
                table: "Objects",
                column: "BrandID");

            migrationBuilder.CreateIndex(
                name: "IX_Objects_SubCategoriID",
                table: "Objects",
                column: "SubCategoriID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_discountCodeID",
                table: "Payments",
                column: "discountCodeID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UserID",
                table: "Payments",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Sells_ObjectID",
                table: "Sells",
                column: "ObjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Sells_PaymentID",
                table: "Sells",
                column: "PaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_Sells_UserID",
                table: "Sells",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategoris_CategoriID",
                table: "SubCategoris",
                column: "CategoriID");

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_UserID",
                table: "Tokens",
                column: "UserID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sells");

            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.DropTable(
                name: "Objects");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Attributes");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "SubCategoris");

            migrationBuilder.DropTable(
                name: "disCountCodes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categoris");
        }
    }
}
