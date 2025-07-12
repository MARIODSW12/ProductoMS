using Producto.Domain.Exceptions;

namespace Producto.Domain.ValueObjects
{
    public class VOCantidad
    {
        public int CantidadProducto { get; private set; }

        public VOCantidad(int cantidadProducto)
        {
            if (cantidadProducto <= 0)
                throw new CantidadInvalidaException();

            CantidadProducto = cantidadProducto;
        }
    }
}
