using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Invictus.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgendasTrimestre",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Inicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Fim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UnidadeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgendasTrimestre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Alunos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomeSocial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CPF = table.Column<string>(type: "nvarchar(11)", nullable: true),
                    RG = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomePai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomeMae = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Naturalidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NaturalidadeUF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelReferencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomeContatoReferencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelCelular = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelResidencial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelWhatsapp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    UnidadeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CEP = table.Column<string>(type: "nvarchar(8)", nullable: true),
                    Complemento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logradouro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UF = table.Column<string>(type: "nvarchar(2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlunosAnotacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MatriculaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunosAnotacoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlunosDocumentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MatriculaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocEnviado = table.Column<bool>(type: "bit", nullable: false),
                    Analisado = table.Column<bool>(type: "bit", nullable: false),
                    Tamanho = table.Column<int>(type: "int", nullable: false),
                    Validado = table.Column<bool>(type: "bit", nullable: false),
                    TipoArquivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentArquivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataFile = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PrazoValidade = table.Column<int>(type: "int", nullable: false),
                    TurmaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunosDocumentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlunosPlanoPagamento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(11,2)", precision: 11, scale: 2, nullable: false),
                    TaxaMatricula = table.Column<decimal>(type: "decimal(11,2)", precision: 11, scale: 2, nullable: false),
                    Parcelas = table.Column<int>(type: "int", nullable: false),
                    MaterialGratuito = table.Column<bool>(type: "bit", nullable: false),
                    ValorMaterial = table.Column<decimal>(type: "decimal(11,2)", precision: 11, scale: 2, nullable: false),
                    BonusPontualidade = table.Column<decimal>(type: "decimal(11,2)", precision: 11, scale: 2, nullable: false),
                    MatriculaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunosPlanoPagamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Autorizacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Unidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnidadeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autorizacoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Calendarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiaAula = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiaDaSemana = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MateriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HoraInicial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HoraFinal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfessorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TurmaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnidadeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AulaIniciada = table.Column<bool>(type: "bit", nullable: false),
                    TemReposicao = table.Column<bool>(type: "bit", nullable: false),
                    DateAulaIniciada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AulaConcluida = table.Column<bool>(type: "bit", nullable: false),
                    DateAulaConcluida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Observacoes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SalaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Colaboradores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CPF = table.Column<string>(type: "nvarchar(11)", nullable: true),
                    Celular = table.Column<string>(type: "nvarchar(12)", nullable: true),
                    CargoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnidadeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CEP = table.Column<string>(type: "nvarchar(8)", nullable: true),
                    Complemento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logradouro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UF = table.Column<string>(type: "nvarchar(2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colaboradores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contratos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodigoContrato = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypePacoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PodeEditar = table.Column<bool>(type: "bit", nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contratos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentacaoTemplate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidadeDias = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentacaoTemplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MateriasTemplate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Modalidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CargaHoraria = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    TypePacoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MateriasTemplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Matriculas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlunoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumeroMatricula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TurmaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiaMatricula = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matriculas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pacotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalHoras = table.Column<int>(type: "int", nullable: false),
                    UnidadeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TypePacoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacotes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParametrosKey",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParametrosKey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlanoPagamentoTemplate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(11,2)", precision: 11, scale: 2, nullable: false),
                    TaxaMatricula = table.Column<decimal>(type: "decimal(11,2)", precision: 11, scale: 2, nullable: false),
                    MaterialGratuito = table.Column<bool>(type: "bit", nullable: false),
                    ValorMaterial = table.Column<decimal>(type: "decimal(11,2)", precision: 11, scale: 2, nullable: false),
                    BonusPontualidade = table.Column<decimal>(type: "decimal(11,2)", precision: 11, scale: 2, nullable: false),
                    ContratoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypePacoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanoPagamentoTemplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodigoProduto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Preco = table.Column<decimal>(type: "decimal(11,2)", precision: 11, scale: 2, nullable: false),
                    PrecoCusto = table.Column<decimal>(type: "decimal(11,2)", precision: 11, scale: 2, nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    NivelMinimo = table.Column<int>(type: "int", nullable: false),
                    UnidadeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Observacoes = table.Column<string>(type: "nvarchar(400)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Professores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CPF = table.Column<string>(type: "nvarchar(11)", nullable: true),
                    Celular = table.Column<string>(type: "nvarchar(12)", nullable: true),
                    UnidadeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CEP = table.Column<string>(type: "nvarchar(8)", nullable: true),
                    Complemento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logradouro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UF = table.Column<string>(type: "nvarchar(2)", nullable: true),
                    BancoNumero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Agencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Conta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoConta = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfessoresDisponibilidades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Domingo = table.Column<bool>(type: "bit", nullable: false),
                    Segunda = table.Column<bool>(type: "bit", nullable: false),
                    Terca = table.Column<bool>(type: "bit", nullable: false),
                    Quarta = table.Column<bool>(type: "bit", nullable: false),
                    Quinta = table.Column<bool>(type: "bit", nullable: false),
                    Sexta = table.Column<bool>(type: "bit", nullable: false),
                    Sabado = table.Column<bool>(type: "bit", nullable: false),
                    UnidadeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PessoaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessoresDisponibilidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Responsaveis",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CPF = table.Column<string>(type: "nvarchar(11)", nullable: true),
                    RG = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nascimento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Naturalidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NaturalidadeUF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelCelular = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelResidencial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelWhatsapp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TemRespFin = table.Column<bool>(type: "bit", nullable: false),
                    MatriculaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CEP = table.Column<string>(type: "nvarchar(8)", nullable: true),
                    Complemento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logradouro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UF = table.Column<string>(type: "nvarchar(2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responsaveis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Turmas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Identificador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalAlunos = table.Column<int>(type: "int", nullable: false),
                    MinimoAlunos = table.Column<int>(type: "int", nullable: false),
                    StatusAndamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnidadeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PacoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypePacoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrevisaoAtual = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PrevisaoTerminoAtual = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PrevisaoInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turmas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TurmasNotas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AvaliacaoUm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SegundaChamadaAvaliacaoUm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvaliacaoDois = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SegundaChamadaAvaliacaoDois = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvaliacaoTres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SegundaChamadaAvaliacaoTres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MateriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MateriaDescricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Resultado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatriculaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TurmaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurmasNotas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TurmasPrevisoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrevisionStartOne = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrevisionStartTwo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrevisionStartThree = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrevisionEndingOne = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrevisionEndingTwo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrevisionEndingThree = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TurmaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurmasPrevisoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypePacote",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nivel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypePacote", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Unidades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sigla = table.Column<string>(type: "nvarchar(3)", nullable: true),
                    CNPJ = table.Column<string>(type: "nvarchar(14)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CEP = table.Column<string>(type: "nvarchar(8)", nullable: true),
                    Complemento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logradouro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UF = table.Column<string>(type: "nvarchar(2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConteudoContratos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContratoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConteudoContratos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConteudoContratos_Contratos_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contratos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PacotesDocumentacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Titular = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidadeDias = table.Column<int>(type: "int", nullable: false),
                    ObrigatorioParaMatricula = table.Column<bool>(type: "bit", nullable: false),
                    PacoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacotesDocumentacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PacotesDocumentacao_Pacotes_PacoteId",
                        column: x => x.PacoteId,
                        principalTable: "Pacotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PacotesMaterias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MateriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ordem = table.Column<int>(type: "int", nullable: false),
                    CargaHoraria = table.Column<int>(type: "int", nullable: false),
                    Modalidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PacoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacotesMaterias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PacotesMaterias_Pacotes_PacoteId",
                        column: x => x.PacoteId,
                        principalTable: "Pacotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParametrosValue",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParametrosKeyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParametrosValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParametrosValue_ParametrosKey_ParametrosKeyId",
                        column: x => x.ParametrosKeyId,
                        principalTable: "ParametrosKey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfessoresMaterias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PacoteMateriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfessorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessoresMaterias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfessoresMaterias_Professores_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TurmasHorarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiaSemanada = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HorarioInicio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HorarioFim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TurmaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurmasHorarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TurmasHorarios_Turmas_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turmas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TurmasMaterias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Modalidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CargaHoraria = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    TypePacoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TurmaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MateriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurmasMaterias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TurmasMaterias_Turmas_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turmas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnidadesSalas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Comentarios = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Capacidade = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    UnidadeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadesSalas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnidadesSalas_Unidades_UnidadeId",
                        column: x => x.UnidadeId,
                        principalTable: "Unidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Autorizacoes_UsuarioId",
                table: "Autorizacoes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Calendarios_DiaAula",
                table: "Calendarios",
                column: "DiaAula");

            migrationBuilder.CreateIndex(
                name: "IX_ConteudoContratos_ContratoId",
                table: "ConteudoContratos",
                column: "ContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentacaoTemplate_Nome",
                table: "DocumentacaoTemplate",
                column: "Nome");

            migrationBuilder.CreateIndex(
                name: "IX_MateriasTemplate_TypePacoteId",
                table: "MateriasTemplate",
                column: "TypePacoteId");

            migrationBuilder.CreateIndex(
                name: "IX_PacotesDocumentacao_PacoteId",
                table: "PacotesDocumentacao",
                column: "PacoteId");

            migrationBuilder.CreateIndex(
                name: "IX_PacotesMaterias_PacoteId",
                table: "PacotesMaterias",
                column: "PacoteId");

            migrationBuilder.CreateIndex(
                name: "IX_ParametrosValue_ParametrosKeyId",
                table: "ParametrosValue",
                column: "ParametrosKeyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessoresMaterias_ProfessorId",
                table: "ProfessoresMaterias",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_TurmasHorarios_TurmaId",
                table: "TurmasHorarios",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_TurmasMaterias_TurmaId",
                table: "TurmasMaterias",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Unidades_Sigla",
                table: "Unidades",
                column: "Sigla");

            migrationBuilder.CreateIndex(
                name: "IX_UnidadesSalas_Descricao",
                table: "UnidadesSalas",
                column: "Descricao");

            migrationBuilder.CreateIndex(
                name: "IX_UnidadesSalas_UnidadeId",
                table: "UnidadesSalas",
                column: "UnidadeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgendasTrimestre");

            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropTable(
                name: "AlunosAnotacoes");

            migrationBuilder.DropTable(
                name: "AlunosDocumentos");

            migrationBuilder.DropTable(
                name: "AlunosPlanoPagamento");

            migrationBuilder.DropTable(
                name: "Autorizacoes");

            migrationBuilder.DropTable(
                name: "Calendarios");

            migrationBuilder.DropTable(
                name: "Colaboradores");

            migrationBuilder.DropTable(
                name: "ConteudoContratos");

            migrationBuilder.DropTable(
                name: "DocumentacaoTemplate");

            migrationBuilder.DropTable(
                name: "MateriasTemplate");

            migrationBuilder.DropTable(
                name: "Matriculas");

            migrationBuilder.DropTable(
                name: "PacotesDocumentacao");

            migrationBuilder.DropTable(
                name: "PacotesMaterias");

            migrationBuilder.DropTable(
                name: "ParametrosValue");

            migrationBuilder.DropTable(
                name: "PlanoPagamentoTemplate");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "ProfessoresDisponibilidades");

            migrationBuilder.DropTable(
                name: "ProfessoresMaterias");

            migrationBuilder.DropTable(
                name: "Responsaveis");

            migrationBuilder.DropTable(
                name: "TurmasHorarios");

            migrationBuilder.DropTable(
                name: "TurmasMaterias");

            migrationBuilder.DropTable(
                name: "TurmasNotas");

            migrationBuilder.DropTable(
                name: "TurmasPrevisoes");

            migrationBuilder.DropTable(
                name: "TypePacote");

            migrationBuilder.DropTable(
                name: "UnidadesSalas");

            migrationBuilder.DropTable(
                name: "Contratos");

            migrationBuilder.DropTable(
                name: "Pacotes");

            migrationBuilder.DropTable(
                name: "ParametrosKey");

            migrationBuilder.DropTable(
                name: "Professores");

            migrationBuilder.DropTable(
                name: "Turmas");

            migrationBuilder.DropTable(
                name: "Unidades");
        }
    }
}
