using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Invictus.Data.Migrations
{
    public partial class update37 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FormaPAgamento",
                table: "Boletos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Caixa",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnidadeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HorarioTransacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BoletoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caixa", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Caixa");

            migrationBuilder.DropColumn(
                name: "FormaPAgamento",
                table: "Boletos");
        }
    }
}
