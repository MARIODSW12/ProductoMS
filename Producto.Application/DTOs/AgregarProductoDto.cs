
namespace Producto.Application.DTOs
{
    public class AgregarProductoDto
    {
        public string NombreProducto { get; set; }
        public string Categoria { get; set; }
        public decimal PrecioBase { get; set; }
        public string Descripcion { get; set; }
        public string ImagenProducto { get; set; }
        public string IdSubastador { get; set; }
        public int CantidadProducto { get; set; }
    }
}
