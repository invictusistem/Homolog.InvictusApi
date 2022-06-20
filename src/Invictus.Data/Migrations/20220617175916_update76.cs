using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Invictus.Data.Migrations
{
    public partial class update76 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Agencia",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "BancoNumero",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "Conta",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "TipoConta",
                table: "Professores");

            migrationBuilder.AddColumn<Guid>(
                name: "FormaRecebimentoId",
                table: "Boletos",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormaRecebimentoId",
                table: "Boletos");

            migrationBuilder.AddColumn<string>(
                name: "Agencia",
                table: "Professores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BancoNumero",
                table: "Professores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Conta",
                table: "Professores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoConta",
                table: "Professores",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
