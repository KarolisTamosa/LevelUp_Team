using AutoMapper;
using Domain.IRepositories;
using Domain.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Persistence.Context;
using Persistence.Repositories;
using Services;

namespace ProyectoAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProyectoAPI", Version = "v1" });
            });

            // Contexto de base de datos
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=PruebaBD3;Trusted_Connection=True;MultipleActiveResultSets=true");
            });

            // Servicios
            builder.Services.AddScoped<IMonedaService, MonedaService>();
            builder.Services.AddScoped<IApiMonedasService, ApiMonedasService>();
            builder.Services.AddScoped<IHistorialService, HistorialService>();
            builder.Services.AddScoped<IUsuarioService, UsuarioService>();
            builder.Services.AddScoped<IPaisService, PaisService>();

            // Repositorios
            builder.Services.AddScoped<IMonedaRepository, MonedaRepository>();
            builder.Services.AddScoped<IHistorialRepository, HistorialRepository>();
            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddScoped<IPaisRepository, PaisRepository>();

            builder.Services.AddControllers().AddNewtonsoftJson(setupAction =>
            {
                setupAction.SerializerSettings.ContractResolver =
                    new CamelCasePropertyNamesContractResolver();
            });

            // Añadir AutoMapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Configurar CORS
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProyectoAPI v1");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors();

            app.MapControllers();

            app.Run();
        }
    }
}
