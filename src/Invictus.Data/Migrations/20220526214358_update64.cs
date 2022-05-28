using Microsoft.EntityFrameworkCore.Migrations;

namespace Invictus.Data.Migrations
{
    public partial class update64 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CentrocustoId",
                table: "Boletos",
                newName: "CentroCustoId");

            migrationBuilder.AddColumn<string>(
                name: "TipoPessoa",
                table: "Boletos",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoPessoa",
                table: "Boletos");

            migrationBuilder.RenameColumn(
                name: "CentroCustoId",
                table: "Boletos",
                newName: "CentrocustoId");
        }
    }
}
