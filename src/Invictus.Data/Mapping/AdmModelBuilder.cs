using Invictus.Data.Mapping.AdmMappings;
using Invictus.Data.Mapping.AdmMappings.AgendaTrimestreMapping;
using Invictus.Data.Mapping.AdmMappings.AlunoMapping;
using Invictus.Data.Mapping.AdmMappings.AutorizacaoMapping;
using Invictus.Data.Mapping.AdmMappings.CalendarioMapping;
using Invictus.Data.Mapping.AdmMappings.ColaboradorMapping;
using Invictus.Data.Mapping.AdmMappings.ContratoMapping;
using Invictus.Data.Mapping.AdmMappings.DocumentacaoTemplateMapping;
using Invictus.Data.Mapping.AdmMappings.FuncionarioMapping;
using Invictus.Data.Mapping.AdmMappings.LogMapping;
using Invictus.Data.Mapping.AdmMappings.MatriculaMapping;
using Invictus.Data.Mapping.AdmMappings.ModelMapping;
using Invictus.Data.Mapping.AdmMappings.PacoteMapping;
using Invictus.Data.Mapping.AdmMappings.ParametrosMapping;
using Invictus.Data.Mapping.AdmMappings.ProdutoMapping;
using Invictus.Data.Mapping.AdmMappings.ProfessorMapping;
using Invictus.Data.Mapping.AdmMappings.RequerimentoMapping;
using Invictus.Data.Mapping.AdmMappings.ResponsavelMapping;
using Invictus.Data.Mapping.AdmMappings.TurmaAggregateMapping;
using Invictus.Data.Mapping.AdmMappings.UnidadeMapping;
using Microsoft.EntityFrameworkCore;

namespace Invictus.Data.Mapping
{
    public class AdmModelBuilder
    {
        public static void RegisterBuilders(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AgendaTrimestreDbMapping());
            modelBuilder.ApplyConfiguration(new AlunoDbMapping());
            modelBuilder.ApplyConfiguration(new AlunoDocumentoDbMapping());
            modelBuilder.ApplyConfiguration(new AlunoPlanoPagamentoDbMapping());
            modelBuilder.ApplyConfiguration(new AutorizacaoDbMapping());
            modelBuilder.ApplyConfiguration(new CalendarioDbMapping());
            modelBuilder.ApplyConfiguration(new AnotacaoColaboradorDbMapping());
            modelBuilder.ApplyConfiguration(new ColaboradorDbMapping());
            modelBuilder.ApplyConfiguration(new ConteudoDbMapping());
            modelBuilder.ApplyConfiguration(new ContratoDbMapping());
            modelBuilder.ApplyConfiguration(new DocumentacaoTemplateDbMapping());
            modelBuilder.ApplyConfiguration(new EnderecoDbMapping());
            modelBuilder.ApplyConfiguration(new PessoaDbMapping());
            modelBuilder.ApplyConfiguration(new MateriaTemplateDbMapping());
            modelBuilder.ApplyConfiguration(new MatriculaDbMapping());
            modelBuilder.ApplyConfiguration(new DocumentaoExigenciaDbMapping());
            modelBuilder.ApplyConfiguration(new LogBoletoDbMapping());
            modelBuilder.ApplyConfiguration(new LogCalendarioDbMapping());
            modelBuilder.ApplyConfiguration(new LogLoginDbMapping());
            modelBuilder.ApplyConfiguration(new LogMatriculasDbMapping());
            modelBuilder.ApplyConfiguration(new LogProdutoDbMapping());
            modelBuilder.ApplyConfiguration(new LogTurmasDbMapping());
            modelBuilder.ApplyConfiguration(new MateriaDbMapping());
            modelBuilder.ApplyConfiguration(new PacoteDbMapping());
            modelBuilder.ApplyConfiguration(new ParametrosKeyDbMapping());
            modelBuilder.ApplyConfiguration(new ParametrosValueDbMapping());
            modelBuilder.ApplyConfiguration(new PlanoPagamentoTemplateDbMapping());
            modelBuilder.ApplyConfiguration(new ProdutoDbMapping());
            modelBuilder.ApplyConfiguration(new ProdutoLogDbMapping());
            modelBuilder.ApplyConfiguration(new DisponibilidadeDbMapping());
            modelBuilder.ApplyConfiguration(new MateriaHabilitadaDbMapping());
            modelBuilder.ApplyConfiguration(new ProfessorDbMapping());
            modelBuilder.ApplyConfiguration(new RequerimentoDbMapping());
            modelBuilder.ApplyConfiguration(new TipoRequerimentoDbMapping());
            modelBuilder.ApplyConfiguration(new ResponsavelDbMapping());
            modelBuilder.ApplyConfiguration(new HorariosDbMapping());
            modelBuilder.ApplyConfiguration(new PresencaDbMapping());
            modelBuilder.ApplyConfiguration(new PrevisoesDbMapping());
            modelBuilder.ApplyConfiguration(new TurmaDbMaping());
            modelBuilder.ApplyConfiguration(new TurmaMateriaDbMapping());
            modelBuilder.ApplyConfiguration(new TurmaProfessorDbMapping());
            modelBuilder.ApplyConfiguration(new TypePacoteDbMapping());
            modelBuilder.ApplyConfiguration(new SalaDbMapping());
            modelBuilder.ApplyConfiguration(new UnidadeDbMapping());

            modelBuilder.ApplyConfiguration(new TransacaoDbMapping());
        }
    }
}
