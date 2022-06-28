using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Invictus.Data.Migrations
{
    public partial class update83 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Identificador",
                table: "Boletos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tipoVenda",
                table: "Boletos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Leads",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CursoPretendido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnidadeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataInclusaoSistema = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResponsavelLead = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Efetivada = table.Column<bool>(type: "bit", nullable: false),
                    MatriculaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leads", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Leads");

            migrationBuilder.DropColumn(
                name: "Identificador",
                table: "Boletos");

            migrationBuilder.DropColumn(
                name: "tipoVenda",
                table: "Boletos");
        }
    }
}
