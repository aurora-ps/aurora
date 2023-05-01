using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Aurora.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyInitialSeedDataKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "DurhamCRT", "Administration" });

            migrationBuilder.DeleteData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "DurhamCRT", "CrisisCall" });

            migrationBuilder.DeleteData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "DurhamCRT", "DeathCall" });

            migrationBuilder.DeleteData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "DurhamCRT", "Other" });

            migrationBuilder.DeleteData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "DurhamCRT", "Training" });

            migrationBuilder.DeleteData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "PersonCRT", "Administration" });

            migrationBuilder.DeleteData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "PersonCRT", "CrisisCall" });

            migrationBuilder.DeleteData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "PersonCRT", "DeathCall" });

            migrationBuilder.DeleteData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "PersonCRT", "Other" });

            migrationBuilder.DeleteData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "PersonCRT", "Training" });

            migrationBuilder.DeleteData(
                table: "Agencies",
                keyColumn: "Id",
                keyValue: "DurhamCRT");

            migrationBuilder.DeleteData(
                table: "Agencies",
                keyColumn: "Id",
                keyValue: "PersonCRT");

            migrationBuilder.DeleteData(
                table: "IncidentTypes",
                keyColumn: "Id",
                keyValue: "Administration");

            migrationBuilder.DeleteData(
                table: "IncidentTypes",
                keyColumn: "Id",
                keyValue: "CrisisCall");

            migrationBuilder.DeleteData(
                table: "IncidentTypes",
                keyColumn: "Id",
                keyValue: "DeathCall");

            migrationBuilder.DeleteData(
                table: "IncidentTypes",
                keyColumn: "Id",
                keyValue: "Other");

            migrationBuilder.DeleteData(
                table: "IncidentTypes",
                keyColumn: "Id",
                keyValue: "Training");

            migrationBuilder.InsertData(
                table: "Agencies",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "87D773C9-9420-4B42-857D-3DB4783476BC", "Durham - CRT" },
                    { "BAD172CE-D1D5-4AD0-809C-9BD04D6D22AC", "Person - CRT" }
                });

            migrationBuilder.InsertData(
                table: "IncidentTypes",
                columns: new[] { "Id", "CollectLocation", "CollectPerson", "CollectTime", "Description", "Name", "RequiresTime" },
                values: new object[,]
                {
                    { "105B5539-879E-4F8C-B6F1-2C493CF81FAB", false, false, true, null, "Other", false },
                    { "5D035B97-5CB0-4FA9-978E-7B34A250426E", true, false, true, null, "Training", true },
                    { "A7A975E0-5952-434E-9D87-F8B049D84016", true, false, true, null, "Crisis Call", true },
                    { "AB0F1C6E-E6E5-489D-A88E-8010AB8A358A", false, false, false, null, "Administration", false },
                    { "EB4F4F16-7B39-448D-9215-B578335F08DE", true, true, true, null, "Death Call", true }
                });

            migrationBuilder.InsertData(
                table: "AgencyIncidentType",
                columns: new[] { "AgencyId", "IncidentTypeId" },
                values: new object[,]
                {
                    { "87D773C9-9420-4B42-857D-3DB4783476BC", "105B5539-879E-4F8C-B6F1-2C493CF81FAB" },
                    { "87D773C9-9420-4B42-857D-3DB4783476BC", "5D035B97-5CB0-4FA9-978E-7B34A250426E" },
                    { "87D773C9-9420-4B42-857D-3DB4783476BC", "A7A975E0-5952-434E-9D87-F8B049D84016" },
                    { "87D773C9-9420-4B42-857D-3DB4783476BC", "AB0F1C6E-E6E5-489D-A88E-8010AB8A358A" },
                    { "87D773C9-9420-4B42-857D-3DB4783476BC", "EB4F4F16-7B39-448D-9215-B578335F08DE" },
                    { "BAD172CE-D1D5-4AD0-809C-9BD04D6D22AC", "105B5539-879E-4F8C-B6F1-2C493CF81FAB" },
                    { "BAD172CE-D1D5-4AD0-809C-9BD04D6D22AC", "5D035B97-5CB0-4FA9-978E-7B34A250426E" },
                    { "BAD172CE-D1D5-4AD0-809C-9BD04D6D22AC", "A7A975E0-5952-434E-9D87-F8B049D84016" },
                    { "BAD172CE-D1D5-4AD0-809C-9BD04D6D22AC", "AB0F1C6E-E6E5-489D-A88E-8010AB8A358A" },
                    { "BAD172CE-D1D5-4AD0-809C-9BD04D6D22AC", "EB4F4F16-7B39-448D-9215-B578335F08DE" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "87D773C9-9420-4B42-857D-3DB4783476BC", "105B5539-879E-4F8C-B6F1-2C493CF81FAB" });

            migrationBuilder.DeleteData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "87D773C9-9420-4B42-857D-3DB4783476BC", "5D035B97-5CB0-4FA9-978E-7B34A250426E" });

            migrationBuilder.DeleteData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "87D773C9-9420-4B42-857D-3DB4783476BC", "A7A975E0-5952-434E-9D87-F8B049D84016" });

            migrationBuilder.DeleteData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "87D773C9-9420-4B42-857D-3DB4783476BC", "AB0F1C6E-E6E5-489D-A88E-8010AB8A358A" });

            migrationBuilder.DeleteData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "87D773C9-9420-4B42-857D-3DB4783476BC", "EB4F4F16-7B39-448D-9215-B578335F08DE" });

            migrationBuilder.DeleteData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "BAD172CE-D1D5-4AD0-809C-9BD04D6D22AC", "105B5539-879E-4F8C-B6F1-2C493CF81FAB" });

            migrationBuilder.DeleteData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "BAD172CE-D1D5-4AD0-809C-9BD04D6D22AC", "5D035B97-5CB0-4FA9-978E-7B34A250426E" });

            migrationBuilder.DeleteData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "BAD172CE-D1D5-4AD0-809C-9BD04D6D22AC", "A7A975E0-5952-434E-9D87-F8B049D84016" });

            migrationBuilder.DeleteData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "BAD172CE-D1D5-4AD0-809C-9BD04D6D22AC", "AB0F1C6E-E6E5-489D-A88E-8010AB8A358A" });

            migrationBuilder.DeleteData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "BAD172CE-D1D5-4AD0-809C-9BD04D6D22AC", "EB4F4F16-7B39-448D-9215-B578335F08DE" });

            migrationBuilder.DeleteData(
                table: "Agencies",
                keyColumn: "Id",
                keyValue: "87D773C9-9420-4B42-857D-3DB4783476BC");

            migrationBuilder.DeleteData(
                table: "Agencies",
                keyColumn: "Id",
                keyValue: "BAD172CE-D1D5-4AD0-809C-9BD04D6D22AC");

            migrationBuilder.DeleteData(
                table: "IncidentTypes",
                keyColumn: "Id",
                keyValue: "105B5539-879E-4F8C-B6F1-2C493CF81FAB");

            migrationBuilder.DeleteData(
                table: "IncidentTypes",
                keyColumn: "Id",
                keyValue: "5D035B97-5CB0-4FA9-978E-7B34A250426E");

            migrationBuilder.DeleteData(
                table: "IncidentTypes",
                keyColumn: "Id",
                keyValue: "A7A975E0-5952-434E-9D87-F8B049D84016");

            migrationBuilder.DeleteData(
                table: "IncidentTypes",
                keyColumn: "Id",
                keyValue: "AB0F1C6E-E6E5-489D-A88E-8010AB8A358A");

            migrationBuilder.DeleteData(
                table: "IncidentTypes",
                keyColumn: "Id",
                keyValue: "EB4F4F16-7B39-448D-9215-B578335F08DE");

            migrationBuilder.InsertData(
                table: "Agencies",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "DurhamCRT", "Durham - CRT" },
                    { "PersonCRT", "Person - CRT" }
                });

            migrationBuilder.InsertData(
                table: "IncidentTypes",
                columns: new[] { "Id", "CollectLocation", "CollectPerson", "CollectTime", "Description", "Name", "RequiresTime" },
                values: new object[,]
                {
                    { "Administration", false, false, false, null, "Administration", false },
                    { "CrisisCall", true, false, true, null, "Crisis Call", true },
                    { "DeathCall", true, true, true, null, "Death Call", true },
                    { "Other", false, false, true, null, "Other", false },
                    { "Training", true, false, true, null, "Training", true }
                });

            migrationBuilder.InsertData(
                table: "AgencyIncidentType",
                columns: new[] { "AgencyId", "IncidentTypeId" },
                values: new object[,]
                {
                    { "DurhamCRT", "Administration" },
                    { "DurhamCRT", "CrisisCall" },
                    { "DurhamCRT", "DeathCall" },
                    { "DurhamCRT", "Other" },
                    { "DurhamCRT", "Training" },
                    { "PersonCRT", "Administration" },
                    { "PersonCRT", "CrisisCall" },
                    { "PersonCRT", "DeathCall" },
                    { "PersonCRT", "Other" },
                    { "PersonCRT", "Training" }
                });
        }
    }
}
