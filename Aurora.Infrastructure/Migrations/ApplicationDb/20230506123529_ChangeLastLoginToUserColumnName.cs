using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aurora.Infrastructure.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class ChangeLastLoginToUserColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastLoginOnUtc",
                table: "AspNetUsers",
                newName: "LastLoginUtc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastLoginUtc",
                table: "AspNetUsers",
                newName: "LastLoginOnUtc");
        }
    }
}
