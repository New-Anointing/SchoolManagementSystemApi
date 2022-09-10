using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagementSystemApi.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "OrganisationReg",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "OrganisationReg",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "OrganisationReg",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "OrganisationReg",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "OrganisationReg",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "OrganisationReg",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "OrganisationReg",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "OrganisationReg",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "OrganisationReg",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "OrganisationReg",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "OrganisationReg",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "OrganisationReg");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "OrganisationReg");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "OrganisationReg");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "OrganisationReg");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "OrganisationReg");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "OrganisationReg");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "OrganisationReg");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "OrganisationReg");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "OrganisationReg");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "OrganisationReg");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "OrganisationReg");
        }
    }
}
