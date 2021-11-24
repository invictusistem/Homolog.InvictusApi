using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Invictus.Data.Migrations
{
    public partial class update11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MateriaId",
                table: "ProfessoresMaterias",
                newName: "PacoteMateriaId");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "PacotesMaterias",
                newName: "Nome");

            migrationBuilder.AddColumn<Guid>(
                name: "MateriaId",
                table: "TurmasMaterias",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MateriaId",
                table: "TurmasMaterias");

            migrationBuilder.RenameColumn(
                name: "PacoteMateriaId",
                table: "ProfessoresMaterias",
                newName: "MateriaId");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "PacotesMaterias",
                newName: "Descricao");
        }
    }
}
