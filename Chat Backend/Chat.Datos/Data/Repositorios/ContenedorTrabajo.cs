using Chat.Datos.Data.Repositorios.IRepositorios;
using Chat.Datos.Data.Repository;
using Chat.Entidades.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Datos.Data.Repositorios
{
    public class ContenedorTrabajo: IContenedorTrabajo
    {
        private readonly ApplicationDbContext _db;
        public ContenedorTrabajo(ApplicationDbContext db)
        {
            _db = db;
            Usuario = new UsuarioRepositorio(_db);
            ChatRoom = new ChatRoomRepositorio(_db);
            ChatMensaje = new ChatMensajeRepositorio(_db);
            
        }
        public IUsuarioRepositorio Usuario {  get; private set; }
        public IChatRoomRepositorio ChatRoom { get; private set; }
        public IChatMensajeRepositorio ChatMensaje { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
