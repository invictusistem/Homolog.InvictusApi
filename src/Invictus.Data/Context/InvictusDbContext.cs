using Invictus.Data.Mapping;
using Invictus.Data.Mapping.AdministrativoMappings.AlunoMapping;
using Invictus.Data.Mapping.AdministrativoMappings.ColaboradorMapping;
using Invictus.Data.Mapping.AdministrativoMappings.ContratoMapping;
using Invictus.Data.Mapping.AdministrativoMappings.ModelsMapping;
using Invictus.Data.Mapping.AdministrativoMappings.ModuloMapping;
using Invictus.Data.Mapping.AdministrativoMappings.ParametrosMapping;
using Invictus.Data.Mapping.AdministrativoMappings.ProfessorMapping;
using Invictus.Data.Mapping.AdministrativoMappings.TurmaMappings;
using Invictus.Data.Mapping.AdministrativoMappings.UnidadeMapping;
using Invictus.Data.Mapping.ComercialMapping.Leads;
using Invictus.Data.Mapping.FinanceiroMapping.Configuracoes;
using Invictus.Data.Mapping.FinanceiroMapping.FornecedorMapping;
using Invictus.Data.Mapping.FinanceiroMapping.InfoFinancMapping;
using Invictus.Data.Mapping.FinanceiroMapping.ProdutoMapping;
using Invictus.Data.Mapping.FinanceiroMapping.TransacoesMapping;
using Invictus.Data.Mapping.FinanceiroMapping.VendaCurso;
using Invictus.Data.Mapping.FinanceiroMapping.VendaProduto;
using Invictus.Data.Mapping.PedagogicoMapping.HistoricoMapping;
using Invictus.Data.Mapping.PedagogicoMapping.ModelsMapping;
using Invictus.Data.Mapping.PedagogicoMapping.TurmaMapping;
using Invictus.Domain;
using Invictus.Domain.Administrativo.AlunoAggregate;
using Invictus.Domain.Administrativo.ColaboradorAggregate;
using Invictus.Domain.Administrativo.ContratosAggregate;
using Invictus.Domain.Administrativo.Models;
using Invictus.Domain.Administrativo.PacoteAggregate;
using Invictus.Domain.Administrativo.Parametros;
using Invictus.Domain.Administrativo.ProfessorAggregate;
using Invictus.Domain.Administrativo.TurmaAggregate;
using Invictus.Domain.Administrativo.UnidadeAggregate;
using Invictus.Domain.Comercial.Leads;
using Invictus.Domain.Financeiro.Aluno;
using Invictus.Domain.Financeiro.Configuracoes;
using Invictus.Domain.Financeiro.Fornecedor;
using Invictus.Domain.Financeiro.NewFolder;
using Invictus.Domain.Financeiro.Transacoes;
using Invictus.Domain.Financeiro.VendaCurso;
using Invictus.Domain.Financeiro.VendaProduto;
using Invictus.Domain.Pedagogico.EstagioAggregate;
using Invictus.Domain.Pedagogico.HistoricoEscolarAggregate;
using Invictus.Domain.Pedagogico.Models;
using Invictus.Domain.Pedagogico.TurmaAggregate;
using Microsoft.EntityFrameworkCore;

namespace Invictus.Data.Context
{
    public class InvictusDbContext : DbContext
    {   
        public InvictusDbContext(DbContextOptions<InvictusDbContext> options) : base(options) { }

        #region Administrativo
        // Unidade Aggre
        public DbSet<Unidade> Unidades { get; set; }
        public DbSet<Pacote> Modulos { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<DocumentacaoExigencia> DocsExigencias { get; set; }
        public DbSet<Sala> Salas { get; set; }
        //public DbSet<PlanoPagamento> Planos { get; set; }

        // Models
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<TypePacote> TypePacotes { get; set; }
        public DbSet<Matriculados> Matriculados { get; set; }
        public DbSet<ProfessorNew> ProfessoresNew { get; set; }
        public DbSet<MateriasDaTurma> MateriasDaTurma { get; set; }
        public DbSet<Mensagem> Mensagens { get; set; }
        public DbSet<DocumentoAluno> DocumentosAlunos { get; set; }
        public DbSet<PlanoPagamento> Planos { get; set; }
       // public DbSet<Pacote> Pacotes { get; set; }
        public DbSet<SenhaBolsas> SenhasBolsas { get; set; }
        // Parametros 
        public DbSet<ParametrosType> ParametrosTypes { get; set; }
        public DbSet<ParametrosValue> ParametrosValues { get; set; }
        // Turma Aggr
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<HorarioBase> HorariosBase { get; set; }
        public DbSet<Previsao> Previsoes { get; set; }
        public DbSet<Calendario> Calendarios { get; set; }
        // Aluno Aggr
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Responsavel> Responsaveis { get; set; }
        //Colaborador Agg
        public DbSet<Colaborador> Colaboradores { get; set; }
        public DbSet<DocumentacaoColaborador> DocumentacoesColaborador { get; set; }

    //Contratos

    public DbSet<Contrato> Contratos { get; set; }
        public DbSet<Conteudo> Conteudos { get; set; }
        // Professores
        public DbSet<Domain.Administrativo.ProfessorAggregate.Professor> Professores { get; set; }
        public DbSet<MateriasHabilitadas> MateriasHabilitadas { get; set; }


        #endregion

        #region Pedagogico
        // TurmaPedagAggregate
        public DbSet<TurmaPedagogico> TurmaPedag { get; set; }
        //public DbSet<Domain.Pedagogico.TurmaAggregate.Professor> Professores { get; set; }
        public DbSet<MateriaPedag> ProfMaterias { get; set; }
        public DbSet<LivroMatriculaAlunos> LivroMatriculaAlunos { get; set; }
        public DbSet<LivroMatricula> LivroMatricula { get; set; }
        // HistoricoAggregate
        public DbSet<HistoricoEscolar> HistoricosEscolares { get; set; }
        public DbSet<BoletimEscolar> BoletinsEscolares { get; set; }
        //public DbSet<ListaPresenca> ListaPresencas { get; set; }
        public DbSet<NotasDisciplinas> NotasDisciplinas { get; set; }

        public DbSet<ProvasAgenda> ProvasAgenda { get; set; }
        public DbSet<Estagio> Estagios { get; set; }
        public DbSet<EstagioMatricula> EstagioMatriculas { get; set; }
        public DbSet<Documento> DocumentosEStagio { get; set; }

        public DbSet<LivroPresenca> LivroPresencas { get; set; }

        #endregion

        #region Comercial

        public DbSet<Lead> Leads { get; set; }

        #endregion

        #region Financeiro
        // InfoFinanceiro
        public DbSet<InformacaoFinanceiraAggregate> InfoFinanceiras { get; set; }
        public DbSet<Debito> Debitos { get; set; }
        //public DbSet<ValorEntrada> Entradas { get; set; }

        // Fornecedores
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<FornecedorEntrada> FornecedoresEntradas { get; set; }
        public DbSet<FornecedorSaida> FornecedoresSaidas { get; set; }

        // Produto
        public DbSet<Produto> Produtos { get; set; }

        // Transacoes
        public DbSet<TransacaoBoletoAggregate> TransacoesBoletos { get; set; }
        public DbSet<TransacaoBoletoEndereco> TransacoesBoletosAggregate { get; set; }
        public DbSet<TransacaoCartaoAggregate> TransacoesCartoes { get; set; }
        public DbSet<Boleto> Boletos { get; set; }
        //VendaProduto
        public DbSet<VendaProdutoAggregate> VendasProdutos { get; set; }
        public DbSet<ProdutoVenda> ProdutosDaVenda { get; set; }
        //CursoVenda
        public DbSet<VendaCursoAggregate> VendasCursos { get; set; }
        public DbSet<CursoVenda> CursosDaVenda { get; set; }
        // Configurações
        public DbSet<SubConta> SubContas { get; set; }
        public DbSet<CentroCusto> CentroCustos { get; set; }

        #endregion

        public DbSet<TestandoEnum> Testando { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Administrativo
            // Unidade Aggre
            //modelBuilder.ApplyConfiguration(new UnidadeDbMapping());
            //private static void RegisterServices(IServiceCollection services)
            //{
            //    NativeInjectorBootStrapper.RegisterServices(services);
            //}
            AdmModelBuilder.RegisterBuilders(modelBuilder);
            modelBuilder.ApplyConfiguration(new MateriaDbMapping());
            modelBuilder.ApplyConfiguration(new ModuloDbMapping());
            modelBuilder.ApplyConfiguration(new DocumentaoExigenciaDbMapping());
            modelBuilder.ApplyConfiguration(new SalaDbMapping());
            // Models
            modelBuilder.ApplyConfiguration(new CargoDbMapping());
            modelBuilder.ApplyConfiguration(new MateriasDaTurmaMapping());
            modelBuilder.ApplyConfiguration(new ProfessorNewDbMapping());
            modelBuilder.ApplyConfiguration(new MateriasDaTurmaMapping());
            modelBuilder.ApplyConfiguration(new MensagemDbMapping());
            modelBuilder.ApplyConfiguration(new DocumentoAlunoDbMapping());
            modelBuilder.ApplyConfiguration(new PlanoPagamentoDbMapping());
           // modelBuilder.ApplyConfiguration(new PacoteDbMapping());
            modelBuilder.ApplyConfiguration(new TypePacoteDbMapping());
            modelBuilder.ApplyConfiguration(new SenhaBolsasDbMapping());
            // Parametros 
            modelBuilder.ApplyConfiguration(new ParametrosTypeDbMapping());
            modelBuilder.ApplyConfiguration(new ParametrosValueDbMapping());

            // Turma Aggr
            modelBuilder.ApplyConfiguration(new TurmaDbMapping());
            modelBuilder.ApplyConfiguration(new HorarioBaseDbMapping());
            modelBuilder.ApplyConfiguration(new PrevisoesDbMapping());
            modelBuilder.ApplyConfiguration(new CalendarioDbMapping());

           // modelBuilder.ApplyConfiguration(new PlanoPagamentoDbMapping());
            // Aluno Aggr
            modelBuilder.ApplyConfiguration(new AlunoDbMapping());
            modelBuilder.ApplyConfiguration(new ResponsavelDbMapping());
            //Colaborador Agg
            modelBuilder.ApplyConfiguration(new ColaboradorDbMapping());
            modelBuilder.ApplyConfiguration(new DocumentacaoColaboradorDbMapping());
            // Contratos Ag
            modelBuilder.ApplyConfiguration(new ContratoDbMapping());
            modelBuilder.ApplyConfiguration(new ConteudoDbMapping());
            // Professores
            modelBuilder.ApplyConfiguration(new ProfessoresDbMapping());
            modelBuilder.ApplyConfiguration(new MateriasHabilitadasDbMapping());

            #endregion

            #region Pedagogico
            // TurmaPedagAggregate
            modelBuilder.ApplyConfiguration(new TurmaPedagDbMapping());
            //modelBuilder.ApplyConfiguration(new ProfessorDbMapping());
            modelBuilder.ApplyConfiguration(new Mapping.PedagogicoMapping.TurmaMapping.MateriaPedagDbMapping());
            modelBuilder.ApplyConfiguration(new LivroMatriculaDbMapping());
            modelBuilder.ApplyConfiguration(new LivroMatriculaAlunosDbMapping());

            modelBuilder.ApplyConfiguration(new HistoricoEscolarDbMapping());
            modelBuilder.ApplyConfiguration(new BoletimDbMapping());
            ///modelBuilder.ApplyConfiguration(new ListaPresencaDbMapping());
            modelBuilder.ApplyConfiguration(new NotasDisciplinaDbMapping());

            modelBuilder.ApplyConfiguration(new EstagioDbMapping());
            modelBuilder.ApplyConfiguration(new EstagioMatriculaDbMapping());
            modelBuilder.ApplyConfiguration(new DocumentoDbMapping());

            modelBuilder.ApplyConfiguration(new LivroPresencaDbMapping());

            #endregion

            #region Comercial

            modelBuilder.ApplyConfiguration(new LeadDbMapping());

            #endregion

            #region Financeiro

            modelBuilder.ApplyConfiguration(new InfoFinancDbMapping());
            modelBuilder.ApplyConfiguration(new DebitoDbMapping());
            //modelBuilder.ApplyConfiguration(new ValorEntradaDbMapping());
            
            modelBuilder.ApplyConfiguration(new FornecedorDbMapping());
            modelBuilder.ApplyConfiguration(new FornecedorSaidaDbMapping());
            modelBuilder.ApplyConfiguration(new FornecedorEntradaDbMapping());

            modelBuilder.ApplyConfiguration(new ProdutoDbMapping());

            modelBuilder.ApplyConfiguration(new TransacaoBoletoAggDbMapping());
            modelBuilder.ApplyConfiguration(new TransacaoBoletoEnderecoDbMapping());
            modelBuilder.ApplyConfiguration(new TransacaoCartaoDbMapping());
            modelBuilder.ApplyConfiguration(new BoletoDbMapping());

            modelBuilder.ApplyConfiguration(new VendaProdutoDbMapping());
            modelBuilder.ApplyConfiguration(new ProdutoVendaDbMapping());

            modelBuilder.ApplyConfiguration(new VendaCursoDbMapping());
            modelBuilder.ApplyConfiguration(new CursoVendaDbMapping());

            modelBuilder.ApplyConfiguration(new CentroCustoDbMapping());
            modelBuilder.ApplyConfiguration(new SubContaDbMapping());

            #endregion

            modelBuilder.ApplyConfiguration(new TesteDbMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}
