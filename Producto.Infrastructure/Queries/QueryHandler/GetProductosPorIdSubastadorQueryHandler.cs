using MediatR;

using Producto.Application.DTOs;

using Producto.Infrastructure.Interfaces;

namespace Producto.Infrastructure.Queries.QueryHandler
{
    public class GetProductosPorIdSubastadorQueryHandler : IRequestHandler<GetProductosPorIdSubastadorQuery, List<ProductoDto>>
    {
        private readonly IProductoReadRepository ProductoRepository;

        public GetProductosPorIdSubastadorQueryHandler(IProductoReadRepository productoRepository)
        {
            ProductoRepository = productoRepository;
        }

        public async Task<List<ProductoDto>> Handle(GetProductosPorIdSubastadorQuery productoIdSubastador, CancellationToken cancellationToken)
        {
            try
            {
                var productos = await ProductoRepository.GetProductosPorIdSubastador(productoIdSubastador.IdSubastador);

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
