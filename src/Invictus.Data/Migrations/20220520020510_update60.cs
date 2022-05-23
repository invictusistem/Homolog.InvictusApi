using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Invictus.Data.Migrations
{
    public partial class update60 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boletos_InformacoesDebitos_InformacaoDebitoId",
                table: "Boletos");

            migrationBuilder.AlterColumn<Guid>(
                name: "InformacaoDebitoId",
                table: "Boletos",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Boletos_InformacoesDebitos_InformacaoDebitoId",
                table: "Boletos",
                column: "InformacaoDebitoId",
                principalTable: "InformacoesDebitos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boletos_InformacoesDebitos_InformacaoDebitoId",
                table: "Boletos");

            migrationBuilder.AlterColumn<Guid>(
                name: "InformacaoDebitoId",
                table: "Boletos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Boletos_InformacoesDebitos_InformacaoDebitoId",
                table: "Boletos",
                column: "InformacaoDebitoId",
                principalTable: "InformacoesDebitos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
