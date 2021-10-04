using Microsoft.EntityFrameworkCore.Migrations;

namespace Invictus.Data.Migrations
{
    public partial class update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DocumentacaoExigencia_ModuloId",
                table: "DocumentacaoExigencia",
                column: "ModuloId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentacaoExigencia_Modulos_ModuloId",
                table: "DocumentacaoExigencia",
                column: "ModuloId",
                principalTable: "Modulos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentacaoExigencia_Modulos_ModuloId",
                table: "DocumentacaoExigencia");

            migrationBuilder.DropIndex(
                name: "IX_DocumentacaoExigencia_ModuloId",
                table: "DocumentacaoExigencia");
        }
    }
}
