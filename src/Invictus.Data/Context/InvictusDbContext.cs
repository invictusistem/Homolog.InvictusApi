using Invictus.Data.Mapping.AdmMappings;
using Invictus.Data.Mapping.PedagMappings;
using Invictus.Domain.Administrativo.AlunoAggregate;
using Invictus.Domain.Administrativo.Calendarios;
using Invictus.Domain.Administrativo.ColaboradorAggregate;
using Invictus.Domain.Administrativo.ContratoAggregate;
using Invictus.Domain.Administrativo.ContratosAggregate;
using Invictus.Domain.Administrativo.DocumentacaoTemplateAggregate;
using Invictus.Domain.Administrativo.Models;
using Invictus.Domain.Administrativo.PacoteAggregate;
using Invictus.Domain.Administrativo.Parametros;
using Invictus.Domain.Administrativo.ProfessorAggregate;
using Invictus.Domain.Administrativo.RegistroMatricula;
using Invictus.Domain.Administrativo.TurmaAggregate;
using Invictus.Domain.Administrativo.UnidadeAggregate;
using Invictus.Domain.Administrativo.UnidadeAuth;
using Invictus.Domain.Padagogico.NotasTurmas;
using Invictus.Domain.Pedagogico.AlunoAggregate;
using Invictus.Domain.Pedagogico.Responsaveis;
using Microsoft.EntityFrameworkCore;

namespace Invictus.Data.Context
{
    public class InvictusDbContext : DbContext
    {
        public InvictusDbContext(DbContextOptions<InvictusDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }


        #region Administrativo
        public DbSet<AgendaTrimestre> AgendasTrimestres { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<AlunoDocumento> AlunosDocs { get; set; }
        public DbSet<AlunoPlanoPagamento> AlunoPlanos { get; set; }
        public DbSet<Autorizacao> Autorizacoes { get; set; }
        public DbSet<Calendario> Calendarios { get; set; }
        public DbSet<Colaborador> Colaboradores { get; set; }
        public DbSet<Conteudo> Conteudos { get; set; }
        public DbSet<Contrato> Contratos { get; set; }
        public DbSet<DocumentacaoTemplate> DocumentacoesTemplate { get; set; }
        public DbSet<MateriaTemplate> MateriasTemplates { get; set; }
        public DbSet<DocumentacaoExigencia> DocumentacoesExigencias { get; set; }
        public DbSet<PacoteMateria> Materias { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }
        public DbSet<Pacote> Pacotes { get; set; }
        public DbSet<ParametrosKey> ParametrosKeys { get; set; }
        public DbSet<ParametrosValue> ParametrosValues { get; set; }
        public DbSet<PlanoPagamentoTemplate> PlanosPgmTemplate { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Disponibilidade> Disponibilidades { get; set; }
        public DbSet<MateriaHabilitada> MateriasHabilitadas { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<Responsavel> Responsaveis { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<Previsoes> Previsoes { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<TurmaMaterias> TurmasMaterias { get; set; }
        public DbSet<TypePacote> TypePacotes { get; set; }
        public DbSet<Sala> Salas { get; set; }
        public DbSet<Unidade> Unidades { get; set; }
        #endregion

        #region PEDAGOGICO
        public DbSet<TurmaNotas> TurmasNotas { get; set; }
        public DbSet<AlunoAnotacao> AlunosAnotacoes { get; set; }

        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            AdmModelBuilder.RegisterBuilders(modelBuilder);

            PedagModelBuilder.RegisterBuilders(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}
