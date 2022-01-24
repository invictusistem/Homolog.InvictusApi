using Microsoft.EntityFrameworkCore.Migrations;

namespace Invictus.Data.Migrations
{
    public partial class update18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Command",
                table: "LogMatriculas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "LogLogins",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Command",
                table: "LogMatriculas");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "LogLogins");
        }
    }
}
