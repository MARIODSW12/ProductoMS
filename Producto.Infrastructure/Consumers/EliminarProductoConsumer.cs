using MassTransit;
using MongoDB.Bson;
using Producto.Domain.Events;
using Producto.Infrastructure.Interfaces;

namespace Producto.Infrastructure.Consumers
{
    public class EliminarProductoConsumer(
        IServiceProvider serviceProvider,
        IProductoReadRepository productoReadRepository) : IConsumer<EliminarProductoEvent>
    {
        private readonly IServiceProvider ServiceProvider = serviceProvider;
        private readonly IProductoReadRepository ProductoReadRepository = productoReadRepository;
        public async Task Consume(ConsumeContext<EliminarProductoEvent> context)
        {
            try
            {
                var idProducto = context.Message.IdProducto;
                await ProductoReadRepository.EliminarProducto(idProducto);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
