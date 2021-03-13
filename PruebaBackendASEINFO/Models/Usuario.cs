using System;
using System.Collections.Generic;

#nullable disable

namespace PruebaBackendASEINFO.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Clientes = new HashSet<Cliente>();
            Compras = new HashSet<Compra>();
            Logins = new HashSet<Login>();
            RecuperaContrasenia = new HashSet<RecuperaContrasenia>();
        }

        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Correo { get; set; }
        public string Contrasenia { get; set; }
        public string Salt { get; set; }
        public string Imagen { get; set; }
        public int? IdRol { get; set; }
        public bool? Habilitado { get; set; }
        public DateTime FechaModifica { get; set; }
        public string IpModifica { get; set; }

        public virtual Rol IdRolNavigation { get; set; }
        public virtual ICollection<Cliente> Clientes { get; set; }
        public virtual ICollection<Compra> Compras { get; set; }
        public virtual ICollection<Login> Logins { get; set; }
        public virtual ICollection<RecuperaContrasenia> RecuperaContrasenia { get; set; }
    }
}
