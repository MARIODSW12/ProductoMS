using MediatR;

using Producto.Application.DTOs;

using Producto.Infrastructure.Interfaces;

namespace Producto.Infrastructure.Queries.QueryHandler
{
    public class GetProductosPorCategoriaQueryHandler: IRequestHandler<GetProductosPorCategoriaQuery, List<ProductoDto>>
    {
        private readonly IProductoReadRepository ProductoRepository;

        public GetProductosPorCategoriaQueryHandler(IProductoReadRepository productoRepository)
        {
            ProductoRepository = productoRepository;
        }

        public async Task<List<ProductoDto>> Handle(GetProductosPorCategoriaQuery productoCategoria, CancellationToken cancellationToken)
        {
            try
            {
                var productos = await ProductoRepository.GetProductosPorCategoria(productoCategoria.Categoria);

                if (productos == null)
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
