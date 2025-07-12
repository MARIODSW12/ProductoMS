using MassTransit;
using MediatR;
using Producto.Domain.Events;
using Producto.Domain.Interfaces;


namespace Producto.Application.Commands.CommandHandler
{
    public class EliminarProductoCommandHandler : IRequestHandler<EliminarProductoCommand, bool>
    {
        private readonly IPublishEndpoint PublishEndpoint;
        private readonly IProductoRepository ProductoRepository;

        public EliminarProductoCommandHandler(IPublishEndpoint publishEndpoint, IProductoRepository productoRepository)
        {
            PublishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
            ProductoRepository = productoRepository ?? throw new ArgumentNullException(nameof(productoRepository));
        }

        public async Task<bool> Handle(EliminarProductoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var producto = await ProductoRepository.ObtenerProductoPorId(request.IdProducto);

                if (producto == null)
                {
                    throw new ArgumentNullException(nameof(request.IdProducto));
                }

                await ProductoRepository.EliminarProducto(producto.IdProducto.IdProducto);

                // Publicar evento de eliminación de producto
                var productoEliminado = new EliminarProductoEvent(producto.IdProducto.IdProducto);

                await PublishEndpoint.Publish(productoEliminado);

                return true; // Retornar true si la eliminación fue exitosa
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
