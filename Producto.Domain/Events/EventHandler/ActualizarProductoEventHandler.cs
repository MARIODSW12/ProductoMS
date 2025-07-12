using MediatR;
using MassTransit;

namespace Producto.Domain.Events.EventHandler
{
    public class ActualizarProductoEventHandler : INotificationHandler<ActualizarProductoEvent>
    {
        private readonly ISendEndpointProvider SendEndpointProvider;

        public ActualizarProductoEventHandler(ISendEndpointProvider sendEndpointProvider)
        {
            SendEndpointProvider = sendEndpointProvider;
        }

        public async Task Handle(ActualizarProductoEvent actualizarProductoEvent, CancellationToken cancellationToken)
        {
            try
            {
                await SendEndpointProvider.Send(actualizarProductoEvent, cancellationToken);
            }
            catch (Exception e)
            {
                throw;
            }

        }
    }
}