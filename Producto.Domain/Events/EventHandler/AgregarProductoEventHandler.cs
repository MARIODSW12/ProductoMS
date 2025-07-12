using MassTransit;
using MediatR;

namespace Producto.Domain.Events.EventHandler
{
    public class AgregarProductoEventHandler : INotificationHandler<AgregarProductoEvent>
    {
        private readonly ISendEndpointProvider PublishEndpoint;

        public AgregarProductoEventHandler(ISendEndpointProvider publishEndpoint)
        {
            PublishEndpoint = publishEndpoint;
        }

        public async Task Handle(AgregarProductoEvent ProductoAgregadoEvento, CancellationToken cancellationToken)
        {
            try
            {
                await PublishEndpoint.Send(ProductoAgregadoEvento, cancellationToken);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
