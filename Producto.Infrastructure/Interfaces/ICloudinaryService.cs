using CloudinaryDotNet.Actions;

namespace Producto.Infrastructure.Interfaces
{
    public interface ICloudinaryService
    {
        Task<string> SubirImagen(Stream archivoStream, string nombreArchivo);
    }
}
