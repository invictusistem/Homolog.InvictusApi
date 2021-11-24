using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Invictus.Data.Migrations
{
    public partial class update13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Calendarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiaAula = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiaDaSemana = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MateriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HoraInicial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HoraFinal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfessorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TurmaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnidadeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AulaIniciada = table.Column<bool>(type: "bit", nullable: false),
                    TemReposicao = table.Column<bool>(type: "bit", nullable: false),
                    DateAulaIniciada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AulaConcluida = table.Column<bool>(type: "bit", nullable: false),
                    DateAulaConcluida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Observacoes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SalaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendarios", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calendarios_DiaAula",
                table: "Calendarios",
                column: "DiaAula");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calendarios");
        }
    }
}
