using Microsoft.EntityFrameworkCore.Migrations;

namespace Bank.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccount_AccountType_AccTypeId",
                schema: "Identity",
                table: "BankAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_BankAccount_Client_ClientId",
                schema: "Identity",
                table: "BankAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_BankAccount_Currency_CurrencyId",
                schema: "Identity",
                table: "BankAccount");

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                schema: "Identity",
                table: "BankAccount",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                schema: "Identity",
                table: "BankAccount",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AccTypeId",
                schema: "Identity",
                table: "BankAccount",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccount_AccountType_AccTypeId",
                schema: "Identity",
                table: "BankAccount",
                column: "AccTypeId",
                principalSchema: "Identity",
                principalTable: "AccountType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccount_Client_ClientId",
                schema: "Identity",
                table: "BankAccount",
                column: "ClientId",
                principalSchema: "Identity",
                principalTable: "Client",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccount_Currency_CurrencyId",
                schema: "Identity",
                table: "BankAccount",
                column: "CurrencyId",
                principalSchema: "Identity",
                principalTable: "Currency",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccount_AccountType_AccTypeId",
                schema: "Identity",
                table: "BankAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_BankAccount_Client_ClientId",
                schema: "Identity",
                table: "BankAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_BankAccount_Currency_CurrencyId",
                schema: "Identity",
                table: "BankAccount");

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                schema: "Identity",
                table: "BankAccount",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                schema: "Identity",
                table: "BankAccount",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AccTypeId",
                schema: "Identity",
                table: "BankAccount",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccount_AccountType_AccTypeId",
                schema: "Identity",
                table: "BankAccount",
                column: "AccTypeId",
                principalSchema: "Identity",
                principalTable: "AccountType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccount_Client_ClientId",
                schema: "Identity",
                table: "BankAccount",
                column: "ClientId",
                principalSchema: "Identity",
                principalTable: "Client",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccount_Currency_CurrencyId",
                schema: "Identity",
                table: "BankAccount",
                column: "CurrencyId",
                principalSchema: "Identity",
                principalTable: "Currency",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
