using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(type: "longtext", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: false),
                    Role = table.Column<string>(type: "longtext", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AccountName = table.Column<string>(type: "longtext", nullable: false),
                    AccountType = table.Column<string>(type: "longtext", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionCategories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "longtext", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_TransactionCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "TransactionCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserId",
                table: "Accounts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionCategories_UserId",
                table: "TransactionCategories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountId",
                table: "Transactions",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CategoryId",
                table: "Transactions",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Transactions");
            migrationBuilder.DropTable(name: "Accounts");
            migrationBuilder.DropTable(name: "TransactionCategories");
            migrationBuilder.DropTable(name: "Users");
        }
    }
}




















//using System;
//using Microsoft.EntityFrameworkCore.Migrations;

//#nullable disable

//namespace FinancialApp.Migrations
//{
//    /// <inheritdoc />
//    public partial class InitialCreate : Migration
//    {
//        /// <inheritdoc />
//        protected override void Up(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.CreateTable(
//                name: "Users",
//                columns: table => new
//                {
//                    Id = table.Column<int>(type: "int", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_Users", x => x.Id);
//                });

//            migrationBuilder.CreateTable(
//                name: "Accounts",
//                columns: table => new
//                {
//                    Id = table.Column<int>(type: "int", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    UserId = table.Column<int>(type: "int", nullable: false),
//                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    AccountType = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
//                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_Accounts", x => x.Id);
//                    table.ForeignKey(
//                        name: "FK_Accounts_Users_UserId",
//                        column: x => x.UserId,
//                        principalTable: "Users",
//                        principalColumn: "Id",
//                        onDelete: ReferentialAction.Cascade);
//                });

//            migrationBuilder.CreateTable(
//                name: "TransactionCategories",
//                columns: table => new
//                {
//                    Id = table.Column<int>(type: "int", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    UserId = table.Column<int>(type: "int", nullable: false),
//                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_TransactionCategories", x => x.Id);
//                    table.ForeignKey(
//                        name: "FK_TransactionCategories_Users_UserId",
//                        column: x => x.UserId,
//                        principalTable: "Users",
//                        principalColumn: "Id",
//                        onDelete: ReferentialAction.Cascade);
//                });

//            migrationBuilder.CreateTable(
//                name: "Transactions",
//                columns: table => new
//                {
//                    Id = table.Column<int>(type: "int", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    AccountId = table.Column<int>(type: "int", nullable: false),
//                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
//                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    CategoryId = table.Column<int>(type: "int", nullable: true),
//                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_Transactions", x => x.Id);
//                    table.ForeignKey(
//                        name: "FK_Transactions_Accounts_AccountId",
//                        column: x => x.AccountId,
//                        principalTable: "Accounts",
//                        principalColumn: "Id",
//                        onDelete: ReferentialAction.Cascade);
//                    table.ForeignKey(
//                        name: "FK_Transactions_TransactionCategories_CategoryId",
//                        column: x => x.CategoryId,
//                        principalTable: "TransactionCategories",
//                        principalColumn: "Id");
//                });

//            migrationBuilder.CreateIndex(
//                name: "IX_Accounts_UserId",
//                table: "Accounts",
//                column: "UserId");

//            migrationBuilder.CreateIndex(
//                name: "IX_TransactionCategories_UserId",
//                table: "TransactionCategories",
//                column: "UserId");

//            migrationBuilder.CreateIndex(
//                name: "IX_Transactions_AccountId",
//                table: "Transactions",
//                column: "AccountId");

//            migrationBuilder.CreateIndex(
//                name: "IX_Transactions_CategoryId",
//                table: "Transactions",
//                column: "CategoryId");
//        }

//        /// <inheritdoc />
//        protected override void Down(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.DropTable(
//                name: "Transactions");

//            migrationBuilder.DropTable(
//                name: "Accounts");

//            migrationBuilder.DropTable(
//                name: "TransactionCategories");

//            migrationBuilder.DropTable(
//                name: "Users");
//        }
//    }
//}
