using System.Runtime.CompilerServices;
using MediatR;

using Producto.Application.DTOs;

using Producto.Infrastructure.Interfaces;

namespace Producto.Infrastructure.Queries.QueryHandler
{
    public class GetTodosLosProductosQueryHandler : IRequestHandler<GetTodosLosProductosQuery, List<ProductoDto>>
    {
        private readonly IProductoReadRepository ProductoRepository;

        public GetTodosLosProductosQueryHandler(IProductoReadRepository productoRepository)
        {
            ProductoRepository = productoRepository;
        }

        public async Task<List<ProductoDto>> Handle(GetTodosLosProductosQuery todosProductos, CancellationToken cancellationToken)
        {
            try
            {
                var productos = await ProductoRepository.GetTodosLosProductos();

                if (productos == null || !productos.Any())
                {
                    return new List<ProductoDto>();
                }

                var listaProductos = productos.Select(p => new ProductoDto
                {
                    IdProducto = p["_id"].AsString,
                    NombreProducto = p["nombre"].AsString,
                    Categoria = p["categoria"].AsString,
                    PrecioBase = p["precioBase"].AsDecimal,
                    Descripcion = p["descripcion"].AsString,
                    ImagenProducto = p["imagen"].AsString,
                    IdSubastador = p["idSubastador"].AsString,
                    CantidadProducto = p["cantidad"].AsInt32
                }).ToList();

                return listaProductos;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
