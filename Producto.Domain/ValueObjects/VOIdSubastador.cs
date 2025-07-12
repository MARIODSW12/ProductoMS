
namespace Producto.Domain.ValueObjects
{
    public class VOIdSubastador
    {
        public string IdSubastador { get; private set; }
        public VOIdSubastador(string idSubastador)
        {
            if (string.IsNullOrWhiteSpace(idSubastador))
                throw new ArgumentException("El ID de usuario no puede estar vacío.");

            if (!Guid.TryParse(idSubastador, out _))
                throw new ArgumentException("El ID de usuario debe ser un GUID válido.");

            IdSubastador = idSubastador;
        }
    }
}
