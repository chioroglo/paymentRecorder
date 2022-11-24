using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class setalldeletebehaviorstonoactionforintegrity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Agent_AgentId",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK_Account_Bank_BankId",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Order_OrderId",
                table: "Transaction");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Agent_AgentId",
                table: "Account",
                column: "AgentId",
                principalTable: "Agent",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Bank_BankId",
                table: "Account",
                column: "BankId",
                principalTable: "Bank",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Order_OrderId",
                table: "Transaction",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Agent_AgentId",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK_Account_Bank_BankId",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Order_OrderId",
                table: "Transaction");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Agent_AgentId",
                table: "Account",
                column: "AgentId",
                principalTable: "Agent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Bank_BankId",
                table: "Account",
                column: "BankId",
                principalTable: "Bank",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Order_OrderId",
                table: "Transaction",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
