using Microsoft.EntityFrameworkCore.Migrations;

namespace Invictus.Data.Migrations
{
    public partial class update6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Professores_MateriasTurmas_MateriaId",
                table: "Professores");

            migrationBuilder.DropIndex(
                name: "IX_Professores_MateriaId",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "MateriaId",
                table: "Professores");

            migrationBuilder.RenameColumn(
                name: "ProfId",
                table: "Professores",
                newName: "UnidadeId");

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Professores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Bairro",
                table: "Professores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CEP",
                table: "Professores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CPF",
                table: "Professores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cargo",
                table: "Professores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Celular",
                table: "Professores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cidade",
                table: "Professores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Complemento",
                table: "Professores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Logradouro",
                table: "Professores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Perfil",
                table: "Professores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PerfilAtivo",
                table: "Professores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UF",
                table: "Professores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Professor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfId = table.Column<int>(type: "int", nullable: false),
                    MateriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Professor_MateriasTurmas_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "MateriasTurmas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MateriasHabilitadas_ProfessorId",
                table: "MateriasHabilitadas",
                column: "ProfessorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Professor_MateriaId",
                table: "Professor",
                column: "MateriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_MateriasHabilitadas_Professores_ProfessorId",
                table: "MateriasHabilitadas",
                column: "ProfessorId",
                principalTable: "Professores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MateriasHabilitadas_Professores_ProfessorId",
                table: "MateriasHabilitadas");

            migrationBuilder.DropTable(
                name: "Professor");

            migrationBuilder.DropIndex(
                name: "IX_MateriasHabilitadas_ProfessorId",
                table: "MateriasHabilitadas");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "Bairro",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "CEP",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "CPF",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "Cargo",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "Celular",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "Cidade",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "Complemento",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "Logradouro",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "Perfil",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "PerfilAtivo",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "UF",
                table: "Professores");

            migrationBuilder.RenameColumn(
                name: "UnidadeId",
                table: "Professores",
                newName: "ProfId");

            migrationBuilder.AddColumn<int>(
                name: "MateriaId",
                table: "Professores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Professores_MateriaId",
                table: "Professores",
                column: "MateriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Professores_MateriasTurmas_MateriaId",
                table: "Professores",
                column: "MateriaId",
                principalTable: "MateriasTurmas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
