using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.DTOs.Modelos
{
    public class ActualizarUsuarioDTO
    {
        public string Nombre {  get; set; }
        public string ClaveActual { get; set; }
        public string ClaveNueva { get; set; }
    }
}
