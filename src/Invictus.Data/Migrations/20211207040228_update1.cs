using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Invictus.Data.Migrations
{
    public partial class update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProfessorId",
                table: "TurmasMaterias",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfessorId",
                table: "TurmasMaterias");
        }
    }
}
