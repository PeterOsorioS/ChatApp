using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Entidades.Modelos
{
    public class ChatMensajes
    {
        public int Id {  get; set; }
        public int ChatSalaId { get; set; }
        public string Mensajes { get; set; }
        public int RemitenteID { get; set; }
        [ForeignKey("ChatSalaId")]
        public ChatRoom ChatRoom { get; set; }
    }
}
