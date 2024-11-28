using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Datos.Data.Repositorios.IRepositorios
{
    public interface IContenedorTrabajo : IDisposable
    {
        IUsuarioRepositorio Usuario {get;}
        IChatRoomRepositorio ChatRoom {get;}
        IChatMensajeRepositorio ChatMensaje {get;}

        void Save();
        Task SaveAsync();
    }
}
