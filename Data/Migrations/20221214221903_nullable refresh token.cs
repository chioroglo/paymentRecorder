using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class nullablerefreshtoken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ApplicationUser_RefreshToken",
                table: "ApplicationUser");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RefreshTokenExpirationDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "RefreshToken",
                table: "ApplicationUser",
                type: "nchar(128)",
                fixedLength: true,
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(128)",
                oldFixedLength: true,
                oldMaxLength: 128);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_RefreshToken",
                table: "ApplicationUser",
                column: "RefreshToken",
                unique: true,
                filter: "[RefreshToken] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ApplicationUser_RefreshToken",
                table: "ApplicationUser");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RefreshTokenExpirationDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "RefreshToken",
                table: "ApplicationUser",
                type: "nchar(128)",
                fixedLength: true,
                maxLength: 128,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nchar(128)",
                oldFixedLength: true,
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_RefreshToken",
                table: "ApplicationUser",
                column: "RefreshToken",
                unique: true);
        }
    }
}
