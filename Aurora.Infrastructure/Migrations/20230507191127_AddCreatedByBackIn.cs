using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aurora.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedByBackIn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "Reports",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_CreatedByUserId",
                table: "Reports",
                column: "CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_AspNetUsers_CreatedByUserId",
                table: "Reports",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_AspNetUsers_CreatedByUserId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_CreatedByUserId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Reports");
        }
    }
}
