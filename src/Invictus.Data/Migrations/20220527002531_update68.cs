using Microsoft.EntityFrameworkCore.Migrations;

namespace Invictus.Data.Migrations
{
    public partial class update68 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Funcionarios_Enderecos_Id",
                table: "Funcionarios");

            migrationBuilder.CreateIndex(
                name: "IX_Enderecos_FuncionarioId",
                table: "Enderecos",
                column: "FuncionarioId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Enderecos_Funcionarios_FuncionarioId",
                table: "Enderecos",
                column: "FuncionarioId",
                principalTable: "Funcionarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enderecos_Funcionarios_FuncionarioId",
                table: "Enderecos");

            migrationBuilder.DropIndex(
                name: "IX_Enderecos_FuncionarioId",
                table: "Enderecos");

            migrationBuilder.AddForeignKey(
                name: "FK_Funcionarios_Enderecos_Id",
                table: "Funcionarios",
                column: "Id",
                principalTable: "Enderecos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
