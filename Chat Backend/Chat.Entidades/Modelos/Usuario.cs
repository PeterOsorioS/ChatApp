using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Entidades.Modelos
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }
        public string NombreUsuario { get; set; }

        public string Correo { get; set; }

        public string Clave { get; set; }
    }
}
