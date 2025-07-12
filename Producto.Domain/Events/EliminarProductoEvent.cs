using MediatR;

namespace Producto.Domain.Events
{
    public class EliminarProductoEvent
    {
        public string IdProducto { get; set; }

        public EliminarProductoEvent(string idProducto)
        {
            IdProducto = idProducto;
        }
    }
}
