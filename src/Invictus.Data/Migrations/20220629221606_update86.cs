using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Invictus.Data.Migrations
{
    public partial class update86 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfessoresMaterias_Professores_ProfessorId",
                table: "ProfessoresMaterias");

            migrationBuilder.AddColumn<Guid>(
                name: "ProfessorId1",
                table: "ProfessoresMaterias",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfessoresMaterias_ProfessorId1",
                table: "ProfessoresMaterias",
                column: "ProfessorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessoresMaterias_Pessoas_ProfessorId",
                table: "ProfessoresMaterias",
                column: "ProfessorId",
                principalTable: "Pessoas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessoresMaterias_Professores_ProfessorId1",
                table: "ProfessoresMaterias",
                column: "ProfessorId1",
                principalTable: "Professores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfessoresMaterias_Pessoas_ProfessorId",
                table: "ProfessoresMaterias");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessoresMaterias_Professores_ProfessorId1",
                table: "ProfessoresMaterias");

            migrationBuilder.DropIndex(
                name: "IX_ProfessoresMaterias_ProfessorId1",
                table: "ProfessoresMaterias");

            migrationBuilder.DropColumn(
                name: "ProfessorId1",
                table: "ProfessoresMaterias");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessoresMaterias_Professores_ProfessorId",
                table: "ProfessoresMaterias",
                column: "ProfessorId",
                principalTable: "Professores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
