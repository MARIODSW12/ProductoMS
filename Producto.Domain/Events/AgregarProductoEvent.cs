using MediatR;

namespace Producto.Domain.Events
{
    public class AgregarProductoEvent : INotification
    {
        public string IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public string Categoria { get; set; }
        public decimal PrecioBase { get; set; }
        public string Descripcion { get; set; }
        public string ImagenProducto { get; set; }
        public string IdSubastador { get; set; }
        public int CantidadProducto { get; set; }

        public AgregarProductoEvent(string idProducto, string nombreProducto, string categoria, decimal precioBase, string descripcion, string imagenProducto, string idSubastador, 
            int cantidadProducto)
        {
            IdProducto = idProducto;
            NombreProducto = nombreProducto;
            Categoria = categoria;
            PrecioBase = precioBase;
            Descripcion = descripcion;
            ImagenProducto = imagenProducto;
            IdSubastador = idSubastador;
            CantidadProducto = cantidadProducto;
        }
    }
}
