using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aurora.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeReportUserCreatedByToReportUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_AspNetUsers_CreatedByUserId",
                table: "Reports");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "Reports",
                newName: "ReportUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_CreatedByUserId",
                table: "Reports",
                newName: "IX_Reports_ReportUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_AspNetUsers_ReportUserId",
                table: "Reports",
                column: "ReportUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_AspNetUsers_ReportUserId",
                table: "Reports");

            migrationBuilder.RenameColumn(
                name: "ReportUserId",
                table: "Reports",
                newName: "CreatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_ReportUserId",
                table: "Reports",
                newName: "IX_Reports_CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_AspNetUsers_CreatedByUserId",
                table: "Reports",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
