using Microsoft.EntityFrameworkCore.Migrations;

namespace Invictus.Data.Migrations
{
    public partial class update75 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enderecos_Pessoa_FuncionarioId",
                table: "Enderecos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pessoa",
                table: "Pessoa");

            migrationBuilder.RenameTable(
                name: "Pessoa",
                newName: "Pessoas");

            migrationBuilder.RenameColumn(
                name: "FuncionarioId",
                table: "Enderecos",
                newName: "PessoaId");

            migrationBuilder.RenameIndex(
                name: "IX_Enderecos_FuncionarioId",
                table: "Enderecos",
                newName: "IX_Enderecos_PessoaId");

            migrationBuilder.RenameIndex(
                name: "IX_Pessoa_TipoPessoa",
                table: "Pessoas",
                newName: "IX_Pessoas_TipoPessoa");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pessoas",
                table: "Pessoas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Enderecos_Pessoas_PessoaId",
                table: "Enderecos",
                column: "PessoaId",
                principalTable: "Pessoas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enderecos_Pessoas_PessoaId",
                table: "Enderecos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pessoas",
                table: "Pessoas");

            migrationBuilder.RenameTable(
                name: "Pessoas",
                newName: "Pessoa");

            migrationBuilder.RenameColumn(
                name: "PessoaId",
                table: "Enderecos",
                newName: "FuncionarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Enderecos_PessoaId",
                table: "Enderecos",
                newName: "IX_Enderecos_FuncionarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Pessoas_TipoPessoa",
                table: "Pessoa",
                newName: "IX_Pessoa_TipoPessoa");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pessoa",
                table: "Pessoa",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Enderecos_Pessoa_FuncionarioId",
                table: "Enderecos",
                column: "FuncionarioId",
                principalTable: "Pessoa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
