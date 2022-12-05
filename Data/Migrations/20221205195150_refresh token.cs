using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class refreshtoken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Order_OrderId",
                table: "Transaction");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "ApplicationUser",
                type: "nchar(64)",
                fixedLength: true,
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpirationDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Order_OrderId",
                table: "Transaction",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Order_OrderId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpirationDate",
                table: "ApplicationUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Order_OrderId",
                table: "Transaction",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id");
        }
    }
}
