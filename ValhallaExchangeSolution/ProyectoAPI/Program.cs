using Domain.IRepositories;
using Domain.IServices;
using Microsoft.EntityFrameworkCore;
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



            // Add services to the container.



            //Contexto de base de datos
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=PruebaBD3;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
            );



            //Servicios
            builder.Services.AddScoped<IMonedaService, MonedaService>();
            builder.Services.AddScoped<IApiMonedasService, ApiMonedasService>();
            builder.Services.AddScoped<IHistorialService, HistorialService>();
            builder.Services.AddScoped<IUsuarioService, UsuarioService>();
            builder.Services.AddScoped<IPaisService, PaisService>();
            //Repositorios
            builder.Services.AddScoped<IMonedaRepository, MonedaRepository>();
            builder.Services.AddScoped<IHistorialRepository, HistorialRepository>();
            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddScoped<IPaisRepository, PaisRepository>();





            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();




            var app = builder.Build();



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }



            app.UseHttpsRedirection();



            app.UseAuthorization();





            app.MapControllers();



            app.Run();
        }
    }
}