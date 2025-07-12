using MediatR;

using Producto.Application.DTOs;

using Producto.Infrastructure.Interfaces;

namespace Producto.Infrastructure.Queries.QueryHandler
{
    public class GetProductoPorIdQueryHandler : IRequestHandler<GetProductoPorIdQuery, ProductoDto>
    {
        private readonly IProductoReadRepository ProductoRepository;

        public GetProductoPorIdQueryHandler(IProductoReadRepository productoRepository)
        {
            ProductoRepository = productoRepository;
        }

        public async Task<ProductoDto> Handle(GetProductoPorIdQuery idProducto, CancellationToken cancellationToken)
        {
            try
            {
                var producto = await ProductoRepository.GetProductoPorId(idProducto.IdProducto);

                if (producto == null)
                {
                    return null;
                }

                var productoPorId = new ProductoDto
                {
                    IdProducto = producto["_id"].AsString,
                    NombreProducto = producto["nombre"].AsString,
                    Categoria = producto["categoria"].AsString,
                    PrecioBase = producto["precioBase"].AsDecimal,
                    Descripcion = producto["descripcion"].AsString,
                    ImagenProducto = producto["imagen"].AsString,
                    IdSubastador = producto["idSubastador"].AsString,
                    CantidadProducto = producto["cantidad"].AsInt32
                };

                return productoPorId;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
