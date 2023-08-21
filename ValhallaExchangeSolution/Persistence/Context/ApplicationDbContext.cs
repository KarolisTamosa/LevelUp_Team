using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opciones) : base() { }
        public DbSet<Moneda> Monedas { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<HistorialPorUsuario> HistorialPorUsuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Configura la cadena de conexión y cambia la biblioteca de migraciones
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=PruebaBD3;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HistorialPorUsuario>()
                .HasOne(historial => historial.MonedaOrigen)
                .WithMany(moneda => moneda.HistorialesPorMonedaOrigen)
                .HasForeignKey(historial => historial.IdMonedaOrigen);

            modelBuilder.Entity<HistorialPorUsuario>()
                .HasOne(historial => historial.MonedaDestino)
                .WithMany(moneda => moneda.HistorialesPorMonedaDestino)
                .HasForeignKey(historial => historial.IdMonedaDestino);
        }
    }

}
