using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Entidades.Modelos
{
    public class ChatRoom
    {
        public int Id { get; set; }
        public string IdentificadorChat { get; set; }
        public int UsuarioId1 { get; set; }
        public int UsuarioId2 { get; set; }
    }
}
