#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Aurora.Api.Migrations;

/// <inheritdoc />
public partial class ExtendIdentityUser : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            "FirstName",
            "AspNetUsers",
            "nvarchar(100)",
            maxLength: 100,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "LastName",
            "AspNetUsers",
            "nvarchar(100)",
            maxLength: 100,
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            "FirstName",
            "AspNetUsers");

        migrationBuilder.DropColumn(
            "LastName",
            "AspNetUsers");
    }
}