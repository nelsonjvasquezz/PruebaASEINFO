using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace PruebaBackendASEINFO.Models
{
    public partial class Producto
    {
        public Producto()
        {
            Compras = new HashSet<Compra>();
        }

        public int IdProducto { get; set; }
        public string Nombre { get; set; }

        [MaxLength(300)]
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string Imagen { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public int? IdMarca { get; set; }
        public int? IdCategoria { get; set; }
        public int? IdTipo { get; set; }
        public bool? Habilitado { get; set; }
        public DateTime FechaModifica { get; set; }
        public string IpModifica { get; set; }

        public virtual Categoria IdCategoriaNavigation { get; set; }
        public virtual Marca IdMarcaNavigation { get; set; }
        public virtual Tipo IdTipoNavigation { get; set; }
        public virtual ICollection<Compra> Compras { get; set; }
    }
}
