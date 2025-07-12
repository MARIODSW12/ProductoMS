using MediatR;
using Producto.Application.DTOs;

namespace Producto.Application.Commands
{
    public class EliminarProductoCommand : IRequest<bool>
    {
        public string IdProducto { get; set; }
        public EliminarProductoCommand(string idProducto)
        {
            IdProducto = idProducto ?? throw new ArgumentNullException(nameof(idProducto));
        }
    }
}
