using MediatR;

using Producto.Application.DTOs;

namespace Producto.Infrastructure.Queries
{
    public class GetTodosLosProductosQuery: IRequest<List<ProductoDto>>
    {
    }
}
