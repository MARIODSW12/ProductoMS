
namespace Producto.Domain.ValueObjects
{
    public class VONombreProducto
    {
        public string NombreProducto { get; private set; }
        public VONombreProducto(string nombreProducto)
        {
            if (string.IsNullOrWhiteSpace(nombreProducto))
                throw new ArgumentException("El nombre del producto no puede estar vacío.");

            if (nombreProducto.Length < 3 || nombreProducto.Length > 100)
                throw new ArgumentException("El nombre del producto debe tener entre 3 y 100 caracteres.");

            NombreProducto = nombreProducto;
        }
    }
}
