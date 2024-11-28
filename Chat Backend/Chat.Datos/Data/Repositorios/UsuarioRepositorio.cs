using Chat.Datos.Data.Repositorios;
using Chat.Datos.Data.Repositorios.IRepositorios;
using Chat.Entidades.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Datos.Data.Repository
{
    public class UsuarioRepositorio : Repositorio<Usuario>, IUsuarioRepositorio
    {
        private readonly ApplicationDbContext _db;
        public UsuarioRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Usuario usuario)
        {
            var objDesdeDb = _db.Usuarios.FirstOrDefault(u => u.Id == usuario.Id);
            objDesdeDb.Nombre = usuario.Nombre;
            objDesdeDb.Clave = usuario.Clave;
        }
        public Usuario GetByEmail(string correo)
        {
            var objDesdeDb = _db.Usuarios.AsNoTracking().FirstOrDefault(u => u.Correo == correo);
            return objDesdeDb;
        }
        public async Task<Usuario> GetByEmailAsync(string correo)
        {
            var objDesdeDb = await _db.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.Correo == correo);
            return objDesdeDb;
        }
    }
}
