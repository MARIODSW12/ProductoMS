using MediatR;
using MassTransit;
using Producto.Domain.Events;
using Producto.Domain.Interfaces;

namespace Producto.Application.Commands.CommandHandler
{
    public class ActualizarProductoCommandHandler : IRequestHandler<ActualizarProductoCommand, bool>
    {
        private readonly IPublishEndpoint PublishEndpoint;
        private readonly IProductoRepository ProductoRepository;
        public ActualizarProductoCommandHandler(IPublishEndpoint publishEndpoint, IProductoRepository productoRepository)
        {
            PublishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
            ProductoRepository = productoRepository ?? throw new ArgumentNullException(nameof(productoRepository));
        }
        public async Task<bool> Handle(ActualizarProductoCommand ProductoActualizar, CancellationToken cancellationToken)
        {
            try
            {
                var producto = await ProductoRepository.ObtenerProductoPorId(ProductoActualizar.IdProducto);
                if (producto == null)
                {
                    throw new ArgumentNullException(nameof(ProductoActualizar));
                }
                
                producto.ActualizarProducto(
                    (ProductoActualizar.ProductDto.NombreProducto),
                    (ProductoActualizar.ProductDto.Categoria),
                    (ProductoActualizar.ProductDto.PrecioBase),
                    (ProductoActualizar.ProductDto.Descripcion),
                    (ProductoActualizar.ProductDto.ImagenProducto),
                    (ProductoActualizar.ProductDto.CantidadProducto)
                    );

                await ProductoRepository.ActualizarProducto(producto);

                var productoActualizado = new ActualizarProductoEvent(producto.IdProducto, producto.NombreProducto, producto.Categoria, producto.PrecioBase,
                    producto.Descripcion, producto.ImagenProducto, producto.CantidadProducto);
                await PublishEndpoint.Publish(productoActualizado);

                return true; // Retornar true si la actualización fue exitosa
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
