using Dapper;
using DinkToPdf;
using DinkToPdf.Contracts;
using Invictus.Api.Configurations;
using Invictus.Api.Helpers;
using Invictus.Api.HuSignalR;
using Invictus.Api.Identity;
using Invictus.Application.AdmApplication;
using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Application.AutoMapper;
using Invictus.Application.Ioc;
using Invictus.BackgroundTasks;
using Invictus.Core.Enumerations;
using Invictus.Data.Context;
using Invictus.Dtos;
using Invictus.IoC;
using Invictus.QueryService.IoC;
using Invictus.ReportService.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace Invictus.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IHostEnvironment hostEnvironment)
        {
            #region appSettings
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            //f (hostEnvironment.IsDevelopment()) builder.AddUserSecrets<Startup>();

            Configuration = builder.Build();
            #endregion
        }
        public void ConfigureServices(IServiceCollection services)
        {



            #region SQLSERVER
            var conn = Configuration.GetConnectionString("InvictusConnection");
            services.AddDbContext<InvictusDbContext>(
                options => options.UseSqlServer(conn,
                providerOptions =>
                providerOptions.EnableRetryOnFailure()));

            #endregion

            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            services.AddSignalR();
            #region Newtonsoft
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                //options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
            });
            #endregion

            #region Identity

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("InvictusConnection"),
            providerOptions =>
                providerOptions.EnableRetryOnFailure()));
            //services.AddDbContext<InvictusDbContext>(options => options.UseSqlServer(Configuration
            //    .GetConnectionString("InvictusConnection", providerOptions => providerOptions.EnableRetryOnFailure())));
            // options.EnableRetryOnFailure())
            //SqlMapper.AddTypeMap(typeof(TipoTransacao), new TransacaoHandler());
            
            services.AddDefaultIdentity<IdentityUser>(opts =>
            {
                opts.Password.RequiredLength = 8;
                opts.Password.RequireDigit = false;
                opts.Password.RequireLowercase = true;
                opts.Password.RequireUppercase = true;
                opts.Password.RequireNonAlphanumeric = false;
            })
                .AddRoles<IdentityRole>()
                .AddErrorDescriber<IdentityMensagensPortugues>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                //options.Password.RequireDigit = true;
                //options.Password.RequireLowercase = true;
                //options.Password.RequireNonAlphanumeric = true;
                //options.Password.RequireUppercase = true;
                //options.Password.RequiredLength = 6;
                //options.Password.RequiredUniqueChars = 1;

                //// Lockout settings.
                //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                //options.Lockout.MaxFailedAccessAttempts = 5;
                //options.Lockout.AllowedForNewUsers = true;


                // User settings.
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ ";
                options.User.RequireUniqueEmail = true;
            });

            #endregion

            #region AutoMApper
            services.AddAutoMapperConfiguration();
            #endregion

            #region Swagger

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Invictus",
                    Description = "Curso Invictus",
                    Contact = new OpenApiContact() { Name = "Curso Invictus", Email = "invictus.bdazure@gmail.com" },

                });

                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "insira o token: Bearer {token}",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string []{}
                    }
                });
            });

            #endregion

            #region CORS
            services.AddCors(options =>
            {
                options.AddPolicy("EnableCORS", builder =>
                {
                    builder.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod();
                    //.AllowCredentials();
                });
            });

            #endregion

            #region JWT
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();
            var Key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    //ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    //ValidIssuer = appSettings.Emissor,
                    //ValidAudience = appSettings.Validation,
                    IssuerSigningKey = new SymmetricSecurityKey(Key)
                };


            }

            );

            #endregion
            services.AddScoped<ITemplate, TemplateGenerator>();
            services.AddScoped<IRelatorioApp, RelatorioApp>();
            services.AddHostedService<LongRunningService>();
            services.AddSingleton<BackgroundWorkerQueue>();

            RegisterServices(services);
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("EnableCORS");

            app.UseRouting();

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChartHub>("/chart");
                //.RequireCors(MyAllowSpecificOrigins); ;
            });


            if (env.IsDevelopment())
            {
                //SeedData.EnsurePopulated(app, Configuration);
            }
            //SeedData.EnsurePopulated(app, Configuration);


        }

        private static void RegisterServices(IServiceCollection services)
        {
            NativeInjectorBootStrapper.RegisterServices(services);
            QueriesBootStrapper.RegisterServices(services);
            ReportBootStrapper.RegisterServices(services);
            ApplicationBootStrapper.RegisterServices(services);
        }
    }
}
