using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Invictus.Data.Migrations
{
    public partial class Update53 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bancos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EhCaixaEscola = table.Column<bool>(type: "bit", nullable: false),
                    Agencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgenciaDigito = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Conta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContaDigito = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UtilizadoParaImpressao = table.Column<bool>(type: "bit", nullable: false),
                    UnidadeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bancos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CentroCustos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    AlertaMediaGastos = table.Column<bool>(type: "bit", nullable: false),
                    UnidadeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentroCustos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormasRecebimento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ativo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EhCartao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiasParaCompensacao = table.Column<int>(type: "int", nullable: false),
                    Taxa = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PermiteParcelamento = table.Column<bool>(type: "bit", nullable: false),
                    BancoPermitidoParaCreditoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubcontaTaxaVinculadaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FornecedorTaxaVinculadaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CentroDeCustoTaxaVinculadaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompensarAutomaticamenteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnidadeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormasRecebimento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MeioPagamentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeioPagamentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlanoContas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanoContas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubContas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    PlanoContaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubContas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubContas_PlanoContas_PlanoContaId",
                        column: x => x.PlanoContaId,
                        principalTable: "PlanoContas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubContas_PlanoContaId",
                table: "SubContas",
                column: "PlanoContaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bancos");

            migrationBuilder.DropTable(
                name: "CentroCustos");

            migrationBuilder.DropTable(
                name: "FormasRecebimento");

            migrationBuilder.DropTable(
                name: "MeioPagamentos");

            migrationBuilder.DropTable(
                name: "SubContas");

            migrationBuilder.DropTable(
                name: "PlanoContas");
        }
    }
}
