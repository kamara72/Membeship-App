using Microsoft.EntityFrameworkCore.Migrations;

namespace MembershipProjectApp.Migrations
{
    public partial class AddedNewColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Language",
                table: "Memberships",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Dialect",
                table: "Memberships",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DialectTwo",
                table: "Memberships",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LanguageThree",
                table: "Memberships",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LanguageTwo",
                table: "Memberships",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherDialectandLanguage",
                table: "Memberships",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Language",
                table: "Memberships",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
