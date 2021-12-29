using Invictus.Application.AdmApplication;
using Invictus.Application.AdmApplication.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Invictus.Application.Ioc
{
    public class ApplicationBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASPNET
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // app application            
            services.AddScoped<IAgendaTriApplication, AgendaTriApplication>();
            services.AddScoped<IAlunoApplication, AlunoApplication>();
            services.AddScoped<IColaboradorApplication, ColaboradorApplication>();
            services.AddScoped<IContratoApplication, ContratoApplication>();
            services.AddScoped<IDocTemplateApplication, DocTemplateApplication>();
            services.AddScoped<IMateriaTemplateApplication, MateriaTemplateApplication>();
            services.AddScoped<IMatriculaApplication, MatriculaApplication>();
            services.AddScoped<IPacoteApplication, PacoteApplication>();
            services.AddScoped<IParametroApplication, ParametroApplication>();
            services.AddScoped<IPedagDocApp, PedagDocApp>();
            services.AddScoped<IPlanoPagamentoApplication, PlanoPagamentoApplication>();
            services.AddScoped<IProdutoApplication, ProdutoApplication>();
            services.AddScoped<IProfessorApplication, ProfessorApplication>();
            services.AddScoped<ITurmaApplication, TurmaApplication>();
            services.AddScoped<IUnidadeApplication, UnidadeApplication>();
            services.AddScoped<IUsuarioApplication, UsuarioApplication>();
            services.AddScoped<IBoletoService, BoletoService>();

        }
    }
}
