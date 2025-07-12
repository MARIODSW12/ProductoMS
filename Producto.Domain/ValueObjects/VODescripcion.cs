
namespace Producto.Domain.ValueObjects
{
    public class VODescripcion
    {
        public string DescripcionProducto { get; private set; }
        public VODescripcion(string descripcionProducto)
        {
            /*if (string.IsNullOrWhiteSpace(descripcionProducto))
                throw new ArgumentException("El nombre del producto no puede estar vacío.");*/

            if (descripcionProducto.Length < 3 || descripcionProducto.Length > 100)
                throw new ArgumentException("La descripción de un producto debe tener entre 3 y 100 caracteres.");

            DescripcionProducto = descripcionProducto;
        }
    }
}
