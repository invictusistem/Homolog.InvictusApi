using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Invictus.Data.Migrations
{
    public partial class update9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlunosDocumentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MatriculaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocEnviado = table.Column<bool>(type: "bit", nullable: false),
                    Analisado = table.Column<bool>(type: "bit", nullable: false),
                    Tamanho = table.Column<int>(type: "int", nullable: false),
                    Validado = table.Column<bool>(type: "bit", nullable: false),
                    TipoArquivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentArquivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataFile = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PrazoValidade = table.Column<int>(type: "int", nullable: false),
                    TurmaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunosDocumentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlunosPlanoPagamento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(11,2)", precision: 11, scale: 2, nullable: false),
                    TaxaMatricula = table.Column<decimal>(type: "decimal(11,2)", precision: 11, scale: 2, nullable: false),
                    Parcelas = table.Column<int>(type: "int", nullable: false),
                    MaterialGratuito = table.Column<bool>(type: "bit", nullable: false),
                    ValorMaterial = table.Column<decimal>(type: "decimal(11,2)", precision: 11, scale: 2, nullable: false),
                    BonusPontualidade = table.Column<decimal>(type: "decimal(11,2)", precision: 11, scale: 2, nullable: false),
                    MatriculaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunosPlanoPagamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TurmasNotas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AvaliacaoUm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SegundaChamadaAvaliacaoUm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvaliacaoDois = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SegundaChamadaAvaliacaoDois = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvaliacaoTres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SegundaChamadaAvaliacaoTres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MateriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MateriaDescricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Resultado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatriculaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TurmaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurmasNotas", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlunosDocumentos");

            migrationBuilder.DropTable(
                name: "AlunosPlanoPagamento");

            migrationBuilder.DropTable(
                name: "TurmasNotas");
        }
    }
}
