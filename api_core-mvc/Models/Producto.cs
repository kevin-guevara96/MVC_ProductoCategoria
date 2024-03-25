using System.ComponentModel.DataAnnotations.Schema;

namespace api_core_mvc.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string CodigoBarra { get; set; }
        public string Descripcion { get; set; }
        public string Marca { get; set; }
        public int IdCategoria { get; set; }
        public decimal Precio { get; set; }

        public virtual Categoria Categoria { get; set; }
    }
}
