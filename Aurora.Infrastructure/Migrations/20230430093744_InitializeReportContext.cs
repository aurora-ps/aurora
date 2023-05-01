using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aurora.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitializeReportContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agency",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IncidentType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CollectTime = table.Column<bool>(type: "bit", nullable: false),
                    RequiresTime = table.Column<bool>(type: "bit", nullable: false),
                    CollectLocation = table.Column<bool>(type: "bit", nullable: false),
                    CollectPerson = table.Column<bool>(type: "bit", nullable: false),
                    AgencyId = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncidentType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IncidentType_Agency_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agency",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Time = table.Column<TimeSpan>(type: "time", nullable: true),
                    Miles = table.Column<double>(type: "float", nullable: true),
                    Location_Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Location_City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Location_State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Location_Zip = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Location_LocationType = table.Column<int>(type: "int", nullable: false),
                    Narrative = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuroraUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AgencyId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    IncidentTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_Agency_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reports_AspNetUsers_AuroraUserId",
                        column: x => x.AuroraUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reports_IncidentType_IncidentTypeId",
                        column: x => x.IncidentTypeId,
                        principalTable: "IncidentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportPerson",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Location_Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Location_City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Location_State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Location_Zip = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Location_LocationType = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber_Number = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PhoneNumber_Type = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ReportId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportPerson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportPerson_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_IncidentType_AgencyId",
                table: "IncidentType",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportPerson_ReportId",
                table: "ReportPerson",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_AgencyId",
                table: "Reports",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_AuroraUserId",
                table: "Reports",
                column: "AuroraUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_IncidentTypeId",
                table: "Reports",
                column: "IncidentTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportPerson");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "IncidentType");

            migrationBuilder.DropTable(
                name: "Agency");
        }
    }
}
