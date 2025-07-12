using MongoDB.Bson;
using MongoDB.Driver;

using Producto.Domain.Aggregate;
using Producto.Domain.Interfaces;
using Producto.Domain.Exceptions;
using Producto.Domain.ValueObjects;
using Producto.Infrastructure.Configurations;

namespace Producto.Infrastructure.Persistences.Repositories.MongoWrite
{
    public class ProductoWriteRepository: IProductoRepository
    {
        private readonly IMongoCollection<BsonDocument> ProductoColexion;

        public ProductoWriteRepository(MongoWriteProductoDbConfig mongoConfig)
        {
            ProductoColexion = mongoConfig.db.GetCollection<BsonDocument>("productos_write");
        }

        #region AgregarProducto(Product producto)
        public async Task AgregarProducto(Product producto)
        {
            try
            {
                var bsonUser = new BsonDocument
                {
                    { "_id", producto.IdProducto.IdProducto},
                    { "nombre", producto.NombreProducto.NombreProducto},
                    { "categoria", producto.Categoria.CategoriaProducto},
                    { "precioBase", producto.PrecioBase.PrecioBaseProducto},
                    { "descripcion", producto.Descripcion.DescripcionProducto},
                    { "imagen", producto.ImagenProducto.ImagenProducto},
                    { "idSubastador", producto.IdSubastador.IdSubastador},
                    { "cantidad", producto.CantidadProducto.CantidadProducto},
                    { "fechaCreacion", DateTime.UtcNow },
                    { "fechaActualizacion", DateTime.UtcNow }
                };

                await ProductoColexion.InsertOneAsync(bsonUser);
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

        #region ActualizarProducto(Product producto)
        public async Task ActualizarProducto(Product producto)
        {
            try
            {
                var idProductoActualizar = Builders<BsonDocument>.Filter.Eq("_id", producto.IdProducto.IdProducto);

                var actualizar = Builders<BsonDocument>.Update
                    .Set("nombre", producto.NombreProducto.NombreProducto)
                    .Set("categoria", producto.Categoria.CategoriaProducto)
                    .Set("precioBase", producto.PrecioBase.PrecioBaseProducto)
                    .Set("descripcion", producto.Descripcion.DescripcionProducto)
                    .Set("imagen", producto.ImagenProducto.ImagenProducto)
                    .Set("cantidad", producto.CantidadProducto.CantidadProducto)
                    .Set("fechaActualizacion", DateTime.UtcNow);

                var productoModificado = await ProductoColexion.UpdateOneAsync(idProductoActualizar, actualizar);

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
                var idProductoEliminar = Builders<BsonDocument>.Filter.Eq("_id", idProducto);

                var productoEliminado = await ProductoColexion.DeleteOneAsync(idProductoEliminar);

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

        #region ObtenerProductoPorId(string idProducto)
        public async Task<Product?> ObtenerProductoPorId(string idProducto)
        {
            try
            {
                var idProductoBuscar = Builders<BsonDocument>.Filter.Eq("_id", idProducto);
                var productoEncontrado = await ProductoColexion.Find(idProductoBuscar).FirstOrDefaultAsync();
                if (productoEncontrado == null)
                {
                    return null;
                }
                return new Product(
                    new VOIdProducto(productoEncontrado["_id"].AsString),
                    new VONombreProducto(productoEncontrado["nombre"].AsString),
                    new VOCategoria(productoEncontrado["categoria"].AsString),
                    new VOPrecioBase(productoEncontrado["precioBase"].AsDecimal),
                    new VODescripcion(productoEncontrado["descripcion"].AsString),
                    new VOImagenProducto(productoEncontrado["imagen"].AsString),
                    new VOIdSubastador(productoEncontrado["idSubastador"].AsString),
                    new VOCantidad(productoEncontrado["cantidad"].AsInt32)
                );
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
