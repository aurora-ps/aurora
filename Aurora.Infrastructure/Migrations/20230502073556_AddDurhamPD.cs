using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Aurora.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDurhamPD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Agencies",
                columns: new[] { "Id", "Name" },
                values: new object[] { "50978BC3-DC9B-496A-84BB-53071C081BFC", "Durham PD" });

            migrationBuilder.InsertData(
                table: "AgencyIncidentType",
                columns: new[] { "AgencyId", "IncidentTypeId" },
                values: new object[,]
                {
                    { "50978BC3-DC9B-496A-84BB-53071C081BFC", "105B5539-879E-4F8C-B6F1-2C493CF81FAB" },
                    { "50978BC3-DC9B-496A-84BB-53071C081BFC", "5D035B97-5CB0-4FA9-978E-7B34A250426E" },
                    { "50978BC3-DC9B-496A-84BB-53071C081BFC", "A7A975E0-5952-434E-9D87-F8B049D84016" },
                    { "50978BC3-DC9B-496A-84BB-53071C081BFC", "AB0F1C6E-E6E5-489D-A88E-8010AB8A358A" },
                    { "50978BC3-DC9B-496A-84BB-53071C081BFC", "EB4F4F16-7B39-448D-9215-B578335F08DE" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "50978BC3-DC9B-496A-84BB-53071C081BFC", "105B5539-879E-4F8C-B6F1-2C493CF81FAB" });

            migrationBuilder.DeleteData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "50978BC3-DC9B-496A-84BB-53071C081BFC", "5D035B97-5CB0-4FA9-978E-7B34A250426E" });

            migrationBuilder.DeleteData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "50978BC3-DC9B-496A-84BB-53071C081BFC", "A7A975E0-5952-434E-9D87-F8B049D84016" });

            migrationBuilder.DeleteData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "50978BC3-DC9B-496A-84BB-53071C081BFC", "AB0F1C6E-E6E5-489D-A88E-8010AB8A358A" });

            migrationBuilder.DeleteData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "50978BC3-DC9B-496A-84BB-53071C081BFC", "EB4F4F16-7B39-448D-9215-B578335F08DE" });

            migrationBuilder.DeleteData(
                table: "Agencies",
                keyColumn: "Id",
                keyValue: "50978BC3-DC9B-496A-84BB-53071C081BFC");
        }
    }
}
