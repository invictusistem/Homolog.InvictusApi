using Microsoft.EntityFrameworkCore.Migrations;

namespace Invictus.Data.Migrations
{
    public partial class update70 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TipoPessoa",
                table: "Funcionarios",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RazaoSocial",
                table: "Funcionarios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_TipoPessoa",
                table: "Funcionarios",
                column: "TipoPessoa");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Funcionarios_TipoPessoa",
                table: "Funcionarios");

            migrationBuilder.DropColumn(
                name: "RazaoSocial",
                table: "Funcionarios");

            migrationBuilder.AlterColumn<string>(
                name: "TipoPessoa",
                table: "Funcionarios",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
