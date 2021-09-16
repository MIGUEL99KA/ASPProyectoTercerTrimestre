using System;
using System.ComponentModel.DataAnnotations;

namespace ASPProyectoTercerTrimestre.Models
{
    public class Cuenta
    {
        [Required]
        [StringLength(10)]
        public String NombreCliente { get; set; }

        [Required]
        public String DocumentoCliente { get; set; }


        [Required]
        public String NombreProducto { get; set; }


        [Required]
        public int PrecioProducto { get; set; }


        [Required]
        public int CantidadProducto { get; set; }


    }
}