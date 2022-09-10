using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagementSystemApi.Data.Migrations
{
    public partial class updatedOrganisationRegmodelandcreatedOrganisationmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrgId",
                table: "OrganisationReg",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Organisation",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrganisationName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organisation", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Organisation");

            migrationBuilder.DropColumn(
                name: "OrgId",
                table: "OrganisationReg");
        }
    }
}
