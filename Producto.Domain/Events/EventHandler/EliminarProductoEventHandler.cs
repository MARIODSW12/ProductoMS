using MassTransit;
using MediatR;

namespace Producto.Domain.Events.EventHandler
{
    public class EliminarProductoEventHandler
    {
        private readonly ISendEndpointProvider PublishEndpoint;

        public EliminarProductoEventHandler(ISendEndpointProvider publishEndpoint)
        {
            PublishEndpoint = publishEndpoint;
        }

        public async Task Handle(EliminarProductoEvent ProductoEliminadoEvento, CancellationToken cancellationToken)
        {
            try
            {
                await PublishEndpoint.Send(ProductoEliminadoEvento, cancellationToken);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
