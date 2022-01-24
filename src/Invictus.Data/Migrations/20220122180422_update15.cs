using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Invictus.Data.Migrations
{
    public partial class update15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogMatriculas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MatriculaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GuidColaborador = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogMatriculas", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogMatriculas");
        }
    }
}
