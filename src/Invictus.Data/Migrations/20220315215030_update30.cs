using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Invictus.Data.Migrations
{
    public partial class update30 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataCadastro",
                table: "EstagiosMatriculas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "NumeroMatricula",
                table: "EstagiosMatriculas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "EstagiosMatriculas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsColaborador",
                table: "Colaboradores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsProfessor",
                table: "Colaboradores",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataCadastro",
                table: "EstagiosMatriculas");

            migrationBuilder.DropColumn(
                name: "NumeroMatricula",
                table: "EstagiosMatriculas");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "EstagiosMatriculas");

            migrationBuilder.DropColumn(
                name: "IsColaborador",
                table: "Colaboradores");

            migrationBuilder.DropColumn(
                name: "IsProfessor",
                table: "Colaboradores");
        }
    }
}
