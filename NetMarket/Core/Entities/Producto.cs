using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Producto : ClaseBase
    {
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public int Stock { get; set; }

        public int MarcaId { get; set; }

        public Marca Marca { get; set; }

        public int CategoriaId { get; set; }

        public Categoria Categoria { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Precio { get; set; }

        public string Imagen { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}")]
        [DataType(DataType.DateTime)]
        public DateTime RegistrationDate { get; set; }
    }
}
