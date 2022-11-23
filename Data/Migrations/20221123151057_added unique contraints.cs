using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class addeduniquecontraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Order_Number",
                table: "Order",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bank_Code",
                table: "Bank",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Agent_FiscalCode",
                table: "Agent",
                column: "FiscalCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Account_AccountCode",
                table: "Account",
                column: "AccountCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Order_Number",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Bank_Code",
                table: "Bank");

            migrationBuilder.DropIndex(
                name: "IX_Agent_FiscalCode",
                table: "Agent");

            migrationBuilder.DropIndex(
                name: "IX_Account_AccountCode",
                table: "Account");
        }
    }
}
