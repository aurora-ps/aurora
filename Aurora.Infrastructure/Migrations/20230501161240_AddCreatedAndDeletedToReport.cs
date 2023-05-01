using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aurora.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedAndDeletedToReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOnUtc",
                table: "Reports",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GetUtcDate()");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnUtc",
                table: "Reports",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOnUtc",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                table: "Reports");
        }
    }
}
