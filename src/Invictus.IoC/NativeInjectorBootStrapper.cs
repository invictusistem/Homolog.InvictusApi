using Invictus.Application.Services;
using Invictus.Core;
using Invictus.Core.Interfaces;
using Invictus.Data.Repositories.Administrativo;
using Invictus.Data.Repositories.Financeiro;
using Invictus.Data.Repositories.Pedagogico;
using Invictus.Domain.Administrativo.AdmProduto.Interfaces;
using Invictus.Domain.Administrativo.AgendaTri.Interfaces;
using Invictus.Domain.Administrativo.AlunoAggregate.Interface;
using Invictus.Domain.Administrativo.Calendarios.Interfaces;
using Invictus.Domain.Administrativo.ColaboradorAggregate.Interfaces;
using Invictus.Domain.Administrativo.ContratoAggregate.Interfaces;
using Invictus.Domain.Administrativo.DocumentacaoTemplateAggregate.Interface;
using Invictus.Domain.Administrativo.MatriculaRegistro;
using Invictus.Domain.Administrativo.MatTemplate.Interfaces;
using Invictus.Domain.Administrativo.PacoteAggregate.Interfaces;
using Invictus.Domain.Administrativo.Parametros.Interfaces;
using Invictus.Domain.Administrativo.PlanoPagamento;
using Invictus.Domain.Administrativo.ProfessorAggregate.Interfaces;
using Invictus.Domain.Administrativo.TurmaAggregate.Interfaces;
using Invictus.Domain.Administrativo.UnidadeAggregate.Interfaces;
using Invictus.Domain.Administrativo.UnidadeAuth.Interfaces;
using Invictus.Domain.Financeiro.Interfaces;
using Invictus.Domain.Padagogico.AlunoAggregate.Interfaces;
using Invictus.Domain.Padagogico.NotasTurmas.Interface;
using Invictus.Domain.Pedagogico.Responsaveis.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;


namespace Invictus.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASPNET
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();
            services.AddScoped<IEmailSender, EmailMessage>();

            // repositories
            services.AddScoped<IUnidadeRepository, UnidadeRepository>();
            services.AddScoped<ICalendarioRepo, CalendarioRepo>();
            services.AddScoped<IAgendaTriRepository, AgendaTriRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IDocTemplateRepository, DocTemplateRepository>();
            services.AddScoped<IMateriaRepo, MateriaRepository>();
            //services.AddScoped<IModelRepository, ModelRepository>();
            services.AddScoped<IPlanoPgmRepository, PlanoPgmRepository>();
            services.AddScoped<IContratoRepository, ContratoRepository>();
            services.AddScoped<IPacoteRepository, PacoteRepository>();
            services.AddScoped<IColaboradorRepository, ColaboradorRepository>();
            services.AddScoped<IProfessorRepository, ProfessorRepository>();
            services.AddScoped<ITurmaRepo, TurmaRepo>();
            services.AddScoped<IParametroRepo, ParametroRepo>();
            services.AddScoped<IAlunoRepo, AlunoRepo>();
            services.AddScoped<IMatriculaRepo, MatriculaRepo>();
            services.AddScoped<ITurmaNotasRepo, TurmaNotasRepo>();
            services.AddScoped<IAutorizacaoRepo,AutorizacaoRepo > ();
            // Pedag
            services.AddScoped<IAlunoPedagRepo, AlunoPedagRepo>();
            services.AddScoped<IRespRepo, RespRepo>();
            // Financ
            services.AddTransient<IDebitosRepos, InfoFinancRepo>();


        }
    }
}