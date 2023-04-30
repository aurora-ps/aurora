using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aurora.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIncidentTypesManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IncidentType_Agency_AgencyId",
                table: "IncidentType");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Agency_AgencyId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_IncidentType_IncidentTypeId",
                table: "Reports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IncidentType",
                table: "IncidentType");

            migrationBuilder.DropIndex(
                name: "IX_IncidentType_AgencyId",
                table: "IncidentType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agency",
                table: "Agency");

            migrationBuilder.DropColumn(
                name: "AgencyId",
                table: "IncidentType");

            migrationBuilder.RenameTable(
                name: "IncidentType",
                newName: "IncidentTypes");

            migrationBuilder.RenameTable(
                name: "Agency",
                newName: "Agencies");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IncidentTypes",
                table: "IncidentTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agencies",
                table: "Agencies",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AgencyIncidentType",
                columns: table => new
                {
                    AgencyId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    IncidentTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyIncidentType", x => new { x.AgencyId, x.IncidentTypeId });
                    table.ForeignKey(
                        name: "FK_AgencyIncidentType_Agencies_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgencyIncidentType_IncidentTypes_IncidentTypeId",
                        column: x => x.IncidentTypeId,
                        principalTable: "IncidentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgencyIncidentType_IncidentTypeId",
                table: "AgencyIncidentType",
                column: "IncidentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Agencies_AgencyId",
                table: "Reports",
                column: "AgencyId",
                principalTable: "Agencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_IncidentTypes_IncidentTypeId",
                table: "Reports",
                column: "IncidentTypeId",
                principalTable: "IncidentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Agencies_AgencyId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_IncidentTypes_IncidentTypeId",
                table: "Reports");

            migrationBuilder.DropTable(
                name: "AgencyIncidentType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IncidentTypes",
                table: "IncidentTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agencies",
                table: "Agencies");

            migrationBuilder.RenameTable(
                name: "IncidentTypes",
                newName: "IncidentType");

            migrationBuilder.RenameTable(
                name: "Agencies",
                newName: "Agency");

            migrationBuilder.AddColumn<string>(
                name: "AgencyId",
                table: "IncidentType",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_IncidentType",
                table: "IncidentType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agency",
                table: "Agency",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_IncidentType_AgencyId",
                table: "IncidentType",
                column: "AgencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_IncidentType_Agency_AgencyId",
                table: "IncidentType",
                column: "AgencyId",
                principalTable: "Agency",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Agency_AgencyId",
                table: "Reports",
                column: "AgencyId",
                principalTable: "Agency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_IncidentType_IncidentTypeId",
                table: "Reports",
                column: "IncidentTypeId",
                principalTable: "IncidentType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
