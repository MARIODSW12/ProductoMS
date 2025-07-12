using MediatR;

using Producto.Application.DTOs;

namespace Producto.Infrastructure.Queries
{
    public class GetProductosPorIdSubastadorQuery : IRequest<List<ProductoDto>>
    {
        public string IdSubastador { get; set; }
        public GetProductosPorIdSubastadorQuery(string idSubastador)
        {
            IdSubastador = idSubastador;
        }
    }
}
