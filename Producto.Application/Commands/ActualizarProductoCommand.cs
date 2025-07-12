using MediatR;
using Producto.Application.DTOs;

namespace Producto.Application.Commands
{
    public class ActualizarProductoCommand : IRequest<bool>
    {
        public string IdProducto { get; set; }
        public ActualizarProductoDto ProductDto { get; set; }
        public ActualizarProductoCommand(string idProducto, ActualizarProductoDto productoDto)
        {
            IdProducto = idProducto ?? throw new ArgumentNullException(nameof(idProducto));
            ProductDto = productoDto ?? throw new ArgumentNullException(nameof(productoDto));
        }
    }
}
