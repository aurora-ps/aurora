using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Aurora.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewIncidentTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "IncidentTypes",
                columns: new[] { "Id", "CollectLocation", "CollectPerson", "CollectTime", "Description", "Name", "RequiresTime" },
                values: new object[,]
                {
                    { "03A14A69-C6B7-4573-B95E-12574354C65B", false, false, false, null, "Roll Call/Meeting", false },
                    { "199E7EA4-9AD2-4221-8A9D-F410621AA3CC", false, false, false, null, "Ride Along", false }
                });

            migrationBuilder.InsertData(
                table: "AgencyIncidentType",
                columns: new[] { "AgencyId", "IncidentTypeId" },
                values: new object[,]
                {
                    { "50978BC3-DC9B-496A-84BB-53071C081BFC", "03A14A69-C6B7-4573-B95E-12574354C65B" },
                    { "50978BC3-DC9B-496A-84BB-53071C081BFC", "199E7EA4-9AD2-4221-8A9D-F410621AA3CC" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "50978BC3-DC9B-496A-84BB-53071C081BFC", "03A14A69-C6B7-4573-B95E-12574354C65B" });

            migrationBuilder.DeleteData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "50978BC3-DC9B-496A-84BB-53071C081BFC", "199E7EA4-9AD2-4221-8A9D-F410621AA3CC" });

            migrationBuilder.DeleteData(
                table: "IncidentTypes",
                keyColumn: "Id",
                keyValue: "03A14A69-C6B7-4573-B95E-12574354C65B");

            migrationBuilder.DeleteData(
                table: "IncidentTypes",
                keyColumn: "Id",
                keyValue: "199E7EA4-9AD2-4221-8A9D-F410621AA3CC");
        }
    }
}
