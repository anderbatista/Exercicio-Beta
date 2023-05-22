using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ProjetoAPI.Data;
using ProjetoAPI.Data.DAO;
using ProjetoAPI.Data.Interfaces;
using ProjetoAPI.Data.Repository;
using ProjetoAPI.Services;
using System;
using Newtonsoft.Json;
using ProjetoAPI.Middleware;
//using System.Text.Json.Serialization; // <- Usar outro pacote

namespace ProjetoAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddTransient<IDbConnection>((sp) => new MySqlConnection(Configuration.GetConnectionString("CategoriaConnection")));
            services.AddDbContext<AppDbContext>(opts => opts.UseLazyLoadingProxies().UseMySQL(Configuration.GetConnectionString("CategoriaConnection")));

            services.AddScoped<ICategoriaDao, CategoriaDao>();
            services.AddScoped<ISubcategoriaDao, SubcategoriaDao>();
            services.AddScoped<IProdutoDao, ProdutoDao>();
            services.AddScoped<ICentroDistribuicaoDao, CentroDistribuicaoDao>();
            services.AddScoped<ICarrinhoDeCompraDao, CarrinhoDeCompraDao>();
            services.AddScoped<IProdutoNoCarrinhoDao, ProdutoNoCarrinhoDao>();

            services.AddScoped<CarrinhoDeCompraService>();
            services.AddScoped<ProdutoService>();
            services.AddScoped<CentroDistribuicaoService>();
            services.AddScoped<ConsultaCepService>();
            services.AddScoped<CategoriaService>();
            services.AddScoped<SubcategoriaService>();

            services.AddControllers()
                .AddNewtonsoftJson(opts =>
                opts.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProjetoAPI", Version = "v1" });
            });
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProjetoAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseMiddleware(typeof(ErrorHandlingMiddleware)); // <- Novo

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
