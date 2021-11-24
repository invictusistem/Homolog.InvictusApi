using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Invictus.Data.Migrations
{
    public partial class update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentoId",
                table: "PacotesDocumentacao");

            migrationBuilder.AddColumn<bool>(
                name: "ObrigatorioParaMatricula",
                table: "PacotesDocumentacao",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ValidadeDias",
                table: "PacotesDocumentacao",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ObrigatorioParaMatricula",
                table: "PacotesDocumentacao");

            migrationBuilder.DropColumn(
                name: "ValidadeDias",
                table: "PacotesDocumentacao");

            migrationBuilder.AddColumn<Guid>(
                name: "DocumentoId",
                table: "PacotesDocumentacao",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
