using Chat.Entidades.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Datos.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {          
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<ChatMensajes> ChatMensajes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Correo)
                .IsUnique();

            modelBuilder.Entity<Usuario>()
               .HasIndex(u => u.NombreUsuario)
               .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
