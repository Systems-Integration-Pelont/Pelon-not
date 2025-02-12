using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apiUsuariosCuenta.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Atms",
                columns: table => new
                {
                    ATMId = table.Column<Guid>(type: "uuid", nullable: false),
                    Funds = table.Column<double>(type: "double precision", nullable: true),
                    Direction = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atms", x => x.ATMId);
                });

            migrationBuilder.CreateTable(
                name: "InterUserTransactionTypes",
                columns: table => new
                {
                    InterUserTransactionTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    TypeName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterUserTransactionTypes", x => x.InterUserTransactionTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Rols",
                columns: table => new
                {
                    RolId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    AccessLevel = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rols", x => x.RolId);
                });

            migrationBuilder.CreateTable(
                name: "SelfOperationTypes",
                columns: table => new
                {
                    SelfOperationTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    TypeName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelfOperationTypes", x => x.SelfOperationTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Cuentas",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: true),
                    Quantity = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuentas", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_Cuentas_Usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "Usuarios",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAccesses",
                columns: table => new
                {
                    UserAccessId = table.Column<Guid>(type: "uuid", nullable: false),
                    RolID = table.Column<Guid>(type: "uuid", nullable: true),
                    UserID = table.Column<Guid>(type: "uuid", nullable: true),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccesses", x => x.UserAccessId);
                    table.ForeignKey(
                        name: "FK_UserAccesses_Rols_RolID",
                        column: x => x.RolID,
                        principalTable: "Rols",
                        principalColumn: "RolId");
                    table.ForeignKey(
                        name: "FK_UserAccesses_Usuarios_UserID",
                        column: x => x.UserID,
                        principalTable: "Usuarios",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "ATMInterUserTransactions",
                columns: table => new
                {
                    ATMInterUserTransactionId = table.Column<Guid>(type: "uuid", nullable: false),
                    DestinyAccountID = table.Column<Guid>(type: "uuid", nullable: true),
                    SenderAccountID = table.Column<Guid>(type: "uuid", nullable: true),
                    ATMID = table.Column<Guid>(type: "uuid", nullable: true),
                    InterUserTransactionTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    CuentaAccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    Quantity = table.Column<double>(type: "double precision", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ATMInterUserTransactions", x => x.ATMInterUserTransactionId);
                    table.ForeignKey(
                        name: "FK_ATMInterUserTransactions_Atms_ATMID",
                        column: x => x.ATMID,
                        principalTable: "Atms",
                        principalColumn: "ATMId");
                    table.ForeignKey(
                        name: "FK_ATMInterUserTransactions_Cuentas_CuentaAccountId",
                        column: x => x.CuentaAccountId,
                        principalTable: "Cuentas",
                        principalColumn: "AccountId");
                    table.ForeignKey(
                        name: "FK_ATMInterUserTransactions_InterUserTransactionTypes_InterUse~",
                        column: x => x.InterUserTransactionTypeId,
                        principalTable: "InterUserTransactionTypes",
                        principalColumn: "InterUserTransactionTypeId");
                });

            migrationBuilder.CreateTable(
                name: "InterUserTransactions",
                columns: table => new
                {
                    InterUserTransactionId = table.Column<Guid>(type: "uuid", nullable: false),
                    DestinyAccountID = table.Column<Guid>(type: "uuid", nullable: true),
                    SenderAccountID = table.Column<Guid>(type: "uuid", nullable: true),
                    InterUserTransactionTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    CuentaAccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    Quantity = table.Column<double>(type: "double precision", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterUserTransactions", x => x.InterUserTransactionId);
                    table.ForeignKey(
                        name: "FK_InterUserTransactions_Cuentas_CuentaAccountId",
                        column: x => x.CuentaAccountId,
                        principalTable: "Cuentas",
                        principalColumn: "AccountId");
                    table.ForeignKey(
                        name: "FK_InterUserTransactions_InterUserTransactionTypes_InterUserTr~",
                        column: x => x.InterUserTransactionTypeId,
                        principalTable: "InterUserTransactionTypes",
                        principalColumn: "InterUserTransactionTypeId");
                });

            migrationBuilder.CreateTable(
                name: "SelfAtmOperations",
                columns: table => new
                {
                    SelfATMOperationId = table.Column<Guid>(type: "uuid", nullable: false),
                    BankAcountID = table.Column<Guid>(type: "uuid", nullable: true),
                    ATMID = table.Column<Guid>(type: "uuid", nullable: true),
                    SelfOperationTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    CuentaAccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    Quantity = table.Column<double>(type: "double precision", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelfAtmOperations", x => x.SelfATMOperationId);
                    table.ForeignKey(
                        name: "FK_SelfAtmOperations_Atms_ATMID",
                        column: x => x.ATMID,
                        principalTable: "Atms",
                        principalColumn: "ATMId");
                    table.ForeignKey(
                        name: "FK_SelfAtmOperations_Cuentas_CuentaAccountId",
                        column: x => x.CuentaAccountId,
                        principalTable: "Cuentas",
                        principalColumn: "AccountId");
                    table.ForeignKey(
                        name: "FK_SelfAtmOperations_SelfOperationTypes_SelfOperationTypeId",
                        column: x => x.SelfOperationTypeId,
                        principalTable: "SelfOperationTypes",
                        principalColumn: "SelfOperationTypeId");
                });

            migrationBuilder.CreateTable(
                name: "SelfOperations",
                columns: table => new
                {
                    SelfOperationId = table.Column<Guid>(type: "uuid", nullable: false),
                    BankAcountID = table.Column<Guid>(type: "uuid", nullable: true),
                    SelfOperationTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    CuentaAccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    ATMId = table.Column<Guid>(type: "uuid", nullable: true),
                    Quantity = table.Column<double>(type: "double precision", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelfOperations", x => x.SelfOperationId);
                    table.ForeignKey(
                        name: "FK_SelfOperations_Atms_ATMId",
                        column: x => x.ATMId,
                        principalTable: "Atms",
                        principalColumn: "ATMId");
                    table.ForeignKey(
                        name: "FK_SelfOperations_Cuentas_CuentaAccountId",
                        column: x => x.CuentaAccountId,
                        principalTable: "Cuentas",
                        principalColumn: "AccountId");
                    table.ForeignKey(
                        name: "FK_SelfOperations_SelfOperationTypes_SelfOperationTypeId",
                        column: x => x.SelfOperationTypeId,
                        principalTable: "SelfOperationTypes",
                        principalColumn: "SelfOperationTypeId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ATMInterUserTransactions_ATMID",
                table: "ATMInterUserTransactions",
                column: "ATMID");

            migrationBuilder.CreateIndex(
                name: "IX_ATMInterUserTransactions_CuentaAccountId",
                table: "ATMInterUserTransactions",
                column: "CuentaAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ATMInterUserTransactions_InterUserTransactionTypeId",
                table: "ATMInterUserTransactions",
                column: "InterUserTransactionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cuentas_UserId",
                table: "Cuentas",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InterUserTransactions_CuentaAccountId",
                table: "InterUserTransactions",
                column: "CuentaAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_InterUserTransactions_InterUserTransactionTypeId",
                table: "InterUserTransactions",
                column: "InterUserTransactionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SelfAtmOperations_ATMID",
                table: "SelfAtmOperations",
                column: "ATMID");

            migrationBuilder.CreateIndex(
                name: "IX_SelfAtmOperations_CuentaAccountId",
                table: "SelfAtmOperations",
                column: "CuentaAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SelfAtmOperations_SelfOperationTypeId",
                table: "SelfAtmOperations",
                column: "SelfOperationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SelfOperations_ATMId",
                table: "SelfOperations",
                column: "ATMId");

            migrationBuilder.CreateIndex(
                name: "IX_SelfOperations_CuentaAccountId",
                table: "SelfOperations",
                column: "CuentaAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SelfOperations_SelfOperationTypeId",
                table: "SelfOperations",
                column: "SelfOperationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccesses_RolID",
                table: "UserAccesses",
                column: "RolID");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccesses_UserID",
                table: "UserAccesses",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ATMInterUserTransactions");

            migrationBuilder.DropTable(
                name: "InterUserTransactions");

            migrationBuilder.DropTable(
                name: "SelfAtmOperations");

            migrationBuilder.DropTable(
                name: "SelfOperations");

            migrationBuilder.DropTable(
                name: "UserAccesses");

            migrationBuilder.DropTable(
                name: "InterUserTransactionTypes");

            migrationBuilder.DropTable(
                name: "Atms");

            migrationBuilder.DropTable(
                name: "Cuentas");

            migrationBuilder.DropTable(
                name: "SelfOperationTypes");

            migrationBuilder.DropTable(
                name: "Rols");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
