using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Invictus.Data.Migrations
{
    public partial class update58 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BancoId",
                table: "Boletos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CentrocustoId",
                table: "Boletos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MeioPagamentoId",
                table: "Boletos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SubContaId",
                table: "Boletos",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BancoId",
                table: "Boletos");

            migrationBuilder.DropColumn(
                name: "CentrocustoId",
                table: "Boletos");

            migrationBuilder.DropColumn(
                name: "MeioPagamentoId",
                table: "Boletos");

            migrationBuilder.DropColumn(
                name: "SubContaId",
                table: "Boletos");
        }
    }
}
