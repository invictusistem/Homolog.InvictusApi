using Invictus.Application.Queries;
using Invictus.Application.Queries.Interfaces;
using Invictus.Data;
using Invictus.Data.Context;
using Invictus.Data.Repository;
using Invictus.Domain.Administrativo.AlunoAggregate.Interfaces;
using Invictus.Domain.Pedagogico.HistoricoEscolarAggregate.Interfaces;
using Invictus.Domain.Pedagogico.Models.IPedagModelRepository;
using Invictus.Domain.Pedagogico.TurmaAggregate.Interfaces;
using Invictus.Domain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Invictus.Application.Services;
using static Invictus.Application.Services.EmailMessage;
using Invictus.Application.AuthApplication;
using Invictus.Application.AuthApplication.Interface;
using Invictus.Application.FinanceiroAppication;
using Invictus.Application.FinanceiroAppication.interfaces;
using Invictus.Application.Services.Interface;

namespace Invictus.Cross
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASPNET
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Application
            services.AddScoped<IAuthApplication, AuthApplication>();
            services.AddScoped<IFinanceiroApp, FinanceiroApp>();
            services.AddScoped<IBoletoService, BoletoService>();
            services.AddScoped<IAdmApplication, AdmApplication>();
            services.AddScoped<IRelatorioExcel, RelatorioExcel>();

            // Services
            services.AddTransient<IEmailSender, AuthMessageSender>();

            // Infra - Data
            services.AddScoped<IColaboradorRepository, ColaboradorRepository>();
            services.AddScoped<ICursoRepository, CursoRepository>();
            services.AddScoped<IAlunoRepository, AlunoRepository>();
            services.AddScoped<ILeadRepository, LeadRepository>();
            services.AddScoped<IHistoricoEscolarRepo, HistoricoRepository>();
            services.AddScoped<ICalendarioRepository, CalendarioRepository>();
            services.AddScoped<ITurmaRepository, TurmaRepository>();
            services.AddScoped<IAgendaProvasRepository, AgendaProvasRepository>();
            services.AddScoped<IPedagModelsRepository, PedagModelsRepository>();
            services.AddScoped<ITurmaPedagRepository, TurmaPedagogicoRepository>();
            services.AddScoped<InvictusDbContext>();

            // Queries
            services.AddScoped<IFinanceiroQueries, FinanceiroQueries>();
            services.AddScoped<IColaboradorQueries, ColaboradorQueries>();
            services.AddScoped<ICursoQueries, CursoQueries>();
            services.AddScoped<IMatriculaQueries, MatriculaQueries>();
            services.AddScoped<IModuloQueries, ModuloQuery>();
            services.AddScoped<ICalendarioQueries, CalendarioQueries>();
            services.AddScoped<IPedagogicoQueries, PedagogicoQuery>();
            services.AddScoped<IPedagModelsQueries, PedagModelsQueries>();
            services.AddScoped<IMateriaQueries, MateriaQuery>();
            services.AddScoped<ITurmaQueries, TurmaQueries>();
            services.AddScoped<IUnidadeQueries, UnidadeQueries>();
            services.AddScoped<IEstagioQueries, EstagioQueries>();
            services.AddScoped<IAlunoQueries, AlunoQueries>();
            services.AddScoped<IComercialQueries, ComercialQueries>();
        }
    }
}
