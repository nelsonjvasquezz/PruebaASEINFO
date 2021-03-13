using System;
using System.Collections.Generic;

#nullable disable

namespace PruebaBackendASEINFO.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            Productos = new HashSet<Producto>();
        }

        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public bool? Habilitado { get; set; }
        public DateTime FechaModifica { get; set; }
        public string IpModifica { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
