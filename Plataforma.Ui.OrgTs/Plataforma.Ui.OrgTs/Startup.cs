using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Plataforma.Domain.Entities.NotMapped;
using Plataforma.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using System.Collections.Generic;


namespace Plataforma.Ui.OrgTs
{
    public class Startup
    {
        // Variável que recupera a string de conexão
        public static string ConnectionStringPostGres { get; private set; }

        public IConfigurationRoot Configuration { get; }

        public static DadosConexaoEmail DadosEmail { get; private set; }
        public static Guid id_empresa_unifacef { get; private set; }

        // libera acesso as pastas de arquivos do sistema
        private IHostingEnvironment Environment;
        public IConfiguration ConfigurationAuth { get; }        

        public Startup(IHostingEnvironment env, IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            DadosEmail = new DadosConexaoEmail()
            {
                FromAddress = Configuration.GetSection("Email").GetSection("FromAddress").Value,
                PassEmail = Configuration.GetSection("Email").GetSection("Pass").Value,
                UserEmail = Configuration.GetSection("Email").GetSection("User").Value,
                SmtpPortNumber = int.Parse(Configuration.GetSection("Email").GetSection("SmtpPortNumber").Value),
                SmtpServer = Configuration.GetSection("Email").GetSection("SmtpServer").Value
            };

            id_empresa_unifacef = new Guid(Configuration.GetSection("Permissoes").GetSection("id_empresa_unifacef").Value);
            ConfigurationAuth = configuration;
            Environment = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {        
            var sqlConnectionString = Configuration.GetSection("Data:StringPostGres").Value;
            services.AddDbContext<Repository.Context.PlataformaContext>(options => options.UseNpgsql(sqlConnectionString, b => b.MigrationsAssembly("Plataforma.Ui.OrgTs")));
            services.AddAutoMapper();

            // Add framework services.
            
            // Configuration  the dependence injection  
            services.AddSingleton<IConfiguration>(Configuration);
            InjectionDependency.InjecaoDependenciaRepositorios(ref services);
            InjectionDependency.InjecaoDependenciaServicos(ref services);

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("CorsPolicy",
            //        builder => builder.AllowAnyOrigin()
            //        .AllowAnyMethod()
            //        .AllowAnyHeader()
            //        .AllowCredentials());
            //});

            //// Cadastor de todos os perfis de visualização da ("Tribunal", "Pastoral", "Admin"...)
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("Curias", policy => policy.RequireRole("Tribunal", "Pastoral", "Admin"));
            //});

            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/Sistema_Contas/login";
            });

            // Adicionando servicos do framework
            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    // handle loops correctly
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                    // use standard name conversion of properties
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                    // include $id property in the output
                    // options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMvc();           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline. 
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //Fixar Cultura para pt-BR
            RequestLocalizationOptions localizationOptions = new RequestLocalizationOptions
            {
                SupportedCultures = new List<CultureInfo> { new CultureInfo("pt-BR") },
                SupportedUICultures = new List<CultureInfo> { new CultureInfo("pt-BR") },
                DefaultRequestCulture = new RequestCulture("pt-BR")
            };

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            //app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}