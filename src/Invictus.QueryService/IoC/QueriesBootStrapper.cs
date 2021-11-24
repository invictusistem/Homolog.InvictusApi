

using Invictus.QueryService.AdministrativoQueries;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Invictus.QueryService.PedagogicoQueries;
using Invictus.QueryService.PedagogicoQueries.Interfaces;
using Invictus.QueryService.Utilitarios;
using Invictus.QueryService.Utilitarios.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Invictus.QueryService.IoC
{
    public class QueriesBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASPNET
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IAgendaTriQueries, AgendaTriQueries>();
            services.AddScoped<IAlunoQueries, AlunoQueries>();
            services.AddScoped<IAutorizacaoQueries, AutorizacaoQueries>();
            services.AddScoped<IColaboradorQueries, ColaboradorQueries>();
            services.AddScoped<IContratoQueries, ContratoQueries>();
            services.AddScoped<IDocTemplateQueries, DocTemplateQueries>();
            services.AddScoped<IMateriaTemplateQueries, MateriaTemplateQueries>();
            services.AddScoped<IMatriculaQueries, MatriculaQueries>();
            services.AddScoped<IPacoteQueries, PacoteQueries>();
            services.AddScoped<IParametrosQueries, ParametrosQueries>();
            services.AddScoped<IPlanoPagamentoQueries, PlanoPagamentoQueries>();
            services.AddScoped<IProdutoQueries, ProdutoQueries>();
            services.AddScoped<IProfessorQueries, ProfessorQueries>();
            services.AddScoped<ITemplateQueries, TemplateQueries>();
            services.AddScoped<IUnidadeQueries, UnidadeQueries>();
            services.AddScoped<ITurmaQueries, TurmaQueries>();
            services.AddScoped<ITypePacoteQueries, TypePacoteQueries>();
            services.AddScoped<IUsuariosQueries, UsuariosQueries>();

            services.AddScoped<IUtils, Utils>();
            // Pedag
            services.AddScoped<IPedagMatriculaQueries, PedagMatriculaQueries>();
            services.AddScoped<ITurmaPedagQueries, TurmaPedagQueries>();
        }

    }
}
