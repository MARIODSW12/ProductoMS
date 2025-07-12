using MediatR;
using Producto.Application.DTOs;

namespace Producto.Application.Commands
{
    public class AgregarProductoCommand : IRequest<String>
    {
        public AgregarProductoDto Producto { get; set; }
        public AgregarProductoCommand(AgregarProductoDto producto)
        {
            Producto = producto;
                //?? throw new ArgumentNullException(nameof(producto))
        }
    }
}
