using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aurora.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdjustingReportingOptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "87D773C9-9420-4B42-857D-3DB4783476BC", "105B5539-879E-4F8C-B6F1-2C493CF81FAB" });

            migrationBuilder.AddColumn<bool>(
                name: "ShowBaptisms",
                table: "IncidentTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ShowBibleStudies",
                table: "IncidentTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ShowCounselingOpportunities",
                table: "IncidentTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ShowGospelPresentations",
                table: "IncidentTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ShowProfessionsOfFaith",
                table: "IncidentTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CollectLocation",
                table: "AgencyIncidentType",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CollectPerson",
                table: "AgencyIncidentType",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CollectTime",
                table: "AgencyIncidentType",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RequiresTime",
                table: "AgencyIncidentType",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ShowBaptisms",
                table: "AgencyIncidentType",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ShowBibleStudies",
                table: "AgencyIncidentType",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ShowCounselingOpportunities",
                table: "AgencyIncidentType",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ShowGospelPresentations",
                table: "AgencyIncidentType",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ShowProfessionsOfFaith",
                table: "AgencyIncidentType",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "50978BC3-DC9B-496A-84BB-53071C081BFC", "03A14A69-C6B7-4573-B95E-12574354C65B" },
                columns: new[] { "CollectLocation", "CollectPerson", "CollectTime", "RequiresTime", "ShowBaptisms", "ShowBibleStudies", "ShowCounselingOpportunities", "ShowGospelPresentations", "ShowProfessionsOfFaith" },
                values: new object[] { null, null, null, null, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "50978BC3-DC9B-496A-84BB-53071C081BFC", "105B5539-879E-4F8C-B6F1-2C493CF81FAB" },
                columns: new[] { "CollectLocation", "CollectPerson", "CollectTime", "RequiresTime", "ShowBaptisms", "ShowBibleStudies", "ShowCounselingOpportunities", "ShowGospelPresentations", "ShowProfessionsOfFaith" },
                values: new object[] { null, null, null, null, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "50978BC3-DC9B-496A-84BB-53071C081BFC", "199E7EA4-9AD2-4221-8A9D-F410621AA3CC" },
                columns: new[] { "CollectLocation", "CollectPerson", "CollectTime", "RequiresTime", "ShowBaptisms", "ShowBibleStudies", "ShowCounselingOpportunities", "ShowGospelPresentations", "ShowProfessionsOfFaith" },
                values: new object[] { null, null, null, null, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "50978BC3-DC9B-496A-84BB-53071C081BFC", "5D035B97-5CB0-4FA9-978E-7B34A250426E" },
                columns: new[] { "CollectLocation", "CollectPerson", "CollectTime", "RequiresTime", "ShowBaptisms", "ShowBibleStudies", "ShowCounselingOpportunities", "ShowGospelPresentations", "ShowProfessionsOfFaith" },
                values: new object[] { null, null, null, null, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "50978BC3-DC9B-496A-84BB-53071C081BFC", "A7A975E0-5952-434E-9D87-F8B049D84016" },
                columns: new[] { "CollectLocation", "CollectPerson", "CollectTime", "RequiresTime", "ShowBaptisms", "ShowBibleStudies", "ShowCounselingOpportunities", "ShowGospelPresentations", "ShowProfessionsOfFaith" },
                values: new object[] { null, null, null, null, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "50978BC3-DC9B-496A-84BB-53071C081BFC", "AB0F1C6E-E6E5-489D-A88E-8010AB8A358A" },
                columns: new[] { "CollectLocation", "CollectPerson", "CollectTime", "RequiresTime", "ShowBaptisms", "ShowBibleStudies", "ShowCounselingOpportunities", "ShowGospelPresentations", "ShowProfessionsOfFaith" },
                values: new object[] { null, null, null, null, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "50978BC3-DC9B-496A-84BB-53071C081BFC", "EB4F4F16-7B39-448D-9215-B578335F08DE" },
                columns: new[] { "CollectLocation", "CollectPerson", "CollectTime", "RequiresTime", "ShowBaptisms", "ShowBibleStudies", "ShowCounselingOpportunities", "ShowGospelPresentations", "ShowProfessionsOfFaith" },
                values: new object[] { null, null, null, null, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "87D773C9-9420-4B42-857D-3DB4783476BC", "5D035B97-5CB0-4FA9-978E-7B34A250426E" },
                columns: new[] { "CollectLocation", "CollectPerson", "CollectTime", "RequiresTime", "ShowBaptisms", "ShowBibleStudies", "ShowCounselingOpportunities", "ShowGospelPresentations", "ShowProfessionsOfFaith" },
                values: new object[] { null, null, null, null, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "87D773C9-9420-4B42-857D-3DB4783476BC", "A7A975E0-5952-434E-9D87-F8B049D84016" },
                columns: new[] { "CollectLocation", "CollectPerson", "CollectTime", "RequiresTime", "ShowBaptisms", "ShowBibleStudies", "ShowCounselingOpportunities", "ShowGospelPresentations", "ShowProfessionsOfFaith" },
                values: new object[] { null, null, null, null, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "87D773C9-9420-4B42-857D-3DB4783476BC", "AB0F1C6E-E6E5-489D-A88E-8010AB8A358A" },
                columns: new[] { "CollectLocation", "CollectPerson", "CollectTime", "RequiresTime", "ShowBaptisms", "ShowBibleStudies", "ShowCounselingOpportunities", "ShowGospelPresentations", "ShowProfessionsOfFaith" },
                values: new object[] { null, null, null, null, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "87D773C9-9420-4B42-857D-3DB4783476BC", "EB4F4F16-7B39-448D-9215-B578335F08DE" },
                columns: new[] { "CollectLocation", "CollectPerson", "CollectTime", "RequiresTime", "ShowBaptisms", "ShowBibleStudies", "ShowCounselingOpportunities", "ShowGospelPresentations", "ShowProfessionsOfFaith" },
                values: new object[] { null, null, null, null, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "BAD172CE-D1D5-4AD0-809C-9BD04D6D22AC", "105B5539-879E-4F8C-B6F1-2C493CF81FAB" },
                columns: new[] { "CollectLocation", "CollectPerson", "CollectTime", "RequiresTime", "ShowBaptisms", "ShowBibleStudies", "ShowCounselingOpportunities", "ShowGospelPresentations", "ShowProfessionsOfFaith" },
                values: new object[] { null, null, null, null, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "BAD172CE-D1D5-4AD0-809C-9BD04D6D22AC", "5D035B97-5CB0-4FA9-978E-7B34A250426E" },
                columns: new[] { "CollectLocation", "CollectPerson", "CollectTime", "RequiresTime", "ShowBaptisms", "ShowBibleStudies", "ShowCounselingOpportunities", "ShowGospelPresentations", "ShowProfessionsOfFaith" },
                values: new object[] { null, null, null, null, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "BAD172CE-D1D5-4AD0-809C-9BD04D6D22AC", "A7A975E0-5952-434E-9D87-F8B049D84016" },
                columns: new[] { "CollectLocation", "CollectPerson", "CollectTime", "RequiresTime", "ShowBaptisms", "ShowBibleStudies", "ShowCounselingOpportunities", "ShowGospelPresentations", "ShowProfessionsOfFaith" },
                values: new object[] { null, null, null, null, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "BAD172CE-D1D5-4AD0-809C-9BD04D6D22AC", "AB0F1C6E-E6E5-489D-A88E-8010AB8A358A" },
                columns: new[] { "CollectLocation", "CollectPerson", "CollectTime", "RequiresTime", "ShowBaptisms", "ShowBibleStudies", "ShowCounselingOpportunities", "ShowGospelPresentations", "ShowProfessionsOfFaith" },
                values: new object[] { null, null, null, null, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "AgencyIncidentType",
                keyColumns: new[] { "AgencyId", "IncidentTypeId" },
                keyValues: new object[] { "BAD172CE-D1D5-4AD0-809C-9BD04D6D22AC", "EB4F4F16-7B39-448D-9215-B578335F08DE" },
                columns: new[] { "CollectLocation", "CollectPerson", "CollectTime", "RequiresTime", "ShowBaptisms", "ShowBibleStudies", "ShowCounselingOpportunities", "ShowGospelPresentations", "ShowProfessionsOfFaith" },
                values: new object[] { null, null, null, null, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "IncidentTypes",
                keyColumn: "Id",
                keyValue: "03A14A69-C6B7-4573-B95E-12574354C65B",
                columns: new[] { "ShowBaptisms", "ShowBibleStudies", "ShowCounselingOpportunities", "ShowGospelPresentations", "ShowProfessionsOfFaith" },
                values: new object[] { false, false, false, false, false });

            migrationBuilder.UpdateData(
                table: "IncidentTypes",
                keyColumn: "Id",
                keyValue: "105B5539-879E-4F8C-B6F1-2C493CF81FAB",
                columns: new[] { "ShowBaptisms", "ShowBibleStudies", "ShowCounselingOpportunities", "ShowGospelPresentations", "ShowProfessionsOfFaith" },
                values: new object[] { false, false, true, false, false });

            migrationBuilder.UpdateData(
                table: "IncidentTypes",
                keyColumn: "Id",
                keyValue: "199E7EA4-9AD2-4221-8A9D-F410621AA3CC",
                columns: new[] { "ShowBaptisms", "ShowBibleStudies", "ShowCounselingOpportunities", "ShowGospelPresentations", "ShowProfessionsOfFaith" },
                values: new object[] { false, false, false, false, false });

            migrationBuilder.UpdateData(
                table: "IncidentTypes",
                keyColumn: "Id",
                keyValue: "5D035B97-5CB0-4FA9-978E-7B34A250426E",
                columns: new[] { "ShowBaptisms", "ShowBibleStudies", "ShowCounselingOpportunities", "ShowGospelPresentations", "ShowProfessionsOfFaith" },
                values: new object[] { false, false, false, false, false });

            migrationBuilder.UpdateData(
                table: "IncidentTypes",
                keyColumn: "Id",
                keyValue: "A7A975E0-5952-434E-9D87-F8B049D84016",
                columns: new[] { "ShowBaptisms", "ShowBibleStudies", "ShowCounselingOpportunities", "ShowGospelPresentations", "ShowProfessionsOfFaith" },
                values: new object[] { false, false, true, false, false });

            migrationBuilder.UpdateData(
                table: "IncidentTypes",
                keyColumn: "Id",
                keyValue: "AB0F1C6E-E6E5-489D-A88E-8010AB8A358A",
                columns: new[] { "ShowBaptisms", "ShowBibleStudies", "ShowCounselingOpportunities", "ShowGospelPresentations", "ShowProfessionsOfFaith" },
                values: new object[] { false, false, false, false, false });

            migrationBuilder.UpdateData(
                table: "IncidentTypes",
                keyColumn: "Id",
                keyValue: "EB4F4F16-7B39-448D-9215-B578335F08DE",
                columns: new[] { "ShowBaptisms", "ShowBibleStudies", "ShowCounselingOpportunities", "ShowGospelPresentations", "ShowProfessionsOfFaith" },
                values: new object[] { false, false, true, false, false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowBaptisms",
                table: "IncidentTypes");

            migrationBuilder.DropColumn(
                name: "ShowBibleStudies",
                table: "IncidentTypes");

            migrationBuilder.DropColumn(
                name: "ShowCounselingOpportunities",
                table: "IncidentTypes");

            migrationBuilder.DropColumn(
                name: "ShowGospelPresentations",
                table: "IncidentTypes");

            migrationBuilder.DropColumn(
                name: "ShowProfessionsOfFaith",
                table: "IncidentTypes");

            migrationBuilder.DropColumn(
                name: "CollectLocation",
                table: "AgencyIncidentType");

            migrationBuilder.DropColumn(
                name: "CollectPerson",
                table: "AgencyIncidentType");

            migrationBuilder.DropColumn(
                name: "CollectTime",
                table: "AgencyIncidentType");

            migrationBuilder.DropColumn(
                name: "RequiresTime",
                table: "AgencyIncidentType");

            migrationBuilder.DropColumn(
                name: "ShowBaptisms",
                table: "AgencyIncidentType");

            migrationBuilder.DropColumn(
                name: "ShowBibleStudies",
                table: "AgencyIncidentType");

            migrationBuilder.DropColumn(
                name: "ShowCounselingOpportunities",
                table: "AgencyIncidentType");

            migrationBuilder.DropColumn(
                name: "ShowGospelPresentations",
                table: "AgencyIncidentType");

            migrationBuilder.DropColumn(
                name: "ShowProfessionsOfFaith",
                table: "AgencyIncidentType");

            migrationBuilder.InsertData(
                table: "AgencyIncidentType",
                columns: new[] { "AgencyId", "IncidentTypeId" },
                values: new object[] { "87D773C9-9420-4B42-857D-3DB4783476BC", "105B5539-879E-4F8C-B6F1-2C493CF81FAB" });
        }
    }
}
