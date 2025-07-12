using MediatR;
using Producto.Domain.ValueObjects;

namespace Producto.Domain.Events
{
    public class ActualizarProductoEvent : INotification
    {
        public VOIdProducto IdProducto { get; set; }
        public VONombreProducto NombreProducto { get; set; }
        public VOCategoria Categoria { get; set; }
        public VOPrecioBase PrecioBase { get; set; }
        public VODescripcion Descripcion { get; set; }
        public VOImagenProducto ImagenProducto { get; set; }
        public VOCantidad CantidadProducto { get; set; }

           public ActualizarProductoEvent(VOIdProducto idProducto, VONombreProducto nombreProducto, VOCategoria categoria, VOPrecioBase precioBase,
               VODescripcion descripcion, VOImagenProducto imagenProducto, VOCantidad cantidadProducto)
            {
                IdProducto = idProducto;
                NombreProducto = nombreProducto;
                Categoria = categoria;
                PrecioBase = precioBase;
                Descripcion = descripcion;
                ImagenProducto = imagenProducto;
                CantidadProducto = cantidadProducto;
            }
    }
}
