
namespace Producto.Domain.ValueObjects
{
    public class VOPrecioBase
    {
        public decimal PrecioBaseProducto { get; private set; }

        public VOPrecioBase(decimal precioBaseProducto)
        {
            if (precioBaseProducto <= 0)
                throw new ArgumentException("El precio debe ser mayor a cero.");

            PrecioBaseProducto = precioBaseProducto;
        }

    }
}
