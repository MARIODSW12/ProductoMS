using MediatR;

using Producto.Application.DTOs;

namespace Producto.Infrastructure.Queries
{
    public class GetProductoPorIdQuery : IRequest<ProductoDto>
    {
        public string IdProducto { get; set; }
        public GetProductoPorIdQuery(string idProducto)
        {
            IdProducto = idProducto;
        }
    }
}
