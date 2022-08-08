using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MembershipProjectApp.Migrations
{
    public partial class AddedUserRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateRecorded",
                table: "Memberships",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "RecordedBy",
                table: "Memberships",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateRecorded",
                table: "Memberships");

            migrationBuilder.DropColumn(
                name: "RecordedBy",
                table: "Memberships");
        }
    }
}
