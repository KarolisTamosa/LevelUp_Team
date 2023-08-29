using AutoMapper;
using Domain.IRepositories;
using Domain.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Persistence.Context;
using Persistence.Repositories;
using Services;
using System.Text;

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

            // A�adir AutoMapper
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

            // Add Authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)//que tipo de esquema utilizamos
                    .AddJwtBearer(options =>
                        options.TokenValidationParameters = new TokenValidationParameters//que cosas quereoms validar cada vez que ingrese el token
                        {
                            ValidateIssuer = true,//que valide el emisoe
                            ValidateAudience = true,//valide audiencia
                            ValidateLifetime = true,//que el token no haya expirado (que no pase el timepo que asignamos al crear token)
                            ValidateIssuerSigningKey = true,//que valide la security key (del appsetings.json)
                            ValidIssuer = builder.Configuration["Jwt:Issuer"],//pasando parametros (los que configuramos en el appsettings.json)
                            ValidAudience = builder.Configuration["Jwt:Audience"],//seteamos la audiencia valida (backend valido) para checkear que sea correcta
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])),//seteamos la secretkey para que la valide a partir de ella
                            ClockSkew = TimeSpan.Zero
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

            app.UseAuthentication();//

            app.MapControllers();

            app.Run();
        }
    }
}
