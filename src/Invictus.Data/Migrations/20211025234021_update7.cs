using Microsoft.EntityFrameworkCore.Migrations;

namespace Invictus.Data.Migrations
{
    public partial class update7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EntreUnidades",
                table: "VendaProduto",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "UnidadeCompradoraId",
                table: "VendaProduto",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntreUnidades",
                table: "VendaProduto");

            migrationBuilder.DropColumn(
                name: "UnidadeCompradoraId",
                table: "VendaProduto");
        }
    }
}
