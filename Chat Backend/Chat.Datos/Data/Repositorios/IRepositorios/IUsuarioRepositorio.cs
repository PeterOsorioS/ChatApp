using Chat.Entidades.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Datos.Data.Repositorios.IRepositorios
{
    public interface IUsuarioRepositorio: IRepositorio<Usuario>
    {
        void Update(Usuario usuario);
        Usuario GetByEmail(string correo);
        Task<Usuario> GetByEmailAsync(string correo);
    }
}
