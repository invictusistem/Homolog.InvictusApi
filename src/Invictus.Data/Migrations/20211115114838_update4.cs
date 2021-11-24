using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Invictus.Data.Migrations
{
    public partial class update4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Turmas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Identificador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalAlunos = table.Column<int>(type: "int", nullable: false),
                    MinimoAlunos = table.Column<int>(type: "int", nullable: false),
                    StatusAndamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnidadeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PacoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrevisaoAtual = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PrevisaoTerminoAtual = table.Column<string>(type: "nvarchar(8)", nullable: true),
                    PrevisaoInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turmas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TurmasPrevisoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrevisionStartOne = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrevisionStartTwo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrevisionStartThree = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrevisionEndingOne = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrevisionEndingTwo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrevisionEndingThree = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TurmaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurmasPrevisoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TurmasHorarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiaSemanada = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HorarioInicio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HorarioFim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TurmaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurmasHorarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TurmasHorarios_Turmas_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turmas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TurmasMaterias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Modalidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CargaHoraria = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    TypePacoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TurmaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurmasMaterias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TurmasMaterias_Turmas_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turmas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TurmasHorarios_TurmaId",
                table: "TurmasHorarios",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_TurmasMaterias_TurmaId",
                table: "TurmasMaterias",
                column: "TurmaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TurmasHorarios");

            migrationBuilder.DropTable(
                name: "TurmasMaterias");

            migrationBuilder.DropTable(
                name: "TurmasPrevisoes");

            migrationBuilder.DropTable(
                name: "Turmas");
        }
    }
}
