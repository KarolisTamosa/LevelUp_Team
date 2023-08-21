﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence.Context;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Models.HistorialPorUsuario", b =>
                {
                    b.Property<int>("IdHistorialPorUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdHistorialPorUsuario"), 1L, 1);

                    b.Property<int>("FactorCambio")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaConversion")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdMonedaDestino")
                        .HasColumnType("int");

                    b.Property<int>("IdMonedaOrigen")
                        .HasColumnType("int");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<double>("Importe")
                        .HasColumnType("float");

                    b.HasKey("IdHistorialPorUsuario");

                    b.HasIndex("IdMonedaDestino");

                    b.HasIndex("IdMonedaOrigen");

                    b.HasIndex("IdUsuario");

                    b.ToTable("HistorialPorUsuarios");
                });

            modelBuilder.Entity("Domain.Models.Moneda", b =>
                {
                    b.Property<int>("IdMoneda")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdMoneda"), 1L, 1);

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Eliminado")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ValorEnDolares")
                        .HasColumnType("float");

                    b.HasKey("IdMoneda");

                    b.ToTable("Monedas");
                });

            modelBuilder.Entity("Domain.Models.Pais", b =>
                {
                    b.Property<int>("IdPais")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPais"), 1L, 1);

                    b.Property<bool>("Eliminado")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdPais");

                    b.ToTable("Paises");
                });

            modelBuilder.Entity("Domain.Models.Usuario", b =>
                {
                    b.Property<int>("IdUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUsuario"), 1L, 1);

                    b.Property<bool>("Eliminado")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdPais")
                        .HasColumnType("int");

                    b.Property<string>("PasswordEncriptado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdUsuario");

                    b.HasIndex("IdPais");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Domain.Models.HistorialPorUsuario", b =>
                {
                    b.HasOne("Domain.Models.Moneda", "MonedaDestino")
                        .WithMany("HistorialesPorMonedaDestino")
                        .HasForeignKey("IdMonedaDestino")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Moneda", "MonedaOrigen")
                        .WithMany("HistorialesPorMonedaOrigen")
                        .HasForeignKey("IdMonedaOrigen")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Usuario", "Usuario")
                        .WithMany("HistorialesPorUsuario")
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MonedaDestino");

                    b.Navigation("MonedaOrigen");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Domain.Models.Usuario", b =>
                {
                    b.HasOne("Domain.Models.Pais", "Pais")
                        .WithMany("Usuarios")
                        .HasForeignKey("IdPais")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pais");
                });

            modelBuilder.Entity("Domain.Models.Moneda", b =>
                {
                    b.Navigation("HistorialesPorMonedaDestino");

                    b.Navigation("HistorialesPorMonedaOrigen");
                });

            modelBuilder.Entity("Domain.Models.Pais", b =>
                {
                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("Domain.Models.Usuario", b =>
                {
                    b.Navigation("HistorialesPorUsuario");
                });
#pragma warning restore 612, 618
        }
    }
}
