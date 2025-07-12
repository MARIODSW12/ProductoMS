
namespace Producto.Application.DTOs
{
    public class ActualizarProductoDto
    {
        public string? NombreProducto { get; set; }
        public string? Categoria { get; set; }
        public decimal? PrecioBase { get; set; }
        public string? Descripcion { get; set; }
        public string? ImagenProducto { get; set; }
        public int? CantidadProducto { get; set; }
    }
}
