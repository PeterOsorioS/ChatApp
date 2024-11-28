using Chat.Datos.Data.Repositorios.IRepositorios;
using Chat.Entidades.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Datos.Data.Repositorios
{
    public class ChatRoomRepositorio : Repositorio<ChatRoom>, IChatRoomRepositorio
    {
        private readonly ApplicationDbContext _db;
        public ChatRoomRepositorio(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
    }
}
