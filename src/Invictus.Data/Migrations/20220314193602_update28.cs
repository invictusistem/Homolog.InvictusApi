using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Invictus.Data.Migrations
{
    public partial class update28 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CNPJ",
                table: "Colaboradores");

            migrationBuilder.DropColumn(
                name: "DataEntrada",
                table: "Colaboradores");

            migrationBuilder.DropColumn(
                name: "DataSaida",
                table: "Colaboradores");

            migrationBuilder.DropColumn(
                name: "NomeContato",
                table: "Colaboradores");

            migrationBuilder.DropColumn(
                name: "TelefoneContato",
                table: "Colaboradores");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CNPJ",
                table: "Colaboradores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataEntrada",
                table: "Colaboradores",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataSaida",
                table: "Colaboradores",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "NomeContato",
                table: "Colaboradores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TelefoneContato",
                table: "Colaboradores",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
