using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Aurora.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDataForAgencyIncidentTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
