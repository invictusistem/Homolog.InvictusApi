using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Invictus.Data.Migrations
{
    public partial class update74 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Telefonecontato",
                table: "Pessoa",
                newName: "TelefoneContato");

            migrationBuilder.RenameColumn(
                name: "CNPJ_CPF",
                table: "Pessoa",
                newName: "RG");

            migrationBuilder.AddColumn<string>(
                name: "Mae",
                table: "Pessoa",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Nascimento",
                table: "Pessoa",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Naturalidade",
                table: "Pessoa",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NaturalidadeUF",
                table: "Pessoa",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomeSocial",
                table: "Pessoa",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pai",
                table: "Pessoa",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Boletos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Boletos_Ativo",
                table: "Boletos",
                column: "Ativo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Boletos_Ativo",
                table: "Boletos");

            migrationBuilder.DropColumn(
                name: "Mae",
                table: "Pessoa");

            migrationBuilder.DropColumn(
                name: "Nascimento",
                table: "Pessoa");

            migrationBuilder.DropColumn(
                name: "Naturalidade",
                table: "Pessoa");

            migrationBuilder.DropColumn(
                name: "NaturalidadeUF",
                table: "Pessoa");

            migrationBuilder.DropColumn(
                name: "NomeSocial",
                table: "Pessoa");

            migrationBuilder.DropColumn(
                name: "Pai",
                table: "Pessoa");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Boletos");

            migrationBuilder.RenameColumn(
                name: "TelefoneContato",
                table: "Pessoa",
                newName: "Telefonecontato");

            migrationBuilder.RenameColumn(
                name: "RG",
                table: "Pessoa",
                newName: "CNPJ_CPF");
        }
    }
}
