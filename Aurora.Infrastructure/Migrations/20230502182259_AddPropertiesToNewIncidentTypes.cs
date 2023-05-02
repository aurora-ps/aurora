using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aurora.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertiesToNewIncidentTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IncidentTypes",
                keyColumn: "Id",
                keyValue: "03A14A69-C6B7-4573-B95E-12574354C65B",
                columns: new[] { "CollectLocation", "CollectTime" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                table: "IncidentTypes",
                keyColumn: "Id",
                keyValue: "199E7EA4-9AD2-4221-8A9D-F410621AA3CC",
                columns: new[] { "CollectLocation", "CollectTime" },
                values: new object[] { true, true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IncidentTypes",
                keyColumn: "Id",
                keyValue: "03A14A69-C6B7-4573-B95E-12574354C65B",
                columns: new[] { "CollectLocation", "CollectTime" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "IncidentTypes",
                keyColumn: "Id",
                keyValue: "199E7EA4-9AD2-4221-8A9D-F410621AA3CC",
                columns: new[] { "CollectLocation", "CollectTime" },
                values: new object[] { false, false });
        }
    }
}
