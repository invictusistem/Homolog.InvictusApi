using Invictus.Application.AdmApplication;
using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Application.FinancApplication;
using Invictus.Application.FinancApplication.Interfaces;
using Invictus.Application.PedagApplication;
using Invictus.Application.PedagApplication.Interfaces;
using Invictus.Application.ReportService;
using Invictus.Application.ReportService.Interfaces;
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

            //pedag
            services.AddScoped<IPedagogicoApplication, PedagogicoApplication>();
            // report services
            services.AddScoped<IReportServices, ReportServices>();
            services.AddScoped<IPDFDesigns, PDFDesigns>();
            // Financ
            services.AddScoped<IBolsasApp, BolsasApp>();
        }
    }
}
