using Producto.Domain.Aggregate;

namespace Producto.Domain.Interfaces
{
    public interface IProductoRepository
    {
        Task AgregarProducto (Product producto);
        Task ActualizarProducto (Product producto);
        Task EliminarProducto (string idProducto);
        Task<Product?> ObtenerProductoPorId(string idProducto);
    }
}
