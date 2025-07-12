
namespace Producto.Domain.ValueObjects
{
    public class VOIdProducto
    {
        public string IdProducto { get; private set; }
        public VOIdProducto (string idProducto)
        {
            if (string.IsNullOrWhiteSpace(idProducto))
                throw new ArgumentException("El ID del producto no puede estar vacío.");

            if (!Guid.TryParse(idProducto, out _))
                throw new ArgumentException("El ID del producto debe ser un GUID válido.");

            IdProducto = idProducto;
        }
    }
}
