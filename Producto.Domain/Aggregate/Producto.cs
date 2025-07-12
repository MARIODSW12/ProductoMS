using Producto.Domain.ValueObjects;
using Producto.Domain.Exceptions;

namespace Producto.Domain.Aggregate
{
    public class Product
    {
        public VOIdProducto IdProducto { get; private set;}
        public VONombreProducto NombreProducto { get; private set;}
        public VOCategoria Categoria { get; private set;}
        public VOPrecioBase PrecioBase { get; private set;}
        public VODescripcion Descripcion { get; private set;}
        public VOImagenProducto ImagenProducto { get; private set; }
        public VOIdSubastador IdSubastador { get; private set; }
        public VOCantidad CantidadProducto { get; private set; }

        public Product (VOIdProducto idProducto, VONombreProducto nombreProducto, VOCategoria categoria, VOPrecioBase precioBase, 
            VODescripcion descripcion, VOImagenProducto imagenProducto, VOIdSubastador idSubastador, VOCantidad cantidad)
        {
            IdProducto = idProducto;
            NombreProducto = nombreProducto;
            Categoria = categoria;
            PrecioBase = precioBase;
            Descripcion = descripcion;
            ImagenProducto = imagenProducto;
            IdSubastador = idSubastador;
            CantidadProducto = cantidad;
        }

        public void ActualizarProducto (string? nombreProducto, string? categoria, decimal? precioBase, string? descripcion, 
            string? imagenProducto, int? cantidad)
        {
            if (nombreProducto != null)
            {
                this.NombreProducto = new VONombreProducto(nombreProducto);
            }

            if (categoria != null)
            {
                this.Categoria = new VOCategoria(categoria);
            }

            if (precioBase != null)
            {
                this.PrecioBase = new VOPrecioBase(precioBase.GetValueOrDefault());
            }

            if (descripcion != null)
            {
                this.Descripcion = new VODescripcion(descripcion);
            }

            if (imagenProducto != null)
            {
                this.ImagenProducto = new VOImagenProducto(imagenProducto);
            }

            if (cantidad != null)
            {
                this.CantidadProducto = new VOCantidad(cantidad.Value);
            }
        }

        public void ActualizarCantidad(int cantidadProducto)
        {
            if (cantidadProducto <= 0)
                throw new CantidadInvalidaException();

            CantidadProducto = new VOCantidad(cantidadProducto);
        }
    }
}
