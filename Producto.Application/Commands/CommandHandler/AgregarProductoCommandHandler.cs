using MediatR;
using Producto.Domain.Aggregate;
using Producto.Domain.Events;
using Producto.Domain.Interfaces;
using Producto.Domain.ValueObjects;
using System.Net;
using System.Xml.Linq;

namespace Producto.Application.Commands.CommandHandler
{
    public class AgregarProductoCommandHandler : IRequestHandler<AgregarProductoCommand, string>
    {
        private readonly IProductoRepository ProductoWriteRepository;
        private readonly IMediator Mediator;

        public AgregarProductoCommandHandler(IProductoRepository productoWriteRepository, IMediator mediator)
        {
            ProductoWriteRepository = productoWriteRepository ?? throw new ArgumentNullException(nameof(productoWriteRepository));
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<string> Handle(AgregarProductoCommand Producto, CancellationToken cancellationToken)
        {
            try
            {
                var producto = new Product(
                    new VOIdProducto( Guid.NewGuid().ToString()),
                    new VONombreProducto(Producto.Producto.NombreProducto),
                    new VOCategoria(Producto.Producto.Categoria),
                    new VOPrecioBase(Producto.Producto.PrecioBase),
                    new VODescripcion(Producto.Producto.Descripcion),
                    new VOImagenProducto(Producto.Producto.ImagenProducto),
                    new VOIdSubastador(Producto.Producto.IdSubastador),
                    new VOCantidad(Producto.Producto.CantidadProducto)
                );

                await ProductoWriteRepository.AgregarProducto(producto);

                // Publicar el evento de usuario creado
                var ProductoAgregadoEvento = new AgregarProductoEvent(
                    producto.IdProducto.IdProducto,
                    producto.NombreProducto.NombreProducto, 
                    producto.Categoria.CategoriaProducto, 
                    producto.PrecioBase.PrecioBaseProducto, 
                    producto.Descripcion.DescripcionProducto, 
                    producto.ImagenProducto.ImagenProducto, 
                    producto.IdSubastador.IdSubastador, 
                    producto.CantidadProducto.CantidadProducto
                );
                await Mediator.Publish(ProductoAgregadoEvento);

                return producto.IdProducto.IdProducto;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
