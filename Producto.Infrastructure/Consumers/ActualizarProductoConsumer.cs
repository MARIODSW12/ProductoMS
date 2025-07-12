using MassTransit;
using MongoDB.Bson;
using Producto.Domain.Events;
using Producto.Infrastructure.Interfaces;
using Producto.Infrastructure.Persistences.Repositories.MongoRead;

namespace Producto.Infrastructure.Consumers
{
    public class ActualizarProductoConsumer(IServiceProvider serviceProvider, IProductoReadRepository productoReadRepository) : IConsumer<ActualizarProductoEvent>
    {
        private readonly IServiceProvider ServiceProvider = serviceProvider;
        private readonly IProductoReadRepository ProductoReadRepository = productoReadRepository;

        public async Task Consume(ConsumeContext<ActualizarProductoEvent> context)
        {
            try
            {
                var actualizarProductoEvent = context.Message;

                var ProductoActualizado = new BsonDocument
                {
                    { "_id", actualizarProductoEvent.IdProducto.IdProducto },
                    { "nombre", actualizarProductoEvent.NombreProducto.NombreProducto },
                    { "categoria", actualizarProductoEvent.Categoria.CategoriaProducto },
                    { "precioBase", actualizarProductoEvent.PrecioBase.PrecioBaseProducto },
                    { "descripcion", actualizarProductoEvent.Descripcion.DescripcionProducto },
                    { "imagen", actualizarProductoEvent.ImagenProducto.ImagenProducto },
                    { "cantidad", actualizarProductoEvent.CantidadProducto.CantidadProducto }
                };

                await ProductoReadRepository.ActualizarProducto(ProductoActualizado);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al procesar el evento ActualizarProducto: {ex.Message}", ex);
            }
            
        }
    }
}
