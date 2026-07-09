using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MavericksBank.Migrations
{
    /// <inheritdoc />
    public partial class RenameLoanApplications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoansApplication_Customers_CustomerId",
                table: "LoansApplication");

            migrationBuilder.DropForeignKey(
                name: "FK_LoansApplication_Loans_LoanId",
                table: "LoansApplication");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoansApplication",
                table: "LoansApplication");

            migrationBuilder.RenameTable(
                name: "LoansApplication",
                newName: "LoanApplications");

            migrationBuilder.RenameIndex(
                name: "IX_LoansApplication_LoanId",
                table: "LoanApplications",
                newName: "IX_LoanApplications_LoanId");

            migrationBuilder.RenameIndex(
                name: "IX_LoansApplication_CustomerId",
                table: "LoanApplications",
                newName: "IX_LoanApplications_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoanApplications",
                table: "LoanApplications",
                column: "LoanApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoanApplications_Customers_CustomerId",
                table: "LoanApplications",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LoanApplications_Loans_LoanId",
                table: "LoanApplications",
                column: "LoanId",
                principalTable: "Loans",
                principalColumn: "LoanId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoanApplications_Customers_CustomerId",
                table: "LoanApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanApplications_Loans_LoanId",
                table: "LoanApplications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoanApplications",
                table: "LoanApplications");

            migrationBuilder.RenameTable(
                name: "LoanApplications",
                newName: "LoansApplication");

            migrationBuilder.RenameIndex(
                name: "IX_LoanApplications_LoanId",
                table: "LoansApplication",
                newName: "IX_LoansApplication_LoanId");

            migrationBuilder.RenameIndex(
                name: "IX_LoanApplications_CustomerId",
                table: "LoansApplication",
                newName: "IX_LoansApplication_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoansApplication",
                table: "LoansApplication",
                column: "LoanApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoansApplication_Customers_CustomerId",
                table: "LoansApplication",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LoansApplication_Loans_LoanId",
                table: "LoansApplication",
                column: "LoanId",
                principalTable: "Loans",
                principalColumn: "LoanId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
