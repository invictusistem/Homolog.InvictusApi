//using DinkToPdf;
//using DinkToPdf.Contracts;
using DinkToPdf;
using DinkToPdf.Contracts;
using Invictus.Api.Configuration;
using Invictus.Api.Data;
using Invictus.Api.Model;
using Invictus.Application.Dtos;
using Invictus.Cross;
using Invictus.Data.Context;
using Invictus.Domain.Administrativo.AlunoAggregate.Interfaces;
using Invictus.Domain.Administrativo.TurmaAggregate.Interfaces;
using Invictus.Domain.Pedagogico.HistoricoEscolarAggregate.Interfaces;
using Invictus.Domain.Pedagogico.Models.IPedagModelRepository;
using Invictus.Domain.Pedagogico.TurmaAggregate.Interfaces;
using Invictus.Domain.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Text;
//using Wkhtmltopdf.NetCore;

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


        //readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public void ConfigureServices(IServiceCollection services)
        {
            var conn = Configuration.GetConnectionString("InvictusConnection");
            #region Identity
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("InvictusConnection"),
            providerOptions =>
                providerOptions.EnableRetryOnFailure()));
            //services.AddDbContext<InvictusDbContext>(options => options.UseSqlServer(Configuration
            //    .GetConnectionString("InvictusConnection", providerOptions => providerOptions.EnableRetryOnFailure())));
            // options.EnableRetryOnFailure())

            services.AddDbContext<InvictusDbContext>(
                options => options.UseSqlServer(conn,
                providerOptions =>
                providerOptions.EnableRetryOnFailure()));

            //services.AddSingleton(typeof(IConverter),
            //new SynchronizedConverter(new PdfTools()));

            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            services.AddControllers().AddNewtonsoftJson(options =>
                  options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
               );

            services.AddDefaultIdentity<IdentityUser>(opts =>
            {
                opts.Password.RequiredLength = 8;
                opts.Password.RequireDigit = false;
                opts.Password.RequireLowercase = true;
                opts.Password.RequireUppercase = true;
                opts.Password.RequireNonAlphanumeric = true;
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



            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();



            services.AddAutoMapperConfiguration();

            #region Swagger

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Invictus",
                    Description = "Curso Invictus",
                    Contact = new OpenApiContact() { Name = "Álvaro Carlos", Email = "invictus.bdazure@gmail.com" },

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
                });
            });

            #endregion

            
            //services.Configure<FormOptions>(o => {
            //    o.ValueLengthLimit = int.MaxValue;
            //    o.MultipartBodyLengthLimit = int.MaxValue;
            //    o.MemoryBufferThreshold = int.MaxValue;
            //});



            #region JWT
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

            //services.AddWkhtmltopdf("wkhtmltopdf");
            //var context = new CustomAssemblyLoadContext();
            //context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "wkhtmltox\\v0.12.4\\libwkhtmltox.dll"));

            //services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
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





            //app.UseCors(cors =>
            //{
            //    cors.AllowAnyHeader();
            //    cors.AllowAnyMethod();
            //    cors.AllowAnyOrigin();

            //});

            //app.UseStaticFiles();
            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
            //    RequestPath = new PathString("/Resources")
            //});
            //app.UseCors(MyAllowSpecificOrigins);

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //.RequireCors(MyAllowSpecificOrigins); ;
            });
            if (env.IsDevelopment())
            {
                //SeedData.EnsurePopulated(app, Configuration);
            }

            //SeedData.EnsurePopulated(app, Configuration);
            //var wkHtmlToPdfPath = "";
            //if (env.IsDevelopment())
            //{
            // wkHtmlToPdfPath = Path.Combine(env.ContentRootPath, $"wkhtmltox\\v0.12.4\\libwkhtmltox");
            // SeedData.EnsurePopulated(app, Configuration);
            //}
            //else
            //{
            //wkHtmlToPdfPath = Path.Combine(env.ContentRootPath, $"C:\\Hosting\\alvaro.junior\\api.invictustemp.com\\wwwroot\\wkhtmltox\\v0.12.4\\libwkhtmltox");

            //var wkHtmlToPdfPath = Path.Combine(env.ContentRootPath, $"C:\\Hosting\\alvaro.junior\\api.invictustemp.com\\wwwroot\\wkhtmltox\\v0.12.4\\libwkhtmltox");
            var wkHtmlToPdfPath = Path.Combine(env.ContentRootPath, $"wkhtmltox\\v0.12.4\\libwkhtmltox.dll");
            //var wkHtmlToPdfPath = $"C:\\Hosting\\alvaro.junior\\api.invictustemp.com\\wwwroot\\wkhtmltox\\v0.12.4\\libwkhtmltox";
            //var wkHtmlToPdfPath = $"C:\\Projetos\\INVICTUS\\back\\Invictus\\src\\Invictus.Api\\wkhtmltox\\v0.12.4\\libwkhtmltox";
            CustomAssemblyLoadContext context = new CustomAssemblyLoadContext();
            context.LoadUnmanagedLibrary(wkHtmlToPdfPath);

            // Process.Start("C:\\Projetos\\INVICTUS\\back\\Invictus\\src\\Invictus.Api\\Worker\\WorkerService1.exe");
        }

        private static void RegisterServices(IServiceCollection services)
        {
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}
