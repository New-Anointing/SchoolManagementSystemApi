using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagementSystemApi.Data.Migrations
{
    public partial class changestoparentdatabasetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Parents_ParentsId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Parents_Students_StudentsId",
                table: "Parents");

            migrationBuilder.DropIndex(
                name: "IX_Parents_StudentsId",
                table: "Parents");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ParentsId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StudentsId",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "ParentsId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "ParentsStudents",
                columns: table => new
                {
                    ParentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentsStudents", x => new { x.ParentsId, x.StudentUserId });
                    table.ForeignKey(
                        name: "FK_ParentsStudents_Parents_ParentsId",
                        column: x => x.ParentsId,
                        principalTable: "Parents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ParentsStudents_Students_StudentUserId",
                        column: x => x.StudentUserId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParentsStudents_StudentUserId",
                table: "ParentsStudents",
                column: "StudentUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParentsStudents");

            migrationBuilder.AddColumn<Guid>(
                name: "StudentsId",
                table: "Parents",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentsId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parents_StudentsId",
                table: "Parents",
                column: "StudentsId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ParentsId",
                table: "AspNetUsers",
                column: "ParentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Parents_ParentsId",
                table: "AspNetUsers",
                column: "ParentsId",
                principalTable: "Parents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_Students_StudentsId",
                table: "Parents",
                column: "StudentsId",
                principalTable: "Students",
                principalColumn: "Id");
        }
    }
}
