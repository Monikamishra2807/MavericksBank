using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MavericksBank.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAccountClosureRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountClosureRequests");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountClosureRequests",
                columns: table => new
                {
                    AccountClosureRequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountClosureRequests", x => x.AccountClosureRequestId);
                    table.ForeignKey(
                        name: "FK_AccountClosureRequests_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountClosureRequests_AccountId",
                table: "AccountClosureRequests",
                column: "AccountId");
        }
    }
}
