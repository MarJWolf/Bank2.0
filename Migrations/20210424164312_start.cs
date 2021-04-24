using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bank.Migrations
{
    public partial class start : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Identity");

            migrationBuilder.CreateTable(
                name: "AccountType",
                schema: "Identity",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LIHVA = table.Column<double>(type: "float", nullable: false),
                    MESECHNA_TAKSA = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                schema: "Identity",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FULL_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Position",
                schema: "Identity",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TransactionCategory",
                schema: "Identity",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COEF = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionCategory", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                schema: "Identity",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EGN = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: true),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PN = table.Column<string>(type: "VARCHAR(13)", maxLength: 13, nullable: true),
                    ID_user = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Client_AspNetUsers_ID_user",
                        column: x => x.ID_user,
                        principalSchema: "Identity",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                schema: "Identity",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PN = table.Column<string>(type: "VARCHAR(13)", maxLength: 13, nullable: true),
                    ID_position = table.Column<int>(type: "int", nullable: true),
                    ID_user = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Employee_AspNetUsers_ID_user",
                        column: x => x.ID_user,
                        principalSchema: "Identity",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employee_Position_ID_position",
                        column: x => x.ID_position,
                        principalSchema: "Identity",
                        principalTable: "Position",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BankAccount",
                schema: "Identity",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_currency = table.Column<int>(type: "int", nullable: true),
                    INTEREST = table.Column<float>(type: "real", nullable: false),
                    BALANCE = table.Column<float>(type: "real", nullable: false),
                    EGN_client = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccount", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BankAccount_Client_EGN_client",
                        column: x => x.EGN_client,
                        principalSchema: "Identity",
                        principalTable: "Client",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BankAccount_Currency_ID_currency",
                        column: x => x.ID_currency,
                        principalSchema: "Identity",
                        principalTable: "Currency",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                schema: "Identity",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME_Category = table.Column<int>(type: "int", nullable: true),
                    SUM = table.Column<float>(type: "real", nullable: false),
                    DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ID_employee = table.Column<int>(type: "int", nullable: true),
                    ID_bankAccount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Transaction_BankAccount_ID_bankAccount",
                        column: x => x.ID_bankAccount,
                        principalSchema: "Identity",
                        principalTable: "BankAccount",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_Employee_ID_employee",
                        column: x => x.ID_employee,
                        principalSchema: "Identity",
                        principalTable: "Employee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_TransactionCategory_NAME_Category",
                        column: x => x.NAME_Category,
                        principalSchema: "Identity",
                        principalTable: "TransactionCategory",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "Identity",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "Identity",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_EGN_client",
                schema: "Identity",
                table: "BankAccount",
                column: "EGN_client");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_ID_currency",
                schema: "Identity",
                table: "BankAccount",
                column: "ID_currency");

            migrationBuilder.CreateIndex(
                name: "IX_Client_ID_user",
                schema: "Identity",
                table: "Client",
                column: "ID_user");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_ID_position",
                schema: "Identity",
                table: "Employee",
                column: "ID_position");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_ID_user",
                schema: "Identity",
                table: "Employee",
                column: "ID_user");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_ID_bankAccount",
                schema: "Identity",
                table: "Transaction",
                column: "ID_bankAccount");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_ID_employee",
                schema: "Identity",
                table: "Transaction",
                column: "ID_employee");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_NAME_Category",
                schema: "Identity",
                table: "Transaction",
                column: "NAME_Category");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountType",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Transaction",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "BankAccount",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Employee",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "TransactionCategory",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Client",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Currency",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Position",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "Identity");
        }
    }
}
