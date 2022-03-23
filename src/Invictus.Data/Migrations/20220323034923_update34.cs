using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Invictus.Data.Migrations
{
    public partial class update34 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Editada",
                table: "Calendarios",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "LogCalendarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CalendarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ColaboradorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Metodo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OldCommand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewCommand = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogCalendarios", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogCalendarios");

            migrationBuilder.DropColumn(
                name: "Editada",
                table: "Calendarios");
        }
    }
}
