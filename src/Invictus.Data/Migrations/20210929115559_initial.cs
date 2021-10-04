using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Invictus.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aluno",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeroMatricula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomeSocial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RG = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Naturalidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NaturalidadeUF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelReferencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomeContatoReferencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CienciaCurso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelCelular = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelResidencial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelWhatsapp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CEP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logradouro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Complemento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observacoes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TemRespMenor = table.Column<bool>(type: "bit", nullable: false),
                    TemRespFin = table.Column<bool>(type: "bit", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    UnidadeCadastrada = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aluno", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Boleto",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_unico = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    id_unico_original = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    msg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nossonumero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    linkBoleto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    linkGrupo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    linhaDigitavel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pedido_numero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    banco_numero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    token_facilitador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    credencial = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boleto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cargo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CentroCusto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentroCusto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Colaborador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Celular = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cargo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CargoId = table.Column<int>(type: "int", nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnidadeId = table.Column<int>(type: "int", nullable: false),
                    Perfil = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PerfilAtivo = table.Column<bool>(type: "bit", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    CEP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logradouro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Complemento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colaborador", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contratos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoContrato = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PacoteId = table.Column<int>(type: "int", nullable: false),
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
                name: "DocumentacaoExigencia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Titular = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModuloId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentacaoExigencia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Documento_Aluno",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlunoId = table.Column<int>(type: "int", nullable: false),
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
                    TurmaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documento_Aluno", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estagio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataInicio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trimestre = table.Column<int>(type: "int", nullable: false),
                    Vagas = table.Column<int>(type: "int", nullable: false),
                    CEP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logradouro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Complemento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estagio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FornecedorEntrada",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(11,2)", precision: 11, scale: 2, nullable: false),
                    ValorComDesconto = table.Column<decimal>(type: "decimal(11,2)", precision: 11, scale: 2, nullable: false),
                    ValorPago = table.Column<decimal>(type: "decimal(11,2)", precision: 11, scale: 2, nullable: false),
                    DiaPagamento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MeioPagamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusPagamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescricaoTransacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FornecedorId = table.Column<long>(type: "bigint", nullable: false),
                    UnidadeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FornecedorEntrada", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fornecedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RazaoSocial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IE_RG = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CNPJ_CPF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelContato = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhatsApp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomeContato = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CEP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logradouro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Complemento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    UnidadeId = table.Column<int>(type: "int", nullable: false),
                    Observacoes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FornecedorSaida",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(11,2)", precision: 11, scale: 2, nullable: false),
                    ValorPago = table.Column<decimal>(type: "decimal(11,2)", precision: 11, scale: 2, nullable: false),
                    DiaPagamento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MeioPagamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusPagamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescricaoTransacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FornecedorId = table.Column<long>(type: "bigint", nullable: false),
                    UnidadeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FornecedorSaida", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistoricoEscolar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Aluno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlunoId = table.Column<int>(type: "int", nullable: false),
                    TurmaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoEscolar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InfoFinanceira",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlunoId = table.Column<int>(type: "int", nullable: false),
                    TurmaId = table.Column<int>(type: "int", nullable: false),
                    ValorCurso = table.Column<decimal>(type: "decimal(11,2)", precision: 11, scale: 2, nullable: false),
                    DataRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUnidadeCadastroInicial = table.Column<int>(type: "int", nullable: false),
                    Parcelas = table.Column<int>(type: "int", nullable: false),
                    MatConfirmada = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoFinanceira", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Leads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CursoPretendido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataInclusaoSistema = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResponsavelLead = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColaboradorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LivroPresencas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CalendarioId = table.Column<int>(type: "int", nullable: false),
                    IsPresent = table.Column<bool>(type: "bit", nullable: false),
                    IsPresentToString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlunoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LivroPresencas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Matriculados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlunoId = table.Column<int>(type: "int", nullable: false),
                    NumeroMatricula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TurmaId = table.Column<int>(type: "int", nullable: false),
                    DiaMatricula = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matriculados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mensagens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Conteudo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataExpiracao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Criador = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mensagens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modulos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DuracaoMeses = table.Column<int>(type: "int", nullable: false),
                    TotalHoras = table.Column<int>(type: "int", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(11,2)", precision: 11, scale: 2, nullable: false),
                    UnidadeId = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TypePacoteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modulos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotasDisciplinas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Trimestre = table.Column<int>(type: "int", nullable: false),
                    AvaliacaoUm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SegundaChamadaAvaliacaoUm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvaliacaoDois = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SegundaChamadaAvaliacaoDois = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvaliacaoTres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SegundaChamadaAvaliacaoTres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MateriaId = table.Column<int>(type: "int", nullable: false),
                    MateriaDescricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Resultado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlunoId = table.Column<int>(type: "int", nullable: false),
                    TurmaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotasDisciplinas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParametrosType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParametrosType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlanoPagamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PacoteId = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(11,2)", precision: 11, scale: 2, nullable: false),
                    TaxaMatricula = table.Column<decimal>(type: "decimal(11,2)", precision: 11, scale: 2, nullable: false),
                    Parcelamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialGratuito = table.Column<bool>(type: "bit", nullable: false),
                    BonusMensalidade = table.Column<decimal>(type: "decimal(11,2)", precision: 11, scale: 2, nullable: false),
                    UnidadeId = table.Column<int>(type: "int", nullable: false),
                    ContratoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanoPagamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoProduto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Preco = table.Column<decimal>(type: "decimal(11,2)", precision: 11, scale: 2, nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    NivelMinimo = table.Column<int>(type: "int", nullable: false),
                    UnidadeId = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Observacoes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfessorNew",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfId = table.Column<int>(type: "int", nullable: false),
                    TurmaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessorNew", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProvasAgenda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AvaliacaoUm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SegundaChamadaAvaliacaoUm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AvaliacaoDois = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SegundaChamadaAvaliacaoDois = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AvaliacaoTres = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SegundaChamadaAvaliacaoTres = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MateriaId = table.Column<int>(type: "int", nullable: false),
                    Materia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TurmaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProvasAgenda", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SenhaBolsa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataExpiracao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmissorId = table.Column<int>(type: "int", nullable: false),
                    PacoteId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PercentualBolsa = table.Column<decimal>(type: "decimal(11,2)", precision: 11, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SenhaBolsa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubConta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubConta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Testando",
                columns: table => new
                {
                    Descricao = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Cardtype = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Testando", x => x.Descricao);
                });

            migrationBuilder.CreateTable(
                name: "TransacaoBoleto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodigoTransacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodigoReferencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Valor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Taxas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalLiquido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataVencimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MeioPagamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompradorNome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompradorEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompradorTelefone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransacaoBoleto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransacaoCartao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodigoTransacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodigoVenda = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Valor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Taxas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalLiquido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeioPagamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NSU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeroSerie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ARQC = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransacaoCartao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Turma",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Identificador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModuloId = table.Column<int>(type: "int", nullable: false),
                    TotalAlunos = table.Column<int>(type: "int", nullable: false),
                    SemetreAtual = table.Column<int>(type: "int", nullable: false),
                    MinimoAlunos = table.Column<int>(type: "int", nullable: false),
                    StatusDaTurma = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    UnidadeId = table.Column<int>(type: "int", nullable: false),
                    Iniciada = table.Column<bool>(type: "bit", nullable: false),
                    PrevisaoAtual = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrevisaoTerminoAtual = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Previsao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SalaId = table.Column<int>(type: "int", nullable: false),
                    PlanoPagamentoId = table.Column<int>(type: "int", nullable: false),
                    PacoteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turma", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TurmaPedagogico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TurmaId = table.Column<int>(type: "int", nullable: false),
                    ModuloId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurmaPedagogico", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypePacote",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                name: "Unidade",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sigla = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CNPJ = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nvarchar150 = table.Column<string>(name: "nvarchar(150)", type: "nvarchar(max)", nullable: true),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CEP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Complemento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logradouro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unidade", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VendaCurso",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataVenda = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValorTotalVenda = table.Column<decimal>(type: "decimal(11,2)", precision: 11, scale: 2, nullable: false),
                    RespVendaId = table.Column<int>(type: "int", nullable: false),
                    UnidadeId = table.Column<int>(type: "int", nullable: false),
                    CPF_Comprador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatriculaComprador = table.Column<int>(type: "int", nullable: false),
                    MeioPagamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentificadorPagamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Parcelas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendaCurso", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VendaProduto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataVenda = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValorTotalVenda = table.Column<decimal>(type: "decimal(11,2)", precision: 11, scale: 2, nullable: false),
                    RespVendaId = table.Column<int>(type: "int", nullable: false),
                    UnidadeId = table.Column<int>(type: "int", nullable: false),
                    CPF_Comprador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatriculaComprador = table.Column<int>(type: "int", nullable: false),
                    MeioPagamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentificadorPagamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Parcelas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendaProduto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Responsaveis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RG = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Naturalidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NaturalidadeUF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelCelular = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelResidencial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelWhatsapp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CEP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logradouro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Complemento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observacoes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlunoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responsaveis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Responsaveis_Aluno_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Aluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContratoConteudo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContratoId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContratoConteudo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContratoConteudo_Contratos_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contratos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EstagioMatricula",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlunoId = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstagioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstagioMatricula", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EstagioMatricula_Estagio_EstagioId",
                        column: x => x.EstagioId,
                        principalTable: "Estagio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoletinsEscolares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Disciplina = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HistoricoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoletinsEscolares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoletinsEscolares_HistoricoEscolar_HistoricoId",
                        column: x => x.HistoricoId,
                        principalTable: "HistoricoEscolar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Debito",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Competencia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUnidadeResponsavel = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    ValorTitulo = table.Column<decimal>(type: "decimal(11,2)", precision: 11, scale: 2, nullable: false),
                    ValorPago = table.Column<decimal>(type: "decimal(11,2)", precision: 11, scale: 2, nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataPagamento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MeioPagamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParcelaNumero = table.Column<int>(type: "int", nullable: false),
                    TransacaoId = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoletoId = table.Column<int>(type: "int", nullable: false),
                    InfoFinancId = table.Column<int>(type: "int", nullable: false),
                    IdDebitoOriginal = table.Column<int>(type: "int", nullable: false),
                    HistoricoDivida = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Debito", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Debito_InfoFinanceira_InfoFinancId",
                        column: x => x.InfoFinancId,
                        principalTable: "InfoFinanceira",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Destinatario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Perfil = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MensagemId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destinatario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Destinatario_Mensagens_MensagemId",
                        column: x => x.MensagemId,
                        principalTable: "Mensagens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Materias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QntAulas = table.Column<int>(type: "int", nullable: false),
                    PrimeiroDiaAula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimeiroDaLista = table.Column<bool>(type: "bit", nullable: false),
                    Semestre = table.Column<int>(type: "int", nullable: false),
                    CargaHoraria = table.Column<int>(type: "int", nullable: false),
                    QntProvas = table.Column<int>(type: "int", nullable: false),
                    TemRecuperacao = table.Column<bool>(type: "bit", nullable: false),
                    Modalidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModuloId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Materias_Modulos_ModuloId",
                        column: x => x.ModuloId,
                        principalTable: "Modulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParametrosValue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ParametrosTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParametrosValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParametrosValue_ParametrosType_ParametrosTypeId",
                        column: x => x.ParametrosTypeId,
                        principalTable: "ParametrosType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MateriasDaTurma",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MateriaId = table.Column<int>(type: "int", nullable: false),
                    ProfId = table.Column<int>(type: "int", nullable: false),
                    ProfessorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MateriasDaTurma", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MateriasDaTurma_ProfessorNew_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "ProfessorNew",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransacaoBoletoEndereco",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CEP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransacaoBoletoId = table.Column<int>(type: "int", nullable: false),
                    TransacaoBoletoId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransacaoBoletoEndereco", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransacaoBoletoEndereco_TransacaoBoleto_TransacaoBoletoId",
                        column: x => x.TransacaoBoletoId,
                        principalTable: "TransacaoBoleto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransacaoBoletoEndereco_TransacaoBoleto_TransacaoBoletoId1",
                        column: x => x.TransacaoBoletoId1,
                        principalTable: "TransacaoBoleto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Calendarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiaAula = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiaDaSemana = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    MateriaId = table.Column<int>(type: "int", nullable: false),
                    HoraInicial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HoraFinal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfessorId = table.Column<int>(type: "int", nullable: false),
                    TurmaId = table.Column<int>(type: "int", nullable: false),
                    UnidadeId = table.Column<int>(type: "int", nullable: false),
                    AulaIniciada = table.Column<bool>(type: "bit", nullable: false),
                    TemReposicao = table.Column<bool>(type: "bit", nullable: false),
                    DateAulaIniciada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AulaConcluida = table.Column<bool>(type: "bit", nullable: false),
                    DateAulaConcluida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Observacoes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SalaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calendarios_Turma_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Horarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeekDayOne = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InitialHourOne = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinalHourOne = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeekDayTwo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InitialHourTwo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinalHourTwo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TurmaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Horarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Horarios_Turma_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Previsoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrevisionStartOne = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrevisionStartTwo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrevisionStartThree = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrevisionEndingOne = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrevisionEndingTwo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrevisionEndingThree = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TurmaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Previsoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Previsoes_Turma_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LivroMatriculas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TurmaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LivroMatriculas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LivroMatriculas_TurmaPedagogico_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "TurmaPedagogico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MateriasTurmas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MateriaId = table.Column<int>(type: "int", nullable: false),
                    TurmaPedagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MateriasTurmas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MateriasTurmas_TurmaPedagogico_TurmaPedagId",
                        column: x => x.TurmaPedagId,
                        principalTable: "TurmaPedagogico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Salas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comentarios = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Capacidade = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    UnidadeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Salas_Unidade_UnidadeId",
                        column: x => x.UnidadeId,
                        principalTable: "Unidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CursosVenda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    ValorUnitario = table.Column<decimal>(type: "decimal(11,2)", precision: 11, scale: 2, nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(11,2)", precision: 11, scale: 2, nullable: false),
                    VendaProdutoId = table.Column<int>(type: "int", nullable: false),
                    VendaCursoAggregateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursosVenda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CursosVenda_VendaCurso_VendaCursoAggregateId",
                        column: x => x.VendaCursoAggregateId,
                        principalTable: "VendaCurso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProdutosVenda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    ValorUnitario = table.Column<decimal>(type: "decimal(11,2)", precision: 11, scale: 2, nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(11,2)", precision: 11, scale: 2, nullable: false),
                    VendaProdutoId = table.Column<int>(type: "int", nullable: false),
                    VendaProdutoAggregateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutosVenda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProdutosVenda_VendaProduto_VendaProdutoAggregateId",
                        column: x => x.VendaProdutoAggregateId,
                        principalTable: "VendaProduto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocumentosEstagio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlunoId = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Analisado = table.Column<bool>(type: "bit", nullable: false),
                    Validado = table.Column<bool>(type: "bit", nullable: false),
                    TipoArquivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentArquivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataFile = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EstagioMatriculaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentosEstagio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentosEstagio_EstagioMatricula_EstagioMatriculaId",
                        column: x => x.EstagioMatriculaId,
                        principalTable: "EstagioMatricula",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LivroMatriculasAlunos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlunoId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LivroMatriculaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LivroMatriculasAlunos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LivroMatriculasAlunos_LivroMatriculas_LivroMatriculaId",
                        column: x => x.LivroMatriculaId,
                        principalTable: "LivroMatriculas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Professores",
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
                    table.PrimaryKey("PK_Professores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Professores_MateriasTurmas_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "MateriasTurmas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoletinsEscolares_HistoricoId",
                table: "BoletinsEscolares",
                column: "HistoricoId");

            migrationBuilder.CreateIndex(
                name: "IX_Calendarios_DiaAula",
                table: "Calendarios",
                column: "DiaAula");

            migrationBuilder.CreateIndex(
                name: "IX_Calendarios_TurmaId",
                table: "Calendarios",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_ContratoConteudo_ContratoId",
                table: "ContratoConteudo",
                column: "ContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_CursosVenda_VendaCursoAggregateId",
                table: "CursosVenda",
                column: "VendaCursoAggregateId");

            migrationBuilder.CreateIndex(
                name: "IX_Debito_InfoFinancId",
                table: "Debito",
                column: "InfoFinancId");

            migrationBuilder.CreateIndex(
                name: "IX_Destinatario_MensagemId",
                table: "Destinatario",
                column: "MensagemId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentosEstagio_EstagioMatriculaId",
                table: "DocumentosEstagio",
                column: "EstagioMatriculaId");

            migrationBuilder.CreateIndex(
                name: "IX_EstagioMatricula_EstagioId",
                table: "EstagioMatricula",
                column: "EstagioId");

            migrationBuilder.CreateIndex(
                name: "IX_Horarios_TurmaId",
                table: "Horarios",
                column: "TurmaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LivroMatriculas_TurmaId",
                table: "LivroMatriculas",
                column: "TurmaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LivroMatriculasAlunos_LivroMatriculaId",
                table: "LivroMatriculasAlunos",
                column: "LivroMatriculaId");

            migrationBuilder.CreateIndex(
                name: "IX_Materias_ModuloId",
                table: "Materias",
                column: "ModuloId");

            migrationBuilder.CreateIndex(
                name: "IX_MateriasDaTurma_ProfessorId",
                table: "MateriasDaTurma",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_MateriasTurmas_TurmaPedagId",
                table: "MateriasTurmas",
                column: "TurmaPedagId");

            migrationBuilder.CreateIndex(
                name: "IX_ParametrosValue_Nome",
                table: "ParametrosValue",
                column: "Nome");

            migrationBuilder.CreateIndex(
                name: "IX_ParametrosValue_ParametrosTypeId",
                table: "ParametrosValue",
                column: "ParametrosTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Previsoes_TurmaId",
                table: "Previsoes",
                column: "TurmaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProdutosVenda_VendaProdutoAggregateId",
                table: "ProdutosVenda",
                column: "VendaProdutoAggregateId");

            migrationBuilder.CreateIndex(
                name: "IX_Professores_MateriaId",
                table: "Professores",
                column: "MateriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Responsaveis_AlunoId",
                table: "Responsaveis",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Salas_UnidadeId",
                table: "Salas",
                column: "UnidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_TransacaoBoletoEndereco_TransacaoBoletoId",
                table: "TransacaoBoletoEndereco",
                column: "TransacaoBoletoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransacaoBoletoEndereco_TransacaoBoletoId1",
                table: "TransacaoBoletoEndereco",
                column: "TransacaoBoletoId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoletinsEscolares");

            migrationBuilder.DropTable(
                name: "Boleto");

            migrationBuilder.DropTable(
                name: "Calendarios");

            migrationBuilder.DropTable(
                name: "Cargo");

            migrationBuilder.DropTable(
                name: "CentroCusto");

            migrationBuilder.DropTable(
                name: "Colaborador");

            migrationBuilder.DropTable(
                name: "ContratoConteudo");

            migrationBuilder.DropTable(
                name: "CursosVenda");

            migrationBuilder.DropTable(
                name: "Debito");

            migrationBuilder.DropTable(
                name: "Destinatario");

            migrationBuilder.DropTable(
                name: "DocumentacaoExigencia");

            migrationBuilder.DropTable(
                name: "Documento_Aluno");

            migrationBuilder.DropTable(
                name: "DocumentosEstagio");

            migrationBuilder.DropTable(
                name: "FornecedorEntrada");

            migrationBuilder.DropTable(
                name: "Fornecedores");

            migrationBuilder.DropTable(
                name: "FornecedorSaida");

            migrationBuilder.DropTable(
                name: "Horarios");

            migrationBuilder.DropTable(
                name: "Leads");

            migrationBuilder.DropTable(
                name: "LivroMatriculasAlunos");

            migrationBuilder.DropTable(
                name: "LivroPresencas");

            migrationBuilder.DropTable(
                name: "Materias");

            migrationBuilder.DropTable(
                name: "MateriasDaTurma");

            migrationBuilder.DropTable(
                name: "Matriculados");

            migrationBuilder.DropTable(
                name: "NotasDisciplinas");

            migrationBuilder.DropTable(
                name: "ParametrosValue");

            migrationBuilder.DropTable(
                name: "PlanoPagamento");

            migrationBuilder.DropTable(
                name: "Previsoes");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "ProdutosVenda");

            migrationBuilder.DropTable(
                name: "Professores");

            migrationBuilder.DropTable(
                name: "ProvasAgenda");

            migrationBuilder.DropTable(
                name: "Responsaveis");

            migrationBuilder.DropTable(
                name: "Salas");

            migrationBuilder.DropTable(
                name: "SenhaBolsa");

            migrationBuilder.DropTable(
                name: "SubConta");

            migrationBuilder.DropTable(
                name: "Testando");

            migrationBuilder.DropTable(
                name: "TransacaoBoletoEndereco");

            migrationBuilder.DropTable(
                name: "TransacaoCartao");

            migrationBuilder.DropTable(
                name: "TypePacote");

            migrationBuilder.DropTable(
                name: "HistoricoEscolar");

            migrationBuilder.DropTable(
                name: "Contratos");

            migrationBuilder.DropTable(
                name: "VendaCurso");

            migrationBuilder.DropTable(
                name: "InfoFinanceira");

            migrationBuilder.DropTable(
                name: "Mensagens");

            migrationBuilder.DropTable(
                name: "EstagioMatricula");

            migrationBuilder.DropTable(
                name: "LivroMatriculas");

            migrationBuilder.DropTable(
                name: "Modulos");

            migrationBuilder.DropTable(
                name: "ProfessorNew");

            migrationBuilder.DropTable(
                name: "ParametrosType");

            migrationBuilder.DropTable(
                name: "Turma");

            migrationBuilder.DropTable(
                name: "VendaProduto");

            migrationBuilder.DropTable(
                name: "MateriasTurmas");

            migrationBuilder.DropTable(
                name: "Aluno");

            migrationBuilder.DropTable(
                name: "Unidade");

            migrationBuilder.DropTable(
                name: "TransacaoBoleto");

            migrationBuilder.DropTable(
                name: "Estagio");

            migrationBuilder.DropTable(
                name: "TurmaPedagogico");
        }
    }
}
