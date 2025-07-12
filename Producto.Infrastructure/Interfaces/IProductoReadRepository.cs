using MongoDB.Bson;
using Producto.Domain.Aggregate;

namespace Producto.Infrastructure.Interfaces
{
    public interface IProductoReadRepository
    {
        Task<List<BsonDocument>> GetTodosLosProductos();
        Task<List<BsonDocument>> GetProductosPorIdSubastador(string idSubastador);
        Task<List<BsonDocument>> GetProductosPorCategoria(string categoria);
        Task<BsonDocument> GetProductoPorId(string idProducto);
        Task AgregarProducto(BsonDocument producto);
        Task ActualizarProducto(BsonDocument producto);
        Task EliminarProducto(string idProducto);
    }
}
