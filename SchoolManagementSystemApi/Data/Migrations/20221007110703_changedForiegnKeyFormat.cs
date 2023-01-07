using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagementSystemApi.Data.Migrations
{
    public partial class changedForiegnKeyFormat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeTable_ClassRoom_ClassId",
                table: "TimeTable");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeTable_Subjects_SubjectId",
                table: "TimeTable");

            migrationBuilder.RenameColumn(
                name: "SubjectId",
                table: "TimeTable",
                newName: "SubjectsId");

            migrationBuilder.RenameColumn(
                name: "ClassId",
                table: "TimeTable",
                newName: "ClassRoomId");

            migrationBuilder.RenameIndex(
                name: "IX_TimeTable_SubjectId",
                table: "TimeTable",
                newName: "IX_TimeTable_SubjectsId");

            migrationBuilder.RenameIndex(
                name: "IX_TimeTable_ClassId",
                table: "TimeTable",
                newName: "IX_TimeTable_ClassRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTable_ClassRoom_ClassRoomId",
                table: "TimeTable",
                column: "ClassRoomId",
                principalTable: "ClassRoom",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTable_Subjects_SubjectsId",
                table: "TimeTable",
                column: "SubjectsId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeTable_ClassRoom_ClassRoomId",
                table: "TimeTable");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeTable_Subjects_SubjectsId",
                table: "TimeTable");

            migrationBuilder.RenameColumn(
                name: "SubjectsId",
                table: "TimeTable",
                newName: "SubjectId");

            migrationBuilder.RenameColumn(
                name: "ClassRoomId",
                table: "TimeTable",
                newName: "ClassId");

            migrationBuilder.RenameIndex(
                name: "IX_TimeTable_SubjectsId",
                table: "TimeTable",
                newName: "IX_TimeTable_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_TimeTable_ClassRoomId",
                table: "TimeTable",
                newName: "IX_TimeTable_ClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTable_ClassRoom_ClassId",
                table: "TimeTable",
                column: "ClassId",
                principalTable: "ClassRoom",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTable_Subjects_SubjectId",
                table: "TimeTable",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
