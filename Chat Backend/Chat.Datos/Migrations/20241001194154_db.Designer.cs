﻿// <auto-generated />
using Chat.Datos.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Chat.Datos.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241001194154_db")]
    partial class db
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Chat.Entidades.Modelos.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Clave")
                        .HasColumnType("text");

                    b.Property<string>("Correo")
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .HasColumnType("text");

                    b.Property<string>("NombreUsuario")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Correo")
                        .IsUnique();

                    b.HasIndex("NombreUsuario")
                        .IsUnique();

                    b.ToTable("Usuarios");
                });
#pragma warning restore 612, 618
        }
    }
}
