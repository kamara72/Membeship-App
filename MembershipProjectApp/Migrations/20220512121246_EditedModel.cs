using Microsoft.EntityFrameworkCore.Migrations;

namespace MembershipProjectApp.Migrations
{
    public partial class EditedModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dialect",
                table: "Memberships");

            migrationBuilder.DropColumn(
                name: "DialectTwo",
                table: "Memberships");

            migrationBuilder.DropColumn(
                name: "LanguageThree",
                table: "Memberships");

            migrationBuilder.DropColumn(
                name: "LanguageTwo",
                table: "Memberships");

            migrationBuilder.DropColumn(
                name: "OtherDialectandLanguage",
                table: "Memberships");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Dialect",
                table: "Memberships",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DialectTwo",
                table: "Memberships",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LanguageThree",
                table: "Memberships",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LanguageTwo",
                table: "Memberships",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherDialectandLanguage",
                table: "Memberships",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
