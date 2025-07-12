using MongoDB.Bson.Serialization.Attributes;

namespace Producto.Infrastructure.Persistences.Repositories.MongoRead.Documents
{
    public class ProductoDocument
    {
        [BsonId]
        [BsonElement("id")]
        public required string IdProducto { get; set; }

        [BsonElement("nombre")]
        public required string NombreProducto { get; set; }

        [BsonElement("categoria")]
        public required string CatefgoriaProducto { get; set; }

        [BsonElement("precioBase")]
        public required decimal PrecioBase { get; set; }

        [BsonElement("descripcion")]
        public required string Descripcion { get; set; }

        [BsonElement("imagen")]
        public required string ImagenProducto { get; set; }

        [BsonElement("idSubastador")]
        public required string IdSubastador { get; set; }

        [BsonElement("cantidad")]
        public required int CantidadProducto { get; set; }
    }
}
