using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Invictus.Data.Migrations
{
    public partial class update38 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FormaPAgamento",
                table: "Boletos",
                newName: "FormaPagamento");

            migrationBuilder.AddColumn<string>(
                name: "DigitosCartao",
                table: "Boletos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ResponsavelCadastroId",
                table: "Boletos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Boletos",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DigitosCartao",
                table: "Boletos");

            migrationBuilder.DropColumn(
                name: "ResponsavelCadastroId",
                table: "Boletos");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Boletos");

            migrationBuilder.RenameColumn(
                name: "FormaPagamento",
                table: "Boletos",
                newName: "FormaPAgamento");
        }
    }
}
