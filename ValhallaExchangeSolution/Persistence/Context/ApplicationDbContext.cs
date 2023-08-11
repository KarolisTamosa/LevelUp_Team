using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Context
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opciones) : base(opciones) { }
        public DbSet<Moneda> Monedas { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<HistorialPorUsuario> HistorialPorUsuarios { get; set; }



    }

}
