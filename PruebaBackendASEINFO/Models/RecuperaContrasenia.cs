using System;
using System.Collections.Generic;

#nullable disable

namespace PruebaBackendASEINFO.Models
{
    public partial class RecuperaContrasenia
    {
        public int IdRecupera { get; set; }
        public string Token { get; set; }
        public int IdUsuario { get; set; }
        public bool? Habilitado { get; set; }
        public DateTime FechaModifica { get; set; }
        public string IpModifica { get; set; }

        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
