using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagementSystemApi.Data.Migrations
{
    public partial class updatedstudentsdbmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Students_StudentsId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_StudentsId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "StudentsId",
                table: "Subjects");

            migrationBuilder.CreateTable(
                name: "StudentsSubjects",
                columns: table => new
                {
                    StudentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentsSubjects", x => new { x.StudentsId, x.SubjectsId });
                    table.ForeignKey(
                        name: "FK_StudentsSubjects_Students_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentsSubjects_Subjects_SubjectsId",
                        column: x => x.SubjectsId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentsSubjects_SubjectsId",
                table: "StudentsSubjects",
                column: "SubjectsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentsSubjects");

            migrationBuilder.AddColumn<Guid>(
                name: "StudentsId",
                table: "Subjects",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_StudentsId",
                table: "Subjects",
                column: "StudentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Students_StudentsId",
                table: "Subjects",
                column: "StudentsId",
                principalTable: "Students",
                principalColumn: "Id");
        }
    }
}
