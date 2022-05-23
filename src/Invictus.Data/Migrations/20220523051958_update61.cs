using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Invictus.Data.Migrations
{
    public partial class update61 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boletos_InformacoesDebitos_InformacaoDebitoId",
                table: "Boletos");

            migrationBuilder.DropTable(
                name: "InformacoesDebitos");

            migrationBuilder.DropIndex(
                name: "IX_Boletos_InformacaoDebitoId",
                table: "Boletos");

            migrationBuilder.DropColumn(
                name: "InformacaoDebitoId",
                table: "Boletos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "InformacaoDebitoId",
                table: "Boletos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InformacoesDebitos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FornecedorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Historico = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatriculaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumeroParcelas = table.Column<int>(type: "int", nullable: false),
                    Origem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusPagamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubConta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnidadeCusto = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(11,2)", precision: 11, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InformacoesDebitos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Boletos_InformacaoDebitoId",
                table: "Boletos",
                column: "InformacaoDebitoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boletos_InformacoesDebitos_InformacaoDebitoId",
                table: "Boletos",
                column: "InformacaoDebitoId",
                principalTable: "InformacoesDebitos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
