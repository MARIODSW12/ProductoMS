using MongoDB.Bson;
using MongoDB.Driver;

using Producto.Domain.Aggregate;
using Producto.Domain.Exceptions;

using Producto.Infrastructure.Configurations;
using Producto.Infrastructure.Interfaces;
using Producto.Infrastructure.Persistences.Repositories.MongoRead.Documents;

namespace Producto.Infrastructure.Persistences.Repositories.MongoRead
{
    public class ProductoReadRepository: IProductoReadRepository
    {
        private readonly IMongoCollection<BsonDocument> ProductoColexion;

        public ProductoReadRepository(MongoReadProductoDbConfig mongoConfig)
        {
            ProductoColexion = mongoConfig.db.GetCollection<BsonDocument>("productos_read");
        }

        #region GetTodosLosProductos()
        public async Task<List<BsonDocument>> GetTodosLosProductos()
        {
            try
            {
                var productos = await ProductoColexion.Find(_ => true).ToListAsync();

                if (productos == null || !productos.Any())
                {
                    return new List<BsonDocument>();
                }

                return productos;
            }
            catch (MongoConnectionException ex)
            {
                throw new ErrorConexionBd();
            }
            catch (MongoCommandException ex)
            {
                throw new ErrorGuardadoBd();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region GetProductosPorIdSubastador(string idSubastador)
        public async Task<List<BsonDocument>> GetProductosPorIdSubastador(string idSubastador)
        {
            try
            {
                var filtroIdSubastador = Builders<BsonDocument>.Filter.Eq("idSubastador", idSubastador);
                var productos = await ProductoColexion.Find(filtroIdSubastador).ToListAsync();

                if (productos == null)
                {
                    return new List<BsonDocument>();
                }

                return productos;
            }
            catch (MongoConnectionException ex)
            {
                throw new ErrorConexionBd();
            }
            catch (MongoCommandException ex)
            {
                throw new ErrorGuardadoBd();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region GetProductosPorCategoria(string categoria)
        public async Task<List<BsonDocument>> GetProductosPorCategoria(string categoria)
        {
            try
            {
                var filtroCategoria = Builders<BsonDocument>.Filter.Eq("categoria", categoria);
                var productos = await ProductoColexion.Find(filtroCategoria).ToListAsync();
                if (productos == null)
                {
                    return new List<BsonDocument>();
                }
                return productos;
            }
            catch (MongoConnectionException ex)
            {
                throw new ErrorConexionBd();
            }
            catch (MongoCommandException ex)
            {
                throw new ErrorGuardadoBd();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region GetProductoPorId(string idProducto)
        public async Task<BsonDocument> GetProductoPorId(string idProducto)
        {
            try
            {
                var filtroIdProducto = Builders<BsonDocument>.Filter.Eq("_id", idProducto);

                var producto = await ProductoColexion.Find(filtroIdProducto).FirstOrDefaultAsync();

                if (producto == null)
                {
                    throw new ProductoNoEncontradoException();
                }
                return producto;
            }
            catch (MongoConnectionException ex)
            {
                throw new ErrorConexionBd();
            }
            catch (MongoCommandException ex)
            {
                throw new ErrorGuardadoBd();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region AgregarProducto(BsonDocument producto)
        public async Task AgregarProducto(BsonDocument producto)
        {
            try
            {
                await ProductoColexion.InsertOneAsync(producto);
            }
            catch (MongoConnectionException ex)
            {
                throw new ErrorConexionBd();
            }
            catch (MongoCommandException ex)
            {
                throw new ErrorGuardadoBd();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region ActualizarProducto(BsonDocument producto)
        public async Task ActualizarProducto(BsonDocument producto)
        {
            try
            {
                var idProductoActualizar = Builders<BsonDocument>.Filter.Eq("_id", (producto["_id"]).AsString);

                var ProductoActualizar = Builders<BsonDocument>.Update
                    .Set("nombre", producto["nombre"].AsString)
                    .Set("categoria", producto["categoria"].AsString)
                    .Set("precioBase", producto["precioBase"].AsDecimal)
                    .Set("descripcion", producto["descripcion"].AsString)
                    .Set("imagen", producto["imagen"].AsString)
                    .Set("cantidad", producto["cantidad"].AsInt32)
                    .Set("fechaActualizacion", DateTime.UtcNow);

                var productoModificado = await ProductoColexion.UpdateOneAsync(idProductoActualizar, ProductoActualizar);

                if (productoModificado.ModifiedCount == 0)
                {
                    throw new ProductoNoModificadoException();
                }
            }
            catch (MongoConnectionException ex)
            {
                throw new ErrorConexionBd();
            }
            catch (MongoCommandException ex)
            {
                throw new ErrorGuardadoBd();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region EliminarProducto(string idProducto)
        public async Task EliminarProducto(string idProducto)
        {
            try
            {
                var filtroIdProducto = Builders<BsonDocument>.Filter.Eq("_id", idProducto);

                var productoEliminado = await ProductoColexion.DeleteOneAsync(filtroIdProducto);

                if (productoEliminado.DeletedCount == 0)
                {
                    throw new ProductoNoEliminadoException();
                }
            }
            catch (MongoConnectionException ex)
            {
                throw new ErrorConexionBd();
            }
            catch (MongoCommandException ex)
            {
                throw new ErrorGuardadoBd();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
    }
}
