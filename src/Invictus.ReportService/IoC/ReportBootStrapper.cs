using Invictus.ReportService.Queries;
using Invictus.ReportService.Queries.Interfaces;
using Invictus.ReportService.Reports;
using Invictus.ReportService.Reports.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Invictus.ReportService.IoC
{
    public class ReportBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IReportAdm, ReportAdm>();
            services.AddScoped<IReportFin, ReportFin>();
            services.AddScoped<IReportPedag, ReportPedag>();

            services.AddScoped<IAdmReportQueries, AdmReportQueries>();
        }
    }
}
