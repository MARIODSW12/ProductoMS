
namespace Producto.Domain.ValueObjects
{
    public class VOCategoria
    {
        public string CategoriaProducto { get; private set; }
        public VOCategoria(string categoriaProducto)
        {
            if (string.IsNullOrWhiteSpace(categoriaProducto))
                throw new ArgumentException("El nombre del producto no puede estar vacío.");

            if (categoriaProducto.Length < 3 || categoriaProducto.Length > 100)
                throw new ArgumentException("El nombre del producto debe tener entre 3 y 100 caracteres.");

            CategoriaProducto = categoriaProducto;
        }
    }
}
