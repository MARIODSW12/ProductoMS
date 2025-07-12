using MongoDB.Driver;
using Producto.Domain.Exceptions;

namespace Producto.Infrastructure.Configurations
{
    public class MongoReadProductoDbConfig
    {
        public MongoClient client;
        public IMongoDatabase db;

        public MongoReadProductoDbConfig()
        {
            try
            {
                string connectionUri = Environment.GetEnvironmentVariable("MONGODB_CNN_READ");

                if (string.IsNullOrWhiteSpace(connectionUri))
                {
                    throw new ConexionBdProductoInvalida();
                }

                var settings = MongoClientSettings.FromConnectionString(connectionUri);
                settings.ServerApi = new ServerApi(ServerApiVersion.V1);

                client = new MongoClient(settings);

                string databaseName = Environment.GetEnvironmentVariable("MONGODB_NAME_READ");
                if (string.IsNullOrWhiteSpace(databaseName))
                {
                    throw new NombreBdInvalido();
                }

                db = client.GetDatabase(databaseName);
            }
            catch (MongoException ex)
            {
                throw new ErrorConexionBd();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
