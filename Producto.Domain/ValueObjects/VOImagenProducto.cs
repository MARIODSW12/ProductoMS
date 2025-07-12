using System.Text.RegularExpressions;

namespace Producto.Domain.ValueObjects
{
    public class VOImagenProducto
    {
        private static readonly Regex UrlRegex = new Regex
        (
            @"^(https?|ftp):\/\/.*\.(jpg|jpeg|png|gif|bmp|webp)$",
            RegexOptions.IgnoreCase | RegexOptions.Compiled
        );
        public string ImagenProducto { get; private set; }
        public VOImagenProducto(string imagenProducto)
        {
            if (string.IsNullOrWhiteSpace(imagenProducto))
                throw new ArgumentException("El producto debe tener una imagen.");

            if (!UrlRegex.IsMatch(imagenProducto))
                throw new ArgumentException("La imagen del producto debe ser un URL válido.");

            ImagenProducto = imagenProducto;
        }
    }
}