using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apiUsuariosCuenta.Migrations
{
    /// <inheritdoc />
    public partial class updateNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InterUserTransactions_Cuentas_AmountAccountId",
                table: "InterUserTransactions");

            migrationBuilder.RenameColumn(
                name: "InterUserTransactionTypeId",
                table: "InterUserTransactions",
                newName: "InterUserTransactionTypeID");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "InterUserTransactions",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "AmountAccountId",
                table: "InterUserTransactions",
                newName: "CuentaAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_InterUserTransactions_InterUserTransactionTypeId",
                table: "InterUserTransactions",
                newName: "IX_InterUserTransactions_InterUserTransactionTypeID");

            migrationBuilder.RenameIndex(
                name: "IX_InterUserTransactions_AmountAccountId",
                table: "InterUserTransactions",
                newName: "IX_InterUserTransactions_CuentaAccountId");

            migrationBuilder.RenameColumn(
                name: "InterUserTransactionTypeId",
                table: "ATMInterUserTransactions",
                newName: "InterUserTransactionTypeID");

            migrationBuilder.RenameColumn(
                name: "ATMInterUserTransactionId",
                table: "ATMInterUserTransactions",
                newName: "ATMInterUserTransactionID");

            migrationBuilder.RenameIndex(
                name: "IX_ATMInterUserTransactions_InterUserTransactionTypeId",
                table: "ATMInterUserTransactions",
                newName: "IX_ATMInterUserTransactions_InterUserTransactionTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_InterUserTransactions_Cuentas_CuentaAccountId",
                table: "InterUserTransactions",
                column: "CuentaAccountId",
                principalTable: "Cuentas",
                principalColumn: "AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InterUserTransactions_Cuentas_CuentaAccountId",
                table: "InterUserTransactions");

            migrationBuilder.RenameColumn(
                name: "InterUserTransactionTypeID",
                table: "InterUserTransactions",
                newName: "InterUserTransactionTypeId");

            migrationBuilder.RenameColumn(
                name: "CuentaAccountId",
                table: "InterUserTransactions",
                newName: "AmountAccountId");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "InterUserTransactions",
                newName: "Quantity");

            migrationBuilder.RenameIndex(
                name: "IX_InterUserTransactions_InterUserTransactionTypeID",
                table: "InterUserTransactions",
                newName: "IX_InterUserTransactions_InterUserTransactionTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_InterUserTransactions_CuentaAccountId",
                table: "InterUserTransactions",
                newName: "IX_InterUserTransactions_AmountAccountId");

            migrationBuilder.RenameColumn(
                name: "InterUserTransactionTypeID",
                table: "ATMInterUserTransactions",
                newName: "InterUserTransactionTypeId");

            migrationBuilder.RenameColumn(
                name: "ATMInterUserTransactionID",
                table: "ATMInterUserTransactions",
                newName: "ATMInterUserTransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_ATMInterUserTransactions_InterUserTransactionTypeID",
                table: "ATMInterUserTransactions",
                newName: "IX_ATMInterUserTransactions_InterUserTransactionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_InterUserTransactions_Cuentas_AmountAccountId",
                table: "InterUserTransactions",
                column: "AmountAccountId",
                principalTable: "Cuentas",
                principalColumn: "AccountId");
        }
    }
}
