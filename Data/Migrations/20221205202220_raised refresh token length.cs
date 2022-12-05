using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class raisedrefreshtokenlength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RefreshToken",
                table: "ApplicationUser",
                type: "nchar(128)",
                fixedLength: true,
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(64)",
                oldFixedLength: true,
                oldMaxLength: 64);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RefreshToken",
                table: "ApplicationUser",
                type: "nchar(64)",
                fixedLength: true,
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(128)",
                oldFixedLength: true,
                oldMaxLength: 128);
        }
    }
}
