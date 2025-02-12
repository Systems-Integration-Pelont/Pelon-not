using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apiUsuariosCuenta.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InterUserTransactions_Cuentas_CuentaAccountId",
                table: "InterUserTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_SelfOperations_Atms_ATMId",
                table: "SelfOperations");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "SelfAtmOperations");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "InterUserTransactions");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ATMInterUserTransactions");

            migrationBuilder.RenameColumn(
                name: "ATMId",
                table: "SelfOperations",
                newName: "ATMID");

            migrationBuilder.RenameColumn(
                name: "SelfOperationId",
                table: "SelfOperations",
                newName: "SelfOperationID");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "SelfOperations",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "SelfOperations",
                newName: "DateTime");

            migrationBuilder.RenameIndex(
                name: "IX_SelfOperations_ATMId",
                table: "SelfOperations",
                newName: "IX_SelfOperations_ATMID");

            migrationBuilder.RenameColumn(
                name: "SelfATMOperationId",
                table: "SelfAtmOperations",
                newName: "SelfATMOperationID");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "SelfAtmOperations",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "SelfAtmOperations",
                newName: "DateTime");

            migrationBuilder.RenameColumn(
                name: "InterUserTransactionTypeId",
                table: "InterUserTransactionTypes",
                newName: "InterUserTransactionTypeID");

            migrationBuilder.RenameColumn(
                name: "InterUserTransactionId",
                table: "InterUserTransactions",
                newName: "InterUserTransactionID");

            migrationBuilder.RenameColumn(
                name: "SenderAccountID",
                table: "InterUserTransactions",
                newName: "ToBankAccountID");

            migrationBuilder.RenameColumn(
                name: "DestinyAccountID",
                table: "InterUserTransactions",
                newName: "FromBankAccountID");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "InterUserTransactions",
                newName: "DateTime");

            migrationBuilder.RenameColumn(
                name: "CuentaAccountId",
                table: "InterUserTransactions",
                newName: "AmountAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_InterUserTransactions_CuentaAccountId",
                table: "InterUserTransactions",
                newName: "IX_InterUserTransactions_AmountAccountId");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Cuentas",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "Number",
                table: "Cuentas",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "ATMId",
                table: "Atms",
                newName: "ATMID");

            migrationBuilder.RenameColumn(
                name: "SenderAccountID",
                table: "ATMInterUserTransactions",
                newName: "ToBankAccountID");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "ATMInterUserTransactions",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "DestinyAccountID",
                table: "ATMInterUserTransactions",
                newName: "FromBankAccountID");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "ATMInterUserTransactions",
                newName: "DateTime");

            migrationBuilder.AddForeignKey(
                name: "FK_InterUserTransactions_Cuentas_AmountAccountId",
                table: "InterUserTransactions",
                column: "AmountAccountId",
                principalTable: "Cuentas",
                principalColumn: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_SelfOperations_Atms_ATMID",
                table: "SelfOperations",
                column: "ATMID",
                principalTable: "Atms",
                principalColumn: "ATMID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InterUserTransactions_Cuentas_AmountAccountId",
                table: "InterUserTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_SelfOperations_Atms_ATMID",
                table: "SelfOperations");

            migrationBuilder.RenameColumn(
                name: "ATMID",
                table: "SelfOperations",
                newName: "ATMId");

            migrationBuilder.RenameColumn(
                name: "SelfOperationID",
                table: "SelfOperations",
                newName: "SelfOperationId");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "SelfOperations",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "SelfOperations",
                newName: "Quantity");

            migrationBuilder.RenameIndex(
                name: "IX_SelfOperations_ATMID",
                table: "SelfOperations",
                newName: "IX_SelfOperations_ATMId");

            migrationBuilder.RenameColumn(
                name: "SelfATMOperationID",
                table: "SelfAtmOperations",
                newName: "SelfATMOperationId");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "SelfAtmOperations",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "SelfAtmOperations",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "InterUserTransactionTypeID",
                table: "InterUserTransactionTypes",
                newName: "InterUserTransactionTypeId");

            migrationBuilder.RenameColumn(
                name: "InterUserTransactionID",
                table: "InterUserTransactions",
                newName: "InterUserTransactionId");

            migrationBuilder.RenameColumn(
                name: "ToBankAccountID",
                table: "InterUserTransactions",
                newName: "SenderAccountID");

            migrationBuilder.RenameColumn(
                name: "FromBankAccountID",
                table: "InterUserTransactions",
                newName: "DestinyAccountID");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "InterUserTransactions",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "AmountAccountId",
                table: "InterUserTransactions",
                newName: "CuentaAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_InterUserTransactions_AmountAccountId",
                table: "InterUserTransactions",
                newName: "IX_InterUserTransactions_CuentaAccountId");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Cuentas",
                newName: "Number");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Cuentas",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "ATMID",
                table: "Atms",
                newName: "ATMId");

            migrationBuilder.RenameColumn(
                name: "ToBankAccountID",
                table: "ATMInterUserTransactions",
                newName: "SenderAccountID");

            migrationBuilder.RenameColumn(
                name: "FromBankAccountID",
                table: "ATMInterUserTransactions",
                newName: "DestinyAccountID");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "ATMInterUserTransactions",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "ATMInterUserTransactions",
                newName: "Quantity");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "SelfAtmOperations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "InterUserTransactions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ATMInterUserTransactions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_InterUserTransactions_Cuentas_CuentaAccountId",
                table: "InterUserTransactions",
                column: "CuentaAccountId",
                principalTable: "Cuentas",
                principalColumn: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_SelfOperations_Atms_ATMId",
                table: "SelfOperations",
                column: "ATMId",
                principalTable: "Atms",
                principalColumn: "ATMId");
        }
    }
}
