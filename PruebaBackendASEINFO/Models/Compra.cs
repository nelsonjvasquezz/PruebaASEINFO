using System;
using System.Collections.Generic;

#nullable disable

namespace PruebaBackendASEINFO.Models
{
    public partial class Compra
    {
        public int IdCompra { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal TotalPagar { get; set; }
        public bool? Habilitado { get; set; }
        public DateTime FechaModifica { get; set; }
        public string IpModifica { get; set; }

        public virtual Producto IdProductoNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
