using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagementSystemApi.Data.Migrations
{
    public partial class madechangestoteachersmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_ClassRoom_ClassRoomId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_ClassRoomId",
                table: "Teachers");

            migrationBuilder.RenameColumn(
                name: "ClassRoomId",
                table: "Teachers",
                newName: "ClassRoomID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClassRoomID",
                table: "Teachers",
                newName: "ClassRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_ClassRoomId",
                table: "Teachers",
                column: "ClassRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_ClassRoom_ClassRoomId",
                table: "Teachers",
                column: "ClassRoomId",
                principalTable: "ClassRoom",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
