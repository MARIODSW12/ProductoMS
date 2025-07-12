using MassTransit;
using MongoDB.Bson;
using Producto.Domain.Events;
using Producto.Infrastructure.Interfaces;

namespace Producto.Infrastructure.Consumers
{
    public class AgregarProductoConsumer(
        IServiceProvider serviceProvider,
        IProductoReadRepository productoReadRepository) : IConsumer<AgregarProductoEvent>
    {
        private readonly IServiceProvider ServiceProvider = serviceProvider;
        private readonly IProductoReadRepository ProductoReadRepository = productoReadRepository;

        public async Task Consume(ConsumeContext<AgregarProductoEvent> context)
        {
            try
            {
                var producto = new BsonDocument
                {
                    { "_id", context.Message.IdProducto },
                    { "nombre", context.Message.NombreProducto },
                    { "categoria", context.Message.Categoria },
                    { "precioBase", context.Message.PrecioBase },
                    { "descripcion", context.Message.Descripcion },
                    { "imagen", context.Message.ImagenProducto },
                    { "idSubastador", context.Message.IdSubastador },
                    { "cantidad", context.Message.CantidadProducto },
                    { "fechaCreacion", DateTime.UtcNow },
                    { "fechaActualizacion", DateTime.UtcNow }
                };
                await ProductoReadRepository.AgregarProducto(producto);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
