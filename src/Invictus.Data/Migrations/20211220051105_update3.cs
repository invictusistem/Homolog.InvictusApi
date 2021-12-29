using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Invictus.Data.Migrations
{
    public partial class update3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InformacoesDebitos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumeroParcelas = table.Column<int>(type: "int", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(11,2)", precision: 11, scale: 2, nullable: false),
                    Historico = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusPagamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Origem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnidadeCusto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubConta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatriculaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InformacoesDebitos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Boletos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Vencimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataPagamento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(11,2)", precision: 11, scale: 2, nullable: false),
                    ValorPago = table.Column<decimal>(type: "decimal(11,2)", precision: 11, scale: 2, nullable: false),
                    Juros = table.Column<int>(type: "int", nullable: false),
                    JurosFixo = table.Column<int>(type: "int", nullable: false),
                    Multa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MultaFixo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Desconto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiasDesconto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusBoleto = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReparcelamentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CentroCustoUnidadeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InformacaoDebitoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id_unico = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id_unico_original = table.Column<string>(type: "nvarchar(8)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Msg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nossonumero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkBoleto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkGrupo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinhaDigitavel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pedido_numero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Banco_numero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Token_facilitador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Credencial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    infoBoletos_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boletos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Boletos_InformacoesDebitos_InformacaoDebitoId",
                        column: x => x.InformacaoDebitoId,
                        principalTable: "InformacoesDebitos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Boletos_CentroCustoUnidadeId",
                table: "Boletos",
                column: "CentroCustoUnidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Boletos_InformacaoDebitoId",
                table: "Boletos",
                column: "InformacaoDebitoId");

            migrationBuilder.CreateIndex(
                name: "IX_Boletos_StatusBoleto",
                table: "Boletos",
                column: "StatusBoleto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Boletos");

            migrationBuilder.DropTable(
                name: "InformacoesDebitos");
        }
    }
}
