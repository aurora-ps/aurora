using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aurora.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IncidentTypeSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IncidentTypes",
                keyColumn: "Id",
                keyValue: "105B5539-879E-4F8C-B6F1-2C493CF81FAB",
                columns: new[] { "CollectLocation", "CollectPerson" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                table: "IncidentTypes",
                keyColumn: "Id",
                keyValue: "A7A975E0-5952-434E-9D87-F8B049D84016",
                column: "CollectPerson",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IncidentTypes",
                keyColumn: "Id",
                keyValue: "105B5539-879E-4F8C-B6F1-2C493CF81FAB",
                columns: new[] { "CollectLocation", "CollectPerson" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "IncidentTypes",
                keyColumn: "Id",
                keyValue: "A7A975E0-5952-434E-9D87-F8B049D84016",
                column: "CollectPerson",
                value: false);
        }
    }
}
