using Microsoft.EntityFrameworkCore.Migrations;

namespace Invictus.Data.Migrations
{
    public partial class update43 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AlunoId",
                table: "EstagiosMatriculas",
                newName: "TypeEstagioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TypeEstagioId",
                table: "EstagiosMatriculas",
                newName: "AlunoId");
        }
    }
}
