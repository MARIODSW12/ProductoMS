using MediatR;

using Producto.Application.DTOs;

namespace Producto.Infrastructure.Queries
{
    public class GetProductosPorCategoriaQuery : IRequest<List<ProductoDto>>
    {
        public string Categoria { get; set; }
        public GetProductosPorCategoriaQuery(string categoria)
        {
            Categoria = categoria;
        }
    }
}
