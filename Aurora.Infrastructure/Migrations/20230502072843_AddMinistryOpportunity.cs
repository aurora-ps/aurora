using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aurora.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMinistryOpportunity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Location_LocationType",
                table: "Reports",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "MinistryOpportunity_Baptisms",
                table: "Reports",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinistryOpportunity_BibleStudies",
                table: "Reports",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinistryOpportunity_CounselingOpportunities",
                table: "Reports",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinistryOpportunity_GospelPresentations",
                table: "Reports",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinistryOpportunity_ProfessionsOfFaith",
                table: "Reports",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinistryOpportunity_Baptisms",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "MinistryOpportunity_BibleStudies",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "MinistryOpportunity_CounselingOpportunities",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "MinistryOpportunity_GospelPresentations",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "MinistryOpportunity_ProfessionsOfFaith",
                table: "Reports");

            migrationBuilder.AlterColumn<int>(
                name: "Location_LocationType",
                table: "Reports",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
